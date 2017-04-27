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
using System.ComponentModel;
using System.Diagnostics;

namespace DAL.Abstract
{
    public abstract class AbstractNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            CheckPropertyName(e.PropertyName);

            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        [Conditional("DEBUG")]
        private void CheckPropertyName(string propertyName)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];

            if (propertyDescriptor == null)
            {
                throw new InvalidOperationException($@"Свойства с именем '{propertyName}' не существует.");
            }
        }
    }
}