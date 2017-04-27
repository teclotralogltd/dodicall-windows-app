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
    public class ModelEnumVoipEncryption : AbstractModelEnum<ModelEnumVoipEncryption>
    {
        ///// <summary> Список типов шифрования </summary>
        //private static List<ModelEnumVoipEncryption> _listModelEnumVoipEncryption;

        ///// <summary> Список типов шифрования </summary>
        //public static List<ModelEnumVoipEncryption> ListModelEnumVoipEncryption
        //{
        //    get
        //    {
        //        if (_listModelEnumVoipEncryption == null)
        //        {
        //            _listModelEnumVoipEncryption = new List<ModelEnumVoipEncryption>
        //            {
        //                new ModelEnumVoipEncryption {Code = 0, _keyName = @"ModelEnumVoipEncryption_None", _keyDescription = @"ModelEnumVoipEncryption_None"},
        //                new ModelEnumVoipEncryption {Code = 1, _keyName = @"ModelEnumVoipEncryption_Srtp", _keyDescription = @"ModelEnumVoipEncryption_Srtp"}
        //            };
        //        }

        //        return _listModelEnumVoipEncryption;
        //    }
        //}

        ///// <summary> Получить ModelEnumVoipEncryption по коду </summary>
        //public static ModelEnumVoipEncryption GetModelEnumVoipEncryption(int code)
        //{
        //    return ListModelEnumVoipEncryption.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumVoipEncryption>
            {
                new ModelEnumVoipEncryption {Code = 0, _keyName = @"ModelEnumVoipEncryption_None", _keyDescription = @"ModelEnumVoipEncryption_None"},
                new ModelEnumVoipEncryption {Code = 1, _keyName = @"ModelEnumVoipEncryption_Srtp", _keyDescription = @"ModelEnumVoipEncryption_Srtp"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumVoipEncryption()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
