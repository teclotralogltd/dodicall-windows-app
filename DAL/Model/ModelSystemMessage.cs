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
    public class ModelSystemMessage : AbstractModel<ModelSystemMessage>
    {
        private string _prefix;
        private string _sender;
        private string _chatAction;
        private string _content;
        private string _postfix;

        /// <summary> Префикс системного сообщения </summary>
        public string Prefix
        {
            get { return _prefix; }
            set
            {
                if (_prefix == value) return;
                _prefix = value;
                OnPropertyChanged("Prefix");
            }
        }

        /// <summary> Отправитель системного сообщения </summary>
        public string Sender
        {
            get { return _sender; }
            set
            {
                if (_sender == value) return;
                _sender = value;
                OnPropertyChanged("Sender");
            }
        }

        /// <summary> Тип системного сообщения </summary>
        public string ChatAction
        {
            get { return _chatAction; }
            set
            {
                if (_chatAction == value) return;
                _chatAction = value;
                OnPropertyChanged("ChatAction");
            }
        }

        /// <summary> Содержание системного сообщения </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                if (_content == value) return;
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        /// <summary> Постфикс системного сообщения </summary>
        public string Postfix
        {
            get { return _postfix; }
            set
            {
                if (_postfix == value) return;
                _postfix = value;
                OnPropertyChanged("Postfix");
            }
        }

        /// <summary> Копирование системного сообщения </summary>
        public override ModelSystemMessage GetDeepCopy()
        {
            return new ModelSystemMessage
            {
                ChatAction = _chatAction,
                Sender = _sender,
                Prefix = _prefix,
                Content = _content
            };
        }
    }
}
