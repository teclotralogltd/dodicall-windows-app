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
    public class ModelEnumCallHistoryAddressType : AbstractModelEnum<ModelEnumCallHistoryAddressType>
    {
        ///// <summary> Список типов звонков </summary>
        //private static List<ModelEnumCallHistoryAddressType> _listModelEnumHistoryAddressType;

        ///// <summary> Список типов звонков </summary>
        //public static List<ModelEnumCallHistoryAddressType> ListModelEnumHistoryAddressType
        //{
        //    get
        //    {
        //        if (_listModelEnumHistoryAddressType == null)
        //        {
        //            _listModelEnumHistoryAddressType = new List<ModelEnumCallHistoryAddressType>
        //            {
        //                new ModelEnumCallHistoryAddressType {Code = 28672, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeAny", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeAny"}, // все звонки, пока не используется, нужно будет для передачи фильтра
        //                new ModelEnumCallHistoryAddressType {Code = 4096, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypePhone", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypePhone"}, // звонок на телефон
        //                new ModelEnumCallHistoryAddressType {Code = 8192, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip11", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip11"}, // звонок на dodicall длиный номер
        //                new ModelEnumCallHistoryAddressType {Code = 16384, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip4", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip4"}, // звонок на dodicall короткий номер
        //            };
        //        }

        //        return _listModelEnumHistoryAddressType;
        //    }
        //}

        ///// <summary> Получить ModelHistoryAddressType по коду </summary>
        //public static ModelEnumCallHistoryAddressType GetModelEnumHistoryAddressType(int code)
        //{
        //    return ListModelEnumHistoryAddressType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallHistoryAddressType>
            {
                new ModelEnumCallHistoryAddressType {Code = 28672, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeAny", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeAny"}, // все звонки, пока не используется, нужно будет для передачи фильтра
                new ModelEnumCallHistoryAddressType {Code = 4096, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypePhone", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypePhone"}, // звонок на телефон
                new ModelEnumCallHistoryAddressType {Code = 8192, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip11", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip11"}, // звонок на dodicall длиный номер
                new ModelEnumCallHistoryAddressType {Code = 16384, _keyName = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip4", _keyDescription = @"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip4"}, // звонок на dodicall короткий номер
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallHistoryAddressType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
