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
    public class ModelEnumChatMessageType : AbstractModelEnum<ModelEnumChatMessageType>
    {
        ///// <summary> Список типов сообщений чатов </summary>
        //private static List<ModelEnumChatMessageType> _listModelEnumChatMessageType;

        ///// <summary> Список типов сообщений чатов </summary>
        //public static List<ModelEnumChatMessageType> ListModelEnumChatMessageType
        //{
        //    get
        //    {
        //        if (_listModelEnumChatMessageType == null)
        //        {
        //            _listModelEnumChatMessageType = new List<ModelEnumChatMessageType>
        //            {
        //                new ModelEnumChatMessageType {Code = 0, _keyName = @"ModelEnumChatMessageType_TextMessage", _keyDescription = @"ModelEnumChatMessageType_TextMessage"},
        //                new ModelEnumChatMessageType {Code = 1, _keyName = @"ModelEnumChatMessageType_Subject", _keyDescription = @"ModelEnumChatMessageType_Subject_Description"},
        //                new ModelEnumChatMessageType {Code = 2, _keyName = @"ModelEnumChatMessageType_AudioMessage", _keyDescription = @"ModelEnumChatMessageType_AudioMessage"},
        //                new ModelEnumChatMessageType {Code = 3, _keyName = @"ModelEnumChatMessageType_Notification", _keyDescription = @"ModelEnumChatMessageType_Notification"},
        //                new ModelEnumChatMessageType {Code = 4, _keyName = @"ModelEnumChatMessageType_Contact", _keyDescription = @"ModelEnumChatMessageType_Contact"},
        //                new ModelEnumChatMessageType {Code = 5, _keyName = @"ModelEnumChatMessageType_Deleter", _keyDescription = @"ModelEnumChatMessageType_Deleter"}
        //            };
        //        }

        //        return _listModelEnumChatMessageType;
        //    }
        //}

        ///// <summary> Получить ModelEnumChatMessageType по коду </summary>
        //public static ModelEnumChatMessageType GetModelEnumChatMessageType(int code)
        //{
        //    return ListModelEnumChatMessageType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumChatMessageType>
            {
                new ModelEnumChatMessageType {Code = 0, _keyName = @"ModelEnumChatMessageType_TextMessage", _keyDescription = @"ModelEnumChatMessageType_TextMessage"},
                new ModelEnumChatMessageType {Code = 1, _keyName = @"ModelEnumChatMessageType_Subject", _keyDescription = @"ModelEnumChatMessageType_Subject_Description"},
                new ModelEnumChatMessageType {Code = 2, _keyName = @"ModelEnumChatMessageType_AudioMessage", _keyDescription = @"ModelEnumChatMessageType_AudioMessage"},
                new ModelEnumChatMessageType {Code = 3, _keyName = @"ModelEnumChatMessageType_Notification", _keyDescription = @"ModelEnumChatMessageType_Notification"},
                new ModelEnumChatMessageType {Code = 4, _keyName = @"ModelEnumChatMessageType_Contact", _keyDescription = @"ModelEnumChatMessageType_Contact"},
                new ModelEnumChatMessageType {Code = 5, _keyName = @"ModelEnumChatMessageType_Deleter", _keyDescription = @"ModelEnumChatMessageType_Deleter"},
                new ModelEnumChatMessageType {Code = 6, _keyName = @"ModelEnumChatMessageType_Secured", _keyDescription = @"ModelEnumChatMessageType_Secured"},
                new ModelEnumChatMessageType {Code = 7, _keyName = @"ModelEnumChatMessageType_Draft", _keyDescription = @"ModelEnumChatMessageType_Draft"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumChatMessageType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
