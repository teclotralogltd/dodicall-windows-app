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
using dodicall;
using DAL.Abstract;
using DAL.Localization;
using DAL.ModelEnum;

namespace DAL.Model
{
    public class ModelUserSettings : AbstractNotifyPropertyChanged
    {
        private bool _autoLogin;
        private bool _autostart;
        private bool _doNotDesturbMode;
        private bool _guiAnimation;
        private int _guiFontSize;
        private bool _traceMode;
        private string _userExtendedStatus;
        private bool _videoEnabled;

        private ModelEnumTheme _modelEnumThemeObj;
        private ModelEnumEchoCancellationMode _modelEnumEchoCancellationModeObj;
        private ModelEnumUserBaseStatus _modelEnumUserBaseStatusObj;
        private ModelEnumVideoSizeCell _modelEnumVideoSizeCellObj;
        private ModelEnumVideoSizeWifi _modelEnumVideoSizeWifiObj;
        private ModelEnumVoipEncryption _modelEnumVoipEncryptionObj;
        private ModelLanguage _modelLanguageObj;
        private ModelUserAccount _defaultModelUserAccountObj;

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

        /// <summary> Флаг режима не беспокоить </summary>
        public bool DoNotDesturbMode
        {
            get { return _doNotDesturbMode; }
            set
            {
                if (_doNotDesturbMode == value) return;
                _doNotDesturbMode = value;
                OnPropertyChanged("DoNotDesturbMode");
            }
        }

        /// <summary> Флаг анимации приложения (не используется) </summary>
        public bool GuiAnimation
        {
            get { return _guiAnimation; }
            set
            {
                if (_guiAnimation == value) return;
                _guiAnimation = value;
                OnPropertyChanged("GuiAnimation");
            }
        }

        /// <summary> Размер шрифта сообщений в чате </summary>
        public int GuiFontSize
        {
            get { return _guiFontSize; }
            set
            {
                if (_guiFontSize == value) return;
                _guiFontSize = value;
                OnPropertyChanged("GuiFontSize");
            }
        }

        /// <summary> Флаг режима трассировки </summary>
        public bool TraceMode
        {
            get { return _traceMode; }
            set
            {
                if (_traceMode == value) return;
                _traceMode = value;
                OnPropertyChanged("TraceMode");
            }
        }

        /// <summary> Расширенный статус пользователя </summary>
        public string UserExtendedStatus
        {
            get { return _userExtendedStatus; }
            set
            {
                if (_userExtendedStatus == value) return;
                _userExtendedStatus = value;
                OnPropertyChanged("UserExtendedStatus");
            }
        }

        /// <summary> Флаг разрешены ли видеозвонки </summary>
        public bool VideoEnabled
        {
            get { return _videoEnabled; }
            set
            {
                if (_videoEnabled == value) return;
                _videoEnabled = value;
                OnPropertyChanged("VideoEnabled");
            }
        }

        /// <summary> Тема приложения </summary>
        public ModelEnumTheme ModelEnumThemeObj
        {
            get { return _modelEnumThemeObj; }
            set
            {
                if (_modelEnumThemeObj == value) return;
                _modelEnumThemeObj = value;
                OnPropertyChanged("ModelEnumThemeObj");
            }
        }

        /// <summary> Объект шумоподавления </summary>
        public ModelEnumEchoCancellationMode ModelEnumEchoCancellationModeObj
        {
            get { return _modelEnumEchoCancellationModeObj; }
            set
            {
                if (_modelEnumEchoCancellationModeObj == value) return;
                _modelEnumEchoCancellationModeObj = value;
                OnPropertyChanged("ModelEnumEchoCancellationModeObj");
            }
        }

        /// <summary> Статус пользователя </summary>
        public ModelEnumUserBaseStatus ModelEnumUserBaseStatusObj
        {
            get { return _modelEnumUserBaseStatusObj; }
            set
            {
                if (_modelEnumUserBaseStatusObj == value) return;
                _modelEnumUserBaseStatusObj = value;
                OnPropertyChanged("ModelEnumUserBaseStatusObj");
            }
        }

        /// <summary> Качество видео через GSM </summary>
        public ModelEnumVideoSizeCell ModelEnumVideoSizeCellObj
        {
            get { return _modelEnumVideoSizeCellObj; }
            set
            {
                if (_modelEnumVideoSizeCellObj == value) return;
                _modelEnumVideoSizeCellObj = value;
                OnPropertyChanged("ModelEnumVideoSizeCellObj");
            }
        }

        /// <summary> Качество видео через WiFi </summary>
        public ModelEnumVideoSizeWifi ModelEnumVideoSizeWifiObj
        {
            get { return _modelEnumVideoSizeWifiObj; }
            set
            {
                if (_modelEnumVideoSizeWifiObj == value) return;
                _modelEnumVideoSizeWifiObj = value;
                OnPropertyChanged("ModelEnumVideoSizeWifiObj");
            }
        }

        /// <summary> Объект режим шифрования </summary>
        public ModelEnumVoipEncryption ModelEnumVoipEncryptionObj
        {
            get { return _modelEnumVoipEncryptionObj; }
            set
            {
                if (_modelEnumVoipEncryptionObj == value) return;
                _modelEnumVoipEncryptionObj = value;
                OnPropertyChanged("ModelEnumVoipEncryptionObj");
            }
        }

        /// <summary> Язык интерфейса </summary>
        public ModelLanguage ModelLanguageObj
        {
            get { return _modelLanguageObj; }
            set
            {
                if (_modelLanguageObj == value) return;
                _modelLanguageObj = value;
                LocalizationApp.GetInstance().ModelLanguageObj = _modelLanguageObj;
                OnPropertyChanged("ModelLanguageObj");
            }
        }

        /// <summary> Аккаунт пользователя используемый по умолчанию </summary>
        public ModelUserAccount DefaultModelUserAccountObj
        {
            get { return _defaultModelUserAccountObj; }
            set
            {
                if (_defaultModelUserAccountObj == value) return;
                _defaultModelUserAccountObj = value;
                OnPropertyChanged("DefaultModelUserAccountObj");
            }
        }

        /// <summary> Список аккаунтов пользователя </summary>
        public List<ModelUserAccount> ListModelUserAccount { get; set; }

        /// <summary> Список настроек кодеков Аудио (WiFi) </summary>
        public List<ModelCodecSettings> ListModelCodecSettingsAudioWifi { get; set; }

        /// <summary> Список настроек кодеков Аудио (Cell) </summary>
        public List<ModelCodecSettings> ListModelCodecSettingsAudioCell { get; set; }

        /// <summary> Список настроек кодеков Видео </summary>
        public List<ModelCodecSettings> ListModelCodecSettingsVideo { get; set; }
    }
}
