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
    public class ModelEnumCallDirection : AbstractModelEnum<ModelEnumCallDirection>
    {
        ///// <summary> Список типов вызовов </summary>
        //private static List<ModelEnumCallDirection> _listModelEnumCallDirection;

        ///// <summary> Список типов вызовов </summary>
        //public static List<ModelEnumCallDirection> ListModelEnumCallDirection
        //{
        //    get
        //    {
        //        if (_listModelEnumCallDirection == null)
        //        {
        //            _listModelEnumCallDirection = new List<ModelEnumCallDirection>
        //            {
        //                new ModelEnumCallDirection {Code = 0, _keyName = @"ModelEnumCallDirection_Outgoing", _keyDescription = @"ModelEnumCallDirection_Outgoing"}, // исходящий вызов
        //                new ModelEnumCallDirection {Code = 1, _keyName = @"ModelEnumCallDirection_Incoming", _keyDescription = @"ModelEnumCallDirection_Incoming"}  // входящий вызов
        //            };
        //        }

        //        return _listModelEnumCallDirection;
        //    }
        //}

        ///// <summary> Получить ModelEnumCallDirection по коду </summary>
        //public static ModelEnumCallDirection GetModelEnumCallDirection(int code)
        //{
        //    return ListModelEnumCallDirection.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallDirection>
            {
                new ModelEnumCallDirection {Code = 0, _keyName = @"ModelEnumCallDirection_Outgoing", _keyDescription = @"ModelEnumCallDirection_Outgoing"}, // исходящий вызов
                new ModelEnumCallDirection {Code = 1, _keyName = @"ModelEnumCallDirection_Incoming", _keyDescription = @"ModelEnumCallDirection_Incoming"}  // входящий вызов
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallDirection()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
