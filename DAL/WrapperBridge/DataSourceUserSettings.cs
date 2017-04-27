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
using DAL.Model;
using DAL.ModelEnum;

namespace DAL.WrapperBridge
{
    internal class DataSourceUserSettings : AbstractDataSource
    {
        /// <summary> Получить настройки пользователя </summary>
        public static ModelUserSettings GetModelUserSettings()
        {
            var userSettings = Logic.GetUserSettings();

            var deviceSettings = Logic.GetDeviceSettings();

            var listModelUserAccount = new List<ModelUserAccount>();

            foreach (var serverSettingModelManaged in deviceSettings.ServerSettings.Where(obj => obj.ServerType == ServerSettingTypeManaged.Sip))
            {
                var modelUserAccount = new ModelUserAccount
                {
                    Name = serverSettingModelManaged.AuthUserName,
                    Domain = serverSettingModelManaged.Domain,
                    Default = serverSettingModelManaged.Default
                };

                listModelUserAccount.Add(modelUserAccount);
            }

            var defaultModelUserAccount = listModelUserAccount.FirstOrDefault(obj => obj.Domain.Equals(userSettings.DefaultVoipServer, StringComparison.InvariantCultureIgnoreCase)) ?? listModelUserAccount.FirstOrDefault(obj => obj.Default);

            var result = new ModelUserSettings
            {
                AutoLogin = userSettings.Autologin,
                Autostart = userSettings.Autostart,
                GuiAnimation = userSettings.GuiAnimation,
                TraceMode = userSettings.TraceMode,
                VideoEnabled = userSettings.VideoEnabled,
                GuiFontSize = userSettings.GuiFontSize,
                DoNotDesturbMode = userSettings.DoNotDesturbMode,
                ModelLanguageObj = ModelLanguage.GetModelLanguage(userSettings.GuiLanguage),
                ModelEnumThemeObj = ModelEnumTheme.GetModelEnum(userSettings.GuiThemeName),
                UserExtendedStatus = userSettings.UserExtendedStatus,
                ModelEnumEchoCancellationModeObj = ModelEnumEchoCancellationMode.GetModelEnum((int)userSettings.EchoCancellationMode),
                ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum((int)userSettings.UserBaseStatus),
                ModelEnumVideoSizeCellObj = ModelEnumVideoSizeCell.GetModelEnum((int)userSettings.VideoSizeCell),
                ModelEnumVideoSizeWifiObj = ModelEnumVideoSizeWifi.GetModelEnum((int)userSettings.VideoSizeWifi),
                ListModelUserAccount = listModelUserAccount,
                DefaultModelUserAccountObj = defaultModelUserAccount,
                ModelEnumVoipEncryptionObj = ModelEnumVoipEncryption.GetModelEnum((int)userSettings.VoipEncryption),
                ListModelCodecSettingsAudioWifi = deviceSettings.CodecSettings.Where(obj => obj.Type == CodecTypeManaged.Audio && obj.ConnectionType == CodecConnectionTypeManaged.Wifi).Select(obj => new ModelCodecSettings { CodecSettingModelManagedObj = obj }).ToList(),
                ListModelCodecSettingsAudioCell = deviceSettings.CodecSettings.Where(obj => obj.Type == CodecTypeManaged.Audio && obj.ConnectionType == CodecConnectionTypeManaged.Cell).Select(obj => new ModelCodecSettings { CodecSettingModelManagedObj = obj }).ToList(),
                ListModelCodecSettingsVideo = deviceSettings.CodecSettings.Where(obj => obj.Type == CodecTypeManaged.Video).Select(obj => new ModelCodecSettings { CodecSettingModelManagedObj = obj }).ToList()
            };

            return result;
        }

        /// <summary> Сохранить настройки пользователя </summary>
        public static bool SaveModelUserSettings(ModelUserSettings modelUserSettings)
        {
            //var dtoUserSettings = GetDtoUserSettingsFromModelUserSettings(modelUserSettings);

            var userSettingsModelManaged = new UserSettingsModelManaged
            {
                Autologin = modelUserSettings.AutoLogin,
                Autostart = modelUserSettings.Autostart,
                DefaultVoipServer = modelUserSettings.DefaultModelUserAccountObj?.Domain,
                DoNotDesturbMode = modelUserSettings.DoNotDesturbMode,
                GuiAnimation = modelUserSettings.GuiAnimation,
                GuiFontSize = modelUserSettings.GuiFontSize,
                GuiLanguage = modelUserSettings.ModelLanguageObj.CodeName,
                GuiThemeName = ModelEnumTheme.ListModelEnum.First().CodeName, // modelUserSettings.ModelEnumThemeObj.CodeName, сейчас из BL приходит пустая тема и он возвращает null по этому закомментировал, т.к. тема пока одна
                TraceMode = modelUserSettings.TraceMode,
                UserExtendedStatus = modelUserSettings.UserExtendedStatus,
                VideoEnabled = modelUserSettings.VideoEnabled,
                EchoCancellationMode = (EchoCancellationModeManaged)modelUserSettings.ModelEnumEchoCancellationModeObj.Code,
                UserBaseStatus = (BaseUserStatusManaged)modelUserSettings.ModelEnumUserBaseStatusObj.Code,
                VideoSizeCell = (VideoSizeManaged)modelUserSettings.ModelEnumVideoSizeCellObj.Code,
                VideoSizeWifi = (VideoSizeManaged)modelUserSettings.ModelEnumVideoSizeWifiObj.Code,
                VoipEncryption = (VoipEncryptionModeManaged)modelUserSettings.ModelEnumVoipEncryptionObj.Code
            };

            var listCodecSettingModelManaged = new List<CodecSettingModelManaged>();

            listCodecSettingModelManaged.AddRange(modelUserSettings.ListModelCodecSettingsAudioWifi.Select(obj => obj.CodecSettingModelManagedObj));
            listCodecSettingModelManaged.AddRange(modelUserSettings.ListModelCodecSettingsAudioCell.Select(obj => obj.CodecSettingModelManagedObj));
            listCodecSettingModelManaged.AddRange(modelUserSettings.ListModelCodecSettingsVideo.Select(obj => obj.CodecSettingModelManagedObj));

            Logic.ChangeCodecSettings(listCodecSettingModelManaged.ToArray());

            return Logic.SaveUserSettings(userSettingsModelManaged);
        }

        /// <summary> Получить размер шрифта из настроек пользователя </summary>
        public static int GetGuiFontSize()
        {
            return Logic.GetUserSettings().GuiFontSize;
        }
    }
}
