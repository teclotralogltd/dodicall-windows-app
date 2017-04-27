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

using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Abstract;

namespace DAL.ModelEnum
{
    public class ModelEnumUserSettingsGroup : AbstractModelEnum<ModelEnumUserSettingsGroup>
    {
        ///// <summary> Код </summary>
        //public string CodeName { get; set; }

        ///// <summary> Список </summary>
        //private static List<ModelEnumUserSettingsGroup> _listModelEnumUserSettingsGroup;

        ///// <summary> Список </summary>
        //public static List<ModelEnumUserSettingsGroup> ListModelEnumUserSettingsGroup
        //{
        //    get
        //    {
        //        if (_listModelEnumUserSettingsGroup == null)
        //        {
        //            _listModelEnumUserSettingsGroup = new List<ModelEnumUserSettingsGroup>
        //            {
        //                new ModelEnumUserSettingsGroup {Code = 0, CodeName = @"common", _keyName = @"ModelEnumUserSettingsGroup_Common", _keyDescription = @"ModelEnumUserSettingsGroup_Common"},
        //                new ModelEnumUserSettingsGroup {Code = 1, CodeName = @"telecommunication", _keyName = @"ModelEnumUserSettingsGroup_Telecommunication", _keyDescription = @"ModelEnumUserSettingsGroup_Telecommunication"},
        //                new ModelEnumUserSettingsGroup {Code = 2, CodeName = @"chat", _keyName = @"ModelEnumUserSettingsGroup_Chat", _keyDescription = @"ModelEnumUserSettingsGroup_Chat"},
        //                new ModelEnumUserSettingsGroup {Code = 3, CodeName = @"guisettings", _keyName = @"ModelEnumUserSettingsGroup_GuiSettings", _keyDescription = @"ModelEnumUserSettingsGroup_GuiSettings"},
        //                new ModelEnumUserSettingsGroup {Code = 4, CodeName = @"information", _keyName = @"ModelEnumUserSettingsGroup_Information", _keyDescription = @"ModelEnumUserSettingsGroup_Information"},
        //                new ModelEnumUserSettingsGroup {Code = 5, CodeName = @"trace", _keyName = @"ModelEnumUserSettingsGroup_Trace", _keyDescription = @"ModelEnumUserSettingsGroup_Trace"}
        //            };
        //        }

        //        return _listModelEnumUserSettingsGroup;
        //    }
        //}

        ///// <summary> Получить ModelEnumUserSettingsGroup по коду </summary>
        //public static ModelEnumUserSettingsGroup GetModelEnumUserSettingsGroup(string code)
        //{
        //    return ListModelEnumUserSettingsGroup.FirstOrDefault(obj => obj.CodeName.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumUserSettingsGroup>
            {
                new ModelEnumUserSettingsGroup {Code = 0, CodeName = @"common", _keyName = @"ModelEnumUserSettingsGroup_Common", _keyDescription = @"ModelEnumUserSettingsGroup_Common"},
                new ModelEnumUserSettingsGroup {Code = 1, CodeName = @"security", _keyName = @"ModelEnumUserSettingsGroup_Security", _keyDescription = @"ModelEnumUserSettingsGroup_Security"},
                new ModelEnumUserSettingsGroup {Code = 2, CodeName = @"telecommunication", _keyName = @"ModelEnumUserSettingsGroup_Telecommunication", _keyDescription = @"ModelEnumUserSettingsGroup_Telecommunication"},
                new ModelEnumUserSettingsGroup {Code = 3, CodeName = @"chat", _keyName = @"ModelEnumUserSettingsGroup_Chat", _keyDescription = @"ModelEnumUserSettingsGroup_Chat"},
                new ModelEnumUserSettingsGroup {Code = 4, CodeName = @"guisettings", _keyName = @"ModelEnumUserSettingsGroup_GuiSettings", _keyDescription = @"ModelEnumUserSettingsGroup_GuiSettings"},
                new ModelEnumUserSettingsGroup {Code = 5, CodeName = @"information", _keyName = @"ModelEnumUserSettingsGroup_Information", _keyDescription = @"ModelEnumUserSettingsGroup_Information"},
                new ModelEnumUserSettingsGroup {Code = 6, CodeName = @"trace", _keyName = @"ModelEnumUserSettingsGroup_Trace", _keyDescription = @"ModelEnumUserSettingsGroup_Trace"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumUserSettingsGroup()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
