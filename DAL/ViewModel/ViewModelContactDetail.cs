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
using DAL.WrapperBridge;
using System.Diagnostics;

namespace DAL.ViewModel
{
    public class ViewModelContactDetail : AbstractNotifyPropertyChanged
    {
        ///<summary> Событие успешного прочтения приложения </summary>
        public event EventHandler InviteReadSuccessful;

        ///<summary> Событие успешной добавления/удаления в белый список </summary>
        public event EventHandler WhiteChanged;

        ///<summary> Событие успешной блокировки/разблокировки </summary>
        public event EventHandler BlockedChanged;

        ///<summary> Событие успешного принятия или отклонения приглашения </summary>
        public event EventHandler<bool> ApplyDenyInviteSuccessful;

        ///<summary> Событие успешной отправки заявки в друзья </summary>
        public event EventHandler SendRequestSuccessful;

        ///<summary> Событие успешного удаления контакта </summary>
        public event EventHandler<ModelContact> DeleteModelContactSuccessful;

        /// <summary> Текущий ModelContact </summary>
        public ModelContact CurrentModelContact { get; set; }

        ///<summary> Команда удаления контакта </summary>
        public Command CommandDelete { get; set; }

        ///<summary> Команда принятия приглашения </summary>
        public Command CommandApplyInvite { get; set; }

        ///<summary> Команда отклонения приглашения </summary>
        public Command CommandDenyInvite { get; set; }

        ///<summary> Команда отправки запроса контакту </summary>
        public Command CommandSendRequest { get; set; }

        ///<summary> Команда блокировки контакта </summary>
        public Command CommandBlock { get; set; }

        ///<summary> Команда разблокировки контакта </summary>
        public Command CommandUnblock { get; set; }

        ///<summary> Команда добавление контакта в белые </summary>
        public Command CommandAddToWhite { get; set; }

        ///<summary> Команда удаление контакта в белые </summary>
        public Command CommandDeleteFromWhite { get; set; }

        ///<summary> Флаг отображения карточки для моих контактов </summary>
        private bool _showContactDetailCard;

        ///<summary> Флаг отображения карточки для моих контактов </summary>
        public bool ShowContactDetailCard
        {
            get { return _showContactDetailCard; }
            set
            {
                if (_showContactDetailCard == value) return;
                _showContactDetailCard = value;

                if (value)
                {
                    ShowContactRequestCard = false;
                    ShowContactInviteCard = false;
                }
                //Не знаю есть ли возможность вызвать OnPropertyChanged для всех зависимых свойств, по этому пишу вручную
                OnPropertyChanged("ShowContactDetailCard");
                OnPropertyChanged("ShowStackPanelServices");
                OnPropertyChanged("ShowImageMore");
                OnPropertyChanged("ShowRequestInviteDenyGrid");
            }
        }

        ///<summary> Флаг отображения карточки отправки запроса контакту(открывается в поиске по директории) </summary>
        private bool _showContactRequestCard;

        ///<summary> Флаг отображения карточки отправки запроса контакту(открывается в поиске по директории) </summary>
        public bool ShowContactRequestCard
        {
            get { return _showContactRequestCard; }
            set
            {
                if (_showContactRequestCard == value) return;
                _showContactRequestCard = value;

                if (value)
                {
                    ShowContactDetailCard = false;
                    ShowContactInviteCard = false;
                }
                //Не знаю есть ли возможность вызвать Onpropertychanged для всех зависимых свойств, по этому пишу вручную
                OnPropertyChanged("ShowContactRequestCard");
                OnPropertyChanged("ShowButtonSendRequest");
                OnPropertyChanged("ShowStackPanelApplyDenyInvite");
                OnPropertyChanged("ShowRequestInviteDenyGrid");
            }
        }

        ///<summary> Флаг отображения карточки принятия/отказа от запроса контакта(открывается в запросах и приглашениях) </summary>
        private bool _showContactInviteCard;

        ///<summary> Флаг отображения карточки принятия/отказа от запроса контакта(открывается в запросах и приглашениях) </summary>
        public bool ShowContactInviteCard
        {
            get { return _showContactInviteCard; }
            set
            {
                if (_showContactInviteCard == value) return;
                _showContactInviteCard = value;

                if (value)
                {
                    ShowContactDetailCard = false;
                    ShowContactRequestCard = false;
                }
                //Не знаю есть ли возможность вызвать Onpropertychanged для всех зависимых свойств, по этому пишу вручную
                OnPropertyChanged("ShowStackPanelApplyDenyInvite");
                OnPropertyChanged("ShowContactInviteCard");
                OnPropertyChanged("ShowRequestInviteDenyGrid");
            }
        }

