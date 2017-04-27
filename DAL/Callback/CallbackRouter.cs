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
using System.Diagnostics;
using System.Linq;
using System.Security;
using dodicall;
using DAL.Model;
using DAL.WrapperBridge;

namespace DAL.Callback
{
    internal class CallbackRouter : ICallback
    {
        /// <summary> Объект CallbackRouter </summary>
        private static CallbackRouter _instance;

        /// <summary> Получить объект CallbackRouter </summary>
        public static CallbackRouter Instance => _instance ?? (_instance = new CallbackRouter());

        /// <summary> Делегат для события изменения контактов </summary>
        public delegate void ListModelContactChangedEventHandler(List<ModelContact> listChangedModelContact, List<ModelContact> listDeletedModelContact);

        /// <summary> Событие изменения контактов </summary>
        public event ListModelContactChangedEventHandler ListModelContactChanged;

        /// <summary> Событие изменения собственного статуса </summary>
        public event EventHandler<PackageModelContactStatus> ModelUserStatusChanged;

        /// <summary> Событие изменения статусов контактов </summary>
        public event EventHandler<List<PackageModelContactStatus>> ListModelContactStatusChanged;

        /// <summary> Событие отсутствия секретного ключа </summary>
        public event EventHandler PresenceOffline;

        /// <summary> Событие изменения подписок контактов </summary>
        public event EventHandler<List<PackageModelContactSubscription>> ListModelContactSubscriptionChanged;

        /// <summary> Событие изменения состояния подключений к серверам и сети </summary>
        public event EventHandler<ModelConnectState> ModelConnectStateChanged;

        /// <summary> Событие изменения состояния звонка </summary>
        public event EventHandler<ModelCall> ModelCallChanged;

        /// <summary> Делегат для события изменения чатов </summary>
        public delegate void ListModelChatChangedEventHandler(List<ModelChat> listChangedModelChat, List<ModelChat> listDeletedModelChat);

        /// <summary> Событие изменения чатов </summary>
        public event ListModelChatChangedEventHandler ListModelChatChanged;

        /// <summary> Событие изменения сообщений чата </summary>
        public event EventHandler<List<ModelChatMessage>> ListModelChatMessageChanged;

        /// <summary> Делегат для события генерации секретного ключа </summary>
        public delegate void SecretKeyGeneratedEventHandler(string login, int serverAreaCode, SecureString secretKey);

        /// <summary> Событие генерации секретного ключа </summary>
        public event SecretKeyGeneratedEventHandler SecretKeyGenerated;

        /// <summary> Событие отсутствия секретного ключа </summary>
        public event EventHandler SecretKeyMissing;

        /// <summary> Событие изменения настроек приложения (пока без подгрузки объекта настроек, т.к. нигде не используется) </summary>
        public event EventHandler ModelUserSettingsChanged;

        /// <summary> Событие изменения истории вызовов </summary>
        public event EventHandler<ModelCallHistory> ModelCallHistoryChanged;

        /// <summary> Конструктор </summary>
        private CallbackRouter()
        {
            BusynessLogic.GetInstance().SetupCallbackListener(this);
        }

        /// <summary> Обработчик изменения модели внутри логики C++ </summary>
        public void DoCallback(string modelName, string[] entityIds)
        {
            // Не удалять, т.к. часто нужно !!!
            //Debug.WriteLine("===================== " + modelName);

            switch (modelName)
            {
                case @"Contacts":
                    LogicListContactChangedHandler();
                    break;
                case @"ContactsPresence":
                    if (entityIds.Contains("My"))
                    {
                        LogicUserStatusContactChangedHandler();

                        if (entityIds.Length > 1)
                        {
                            var arrayXmppId = entityIds.Where(obj => obj != "My").ToArray();

                            LogicListStatusContactChangedHandler(arrayXmppId);
                        }
                    }
                    else
                    {
                        LogicListStatusContactChangedHandler(entityIds);
                    }
                    break;
                case @"ContactSubscriptions":
                    LogicListSubscriptionContactChangedHandler(entityIds);
                    break;
                case @"PresenceOffline":
                    LogicPresenceOfflineHandler();
                    break;
                case @"NetworkStateChanged":
                    LogicModelConnectStateChangedHandler();
                    break;
                case @"Calls":
                    LogicModelCallChangedHandler();
                    break;
                case @"Chats":
                    LogicListModelChatChangedHandler(entityIds);
                    break;
                case @"ChatMessages":
                    LogicListModelChatMessageChangedHandler(entityIds);
                    break;
                case @"SecretKey":
                    if (entityIds.Contains("Generated")) LogicSecretKeyGeneratedHandler();
                    if (entityIds.Contains("Needed")) LogicSecretKeyMissingHandler();
                    break;
                case @"UserSettingsChanged":
                    LogicModelUserSettingsChangedHandler();
                    break;
                case @"History":
                    LogicModelCallHistoryChangedHandler();
                    break;
                    //default:
                    //CheckCallbackName(modelName);
                    //break;
            }
        }

