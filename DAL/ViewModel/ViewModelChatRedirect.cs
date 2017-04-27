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
using DAL.Model;
using DAL.WrapperBridge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class ViewModelChatRedirect : AbstractViewModel
    {
        /// <summary> Флаг использования листа с контактами </summary>
        private bool _useContactList;

        /// <summary> Флаг использования листа с чатами </summary>
        private bool _useChatList;

        /// <summary> Список контактов </summary>
        private List<ModelContact> _listModelContact;

        /// <summary> Список чатов </summary>
        private List<ModelChat> _listModelChat;

        /// <summary> Текущий список контактов </summary>
        private ObservableCollection<ModelContact> _currentListModelContact;

        /// <summary> Текущий список чатов </summary>
        private ObservableCollection<ModelChat> _currentListModelChat;

        /// <summary> Фильтр списка контактов </summary>
        private string _searchListFilter;

        /// <summary> Фильтр списка контактов </summary>
        public string SearchListFilter
        {
            get { return _searchListFilter; }
            set
            {
                if (_searchListFilter == value) return;
                _searchListFilter = value;

                ApplyFilter();

                OnPropertyChanged("SearchListFilter");
            }
        }

        /// <summary> Текущий список чатов </summary>
        public ObservableCollection<ModelChat> CurrentListModelChat
        {
            get { return _currentListModelChat; }
            set
            {
                if (_currentListModelChat == value) return;
                _currentListModelChat = value;

                OnPropertyChanged(@"CurrentListModelChat");
            }
        }

        /// <summary> Текущий список контактов </summary>
        public ObservableCollection<ModelContact> CurrentListModelContact
        {
            get { return _currentListModelContact; }
            set
            {
                if (_currentListModelContact == value) return;
                _currentListModelContact = value;

                OnPropertyChanged(@"CurrentListModelContact");
            }
        }

        /// <summary> Флаг использования листа с контактами </summary>
        public bool UseContactList
        {
            get { return _useContactList; }
            set
            {
                if (_useContactList == value) return;
                _useContactList = value;

                if (value)
                    UseChatList = false;

                OnPropertyChanged(@"UseContactList");
            }
        }

        /// <summary> Флаг использования листа с чатами </summary>
        public bool UseChatList
        {
            get { return _useChatList; }
            set
            {
                if (_useChatList == value) return;
                _useChatList = value;

                if (value)
                    UseContactList = false;

                OnPropertyChanged(@"UseChatList");
            }
        }

        /// <summary> Комманда выбора списка контактов </summary>
        public Command CommandSelectContactList { get; set; }

        /// <summary> Комманда выбора списка чатов </summary>
        public Command CommandSelectChatList { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelChatRedirect()
        {
            CommandSelectContactList = new Command(obj => SelectContactList());

            CommandSelectChatList = new Command(obj => SelectChattList());

            UseContactList = true;

            _listModelContact = DataSourceContact.GetListModelContact().Where(obj => obj.IsDodicall && !obj.Blocked).ToList();

            CurrentListModelContact = new ObservableCollection<ModelContact>(_listModelContact);

            _listModelChat = DataSourceChat.GetListModelChat().Where(obj => !obj.IsP2P).ToList();

            CurrentListModelChat = new ObservableCollection<ModelChat>(SortCurrentListModelChat(_listModelChat));
            
        }

        /// <summary> Сортировка списка чатов </summary>
        private List<ModelChat> SortCurrentListModelChat(List<ModelChat> listModelChat)
        {
            // решение по сортировке не айс по производительности, но нет времени переписывать пока (p.s. пох...)

            listModelChat?.Sort((modelChat1, modelChat2) => modelChat2.LastModifiedDate.CompareTo(modelChat1.LastModifiedDate));

            return listModelChat;
        }

        /// <summary> Обработчик выбора контактов </summary>
        private void SelectContactList()
        {
            if (UseContactList) return;

            UseContactList = true;

            SearchListFilter = string.Empty;
        }

        /// <summary> Обработчик выбора чатов </summary>
        private void SelectChattList()
        {
            if (UseChatList) return;

            UseChatList = true;

            SearchListFilter = string.Empty;
        }

        /// <summary> Применить фильтр </summary>
        private void ApplyFilter()
        {
            var filter = SearchListFilter.Trim().ToLower();

            if (UseContactList)
            {
                if (String.IsNullOrWhiteSpace(SearchListFilter))
                {
                    CurrentListModelContact = new ObservableCollection<ModelContact>(_listModelContact);

                    return;
                }
                
                var result = _listModelContact.Where(obj => obj.FullName.ToLower().Contains(filter) ||
                                             obj.ListModelUserContact.Any(c => c.Identity.ToLower().Contains(filter)) ||
                                             obj.ListModelUserContactExtra.Any(c => c.Identity.ToLower().Contains(filter))
                                     ).ToList();

                CurrentListModelContact = new ObservableCollection<ModelContact>(result);
            }

            if(UseChatList)
            {
                if (String.IsNullOrWhiteSpace(SearchListFilter))
                {
                    CurrentListModelChat = new ObservableCollection<ModelChat>(_listModelChat);

                    return;
                }

                var result = _listModelChat.Where(obj => obj.Title.ToLower().Contains(filter));

                CurrentListModelChat = new ObservableCollection<ModelChat>(result);
            }
        }
        
        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            
        }
    }
}
