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
using System.Windows;
using System.Windows.Forms;
using dodicall.View;
using DAL.Enum;
using DAL.Model;
using dodicall.Enum;

namespace dodicall.Window
{
    public class FactoryWindow
    {
        /// <summary> Возвращает окно WindowMain </summary>
        public static WindowMain GetWindowMain()
        {
            var windowDispacher = WindowDispacher.GetInstance;

            var result = windowDispacher.GetWindow(typeof(WindowMain)) as WindowMain;

            if (result != null) return result;

            result = new WindowMain();

            result.Activated += (sender, args) => { windowDispacher.SetActiveWindow(result); };

            windowDispacher.AddToListWindow(result);

            result.Closed += (sender, args) => { windowDispacher.DeleteToListWindow(result); };

            return result;
        }

        /// <summary> Возвращает окно WindowStandard с ViewUserSettings </summary>
        public static WindowStandard GetWindowUserSettings()
        {
            var windowDispacher = WindowDispacher.GetInstance;

            var result = windowDispacher.GetWindowByUserControl(typeof(ViewUserSettings)) as WindowStandard;

            if (result != null) return result;

            result = new WindowStandard(new ViewUserSettings()) { Height = 600, Width = 800, WindowStartupLocation = WindowStartupLocation.CenterScreen };
              
            result.Activated += (sender, args) => { windowDispacher.SetActiveWindow(result); };

            windowDispacher.AddToListWindow(result);

            result.Closed += (sender, args) => { windowDispacher.DeleteToListWindow(result); };

            return result;
        }

        /// <summary> Возвращает окно WindowStandard с ViewContactManual </summary>
        public static WindowStandard GetWindowManualContact()
        {
            var windowDispacher = WindowDispacher.GetInstance;

            var result = windowDispacher.GetWindowByUserControl(typeof(ViewContactManual)) as WindowStandard;

            if (result != null) return result;

            result = new WindowStandard(new ViewContactManual()) { Height = 500, Width = 700, WindowStartupLocation = WindowStartupLocation.CenterOwner };

            result.Activated += (sender, args) => { windowDispacher.SetActiveWindow(result); };

            windowDispacher.AddToListWindow(result);

            result.Closed += (sender, args) => { windowDispacher.DeleteToListWindow(result); };

            return result;
        }

        /// <summary> Возвращает окно WindowStandard с ViewCallRedirect </summary>
        public static WindowStandard GetWindowCallRedirect(ModelCall currentModelCall)
        {
            var windowDispacher = WindowDispacher.GetInstance;

            var result = windowDispacher.GetWindowByUserControl(typeof(ViewCallRedirect)) as WindowStandard;

            if (result != null) return result;
            
            result = new WindowStandard(new ViewCallRedirect(currentModelCall)) { MinWidth = 400, MinHeight = 630, Height = 630, Width = 400, WindowStartupLocation = WindowStartupLocation.CenterOwner };

            result.Activated += (sender, args) => { windowDispacher.SetActiveWindow(result); };

            windowDispacher.AddToListWindow(result);

            result.Closed += (sender, args) => { windowDispacher.DeleteToListWindow(result); };

            return result;
        }

        /// <summary> Возвращает окно WindowStandard с ViewCallRedirect </summary>
        public static WindowStandard GetWindowPasswordBox(PasswordBoxTypeEnum windowType)
        {
            var windowDispacher = WindowDispacher.GetInstance;

            var result = windowDispacher.GetWindowByUserControl(typeof(ViewPasswordBox)) as WindowStandard;

            if (result != null) return result; 

            result = new WindowStandard(new ViewPasswordBox(windowType)) { MinWidth = 500, MinHeight = 200, Height = 200, Width = 500, WindowStartupLocation = WindowStartupLocation.CenterOwner, ResizeMode=ResizeMode.NoResize,ShowInTaskbar = false, };

            result.Activated += (sender, args) => { windowDispacher.SetActiveWindow(result); };

            windowDispacher.AddToListWindow(result);

            result.Closed += (sender, args) => { windowDispacher.DeleteToListWindow(result); };

            return result;
        }
    }
}
