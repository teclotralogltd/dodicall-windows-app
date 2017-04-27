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
    public class ModelEnumCallHistoryEntryResult : AbstractModelEnum<ModelEnumCallHistoryEntryResult>
    {
        ///// <summary> Список типов звонков </summary>
        //private static List<ModelEnumCallHistoryEntryResult> _listModelEnumCallHistoryEntryResult;

        ///// <summary> Список типов звонков </summary>
        //public static List<ModelEnumCallHistoryEntryResult> ListModelEnumCallHistoryEntryResult
        //{
        //    get
        //    {
        //        if (_listModelEnumCallHistoryEntryResult == null)
        //        {
        //            _listModelEnumCallHistoryEntryResult = new List<ModelEnumCallHistoryEntryResult>
        //            {
        //                new ModelEnumCallHistoryEntryResult {Code = 0, _keyName = @"ModelEnumCallHistoryEntryResult_Incomming", _keyDescription = @"ModelEnumCallHistoryEntryResult_Incomming"}, // входящий
        //                new ModelEnumCallHistoryEntryResult {Code = 1, _keyName = @"ModelEnumCallHistoryEntryResult_IncomingFail", _keyDescription = @"ModelEnumCallHistoryEntryResult_IncomingFail"}, // пропущеный
        //                new ModelEnumCallHistoryEntryResult {Code = 2, _keyName = @"ModelEnumCallHistoryEntryResult_Outgoing", _keyDescription = @"ModelEnumCallHistoryEntryResult_Outgoing"}, // исходящий
        //                new ModelEnumCallHistoryEntryResult {Code = 3, _keyName = @"ModelEnumCallHistoryEntryResult_OutgoingFail", _keyDescription = @"ModelEnumCallHistoryEntryResult_OutgoingFail"}, // несостоявшийся
        //            };
        //        }

        //        return _listModelEnumCallHistoryEntryResult;
        //    }
        //}

        ///// <summary> Получить ModelHistoryAddressType по коду </summary>
        //public static ModelEnumCallHistoryEntryResult GetModelEnumHistoryAddressType(int code)
        //{
        //    return ListModelEnumCallHistoryEntryResult.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallHistoryEntryResult>
            {
                new ModelEnumCallHistoryEntryResult {Code = 0, _keyName = @"ModelEnumCallHistoryEntryResult_Incomming", _keyDescription = @"ModelEnumCallHistoryEntryResult_Incomming"}, // входящий
                new ModelEnumCallHistoryEntryResult {Code = 1, _keyName = @"ModelEnumCallHistoryEntryResult_IncomingFail", _keyDescription = @"ModelEnumCallHistoryEntryResult_IncomingFail"}, // пропущеный
                new ModelEnumCallHistoryEntryResult {Code = 2, _keyName = @"ModelEnumCallHistoryEntryResult_Outgoing", _keyDescription = @"ModelEnumCallHistoryEntryResult_Outgoing"}, // исходящий
                new ModelEnumCallHistoryEntryResult {Code = 3, _keyName = @"ModelEnumCallHistoryEntryResult_OutgoingFail", _keyDescription = @"ModelEnumCallHistoryEntryResult_OutgoingFail"}, // несостоявшийся
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallHistoryEntryResult()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
