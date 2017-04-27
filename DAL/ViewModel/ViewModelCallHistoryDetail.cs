//
//  Copyright (C) 2016, Telco Cloud Trading & Logistic Ltd
//
//  This file is part of dodicall.
//  dodicall is free software : you can redistribute it and / or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  dodicall is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with dodicall.If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Callback;
using DAL.Model;
using DAL.WrapperBridge;
using System.Collections.ObjectModel;

namespace DAL.ViewModel
{
    public class ViewModelCallHistoryDetail : AbstractViewModel, IDisposable //, IDoCallback
    {
        /// <summary> Текущий контакт </summary>
        private ModelContact _modelContactObj;

        /// <summary> Текущий контакт </summary>
        public ModelContact ModelContactObj
        {
            get { return _modelContactObj; }
            set
            {
                if (_modelContactObj == value) return;
                _modelContactObj = value;
                OnPropertyChanged("ModelContactObj");
            }
        }

        ///<summary> Заголовок шапки </summary>
        public string Title => ModelCallHistoryPeerObj.ModelPeerObj.ModelContactObj != null ? ModelCallHistoryPeerObj.ModelPeerObj.ModelContactObj.FullName : ModelCallHistoryPeerObj.ModelPeerObj.IdentityString;

        ///<summary> Событие успешной отправки заявки в друзья </summary>
        public event EventHandler SendRequestSuccessful;

        /// <summary> Флаг отображения кнопки звонка </summary>
        public bool ShowCallButton => ModelContactObj != null ? ModelContactObj.IsDodicall : false;

        /// <summary> Флаг отображения кнопки чата </summary>
        public bool ShowChatButton => ModelContactObj != null ? ModelContactObj.IsAccessStatus && ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 && !ModelContactObj.Blocked : false;

        /// <summary> Флаг отображения кнопки видеовызова </summary>
        public bool ShowVideoButton => ModelContactObj != null ? ModelContactObj.IsAccessStatus && ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 && !ModelContactObj.Blocked : false;

        /// <summary> Флаг отображения кнопки отправки запроса </summary>
        public bool ShowSendRequestButton => ModelContactObj != null ? ModelContactObj.IsDodicall && !ModelContactObj.Blocked && !(ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3) && !ModelContactObj.ModelContactSubscriptionObj.Ask : false;

        /// <summary> Флаг отображения кнопки сохранения в контакты </summary> 
        public bool ShowSaveAsNewButton => ModelContactObj != null ? !ModelContactObj.IsDodicall && !(ModelContactObj.Id > 0) : true;

        /// <summary> Флаг отображения кнопки звонка PSNT </summary>
        public bool ShowCallPSNTButton => ModelContactObj != null ? !ModelContactObj.IsDodicall : true;

        /// <summary> Флаг отображения панели со статусом </summary>
        public bool ShowStatusPanel => ModelContactObj != null ? ModelContactObj.IsAccessStatus && !ModelContactObj.Blocked && ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 : false;
        
        /// <summary> Флаг отображения панели со статусом </summary>
        public bool ShowBlockedPanel => ModelContactObj != null ? ModelContactObj.Blocked : false;

        /// <summary> Текущая история вызовов </summary>
        private ModelCallHistoryPeer _modelCallHistoryPeerObj;
         
        /// <summary> Текущая история вызовов </summary>
        public ModelCallHistoryPeer ModelCallHistoryPeerObj
        {
            get { return _modelCallHistoryPeerObj; }
            set
            {
                if (_modelCallHistoryPeerObj == value) return;
                _modelCallHistoryPeerObj = value;
                OnPropertyChanged("ModelCallHistoryPeerObj");
            }
        } 

        /// <summary> Контструктор </summary>
        public ViewModelCallHistoryDetail(ModelCallHistoryPeer modelCallHistoryPeer)
        {
            ModelCallHistoryPeerObj = modelCallHistoryPeer;
            ModelContactObj = ModelCallHistoryPeerObj.ModelPeerObj.ModelContactObj;
 
            CommandSendRequest = new Command(obj => SendRequest());

            //    SendRequestSuccessful += (sender, args) => ViewContact.CurrentViewContact.AddContactListBoxContact(args, false);

            if (ModelContactObj != null)
            {
                if (ModelContactObj.IsDodicall) DataSourceContact.RefreshModelContactStatus(ModelContactObj);

                ModelContactObj.PropertyChanged += ModelContactObj_PropertyChanged;
            }

            CallbackRouter.Instance.ListModelContactChanged += InstanceOnListModelContactChanged;
            CallbackRouter.Instance.ModelCallHistoryChanged += OnModelCallHistoryChanged;
            CallbackRouter.Instance.ListModelContactStatusChanged += OnListModelContactStatusChanged;
        }

