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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Enum;
using DAL.Localization;
using DAL.Utility;

namespace DAL.Model
{
    public class ModelServerArea : AbstractLocalization
    {
        /// <summary> Идентификатор </summary>
        public int Id;

        /// <summary> Назвкание на en </summary>
        public string NameEn;

        /// <summary> Назвкание на ru </summary>
        public string NameRu;

        /// <summary> Домен для ссылок </summary>
        public string Url;

        /// <summary> Адресс для ссылок восстановления пароля </summary>
        public string ForgotPassUrl;

        /// <summary> Адресс для ссылок регистрации </summary>
        public string RegistrationUrl;

        /// <summary> Адресс для ссылок оплаты услуг </summary>
        public string PayUrl;

        /// <summary> Название </summary>
        public string Name => LocalizationApp.GetInstance().ModelLanguageObj.CodeName.ToLower() == "ru" ? NameRu : NameEn;

        /// <summary> Слылка для регистрации </summary>
        public string UrlSignUp
        {
            get
            {
                var country = LocalizationApp.GetInstance().ModelLanguageObj.CodeName == "en" ? "gb" : "ru";
                var language = LocalizationApp.GetInstance().ModelLanguageObj.CodeName == "en" ? "en" : "ru";

                var result = Url + RegistrationUrl.Replace(@"${COUNTRY}", country).Replace(@"${LANG}", language);

                if (UtilityWeb.GetWebBrowserSetAsDefault() == EnumWebBrowser.IE)
                {
                    result = result.Replace(@"https://", @"").Replace(@"admin:", @"");
                }

                if (UtilityWeb.GetWebBrowserSetAsDefault() == EnumWebBrowser.EDGE)
                {
                    // очередной прикол от разработчиков Майкрософт и их супер новым браузером EDGE
                    result = result.Replace(@"admin:", @"");
                }

                return result;
            }
        }

        /// <summary> Слылка для восстановления пароля </summary>
        public string UrlForgotPassword
        {
            get
            {
                var country = LocalizationApp.GetInstance().ModelLanguageObj.CodeName == "en" ? "gb" : "ru";
                var language = LocalizationApp.GetInstance().ModelLanguageObj.CodeName == "en" ? "en" : "ru";

                var result = Url + ForgotPassUrl.Replace(@"${COUNTRY}", country).Replace(@"${LANG}", language);

                if (UtilityWeb.GetWebBrowserSetAsDefault() == EnumWebBrowser.IE)
                {
                    result = result.Replace(@"https://", @"").Replace(@"admin:", @"");
                }

                if (UtilityWeb.GetWebBrowserSetAsDefault() == EnumWebBrowser.EDGE)
                {
                    // очередной прикол от разработчиков Майкрософт и их супер новым браузером EDGE
                    result = result.Replace(@"admin:", @"");
                }

                return result;
            }
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("Name");
        }

        public static ModelServerArea GetDefaultModelServerArea()
        {
            return new ModelServerArea
            {
                Id = 0,
                NameEn = @"Industrial server",
                NameRu = @"Промышленный сервер"
            };
        }
    }
}
