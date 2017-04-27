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
    public class ModelLogScope : AbstractNotifyPropertyChanged
    {
        private string _subject;
        private string _description;
        private bool _logHistoryCall = true;
        private bool _logQualityCall = true;
        private bool _logChat = true;
        private bool _logDatabase = true;
        private bool _logRequest = true;
        private bool _logVoip = true;
        private bool _logTrace = true;
        private bool _logGui = true;

        /// <summary> Номер заявки </summary>
        public int IssueId;

        /// <summary> Тема </summary>
        public string Subject
        {
            get { return _subject; }
            set
            {
                if (_subject == value) return;
                _subject = value;
                OnPropertyChanged("Subject");
            }
        }

        /// <summary> Описание </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary> Флаг отправки лога чата </summary>
        public bool LogChat
        {
            get { return _logChat; }
            set
            {
                if (_logChat == value) return;
                _logChat = value;
                OnPropertyChanged("LogChat");
            }
        }

        /// <summary> Флаг отправки лога базы данных </summary>
        public bool LogDatabase
        {
            get { return _logDatabase; }
            set
            {
                if (_logDatabase == value) return;
                _logDatabase = value;
                OnPropertyChanged("LogDatabase");
            }
        }

        /// <summary> Флаг отправки лога запросов </summary>
        public bool LogRequest
        {
            get { return _logRequest; }
            set
            {
                if (_logRequest == value) return;
                _logRequest = value;
                OnPropertyChanged("LogRequest");
            }
        }

        /// <summary> Флаг отправки лога звонков </summary>
        public bool LogVoip
        {
            get { return _logVoip; }
            set
            {
                if (_logVoip == value) return;
                _logVoip = value;
                OnPropertyChanged("LogVoip");
            }
        }

        /// <summary> Флаг отправки лога истории звонков </summary>
        public bool LogHistoryCall
        {
            get { return _logHistoryCall; }
            set
            {
                if (_logHistoryCall == value) return;
                _logHistoryCall = value;
                OnPropertyChanged("LogHistoryCall");
            }
        }

        /// <summary> Флаг отправки лога качества звонков </summary>
        public bool LogQualityCall
        {
            get { return _logQualityCall; }
            set
            {
                if (_logQualityCall == value) return;
                _logQualityCall = value;
                OnPropertyChanged("LogQualityCall");
            }
        }

        /// <summary> Флаг отправки лога отладки </summary>
        public bool LogTrace
        {
            get { return _logTrace; }
            set
            {
                if (_logTrace == value) return;
                _logTrace = value;
                OnPropertyChanged("LogTrace");
            }
        }

        /// <summary> Флаг отправки лога программы </summary>
        public bool LogGui
        {
            get { return _logGui; }
            set
            {
                if (_logGui == value) return;
                _logGui = value;
                OnPropertyChanged("LogGui");
            }
        }
    }
}
