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

namespace dodicall.Converter
{
    public class ConverterBoolToVisibilityHidden : ConvertorBase<ConverterBoolToVisibilityHidden>
    {
        /// <summary> Конвертация </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (bool)value;

            var result = Visibility.Hidden;

            if (visibility) result = Visibility.Visible;

            return result;
        }

        /// <summary> Обратная конвертация </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
