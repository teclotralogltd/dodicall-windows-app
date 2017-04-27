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

using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelEnum
{
    public class ModelEnumChatMessageSecurityLevel : AbstractModelEnum<ModelEnumChatMessageSecurityLevel>
    {
        /// <summary> Инициализация списка </summary>
        public static void Initialize()
        {
            ListModelEnum = new List<ModelEnumChatMessageSecurityLevel>
            {
                new ModelEnumChatMessageSecurityLevel { Code = 0, _keyName = @"ModelEnumChatMessageSecurityLevel_None", _keyDescription = @"ModelEnumChatMessageSecurityLevel_None" },
                new ModelEnumChatMessageSecurityLevel { Code = 1, _keyName = @"ModelEnumChatMessageSecurityLevel_Crypted", _keyDescription = @"ModelEnumChatMessageSecurityLevel_Crypted" }
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumChatMessageSecurityLevel()
        {
            // пустой для сокрытия создания объектов
        }

    }
}
