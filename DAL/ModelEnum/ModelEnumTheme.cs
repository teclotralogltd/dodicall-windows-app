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
    public class ModelEnumTheme : AbstractModelEnum<ModelEnumTheme>
    {
        ///// <summary> Код </summary>
        //public string CodeName { get; set; }

        ///// <summary> Список </summary>
        //private static List<ModelEnumTheme> _listModelEnumTheme;

        ///// <summary> Список </summary>
        //public static List<ModelEnumTheme> ListModelEnumTheme
        //{
        //    get
        //    {
        //        if (_listModelEnumTheme == null)
        //        {
        //            _listModelEnumTheme = new List<ModelEnumTheme>
        //            {
        //                new ModelEnumTheme { Code = 0, CodeName = @"default", _keyName = @"ModelEnumTheme_default", _keyDescription = @"ModelEnumTheme_default" }
        //            };
        //        }

        //        return _listModelEnumTheme;
        //    }
        //}

        ///// <summary> Получить ModelEnumTheme по коду </summary>
        //public static ModelEnumTheme GetModelEnum(string code)
        //{
        //    // пока тема одна в этой строке нет смысла, по этому поменял что бы быстрее работало
        //    //return ListModelEnumTheme.FirstOrDefault(obj => obj.CodeName.Equals((String.IsNullOrWhiteSpace(code) ? @"default" : code), StringComparison.InvariantCultureIgnoreCase));

        //    return ListModelEnumTheme.First();
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumTheme>
            {
                new ModelEnumTheme { CodeName = @"default", _keyName = @"ModelEnumTheme_default", _keyDescription = @"ModelEnumTheme_default" }
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumTheme()
        {
            // пустой для сокрытия создания объектов
        }
    }
}

