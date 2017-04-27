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
using System.Windows.Media.Imaging;
using DAL.Abstract;
using DAL.Localization;
using DAL.Utility;
using DAL.WrapperBridge;
using DAL.ModelEnum;

namespace DAL.Model
{
    public class ModelChat : AbstractLocalization
    {
        private string _id;
        private List<ModelContact> _listModelContact;
        private bool _active;
        private ModelChatMessage _lastMessage;
        private DateTime _lastModifiedDate;
        private int _newMessagesCount;
        private string _title;
        private int _totalMessagesCount;
        private bool _isP2p;
        private bool _secured;
        private ModelChatMessage _draftMessage;

        /// <summary> Аватар </summary>
        public BitmapImage Avatar => IsP2P ? DataSourceContact.Avatar : DataSourceContact.AvatarGroup;

        /// <summary> Флаг группового чата </summary>
        public bool IsGroup => !IsP2P;

        /// <summary> Флаг p2p-чата </summary>
        public bool IsP2P
        {
            get { return _isP2p;}
            set
            {
                if (_isP2p == value) return;
                _isP2p = value;
                OnPropertyChanged("IsP2P");
            }
        }

        /// <summary> Флаг защищенности чата </summary>
        public bool Secured
        {
            get { return _secured; }
            set
            {
                if (_secured == value) return;
                _secured = value;
                OnPropertyChanged("Secured");
            }
        }
        
        /// <summary> Кол-во участников чата </summary>
        public int CountModelContact => ListModelContact.Count(obj => obj.Iam == false);

        /// <summary> Кол-во участников чата </summary>
        public string CountModelContactString
        {
            get
            {
                var count = CountModelContact;

                var result = count + " ";

                var localization = LocalizationApp.GetInstance();

                if (localization.ModelLanguageObj.CodeName.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                {
                    result += count == 1 ? localization.GetValueByKey(@"ModelChat_Participant1to1") : localization.GetValueByKey(@"ModelChat_Participant2to9");
                }

                if (localization.ModelLanguageObj.CodeName.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
                {
                    var last2Number = count % 100;

                    if (last2Number > 11 && last2Number < 19)
                    {
                        result += localization.GetValueByKey(@"ModelChat_Participant5to9");
                    }
                    else
                    {
                        var last1Number = last2Number % 10;

                        if (last1Number == 1) result += localization.GetValueByKey(@"ModelChat_Participant1to1");

                        if (last1Number > 1 && last1Number < 5) result += localization.GetValueByKey(@"ModelChat_Participant2to4");

                        if (last1Number < 1 || last1Number > 4) result += localization.GetValueByKey(@"ModelChat_Participant5to9");
                    }
                }

                return result;
            }
        }

        /// <summary> Собеседник с которым ведется разговор (актуально только при не групповом чате) </summary>
        public ModelContact ModelContactChat => ListModelContact.FirstOrDefault(obj => obj?.Iam == false);

        /// <summary> Идентификатор </summary>
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary> Список собеседников </summary>
        public List<ModelContact> ListModelContact
        {
            get { return _listModelContact; }
            set
            {
                if (_listModelContact == value) return;
                _listModelContact = value;
                OnPropertyChanged("ListModelContact");
                OnPropertyChanged("Avatar");
                OnPropertyChanged("IsGroup");
                OnPropertyChanged("ModelContactChat");
                OnPropertyChanged("CountModelContact");
                OnPropertyChanged("CountModelContactString");
                OnPropertyChanged("ListModelContactNotMe");
            }
        }

        /// <summary> Список собеседников в алфавитном порядке </summary>
        public List<ModelContact> ListModelContactNotMe
        {
            get
            {
                return ListModelContact.Where(obj => obj.Iam == false).ToList();
            }
        }

        /// <summary> Признак активного чата (в него можно писать сообщения) </summary>
        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active == value) return;
                _active = value;
                OnPropertyChanged("Active");
            }
        }

        /// <summary> Последнее сообщение </summary>
        public ModelChatMessage LastMessage
        {
            get { return _lastMessage; }
            set
            {
                if (_lastMessage == value) return;
                _lastMessage = value;
                OnPropertyChanged("LastMessage");
            }
        }

        /// <summary> Черновик </summary>
        public ModelChatMessage DraftMessage
        {
            get { return _draftMessage; }
            set
            {
                if (_draftMessage == value) return;
                _draftMessage = value;
                OnPropertyChanged("DraftMessage");
                OnPropertyChanged("HaveDraft");
            }
        }

        /// <summary> Есть ли черновик </summary>
        public bool HaveDraft => _draftMessage != null;
        
        /// <summary> Дата последнего активного действия (дата последнего сообщения если оно есть, если нет сообщений то дата создания диалога) </summary>
        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set
            {
                if (_lastModifiedDate == value) return;
                _lastModifiedDate = value;
                OnPropertyChanged("LastModifiedDate");
                OnPropertyChanged("LastModifiedDateString");
            }
        }

        /// <summary> Дата последнего активного действия строкой </summary>
        public string LastModifiedDateString => UtilityDate.ConvertShortDateString(LastModifiedDate);

        /// <summary> Кол-во непрочитанных сообщений </summary>
        public int NewMessagesCount
        {
            get { return _newMessagesCount; }
            set
            {
                if (_newMessagesCount == value) return;
                _newMessagesCount = value;
                OnPropertyChanged("NewMessagesCount");
                OnPropertyChanged("ExistNewMessages");
                OnPropertyChanged("NewMessagesOver");
                OnPropertyChanged("CountMessagesUnreadString");
            }
        }

        /// <summary> Признак наличия непрочитанных сообщений </summary>
        public bool ExistNewMessages => NewMessagesCount > 0;

        /// <summary> Количетсво непрочитанных сообщений строкой </summary>
        public string CountMessagesUnreadString => NewMessagesCount > 99 ? @"99+" : NewMessagesCount.ToString();

        /// <summary> Признак кол-ва непрочитанных сообщений более 99 </summary>
        public bool NewMessagesOver => NewMessagesCount > 99;

        /// <summary> Тема чата </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged("Title");
                OnPropertyChanged("CheckedTitle");
            }
        }

        /// <summary> Тема чата (КАСТЫЛЬ для DMC-5154)</summary>
        public string CheckedTitle
        {
            get
            {
                if (ListModelContact?.Count == 1 && String.IsNullOrWhiteSpace(Title))
                {
                    return LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_Untitled");
                }
                return Title;
            }
        }

        /// <summary> Oбщее кол-во сообщений </summary>
        public int TotalMessagesCount
        {
            get { return _totalMessagesCount; }
            set
            {
                if (_totalMessagesCount == value) return;
                _totalMessagesCount = value;
                OnPropertyChanged("TotalMessagesCount");
            }
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("LastModifiedDateString");
            OnPropertyChanged("CountModelContactString");
            OnPropertyChanged("CheckedTitle");
        }
    }
}
