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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Callback;
using DAL.Enum;
using DAL.Model;
using DAL.Utility;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelUserAuthorization : AbstractViewModel
    {
        ///<summary> Объект BackgroundWorker для асинхронной авторизации </summary>
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        /// <summary> Версия программы </summary>
        public string AppVersion => DataSourceUtility.GetVersionApp();

        /// <summary> Список языков </summary>
        public List<ModelLanguage> ListModelLanguage { get; set; } = ModelLanguage.ListModelLanguage;

        /// <summary> Список площадок </summary>
        public List<ModelServerArea> ListModelServerArea { get; set; } /*= ModelServerArea.ListModelServerArea;*/

        /// <summary> Команда авторизации пользователя </summary>
        public Command CommandLogin { get; set; }

        /// <summary> Команда восстановление пароля </summary>
        public Command CommandForgotPassword { get; set; }

        /// <summary> Команда регистрации нового пользователя </summary>
        public Command CommandSignUp { get; set; }

        /// <summary> Текущий пользователь </summary>
        private ModelLogin _currentModelLogin;

        /// <summary> Текущий пользователь </summary>
        public ModelLogin CurrentModelLogin
        {
            get { return _currentModelLogin; }
            set
            {
                if (_currentModelLogin == value) return;
                _currentModelLogin = value;
                OnPropertyChanged("CurrentModelLogin");
            }
        }

        /// <summary> Результат авторизации </summary>
        private EnumResultLogin _failLogin;

        /// <summary> Результат авторизации </summary>
        public EnumResultLogin FailLogin
        {
            get { return _failLogin; }
            set
            {
                _failLogin = value;
                OnPropertyChanged("FailLogin");         
            }
        }

        /// <summary> Конструктор </summary>
        public ViewModelUserAuthorization()
        {
            CurrentModelLogin = DataSourceLogin.GetLastModelLogin();

            if (CurrentModelLogin.ServerAreaCode > 0) LoadListModelServerArea();

            CurrentModelLogin.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName.Equals("ModelLanguageObj", StringComparison.InvariantCultureIgnoreCase)) DataSourceLogin.SaveModelLanguage(CurrentModelLogin.ModelLanguageObj);
            };

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;

            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

            CommandLogin = new Command(obj => Login());
            CommandForgotPassword = new Command(obj => ForgotPassword());
            CommandSignUp = new Command(obj => SignUp());

            CallbackRouter.Instance.ModelConnectStateChanged += OnModelConnectStateChanged;
        }

        /// <summary> Загрузка списка площадок </summary>
        public void LoadListModelServerArea()
        {
            List<ModelServerArea> listModelServerArea;

            var enumResultLogin = DataSourceLogin.GetListModelServerArea(out listModelServerArea);

            if (enumResultLogin == EnumResultLogin.No)
            {
                ListModelServerArea = listModelServerArea;

                OnPropertyChanged("ListModelServerArea");

                var modelServerArea = listModelServerArea.FirstOrDefault(obj => obj.Id == CurrentModelLogin.ServerAreaCode);

                if (modelServerArea != null)
                {
                    CurrentModelLogin.ModelServerAreaObj = modelServerArea;
                }
                else
                {
                    var modelServerAreaDefault = listModelServerArea.FirstOrDefault(obj => obj.Id == 0);

                    CurrentModelLogin.ModelServerAreaObj = modelServerAreaDefault ?? ModelServerArea.GetDefaultModelServerArea();
                }
            }
        }

        /// <summary> Обработчик изменения состояния подключения </summary>
        private void OnModelConnectStateChanged(object sender, ModelConnectState modelConnectState)
        {
            if (modelConnectState.ModelEnumNetworkTechnologyObj.Code != 0 /* None */ && ListModelServerArea == null)
            {
                LoadListModelServerArea();
                OnPropertyChanged("ListModelServerArea");
            }
        }

        /// <summary> Метод авторизации пользователя </summary>
        private void Login()
        {
            OnLockUI(true);

            _backgroundWorker.RunWorkerAsync();
        }

        ///<summary> Метод выполнения потока BackgroundWorker </summary>
        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = DataSourceLogin.Login(CurrentModelLogin);
        }

        ///<summary> Метод завершения выполнения потока BackgroundWorker </summary>
        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnLockUI(false);

                throw e.Error;
            }

            FailLogin = (EnumResultLogin)e.Result;
        }

        /// <summary> Метод восстановление пароля </summary>
        private void ForgotPassword()
        {
            if (CurrentModelLogin.ModelServerAreaObj == null) LoadListModelServerArea();

            if (CurrentModelLogin.ModelServerAreaObj != null) OpenUrl(CurrentModelLogin.ModelServerAreaObj.UrlForgotPassword);
        }

        /// <summary> Метод регистрации нового пользователя </summary>
        private void SignUp()
        {
            if (CurrentModelLogin.ModelServerAreaObj == null) LoadListModelServerArea();

            if (CurrentModelLogin.ModelServerAreaObj != null) OpenUrl(CurrentModelLogin.ModelServerAreaObj.UrlSignUp);
        }

        /// <summary> Метод открытия Web-страницы </summary>
        private void OpenUrl(string url)
        {
            UtilityWeb.OpenUrl(url);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            
        }
    }
}