        //[Conditional("DEBUG")]
        //private void CheckCallbackName(string callbackName)
        //{
        //    throw new Exception($@"Неизвестный ключ Callback'a: {callbackName}");
        //}

        /// <summary> Обработчик изменений контактов внутри логики С++ </summary>
        private void LogicListContactChangedHandler()
        {
            var listChangedModelContact = new List<ModelContact>();
            var listDeletedModelContact = new List<ModelContact>();

            DataSourceContact.GetListChangedModelContact(listChangedModelContact, listDeletedModelContact);

            OnListModelContactChanged(listChangedModelContact, listDeletedModelContact);
        }

        /// <summary> Обработчик изменения собственного статуса внутри логики С++ </summary>
        private void LogicUserStatusContactChangedHandler()
        {
            var packageModelContactStatus = DataSourceUser.GetModelUserStatus();

            OnModelUserStatusChanged(packageModelContactStatus);
        }

        /// <summary> Обработчик изменений статусов контактов внутри логики С++ </summary>
        private void LogicListStatusContactChangedHandler(string[] entityIds)
        {
            var listPackageModelContactStatus = DataSourceContact.GetListModelContactStatus(entityIds);

            OnListModelContactStatusChanged(listPackageModelContactStatus);
        }

        /// <summary> Обработчик отсутствия статуса внутри логики С++ </summary>
        private void LogicPresenceOfflineHandler()
        {
            OnPresenceOffline();
        }

        /// <summary> Обработчик изменения подписки контактов внутри логики C++ </summary>
        private void LogicListSubscriptionContactChangedHandler(string[] entityIds)
        {
            var listModelContactSubscription = DataSourceContact.GetListModelContactSubscription(entityIds);

            OnListModelContactSubscriptionChanged(listModelContactSubscription);
        }

        /// <summary> Обработчик изменения состояния подключений внутри логики C++ </summary>
        private void LogicModelConnectStateChangedHandler()
        {
            var modelConnectState = DataSourceUtility.GetCurrentModelConnectState();

            OnModelConnectStateChanged(modelConnectState);
        }

        /// <summary> Обработчик изменения состояния звонка внутри логики C++ </summary>
        private void LogicModelCallChangedHandler()
        {
            var modelCall = DataSourceCall.GetListActiveCall()?.FirstOrDefault();

            OnModelCallChanged(modelCall);
        }

        /// <summary> Обработчик изменения чатов внутри логики C++ </summary>
        private void LogicListModelChatChangedHandler(string[] entityIds)
        {
            var listChangedModelChat = new List<ModelChat>();

            var listDeletedModelChat = new List<ModelChat>();

            var listModelChat = DataSourceChat.GetListModelChatById(entityIds);

            foreach (var i in entityIds)
            {
                var modelChat = listModelChat.FirstOrDefault(obj => obj.Id == i);

                if (modelChat != null)
                {
                    listChangedModelChat.Add(modelChat);
                }
                else
                {
                    listDeletedModelChat.Add(new ModelChat { Id = i });
                }
            }

            OnListModelChatChanged(listChangedModelChat, listDeletedModelChat);
        }

        /// <summary> Обработчик изменения сообщений чатов внутри логики C++ </summary>
        private void LogicListModelChatMessageChangedHandler(string[] entityIds)
        {
            var listModelChatMessage = DataSourceChat.GetModelChatMessageById(entityIds);

            OnListModelChatMessageChanged(listModelChatMessage);
        }

