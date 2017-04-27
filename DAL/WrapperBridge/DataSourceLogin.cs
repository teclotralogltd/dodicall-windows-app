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
using System.Security;
using System.Text;
using System.Threading.Tasks;
using dodicall;
using DAL.Abstract;
using DAL.Enum;
using DAL.Model;
using DAL.ModelEnum;
using DAL.Utility;

namespace DAL.WrapperBridge
{
    public class DataSourceLogin : AbstractDataSource
    {
        /// <summary> Текущая площадка (для доступа из любого места) </summary>
        public static ModelServerArea CurrentModelServerArea { get; private set; }

        /// <summary> Авторизация пользователя </summary>
        public static EnumResultLogin Login(ModelLogin modelLogin)
        {
            if (String.IsNullOrWhiteSpace(modelLogin.Login) || modelLogin.Password == null) return EnumResultLogin.AuthFailed;

            var lastLoginUserCryptKey = UtilitySecurity.LoadPrivateCryptKeyFromIsolatedStorage(modelLogin.Login, modelLogin.ServerAreaCode.ToString());   
            var result = EnumResultLogin.No;
   
            if (modelLogin.LastModelAutoLogin)
            { 
                var success = Logic.TryAutoLoginWithPassword(modelLogin.Password, lastLoginUserCryptKey);
                //Если при автологине считался неверный пароль, нужно логинится методом  Logic.Login для этого обнуляется переменная modelLogin.LastModelAutoLogin
                modelLogin.LastModelAutoLogin = success;
                result = success ? EnumResultLogin.No : EnumResultLogin.SystemError;
            }
            else
            {
                var resultLogin = Logic.Login(modelLogin.Login.Trim(), modelLogin.Password, modelLogin.ServerAreaCode, lastLoginUserCryptKey);

                // сохранение дополнительных пораметров при логине пользователя, хотелось бы сразу их передавать в Logic.Login, но Серега сказал пока сделать так
                if (resultLogin.Success)
                {
                    var modelUserSettings = DataSourceUserSettings.GetModelUserSettings();
                    modelUserSettings.AutoLogin = modelLogin.AutoLogin;
                    modelUserSettings.ModelLanguageObj = modelLogin.ModelLanguageObj;
                    modelUserSettings.Autostart = modelLogin.Autostart;

                    var resultSaveUserSettings = DataSourceUserSettings.SaveModelUserSettings(modelUserSettings);

                    if (resultSaveUserSettings == false) result = EnumResultLogin.SystemError;

                    var resultSavePasswordToStorage = UtilitySecurity.SavePasswordToIsolatedStorage(modelLogin.Password, modelLogin.Login); 

                    if (!resultSavePasswordToStorage) result = EnumResultLogin.SystemError;
                }
                else
                {
                    switch (resultLogin.ErrorCode)
                    {
                        case ResultErrorCodeManaged.AuthFailed:
                            result = EnumResultLogin.AuthFailed;
                            break;
                        case ResultErrorCodeManaged.NoNetwork:
                            result = EnumResultLogin.NoNetwork;
                            break;
                        default:
                            result = EnumResultLogin.SystemError;
                            break;
                    }
                } 
            }

            if (result == EnumResultLogin.No)
            {
                if (modelLogin.ModelServerAreaObj != null)
                {
                    CurrentModelServerArea = modelLogin.ModelServerAreaObj;
                }
                else
                {
                    List<ModelServerArea> listModelServerArea;

                    if (GetListModelServerArea(out listModelServerArea) == EnumResultLogin.No) CurrentModelServerArea = listModelServerArea.FirstOrDefault(obj => obj.Id == modelLogin.ServerAreaCode);
                }
            }

            return result;
        }

        /// <summary> Логаут пользователя </summary>
        public static void Logout()
        {
            Logic.Logout();
        }

        /// <summary> Получить последнего залогинившегося пользователя </summary>
        public static ModelLogin GetLastModelLogin()
        {
            var lastLogin = Logic.GetGlobalApplicationSettings();

            var modelLogin = new ModelLogin
            {
                Login = lastLogin.LastLogin, //lastUser.Autologin ? lastUser.LastLogin : String.Empty,
                Password = lastLogin.Autologin && !String.IsNullOrWhiteSpace(lastLogin.LastLogin) ? UtilitySecurity.LoadPasswordFromIsolatedStorage(lastLogin.LastLogin) : null,
                AutoLogin = lastLogin.Autologin,
                LastModelAutoLogin = lastLogin.Autologin,
                Autostart = lastLogin.Autostart,
                ModelLanguageObj = ModelLanguage.GetModelLanguage(lastLogin.DefaultGuiLanguage),
                ServerAreaCode = (int)lastLogin.Area // т.к. СВ сказал не подгружать площадки если ServerAreaCode == 0
            };

            if (String.IsNullOrWhiteSpace(modelLogin.Login) || modelLogin.Password == null)
            { 
                modelLogin.AutoLogin = false;
                modelLogin.LastModelAutoLogin = false; 
            }

            return modelLogin;
        }

        /// <summary> Сохранение языка приложения до авторизации пользователя </summary>
        public static void SaveModelLanguage(ModelLanguage modelLanguage)
        {
            Logic.SaveDefaultGuiLanguage(modelLanguage.CodeName);
        }

        /// <summary> Возвращает список доступных площадок </summary>
        public static EnumResultLogin GetListModelServerArea(out List<ModelServerArea> listModelServerArea)
        {
            listModelServerArea = new List<ModelServerArea>();

            Dictionary<int, ServerAreaModelManaged> listArea;

            var result = Logic.RetrieveAreas(out listArea);

            if (listArea != null && listArea.Count > 0)
            {
                foreach (var i in listArea)
                {
                    listModelServerArea.Add(new ModelServerArea { Id = i.Key, NameEn = i.Value.NameEn, NameRu = i.Value.NameRu, Url = i.Value.LcUrl, ForgotPassUrl = i.Value.ForgotPwd, RegistrationUrl = i.Value.Reg, PayUrl = i.Value.PayUrl });
                }
            }
            else
            {
                result.Success = false;
            }

            // СВ сказал что если не получилось подгрузить площадки то под логином пишем тот же SystemError что и при логине

            var networkStateCode = DataSourceUtility.GetCurrentModelConnectState().ModelEnumNetworkTechnologyObj.Code;

            return result.Success ? EnumResultLogin.No : networkStateCode == 0 ? EnumResultLogin.NoNetwork : EnumResultLogin.SystemError;
        }
    }
}
