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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enum;
using Microsoft.Win32;

namespace DAL.Utility
{
    public class UtilityWeb
    {
        /// <summary> Открывает ссылку в браузере </summary>
        public static void OpenUrl(string url)
        {
            if (String.IsNullOrWhiteSpace(url)) return;

            var webBrowser = GetWebBrowserSetAsDefault();

            if (webBrowser == EnumWebBrowser.IE)
            {
                Process.Start("IExplore.exe", url);
            }
            else
            {
                Process.Start(url);
            }
        }

        /// <summary> Возвращает Веб-браузер установленный по умолчанию </summary>
        public static EnumWebBrowser GetWebBrowserSetAsDefault()
        {
            var result = EnumWebBrowser.Other;

            var versionOS = UtilitySystem.GetVersionOS();

            if (versionOS == EnumVersionOS.Windows7)
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice");

                var val = registryKey?.GetValue("ProgId")?.ToString();

                if (val != null)
                {
                    if (val.ToLower().Contains("ie")) result = EnumWebBrowser.IE; //IE.HTTP

                    if (val.ToLower().Contains("chrome")) result = EnumWebBrowser.Chrome; //ChromeHTML

                    if (val.ToLower().Contains("firefox")) result = EnumWebBrowser.Firefox; //FirefoxURL

                    if (val.ToLower().Contains("opera")) result = EnumWebBrowser.Opera; //OperaStable
                }
            }

            if (versionOS == EnumVersionOS.Window8 || versionOS == EnumVersionOS.Window81)
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice");

                var val = registryKey?.GetValue("ProgId")?.ToString();

                if (val != null)
                {
                    if (val.ToLower().Contains("ie")) result = EnumWebBrowser.IE; //IE.HTTP

                    if (val.ToLower().Contains("chrome")) result = EnumWebBrowser.Chrome; //ChromeHTML

                    if (val.ToLower().Contains("firefox")) result = EnumWebBrowser.Firefox; //FirefoxURL

                    if (val.ToLower().Contains("opera")) result = EnumWebBrowser.Opera; //OperaStable
                }
            }

            if (versionOS == EnumVersionOS.Window10)
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice");

                var val = registryKey?.GetValue("ProgId")?.ToString();

                if (val != null)
                {
                    if (val.ToLower().Contains("ie")) result = EnumWebBrowser.IE; //IE.HTTP

                    if (val.ToLower().Contains("appxq0fevzme2pys62n3e0fbqa7peapykr8v")) result = EnumWebBrowser.EDGE; //AppXq0fevzme2pys62n3e0fbqa7peapykr8v

                    if (val.ToLower().Contains("chrome")) result = EnumWebBrowser.Chrome; //ChromeHTML

                    if (val.ToLower().Contains("firefox")) result = EnumWebBrowser.Firefox; //FirefoxURL

                    if (val.ToLower().Contains("opera")) result = EnumWebBrowser.Opera; //OperaStable
                }
            }

            if (versionOS == EnumVersionOS.Unknown)
            {
                // возможно надо поднимать ошибку DataException
            }

            return result;
        }
    }
}
