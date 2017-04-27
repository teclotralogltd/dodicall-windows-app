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
using DAL.Abstract;
using DAL.Model;
using DAL.ModelEnum;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelUserStatus : AbstractViewModel
    {
        /// <summary> Список статусов </summary>
        public List<ModelEnumUserBaseStatus> ListModelEnumUserBaseStatus { get; set; } = ModelEnumUserBaseStatus.ListModelEnum;

        /// <summary> Настройки пользователя </summary>
        public ModelUserSettings CurrentModelUserSettings { get; set; }

        /// <summary> Команда сохранения настроек </summary>
        public Command CommandSave { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelUserStatus()
        {
            CurrentModelUserSettings = DataSourceUserSettings.GetModelUserSettings();

            CommandSave = new Command(obj => Save());
        }

        /// <summary> Метод сохранения настроек </summary>
        private void Save()
        {
            DataSourceUserSettings.SaveModelUserSettings(CurrentModelUserSettings);

            OnCloseView();
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            
        }
    }
}
