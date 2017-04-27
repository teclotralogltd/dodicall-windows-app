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
using DAL.Enum;
using Microsoft.Win32;

namespace DAL.Utility
{
    public class UtilitySystem
    {
        /// <summary> Возвращает версию операционной системы </summary>
        public static EnumVersionOS GetVersionOS()
        {
            var result = EnumVersionOS.Unknown;

            var registryKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\");

            var name = registryKey?.GetValue("ProductName")?.ToString();
            var version = registryKey?.GetValue("CurrentVersion")?.ToString();

            if (version == "6.1") result = EnumVersionOS.Windows7;

            if (version == "6.2") result = EnumVersionOS.Window8;

            if (version == "6.3") result = EnumVersionOS.Window81;

            if (version == "10.0") result = EnumVersionOS.Window10;

            // потому что когда 10 - установлена поверх старой системы (проапдейчена), то возвращает номер версии старой системы, но имя ОС новое возвращает, Microsoft идиоты !!!
            if (name != null && name.Contains("10")) result = EnumVersionOS.Window10;

            return result;
        }
    }
}
