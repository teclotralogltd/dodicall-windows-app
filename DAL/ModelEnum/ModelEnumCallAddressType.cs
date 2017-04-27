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
    public class ModelEnumCallAddressType : AbstractModelEnum<ModelEnumCallAddressType>
    {
        ///// <summary> Список типов телефонов </summary>
        //private static List<ModelEnumCallAddressType> _listModelEnumCallAddressType;

        ///// <summary> Список типов телефонов </summary>
        //public static List<ModelEnumCallAddressType> ListModelEnumCallAddressType
        //{
        //    get
        //    {
        //        if (_listModelEnumCallAddressType == null)
        //        {
        //            _listModelEnumCallAddressType = new List<ModelEnumCallAddressType>
        //            {
        //                new ModelEnumCallAddressType {Code = 0, _keyName = @"ModelEnumCallAddressType_CallAddressPhone", _keyDescription = @"ModelEnumCallAddressType_CallAddressPhone"}, // вызов на внешний номер
        //                new ModelEnumCallAddressType {Code = 1, _keyName = @"ModelEnumCallAddressType_CallAddressDodicall", _keyDescription = @"ModelEnumCallAddressType_CallAddressDodicall"} // вызов внутренний
        //            };
        //        }

        //        return _listModelEnumCallAddressType;
        //    }
        //}

        ///// <summary> Получить ModelEnumCallAddressType по коду </summary>
        //public static ModelEnumCallAddressType GetModelEnumCallAddressType(int code)
        //{
        //    return ListModelEnumCallAddressType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallAddressType>
            {
                new ModelEnumCallAddressType {Code = 0, _keyName = @"ModelEnumCallAddressType_CallAddressPhone", _keyDescription = @"ModelEnumCallAddressType_CallAddressPhone"}, // вызов на внешний номер
                new ModelEnumCallAddressType {Code = 1, _keyName = @"ModelEnumCallAddressType_CallAddressDodicall", _keyDescription = @"ModelEnumCallAddressType_CallAddressDodicall"} // вызов внутренний
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallAddressType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
