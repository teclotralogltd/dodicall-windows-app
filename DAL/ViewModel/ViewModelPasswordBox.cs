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
using System.Timers;
using DAL.Abstract;
using DAL.Model;
using DAL.WrapperBridge;
using System.Security;
using DAL.Utility;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using DAL.Enum;
using System.ComponentModel;

namespace DAL.ViewModel
{
    public class ViewModelPasswordBox : AbstractViewModel, IDisposable
    {
        /// <summary> Пароль пользователя </summary>
        private SecureString _password;

        /// <summary> Пароль пользователя </summary>
        public SecureString Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        /// <summary> Команда проверки пароля </summary>
        public Command CommandCheckPassword { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelPasswordBox()
        {
            CommandCheckPassword = new Command(obj => CheckPassword());
        }

        /// <summary> Проверка пароля </summary>
        public void CheckPassword()
        {
            OnEventViewModel("CheckPassword", DataSourceSecurity.CheckPassword(_password));
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        } 

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _password.Dispose();
        }
    }
}
