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

using DAL.Abstract;
using DAL.Localization;
using DAL.Model;
using DAL.WrapperBridge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.Callback;
using DAL.ModelEnum;

namespace DAL.ViewModel
{
    public class ViewModelChatDetail : AbstractViewModel, IDisposable
    {
        /// <summary> Текущий размер шрифта </summary>
        public int FontSize
        {
            get
            {
                var fontSize = DataSourceUserSettings.GetGuiFontSize();

                FontSizeLittle = fontSize - 4;

                OnPropertyChanged(@"FontSizeLittle");

                return fontSize;
            }
        }

        /// <summary> Текущий размер шрифта цитированого сообщения </summary>
        public int FontSizeLittle { get; set; }

        /// <summary> Текущее сообщение </summary>
        private ModelChatMessage _currentModelChatMessage = new ModelChatMessage();

        /// <summary> Сообщение со вложением это простой ответ или цитирование </summary>
        private bool _justAnswerCurrentMessage;

        /// <summary> Сообщение со вложением это простой ответ или цитирование </summary>
        public bool JustAnswerCurrentMessage
        {
            get { return _justAnswerCurrentMessage; }
            set
            {
                if (_justAnswerCurrentMessage == value) return;

                _justAnswerCurrentMessage = value;

                OnPropertyChanged("JustAnswerCurrentMessage");
            }
        }

        /// <summary> Текущее сообщение </summary>
        public ModelChatMessage CurrentModelChatMessage
        {
            get { return _currentModelChatMessage; }
            set
            {
                if (_currentModelChatMessage == value) return;

                _currentModelChatMessage = value;

                OnPropertyChanged("CurrentModelChatMessage");
            }
        }

        /// <summary> Текущий лист цитированых сообщений </summary>
        private List<ModelChatMessage> _currentListQuotedMessage = new List<ModelChatMessage>();

        /// <summary> Текущий лист старых сообщений </summary>
        private List<ModelChatMessage> _currentListOldMessage = new List<ModelChatMessage>();

        /// <summary> Cписок сообщений </summary>
        public List<ModelChatMessage> CurrentListOldMessage
        {
            get { return _currentListOldMessage; }
            set
            {
                if (_currentListOldMessage == value) return;

                _currentListOldMessage = value;

                OnPropertyChanged("CurrentListOldMessage");
            }
        }

        ///// <summary> Сообщение для цитирования </summary>
        //private ModelChatMessage _currentModelChatMessageForAnswer = null;

        ///// <summary> Сообщение для цитирования </summary>
        //public ModelChatMessage CurrentModelChatMessageForAnswer
        //{
        //    get { return _currentModelChatMessageForAnswer; }
        //    set
        //    {
        //        if (_currentModelChatMessageForAnswer == value) return;

        //        _currentModelChatMessageForAnswer = value;

        //        OnPropertyChanged("CurrentModelChatMessageForAnswer");
        //        OnPropertyChanged("HasCurrentModelChatMessageForAnswer");
        //    }
        //}

        ///// <summary> Наличие сообщения для цитирования </summary>
        //public bool HasCurrentModelChatMessageForAnswer => CurrentModelChatMessageForAnswer.Id != null;

        /// <summary> Текущий чат </summary>
        public ModelChat CurrentModelChat { get; set; }

        /// <summary> Список сообщений </summary>
        private List<ModelChatMessage> _listModelChatMessage;

        /// <summary> Cписок сообщений </summary>
        public List<ModelChatMessage> ListModelChatMessage
        {
            get { return _listModelChatMessage; }
            set
            {
                if (_listModelChatMessage == value) return;

                _listModelChatMessage = value;

                OnPropertyChanged("ListModelChatMessage");
                OnPropertyChanged("ListModelConctactChatMembers");
            }
        }

