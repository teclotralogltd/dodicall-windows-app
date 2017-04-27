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
using dodicall.Styles.CustomizedWindow;

namespace dodicall.Window
{
    public class WindowDispacher
    {
        /// <summary> Объект WindowDispacher </summary>
        private static WindowDispacher _instance;

        /// <summary> Получить объект WindowDispacher </summary>
        public static WindowDispacher GetInstance => _instance ?? (_instance = new WindowDispacher());

        /// <summary> Список текущих окон </summary>
        private IWindow _lastActiveWindow; // в конструкторе окна в фабрике добавить к событию активации или фокуса

        /// <summary> Список текущих окон </summary>
        private List<IWindow> _listWindow = new List<IWindow>();

        /// <summary> Конструктор </summary>
        private WindowDispacher()
        {
            
        }

        /// <summary> Возвращает окно если оно уже было создано </summary>
        public System.Windows.Window GetWindow(Type type)
        {
            return _listWindow.FirstOrDefault(obj => obj.GetType() == type) as System.Windows.Window;
        }

        /// <summary> Возвращает окно если оно уже было создано </summary>
        public System.Windows.Window GetWindowByUserControl(Type type)
        {
            return _listWindow.FirstOrDefault(obj => (obj as WindowStandard)?.ViewUserControl.GetType() ==  type) as System.Windows.Window;
        }

        /// <summary> Установить окно как активное </summary>
        public void SetActiveWindow(IWindow window)
        {
            _lastActiveWindow = window;
        }

        /// <summary> Добавить окно к списку текущих окон </summary>
        public void AddToListWindow(IWindow window)
        {
            if (!_listWindow.Contains(window)) _listWindow.Add(window);
        }

        /// <summary> Удалить окно из списка текущих окон </summary>
        public void DeleteToListWindow(IWindow window)
        {
            if (_listWindow.Contains(window)) _listWindow.Remove(window);

            if (_lastActiveWindow == window) _lastActiveWindow = null;
        }

        /// <summary> Показать все окна в последнем состоянии </summary>
        public void ShowAllWindow()
        {
            foreach (var window in _listWindow)
            {
                window.ShowWindow();
            }

            //_lastActiveWindow?.Activate(); если надо открывать именно последнее активное окно, последнее состояние запоминается
        }

        /// <summary> Показать главное окно </summary>
        public void ShowMainWindow()
        {
            var windowMain = GetWindow(typeof(WindowMain)) as IWindow;

            windowMain?.ShowWindow();
        }
    }
}
