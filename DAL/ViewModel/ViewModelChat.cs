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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using DAL.Abstract;
using DAL.Model;
using DAL.WrapperBridge;
using System.Text.RegularExpressions;
using DAL.Callback;
using DAL.Localization;

namespace DAL.ViewModel
{
    public class ViewModelChat : AbstractViewModel
    {
        /// <summary> Текущий объект ViewModelChat (кастыль, пока нет времени сделать единый глобальный механизм доспупа к списку контактов и списку чатов) </summary>
        public static ViewModelChat CurrentViewModelChat;

        /// <summary> Текущий список чатов </summary>
        private ObservableCollection<ModelChat> _currentListModelChat = new ObservableCollection<ModelChat>();

        /// <summary> Текущий список чатов </summary>
        public ObservableCollection<ModelChat> CurrentListModelChat
        {
            get { return _currentListModelChat; }
            set
            {
                if (_currentListModelChat == value) return;

                UnsubscribePropertyChanged();

                _currentListModelChat = value;

                SubscribePropertyChanged();

                OnPropertyChanged("CurrentListModelChat");

                // сделано для восстановления выделения текущего контакта после изменения списка
                // нужно для обновления из другого потока в C++ логике т.к. сами объекты могут отличаться и на всякий случай лучше вставлять объект из новой коллекции
                // мало вероятно что это будет другой объект, но на всякий случай + задел на будущее если вдруг схема поменяется
                //if (LastModelContact == null) return;
                //var modelContact = _currentListModelContact?.FirstOrDefault(obj => obj.Id == LastModelContact.Id); // обработать фильтр наложение
                //if (modelContact != null)
                //{
                //    _currentModelContact = modelContact; // св-во обновлять нельзя, т.к. если это будет один и тот же объект, то OnPropertyChanged не произойдет
                //    OnPropertyChanged("CurrentModelContact");
                //}
            }
        }

        /// <summary> Текущий чат </summary>
        public ModelChat CurrentModelChat { get; set; }

        /// <summary> Команда прочтения всех сообщений </summary>
        public Command CommandMarkReadAll { get; set; }

