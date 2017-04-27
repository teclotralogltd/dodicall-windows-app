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
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;

namespace DAL.ModelEnum
{
    public class ModelEnumServerConnectionState : AbstractModelEnum<ModelEnumServerConnectionState>
    {
        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumServerConnectionState>
            {
                new ModelEnumServerConnectionState {Code = 0, _keyName = @"ModelEnumServerConnectionState_None", _keyDescription = @"ModelEnumServerConnectionState_None"},
                new ModelEnumServerConnectionState {Code = 1, _keyName = @"ModelEnumServerConnectionState_Failed", _keyDescription = @"ModelEnumServerConnectionState_Failed"},
                new ModelEnumServerConnectionState {Code = 2, _keyName = @"ModelEnumServerConnectionState_Progress", _keyDescription = @"ModelEnumServerConnectionState_Progress"},
                new ModelEnumServerConnectionState {Code = 3, _keyName = @"ModelEnumServerConnectionState_Success", _keyDescription = @"ModelEnumServerConnectionState_Success"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumServerConnectionState()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
