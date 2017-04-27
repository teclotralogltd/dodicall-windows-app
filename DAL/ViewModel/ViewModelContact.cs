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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using dodicall;
using DAL.Abstract;
using DAL.Callback;
using DAL.Localization;
using DAL.Model;
using DAL.ModelEnum;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelContact : AbstractViewModel
    {
        ///<summary> Событие успешного удаления контакта </summary>
        public event EventHandler<ModelContact> DeleteModelContactSuccessful;

        /// <summary> Количетсво непрочитанных приглашений </summary>
        private int _countInvateUnread;

        /// <summary> Количетсво непрочитанных приглашений </summary>
        public int CountInvateUnread
        {
            get { return _countInvateUnread; }
            set
            {
                if (_countInvateUnread == value) return;
                _countInvateUnread = value;
                OnPropertyChanged("CountInvateUnread");
                OnPropertyChanged("CountInvateUnreadString");
            }
        }

        /// <summary> Количетсво непрочитанных приглашений строкой </summary>
        public string CountInvateUnreadString => CountInvateUnread > 99 ? @"99+" : CountInvateUnread.ToString();

        /// <summary> Последний выделенный контакт </summary>
        public ModelContact LastModelContact;

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
                    ChatList = false;
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
                    ChatList = false;
                    DialpadList = false;
                }

                OnPropertyChanged("HistoryList");
            }
        }

        /// <summary> Флаг выбора чатов </summary>
        private bool _chatList;

        /// <summary> Флаг выбора чатов </summary>
        public bool ChatList
        {
            get { return _chatList; }
            set
            {
                if (_chatList == value) return;
                _chatList = value;

                if (value)
                {
                    ContactList = false;
                    HistoryList = false;
                    DialpadList = false;
                }

                OnPropertyChanged("ChatList");
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
                    ChatList = false;
                }

                OnPropertyChanged("DialpadList");
            }
        }

        /// <summary> Текущее состояние сети </summary>
        private ModelConnectState _modelConnectStateObj;

        /// <summary> Текущее состояние сети </summary>
        public ModelConnectState ModelConnectStateObj
        {
            get { return _modelConnectStateObj; }
            set
            {
                if (_modelConnectStateObj == value) return;
                _modelConnectStateObj = value;
                OnPropertyChanged("ModelConnectStateObj");
            }
        }

        /// <summary> Ключ названия текущего наложенного фильтра </summary>
        private string _currentApplyFilterNameKey = @"ViewModelContact_AllContact";

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

                // сделано для восстановления выделения текущего контакта после изменения списка
                // нужно для обновления из другого потока в C++ логике т.к. сами объекты могут отличаться и на всякий случай лучше вставлять объект из новой коллекции
                // мало вероятно что это будет другой объект, но на всякий случай + задел на будущее если вдруг схема поменяется
                if (LastModelContact == null) return;
                var modelContact = _currentListModelContact?.FirstOrDefault(obj => obj.Id == LastModelContact.Id); // обработать фильтр наложение
                if (modelContact != null)
                {
                    _currentModelContact = modelContact; // св-во обновлять нельзя, т.к. если это будет один и тот же объект, то OnPropertyChanged не произойдет
                    OnPropertyChanged("CurrentModelContact");
                }
            }
        }

        /// <summary> Комманда применения фильтра Все контакты </summary>
        public Command CommandApplyFilterAllContact;

        /// <summary> Комманда применения фильтра dodicall </summary>
        public Command CommandApplyFilterDodicallContact;

        /// <summary> Комманда применения фильтра сохраненные контакты </summary>
        public Command CommandApplyFilterSavedContact;

        /// <summary> Комманда применения фильтра заблокированные контакты </summary>
        public Command CommandApplyFilterBlockedContact;

        /// <summary> Комманда применения фильтра белые контакты </summary>
        public Command CommandApplyFilterWhiteContact;

        ///<summary> Команда блокировки контакта </summary>
        public Command CommandBlockModelContact { get; set; }

        ///<summary> Команда разблокировки контакта </summary>
        public Command CommandUnblockModelContact { get; set; }

        ///<summary> Команда добавление контакта в белые </summary>
        public Command CommandAddToWhiteModelContact { get; set; }

        ///<summary> Команда удаление контакта в белые </summary>
        public Command CommandDeleteFromWhiteModelContact { get; set; }

        ///<summary> Команда удаления контакта </summary>
        public Command CommandDeleteModelContact { get; set; } 

        /// <summary> Конструктор </summary>
        public ViewModelContact()
        {
            ListModelContact = DataSourceContact.GetListModelContact();

            ApplyFilter(); // автоматически присвоит CurrentListModelContact

            CallbackRouter.Instance.ListModelContactChanged += OnListModelContactChanged;
            CallbackRouter.Instance.ListModelContactStatusChanged += OnListModelContactStatusChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged += OnListModelContactSubscriptionChanged;
            CallbackRouter.Instance.ModelConnectStateChanged += OnModelConnectStateChanged;
            CallbackRouter.Instance.PresenceOffline += OnPresenceOffline;

            CommandApplyFilterAllContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelContact_AllContact"));
            CommandApplyFilterDodicallContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelContact_DodicallContact"));
            CommandApplyFilterSavedContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelContact_SavedContact"));
            CommandApplyFilterBlockedContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelContact_BlockedContact"));
            CommandApplyFilterWhiteContact = new Command(obj => ApplyFilterListFilterContact(@"ViewModelContact_WhiteContact"));

            CommandAddToWhiteModelContact = new Command(obj => { if (DataSourceContact.AddToWhiteModelContact(CurrentModelContact)) ApplyFilter(); });
            CommandDeleteFromWhiteModelContact = new Command(obj => DeleteFromWhite());
            CommandBlockModelContact = new Command(obj => { if (DataSourceContact.BlockModelContact(CurrentModelContact)) ApplyFilter(); });
            CommandUnblockModelContact = new Command(obj => { if (DataSourceContact.UnblockModelContact(CurrentModelContact)) ApplyFilter(); });
            CommandDeleteModelContact = new Command(obj =>
            {
                if (DataSourceContact.DeleteModelContact(CurrentModelContact))
                {
                    ApplyFilter();
                    OnDeleteModelContactSuccessful();
                }
            }); 

            ModelConnectStateObj = DataSourceUtility.GetCurrentModelConnectState();

            ChangedInviteUnread();
        }

        /// <summary> Применить фильтр к контактам из списка фильров </summary>
        private void ApplyFilterListFilterContact(string filterKey)
        {
            _currentApplyFilterNameKey = filterKey;

            ApplyFilter();

            OnPropertyChanged("CurrentApplyFilterName");
        }

        /// <summary> Применить фильтр к контактам </summary>
        public void ApplyFilter()
        {
            var result = ListModelContact;

            if (_currentApplyFilterNameKey.Equals("ViewModelContact_AllContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.Blocked != true).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelContact_DodicallContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.IsDodicall && obj.Blocked != true).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelContact_SavedContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => !obj.IsDodicall && obj.Blocked != true).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelContact_BlockedContact", StringComparison.InvariantCultureIgnoreCase))
                result = result.Where(obj => obj.Blocked).ToList();

            if (_currentApplyFilterNameKey.Equals("ViewModelContact_WhiteContact", StringComparison.InvariantCultureIgnoreCase))
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

        ///<summary> Метод удаления из белого списка </summary>
        public void DeleteFromWhite()
        {
            if (DataSourceContact.DeleteFromWhiteModelContact(CurrentModelContact)) ApplyFilter();

            СheckСountWhiteContact();
        }

        ///<summary> Проверка количества контактов в белом списке </summary>
        private void СheckСountWhiteContact()
        {
            var countWhiteContact = DataSourceContact.GetCountWhiteContact();

            if (countWhiteContact == 0)
            {
                var CurrentModelUserSettings = DataSourceUserSettings.GetModelUserSettings();

                CurrentModelUserSettings.DoNotDesturbMode = false;

                DataSourceUserSettings.SaveModelUserSettings(CurrentModelUserSettings);
            }
        }

        /// <summary> Сортировка контактов </summary>
        private ObservableCollection<ModelContact> ListModelContactSort(List<ModelContact> listModelContact)
        {
            listModelContact.Sort((modelContact1, modelContact2) => CompareDependLocalization(modelContact1.FullName, modelContact2.FullName));

            // тут сделать сортировку в зависимости от языка + добавить в обработчик изменения языка

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

        /// <summary> Обработчик изменения списка контактов </summary>
        private void OnListModelContactChanged(List<ModelContact> listChangedModelContact, List<ModelContact> listDeletedModelContact)
        {
            foreach (var modelContact in listChangedModelContact.Where(obj => obj.Id > 0))
            {
                var changedModelContact = ListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

                if (changedModelContact != null)
                {
                    Action action = () =>
                    {
                        var modelContactCopy = modelContact.GetDeepCopy();

                        changedModelContact.Avatar = modelContactCopy.Avatar;
                        changedModelContact.Blocked = modelContactCopy.Blocked;
                        changedModelContact.DodicallId = modelContactCopy.DodicallId;
                        changedModelContact.NativeId = modelContactCopy.NativeId;
                        changedModelContact.White = modelContactCopy.White;
                        changedModelContact.XmppId = modelContactCopy.XmppId;
                        changedModelContact.FirstName = modelContactCopy.FirstName;
                        changedModelContact.MiddleName = modelContactCopy.MiddleName;
                        changedModelContact.LastName = modelContactCopy.LastName;
                        changedModelContact.ModelEnumUserBaseStatusObj = modelContactCopy.ModelEnumUserBaseStatusObj;
                        changedModelContact.UserExtendedStatus = modelContactCopy.UserExtendedStatus;
                        changedModelContact.ModelContactSubscriptionObj = modelContactCopy.ModelContactSubscriptionObj;
                        changedModelContact.ListModelUserContact = modelContactCopy.ListModelUserContact;
                        changedModelContact.ListModelUserContactExtra = modelContactCopy.ListModelUserContactExtra;
                    };

                    CurrentDispatcher.BeginInvoke(action);
                }
                else
                {
                    Action action = () =>
                    {
                        var modelContactCopy = modelContact.GetDeepCopy();

                        ListModelContact.Add(modelContactCopy);
                    };

                    CurrentDispatcher.BeginInvoke(action);
                }
            }

            foreach (var modelContact in listDeletedModelContact)
            {
                var deletedModelContact = ListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

                Action action = () => { ListModelContact.Remove(deletedModelContact); };

                CurrentDispatcher.BeginInvoke(action);
            }

            CurrentDispatcher.BeginInvoke(new Action(ApplyFilter));
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

        /// <summary> Обработчик изменения подписок контактов </summary>
        private void OnListModelContactSubscriptionChanged(object sender, List<PackageModelContactSubscription> listPackageModelContactSubscriptions)
        {
            CurrentDispatcher.BeginInvoke(new Action(ChangedInviteUnread));

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

        /// <summary> Обработчик изменения состояния подключений </summary>
        private void OnModelConnectStateChanged(object sender, ModelConnectState modelConnectState)
        {
            Action action = () => ModelConnectStateObj = DataSourceUtility.GetCurrentModelConnectState();

            CurrentDispatcher.BeginInvoke(action);
        }

        /// <summary> Обработчик отсутсвия подключений </summary>
        private void OnPresenceOffline(object sender, EventArgs eventArgs)
        {
            foreach (var i in ListModelContact)
            {
                i.ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(0);
            }
        }

        ///// <summary> Обработчик изменения модели внутри логики C++ </summary>
        //public void DoCallback(object sender, DoCallbackArgs e)
        //{
        //if (e.ModelName == "ContactsPresence")
        //{
        //    // поменял на этот некрасивый вариант для Perfomance
        //    //CurrentDispatcher.BeginInvoke(new Action(ChangedStatusListModelContact));

        //    ChangedStatusListModelContact(e.EntityIds);
        //}

        //if (e.ModelName == "Contacts")
        //{
        //    // поменял на этот некрасивый вариант для Perfomance
        //    //CurrentDispatcher.BeginInvoke(new Action(ChangedListModelContact));
        //    ChangedListModelContact();
        //}

        //if (e.ModelName == "ContactSubscriptions")
        //{
        //    CurrentDispatcher.BeginInvoke(new Action(ChangedInviteUnread));

        //    //Action action = () => ChangedListModelContactSubscription(e.EntityIds);

        //    //CurrentDispatcher.BeginInvoke(action);

        //    ChangedListModelContactSubscription(e.EntityIds);
        //}

        //if (e.ModelName == "NetworkStateChanged")
        //{
        //    Action action = () => ModelConnectStateObj = _dataSourceUtility.GetCurrentModelConnectState();

        //    CurrentDispatcher.BeginInvoke(action);
        //}
        //}

        ///// <summary> Обработчик изменения статусов в списке контактов внутри логики C++ </summary>
        //private void ChangedStatusListModelContact(string[] arrayXmppId)
        //{
        //    var dictionary = _dataSourceContact.GetModelContactStatusForAsync(arrayXmppId);

        //    foreach (var i in dictionary)
        //    {
        //        var modelContact = ListModelContact.FirstOrDefault(obj => obj.XmppId == i.Key);

        //        if (modelContact != null)
        //        {
        //            Action action = () =>
        //            {
        //                modelContact.ModelEnumUserBaseStatusObj = i.Value.ModelEnumUserBaseStatusObj;
        //                modelContact.UserExtendedStatus = i.Value.UserExtendedStatus;
        //            };

        //            CurrentDispatcher.BeginInvoke(action);
        //        }
        //    }
        //}

        ///// <summary> Обработчик изменения списка контактов внутри логики C++ </summary>
        //private void ChangedListModelContact()
        //{
        //    var listChangedModelContact = new List<ModelContact>();
        //    var listDeletedModelContact = new List<ModelContact>();

        //    _dataSourceContact.GetListChangedModelContact(listChangedModelContact, listDeletedModelContact);

        //    foreach (var modelContact in listChangedModelContact.Where(obj => obj.Id > 0))
        //    {
        //        var changedModelContact = ListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

        //        if (changedModelContact != null)
        //        {
        //            // поменял на этот некрасивый вариант для Perfomance
        //            Action action = () =>
        //            {
        //                var modelContactCopy = modelContact.GetDeepCopy();

        //                changedModelContact.Avatar = modelContactCopy.Avatar;
        //                changedModelContact.Blocked = modelContactCopy.Blocked;
        //                changedModelContact.DodicallId = modelContactCopy.DodicallId;
        //                changedModelContact.NativeId = modelContactCopy.NativeId;
        //                changedModelContact.White = modelContactCopy.White;
        //                changedModelContact.XmppId = modelContactCopy.XmppId;
        //                changedModelContact.FirstName = modelContactCopy.FirstName;
        //                changedModelContact.MiddleName = modelContactCopy.MiddleName;
        //                changedModelContact.LastName = modelContactCopy.LastName;
        //                changedModelContact.ModelEnumUserBaseStatusObj = modelContactCopy.ModelEnumUserBaseStatusObj;
        //                changedModelContact.UserExtendedStatus = modelContactCopy.UserExtendedStatus;
        //                changedModelContact.ModelContactSubscriptionObj = modelContactCopy.ModelContactSubscriptionObj;
        //                //Debug.WriteLine(modelContactCopy.FullName + " " + modelContactCopy.ModelContactSubscriptionObj.Ask);
        //                changedModelContact.ListModelUserContact = modelContactCopy.ListModelUserContact;
        //                changedModelContact.ListModelUserContactExtra = modelContactCopy.ListModelUserContactExtra;
        //            };

        //            CurrentDispatcher.BeginInvoke(action);

        //            //changedModelContact.Avatar = modelContact.Avatar;
        //            //changedModelContact.Blocked = modelContact.Blocked;
        //            //changedModelContact.DodicallId = modelContact.DodicallId;
        //            //changedModelContact.NativeId = modelContact.NativeId;
        //            //changedModelContact.White = modelContact.White;
        //            //changedModelContact.XmppId = modelContact.XmppId;
        //            //changedModelContact.FirstName = modelContact.FirstName;
        //            //changedModelContact.MiddleName = modelContact.MiddleName;
        //            //changedModelContact.LastName = modelContact.LastName;
        //            //changedModelContact.ModelEnumUserBaseStatusObj = modelContact.ModelEnumUserBaseStatusObj;
        //            //changedModelContact.UserExtendedStatus = modelContact.UserExtendedStatus;
        //            //changedModelContact.ModelContactSubscriptionObj = modelContact.ModelContactSubscriptionObj;
        //            //changedModelContact.ListModelUserContact = modelContact.ListModelUserContact;
        //            //changedModelContact.ListModelUserContactExtra = modelContact.ListModelUserContactExtra;
        //        }
        //        else
        //        {
        //            // поменял на этот некрасивый вариант для Perfomance
        //            Action action = () =>
        //            {
        //                var modelContactCopy = modelContact.GetDeepCopy();

        //                ListModelContact.Add(modelContactCopy);
        //            };

        //            CurrentDispatcher.BeginInvoke(action);

        //            //ListModelContact.Add(modelContact);
        //        }
        //    }

        //    foreach (var modelContact in listDeletedModelContact)
        //    {
        //        var deletedModelContact = ListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

        //        Action action = () => { ListModelContact.Remove(deletedModelContact); };

        //        CurrentDispatcher.BeginInvoke(action);

        //        //ListModelContact.Remove(deletedModelContact);
        //    }

        //    CurrentDispatcher.BeginInvoke(new Action(ApplyFilter));
        //}

        ///// <summary> Обработчик изменения подписки контактов внутри логики C++ </summary>
        //private void ChangedListModelContactSubscription(string[] arrayXmppId)
        //{
        //    if (ListModelContact == null) return;

        //    var dictionary = _dataSourceContact.GetDictionaryModelContactSubscriptionByArrayXmppId(arrayXmppId);

        //    foreach (var i in dictionary)
        //    {
        //        var modelContact = ListModelContact.FirstOrDefault(obj => obj.XmppId == i.Key);

        //        if (modelContact != null)
        //        {
        //            Action action = () => { modelContact.ModelContactSubscriptionObj = i.Value.GetDeepCopy(); };

        //            Debug.WriteLine(modelContact.FullName + " - " + i.Value.Ask);

        //            Thread.Sleep(1000);

        //            CurrentDispatcher.BeginInvoke(action);

        //            //modelContact.ModelContactSubscriptionObj = i.Value;
        //        }
        //    }

        //    _dataSourceContact.RefreshModelContactStatus(ListModelContact.Where(obj => dictionary.ContainsKey(obj.XmppId)).ToList());
        //}

        /// <summary> Обработчик изменения кол-во непрочитанных приглашений </summary>
        public void ChangedInviteUnread()
        {
            CountInvateUnread = DataSourceContact.GetCountInviteUnread();
        }

        ///<summary> Инвокатор события DeleteModelContactSuccessful </summary>
        private void OnDeleteModelContactSuccessful()
        {
            DeleteModelContactSuccessful?.Invoke(this, CurrentModelContact);
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("CurrentApplyFilterName");
            ApplyFilter();
        }
    }
}
