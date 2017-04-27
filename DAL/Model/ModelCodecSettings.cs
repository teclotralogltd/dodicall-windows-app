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

namespace DAL.Model
{
    public class ModelCodecSettings : AbstractNotifyPropertyChanged
    {
        /// <summary> Объект из С++ исключительно для сохранения обратно настроек </summary>
        public CodecSettingModelManaged CodecSettingModelManagedObj;

        /// <summary> Наименование </summary>
        public string Name => CodecSettingModelManagedObj?.Name;

        /// <summary> Флаг доступности </summary>
        public bool Enabled
        {
            get { return CodecSettingModelManagedObj.Enabled; }
            set
            {
                if (CodecSettingModelManagedObj.Enabled == value) return;
                CodecSettingModelManagedObj.Enabled = value;
                OnPropertyChanged("Enabled");
            }
        }
    }
}
