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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dodicall;
using DAL.Abstract;
using DAL.Model;
using DAL.ModelEnum;

namespace DAL.WrapperBridge
{
    internal class DataSourceUtility : AbstractDataSource
    {
        /// <summary> Конвертировать ModelConnectState из NetworkStateModelManaged </summary>
        private static ModelConnectState ConvertModelConnectStateFromNetworkStateModelManaged(NetworkStateModelManaged networkStateModelManaged)
        {
            var result = new ModelConnectState
            {
                ChatStatus = ModelEnumServerConnectionState.GetModelEnum((int)networkStateModelManaged.ChatStatus),
                VoipStatus = ModelEnumServerConnectionState.GetModelEnum((int)networkStateModelManaged.VoipStatus),
                ModelEnumNetworkTechnologyObj = ModelEnumNetworkTechnology.GetModelEnum((int)networkStateModelManaged.Technology)
            };

            return result;
        }

        /// <summary> Получить версию приложения </summary>
        public static string GetVersionApp()
        {
            var buildConfiguration = String.Empty;

            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision} - {Logic.GetLibVersion()}" + buildConfiguration;
        }

        /// <summary> Получить текущий объект состояния сети </summary>
        public static ModelConnectState GetCurrentModelConnectState()
        {
            var test = Logic.GetNetworkState();

            return ConvertModelConnectStateFromNetworkStateModelManaged(test);
        }
    }
}
