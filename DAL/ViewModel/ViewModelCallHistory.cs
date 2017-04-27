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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Callback;
using DAL.Model;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelCallHistory : AbstractViewModel, IDisposable //, IDoCallback
    {
        /// <summary> Текущая история вызовов </summary>
        private ModelCallHistory _currentModelCallHistory;

        /// <summary> Текущая история вызовов </summary>
        public ModelCallHistory CurrentModelCallHistory
        {
            get { return _currentModelCallHistory; }
            set
            {
                if (_currentModelCallHistory == value) return;
                _currentModelCallHistory = value;
                OnPropertyChanged("CurrentModelCallHistory");

                // сделано для восстановления выделения текущего контакта после изменения списка
                // нужно для обновления из другого потока в C++ логике т.к. сами объекты могут отличаться и на всякий случай лучше вставлять объект из новой коллекции
                // мало вероятно что это будет другой объект, но на всякий случай + задел на будущее если вдруг схема поменяется
                //if (LastModelContact == null) return;
                //var modelContact = _currentListModelContact?.FirstOrDefault(obj => obj.Id == LastModelContact.Id); // обработать фильтр наложение
                //if (modelContact != null)
                //{
                //    _currentModelContact = modelContact; // св-во обновлять нельзя, т.к. если это будет один и тот же объект, то OnPropertyChanged не произойдет
                //    OnPropertyChanged("CurrentModelContact");
                //}
            }
        }
         
        /// <summary> Текущая запись истории </summary>
        private ModelCallHistoryPeer _currentModelCallHistoryPeer;

        /// <summary> Текущая запись истории </summary>
        public ModelCallHistoryPeer CurrentModelCallHistoryPeer
        {
            get { return _currentModelCallHistoryPeer; }
            set
            {
                if (_currentModelCallHistoryPeer == value) return;
                _currentModelCallHistoryPeer = value;
                OnPropertyChanged("CurrentModelCallHistoryPeer");
            }
        } 

        /// <summary> Конструктор </summary>
        public ViewModelCallHistory()
        {
            CurrentModelCallHistory = DataSourceCall.GetModelCallHistoryAllDetail();

            SortListModelCallHistoryPeer(); 
 
            CallbackRouter.Instance.ListModelContactChanged += InstanceOnListModelContactChanged;
            CallbackRouter.Instance.ModelCallHistoryChanged += OnModelCallHistoryChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged += OnListModelContactSubscriptionChanged; 
        }

        /// <summary> Обработчик изменения контактов </summary>
        private void InstanceOnListModelContactChanged(List<ModelContact> listChangedModelContact, List<ModelContact> listDeletedModelContact)
        {
            CurrentModelCallHistory = DataSourceCall.GetModelCallHistoryAllDetail();
        }

        /// <summary> Обработчик изменения истории вызовов </summary>
        private void OnModelCallHistoryChanged(object sender, ModelCallHistory modelCallHistory)
        {
            Action action = () => ChangeModelCallHistory(modelCallHistory);

            CurrentDispatcher.BeginInvoke(action);
        } 

        /// <summary> Обработчик изменения состояния подписок контактов </summary>
        private void OnListModelContactSubscriptionChanged(object sender, List<PackageModelContactSubscription> listPackageModelContactSubscriptions)
        {
            if (CurrentModelCallHistory == null) return;

            foreach (var i in listPackageModelContactSubscriptions)
            {
                var modelCallHistoryPeer = CurrentModelCallHistory.ListModelCallHistoryPeer.FirstOrDefault(obj => obj.ModelPeerObj.ModelContactObj != null ? obj.ModelPeerObj.ModelContactObj.XmppId == i.XmppId : false);
                
                if (modelCallHistoryPeer != null && modelCallHistoryPeer.ModelPeerObj.ModelContactObj.XmppId == i.XmppId)
                {
                    Action action = () => { modelCallHistoryPeer.ModelPeerObj.ModelContactObj.ModelContactSubscriptionObj = i.ModelContactSubscriptionObj.GetDeepCopy(); };

                    CurrentDispatcher.BeginInvoke(action);
                }
            }
        } 

        /// <summary> Обработчик изменения истории вызовов </summary>
        private void ChangeModelCallHistory(ModelCallHistory modelCallHistory)
        {
            var copyModelCallHistory = modelCallHistory.GetDeepCopy();

            foreach (var i in copyModelCallHistory.ListModelCallHistoryPeer)
            {
                var modelCallHistoryPeer = _currentModelCallHistory.ListModelCallHistoryPeer.FirstOrDefault(obj => DataSourceCall.CompareModelCallHistoryPeerId(obj, i) == 0);

                if (modelCallHistoryPeer == null)
                {
                    _currentModelCallHistory.ListModelCallHistoryPeer.Add(i);
                }
                else
                {
                    modelCallHistoryPeer.ModelCallStatisticsObj = i.ModelCallStatisticsObj;
                    modelCallHistoryPeer.ModelPeerObj = i.ModelPeerObj;
                    modelCallHistoryPeer.ListModelCallHistoryEntry = i.ListModelCallHistoryEntry;
                }
            }

            SortListModelCallHistoryPeer();

            var listMissedCount = CurrentModelCallHistory.ListModelCallHistoryPeer.Select(obj => obj.ModelCallStatisticsObj.NumberOfMissedCalls);

            if (listMissedCount.Any())
            {
                var missedCount = 0;

                foreach (var i in listMissedCount)
                {
                    missedCount += i;
                }

                CurrentModelCallHistory.TotalMissed = missedCount;
            }
        }

        /// <summary> Обработчик изменения истории вызовов </summary>
        private void SortListModelCallHistoryPeer()
        {
            var listModelCallHistoryPeer = _currentModelCallHistory.ListModelCallHistoryPeer.ToList();

            listModelCallHistoryPeer.Sort((modelCallHistoryPeer1, modelCallHistoryPeer2) => modelCallHistoryPeer2.ListModelCallHistoryEntry.Max(obj => obj.StartTime).CompareTo(modelCallHistoryPeer1.ListModelCallHistoryEntry.Max(obj => obj.StartTime)));

            CurrentModelCallHistory.ListModelCallHistoryPeer = new ObservableCollection<ModelCallHistoryPeer>(listModelCallHistoryPeer);
        }

        /// <summary> Пометить все пропущенные звонки прочитанными </summary>
        public void SetModelCallHistoryReadedAll()
        {
            if (CurrentModelCallHistory.TotalMissed == 0) return;

            DataSourceCall.SetCallHistoryReadedAll();

            foreach (var i in CurrentModelCallHistory.ListModelCallHistoryPeer)
            {
                i.ModelCallStatisticsObj.NumberOfMissedCalls = 0;
            }

            CurrentModelCallHistory.TotalMissed = 0;
        }

        /// <summary> Пометить пропущенные звонки прочитанными </summary>
        public void SetModelCallHistoryReaded(ModelCallHistoryPeer modelCallHistoryPeer)
        {
            DataSourceCall.SetCallHistoryReaded(modelCallHistoryPeer);

            CurrentModelCallHistory.TotalMissed -= modelCallHistoryPeer.ModelCallStatisticsObj.NumberOfMissedCalls;

            modelCallHistoryPeer.ModelCallStatisticsObj.NumberOfMissedCalls = 0;
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            CallbackRouter.Instance.ModelCallHistoryChanged -= OnModelCallHistoryChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged -= OnListModelContactSubscriptionChanged; 
        }
    }
}
