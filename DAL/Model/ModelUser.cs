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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media.Imaging;
using dodicall;
using DAL.Abstract;
using DAL.Callback;
using DAL.Localization;
using DAL.ModelEnum;
using DAL.Utility;
using DAL.WrapperBridge;

namespace DAL.Model
{
    public class ModelUser : AbstractLocalization
    {
        /// <summary> Событие изменения статуса пользователя </summary>
        public event EventHandler UserStatusChanged;

        private BitmapImage _avatar = DataSourceContact.Avatar;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private ModelEnumUserBaseStatus _modelEnumUserBaseStatusObj;
        private string _userExtendedStatus;
        private bool _hasBalance;
        private double _balanceValue;
        private ModelEnumCurrency _modelEnumCurrencyObj;
        private ModelServerArea _modelServerAreaObj;
        private List<ModelUserContact> _listModelUserContactMy;
        private List<ModelUserContact> _listModelUserContactExtra;

        /// <summary> Объект ModelUser </summary>
        private static ModelUser _instance;

        /// <summary> Таймер для обновления баланса </summary>
        private Timer _timer;

        /// <summary> Аватар </summary>
        public BitmapImage Avatar
        {
            get { return _avatar; }
            set
            {
                if (_avatar == value) return;
                _avatar = value;
                OnPropertyChanged("Avatar");
            }
        }

        /// <summary> Имя </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary> Отчество </summary>
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (_middleName == value) return;
                _middleName = value;
                OnPropertyChanged("MiddleName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary> Фамилия </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary> Ф.И.О. </summary>
        public string FullName => (String.IsNullOrWhiteSpace(_firstName) ? "" : _firstName) + (String.IsNullOrWhiteSpace(_middleName) ? "" : " " + _middleName) + (String.IsNullOrWhiteSpace(_lastName) ? "" : " " + _lastName);

        /// <summary> Статус пользователя </summary>
        public ModelEnumUserBaseStatus ModelEnumUserBaseStatusObj
        {
            get { return _modelEnumUserBaseStatusObj; }
            set
            {
                if (_modelEnumUserBaseStatusObj == value) return;
                _modelEnumUserBaseStatusObj = value;
                OnPropertyChanged("ModelEnumUserBaseStatusObj");
                OnUserStatusChanged();
            }
        }

        /// <summary> Расширенный статус </summary>
        public string UserExtendedStatus
        {
            get { return _userExtendedStatus; }
            set
            {
                if (_userExtendedStatus == value) return;
                _userExtendedStatus = value;
                OnPropertyChanged("UserExtendedStatus");
                OnPropertyChanged("UserExtendedStatusForFullStatus");
                OnUserStatusChanged();
            }
        }

        /// <summary> Расширенный статус с точкой </summary>
        public string UserExtendedStatusForFullStatus
        {
            get { return String.IsNullOrWhiteSpace(_userExtendedStatus) ? "" : ". " + _userExtendedStatus; }
        }

        /// <summary> Флаг имеет ли пользователь баланс </summary>
        public bool HasBalance
        {
            get { return _hasBalance; }
            set
            {
                if (value == false) TimerOff();

                if (_hasBalance == value) return;
                _hasBalance = value;
                OnPropertyChanged("HasBalance");
            }
        }

        /// <summary> Баланс </summary>
        public double BalanceValue
        {
            get { return _balanceValue; }
            set
            {
                if (_balanceValue.Equals(value)) return;
                _balanceValue = value;
                OnPropertyChanged("BalanceValue");
                OnPropertyChanged("BalanceValueString");
            }
        }

        /// <summary> Баланс строкой </summary>
        public string BalanceValueString => BalanceValue.ToString("N", LocalizationApp.GetInstance().GetNumberFormatInfo());

        /// <summary> Текущая валюта </summary>
        public ModelEnumCurrency ModelEnumCurrencyObj
        {
            get { return _modelEnumCurrencyObj; }
            set
            {
                if (_modelEnumCurrencyObj == value) return;
                _modelEnumCurrencyObj = value;
                OnPropertyChanged("ModelEnumCurrencyObj");
            }
        }

        /// <summary> Текущая площадка </summary>
        public ModelServerArea ModelServerAreaObj
        {
            get { return _modelServerAreaObj; }
            set
            {
                if (_modelServerAreaObj == value) return;
                _modelServerAreaObj = value;
                OnPropertyChanged("ModelServerAreaObj");
            }
        }

        /// <summary> Список моих контактов </summary>
        public List<ModelUserContact> ListModelUserContactMy
        {
            get { return _listModelUserContactMy; }
            set
            {
                if (_listModelUserContactMy == value) return;
                _listModelUserContactMy = value;
                OnPropertyChanged("ListModelUserContactMy");
            }
        }

        /// <summary> Список доп. контактов </summary>
        public List<ModelUserContact> ListModelUserContactExtra
        {
            get { return _listModelUserContactExtra; }
            set
            {
                if (_listModelUserContactExtra == value) return;
                _listModelUserContactExtra = value;
                OnPropertyChanged("ListModelUserContactExtra");
            }
        }

        /// <summary> Получить объект ModelUser </summary>
        public static ModelUser GetInstance()
        {
            if (_instance == null) _instance = new ModelUser();

            return _instance;
        }

        /// <summary> Конструктор </summary>
        private ModelUser()
        {
            _timer = new Timer {Interval = 30000}; // 30 секунд

            _timer.Elapsed += RefreshBalance;

            _timer.Enabled = true;

            CallbackRouter.Instance.ModelUserStatusChanged += OnModelUserStatusChanged;

            CallbackRouter.Instance.PresenceOffline += OnPresenceOffline;
        }

        /// <summary> Выключение таймера </summary>
        private void TimerOff()
        {
            _timer.Enabled = false;

            _timer.Elapsed -= RefreshBalance;

            _timer.Close();
        }

        /// <summary> Обновление баланса </summary>
        private void RefreshBalance(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            DataSourceUser.RefreshModelUserBalance(this);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("BalanceValueString");
        }

        /// <summary> Обработчик события изменения собственного статуса </summary>
        private void OnModelUserStatusChanged(object sender, PackageModelContactStatus packageModelContactStatuses)
        {
            ModelEnumUserBaseStatusObj = packageModelContactStatuses.ModelEnumUserBaseStatusObj;
            UserExtendedStatus = packageModelContactStatuses.UserExtendedStatus;
        }

        /// <summary> Инвокатор события UserStatusChanged </summary>
        private void OnUserStatusChanged()
        {
            UserStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Обработчик события изменения собственного статуса </summary>
        private void OnPresenceOffline(object sender, EventArgs eventArgs)
        {
            ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(0);
        }
    }
}
