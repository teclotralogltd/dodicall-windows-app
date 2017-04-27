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
    public class ModelCall : AbstractModel<ModelCall>
    {
        private string _id;
        private int _duration;
        private string _identity;
        private ModelContact _modelContactObj;
        private ModelEnumCallDirection _modelEnumCallDirectionObj;
        private ModelEnumCallAddressType _modelEnumCallAddressTypeObj;
        private ModelEnumVoipEncryption _modelEnumVoipEncryptionObj;
        private ModelEnumCallState _modelEnumCallStateObj;

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

        /// <summary> Длительность звонка </summary>
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (_duration == value) return;
                _duration = value;
                OnPropertyChanged("Duration");
            }
        }

        /// <summary> Номер телефона </summary>
        public string Identity
        {
            get { return _identity; }
            set
            {
                if (_identity == value) return;
                _identity = value;
                OnPropertyChanged("Identity");
            }
        }

        /// <summary> Заголовок </summary>
        public string Title => ModelContactObj != null ? ModelContactObj.FullName : (_identity.Contains("@") ? _identity.Substring(0, _identity.LastIndexOf('@')) : _identity);

        /// <summary> Аватвр </summary>
        public BitmapImage Avatar => ModelContactObj != null ? ModelContactObj.Avatar : DataSourceContact.Avatar;

        /// <summary> Признак dodicall </summary>
        public bool IsDodicall => ModelContactObj != null ? ModelContactObj.IsDodicall : false;

        /// <summary> Объект контакта </summary>
        public ModelContact ModelContactObj
        {
            get { return _modelContactObj; }
            set
            {
                if (_modelContactObj == value) return;
                _modelContactObj = value;
                OnPropertyChanged("ModelContactObj");
            }
        }

        /// <summary> Тип вызова </summary>
        public ModelEnumCallDirection ModelEnumCallDirectionObj
        {
            get { return _modelEnumCallDirectionObj; }
            set
            {
                if (_modelEnumCallDirectionObj == value) return;
                _modelEnumCallDirectionObj = value;
                OnPropertyChanged("ModelEnumCallDirectionObj");
            }
        }

        /// <summary> Тип телефона </summary>
        public ModelEnumCallAddressType ModelEnumCallAddressTypeObj
        {
            get { return _modelEnumCallAddressTypeObj; }
            set
            {
                if (_modelEnumCallAddressTypeObj == value) return;
                _modelEnumCallAddressTypeObj = value;
                OnPropertyChanged("ModelEnumCallAddressTypeObj");
            }
        }

        /// <summary> Шифрование </summary>
        public ModelEnumVoipEncryption ModelEnumVoipEncryptionObj
        {
            get { return _modelEnumVoipEncryptionObj; }
            set
            {
                if (_modelEnumVoipEncryptionObj == value) return;
                _modelEnumVoipEncryptionObj = value;
                OnPropertyChanged("ModelEnumVoipEncryptionObj");
            }
        }

        /// <summary> Состояние звонка </summary>
        public ModelEnumCallState ModelEnumCallStateObj
        {
            get { return _modelEnumCallStateObj; }
            set
            {
                if (_modelEnumCallStateObj == value) return;
                _modelEnumCallStateObj = value;
                OnPropertyChanged("ModelEnumCallStateObj");
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelCall GetDeepCopy()
        {
            var result = new ModelCall
            {
                Id = Id,
                Duration = Duration,
                Identity = Identity,
                ModelContactObj = ModelContactObj?.GetDeepCopy(),
                ModelEnumCallDirectionObj = ModelEnumCallDirectionObj,
                ModelEnumCallAddressTypeObj = ModelEnumCallAddressTypeObj,
                ModelEnumVoipEncryptionObj = ModelEnumVoipEncryptionObj,
                ModelEnumCallStateObj = ModelEnumCallStateObj
            };

            return result;
        }
    }
}
