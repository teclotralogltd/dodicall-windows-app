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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Localization;
using DAL.Model;
using DAL.ModelEnum;
using DAL.WrapperBridge;
using System.Windows;
using DAL.Utility;
using Microsoft.Win32;

namespace DAL.ViewModel
{
    public class ViewModelUserSettings : AbstractViewModel
    {
        /// <summary> Событие закрытия настроек </summary>
        public event EventHandler CloseUserSettings;

        /// <summary> Ключ шифрования для импорта </summary>
        private string _cryptKeyForImport;

        /// <summary> Ключ шифрования для импорта </summary>
        public string CryptKeyForImport
        {
            get { return _cryptKeyForImport; }
            set
            {
                _cryptKeyForImport = value;
                OnPropertyChanged("CryptKeyForImport");
            } 
        } 

        /// <summary> Кол-во белых контактов </summary>
        private int _countWhiteContact;

        /// <summary> Кол-во белых контактов </summary>
        public int CountWhiteContact
        {
            get { return _countWhiteContact; }
        }

        /// <summary> Кол-во белых контактов строкой </summary>
        public string CountWhiteContactString
        {
            get
            {
                var result = "(" + _countWhiteContact + " ";

                var localization = LocalizationApp.GetInstance();

                if (localization.ModelLanguageObj.CodeName.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                {
                    result += _countWhiteContact == 1 ? localization.GetValueByKey(@"ViewModelUserSettings_WhiteContact1to1") : localization.GetValueByKey(@"ViewModelUserSettings_WhiteContact2to9");
                }

                if (localization.ModelLanguageObj.CodeName.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
                {
                    var last2Number = _countWhiteContact % 100;

                    if (last2Number > 11 && last2Number < 19)
                    {
                        result += localization.GetValueByKey(@"ViewModelUserSettings_WhiteContact5to9");
                    }
                    else
                    {
                        var last1Number = last2Number % 10;

                        if (last1Number == 1) result += localization.GetValueByKey(@"ViewModelUserSettings_WhiteContact1to1");

                        if (last1Number > 1 && last1Number < 5) result += localization.GetValueByKey(@"ViewModelUserSettings_WhiteContact2to4");

                        if (last1Number < 1 || last1Number > 4) result += localization.GetValueByKey(@"ViewModelUserSettings_WhiteContact5to9");
                    }
                }

                return result + ")";
            }
        }

        /// <summary> Текущий язык интерфейса </summary>
        private ModelLanguage _currentModelLanguage;

        /// <summary> Список групп настроек </summary>
        public List<ModelEnumUserSettingsGroup> ListModelEnumUserSettingsGroup { get; set; } = ModelEnumUserSettingsGroup.ListModelEnum;

        /// <summary> Список статусов </summary>
        public List<ModelEnumUserBaseStatus> ListModelEnumUserBaseStatus { get; set; } = ModelEnumUserBaseStatus.ListModelEnum;

        /// <summary> Список методов шифрования </summary>
        public List<ModelEnumVoipEncryption> ListModelEnumVoipEncryption { get; set; } = ModelEnumVoipEncryption.ListModelEnum;

        /// <summary> Список разрешений видео WiFi </summary>
        public List<ModelEnumVideoSizeWifi> ListModelEnumVideoSizeWifi { get; set; } = ModelEnumVideoSizeWifi.ListModelEnum;

        /// <summary> Список разрешений видео Cell </summary>
        public List<ModelEnumVideoSizeCell> ListModelEnumVideoSizeCell { get; set; } = ModelEnumVideoSizeCell.ListModelEnum;

        /// <summary> Список режимов эхошумоподавления </summary>
        public List<ModelEnumEchoCancellationMode> ListModelEnumEchoCancellationMode { get; set; } = ModelEnumEchoCancellationMode.ListModelEnum;

        /// <summary> Список языков </summary>
        public List<ModelLanguage> ListModelLanguage { get; set; } = ModelLanguage.ListModelLanguage;

        /// <summary> Список тем интерфейса </summary>
        public List<ModelEnumTheme> ListModelEnumTheme { get; set; } = ModelEnumTheme.ListModelEnum;

        /// <summary> Настройки пользователя </summary>
        public ModelUserSettings CurrentModelUserSettings { get; set; }

        /// <summary> Текущий группа </summary>
        private ModelEnumUserSettingsGroup _currentModelEnumUserSettingsGroup;

        /// <summary> Текущий группа </summary>
        public ModelEnumUserSettingsGroup CurrentModelEnumUserSettingsGroup
        {
            get { return _currentModelEnumUserSettingsGroup; }
            set
            {
                if (_currentModelEnumUserSettingsGroup == value) return;
                _currentModelEnumUserSettingsGroup = value;
                OnPropertyChanged("CurrentModelEnumUserSettingsGroup");
            }
        }

        /// <summary> Uri для страницы о программе </summary>
        public string AboutUri => LocalizationApp.GetInstance().GetValueByKey(@"ViewModelUserSettings_AboutUri");

        /// <summary> Uri для страницы что нового </summary>
        public string NewsUri => LocalizationApp.GetInstance().GetValueByKey(@"ViewModelUserSettings_NewsUri");

        /// <summary> Uri для страницы известные проблемы </summary>
        public string ProblemsUri => LocalizationApp.GetInstance().GetValueByKey(@"ViewModelUserSettings_ProblemsUri");

        /// <summary> Uri для страницы известные проблемы </summary>
        public string PrivacyPolicyUri => LocalizationApp.GetInstance().GetValueByKey(@"ViewModelUserSettings_PrivacyPolicyUri");

        /// <summary> Uri для страницы помощь </summary>
        public string HelpUri => LocalizationApp.GetInstance().GetValueByKey(@"ViewModelUserSettings_HelpUri");

        /// <summary> Флаг изменения настроек </summary>
        public bool CurrentModelUserSettingsChanged { get; private set; }
         
        /// <summary> Флаг отображения панели импорта ключа шифрования </summary> 
        private bool _showImportPanel;

        /// <summary> Флаг отображения панели импорта ключа шифрования </summary> 
        public bool ShowImportPanel
        {
            get { return _showImportPanel; }
            set
            {
                _showImportPanel = value;
                OnPropertyChanged("ShowImportPanel"); 
            }
        }

        /// <summary> Команда открытия Web-страницы </summary>
        public Command CommandOpenUrl { get; set; }

        /// <summary> Команда открыть лог чата </summary>
        public Command CommandOpenLogChat { get; set; }

        /// <summary> Команда открыть лог базы данных </summary>
        public Command CommandOpenLogDatabase { get; set; }

        /// <summary> Команда открыть лог запросов </summary>
        public Command CommandOpenLogRequest { get; set; }

        /// <summary> Команда открыть лог телефонии </summary>
        public Command CommandOpenLogVoip { get; set; }

        /// <summary> Получить лог качества звонков </summary>
        public Command CommandOpenLogCallQuality { get; set; }

        /// <summary> Получить лог истории звонков </summary>
        public Command CommandOpenLogCallHistory { get; set; }

        /// <summary> Получить лог приложения </summary>
        public Command CommandOpenLogGui { get; set; }

        /// <summary> Команда очистить логи </summary>
        public Command CommandClearLogs { get; set; }

        /// <summary> Команда сохранения настроек </summary>
        public Command CommandSave { get; set; }

        /// <summary> Команда включения белого списка </summary>
        public Command CommandEnableWriteList { get; set; }

        /// <summary> Команда копирования из буфер обмена </summary>
        public Command CommandPasteFromClipBoard { get; set; }

        /// <summary> Команда сканировать QR код</summary>
        public Command CommandScanQRCode { get; set; }  

        /// <summary> Флаг доступности кнопок экспорта ключа шифрования </summary> 
        private bool _exportAviable;

        /// <summary> Флаг доступности кнопок экспорта ключа шифрования </summary> 
        public bool ExportAviable
        {
            get { return _exportAviable; }
            set
            {
                _exportAviable = value; 
                OnPropertyChanged("ExportAviable"); 
            }
        }

        /// <summary> Конструктор </summary>
        public ViewModelUserSettings()
        {
            CurrentModelUserSettings = DataSourceUserSettings.GetModelUserSettings();

            _countWhiteContact = DataSourceContact.GetCountWhiteContact();

            _currentModelLanguage = CurrentModelUserSettings.ModelLanguageObj;

            CommandOpenUrl = new Command(OpenUrl);

            CommandOpenLogChat = new Command(obj => { OpenLog(DataSourceLogScope.GetChatLog()); });
            CommandOpenLogDatabase = new Command(obj => { OpenLog(DataSourceLogScope.GetDatabaseLog()); });
            CommandOpenLogRequest = new Command(obj => { OpenLog(DataSourceLogScope.GetRequestsLog()); });
            CommandOpenLogVoip = new Command(obj => { OpenLog(DataSourceLogScope.GetVoipLog()); });

            CommandOpenLogCallQuality = new Command(obj => { OpenLog(DataSourceLogScope.GetCallQualityLog()); });
            CommandOpenLogCallHistory = new Command(obj => { OpenLog(DataSourceLogScope.GetCallHistoryLog()); });
            CommandOpenLogGui = new Command(obj => { OpenLog(DataSourceLogScope.GetGuiLog()); });

            CommandClearLogs = new Command(obj => { DataSourceLogScope.ClearLogs(); });

            ExportAviable = CheckEncryptionkey();

            CommandSave = new Command(obj => Save(obj as bool? ?? false));

            CommandEnableWriteList = new Command(obj => EnableWriteList());

            CommandPasteFromClipBoard = new Command(obj => { PasteFromClipBoard(); }); 

            CurrentModelUserSettings.PropertyChanged += (sender, args) => { CurrentModelUserSettingsPropertyChanged(); };

            foreach (var modelCodecSettings in CurrentModelUserSettings.ListModelCodecSettingsAudioWifi)
            {
                modelCodecSettings.PropertyChanged += (sender, args) => { CurrentModelUserSettingsPropertyChanged(); };
            }

            foreach (var modelCodecSettings in CurrentModelUserSettings.ListModelCodecSettingsAudioCell)
            {
                modelCodecSettings.PropertyChanged += (sender, args) => { CurrentModelUserSettingsPropertyChanged(); };
            }

            foreach (var modelCodecSettings in CurrentModelUserSettings.ListModelCodecSettingsVideo)
            {
                modelCodecSettings.PropertyChanged += (sender, args) => { CurrentModelUserSettingsPropertyChanged(); };
            }
        }

        /// <summary> Обработчик нажатия кнопки "Вставить из буфера" </summary>
        private void PasteFromClipBoard()
        {
            ShowImportPanel = true;
        } 

        /// <summary> Обработчик изменения настроек </summary>
        private void CurrentModelUserSettingsPropertyChanged()
        {
            CurrentModelUserSettingsChanged = true;
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("AboutUri");
            OnPropertyChanged("NewsUri");
            OnPropertyChanged("ProblemsUri");
            OnPropertyChanged("HelpUri");
            OnPropertyChanged("CountWhiteContactString");
        }

        /// <summary> Метод открытия Web-страницы </summary>
        private void OpenUrl(object obj)
        {
            var uri = obj as Uri;

            if (String.IsNullOrWhiteSpace(uri?.AbsoluteUri)) return;

            Process.Start(new ProcessStartInfo(((Uri)obj).AbsoluteUri));
        }

        /// <summary> Метод открытия логов </summary>
        private void OpenLog(string[] log)
        {
            if (log == null) throw new Exception(LocalizationApp.GetInstance().GetValueByKey(@"ViewModelUserSettings_LogEmpty"));

            var tempPath = Path.GetTempPath() + @"\" + Guid.NewGuid() + @".txt";

            File.WriteAllLines(tempPath, log);

            Process.Start(tempPath);
        }

        /// <summary> Метод сохранения настроек </summary>
        private void Save(bool wihtoutOnCloseUserSettings)
        {
            DataSourceUserSettings.SaveModelUserSettings(CurrentModelUserSettings);

            _currentModelLanguage = CurrentModelUserSettings.ModelLanguageObj;

            CurrentModelUserSettingsChanged = false;

            if (wihtoutOnCloseUserSettings) return;

            OnCloseUserSettings();
        }

        /// <summary> Метод включения белого списка </summary>
        private void EnableWriteList()
        {
            CurrentModelUserSettings.DoNotDesturbMode = true;
        }

        /// <summary> Импортировать приватный ключ шифрования </summary>
        public bool ImportAndSaveCryptKey()
        {
            var result = DataSourceSecurity.ImportUserPrivateKey(UtilitySecurity.ToSecureString(CryptKeyForImport));
            if (result)
            {
                CryptKeyForImport = String.Empty;
                 
                var saveResult = LocalSavePublicKey();
                
                ShowImportPanel = false;
                ExportAviable = true;
            }

            return result;
        }

        /// <summary> Сохранить ключ шифрования в локальном хранилище </summary>
        public bool LocalSavePublicKey()
        { 
            var modelLogin = DataSourceLogin.GetLastModelLogin();
            var login = modelLogin.Login;
            var serverArea = modelLogin.ServerAreaCode;
            var secretKey = DataSourceSecurity.GetUserSecretKey();
            var saveResult = UtilitySecurity.SavePrivateCryptKeyToIsolatedStorage(secretKey, login, serverArea.ToString()); 

            return saveResult;
        }

        /// <summary> Возвращает ключ шифрования залогиненого пользователя для экспорта </summary>
        public string GetUserPrivateKey()
        {
            return UtilitySecurity.ConvertToString(DataSourceSecurity.GetUserPrivateKey());
        } 

        /// <summary> Проверка возможности Экспорта ключа из БЛ</summary>
        public bool CheckEncryptionkey()
        { 
            return !String.IsNullOrEmpty(UtilitySecurity.ConvertToString(DataSourceSecurity.GetUserPrivateKey()));
        }

        /// <summary> Перегенерировать ключ шифрования</summary>
        public bool RegenerateKeyPair()
        { 
            return DataSourceSecurity.RegenerateKeyPair();
        }

        /// <summary> Метод возвращает предыдущий язык (если настройки не были сохранены, то язык приложения нужно вернуть) </summary>
        public void ResetLanguage()
        {
            LocalizationApp.GetInstance().ModelLanguageObj = _currentModelLanguage;
        }

        /// <summary> Инвокатор события CloseUserSettings </summary>
        private void OnCloseUserSettings()
        {
            CloseUserSettings?.Invoke(this, EventArgs.Empty);
        }
    }
}
