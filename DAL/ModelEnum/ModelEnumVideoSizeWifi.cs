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
    public class ModelEnumVideoSizeWifi : AbstractModelEnum<ModelEnumVideoSizeWifi>
    {
        ///// <summary> Список </summary>
        //private static List<ModelEnumVideoSizeWifi> _listModelEnumVideoSizeWifi;

        ///// <summary> Список </summary>
        //public static List<ModelEnumVideoSizeWifi> ListModelEnumVideoSizeWifi
        //{
        //    get
        //    {
        //        if (_listModelEnumVideoSizeWifi == null)
        //        {
        //            _listModelEnumVideoSizeWifi = new List<ModelEnumVideoSizeWifi>
        //            {
        //                new ModelEnumVideoSizeWifi {Code = 0, _keyName = @"ModelEnumVideoSizeWifi_QVGA", _keyDescription = @"ModelEnumVideoSizeWifi_QVGA"},
        //                new ModelEnumVideoSizeWifi {Code = 1, _keyName = @"ModelEnumVideoSizeWifi_VGA", _keyDescription = @"ModelEnumVideoSizeWifi_VGA"},
        //                new ModelEnumVideoSizeWifi {Code = 2, _keyName = @"ModelEnumVideoSizeWifi_p720", _keyDescription = @"ModelEnumVideoSizeWifi_p720"}
        //            };
        //        }

        //        return _listModelEnumVideoSizeWifi;
        //    }
        //}

        ///// <summary> Получить ModelEnumVideoSizeWifi по коду </summary>
        //public static ModelEnumVideoSizeWifi GetModelEnumVideoSizeWifi(int code)
        //{
        //    return ListModelEnumVideoSizeWifi.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumVideoSizeWifi>
            {
                new ModelEnumVideoSizeWifi {Code = 0, _keyName = @"ModelEnumVideoSizeWifi_QVGA", _keyDescription = @"ModelEnumVideoSizeWifi_QVGA"},
                new ModelEnumVideoSizeWifi {Code = 1, _keyName = @"ModelEnumVideoSizeWifi_VGA", _keyDescription = @"ModelEnumVideoSizeWifi_VGA"},
                new ModelEnumVideoSizeWifi {Code = 2, _keyName = @"ModelEnumVideoSizeWifi_p720", _keyDescription = @"ModelEnumVideoSizeWifi_p720"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumVideoSizeWifi()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