        /// <summary> Cписок собеседников, которые проявляли активность в чате </summary>
        //private List<ModelContact> _listModelContactChatMembers = new List<ModelContact>();
        public List<ModelContact> ListModelConctactChatMembers
        {
            get
            {
                var result = new List<ModelContact>();

                foreach (var i in ListModelChatMessage)
                {
                    var modelContact = result.FirstOrDefault(obj => obj?.DodicallId == i?.Sender?.DodicallId);
                    if (modelContact == null)
                    {
                        result.Add(i.Sender);
                    }
                }

                return result;
            }
        }

        /// <summary> Команда отправки сообщения </summary>
        public Command CommandSendModelMessage { get; set; }

        /// <summary> Команда переименования чата </summary>
        public Command CommandRenameChatModel { get; set; }

        /// <summary> Команда удаления из чата </summary>
        public Command CommandRemoveFromChat { get; set; }

        ///// <summary> Команда удаления из чата </summary>
        //public Command CommandNeedGet { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelChatDetail(ModelChat modelChat)
        {
            CurrentModelChat = modelChat;

            InitializatiOnoverallConstructors(null);
        }

        /// <summary> Конструктор </summary>
        public ViewModelChatDetail(ModelContact modelContact)
        {
            var modelChat = DataSourceChat.GetModelChatByModelContact(modelContact);

            InitializatiOnoverallConstructors(modelChat);
        }

        /// <summary> Конструктор </summary>
        public ViewModelChatDetail(List<ModelContact> listModelContact)
        {
            var modelChat = DataSourceChat.GetModelChatByListModelContact(listModelContact);

            InitializatiOnoverallConstructors(modelChat);
        }

        /// <summary> Инициализация конструкторов </summary>
        private void InitializatiOnoverallConstructors(ModelChat modelChat)
        {
            if (modelChat != null)
            {
                var modelChatFromList = ViewModelChat.CurrentViewModelChat.CurrentListModelChat.FirstOrDefault(obj => obj.Id == modelChat.Id);

                if (modelChatFromList == null)
                {
                    ViewModelChat.CurrentViewModelChat.CurrentListModelChat.Add(modelChat);
                    CurrentModelChat = modelChat;
                }
                else
                {
                    CurrentModelChat = modelChatFromList;
                }
            }
            
            _listModelChatMessage = DataSourceChat.GetPagedModelChatMessageByIdChat(CurrentModelChat);

            foreach (var i in ListModelChatMessage)
            {
                var modelContact = ListModelConctactChatMembers.FirstOrDefault(obj => obj?.DodicallId == i?.Sender?.DodicallId);
                if (modelContact == null)
                {
                    ListModelConctactChatMembers.Add(i.Sender);
                }
            }

            foreach (var i in _listModelChatMessage)
            {
                // Сделать отдельный список с собеседниками+теми кто когда-то был в чате
                // Брать ссылку на объект из этого списка, тогда все будет работать
                //var modelContactFromModelChat = CurrentModelChat.ListModelContact.FirstOrDefault(obj => obj.Id == i?.Sender?.Id);
                var modelContactFromChatMessageSenders = ListModelConctactChatMembers.FirstOrDefault(obj => obj?.DodicallId == i?.Sender?.DodicallId);
                if (modelContactFromChatMessageSenders != null)
                {
                    i.Sender = modelContactFromChatMessageSenders;
                }
                //if (modelContactFromModelChat != null) i.Sender = modelContactFromModelChat;
            }

            SortCurrentListModelChatMessage();

            if (!CurrentModelChat.IsP2P)
            {
                CurrentModelChat.ListModelContact = ListModelContactSort(CurrentModelChat.ListModelContact);
                //_dataSourceContact.RefreshModelContactStatus(_listModelChatMessage.Select(obj => obj?.Sender).ToList());
                DataSourceContact.RefreshModelContactStatus(GetAllSendersAndUserFromChat());
                //_dataSourceContact.RefreshModelContactStatus(CurrentModelChat.ListModelContact);
            }

            CommandSendModelMessage = new Command(obj => SendModelMessage());

            CommandRenameChatModel = new Command(obj => RenameChatModel());

            CommandRemoveFromChat = new Command(obj => RemoveFromChatContactModel((ModelContact)obj));

            //CommandInviteAndRevokeChatMembers = new Command(obj => InviteAndRevokeChatMembers());
            
            if (CurrentModelChat.HaveDraft) CurrentModelChatMessage = CurrentModelChat.DraftMessage;

            CallbackRouter.Instance.ListModelChatMessageChanged += OnListModelChatMessageChanged;
            CallbackRouter.Instance.ListModelContactStatusChanged += OnListModelContactStatusChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged += OnListModelContactSubscriptionChanged;
            CallbackRouter.Instance.ModelUserSettingsChanged += OnModelUserSettingsChanged;
        }



