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
    public class ModelContactSubscription : AbstractModel<ModelContactSubscription>
    {
        private bool _ask;
        private ModelEnumSubscriptionStatus _modelEnumSubscriptionStatusObj;
        private ModelEnumSubscriptionState _modelEnumSubscriptionStateObj;

        /// <summary> Флаг запроса на подтверждение добавления в друзья </summary>
        public bool Ask
        {
            get { return _ask; }
            set
            {
                if (_ask == value) return;
                _ask = value;
                OnPropertyChanged("Ask");
            }
        }

        /// <summary> Статус заявки </summary>
        public ModelEnumSubscriptionStatus ModelEnumSubscriptionStatusObj
        {
            get { return _modelEnumSubscriptionStatusObj; }
            set
            {
                if (_modelEnumSubscriptionStatusObj == value) return;
                _modelEnumSubscriptionStatusObj = value;
                OnPropertyChanged("ModelEnumSubscriptionStatusObj");
            }
        }

        /// <summary> Состояние отношений контакта </summary>
        public ModelEnumSubscriptionState ModelEnumSubscriptionStateObj
        {
            get { return _modelEnumSubscriptionStateObj; }
            set
            {
                if (_modelEnumSubscriptionStateObj == value) return;
                _modelEnumSubscriptionStateObj = value;
                OnPropertyChanged("ModelEnumSubscriptionStateObj");
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public override ModelContactSubscription GetDeepCopy()
        {
            return new ModelContactSubscription
            {
                Ask = Ask,
                ModelEnumSubscriptionStatusObj = ModelEnumSubscriptionStatusObj,
                ModelEnumSubscriptionStateObj = ModelEnumSubscriptionStateObj
            };
        }
    }
}
