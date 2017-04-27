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
    public class ModelEnumVideoSizeCell : AbstractModelEnum<ModelEnumVideoSizeCell>
    {
        ///// <summary> Список </summary>
        //private static List<ModelEnumVideoSizeCell> _listModelEnumVideoSizeCell;

        ///// <summary> Список </summary>
        //public static List<ModelEnumVideoSizeCell> ListModelEnumVideoSizeCell
        //{
        //    get
        //    {
        //        if (_listModelEnumVideoSizeCell == null)
        //        {
        //            _listModelEnumVideoSizeCell = new List<ModelEnumVideoSizeCell>
        //            {
        //                new ModelEnumVideoSizeCell {Code = 0, _keyName = @"ModelEnumVideoSizeCell_QVGA", _keyDescription = @"ModelEnumVideoSizeCell_QVGA"},
        //                new ModelEnumVideoSizeCell {Code = 1, _keyName = @"ModelEnumVideoSizeCell_VGA", _keyDescription = @"ModelEnumVideoSizeCell_VGA"},
        //                new ModelEnumVideoSizeCell {Code = 2, _keyName = @"ModelEnumVideoSizeCell_p720", _keyDescription = @"ModelEnumVideoSizeCell_p720"}
        //            };
        //        }

        //        return _listModelEnumVideoSizeCell;
        //    }
        //}

        ///// <summary> Получить ModelEnumVideoSizeCell по коду </summary>
        //public static ModelEnumVideoSizeCell GetModelEnumVideoSizeCell(int code)
        //{
        //    return ListModelEnumVideoSizeCell.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumVideoSizeCell>
            {
                new ModelEnumVideoSizeCell {Code = 0, _keyName = @"ModelEnumVideoSizeCell_QVGA", _keyDescription = @"ModelEnumVideoSizeCell_QVGA"},
                new ModelEnumVideoSizeCell {Code = 1, _keyName = @"ModelEnumVideoSizeCell_VGA", _keyDescription = @"ModelEnumVideoSizeCell_VGA"},
                new ModelEnumVideoSizeCell {Code = 2, _keyName = @"ModelEnumVideoSizeCell_p720", _keyDescription = @"ModelEnumVideoSizeCell_p720"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumVideoSizeCell()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
