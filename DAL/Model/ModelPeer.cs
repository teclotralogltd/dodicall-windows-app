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
using System.Windows.Media.Imaging;
using DAL.WrapperBridge;

namespace DAL.Model
{
    public class ModelPeer : AbstractModel<ModelPeer>
    {
        private string _id;
        private string _identity;
        private ModelContact _modelContactObj;
        private ModelEnumCallAddressType _modelEnumCallAddressTypeObj;

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

        /// <summary> Номер телефона </summary>
        public string IdentityString => _identity.Contains("@") ? _identity.Substring(0, _identity.LastIndexOf('@')) : _identity;

        /// <summary> Аватар</summary>
        public BitmapImage Avatar => ExistModelContact ? ModelContactObj.Avatar : DataSourceContact.Avatar;

        /// <summary> Флаг наличия объекта ModelContactObj</summary>
        public bool ExistModelContact => ModelContactObj != null;

        /// <summary> Флаг записи ModelPeer для контакта Dodicall</summary>
        public bool IsDodicall => ExistModelContact ? ModelContactObj.IsDodicall : false;

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
        
        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelPeer GetDeepCopy()
        {
            var result = new ModelPeer
            {
                Id = Id,
                Identity = Identity,
                ModelContactObj = ModelContactObj?.GetDeepCopy(),
                ModelEnumCallAddressTypeObj = ModelEnumCallAddressTypeObj
            };

            return result;
        }
    }
}
