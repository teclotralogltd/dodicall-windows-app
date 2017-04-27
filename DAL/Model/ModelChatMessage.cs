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
using DAL.ModelEnum;
using System.Collections.ObjectModel;

namespace DAL.Model
{
    public class ModelChatMessage : AbstractLocalization
    {
        
        private string _id;
        private int _rowNum;
        private bool _readed;
        private bool _changed;
        private bool _servered;
        private string _idChat;
        private bool _encrypted;
        private bool _justAnswer;
        private DateTime _sendTime;
        private ModelContact _sender;
        private string _stringContent;
        private ModelContact _modelContactData;
        private ModelNotificationData _modelNotificationDataObj;
        private ModelEnumChatMessageType _modelEnumChatMessageTypeObj;
        private ModelEnumChatMessageQuoteType _modelEnumChatMessageQuoteTypeObj;
        private ObservableCollection<ModelChatMessage> _listQuotedModelChatMessage;
        private ModelEnumChatMessageSecurityLevel _modelEnumChatMessageSecurityLevelObj;

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

        /// <summary> Идентификатор чата </summary>
        public string IdChat
        {
            get { return _idChat; }
            set
            {
                if (_idChat == value) return;
                _idChat = value;
                OnPropertyChanged("IdChat");
            }
        }

        /// <summary> Объект контакта (если в сообщении отправлена карточка контакта) </summary>
        public ModelContact ModelContactData
        {
            get { return _modelContactData; }
            set
            {
                if (_modelContactData == value) return;
                _modelContactData = value;
                OnPropertyChanged("ModelContactData");
            }
        }

        /// <summary> Объект системного сообщения </summary>
        public ModelNotificationData ModelNotificationDataObj
        {
            get { return _modelNotificationDataObj; }
            set
            {
                if (_modelNotificationDataObj == value) return;
                _modelNotificationDataObj = value;
                OnPropertyChanged("ModelNotificationDataObj");
            }
        }

        /// <summary> Признак прочитанности сообщения </summary>
        public bool Readed
        {
            get { return _readed; }
            set
            {
                if (_readed == value) return;
                _readed = value;
                OnPropertyChanged("Readed");
            }
        }

        /// <summary> Признак отредактированости сообщения </summary>
        public bool Changed
        {
            get { return _changed; }
            set
            {
                if (_changed == value) return;
                _changed = value;
                OnPropertyChanged("Changed");
            }
        }

        /// <summary> Дата отправки </summary>
        public DateTime SendTime
        {
            get { return _sendTime; }
            set
            {
                if (_sendTime == value) return;
                _sendTime = value;
                OnPropertyChanged("SendTime");
            }
        }

        /// <summary> Объект контакта отправителя </summary>
        public ModelContact Sender
        {
            get { return _sender; }
            set
            {
                if (_sender == value) return;
                _sender = value;
                OnPropertyChanged("Sender");
            }
        }

        /// <summary> Признак доставки сообщения на сервер </summary>
        public bool Servered
        {
            get { return _servered; }
            set
            {
                if (_servered == value) return;
                _servered = value;
                OnPropertyChanged("Servered");
            }
        }

        /// <summary> Признак зашифрованости сообщения </summary>
        public bool Encrypted
        {
            get { return _encrypted; }
            set
            {
                if (_encrypted == value) return;
                _encrypted = value;
                OnPropertyChanged("Encrypted");
            }
        }

        /// <summary> Флаг цитаты без текста </summary>
        public bool IsQuotationWithoutText => (HaveQuoted && String.IsNullOrWhiteSpace(StringContent));

        /// <summary> Текст сообщения </summary>
        public string StringContent
        {
            get { return _stringContent; }
            set
            {
                if (_stringContent == value) return;
                _stringContent = value;
                OnPropertyChanged("StringContent");
                OnPropertyChanged("StringContentText");
                OnPropertyChanged("StringContentLine");
                OnPropertyChanged("ModelSystemMessageObj");
            }
        }

        /// <summary> Контент системного сообщения </summary>
        public ModelSystemMessage ModelSystemMessageObj
        {
            get
            {
                var message = new ModelSystemMessage();

                var result = String.Empty;

                if (ModelEnumChatMessageTypeObj.Code == 3)
                {
                    message.Prefix = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User");
                    message.Sender = Sender.FullName;
                    message.ChatAction = ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Description;

                    if (ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 1 || ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 2)
                    {
                        foreach (var i in ModelNotificationDataObj.ListModelContact)
                        {
                            if (i != null)
                                result += (i.FullName + ", ");

                        }

                        result = result.Length < 2 ? result : result.Substring(0, result.Length - 2);
                        
                        message.Postfix = ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 1
                            ? LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_ToChat")
                            : LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_FromChat");
                    }

                    message.Content = result;
                }
                else
                {
                    if (ModelEnumChatMessageTypeObj.Code == 1)
                    {
                        message.Prefix = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User");
                        message.Sender = Sender.FullName;
                        message.ChatAction = ModelEnumChatMessageTypeObj?.Name;
                        message.Content = StringContent;
                    }
                }

                return message;
            }
        }