        /// <summary> Обработчик изменения сообщений чата </summary>
        private void OnListModelChatMessageChanged(object sender, List<ModelChatMessage> listModelChatMessages)
        {
            var listSingleModelChatMessages = new List<ModelChatMessage>();

            listSingleModelChatMessages = listModelChatMessages.Where(obj => !listSingleModelChatMessages.Any(message => message.Id == obj.Id)).ToList();

            var listExistMessage = listSingleModelChatMessages.Where(obj => obj.IdChat == CurrentModelChat.Id && ListModelChatMessage.Any(message => message.Id == obj.Id)).ToList();

            if (listExistMessage.Count > 0)
            {
                Action action = () => RefreshExistingModelChatMessage(listExistMessage);

                CurrentDispatcher.BeginInvoke(action);
            }

            var listNewMessage = listSingleModelChatMessages.Where(obj => obj.IdChat == CurrentModelChat.Id && !ListModelChatMessage.Any(message => message.Id == obj.Id)).ToList();

            if (listNewMessage.Count > 0)
            {
                Action action = () => AddNewModelChatMessage(listNewMessage);

                CurrentDispatcher.BeginInvoke(action);
            }

            OnPropertyChanged("ListModelChatMessage");
        }

        /// <summary> Обработчик изменения статусов контактов </summary>
        private void OnListModelContactStatusChanged(object sender, List<PackageModelContactStatus> listPackageModelContactStatuses)
        {
            var listModelContact = GetAllSendersAndUserFromChat();

            foreach (var i in listModelContact.Where(obj => listPackageModelContactStatuses.Any(o => o.XmppId == obj.XmppId)))
            {
                var packageModelContactStatus = listPackageModelContactStatuses.First(obj => obj.XmppId == i.XmppId);

                i.ModelEnumUserBaseStatusObj = packageModelContactStatus.ModelEnumUserBaseStatusObj;
                i.UserExtendedStatus = packageModelContactStatus.UserExtendedStatus;
            }
        }

        /// <summary> Обработчик изменения состояния подписок контактов </summary>
        private void OnListModelContactSubscriptionChanged(object sender, List<PackageModelContactSubscription> listPackageModelContactSubscriptions)
        {
            ChangedListModelContactSubscription(listPackageModelContactSubscriptions);
        }

        /// <summary> Обработчик изменения настроек приложения </summary>
        private void OnModelUserSettingsChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("FontSize");
        }

        ///// <summary> Обработчик изменения модели внутри логики C++ </summary>
        //public void DoCallback(object sender, DoCallbackArgs e)
        //{
        //if (e.ModelName == "ChatMessages")
        //{
        //    var listIdExisMessage = e.EntityIds.Where(obj => ListModelChatMessage.Any(message => message.Id == obj)).ToArray();

        //    if (listIdExisMessage.Length > 0)
        //    {
        //        Action action = () => RefreshExistingModelChatMessage(listIdExisMessage);

        //        CurrentDispatcher.BeginInvoke(action);
        //    }

        //    var listIdNewMessage = e.EntityIds.Where(obj => !ListModelChatMessage.Any(message => message.Id == obj)).ToArray();

        //    if (listIdNewMessage.Length > 0)
        //    {
        //        Action action = () => AddNewModelChatMessage(listIdNewMessage);

        //        CurrentDispatcher.BeginInvoke(action);
        //    }
        //    OnPropertyChanged("ListModelChatMessage");
        //}

