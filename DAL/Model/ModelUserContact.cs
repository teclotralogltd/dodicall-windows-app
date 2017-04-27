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
    public class ModelUserContact : AbstractModel<ModelUserContact>
    {
        private bool _favourite;
        private string _identity;
        private bool _manual;
        private ModelEnumUserContactType _modelEnumUserContactTypeObj;

        /// <summary> Флаг избранное </summary>
        public bool Favourite
        {
            get { return _favourite; }
            set
            {
                if (_favourite == value) return;
                _favourite = value;
                OnPropertyChanged("Favourite");
            }
        }

        /// <summary> Номер </summary>
        public string Identity
        {
            get
            {
                return _identity;
            }
            set
            {
                if (_identity == value) return;
                _identity = value;
                OnPropertyChanged("Identity");
            }
        }

        /// <summary> Номер </summary>
        public string IdentityString
        {
            get
            {
                var result = String.Empty;

                if (_identity != null) result = _identity.Contains("@") ? _identity.Substring(0, _identity.LastIndexOf('@')) : _identity;

                return result;
            }
        }

        /// <summary> Флаг ручной </summary>
        public bool Manual
        {
            get { return _manual; }
            set
            {
                if (_manual == value) return;
                _manual = value;
                OnPropertyChanged("Manual");
            }
        }

        /// <summary> Тип </summary>
        public ModelEnumUserContactType ModelEnumUserContactTypeObj
        {
            get { return _modelEnumUserContactTypeObj; }
            set
            {
                if (_modelEnumUserContactTypeObj == value) return;
                _modelEnumUserContactTypeObj = value;
                OnPropertyChanged("ModelEnumUserContactTypeObj");
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelUserContact GetDeepCopy()
        {
            return new ModelUserContact
            {
                Favourite = Favourite,
                Identity = Identity,
                Manual = Manual,
                ModelEnumUserContactTypeObj = ModelEnumUserContactTypeObj
            };
        }
    }
}
