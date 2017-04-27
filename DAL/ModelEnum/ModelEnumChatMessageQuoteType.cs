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
    public class ModelEnumChatMessageQuoteType : AbstractModelEnum<ModelEnumChatMessageQuoteType>
    {
        /// <summary> Инициализация списка </summary>
        public static void Initialize()
        {
            ListModelEnum = new List<ModelEnumChatMessageQuoteType>
            {
                new ModelEnumChatMessageQuoteType { Code = 0, _keyName = @"ModelEnumChatMessageQuoteType_Answer", _keyDescription = @"ModelEnumChatMessageQuoteType_Answer" },
                new ModelEnumChatMessageQuoteType { Code = 1, _keyName = @"ModelEnumChatMessageQuoteType_Quote", _keyDescription = @"ModelEnumChatMessageQuoteType_Quote" },
                new ModelEnumChatMessageQuoteType { Code = 2, _keyName = @"ModelEnumChatMessageQuoteType_Forward", _keyDescription = @"ModelEnumChatMessageQuoteType_Forward" }
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumChatMessageQuoteType()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
