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
using System.Windows.Media.Imaging;
using DAL.Utility;

namespace DAL.Model
{
    public class ModelLanguage
    {
        /// <summary> Путь к картинкам флагов </summary>
        private const string AssemblyPath = @"DAL.Resources.Flag.";

        /// <summary> Код языка ISO639 </summary>
        public int CodeNumber { get; private set; }

        /// <summary> Код языка ISO639 </summary>
        public string CodeName { get; private set; }

        /// <summary> Название </summary>
        public string Name { get; private set; }

        /// <summary> Описание </summary>
        public string Description { get; private set; }

        /// <summary> Картинка </summary>
        public BitmapImage Picture { get; private set; }

        /// <summary> Список языков </summary>
        private static List<ModelLanguage> _listModelLanguage;

        /// <summary> Список языков </summary>
        public static List<ModelLanguage> ListModelLanguage
        {
            get
            {
                if (_listModelLanguage == null)
                {
                    _listModelLanguage = new List<ModelLanguage>
                    {
                        new ModelLanguage { CodeNumber = 045, CodeName = @"en", Name = @"english", Description = @"English", Picture = UtilityPicture.GetBitmapImageFromStringPathAssembly(AssemblyPath + @"GB.png") },
                        new ModelLanguage { CodeNumber = 570, CodeName = @"ru", Name = @"русский", Description = @"Русский", Picture = UtilityPicture.GetBitmapImageFromStringPathAssembly(AssemblyPath + @"RUS.png") },
                    };
                }

                return _listModelLanguage;
            }
        }

        /// <summary> Получить ModelLanguage по коду </summary>
        public static ModelLanguage GetModelLanguage(int code)
        {
            return ListModelLanguage.FirstOrDefault(obj => obj.CodeNumber == code);
        }

        /// <summary> Получить ModelLanguage по коду </summary>
        public static ModelLanguage GetModelLanguage(string code)
        {
            var codeDefault = @"en";

            if (CultureInfo.CurrentCulture.Name.Substring(0, 2).ToLower() == @"ru") codeDefault = @"ru";

            return ListModelLanguage.FirstOrDefault(obj => obj.CodeName.Equals((String.IsNullOrWhiteSpace(code) ? codeDefault : code), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
