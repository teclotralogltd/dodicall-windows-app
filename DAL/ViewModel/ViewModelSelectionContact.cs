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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Callback;
using DAL.Localization;
using DAL.Model;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelSelectionContact : AbstractViewModel
    {
        /// <summary> Фильтр списка контактов </summary>
        private string _contactListFilter;

        /// <summary> Фильтр списка контактов </summary>
        public string ContactListFilter
        {
            get { return _contactListFilter; }
            set
            {
                if (_contactListFilter == value) return;
                _contactListFilter = value;

                ApplyFilter();

                OnPropertyChanged("ContactListFilter");
                OnPropertyChanged("SelectedModelContactList");
            }
        }

        /// <summary> Список контактов </summary>
        public readonly List<ModelContact> ListModelContact;

        /// <summary> Текущий список контактов </summary>
        private List<ModelContact> _currentListModelContact;

        /// <summary> Текущий список контактов </summary>
        public List<ModelContact> CurrentListModelContact
        {
            get { return _currentListModelContact; }
            set
            {
                if (_currentListModelContact == value) return;
                _currentListModelContact = value;

                OnPropertyChanged("CurrentListModelContact");
                OnPropertyChanged("SelectedModelContactList");
            }
        }

        /// <summary> Список выбранных контактов </summary>
        public readonly List<ModelContact> SelectedListModelContact = new List<ModelContact>();

        /// <summary> Список выбранных контактов </summary>
        public ObservableCollection<ModelContact> SelectedModelContactList
        {
            get
            {
                var result = new List<ModelContact>();

                foreach(var i in SelectedListModelContact)
                {
                    var modelContact = CurrentListModelContact.FirstOrDefault(obj => obj.DodicallId == i.DodicallId);

                    if(modelContact != null)
                    {
                        result.Add(modelContact);
                    }
                }

                return new ObservableCollection<ModelContact>(result);
            }
        }
        
        /// <summary> Кол-во выбранных пользователей </summary>
        public string CountSelectedModelContact
        {
            get
            {
                var result = String.Empty;

                var count = SelectedListModelContact.Count;

                var localization = LocalizationApp.GetInstance();

                if (localization.ModelLanguageObj.CodeName.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                {
                    result = count == 1 ? localization.GetValueByKey(@"ViewModelSelectionContact_User1to1") : localization.GetValueByKey(@"ViewModelSelectionContact_User2to9");
                }

                if (localization.ModelLanguageObj.CodeName.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
                {
                    var last2Number = count % 100;

                    if (last2Number > 10 && last2Number < 19)
                    {
                        result = localization.GetValueByKey(@"ViewModelSelectionContact_User5to9");
                    }
                    else
                    {
                        var last1Number = last2Number % 10;

                        if (last1Number == 1) result += localization.GetValueByKey(@"ViewModelSelectionContact_User1to1");

                        if (last1Number > 1 && last1Number < 5) result += localization.GetValueByKey(@"ViewModelSelectionContact_User2to4");

                        if (last1Number < 1 || last1Number > 4) result += localization.GetValueByKey(@"ViewModelSelectionContact_User5to9");
                    }
                }

                return result;
            }
        }

        /// <summary> Конструктор (без сохраненных контактов и заблокированных) </summary>
        public ViewModelSelectionContact()
        {
            ListModelContact = DataSourceContact.GetListModelContact().Where(obj => obj.IsDodicall && obj.Blocked == false && obj.IsAccessStatus).ToList();

            ApplyFilter(); // автоматически присвоит CurrentListModelContact

            CallbackRouter.Instance.ListModelContactStatusChanged += OnListModelContactStatusChanged;

            CallbackRouter.Instance.ListModelContactSubscriptionChanged += OnListModelContactSubscriptionChanged;
        }

        /// <summary> Применить фильтр к контактам </summary>
        private void ApplyFilter()
        {
            if (!String.IsNullOrWhiteSpace(ContactListFilter))
            {
                var filter = ContactListFilter.Trim().ToLower();

                CurrentListModelContact = ListModelContact.Where(obj => obj.FullName.ToLower().Contains(filter)).ToList();
            }
            else
            {
                CurrentListModelContact = ListModelContact;
            }

            ListModelContactSort();

            Action action = () => OnEventViewModel("ChangedList");

            CurrentDispatcher.BeginInvoke(action);
        }

        /// <summary> Сортировка контактов </summary>
        private void ListModelContactSort()
        {
            // тут сделать сортировку в зависимости от языка + добавить в обработчик изменения языка

            CurrentListModelContact.Sort((modelContact1, modelContact2) => String.Compare(modelContact1.FullName, modelContact2.FullName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary> Добавить контакт в список выбранных </summary>
        public void SelectModelContact(ModelContact modelContact)
        {
            if (SelectedListModelContact.FirstOrDefault(obj => obj.DodicallId == modelContact.DodicallId) != null) return;

            SelectedListModelContact.Add(modelContact);

            OnEventViewModel("ChangedCountSelected");
            OnPropertyChanged("CountSelectedModelContact");
            OnPropertyChanged("SelectedModelContactList");
        }

        /// <summary> Добавить список контактов в список выбранных </summary>
        public void SelectListModelContact(List<ModelContact> listModelContact)
        {
            foreach(var i in listModelContact)
            {
                SelectModelContact(i);
            }

            OnEventViewModel("ChangedCountSelected");
            OnPropertyChanged("CountSelectedModelContact");
            OnPropertyChanged("SelectedModelContactList");
        }

        /// <summary> Удалить контакт из список выбранных </summary>
        public void UnselectModelContact(ModelContact modelContact)
        {
            //SelectedListModelContact.Remove(modelContact); ХЗ почему не работает
            SelectedListModelContact.Remove(SelectedListModelContact.FirstOrDefault(obj=>modelContact.DodicallId == obj.DodicallId));
            OnEventViewModel("ChangedCountSelected");
            OnPropertyChanged("CountSelectedModelContact");
            OnPropertyChanged("SelectedModelContactList");
        }

        /// <summary> Обработчик изменения статусов контактов </summary>
        private void OnListModelContactStatusChanged(object sender, List<PackageModelContactStatus> listPackageModelContactStatuses)
        {
            foreach (var i in listPackageModelContactStatuses)
            {
                var modelContact = ListModelContact.FirstOrDefault(obj => obj.XmppId == i.XmppId);

                if (modelContact != null)
                {
                    Action action = () =>
                    {
                        modelContact.ModelEnumUserBaseStatusObj = i.ModelEnumUserBaseStatusObj;
                        modelContact.UserExtendedStatus = i.UserExtendedStatus;
                    };

                    CurrentDispatcher.BeginInvoke(action);
                }
            }

            OnPropertyChanged("CurrentListModelContact");
            OnPropertyChanged("SelectedModelContactList");
        }

        /// <summary> Обработчик изменения состояний подписки контактов </summary>
        private void OnListModelContactSubscriptionChanged(object sender, List<PackageModelContactSubscription> packageModelContactSubscriptions)
        {
            CurrentListModelContact = DataSourceContact.GetListModelContact().Where(obj => obj.IsDodicall && obj.Blocked == false && obj.IsAccessStatus).ToList();

            ListModelContactSort();

            Action action = () => OnEventViewModel("ChangedList");

            CurrentDispatcher.BeginInvoke(action);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("CountSelectedModelContact");
        }
    }
}
