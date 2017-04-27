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
    public class ModelEnumSubscriptionStatus : AbstractModelEnum<ModelEnumSubscriptionStatus>
    {
        ///// <summary> Список статусов подписок </summary>
        //private static List<ModelEnumSubscriptionStatus> _listModelEnumSubscriptionStatus;

        ///// <summary> Список статусов подписок </summary>
        //public static List<ModelEnumSubscriptionStatus> ListModelEnumSubscriptionStatus
        //{
        //    get
        //    {
        //        if (_listModelEnumSubscriptionStatus == null)
        //        {
        //            _listModelEnumSubscriptionStatus = new List<ModelEnumSubscriptionStatus>
        //            {
        //                new ModelEnumSubscriptionStatus {Code = 0, _keyName = @"ModelEnumSubscriptionStatus_New", _keyDescription = @"ModelEnumSubscriptionStatus_New"},
        //                new ModelEnumSubscriptionStatus {Code = 1, _keyName = @"ModelEnumSubscriptionStatus_Readed", _keyDescription = @"ModelEnumSubscriptionStatus_Readed"},
        //                new ModelEnumSubscriptionStatus {Code = 2, _keyName = @"ModelEnumSubscriptionStatus_Confirmed", _keyDescription = @"ModelEnumSubscriptionStatus_Confirmed"}
        //            };
        //        }

        //        return _listModelEnumSubscriptionStatus;
        //    }
        //}

        ///// <summary> Получить ModelEnumSubscriptionStatus по коду </summary>
        //public static ModelEnumSubscriptionStatus GetModelEnumSubscriptionStatus(int code)
        //{
        //    return ListModelEnumSubscriptionStatus.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumSubscriptionStatus>
            {
                new ModelEnumSubscriptionStatus {Code = 0, _keyName = @"ModelEnumSubscriptionStatus_New", _keyDescription = @"ModelEnumSubscriptionStatus_New"},
                new ModelEnumSubscriptionStatus {Code = 1, _keyName = @"ModelEnumSubscriptionStatus_Readed", _keyDescription = @"ModelEnumSubscriptionStatus_Readed"},
                new ModelEnumSubscriptionStatus {Code = 2, _keyName = @"ModelEnumSubscriptionStatus_Confirmed", _keyDescription = @"ModelEnumSubscriptionStatus_Confirmed"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumSubscriptionStatus()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
