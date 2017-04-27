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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using DAL.Abstract;
using DAL.Callback;
using DAL.Model;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelContactRequestInvite : AbstractViewModel
    {
        ///<summary> Событие успешного принятия приглашения </summary>
        public event EventHandler<ModelContact> ApplyInviteSuccessful;

        /// <summary> Количетсво непрочитанных приглашений </summary>
        private int _countInvateUnread;

        /// <summary> Количетсво непрочитанных приглашений </summary>
        public int CountInvateUnread
        {
            get { return _countInvateUnread; }
            set
            {
                if (_countInvateUnread == value) return;
                _countInvateUnread = value;
                OnPropertyChanged("CountInvateUnread");
                OnPropertyChanged("CountInvateUnreadString");
            }
        }

        /// <summary> Количетсво непрочитанных приглашений строкой </summary>
        public string CountInvateUnreadString => CountInvateUnread > 99 ? @"99+" : CountInvateUnread.ToString();

        ///<summary> Список контактов </summary>
        private List<ModelContact> _listModelContactInvite = new List<ModelContact>();

        ///<summary> Список контактов </summary>
        public List<ModelContact> ListModelContactInvite
        {
            get { return _listModelContactInvite; }
            set
            {
                if (_listModelContactInvite == value) return;
                _listModelContactInvite = value;
                OnPropertyChanged("ListModelContactInvite");
            }
        }

        ///<summary> Список контактов </summary>
        private List<ModelContact> _listModelContactRequest = new List<ModelContact>();

        ///<summary> Список контактов </summary>
        public List<ModelContact> ListModelContactRequest
        {
            get { return _listModelContactRequest; }
            set
            {
                if (_listModelContactRequest == value) return;
                _listModelContactRequest = value;
                OnPropertyChanged("ListModelContactRequest");
            }
        }

        ///<summary> Команда принять приглашение </summary>
        public Command CommandApplyInvite { get; set; }

        ///<summary> Конструктор </summary>
        public ViewModelContactRequestInvite()
        {
            CommandApplyInvite = new Command(obj => ApplyInvite(obj as ModelContact));

            CallbackRouter.Instance.ListModelContactSubscriptionChanged += OnListModelContactSubscriptionChanged;

            RefreshRequestInvite();
        }

        ///<summary> Метод принятия приглашения </summary>
        public void ApplyInvite(ModelContact modelContact)
        {
            if (modelContact == null) return;

            if (DataSourceContact.ApplyInviteModelContact(modelContact))
            {
                OnApplyInviteSuccessful(modelContact);
            }
        }

        ///<summary> Инвокатор события ApplyInviteSuccessful </summary>
        private void OnApplyInviteSuccessful(ModelContact modelContact)
        {
            ApplyInviteSuccessful?.Invoke(this, modelContact);
        }

        /// <summary> Обработчик изменения состояния подписок контактов </summary>
        private void OnListModelContactSubscriptionChanged(object sender, List<PackageModelContactSubscription> packageModelContactSubscriptions)
        {
            CurrentDispatcher.BeginInvoke(new Action(RefreshRequestInvite));
        }

        /// <summary> Метод обновления списков запросов и приглашений </summary>
        public void RefreshRequestInvite()
        {
            var listModelContactInvite = DataSourceContact.GetInviteListModelContact();

            // для сортировки сначала непрочитанные потом прочитанные
            ListModelContactInvite.Clear();
            ListModelContactInvite.AddRange(listModelContactInvite.FindAll(obj => obj.ModelContactSubscriptionObj.ModelEnumSubscriptionStatusObj.Code == 0));
            ListModelContactInvite.AddRange(listModelContactInvite.FindAll(obj => obj.ModelContactSubscriptionObj.ModelEnumSubscriptionStatusObj.Code != 0));

            ListModelContactRequest.Clear();
            ListModelContactRequest = DataSourceContact.GetRequestListModelContact();

            CountInvateUnread = DataSourceContact.GetCountInviteUnread();
        }

        ///<summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            // тут пишем если что то нужно обновить
        }
    }
}
