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
using DAL.Localization;
using DAL.Model;
using DAL.ModelEnum;

namespace DAL.Abstract
{
    public abstract class AbstractModelEnum<T> : AbstractLocalization where T : AbstractModelEnum<T>//, new()
    {
        /// <summary> Код </summary>
        public int Code { get; protected set; }

        /// <summary> Код строковый </summary>
        public string CodeName { get; protected set; }

        /// <summary> Ключ в словаре Название </summary>
        protected string _keyName;

        /// <summary> Название </summary>
        public string Name => LocalizationApp.GetInstance().GetValueByKey(_keyName);

        /// <summary> Ключ в словаре Описание </summary>
        protected string _keyDescription;

        /// <summary> Описание </summary>
        public string Description => LocalizationApp.GetInstance().GetValueByKey(_keyDescription);

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
        }

        /// <summary> Список ModelEnum </summary>
        public static List<T> ListModelEnum { get; protected set; }

        /// <summary> Получить ModelEnum по коду </summary>
        public static T GetModelEnum(int code)
        {
            return ListModelEnum.FirstOrDefault(obj => obj.Code == code);
        }

        /// <summary> Получить ModelEnum по строковому коду </summary>
        public static T GetModelEnum(string code)
        {
            return ListModelEnum.FirstOrDefault(obj => obj.CodeName.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }











        // ------------- Закоментировать это всё -------------

        /// <summary> Возвращает глубокую копию объекта </summary>
        //public T GetDeepCopy()
        //{
        //    return new T
        //    {
        //        Code = Code,
        //        _keyName = _keyName,
        //        _keyDescription = _keyDescription
        //    };
        //}

        /// <summary> Переопределение оператора == (нужно когда делается GetDeepCopy() для сравнения объектов, нужно в свойствах объектов когда сравниваем с value нового значения) </summary>
        public static bool operator == (AbstractModelEnum<T> a, AbstractModelEnum<T> b)
        {
            var result = false;

            if ((object)a != null && (object)b != null && a.Code == b.Code) result = true;

            if ((object)a == null && (object)b == null) result = true;
            
            return result;
        }

        /// <summary> Переопределение оператора != (нужно когда делается GetDeepCopy() для сравнения объектов, нужно в свойствах объектов когда сравниваем с value нового значения) </summary>
        public static bool operator != (AbstractModelEnum<T> a, AbstractModelEnum<T> b)
        {
            return !(a == b);
        }

        // ------------- Закоментировать это всё -------------
    }
}
