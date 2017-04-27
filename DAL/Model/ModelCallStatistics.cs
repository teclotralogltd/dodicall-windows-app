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

namespace DAL.Model
{
    public class ModelCallStatistics : AbstractModel<ModelCallStatistics>
    {
        private bool _hasIncomingEncryptedCall;
        private bool _hasOutgoingEncryptedCall;
        private int _numberOfIncomingSuccessfulCalls;
        private int _numberOfIncomingUnsuccessfulCalls;
        private int _numberOfOutgoingSuccessfulCalls;
        private int _numberOfOutgoingUnsuccessfulCalls;
        private int _numberOfMissedCalls;
        private bool _wasConference;

        /// <summary> Признак шифрования входящих звонков </summary>
        public bool HasIncomingEncryptedCall
        {
            get { return _hasIncomingEncryptedCall; }
            set
            {
                if (_hasIncomingEncryptedCall == value) return;
                _hasIncomingEncryptedCall = value;
                OnPropertyChanged("HasIncomingEncryptedCall");
            }
        }

        /// <summary> Признак шифрования исходящих звонков </summary>
        public bool HasOutgoingEncryptedCall
        {
            get { return _hasOutgoingEncryptedCall; }
            set
            {
                if (_hasOutgoingEncryptedCall == value) return;
                _hasOutgoingEncryptedCall = value;
                OnPropertyChanged("HasOutgoingEncryptedCall");
            }
        }

        /// <summary> Признак имеются ли пропущенные звонки </summary>
        public bool HasMissed => NumberOfMissedCalls > 0;

        /// <summary> Кол-во состоявщихся входящих звонков </summary>
        public int NumberOfIncomingSuccessfulCalls
        {
            get { return _numberOfIncomingSuccessfulCalls; }
            set
            {
                if (_numberOfIncomingSuccessfulCalls == value) return;
                _numberOfIncomingSuccessfulCalls = value;
                OnPropertyChanged("NumberOfIncomingSuccessfulCalls");
            }
        }

        /// <summary> Кол-во несостоявщихся входящих звонков </summary>
        public int NumberOfIncomingUnsuccessfulCalls
        {
            get { return _numberOfIncomingUnsuccessfulCalls; }
            set
            {
                if (_numberOfIncomingUnsuccessfulCalls == value) return;
                _numberOfIncomingUnsuccessfulCalls = value;
                OnPropertyChanged("NumberOfIncomingUnsuccessfulCalls");
            }
        }

        /// <summary> Кол-во состоявщихся исходящих звонков </summary>
        public int NumberOfOutgoingSuccessfulCalls
        {
            get { return _numberOfOutgoingSuccessfulCalls; }
            set
            {
                if (_numberOfOutgoingSuccessfulCalls == value) return;
                _numberOfOutgoingSuccessfulCalls = value;
                OnPropertyChanged("NumberOfOutgoingSuccessfulCalls");
            }
        }

        /// <summary> Кол-во несостоявщихся исходящих звонков </summary>
        public int NumberOfOutgoingUnsuccessfulCalls
        {
            get { return _numberOfOutgoingUnsuccessfulCalls; }
            set
            {
                if (_numberOfOutgoingUnsuccessfulCalls == value) return;
                _numberOfOutgoingUnsuccessfulCalls = value;
                OnPropertyChanged("NumberOfOutgoingUnsuccessfulCalls");
            }
        }

        /// <summary> Кол-во пропущеных звонков (отображать в названии в скобках и красных шрифтом если есть) </summary>
        public int NumberOfMissedCalls
        {
            get { return _numberOfMissedCalls; }
            set
            {
                if (_numberOfMissedCalls == value) return;
                _numberOfMissedCalls = value;
                OnPropertyChanged("NumberOfMissedCalls");
                OnPropertyChanged("HasMissed");
                OnPropertyChanged("NumberOfMissedCallsString");
            }
        }

        /// <summary> Кол-во пропущеных звонков строкой </summary>
        public string NumberOfMissedCallsString => "(" + NumberOfMissedCalls + ")";

        /// <summary> Признак конференции </summary>
        public bool WasConference
        {
            get { return _wasConference; }
            set
            {
                if (_wasConference == value) return;
                _wasConference = value;
                OnPropertyChanged("WasConference");
            }
        }
        
        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelCallStatistics GetDeepCopy()
        {
            var result = new ModelCallStatistics
            {
                HasIncomingEncryptedCall = HasIncomingEncryptedCall,
                HasOutgoingEncryptedCall = HasOutgoingEncryptedCall,
                NumberOfIncomingSuccessfulCalls = NumberOfIncomingSuccessfulCalls,
                NumberOfIncomingUnsuccessfulCalls = NumberOfIncomingUnsuccessfulCalls,
                NumberOfOutgoingSuccessfulCalls = NumberOfOutgoingSuccessfulCalls,
                NumberOfOutgoingUnsuccessfulCalls = NumberOfOutgoingUnsuccessfulCalls,
                NumberOfMissedCalls = NumberOfMissedCalls,
                WasConference = WasConference
            };

            return result;
        }
    }
}