        ///<summary> Флаг отображения TextBlock-a "Ваш запрос отправлен" </summary>
        public bool ShowSendRequestTextBlock => (CurrentModelContact.ModelContactSubscriptionObj == null ? false : CurrentModelContact.ModelContactSubscriptionObj.Ask) && !CurrentModelContact.Blocked;

        ///<summary> Флаг приглашения </summary>
        public bool IsInvite => (CurrentModelContact.ModelContactSubscriptionObj == null ? false : CurrentModelContact.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 2) && CurrentModelContact.Id == 0;

        ///<summary> Флаг отображения панели с кнопками звонка чата видео </summary>
        public bool ShowStackPanelServices => ShowContactDetailCard && CurrentModelContact.IsAccessStatus && !CurrentModelContact.Blocked;

        ///<summary> Флаг отображения статуса </summary>
        public bool ShowGridUserStatus => CurrentModelContact.IsAccessStatus;

        ///<summary> Флаг отображения панели блокировки </summary>
        public bool ShowBlockPanel => CurrentModelContact.Blocked;

        ///<summary> Флаг отображения панели принятия отклонения запроса </summary>
        public bool ShowStackPanelApplyDenyInvite => ShowContactInviteCard && CurrentModelContact.Id == 0;

        ///<summary> Флаг отображения кнопки ImageMore </summary>
        public bool ShowImageMore => ShowContactDetailCard;

        ///<summary> Флаг отображения кнопки ImageServicesMore </summary>
        public bool ShowImageServicesMore => CurrentModelContact.IsDodicall;

        ///<summary> Флаг отображения кнопки меню "Заблокировать" </summary>
        public bool ShowMenuItemBlock => !CurrentModelContact.Blocked;

        ///<summary> Флаг отображения кнопки меню "Разблокировать" </summary>
        public bool ShowMenuItemUnblock => CurrentModelContact.Blocked;

        ///<summary> Флаг отображения кнопки меню "Добавить в белые" </summary>
        public bool ShowMenuItemAddToWhite => !CurrentModelContact.White;

        ///<summary> Флаг отображения кнопки меню "Удалить из белых" </summary>
        public bool MenuItemDeleteFromWhite => CurrentModelContact.White;

        ///<summary> Флаг отображения кнопки меню добавления в контакты </summary>
        public bool ShowButtonSendRequest => CurrentModelContact.Id == 0 && ShowContactRequestCard && CurrentModelContact.IsDodicall;

        ///<summary> Флаг отображения кнопки "Назад" для создания карточки из запросов и приглашений </summary>  
        public bool ShowButtonReturn { get; set; }

        ///<summary> Флаг отображения кнопки "Назад к результатам поиска" для создания карточки из директории </summary>  
        public bool ShowButtonBackToSerachResults { get; set; }

        ///<summary> Флаг отображения label-a номеров </summary>
        public bool ShowLabelContact => CurrentModelContact.ListModelUserContact == null ? false : CurrentModelContact.ListModelUserContact.Count != 0;

        ///<summary> Флаг отображения label-a дополнительных номеров </summary>
        public bool ShowLabelExtraContact => CurrentModelContact.ListModelUserContactExtra == null ? false : CurrentModelContact.ListModelUserContactExtra.Count != 0;

        ///<summary> Флаг отображения Grid-a с элементами для обработки принятия отправки отмены запроса </summary>
        public bool ShowRequestInviteDenyGrid => ShowContactRequestCard || ShowContactInviteCard;

        /// <summary> Событие propertychanged текущего контакта </summary>
        private void CurrentModelContact_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName.ToLower();

            if (propertyName == "id" || propertyName == "blocked")
            {
                OnPropertyChanged("ShowButtonSendRequest");
                OnPropertyChanged("ShowBlockPanel");
                OnPropertyChanged("ShowStackPanelServices");
                OnPropertyChanged("ShowMenuItemBlock"); 
                OnPropertyChanged("ShowSendRequestTextBlock");
                OnPropertyChanged("ShowStackPanelApplyDenyInvite");  
                OnPropertyChanged("ShowMenuItemUnblock");
                OnPropertyChanged("ShowSendRequestTextBlock"); 
            }

            if (propertyName == "modelcontactsubscriptionobj" || propertyName == "modelenumuserbasestatusobj")
            {
                OnPropertyChanged("ShowSendRequestTextBlock");
                OnPropertyChanged("ShowGridUserStatus");
                OnPropertyChanged("ShowStackPanelServices");
            }