        //if (e.ModelName == "ContactsPresence")
        //{
        //    Action action = () => DataSourceContact.RefreshModelContactStatus(GetAllSendersAndUserFromChat());

        //    CurrentDispatcher.BeginInvoke(action);
        //}

        //if (e.ModelName == "ContactSubscriptions")
        //{
        //    ChangedListModelContactSubscription(e.EntityIds);
        //    //OnPropertyChanged("ListModelConctactChatMembers");
        //}

        //if (e.ModelName == "UserSettingsChanged")
        //{
        //    OnPropertyChanged("FontSize");
        //}

        //RefreshListModelContactStatus();
        //}

        //после удаления и редактирования сообщения на другом устройстве колбеки долбят постоянно
        //при удалении воткнут кастыль, при редактировании нельзя воткнуть т.к. сообщение может быть еще раз отредактированно
        /// <summary> Обновление существующих сообщений </summary>
        private void RefreshExistingModelChatMessage(List<ModelChatMessage> listModelChatMessages)
        {
            var listEditModelChatMessage = new List<ModelChatMessage>();

            var listDeleteModelChatMessage = new List<ModelChatMessage>();

            var listServeredModelChatMessage = new List<ModelChatMessage>();

            foreach (var modelChatMessage in listModelChatMessages)
            {
                //ListModelChatMessage

                //var isRefreshMessage = true;

                if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 0 && modelChatMessage.Changed)
                {
                    listEditModelChatMessage.Add(modelChatMessage);

                    //isRefreshMessage = false;
                }

                if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 5)
                {
                    if (_listModelChatMessage.First(obj => obj.Id == modelChatMessage.Id).ModelEnumChatMessageTypeObj.Code != 5)
                    {
                        listDeleteModelChatMessage.Add(modelChatMessage);

                        //isRefreshMessage = false;
                    }
                }

                var oldMessage = ListModelChatMessage.FirstOrDefault(obj => obj.Id == modelChatMessage.Id);

