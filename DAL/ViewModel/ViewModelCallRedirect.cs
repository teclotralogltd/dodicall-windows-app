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
using DAL.Utility;
using System.Security;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using DAL.Callback;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace DAL.ViewModel
{
    public class ViewModelCallRedirect : AbstractViewModel, IDisposable
    {
        /// <summary> Текущий звонок который переадресовывается</summary>
        private ModelCall _currentModelCall;

        /// <summary> Текущий звонок который переадресовывается </summary>
        public ModelCall CurrentModelCall
        {
            get { return _currentModelCall; }
            set
            {
                if (_currentModelCall == value) return;
                _currentModelCall = value;
                OnPropertyChanged("CurrentModelCall");
            }
        }

        /// <summary> Текущий контакт </summary>
        private ModelContact _currentModelContact;

        /// <summary> Текущий контакт </summary>
        public ModelContact CurrentModelContact
        {
            get { return _currentModelContact; }
            set
            {
                if (_currentModelContact == value) return;
                _currentModelContact = value;
                OnPropertyChanged("CurrentModelContact");
            }
        }

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
            }
        }

        /// <summary> Флаг выбора контактов </summary>
        private bool _contactList;

        /// <summary> Флаг выбора контактов </summary>
        public bool ContactList
        {
            get { return _contactList; }
            set
            {
                if (_contactList == value) return;
                _contactList = value;

                if (value)
                {
                    HistoryList = false; 
                    DialpadList = false;
                }

                OnPropertyChanged("ContactList");
            }
        }

        /// <summary> Флаг выбора история </summary>
        private bool _historyList;

        /// <summary> Флаг выбора история </summary>
        public bool HistoryList
        {
            get { return _historyList; }
            set
            {
                if (_historyList == value) return;
                _historyList = value;

                if (value)
                {
                    ContactList = false; 
                    DialpadList = false;
                }

                OnPropertyChanged("HistoryList");
            }
        }

        /// <summary> Флаг выбора диалпада </summary>
        private bool _dialpadList;
          
        /// <summary> Флаг выбора диалпада </summary>
        public bool DialpadList
        {
            get { return _dialpadList; }
            set
            {
                if (_dialpadList == value) return;
                _dialpadList = value;

                if (value)
                {
                    ContactList = false;
                    HistoryList = false; 
                }

                OnPropertyChanged("DialpadList");
            }
        }

        /// <summary> Ключ названия текущего наложенного фильтра </summary>
        private string _currentApplyFilterNameKey = @"ViewModelCallRedirect_AllContact";

        /// <summary> Название текущего наложенного фильтра </summary>
        public string CurrentApplyFilterName => LocalizationApp.GetInstance().GetValueByKey(_currentApplyFilterNameKey);

        /// <summary> Список контактов </summary>
        public readonly List<ModelContact> ListModelContact;

        /// <summary> Текущий список контактов </summary>
        private ObservableCollection<ModelContact> _currentListModelContact;

        /// <summary> Текущий список контактов </summary>
        public ObservableCollection<ModelContact> CurrentListModelContact
        {
            get { return _currentListModelContact; }
            set
            {
                if (_currentListModelContact == value) return;
                _currentListModelContact = value;
                OnPropertyChanged("CurrentListModelContact"); 
            }
        }

        /// <summary> Комманда применения фильтра Все контакты </summary>
        public Command CommandApplyFilterAllContact { get; set; }

        /// <summary> Комманда применения фильтра dodicall </summary>
        public Command CommandApplyFilterDodicallContact { get; set; }

        /// <summary> Комманда применения фильтра сохраненные контакты </summary>
        public Command CommandApplyFilterSavedContact { get; set; }

        /// <summary> Комманда применения фильтра заблокированные контакты </summary>
        public Command CommandApplyFilterBlockedContact { get; set; }

        /// <summary> Комманда применения фильтра белые контакты </summary>
        public Command CommandApplyFilterWhiteContact { get; set; }

        /// <summary> Комманда выбора списка контактов </summary>
        public Command CommandSelectContactList { get; set; }

        /// <summary> Комманда выбора списка истории</summary>
        public Command CommandSelectHistoryList { get; set; }

        /// <summary> Комманда выбора диалпада</summary>
        public Command CommandSelectDialpadList { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelCallRedirect(ModelCall currentModelCall)
        {
            var callerModelContact = currentModelCall?.ModelContactObj;

            ListModelContact = callerModelContact == null ? DataSourceContact.GetListModelContact() : DataSourceContact.GetListModelContact().Where(a => a.Id != currentModelCall.ModelContactObj.Id).ToList();
            
            CurrentModelCall = currentModelCall;

            ApplyFilter(); // автоматически присвоит CurrentListModelContact
             
            CallbackRouter.Instance.ListModelContactStatusChanged += OnListModelContactStatusChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged += OnListModelContactSubscriptionChanged;
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;

            CommandApplyFilterAllContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelCallRedirect_AllContact"));
            CommandApplyFilterDodicallContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelCallRedirect_DodicallContact"));
            CommandApplyFilterSavedContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelCallRedirect_SavedContact"));
            CommandApplyFilterBlockedContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelCallRedirect_BlockedContact"));
            CommandApplyFilterWhiteContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelCallRedirect_WhiteContact")); 

            CommandSelectContactList = new Command(obj => SelectContactList());
            CommandSelectHistoryList = new Command(obj => SelectHistoryList());
            CommandSelectDialpadList = new Command(obj => SelectDialpadList()); 

            ContactList = true;  
        } 

        /// <summary> Обработчик изменения подписок контактов </summary>
        private void OnListModelContactSubscriptionChanged(object sender, List<PackageModelContactSubscription> listPackageModelContactSubscriptions)
        {  
            foreach (var i in listPackageModelContactSubscriptions)
            {
                var modelContact = ListModelContact.FirstOrDefault(obj => obj.XmppId == i.XmppId);

                if (modelContact != null)
                {
                    Action action = () => { modelContact.ModelContactSubscriptionObj = i.ModelContactSubscriptionObj.GetDeepCopy(); };

                    //Debug.WriteLine(modelContact.FullName + " - " + i.ModelContactSubscriptionObj.Ask);

                    Thread.Sleep(1000);

                    CurrentDispatcher.BeginInvoke(action);
                }
            }

            DataSourceContact.RefreshModelContactStatus(ListModelContact.Where(obj => listPackageModelContactSubscriptions.Exists(o => o.XmppId == obj.XmppId)).ToList());
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
        }

        /// <summary> Применить фильтр к контактам </summary>
        public void ApplyFilter()
        {
            var result = ListModelContact;

            if (_currentApplyFilterNameKey.Equals("ViewModelCallRedirect_AllContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.Blocked != true).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelCallRedirect_DodicallContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.IsDodicall && obj.Blocked != true).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelCallRedirect_SavedContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => !obj.IsDodicall && obj.Blocked != true).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelCallRedirect_BlockedContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.Blocked).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelCallRedirect_WhiteContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.White && obj.Blocked != true).ToList();

            if (!String.IsNullOrWhiteSpace(ContactListFilter))
            {
                var filter = ContactListFilter.Trim().ToLower();

                result = result.Where(obj => obj.FullName.ToLower().Contains(filter) ||
                                             obj.ListModelUserContact.Any(c => c.Identity.ToLower().Contains(filter)) ||
                                             obj.ListModelUserContactExtra.Any(c => c.Identity.ToLower().Contains(filter))
                                     ).ToList();
            }

            CurrentListModelContact = ListModelContactSort(result);
        }

        /// <summary> Сортировка контактов </summary>
        private ObservableCollection<ModelContact> ListModelContactSort(List<ModelContact> listModelContact)
        {
            listModelContact.Sort((modelContact1, modelContact2) => CompareDependLocalization(modelContact1.FullName, modelContact2.FullName));

            // тут сделать сортировку в зависимости от языка + добавить в обработчик изменения языка
            // комментарий из ViewModelContact взять изменения оттуда либо добавить потом и туда

            return new ObservableCollection<ModelContact>(listModelContact);
        }

        /// <summary> Сортировка контактов </summary>
        private int CompareDependLocalization(string str1, string str2)
        {
            // -1 str1 или 0 str1=str2 или 1 str2

            var result = 0;

            var length = str1.Length < str2.Length ? str1.Length : str2.Length;

            var r = new Regex(@"[a-z]", RegexOptions.IgnoreCase);

            if (LocalizationApp.GetInstance().ModelLanguageObj.CodeName == "ru")
            {
                r = new Regex(@"[а-я]", RegexOptions.IgnoreCase);
            }

            for (int i = 0; i < length; i++)
            {
                var char1 = str1[i].ToString();
                var char2 = str2[i].ToString();

                var result1 = r.Match(char1);

                var result2 = r.Match(char2);

                if (result1.Success && result2.Success)
                {
                    result = String.Compare(char1, char2, StringComparison.InvariantCultureIgnoreCase);

                    if (result != 0) return result;
                }

                if (result1.Success && !result2.Success)
                {
                    result = -1;

                    return result;
                }

                if (!result1.Success && result2.Success)
                {
                    result = 1;

                    return result;
                }

                if (!result1.Success && !result2.Success)
                {
                    var symbol1 = str1[i];
                    var symbol2 = str2[i];

                    if (Char.IsLetter(symbol1) && Char.IsLetter(symbol2))
                    {
                        result = String.Compare(char1, char2, StringComparison.InvariantCultureIgnoreCase);
                    }

                    if (Char.IsLetter(symbol1) && !Char.IsLetter(symbol2))
                    {
                        result = -1;
                    }

                    if (!Char.IsLetter(symbol1) && Char.IsLetter(symbol2))
                    {
                        result = 1;
                    }

                    if (!Char.IsLetter(symbol1) && !Char.IsLetter(symbol2))
                    {
                        result = String.Compare(char1, char2, StringComparison.InvariantCultureIgnoreCase);
                    }

                    if (result != 0) return result;
                }
            }

            return result;
        } 

        /// <summary> Применить фильтр к контактам из списка фильров </summary>
        private void ApplyFilterListFilterContact(string filterKey)
        {
            _currentApplyFilterNameKey = filterKey;

            ApplyFilter();

            OnPropertyChanged("CurrentApplyFilterName");
        }

        /// <summary> Выбрать вкладку контактов </summary>
        private void SelectContactList()
        {
            ContactList = true;
        }

        /// <summary> Выбрать вкладку истории </summary>
        private void SelectHistoryList()
        {
            HistoryList = true;
        }

        /// <summary> Выбрать вкладку диалпада </summary>
        private void SelectDialpadList()
        {
            DialpadList = true;
        }

        /// <summary> Перевести вызов </summary>
        public bool TransferCall(string number)
        {
            return DataSourceCall.TransferCallToUrl(CurrentModelCall.Id, number);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
             OnPropertyChanged("CurrentApplyFilterName");
             ApplyFilter();
        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        { 
            CallbackRouter.Instance.ListModelContactStatusChanged -= OnListModelContactStatusChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged -= OnListModelContactSubscriptionChanged;
            CallbackRouter.Instance.ModelCallChanged -= OnModelCallChanged;
        }

        /// <summary> Обработчик изменения звонка </summary>
        private void OnModelCallChanged(object sender, ModelCall modelCall)
        {
            Action action = () =>
            { 
                CurrentModelCall = modelCall?.GetDeepCopy(); 

                if (CurrentModelCall == null || CurrentModelCall.ModelEnumCallStateObj.Code == 6) // Ended (почему то иногда прилетает null, видимо в разных потоках уже закрытый звонок удаляется из списка активных в бизнес логике быстрее чем прилетает данный колбек в этом потоке)
                {
                    OnCloseView();
                }
            };

            CurrentDispatcher.BeginInvoke(action);
        } 
    }
}
