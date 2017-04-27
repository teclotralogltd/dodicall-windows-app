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
using System.Windows;
using DAL.Model;

namespace dodicall.Localization
{
    public static class LocalizationUI
    {
        /// <summary> Словарь языка интерфейса по-умолчанию </summary>
        private static string DefaultLocalizationDictionaryUri
        {
            get
            {
                var result = @"pack://application:,,,/dodicall;component/Localization/Localization.lang.en.xaml";

                if (CultureInfo.CurrentCulture.Name.Substring(0, 2).ToLower() == @"ru")
                {
                    result = @"pack://application:,,,/dodicall;component/Localization/Localization.lang.ru.xaml";
                }

                return result;
            }
        }

        /// <summary> Обработчик изменения изменения языка приложения </summary>
        public static void ChangeLanguage(object sender, ModelLanguage modelLanguage)
        {
            if (modelLanguage == null) return;

            var resourceDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault(dict => dict.Source.ToString().Contains(@"Localization"));

            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);

            var localizationDictionaryUri = DefaultLocalizationDictionaryUri;

            switch (modelLanguage.CodeName.Trim().ToLower())
            {
                case "en":
                    localizationDictionaryUri = @"pack://application:,,,/dodicall;component/Localization/Localization.lang.en.xaml";
                    break;
                case "ru":
                    localizationDictionaryUri = @"pack://application:,,,/dodicall;component/Localization/Localization.lang.ru.xaml";
                    break;
            }

            resourceDictionary = new ResourceDictionary { Source = new Uri(localizationDictionaryUri) };

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
