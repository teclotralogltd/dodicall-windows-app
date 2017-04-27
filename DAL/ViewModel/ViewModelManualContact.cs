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
using DAL.Localization;
using DAL.Model;
using DAL.ModelEnum;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelManualContact : AbstractViewModel
    {
        /// <summary> Событие успешного добавления пользователя </summary>
        public event EventHandler<ModelContact> AddManualContactSuccessful;

        /// <summary> Событие добавления телефона контакта </summary>
        public event EventHandler ChangedListModelUserContactExtra;

        /// <summary> Текущий ModelContact </summary>
        public ModelContact CurrentModelContact { get; set; }

        /// <summary> Список типов номеров контакта </summary>
        public List<ModelEnumUserContactType> ListModelEnumUserContactType { get; set; } = ModelEnumUserContactType.ListModelEnum;

        /// <summary> Команда сохранения контакта </summary>
        public Command CommandSave { get; set; }

        /// <summary> Команда добавления телефонного номера </summary>
        public Command CommandAddUserContact { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelManualContact()
        {
            CurrentModelContact = new ModelContact { ListModelUserContactExtra = new List<ModelUserContact>() };

            CommandSave = new Command(obj => Save());

            CommandAddUserContact = new Command(obj => AddUserContact());

            AddUserContact();
        }

        /// <summary> Конструктор </summary>
        public ViewModelManualContact(string number)
        {
            CurrentModelContact = new ModelContact { ListModelUserContactExtra = new List<ModelUserContact> { new ModelUserContact { Manual = true, ModelEnumUserContactTypeObj = ModelEnumUserContactType.GetModelEnum(2), Identity = number } } };

            CommandSave = new Command(obj => Save());

            CommandAddUserContact = new Command(obj => AddUserContact());

            //AddUserContact();
        }

        /// <summary> Метод сохранения контакта </summary>
        private void Save()
        {
            CurrentModelContact.ListModelUserContactExtra = CurrentModelContact.ListModelUserContactExtra.Where(obj => !String.IsNullOrWhiteSpace(obj.Identity.Trim('+')))?.ToList();

            if (DataSourceContact.SaveModelContact(CurrentModelContact))
            {
                AddManualContactSuccessful?.Invoke(this, CurrentModelContact); // кастыль для демо
                OnCloseView();
            }
            else
            {
                throw new Exception(LocalizationApp.GetInstance().GetValueByKey(@"ViewModelManualContact_ContactNotSaved"));
            }
        }

        /// <summary> Метод добавляющий телефонный номер </summary>
        private void AddUserContact()
        {
            if (CurrentModelContact.ListModelUserContactExtra.Count >= 8 ) return;

            CurrentModelContact.ListModelUserContactExtra.Add(new ModelUserContact { Manual = true, ModelEnumUserContactTypeObj = ModelEnumUserContactType.GetModelEnum(2), Identity = @"+" });

            ChangedListModelUserContactExtra?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Метод удаляющий телефонный номер </summary>
        public void RemoveUserContact(ModelUserContact modelUserContact)
        {
            CurrentModelContact.ListModelUserContactExtra.Remove(modelUserContact);

            ChangedListModelUserContactExtra?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Метод форматирования телефонного номера </summary>
        public string FormatPhone(string phone)
        {
            return DataSourceContact.FormatPhone(phone);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }
    }
}