                if (oldMessage.Servered != modelChatMessage.Servered) listServeredModelChatMessage.Add(modelChatMessage);
            }

            if (listEditModelChatMessage.Count > 0) OnEventViewModel("EditMessage", listEditModelChatMessage);

            if (listDeleteModelChatMessage.Count > 0) OnEventViewModel("DeleteMessage", listDeleteModelChatMessage);

            if (listServeredModelChatMessage.Count > 0) OnEventViewModel("ServeredMessage", listServeredModelChatMessage);
        }

        /// <summary> Обработчик изменения подписки контактов внутри логики C++ </summary>
        private void ChangedListModelContactSubscription(List<PackageModelContactSubscription> listPackageModelContactSubscriptions)
        {
            if (ListModelConctactChatMembers == null) return;

            //var dictionary = _dataSourceContact.GetDictionaryModelContactSubscriptionByArrayXmppId(arrayXmppId);

            foreach (var i in listPackageModelContactSubscriptions)
            {
                var modelContact = ListModelConctactChatMembers.FirstOrDefault(obj => obj.XmppId == i.XmppId);

                if (modelContact != null)
                {
                    Action action = () => { modelContact.ModelContactSubscriptionObj = i.ModelContactSubscriptionObj.GetDeepCopy(); };

                    CurrentDispatcher.BeginInvoke(action);

                    //modelContact.ModelContactSubscriptionObj = i.Value;
                }
            }

            DataSourceContact.RefreshModelContactStatus(ListModelConctactChatMembers.Where(obj => listPackageModelContactSubscriptions.Any(o => o.XmppId == obj.XmppId)).ToList());
        }

        /// <summary> Добавление новых сообщений </summary>
        private void AddNewModelChatMessage(List<ModelChatMessage> listModelChatMessages)
        {

            //var listNewModelChatMessage = DataSourceChat.GetModelChatMessageById(entityIds).Where(obj => obj.IdChat == CurrentModelChat.Id).ToList();
            //нужно вернуть TSAPOV
            //foreach (var i in listNewModelChatMessage)
            //{
            //    var modelContact = CurrentModelChat.ListModelContact.FirstOrDefault(obj => obj.Id == i.Sender.Id);

            //    if (modelContact != null) i.Sender = modelContact;
            //}

            //var listNewModelChatMessage = DataSourceChat.GetModelChatMessageById(entityIds).Where(obj => obj.IdChat == CurrentModelChat.Id).ToList();

            foreach (var i in listModelChatMessages)
            {
                var modelContact = CurrentModelChat.ListModelContact.FirstOrDefault(obj => obj.DodicallId == i?.Sender?.DodicallId);

                if (modelContact != null) i.Sender = modelContact;
            }

            if (!CurrentModelChat.IsP2P)
            {
                //_dataSourceContact.RefreshModelContactStatus(listNewModelChatMessage.Select(obj => obj?.Sender).ToList());
                //_dataSourceContact.RefreshModelContactStatus(CurrentModelChat.ListModelContact);
                DataSourceContact.RefreshModelContactStatus(GetAllSendersAndUserFromChat());
            }

            ListModelChatMessage.AddRange(listModelChatMessages);

            OnEventViewModel("NewMessage", listModelChatMessages);

            //MarkReadModelMessage(); // СВ сказал что при любой активности в чате все сообщения считаем прочитанными
        }

        /// <summary> Сортировка сообщений внутри чата </summary>
        private void SortCurrentListModelChatMessage()
        {
            //ListModelChatMessage.Sort((modelMessage1, modelMessage2) => modelMessage1.SendTime.CompareTo(modelMessage2.SendTime));
            ListModelChatMessage.Sort((modelMessage1, modelMessage2) => modelMessage1.Rownum.CompareTo(modelMessage2.Rownum));
        }

        /// <summary> Метод отправки сообщения </summary>
        private void SendModelMessage()
        {
            CurrentModelChatMessage.Sender = DataSourceContact.ModelContactIam;
            CurrentModelChatMessage.SendTime = DateTime.Now;
            CurrentModelChatMessage.ModelEnumChatMessageTypeObj = ModelEnumChatMessageType.GetModelEnum(0); // простое тесктовое сообщение
            CurrentModelChatMessage.IdChat = CurrentModelChat.Id;

            if(CurrentModelChat.IsP2P)
            {
                CurrentModelChatMessage.ModelEnumChatMessageSecurityLevelObj = ModelEnumChatMessageSecurityLevel.GetModelEnum(1); //сообщения зашифрованые
            }
            else
            {
                CurrentModelChatMessage.ModelEnumChatMessageSecurityLevelObj = ModelEnumChatMessageSecurityLevel.GetModelEnum(0); //сообщения не зашифрованые
            }
            
            if (_currentListQuotedMessage.Count != 0)
            {
                if (_currentListQuotedMessage.Count == 1 && JustAnswerCurrentMessage && String.IsNullOrWhiteSpace(_currentListQuotedMessage.First().StringContent))
                    JustAnswerCurrentMessage = false;

                DataSourceChat.QuoteAndSendMessages(CurrentModelChatMessage, _currentListQuotedMessage, JustAnswerCurrentMessage);
                
                _currentListQuotedMessage.Clear();
                CurrentModelChatMessage = new ModelChatMessage();
            }
            else
            {
                if (DataSourceChat.SendModelChatMessage(CurrentModelChatMessage))
                {
                    OnEventViewModel("SendModelMessage", CurrentModelChatMessage);

                    ListModelChatMessage.Add(CurrentModelChatMessage);
                    
                    CurrentModelChatMessage = new ModelChatMessage();
                }
            }

            if (CurrentModelChat.DraftMessage != null) CurrentModelChat.DraftMessage = null;
        }

        /// <summary> Метод переименования чата </summary>
        private void RenameChatModel()
        {
            /*CurrentModelChatMessage.Sender = ModelContact.ModelContactIam;
            CurrentModelChatMessage.SendTime = DateTime.Now;
            CurrentModelChatMessage.ModelEnumChatMessageTypeObj = ModelEnumChatMessageType.GetModelEnumChatMessageType(1); // сообщение о переименовании
            CurrentModelChatMessage.IdChat = CurrentModelChat.Id;*/
            if (DataSourceChat.RenameModelChat(CurrentModelChat))
            {
                MarkReadModelMessage(); // СВ сказал что при любой активности в чате все сообщения считаем прочитанными
            }
        }

        /// <summary> Метод удаления собеседника из чата </summary>
        private void RemoveFromChatContactModel(ModelContact mc)
        {
            if (DataSourceChat.InviteAndRevokeChatMembers(CurrentModelChat, new List<ModelContact>(), new List<ModelContact> { mc }))
            {
                /*
                if (CurrentModelChat.ListModelContactNotMe.Count == 1)
                {
                    CurrentModelChat.Title = LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_Untitled");
                }*/
                MarkReadModelMessage();
            }
        }



        /// <summary> Метод добавления собеседников в чат </summary>
        public void InviteAndRevokeChatMembers(List<ModelContact> membersToInvite, List<ModelContact> membersToRevoke)
        {
            if (DataSourceChat.InviteAndRevokeChatMembers(CurrentModelChat, membersToInvite, membersToRevoke))
            {
                MarkReadModelMessage();
            }
        }

        /// <summary> Метод прочтения сообщений </summary>
        public void MarkReadModelMessage()
        {
            if (ListModelChatMessage.Count == 0) return;

            if (DataSourceChat.MarkReadModelChatMessage(ListModelChatMessage.Last()))
            {
                foreach (var i in ListModelChatMessage)
                {
                    i.Readed = true;
                }

                if (CurrentModelChat.LastMessage != null) CurrentModelChat.LastMessage.Readed = true;

                //кастыль, т.к.пока СВ сказал что мы всегда помечаем все сообщения как прочитанные
                CurrentModelChat.NewMessagesCount = 0;
            }
        }

        /// <summary> Сортировка контактов </summary>
        private List<ModelContact> ListModelContactSort(List<ModelContact> listModelContact)
        {
            listModelContact.Sort((modelContact1, modelContact2) => CompareDependLocalization(modelContact1.FullName, modelContact2.FullName));

            // тут сделать сортировку в зависимости от языка + добавить в обработчик изменения языка

            return listModelContact;
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

        /// <summary> Получение всего списка контактов, когда либо присутствовавших в чате </summary>
        public List<ModelContact> GetAllSendersAndUserFromChat()
        {
            var contactList = CurrentModelChat.ListModelContact;
            contactList = contactList.Concat(_listModelChatMessage.Select(obj => obj?.Sender).ToList()).ToList();
            return contactList;
        }


        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }

        /// <summary> Обновить статусы собеседников </summary>
        public void RefreshListModelContactStatus()
        {
            DataSourceContact.RefreshModelContactStatus(GetAllSendersAndUserFromChat());
            //_dataSourceContact.RefreshModelContactStatus(CurrentModelChat.ListModelContact);
        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            if (!String.IsNullOrWhiteSpace(CurrentModelChatMessage.StringContent) || _currentListQuotedMessage.Count > 0)
            {
                CurrentModelChatMessage.Sender = DataSourceContact.ModelContactIam;
                CurrentModelChatMessage.SendTime = DateTime.Now;
                CurrentModelChatMessage.ModelEnumChatMessageTypeObj = ModelEnumChatMessageType.GetModelEnum(7);
                CurrentModelChatMessage.IdChat = CurrentModelChat.Id;
                if (_currentListQuotedMessage.Count > 0)
                    CurrentModelChatMessage.ListQuotedModelChatMessage = new ObservableCollection<ModelChatMessage>(_currentListQuotedMessage);

                if(DataSourceChat.SaveDraftModelChatMessage(CurrentModelChatMessage))
                {
                    CurrentModelChat.DraftMessage = CurrentModelChatMessage;
                }
            }
            else
            {
                if (CurrentModelChat.DraftMessage != null)
                {
                    DataSourceChat.SaveDraftModelChatMessage(CurrentModelChatMessage);
                    CurrentModelChat.DraftMessage = null;
                }
                    
            }
            
            CallbackRouter.Instance.ListModelChatMessageChanged -= OnListModelChatMessageChanged;
            CallbackRouter.Instance.ListModelContactStatusChanged -= OnListModelContactStatusChanged;
            CallbackRouter.Instance.ListModelContactSubscriptionChanged -= OnListModelContactSubscriptionChanged;
            CallbackRouter.Instance.ModelUserSettingsChanged -= OnModelUserSettingsChanged;
        }

        /// <summary> Удалить сообщение </summary>
        public void DeleteModelChatMessage(ModelChatMessage modelChatMessage)
        {
            if (modelChatMessage.Sender.Iam)
            {
                if (DataSourceChat.DeleteModelChatMessage(modelChatMessage))
                {
                    modelChatMessage.ModelEnumChatMessageTypeObj = ModelEnumChatMessageType.GetModelEnum(5);
                }
            }
            else
            {
                modelChatMessage.ModelEnumChatMessageTypeObj = ModelEnumChatMessageType.GetModelEnum(5);
            }

            if (modelChatMessage.ListQuotedModelChatMessage != null) modelChatMessage.ListQuotedModelChatMessage.Clear();
        }

        /// <summary> Разрешение редактировать сообщение </summary>
        public bool CanEditMessage(ModelChatMessage modelChatMessage)
        {
            return DataSourceChat.CanEditMessage(modelChatMessage);
        }

        /// <summary> Редактировать сообщение </summary>
        public bool EditMessage(ModelChatMessage modelChatMessage, string text)
        {
            var result = true;

            modelChatMessage.StringContent = text;

            modelChatMessage.Changed = true;

            if (modelChatMessage.Sender.Iam)
            {
                result = DataSourceChat.EditMessage(modelChatMessage, text);
            }

            return result;
        }

        /// <summary> Редактировать сообщение с ответом </summary>
        public bool EditMessage(ModelChatMessage modelChatMessage, string text, bool shouldDeleteQuote)
        {
            var result = true;

            if (shouldDeleteQuote)
            {
                modelChatMessage.ListQuotedModelChatMessage.Clear();
            }

            modelChatMessage.StringContent = text;

            modelChatMessage.Changed = true;

            if (modelChatMessage.Sender.Iam)
            {
                result = DataSourceChat.EditMessage(modelChatMessage, text, shouldDeleteQuote);
            }

            return result;
        }

        /// <summary> Отправить контакты </summary>
        public void SendContactListToChat(List<ModelContact> listModelContact)
        {
            if (listModelContact == null) return;

            if (DataSourceChat.SendListModelContactToChat(listModelContact, CurrentModelChat))
            {
                MarkReadModelMessage();
            }
        }

        /// <summary> Добавить сообщение к цитированым </summary>
        public void AddToQuotedMessage(List<ModelChatMessage> listModelChatMessage)
        {
            foreach (var modelChatMessage in listModelChatMessage)
            {
                _currentListQuotedMessage.Add(modelChatMessage);
            }

            JustAnswerCurrentMessage = false;
        }

        /// <summary> Добавить сообщение к цитированым </summary>
        public void AddToQuotedMessage(ModelChatMessage modelChatMessage)
        {
            _currentListQuotedMessage.Add(modelChatMessage);

            JustAnswerCurrentMessage = true;
        }

        /// <summary> Удалить сообщение из цитированых </summary>
        public void RemoveQuotedMessage()
        {
            _currentListQuotedMessage.Clear();

            if (CurrentModelChat.HaveDraft) CurrentModelChat.DraftMessage.ListQuotedModelChatMessage.Clear();
        }

        /// <summary> Получить еще старых сообщений </summary>
        public bool GetMoreOldMessage()
        {
            var lastMessage = ListModelChatMessage.First();

            var listOldMessage = DataSourceChat.GetPagedOldModelChatMessage(lastMessage);

            if (listOldMessage.Count == 0) return false;

            CurrentListOldMessage = listOldMessage;

            ListModelChatMessage.InsertRange(0, CurrentListOldMessage);

            return true;
        }
    }
}
