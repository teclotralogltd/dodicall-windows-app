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
using DAL.ModelEnum;
using DAL.Utility;
using DAL.WrapperBridge;

namespace DAL.Model
{
    public class ModelContact : AbstractModel<ModelContact>
    {
        private BitmapImage _avatar = DataSourceContact.Avatar;
        private int _id; // идентификатор контакта
        private bool _blocked; // признак заблокированности
        private bool _iam; // признак текущего пользователя
        private string _dodicallId; // у контактов из директории строка не пустая, у ручных он пустой, уточнить у Веры в чем отличие директ и ручных контактов
        private string _nativeId; // идентификатор обратный додикол, присутствует только у ручных
        private bool _white; // признак белого списка
        private string _xmppId; // идентификатор директорных на чат сервере, только у них есть статус
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private ModelEnumUserBaseStatus _modelEnumUserBaseStatusObj;
        private string _userExtendedStatus;
        private ModelContactSubscription _modelContactSubscriptionObj;

        private List<ModelUserContact> _listModelUserContact;
        private List<ModelUserContact> _listModelUserContactExtra;
        
        /// <summary> Аватар </summary>
        public BitmapImage Avatar
        {
            get { return _avatar; }
            set
            {
                if (_avatar == value) return;
                _avatar = value;
                OnPropertyChanged("Avatar");
            }
        }

        /// <summary> Идентификатор </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
                OnPropertyChanged("IsAccessStatus");
                OnPropertyChanged("IsFriend");
            }
        }

        /// <summary> Флаг блокировки пользователя </summary>
        public bool Blocked
        {
            get { return _blocked; }
            set
            {
                if (_blocked == value) return;
                _blocked = value;
                OnPropertyChanged("Blocked");
                OnPropertyChanged("IsAccessStatus");
            }
        }

        /// <summary> Флаг текущего пользователя </summary>
        public bool Iam
        {
            get { return _iam; }
            set
            {
                if (_iam == value) return;
                _iam = value;
                OnPropertyChanged("Iam");
            }
        }

        /// <summary> Идентификатор Dodicall (у ручных контактов он пустой) </summary>
        public string DodicallId
        {
            get { return _dodicallId; }
            set
            {
                if (_dodicallId == value) return;
                _dodicallId = value;
                OnPropertyChanged("DodicallId");
                OnPropertyChanged("IsDodicall");
                OnPropertyChanged("IsAccessStatus");
            }
        }

        /// <summary> Флаг контакта Dodicall </summary>
        public bool IsDodicall => !String.IsNullOrWhiteSpace(DodicallId);

        /// <summary> Флаг доступности статуса </summary>
        public bool IsAccessStatus
        {
            get
            {
                var result = true;

                if (String.IsNullOrWhiteSpace(DodicallId))
                {
                    result = false;
                }
                else
                {
                    var subscriptionStateCode = ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code;

                    result = subscriptionStateCode == 2 || subscriptionStateCode == 3;
                }

                return result;
            }
        }



        /// <summary> Идентификатор Manual (у Dodicall контактов он пустой) </summary>
        public string NativeId
        {
            get { return _nativeId; }
            set
            {
                if (_nativeId == value) return;
                _nativeId = value;
                OnPropertyChanged("NativeId");
            }
        }

        /// <summary> Флаг присутствия в белом списке пользователя </summary>
        public bool White
        {
            get { return _white; }
            set
            {
                if (_white == value) return;
                _white = value;
                OnPropertyChanged("White");
            }
        }

        /// <summary> Идентификатор Xmpp </summary>
        public string XmppId
        {
            get { return _xmppId; }
            set
            {
                if (_xmppId == value) return;
                _xmppId = value;
                OnPropertyChanged("XmppId");
            }
        }

        /// <summary> Имя </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary> Отчество </summary>
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (_middleName == value) return;
                _middleName = value;
                OnPropertyChanged("MiddleName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary> Фамилия </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary> Ф.И.О. </summary>
        public string FullName => (String.IsNullOrWhiteSpace(_firstName) ? "" : " " + _firstName) + (String.IsNullOrWhiteSpace(_middleName) ? "" : " " + _middleName) + (String.IsNullOrWhiteSpace(_lastName) ? "" : " " + _lastName);

        /// <summary> Статус пользователя </summary>
        public ModelEnumUserBaseStatus ModelEnumUserBaseStatusObj
        {
            get { return _modelEnumUserBaseStatusObj; }
            set
            {
                if (_modelEnumUserBaseStatusObj == value) return;
                _modelEnumUserBaseStatusObj = value;
                OnPropertyChanged("ModelEnumUserBaseStatusObj");
                OnPropertyChanged("IsAccessStatus");
            }
        }

        /// <summary> Расширенный статус </summary>
        public string UserExtendedStatus
        {
            get { return _userExtendedStatus; }
            set
            {
                if (_userExtendedStatus == value) return;
                _userExtendedStatus = value;
                OnPropertyChanged("UserExtendedStatus");
                OnPropertyChanged("UserExtendedStatusForFullStatus");
                OnPropertyChanged("IsAccessStatus");
            }
        }

        /// <summary> Расширенный статус с точкой </summary>
        public string UserExtendedStatusForFullStatus
        {
            get { return String.IsNullOrWhiteSpace(_userExtendedStatus) ? "" : ". " + _userExtendedStatus; }
        }

        /// <summary> Состояние доступности контакта </summary>
        public ModelContactSubscription ModelContactSubscriptionObj
        {
            get { return _modelContactSubscriptionObj; }
            set
            {
                if (_modelContactSubscriptionObj == value) return;
                _modelContactSubscriptionObj = value;
                OnPropertyChanged("ModelContactSubscriptionObj");
                OnPropertyChanged("IsAccessStatus");
            }
        }

        /// <summary> Список контактов </summary>
        public List<ModelUserContact> ListModelUserContact
        {
            get { return _listModelUserContact; }
            set
            {
                if (_listModelUserContact == value) return;
                _listModelUserContact = value;
                OnPropertyChanged("ListModelUserContact");
            }
        }

        /// <summary> Список доп. контактов </summary>
        public List<ModelUserContact> ListModelUserContactExtra
        {
            get { return _listModelUserContactExtra; }
            set
            {
                if (_listModelUserContactExtra == value) return;
                _listModelUserContactExtra = value;
                OnPropertyChanged("ListModelUserContactExtra");
            }
        }

        /// <summary> Флаг друга </summary>
        public bool IsFriend => _id > 0;

        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelContact GetDeepCopy()
        {
            return new ModelContact
            {
                Avatar = DataSourceContact.Avatar,
                Id = Id,
                Blocked = Blocked,
                Iam = Iam,
                DodicallId = DodicallId,
                NativeId = NativeId,
                White = White,
                XmppId = XmppId,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatusObj,
                UserExtendedStatus = UserExtendedStatus,
                ModelContactSubscriptionObj = ModelContactSubscriptionObj?.GetDeepCopy(),
                ListModelUserContact = ListModelUserContact.Select(obj => obj.GetDeepCopy()).ToList(),
                ListModelUserContactExtra = ListModelUserContactExtra.Select(obj => obj.GetDeepCopy()).ToList()
            };
        }
    }
}
