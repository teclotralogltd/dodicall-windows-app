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
using DAL.Localization;
using System.Security;

namespace DAL.Model
{
    public class ModelLogin : AbstractNotifyPropertyChanged
    {
        private string _login;
        private SecureString _password;
        private bool _autostart;
        private bool _autoLogin;
        private ModelLanguage _modelGuiLanguageObj;
        private ModelServerArea _modelServerAreaObj;

        /// <summary> Флаг для login-a методом tryautologin </summary>
        public bool LastModelAutoLogin;

        /// <summary> Логин пользователя </summary>
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login == value) return;
                _login = value;
                OnPropertyChanged("Login");
            }
        }

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

        /// <summary> Флаг запускать при входе в систему </summary>
        public bool Autostart
        {
            get { return _autostart; }
            set
            {
                if (_autostart == value) return;
                _autostart = value;
                OnPropertyChanged("Autostart");
            }
        }

        /// <summary> Флаг запоминать пароль при входе в приложение </summary>
        public bool AutoLogin
        {
            get { return _autoLogin; }
            set
            {
                if (_autoLogin == value) return;
                _autoLogin = value;
                OnPropertyChanged("AutoLogin");
            }
        }

        /// <summary> Язык интерфейса </summary>
        public ModelLanguage ModelLanguageObj
        {
            get { return _modelGuiLanguageObj; }
            set
            {
                if (_modelGuiLanguageObj == value) return;
                _modelGuiLanguageObj = value;
                LocalizationApp.GetInstance().ModelLanguageObj = _modelGuiLanguageObj;
                OnPropertyChanged("ModelLanguageObj");
            }
        }

        /// <summary> Код площадки (т.к. СВ сказал не подгружать площадки если ServerAreaCode == 0) </summary>
        public int ServerAreaCode;

        /// <summary> Площадка </summary>
        public ModelServerArea ModelServerAreaObj
        {
            get { return _modelServerAreaObj; }
            set
            {
                if (_modelServerAreaObj == value) return;
                _modelServerAreaObj = value;
                ServerAreaCode = _modelServerAreaObj.Id;
                OnPropertyChanged("ModelServerAreaObj");
            }
        }
    }
}
