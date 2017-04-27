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
    public class ModelEnumCallHistoryStatusType : AbstractModelEnum<ModelEnumCallHistoryStatusType>
    {
        ///// <summary> Список типов звонков </summary>
        //private static List<ModelEnumCallHistoryStatusType> _listModelEnumCallHistoryStatusType;

        ///// <summary> Список типов звонков </summary>
        //public static List<ModelEnumCallHistoryStatusType> ListModelEnumCallHistoryStatusType
        //{
        //    get
        //    {
        //        if (_listModelEnumCallHistoryStatusType == null)
        //        {
        //            _listModelEnumCallHistoryStatusType = new List<ModelEnumCallHistoryStatusType>
        //            {
        //                new ModelEnumCallHistoryStatusType {Code = 16, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusSuccess", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusSuccess"}, // звонок состоялся
        //                new ModelEnumCallHistoryStatusType {Code = 32, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusAborted", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusAborted"}, // звонок не состоялся
        //                new ModelEnumCallHistoryStatusType {Code = 64, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusMissed", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusMissed"}, // звонок пропущен
        //                new ModelEnumCallHistoryStatusType {Code = 128, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusDeclined", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusDeclined"}, // звонок сбросили на другой стороне (не используется в GUI)
        //                new ModelEnumCallHistoryStatusType {Code = 240, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusAny", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusAny"} // все звонки (только для фильтра)
        //            };
        //        }

        //        return _listModelEnumCallHistoryStatusType;
        //    }
        //}

        ///// <summary> Получить ModelEnumCallHistoryStatusType по коду </summary>
        //public static ModelEnumCallHistoryStatusType GetModelEnumCallHistoryStatusType(int code)
        //{
        //    return ListModelEnumCallHistoryStatusType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallHistoryStatusType>
            {
                new ModelEnumCallHistoryStatusType {Code = 16, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusSuccess", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusSuccess"}, // звонок состоялся
                new ModelEnumCallHistoryStatusType {Code = 32, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusAborted", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusAborted"}, // звонок не состоялся
                new ModelEnumCallHistoryStatusType {Code = 64, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusMissed", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusMissed"}, // звонок пропущен
                new ModelEnumCallHistoryStatusType {Code = 128, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusDeclined", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusDeclined"}, // звонок сбросили на другой стороне (не используется в GUI)
                new ModelEnumCallHistoryStatusType {Code = 240, _keyName = @"ModelEnumCallHistoryStatusType_HistoryStatusAny", _keyDescription = @"ModelEnumCallHistoryStatusType_HistoryStatusAny"} // все звонки (только для фильтра)
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallHistoryStatusType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
