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
    public class ModelEnumCurrency : AbstractModelEnum<ModelEnumCurrency>
    {
        ///// <summary> Список </summary>
        //private static List<ModelEnumCurrency> _listModelEnumCurrency;

        ///// <summary> Список </summary>
        //public static List<ModelEnumCurrency> ListModelEnumCurrency
        //{
        //    get
        //    {
        //        if (_listModelEnumCurrency == null)
        //        {
        //            _listModelEnumCurrency = new List<ModelEnumCurrency>
        //            {
        //                new ModelEnumCurrency {Code = 0, _keyName = @"ModelEnumCurrency_Rub", _keyDescription = @"ModelEnumCurrency_Rub"},
        //                new ModelEnumCurrency {Code = 1, _keyName = @"ModelEnumCurrency_Usd", _keyDescription = @"ModelEnumCurrency_Usd"},
        //                new ModelEnumCurrency {Code = 2, _keyName = @"ModelEnumCurrency_Eur", _keyDescription = @"ModelEnumCurrency_Eur"}
        //            };
        //        }

        //        return _listModelEnumCurrency;
        //    }
        //}

        ///// <summary> Получить ModelEnumCurrency по коду </summary>
        //public static ModelEnumCurrency GetModelEnumCurrency(int code)
        //{
        //    return ListModelEnumCurrency.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCurrency>
            {
                new ModelEnumCurrency {Code = 0, _keyName = @"ModelEnumCurrency_Rub", _keyDescription = @"ModelEnumCurrency_Rub"},
                new ModelEnumCurrency {Code = 1, _keyName = @"ModelEnumCurrency_Usd", _keyDescription = @"ModelEnumCurrency_Usd"},
                new ModelEnumCurrency {Code = 2, _keyName = @"ModelEnumCurrency_Eur", _keyDescription = @"ModelEnumCurrency_Eur"},
                new ModelEnumCurrency {Code = 3, _keyName = @"ModelEnumCurrency_Pound", _keyDescription = @"ModelEnumCurrency_Pound"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCurrency()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
