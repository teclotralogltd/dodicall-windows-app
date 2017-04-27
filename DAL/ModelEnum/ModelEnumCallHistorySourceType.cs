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
    public class ModelEnumCallHistorySourceType : AbstractModelEnum<ModelEnumCallHistorySourceType>
    {
        ///// <summary> Список типов звонков </summary>
        //private static List<ModelEnumCallHistorySourceType> _listModelEnumCallHistorySourceType;

        ///// <summary> Список типов звонков </summary>
        //public static List<ModelEnumCallHistorySourceType> ListModelEnumCallHistorySourceType
        //{
        //    get
        //    {
        //        if (_listModelEnumCallHistorySourceType == null)
        //        {
        //            _listModelEnumCallHistorySourceType = new List<ModelEnumCallHistorySourceType>
        //            {
        //                new ModelEnumCallHistorySourceType {Code = 1, _keyName = @"ModelEnumCallHistorySourceType_HistorySourcePhoneBook", _keyDescription = @"ModelEnumCallHistorySourceType_HistorySourcePhoneBook"}, // звонок на контакт который есть у тебя в друзьях
        //                new ModelEnumCallHistorySourceType {Code = 2, _keyName = @"ModelEnumCallHistorySourceType_HistorySourceOthers", _keyDescription = @"ModelEnumCallHistorySourceType_HistorySourceOthers"}, // звонок на контакт которого нет у тебя в друзьях
        //                new ModelEnumCallHistorySourceType {Code = 3, _keyName = @"ModelEnumCallHistorySourceType_HistorySourceAny", _keyDescription = @"ModelEnumCallHistorySourceType_HistorySourceAny"} // только для фильтра (вернуть всех)
        //            };
        //        }

        //        return _listModelEnumCallHistorySourceType;
        //    }
        //}

        ///// <summary> Получить ModelEnumCallHistorySourceType по коду </summary>
        //public static ModelEnumCallHistorySourceType GetModelEnumCallHistorySourceType(int code)
        //{
        //    return ListModelEnumCallHistorySourceType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallHistorySourceType>
            {
                new ModelEnumCallHistorySourceType {Code = 1, _keyName = @"ModelEnumCallHistorySourceType_HistorySourcePhoneBook", _keyDescription = @"ModelEnumCallHistorySourceType_HistorySourcePhoneBook"}, // звонок на контакт который есть у тебя в друзьях
                new ModelEnumCallHistorySourceType {Code = 2, _keyName = @"ModelEnumCallHistorySourceType_HistorySourceOthers", _keyDescription = @"ModelEnumCallHistorySourceType_HistorySourceOthers"}, // звонок на контакт которого нет у тебя в друзьях
                new ModelEnumCallHistorySourceType {Code = 3, _keyName = @"ModelEnumCallHistorySourceType_HistorySourceAny", _keyDescription = @"ModelEnumCallHistorySourceType_HistorySourceAny"} // только для фильтра (вернуть всех)
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallHistorySourceType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
