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
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Localization;
using DAL.Utility;

namespace DAL.Model
{
    public class ModelCallHistoryPeer : AbstractLocalization
    {
        private ModelPeer _modelPeerObj;
        private ModelCallStatistics _modelCallStatisticsObj;
        private List<ModelCallHistoryEntry> _listModelCallHistoryEntry;

        /// <summary> Объект звонка </summary>
        public ModelPeer ModelPeerObj
        {
            get { return _modelPeerObj; }
            set
            {
                if (_modelPeerObj == value) return;
                _modelPeerObj = value;
                OnPropertyChanged("ModelPeerObj");
            }
        }

        /// <summary> Дата последнего звонка </summary>
        public string LastModifiedDateString => UtilityDate.ConvertShortDateString(_listModelCallHistoryEntry.Max(obj => obj.StartTime));

        /// <summary> Объект статистики </summary>
        public ModelCallStatistics ModelCallStatisticsObj
        {
            get { return _modelCallStatisticsObj; }
            set
            {
                if (_modelCallStatisticsObj == value) return;
                _modelCallStatisticsObj = value;
                OnPropertyChanged("ModelCallStatisticsObj");
            }
        }

        /// <summary> Список звонков </summary>
        public List<ModelCallHistoryEntry> ListModelCallHistoryEntry
        {
            get { return _listModelCallHistoryEntry; }
            set
            {
                if (_listModelCallHistoryEntry == value) return;
                _listModelCallHistoryEntry = value;
                OnPropertyChanged("ListModelCallHistoryEntry");
                OnPropertyChanged("LastModifiedDateString");
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public ModelCallHistoryPeer GetDeepCopy()
        {
            var result = new ModelCallHistoryPeer
            {
                ModelPeerObj = ModelPeerObj.GetDeepCopy(),
                ModelCallStatisticsObj = ModelCallStatisticsObj.GetDeepCopy(),
                ListModelCallHistoryEntry = new List<ModelCallHistoryEntry>()
            };

            foreach (var i in ListModelCallHistoryEntry)
            {
                result.ListModelCallHistoryEntry.Add(i.GetDeepCopy());
            }

            return result;
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("LastModifiedDateString");
        }
    }
}
