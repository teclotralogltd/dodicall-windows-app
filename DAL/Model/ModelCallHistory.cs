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

namespace DAL.Model
{
    public class ModelCallHistory : AbstractModel<ModelCallHistory>
    {
        private int _totalMissed;
        private ObservableCollection<ModelCallHistoryPeer> _listModelCallHistoryPeer;

        /// <summary> Общее кол-во пропущеных звонков </summary>
        public int TotalMissed
        {
            get { return _totalMissed; }
            set
            {
                if (_totalMissed == value) return;
                _totalMissed = value;
                OnPropertyChanged("TotalMissed");
            }
        }

        /// <summary> Список статистических записей </summary>
        public ObservableCollection<ModelCallHistoryPeer> ListModelCallHistoryPeer
        {
            get { return _listModelCallHistoryPeer; }
            set
            {
                if (_listModelCallHistoryPeer == value) return;
                _listModelCallHistoryPeer = value;
                OnPropertyChanged("ListModelCallHistoryPeer");
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelCallHistory GetDeepCopy()
        {
            var result = new ModelCallHistory
            {
                TotalMissed = TotalMissed,
                ListModelCallHistoryPeer = new ObservableCollection<ModelCallHistoryPeer>()
            };

            foreach (var i in ListModelCallHistoryPeer)
            {
                result.ListModelCallHistoryPeer.Add(i.GetDeepCopy());
            }

            return result;
        }
    }
}
