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
    public class ModelEnumNetworkTechnology : AbstractModelEnum<ModelEnumNetworkTechnology>
    {
        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumNetworkTechnology>
            {
                new ModelEnumNetworkTechnology {Code = 0, _keyName = @"ModelNetworkTechnology_None", _keyDescription = @"ModelNetworkTechnology_None"},
                new ModelEnumNetworkTechnology {Code = 1, _keyName = @"ModelNetworkTechnology_Wifi", _keyDescription = @"ModelNetworkTechnology_Wifi"},
                new ModelEnumNetworkTechnology {Code = 2, _keyName = @"ModelNetworkTechnology_g2", _keyDescription = @"ModelNetworkTechnology_g2"},
                new ModelEnumNetworkTechnology {Code = 3, _keyName = @"ModelNetworkTechnology_g3", _keyDescription = @"ModelNetworkTechnology_g3"},
                new ModelEnumNetworkTechnology {Code = 4, _keyName = @"ModelNetworkTechnology_g4", _keyDescription = @"ModelNetworkTechnology_g4"}
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumNetworkTechnology()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
