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
    public class ModelEnumUserContactType : AbstractModelEnum<ModelEnumUserContactType>
    {
        ///// <summary> Список </summary>
        //private static List<ModelEnumUserContactType> _listModelEnumUserContactType;

        ///// <summary> Список </summary>
        //public static List<ModelEnumUserContactType> ListModelEnumUserContactType
        //{
        //    get
        //    {
        //        if (_listModelEnumUserContactType == null)
        //        {
        //            _listModelEnumUserContactType = new List<ModelEnumUserContactType>
        //            {
        //                // не уберать комментарий !!! закоментировано что бы в карточке создания Manual контакта в списке телефонов был пока только "Телефон"
        //                //new ModelEnumUserContactType {Code = 0, _keyName = @"ModelEnumUserContactType_Sip", _keyDescription = @"ModelEnumUserContactType_Sip"},
        //                //new ModelEnumUserContactType {Code = 1, _keyName = @"ModelEnumUserContactType_Xmpp", _keyDescription = @"ModelEnumUserContactType_Xmpp"},
        //                new ModelEnumUserContactType {Code = 2, _keyName = @"ModelEnumUserContactType_Phone", _keyDescription = @"ModelEnumUserContactType_Phone"}
        //            };
        //        }

        //        return _listModelEnumUserContactType;
        //    }
        //}

        ///// <summary> Получить ModelEnumUserContactType по коду </summary>
        //public static ModelEnumUserContactType GetModelEnumUserContactType(int code)
        //{
        //    return ListModelEnumUserContactType.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumUserContactType>
            {
                // не уберать комментарий !!! закоментировано что бы в карточке создания Manual контакта в списке телефонов был пока только "Телефон"
                //new ModelEnumUserContactType {Code = 0, _keyName = @"ModelEnumUserContactType_Sip", _keyDescription = @"ModelEnumUserContactType_Sip"},
                //new ModelEnumUserContactType {Code = 1, _keyName = @"ModelEnumUserContactType_Xmpp", _keyDescription = @"ModelEnumUserContactType_Xmpp"},
                new ModelEnumUserContactType {Code = 2, _keyName = @"ModelEnumUserContactType_Phone", _keyDescription = @"ModelEnumUserContactType_Phone"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumUserContactType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