        /// <summary> Обработчик генерации секретного ключа внутри логики C++ </summary>
        private void LogicSecretKeyGeneratedHandler()
        {
            var modelLogin = DataSourceLogin.GetLastModelLogin();
            var login = modelLogin.Login;
            var serverArea = modelLogin.ServerAreaCode;

            var secretKey = DataSourceSecurity.GetUserSecretKey();

            OnSecretKeyGenerated(login, serverArea, secretKey);
        }

        /// <summary> Обработчик отсуствия секретного ключа внутри логики C++ </summary>
        private void LogicSecretKeyMissingHandler()
        {
            OnSecretKeyMissing();
        }

        /// <summary> Обработчик изменения настроек внутри логики C++ </summary>
        private void LogicModelUserSettingsChangedHandler()
        {
            OnModelUserSettingsChanged();
        }

        /// <summary> Обработчик изменения истории вызовов логики C++ </summary>
        private void LogicModelCallHistoryChangedHandler()
        {
            var modelCallHistory = DataSourceCall.GetModelCallHistoryAllDetail();

            OnModelCallHistoryChanged(modelCallHistory);
        }

        /// <summary> Инвокатор события ListModelContactChanged </summary>
        private void OnListModelContactChanged(List<ModelContact> listChangedModelContact, List<ModelContact> listDeletedModelContact)
        {
            ListModelContactChanged?.Invoke(listChangedModelContact, listDeletedModelContact);
        }

        /// <summary> Инвокатор события ModelUserStatusChanged </summary>
        private void OnModelUserStatusChanged(PackageModelContactStatus packageModelContactStatus)
        {
            ModelUserStatusChanged?.Invoke(this, packageModelContactStatus);
        }

        /// <summary> Инвокатор события PresenceOffline </summary>
        private void OnPresenceOffline()
        {
            PresenceOffline?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Инвокатор события ListModelContactStatusChanged </summary>
        private void OnListModelContactStatusChanged(List<PackageModelContactStatus> listPackageModelContactStatus)
        {
            ListModelContactStatusChanged?.Invoke(this , listPackageModelContactStatus);
        }

        /// <summary> Инвокатор события ListModelContactSubscriptionChanged </summary>
        private void OnListModelContactSubscriptionChanged(List<PackageModelContactSubscription> listPackageModelContactSubscription)
        {
            ListModelContactSubscriptionChanged?.Invoke(this, listPackageModelContactSubscription);
        }

        /// <summary> Инвокатор события ModelConnectStateChanged </summary>
        private void OnModelConnectStateChanged(ModelConnectState modelConnectState)
        {
            ModelConnectStateChanged?.Invoke(this, modelConnectState);
        }

        /// <summary> Инвокатор события ModelCallChanged </summary>
        private void OnModelCallChanged(ModelCall modelCall)
        {
            ModelCallChanged?.Invoke(this, modelCall);
        }

        /// <summary> Инвокатор события ListModelChatChanged </summary>
        private void OnListModelChatChanged(List<ModelChat> listChangedModelChat, List<ModelChat> listDeletedModelChat)
        {
            ListModelChatChanged?.Invoke(listChangedModelChat, listDeletedModelChat);
        }

        /// <summary> Инвокатор события ListModelChatMessageChanged </summary>
        private void OnListModelChatMessageChanged(List<ModelChatMessage> listModelChatMessage)
        {
            ListModelChatMessageChanged?.Invoke(null, listModelChatMessage);
        }

        /// <summary> Инвокатор события SecretKeyGenerated </summary>
        private void OnSecretKeyGenerated(string login, int serverAreaCode, SecureString secretKey)
        {
            SecretKeyGenerated?.Invoke(login, serverAreaCode, secretKey);
        }

        /// <summary> Инвокатор события SecretKeyMissing </summary>
        private void OnSecretKeyMissing()
        {
            SecretKeyMissing?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Инвокатор события ModelUserSettingsChanged </summary>
        private void OnModelUserSettingsChanged()
        {
            ModelUserSettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Инвокатор события ModelCallHistoryChanged </summary>
        private void OnModelCallHistoryChanged(ModelCallHistory modelCallHistory)
        {
            ModelCallHistoryChanged?.Invoke(this, modelCallHistory);
        }
    }
}
