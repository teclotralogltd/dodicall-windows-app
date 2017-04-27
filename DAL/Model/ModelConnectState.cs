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
using DAL.ModelEnum;

namespace DAL.Model
{
    public class ModelConnectState : AbstractModel<ModelConnectState>
    {
        private ModelEnumServerConnectionState _chatStatus;
        private ModelEnumServerConnectionState _voipStatus;
        private ModelEnumNetworkTechnology _modelEnumNetworkTechnologyObj;

        /// <summary> Статус доступности чат сервера </summary>
        public ModelEnumServerConnectionState ChatStatus
        {
            get { return _chatStatus; }
            set
            {
                if (_chatStatus == value) return;
                _chatStatus = value;
                OnPropertyChanged("ChatStatus");
            }
        }

        /// <summary> Статус доступности сервера звонков </summary>
        public ModelEnumServerConnectionState VoipStatus
        {
            get { return _voipStatus; }
            set
            {
                if (_voipStatus == value) return;
                _voipStatus = value;
                OnPropertyChanged("VoipStatus");
            }
        }

        /// <summary> Тип подключения </summary>
        public ModelEnumNetworkTechnology ModelEnumNetworkTechnologyObj
        {
            get { return _modelEnumNetworkTechnologyObj; }
            set
            {
                if (_modelEnumNetworkTechnologyObj == value) return;
                _modelEnumNetworkTechnologyObj = value;
                OnPropertyChanged("ModelEnumNetworkTechnologyObj");
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelConnectState GetDeepCopy()
        {
            var result = new ModelConnectState
            {
                _chatStatus = _chatStatus,
                _voipStatus = _voipStatus,
                _modelEnumNetworkTechnologyObj = _modelEnumNetworkTechnologyObj
            };

            return result;
        }
    }
}