        /// <summary> Текст контента сообщения </summary>
        public string StringContentText
        {
            get
            {
                var result = String.Empty;

                if (ModelEnumChatMessageTypeObj == null) return result;

                if (IsQuotationWithoutText)
                {
                    result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_Quoted");
                }
                else
                {
                    if (ModelEnumChatMessageTypeObj.Code == 0 || ModelEnumChatMessageTypeObj.Code == 7) result = _stringContent; // текстовое сообщение
                } 
                
                if (ModelEnumChatMessageTypeObj.Code == 3) // системное сообщение
                {
                    if (ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 0)  // create
                    {
                        result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User") + " "+ Sender.FullName +" " + ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Description;
                    }

                    if (ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 1) // invite
                    {
                        result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User") + " " + Sender.FullName + " " + ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Description + " ";

                        foreach (var i in ModelNotificationDataObj.ListModelContact)
                        {
                            if (i != null) result += (i.FullName + ", ");
                        }

                        result = result.Substring(0, result.Length - 2);
                    }

                    if (ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 2)  // revoke
                    {
                        result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User") + " " + Sender.FullName + " " + ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Description + " ";

                        foreach (var i in ModelNotificationDataObj.ListModelContact)
                        {
                            result += (i.FullName + ", ");
                        }

                        result = result.Substring(0, result.Length - 2);
                    }

                    if (ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 3)  // leave
                    {
                        result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User") + " " + Sender.FullName + " " + ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Description;
                    }

                    if (ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code == 4) // remove
                    {
                        result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User") + " " + Sender.FullName + " " + ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Description;
                    }
                }

                if (ModelEnumChatMessageTypeObj.Code == 1) // переименование чата
                {
                    result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_User") + " " + Sender?.FullName + " " + ModelEnumChatMessageTypeObj?.Name + " " + StringContent;
                }

                if (ModelEnumChatMessageTypeObj.Code != 0 && ModelEnumChatMessageTypeObj.Code != 1 && ModelEnumChatMessageTypeObj.Code != 3 && ModelEnumChatMessageTypeObj.Code != 7) result = ModelEnumChatMessageTypeObj.Description; // остальные варианты

                return result;
            }
        }

        public int Rownum
        {
            get { return _rowNum; }
            set
            {
                if (_rowNum == value) return;
                _rowNum = value;
                OnPropertyChanged("Rownum");
            }
        }

        /// <summary> Текст сообщения одной строкой </summary>
        public string StringContentLine => StringContentText.Replace("\n", " ");

        /// <summary> Тип сообщения </summary>
        public ModelEnumChatMessageType ModelEnumChatMessageTypeObj
        {
            get { return _modelEnumChatMessageTypeObj; }
            set
            {
                if (_modelEnumChatMessageTypeObj == value) return;
                _modelEnumChatMessageTypeObj = value;
                OnPropertyChanged("ModelEnumChatMessageTypeObj");
            }
        }

        /// <summary> Тип цитированости сообщения </summary>
        public ModelEnumChatMessageQuoteType ModelEnumChatMessageQuoteTypeObj
        {
            get { return _modelEnumChatMessageQuoteTypeObj; }
            set
            {
                if (_modelEnumChatMessageQuoteTypeObj == value) return;
                _modelEnumChatMessageQuoteTypeObj = value;
                OnPropertyChanged("ModelEnumChatMessageQuoteTypeObj");
            }
        }

        /// <summary> Индикатор защищённости сообщения </summary>
        public ModelEnumChatMessageSecurityLevel ModelEnumChatMessageSecurityLevelObj
        {
            get { return _modelEnumChatMessageSecurityLevelObj; }
            set
            {
                if (_modelEnumChatMessageSecurityLevelObj == value) return;
                _modelEnumChatMessageSecurityLevelObj = value;
                OnPropertyChanged(@"ModelEnumChatMessageSecurityLevelObj");
            }
        }

        /// <summary> Флаг прикрепленных сообщений </summary>
        public bool HaveQuoted
        {
            get
            {
                if (_listQuotedModelChatMessage != null) return _listQuotedModelChatMessage.Count > 0;
                else return false;

            }
        }

        /// <summary> Простой ответ или цитирование </summary>
        public bool JustAnswer
        {
            get { return _justAnswer; }
            set
            {
                if (_justAnswer == value) return;
                _justAnswer = value;
                OnPropertyChanged("JustAnswer");
            }
        }

        /// <summary> Список вложеных сообщений </summary>
        public ObservableCollection<ModelChatMessage> ListQuotedModelChatMessage
        {
            get { return _listQuotedModelChatMessage; }
            set
            {
                if (_listQuotedModelChatMessage == value) return;
                _listQuotedModelChatMessage = value;
                OnPropertyChanged("ListQuotedModelChatMessage");
                OnPropertyChanged("HaveQuoted");
            }
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("StringContentText");
            OnPropertyChanged("StringContentLine");
            OnPropertyChanged("ModelSystemMessageObj");
        }
    }
}