        /// <summary> Команда удаления чата </summary>
        public Command CommandDeleteChat { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelChat()
        {
            var listModelChat = DataSourceChat.GetListModelChat();

            CurrentListModelChat = new ObservableCollection<ModelChat>(SortCurrentListModelChat(listModelChat));

            CallbackRouter.Instance.ListModelChatChanged += OnListModelChatChanged;
            CallbackRouter.Instance.ListModelContactStatusChanged += OnListModelContactStatusChanged;
            CallbackRouter.Instance.ListModelChatMessageChanged += OnListModelChatMessageChanged;

            CommandMarkReadAll = new Command(obj => MarkReadAll());

            CommandDeleteChat = new Command(obj => DeleteChat());

            CurrentViewModelChat = this;
        }

        /// <summary> Получить чат из текушего списка чатов </summary>
        public ModelChat GetChatFromList(ModelChat modelChat)
        {
            var result = new ModelChat();
            
            var modelChatFromList = CurrentListModelChat.FirstOrDefault(obj => obj.Id == modelChat.Id);

            if (modelChatFromList != null)
            {
                result = modelChatFromList;
            }
            else
            {
                CurrentListModelChat.Add(modelChat);

                result = modelChat;
            }

            return result;
        }

        /// <summary> Получить чат из текушего списка чатов </summary>
        public ModelChat GetChatFromList(ModelContact modelContact)
        {
            var result = new ModelChat();

            var modelChat = DataSourceChat.GetModelChatByModelContact(modelContact);

            var modelChatFromList = CurrentListModelChat.FirstOrDefault(obj => obj.Id == modelChat.Id);

            if(modelChatFromList != null)
            {
                result = modelChatFromList;
            }
            else
            {
                CurrentListModelChat.Add(modelChat);

                result = modelChat;
            }

            return result;
        }

        /// <summary> Подписать все чаты в текущей коллекции </summary>
        private void SubscribePropertyChanged()
        {
            foreach (var i in _currentListModelChat)
            {
                i.PropertyChanged += Changed;
            }
        }

        /// <summary> Отписать все чаты в текущей коллекции </summary>
        private void UnsubscribePropertyChanged()
        {
            foreach (var i in _currentListModelChat)
            {
                i.PropertyChanged -= Changed;
            }
        }

        /// <summary> Обработчик изменений внутри чатов </summary>
        private void Changed(object sender, PropertyChangedEventArgs e)
        {
            // кастыль, потому что бизнес-логика не умеет посылать колбеки после прочтения сообщений и последующего изменения кол-во непрочтенных сообщений (Бред !!!)
            if (e.PropertyName == @"NewMessagesCount") OnPropertyChanged("CurrentListModelChat");
        }

        /// <summary> Сортировка списка чатов </summary>
        private List<ModelChat> SortCurrentListModelChat(List<ModelChat> listModelChat)
        {
            // решение по сортировке не айс по производительности, но нет времени переписывать пока (p.s. пох...)

            listModelChat?.Sort((modelChat1, modelChat2) => modelChat2.LastModifiedDate.CompareTo(modelChat1.LastModifiedDate));

            return listModelChat;
        }

        /// <summary> Обработчик изменения чатов </summary>
        private void OnListModelChatChanged(List<ModelChat> listChangedModelChat, List<ModelChat> listDeletedModelChat)
        {
            ChangedListModelChat(listChangedModelChat, listDeletedModelChat);
        }

        /// <summary> Обработчик изменения статусов контактов </summary>
        private void OnListModelContactStatusChanged(object sender, List<PackageModelContactStatus> packageModelContactStatuses)
        {
            Action action = () => DataSourceChat.RefreshModelContactChatStatus(CurrentListModelChat.ToList());

            CurrentDispatcher.BeginInvoke(action);
        }

        /// <summary> Обработчик изменения сообщений в чатах </summary>
        private void OnListModelChatMessageChanged(object sender, List<ModelChatMessage> modelChatMessages)
        {
            var listModelChat = DataSourceChat.GetListModelChatByIdMessage(modelChatMessages.Select(obj => obj.Id).ToArray());

            ChangedListModelChat(listModelChat, new List<ModelChat>());
        }

        ///// <summary> Обработчик изменения модели внутри логики C++ </summary>
        //public void DoCallback(object sender, DoCallbackArgs e)
        //{
            //if (e.ModelName == "Chats")
            //{
            //    Action action = () => ChangedListModelChat(e.EntityIds);

            //    CurrentDispatcher.BeginInvoke(action);
            //}

            //if (e.ModelName == "ChatMessages")
            //{
            //    Action action = () => ChangedListModelChatMessages(e.EntityIds);

            //    CurrentDispatcher.BeginInvoke(action);
            //}

            //if (e.ModelName == "ContactsPresence")
            //{
            //    CurrentDispatcher.BeginInvoke(new Action(ChangedStatusListModelContact));
            //}
        //}

        ///// <summary> Обработчик изменения списка чатов внутри логики C++ </summary>
        //private void ChangedListModelChatMessages(string[] entityIds)
        //{
        //    // вот этот код перенести в колбеки в обработку колбека "ChatMessages"

        //    var listChatIds = _dataSourceChat.GetListModelChatByIdMessage(entityIds);

        //    ChangedListModelChat(listChatIds, new List<ModelChat>());
        //}

        /// <summary> Обработчик изменения списка чатов внутри логики C++ </summary>
        private void ChangedListModelChat(List<ModelChat> listChangedModelChat, List<ModelChat> listDeletedModelChat)
        {
            var listModelChatCurrent = CurrentListModelChat.ToList();

            // удаление
            foreach (var i in listDeletedModelChat)
            {
                listModelChatCurrent.Remove(listModelChatCurrent.FirstOrDefault(obj => obj.Id == i.Id));
            }

            Action action = () =>
            {
                // добавление и обновление
                foreach (var i in listChangedModelChat)
                {
                    var modelChat = listModelChatCurrent.FirstOrDefault(obj => obj.Id == i.Id);

                    if (modelChat != null)
                    {
                        // изменение (пошел через переприсвоение свойств, а не через удалить и добавить заново, что бы не слетало выделение)
                        modelChat.ListModelContact = ListModelContactSort(i.ListModelContact);

                        modelChat.Active = i.Active;
                        modelChat.LastMessage = i.LastMessage;
                        modelChat.LastModifiedDate = i.LastModifiedDate;
                        modelChat.NewMessagesCount = i.NewMessagesCount;
                        modelChat.Title = i.Title;// LocalizationApp.GetInstance().GetValueByKey(@"ModelChatMessage_Untitled");
                        modelChat.TotalMessagesCount = i.TotalMessagesCount;
                    }
                    else
                    {
                        listModelChatCurrent.Add(i);
                    }
                }

                CurrentListModelChat = new ObservableCollection<ModelChat>(SortCurrentListModelChat(listModelChatCurrent));

                DataSourceChat.RefreshModelContactChatStatus(CurrentListModelChat.ToList());
            };

            CurrentDispatcher.BeginInvoke(action);
        }

        /// <summary> Метод прочтения всех сообщений </summary>
        private void MarkReadAll()
        {
            var listModelChat = CurrentListModelChat.Where(obj => obj.ExistNewMessages).ToArray();

            if (!listModelChat.Any()) return;

            foreach (var i in listModelChat)
            {
                if (i.LastMessage != null)
                {
                    DataSourceChat.MarkReadModelChatMessage(i.LastMessage);

                    // кастыль, т.к. из бизнес логики не приходяк колбеки об изменении чатов !!!
                    i.NewMessagesCount = 0;

                    i.LastMessage.Readed = true;
                }
            }
        }

        /// <summary> Метод удаления чата </summary>
        private void DeleteChat()
        {
            if (DataSourceChat.DeleteModelChat(CurrentModelChat))
            {
                CurrentListModelChat.Remove(CurrentModelChat);
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

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            
        }
    }
}
