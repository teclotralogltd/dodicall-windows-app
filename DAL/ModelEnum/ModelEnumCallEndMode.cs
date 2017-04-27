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
    public class ModelEnumCallEndMode : AbstractModelEnum<ModelEnumCallEndMode>
    {
        ///// <summary> Список типов звонков </summary>
        //private static List<ModelEnumCallEndMode> _listModelEnumCallEndMode;

        ///// <summary> Список типов звонков </summary>
        //public static List<ModelEnumCallEndMode> ListModelEnumCallEndMode
        //{
        //    get
        //    {
        //        if (_listModelEnumCallEndMode == null)
        //        {
        //            _listModelEnumCallEndMode = new List<ModelEnumCallEndMode>
        //            {
        //                new ModelEnumCallEndMode {Code = 0, _keyName = @"ModelEnumCallEndMode_CallEndModeNormalManaged", _keyDescription = @"ModelEnumCallEndMode_CallEndModeNormalManaged"}, // состоялся звонок
        //                new ModelEnumCallEndMode {Code = 1, _keyName = @"ModelEnumCallEndMode_CallEndModeCancelManaged", _keyDescription = @"ModelEnumCallEndMode_CallEndModeCancelManaged"} // не состоялся звонок
        //            };
        //        }

        //        return _listModelEnumCallEndMode;
        //    }
        //}

        ///// <summary> Получить ModelEnumCallEndMode по коду </summary>
        //public static ModelEnumCallEndMode GetModelEnumCallEndMode(int code)
        //{
        //    return ListModelEnumCallEndMode.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallEndMode>
            {
                new ModelEnumCallEndMode {Code = 0, _keyName = @"ModelEnumCallEndMode_CallEndModeNormalManaged", _keyDescription = @"ModelEnumCallEndMode_CallEndModeNormalManaged"}, // состоялся звонок
                new ModelEnumCallEndMode {Code = 1, _keyName = @"ModelEnumCallEndMode_CallEndModeCancelManaged", _keyDescription = @"ModelEnumCallEndMode_CallEndModeCancelManaged"} // не состоялся звонок
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallEndMode()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
