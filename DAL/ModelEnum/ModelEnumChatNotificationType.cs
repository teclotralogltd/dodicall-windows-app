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
    public class ModelEnumChatNotificationType : AbstractModelEnum<ModelEnumChatNotificationType>
    {
        ///// <summary> Список типов системных сообщений </summary>
        //private static List<ModelEnumChatNotificationType> _listModelEnumChatNotificationType;

        ///// <summary> Список типов системных сообщений </summary>
        //public static List<ModelEnumChatNotificationType> ListModelEnumChatNotificationType
        //{
        //    get
        //    {
        //        if (_listModelEnumChatNotificationType == null)
        //        {
        //            _listModelEnumChatNotificationType = new List<ModelEnumChatNotificationType>
        //            {
        //                new ModelEnumChatNotificationType {Code = 0, _keyName = @"ModelEnumChatNotificationType_Remove", _keyDescription = @"ModelEnumChatNotificationType_Create"},
        //                new ModelEnumChatNotificationType {Code = 1, _keyName = @"ModelEnumChatNotificationType_Invite", _keyDescription = @"ModelEnumChatNotificationType_Invite"},
        //                new ModelEnumChatNotificationType {Code = 2, _keyName = @"ModelEnumChatNotificationType_Revoke", _keyDescription = @"ModelEnumChatNotificationType_Revoke"},
        //                new ModelEnumChatNotificationType {Code = 3, _keyName = @"ModelEnumChatNotificationType_Leave", _keyDescription = @"ModelEnumChatNotificationType_Leave"},
        //                new ModelEnumChatNotificationType {Code = 4, _keyName = @"ModelEnumChatNotificationType_Remove", _keyDescription = @"ModelEnumChatNotificationType_Remove"}

        //            };
        //        }

        //        return _listModelEnumChatNotificationType;
        //    }
        //}

        ///// <summary> Получить ModelEnumChatNotificationType по коду </summary>
        //public static ModelEnumChatNotificationType GetModelEnumChatNotificationType(int code)
        //{
        //    return ListModelEnumChatNotificationType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumChatNotificationType>
            {
                new ModelEnumChatNotificationType {Code = 0, _keyName = @"ModelEnumChatNotificationType_Remove", _keyDescription = @"ModelEnumChatNotificationType_Create"},
                new ModelEnumChatNotificationType {Code = 1, _keyName = @"ModelEnumChatNotificationType_Invite", _keyDescription = @"ModelEnumChatNotificationType_Invite"},
                new ModelEnumChatNotificationType {Code = 2, _keyName = @"ModelEnumChatNotificationType_Revoke", _keyDescription = @"ModelEnumChatNotificationType_Revoke"},
                new ModelEnumChatNotificationType {Code = 3, _keyName = @"ModelEnumChatNotificationType_Leave", _keyDescription = @"ModelEnumChatNotificationType_Leave"},
                new ModelEnumChatNotificationType {Code = 4, _keyName = @"ModelEnumChatNotificationType_Remove", _keyDescription = @"ModelEnumChatNotificationType_Remove"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumChatNotificationType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
