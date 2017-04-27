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

using System.Collections.Generic;
using System.Linq;
using DAL.Abstract;

namespace DAL.ModelEnum
{
    public class ModelEnumSubscriptionState : AbstractModelEnum<ModelEnumSubscriptionState>
    {
        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumSubscriptionState>
            {
                new ModelEnumSubscriptionState {Code = 0, _keyName = @"ModelEnumSubscriptionState_None", _keyDescription = @"ModelEnumSubscriptionState_None"},
                new ModelEnumSubscriptionState {Code = 1, _keyName = @"ModelEnumSubscriptionState_From", _keyDescription = @"ModelEnumSubscriptionState_From"}, 
                new ModelEnumSubscriptionState {Code = 2, _keyName = @"ModelEnumSubscriptionState_To", _keyDescription = @"ModelEnumSubscriptionState_To"}, // нам отправили приглашение
                new ModelEnumSubscriptionState {Code = 3, _keyName = @"ModelEnumSubscriptionState_Both", _keyDescription = @"ModelEnumSubscriptionState_Both"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumSubscriptionState()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