        /// <summary> Обработчик изменения контактов </summary>
        private void InstanceOnListModelContactChanged(List<ModelContact> listChangedModelContact, List<ModelContact> listDeletedModelContact)
        {
            var modelCallHistoryDetail = DataSourceCall.GetChangedModelCallHistoryDetail(_modelCallHistoryPeerObj);

            Action action = () =>
            {
                ModelCallHistoryPeerObj.ListModelCallHistoryEntry = modelCallHistoryDetail.ListModelCallHistoryPeer.First().GetDeepCopy().ListModelCallHistoryEntry;
            };

            CurrentDispatcher.BeginInvoke(action);
        }

        /// <summary> Обработчик изменения ModelContact </summary>
        private void ModelContactObj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { 
            if (e.PropertyName == "Blocked")
            {
                OnPropertyChanged("ShowChatButton");
                OnPropertyChanged("ShowVideoButton");
                OnPropertyChanged("ShowStatusPanel");
            }
            if (e.PropertyName == "ModelContactSubscriptionObj")
            {
                OnPropertyChanged("ShowChatButton");
                OnPropertyChanged("ShowVideoButton");
                OnPropertyChanged("ShowSendRequestButton");
                OnPropertyChanged("ShowStatusPanel");
            } 
        }

        /// <summary> Обработчик изменения истории вызовов </summary>
        private void OnModelCallHistoryChanged(object sender, ModelCallHistory modelCallHistory)
        {
            if (modelCallHistory.ListModelCallHistoryPeer.Any(obj => obj.ModelPeerObj.Id == _modelCallHistoryPeerObj.ModelPeerObj.Id))
            {
                var modelCallHistoryDetail = DataSourceCall.GetChangedModelCallHistoryDetail(_modelCallHistoryPeerObj);

                Action action = () =>
                {
                    ModelCallHistoryPeerObj.ListModelCallHistoryEntry = modelCallHistoryDetail.ListModelCallHistoryPeer.First().GetDeepCopy().ListModelCallHistoryEntry; 
                };

                CurrentDispatcher.BeginInvoke(action);
            }
        }

        /// <summary> Обработчик изменения статусов контактов </summary>
        private void OnListModelContactStatusChanged(object sender, List<PackageModelContactStatus> listPackageModelContactStatuses)
        {
            if (_modelContactObj != null && listPackageModelContactStatuses.Any(obj => obj.XmppId == _modelContactObj.XmppId))
            {
                var packageModelContactStatus = listPackageModelContactStatuses.First(obj => obj.XmppId == _modelContactObj.XmppId);

                Action action = () =>
                {
                    ModelContactObj.ModelEnumUserBaseStatusObj = packageModelContactStatus.ModelEnumUserBaseStatusObj;

                    ModelContactObj.UserExtendedStatus = packageModelContactStatus.UserExtendedStatus;
                };

                CurrentDispatcher.BeginInvoke(action);
            }
        }

        ///// <summary> Обработчик изменения модели внутри логики C++ </summary>
        //public void DoCallback(object sender, DoCallbackArgs e)
        //{
            //if (e.ModelName == "History" && e.EntityIds.Contains(_modelCallHistoryPeerObj.ModelPeerObj.Id))
            //{
            //    var modelCallHistory = _dataSourceCall.GetChangedModelCallHistoryDetail(_modelCallHistoryPeerObj);

            //    Action action = () =>
            //    {
            //        ModelCallHistoryPeerObj.ListModelCallHistoryEntry = modelCallHistory.ListModelCallHistoryPeer.First().GetDeepCopy().ListModelCallHistoryEntry;

            //        OnEventViewModel("ListCallChanged");
            //    };

            //    CurrentDispatcher.BeginInvoke(action);
            //}

            //if (_modelContactObj != null && e.ModelName == "ContactsPresence" && e.EntityIds.Contains(_modelContactObj.XmppId))
            //{
            //    Action action = () => _dataSourceContact.RefreshModelContactStatus(ModelContactObj);

            //    CurrentDispatcher.BeginInvoke(action);
            //}
        //}

        ///<summary> Команда отправки запроса контакту </summary>
        public Command CommandSendRequest { get; set; }

        ///<summary> Метод отправки запроса контакту </summary>
        public void SendRequest()
        {
            if (_modelContactObj == null) return;

            if (DataSourceContact.SendRequestModelContact(_modelContactObj))
            {
                DataSourceContact.RefreshModelContactStatus(_modelContactObj); // кастылек, СВ сказал вручную после принятия заявки запрашивать статус, т.к. callback со статусом не прилетает

                OnSendRequestSuccessful();
            }
        }

        ///<summary> Инвокатор события SendRequestSuccessful </summary>
        private void OnSendRequestSuccessful()
        {
            SendRequestSuccessful?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            CallbackRouter.Instance.ModelCallHistoryChanged -= OnModelCallHistoryChanged;
            CallbackRouter.Instance.ListModelContactStatusChanged -= OnListModelContactStatusChanged;

            if (ModelContactObj != null) ModelContactObj.PropertyChanged -= ModelContactObj_PropertyChanged; 
        } 
    }
}
