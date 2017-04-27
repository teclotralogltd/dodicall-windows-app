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
using System.Windows.Media;
using DAL.Abstract;
using DAL.Localization;

namespace DAL.ModelEnum
{
    public class ModelEnumUserBaseStatus : AbstractModelEnum<ModelEnumUserBaseStatus>
    {
        ///// <summary> Список </summary>
        //private static List<ModelEnumUserBaseStatus> _listModelEnumUserBaseStatus;

        ///// <summary> Список </summary>
        //public static List<ModelEnumUserBaseStatus> ListModelEnumUserBaseStatus
        //{
        //    get
        //    {
        //        var i = LocalizationApp.GetInstance();

        //        if (_listModelEnumUserBaseStatus == null)
        //        {
        //            _listModelEnumUserBaseStatus = new List<ModelEnumUserBaseStatus>
        //            {
        //                new ModelEnumUserBaseStatus {Color = Brushes.LimeGreen, Code = 1, _keyName = @"ModelEnumUserBaseStatus_Online", _keyDescription = @"ModelEnumUserBaseStatus_Online"},
        //                new ModelEnumUserBaseStatus {Color = Brushes.DarkOrange, Code = 3, _keyName = @"ModelEnumUserBaseStatus_Dnd", _keyDescription = @"ModelEnumUserBaseStatus_Dnd"},
        //                new ModelEnumUserBaseStatus {Color = Brushes.LightGray, Code = 2, _keyName = @"ModelEnumUserBaseStatus_Hidden", _keyDescription = @"ModelEnumUserBaseStatus_Hidden"},
        //                new ModelEnumUserBaseStatus {Color = Brushes.LightGray, Code = 0, _keyName = @"ModelEnumUserBaseStatus_Offline", _keyDescription = @"ModelEnumUserBaseStatus_Offline"}
        //            };
        //        }

        //        return _listModelEnumUserBaseStatus;
        //    }
        //}

        ///// <summary> Получить ModelEnumUserBaseStatus по коду </summary>
        //public static ModelEnumUserBaseStatus GetModelEnumUserBaseStatus(int code)
        //{
        //    return ListModelEnumUserBaseStatus.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Кисть статуса </summary>
        public Brush Color { get; private set; }

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumUserBaseStatus>
            {
                new ModelEnumUserBaseStatus {Color = Brushes.LimeGreen, Code = 1, _keyName = @"ModelEnumUserBaseStatus_Online", _keyDescription = @"ModelEnumUserBaseStatus_Online"},
                new ModelEnumUserBaseStatus {Color = Brushes.DarkOrange, Code = 3, _keyName = @"ModelEnumUserBaseStatus_Dnd", _keyDescription = @"ModelEnumUserBaseStatus_Dnd"},
                new ModelEnumUserBaseStatus {Color = Brushes.LightGray, Code = 2, _keyName = @"ModelEnumUserBaseStatus_Hidden", _keyDescription = @"ModelEnumUserBaseStatus_Hidden"},
                new ModelEnumUserBaseStatus {Color = Brushes.LightGray, Code = 0, _keyName = @"ModelEnumUserBaseStatus_Offline", _keyDescription = @"ModelEnumUserBaseStatus_Offline"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumUserBaseStatus()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