            if (propertyName == "white")
            {
                OnPropertyChanged("MenuItemDeleteFromWhite");
                OnPropertyChanged("ShowMenuItemAddToWhite");
            }
        }

        /// <summary> Конструктор </summary>
        public ViewModelContactDetail(ModelContact modelContact)
        {
            CurrentModelContact = modelContact;

            CommandDelete = new Command(obj =>
            {
                if (DataSourceContact.DeleteModelContact(CurrentModelContact)) OnDeleteModelContactSuccessful();
            });

            CommandApplyInvite = new Command(obj => ApplyInvite());

            CommandDenyInvite = new Command(obj => DenyInvite());

            CommandAddToWhite = new Command(obj => { AddToWhite(); OnWhiteChanged(); });

            CommandDeleteFromWhite = new Command(obj => { DeleteFromWhite(); OnWhiteChanged(); });

            CommandSendRequest = new Command(obj => SendRequest());

            CommandBlock = new Command(obj => { DataSourceContact.BlockModelContact(CurrentModelContact); OnBlockedChanged(); });

            CommandUnblock = new Command(obj => { DataSourceContact.UnblockModelContact(CurrentModelContact); OnBlockedChanged(); });

            CurrentModelContact.PropertyChanged += CurrentModelContact_PropertyChanged;
        }

        ///<summary> Метод принятия приглашения </summary>
        public void ApplyInvite()
        {
            if (CurrentModelContact == null) return;

            if (DataSourceContact.ApplyInviteModelContact(CurrentModelContact))
            {
                OnApplyDenyInviteSuccessful(true);
            }
        }

        ///<summary> Метод отклонения приглашения </summary>
        public void DenyInvite()
        {
            if (CurrentModelContact == null) return;

            if (DataSourceContact.DenyInviteModelContact(CurrentModelContact))
            {
                CurrentModelContact.Id = Int32.MaxValue; // кастыль для демо, нет времени исправлять сейчас
                OnApplyDenyInviteSuccessful(false);
            }
        }

        ///<summary> Метод отправки запроса контакту </summary>
        public void SendRequest()
        {
            if (CurrentModelContact == null) return;

            if (DataSourceContact.SendRequestModelContact(CurrentModelContact))
            {
                OnSendRequestSuccessful();
            }
        }

        ///<summary> Метод прочтения сообщения </summary>
        public void InviteRead()
        {
            if (DataSourceContact.ReadInvite(CurrentModelContact)) OnInviteReadSuccessful();
        }

        ///<summary> Метод принятия приглашения </summary>
        public void AddToWhite()
        {
            DataSourceContact.AddToWhiteModelContact(CurrentModelContact);
        }

        ///<summary> Метод удаления из белого списка </summary>
        public void DeleteFromWhite()
        {
            DataSourceContact.DeleteFromWhiteModelContact(CurrentModelContact);

            СheckСountWhiteContact();
        }

        ///<summary> Проверка количества контактов в белом списке </summary>
        private void СheckСountWhiteContact()
        {
            var countWhiteContact = DataSourceContact.GetCountWhiteContact();

            if(countWhiteContact == 0)
            {
                var CurrentModelUserSettings = DataSourceUserSettings.GetModelUserSettings();

                CurrentModelUserSettings.DoNotDesturbMode = false;

                DataSourceUserSettings.SaveModelUserSettings(CurrentModelUserSettings);
            }
        }

        ///<summary> Инвокатор события ApplyDenyInviteSuccessful </summary>
        private void OnApplyDenyInviteSuccessful(bool result)
        {
            ApplyDenyInviteSuccessful?.Invoke(this, result);
        }

        ///<summary> Инвокатор события SendRequestSuccessful </summary>
        private void OnSendRequestSuccessful()
        {
            SendRequestSuccessful?.Invoke(this, EventArgs.Empty);
        }

        ///<summary> Инвокатор события InviteReadSuccessful </summary>
        private void OnInviteReadSuccessful()
        {
            InviteReadSuccessful?.Invoke(this, EventArgs.Empty);
        }

        ///<summary> Инвокатор события DeleteModelContactSuccessful </summary>
        private void OnDeleteModelContactSuccessful()
        {
            DeleteModelContactSuccessful?.Invoke(this, CurrentModelContact);
        }

        ///<summary> Инвокатор события BlockedChanged </summary>
        private void OnBlockedChanged()
        {
            BlockedChanged?.Invoke(this, EventArgs.Empty);
        }

        ///<summary> Инвокатор события WhiteChanged </summary>
        private void OnWhiteChanged()
        {
            WhiteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
