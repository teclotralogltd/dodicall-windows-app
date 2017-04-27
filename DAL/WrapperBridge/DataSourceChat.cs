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
using System.Threading;
using System.Threading.Tasks;
using dodicall;
using DAL.Abstract;
using DAL.Model;
using DAL.ModelEnum;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DAL.WrapperBridge
{
    internal class DataSourceChat : AbstractDataSource
    {
        /// <summary> Получить ModelChatMessage из ChatMessageModelManaged </summary>
        private static ModelChatMessage GetModelChatMessageFromChatMessageModelManaged(ChatMessageModelManaged chatMessageModelManaged)
        {
            if (chatMessageModelManaged == null) return null;

            var dataSourceContact = new DataSourceContact();

            var result = new ModelChatMessage
            {
                Id = chatMessageModelManaged.Id,
                IdChat = chatMessageModelManaged.ChatId,
                SendTime = chatMessageModelManaged.SendTime,
                Servered = chatMessageModelManaged.Servered,
                Readed = chatMessageModelManaged.Readed,
                StringContent = chatMessageModelManaged.StringContent,
                Changed = chatMessageModelManaged.Changed,
                Rownum = chatMessageModelManaged.Rownum,
                Encrypted = chatMessageModelManaged.Encrypted,

                Sender = DataSourceContact.GetModelContactFromContactModelManaged(chatMessageModelManaged.Sender),
                ModelContactData = DataSourceContact.GetModelContactFromContactModelManaged(chatMessageModelManaged.ContactData),
                
                ModelEnumChatMessageTypeObj = ModelEnumChatMessageType.GetModelEnum((int)chatMessageModelManaged.Type),
                ModelEnumChatMessageQuoteTypeObj = ModelEnumChatMessageQuoteType.GetModelEnum((int)chatMessageModelManaged.QuoteType),
                ModelEnumChatMessageSecurityLevelObj = ModelEnumChatMessageSecurityLevel.GetModelEnum((int)chatMessageModelManaged.SecurityLevel)
            };
            
            if (result.ModelContactData != null)
            {
                DataSourceContact.RefreshModelContactStatus(result.ModelContactData);
            }

            var quotedMessages = ConvertChatMessageModelManagedAtModelChatMessage(chatMessageModelManaged.QuotedMessages);

            if (quotedMessages.Count != 0)
            {
                result.ListQuotedModelChatMessage = new ObservableCollection<ModelChatMessage>(quotedMessages);
            }

            if (chatMessageModelManaged.NotificationData != null)
            {
                result.ModelNotificationDataObj = new ModelNotificationData()
                {
                    ListModelContact = DataSourceContact.GetListModelContactFromArrayContactModelManaged(chatMessageModelManaged.NotificationData.Contacts),
                    ModelEnumChatNotificationTypeObj = ModelEnumChatNotificationType.GetModelEnum((int)chatMessageModelManaged.NotificationData.Type)
                };
            }



            return result;
        }

        /// <summary> Сохранить черновик </summary>
        public static bool SaveDraftModelChatMessage(ModelChatMessage modelChatMessage)
        {
            return Logic.SaveChatMessageDraft(GetChatMessageModelManagedFromModelChatMessage(modelChatMessage));
        }

        /// <summary> Получить ChatMessageModelManaged из ModelChatMessage </summary>
        private static ChatMessageModelManaged GetChatMessageModelManagedFromModelChatMessage(ModelChatMessage modelChatMessage)
        {
            var result = new ChatMessageModelManaged
            {
                Changed = modelChatMessage.Changed,
                ChatId = modelChatMessage.IdChat,
                Readed = modelChatMessage.Readed,
                Rownum = modelChatMessage.Rownum,
                StringContent = modelChatMessage.StringContent,
                Sender = DataSourceContact.GetContactModelManagedFromModelContact(modelChatMessage.Sender)
            };

            if (modelChatMessage.ModelContactData != null)
                result.ContactData = DataSourceContact.GetContactModelManagedFromModelContact(modelChatMessage.ModelContactData);

            if (modelChatMessage.ListQuotedModelChatMessage != null)
            {
                result.QuotedMessages = GetArrayChatMessageModelManagedFromListModelChatMessage(modelChatMessage.ListQuotedModelChatMessage.ToList());
                result.QuoteType = ChatMessageQuoteTypeManaged.Quote;
            }

            //if (modelChatMessage.ListQuotedModelChatMessage.Count > 0) result.QuoteType = ChatMessageQuoteTypeManaged.Quote;

            return result;
        }

        /// <summary> Получить массив ChatMessageModelManaged из листа ModelChatMessage </summary>
        private static ChatMessageModelManaged [] GetArrayChatMessageModelManagedFromListModelChatMessage(List<ModelChatMessage> listModelChatMessage)
        {
            var result = new List<ChatMessageModelManaged>();

            foreach (var i in listModelChatMessage)
            {
                result.Add(GetChatMessageModelManagedFromModelChatMessage(i));
            }

            return result.ToArray();
        }

        /// <summary> Получить ModelChat из ChatModelManaged </summary>
        private static ModelChat GetModelChatFromChatModelManaged(ChatModelManaged chatModelManaged)
        {
            var result = new ModelChat
            {
                Id = chatModelManaged.Id,
                Active = chatModelManaged.Active,
                LastModifiedDate = chatModelManaged.LastModifiedDate,
                NewMessagesCount = chatModelManaged.NewMessagesCount,
                Title = chatModelManaged.Title,
                TotalMessagesCount = chatModelManaged.TotalMessagesCount,
                IsP2P = chatModelManaged.IsP2p,
                Secured = chatModelManaged.Secured,
                ListModelContact = DataSourceContact.GetListModelContactFromArrayContactModelManaged(chatModelManaged.Contacts),
                LastMessage = GetModelChatMessageFromChatMessageModelManaged(chatModelManaged.LastMessage),
                DraftMessage = GetModelChatMessageFromChatMessageModelManaged(chatModelManaged.DraftMessage)
            };

            return result;
        }

        /// <summary> Получить ChatModelManaged из ModelChat </summary>
        private static ChatModelManaged GetChatModelManagedFromModelChat(ModelChat modelChat)
        {
            var result = new ChatModelManaged
            {
                // тут сетим поля из modelChat
            };

            return result;
        }

        /// <summary> Получить список ModelChat из списка ChatModelManaged </summary>
        private static List<ModelChat> GetListModelChatFromArrayChatModelManaged(ChatModelManaged[] arrayChatModelManaged)
        {
            if (arrayChatModelManaged == null) return null;

            var result = new List<ModelChat>();

            foreach (var chatModelManaged in arrayChatModelManaged)
            {
                result.Add(GetModelChatFromChatModelManaged(chatModelManaged));
            }

            return result;
        }

        /// <summary> Получить список чатов </summary>
        public static List<ModelChat> GetListModelChat()
        {
            var arrayChatModelManaged = Logic.GetAllChats();

            var listModelChat = GetListModelChatFromArrayChatModelManaged(arrayChatModelManaged?.ToArray());

            RefreshModelContactChatStatus(listModelChat);

            return listModelChat;
        }

        /// <summary> Обновить статусы контактов чатов </summary>
        public static void RefreshModelContactChatStatus(List<ModelChat> listModelChat)
        {
            if (listModelChat != null && listModelChat.Count > 0)
            {
                var listModelContact = listModelChat.Where(obj => obj.IsP2P && obj.ModelContactChat != null);

                var test = listModelContact.Select(obj => obj.ModelContactChat).ToList();

                DataSourceContact.RefreshModelContactStatus(test);
            }
        }

        /// <summary> Возвращает список чатов по списку Id </summary>
        public static List<ModelChat> GetListModelChatById(string[] entityIds)
        {
            return GetListModelChatFromArrayChatModelManaged(Logic.GetChatsByIds(entityIds));
        }

        /// <summary> Возвращает список чатов по списку Id с измененными сообщениями </summary>
        public static List<ModelChat> GetListModelChatByIdMessage(string[] entityIds)
        {
            var arrayIdChat = Logic.GetMessagesByIds(entityIds).Select(obj => obj.ChatId).ToArray();

            return GetListModelChatFromArrayChatModelManaged(Logic.GetChatsByIds(arrayIdChat));
        }

        /// <summary> Возвращает чат с контактом (если чата нет то создаст новый) </summary>
        public static ModelChat GetModelChatByModelContact(ModelContact modelContact)
        {
            var arrayContactModelManaged = new[] { DataSourceContact.GetContactModelManagedFromModelContact(modelContact) };

            var result = GetModelChatFromChatModelManaged(Logic.CreateChatWithContacts(arrayContactModelManaged));

            RefreshModelContactChatStatus(new List<ModelChat> { result });

            return result;
        }

        /// <summary> Возвращает чат со списком контактов (создает всегда новый чат) </summary>
        public static ModelChat GetModelChatByListModelContact(List<ModelContact> listModelContact)
        {
            var arrayContactModelManaged = DataSourceContact.GetArrayContactModelManagedFromListModelContact(listModelContact);

            var test = Logic.CreateChatWithContacts(arrayContactModelManaged);

            var result = GetModelChatFromChatModelManaged(test);

            RefreshModelContactChatStatus(new List<ModelChat> { result });

            return result;
        }

        /// <summary> Получить список ModelChatMessage из списка ChatMessageModelManaged </summary>
        private static List<ModelChatMessage> ConvertChatMessageModelManagedAtModelChatMessage(ChatMessageModelManaged[] arrayChatMessageModelManaged)
        {
            if (arrayChatMessageModelManaged == null) return null;

            var result = new List<ModelChatMessage>();

            foreach (var chatMessageModelManaged in arrayChatMessageModelManaged)
            {
                result.Add(GetModelChatMessageFromChatMessageModelManaged(chatMessageModelManaged));
            }

            return result;
        }

        /// <summary> Получить список ChatMessageModelManaged из списка ModelChatMessage </summary>
        private static List<ChatMessageModelManaged> ConvertListModelChatMessageFromListChatMessageModelManaged(List<ModelChatMessage> listModelChatMessage)
        {
            if (listModelChatMessage == null) return null;

            var result = new List<ChatMessageModelManaged>();

            foreach (var modelChatMessage in listModelChatMessage)
            {
                result.Add(ConvertModelChatMessageFromChatMessageModelManaged(modelChatMessage));
            }

            return result;
        }

        /// <summary> Возвращает список сообщений по ModelChat </summary>
        public static List<ModelChatMessage> GetListModelChatMessageByIdChat(ModelChat modelChat)
        {
            var chatId = modelChat.Id;

            var listChatMessageModelManaged = ConvertChatMessageModelManagedAtModelChatMessage(Logic.GetChatMessages(chatId));

            return listChatMessageModelManaged;
        }

        /// <summary> Возвращает список сообщений постранично </summary>
        public static List<ModelChatMessage> GetPagedModelChatMessageByIdChat(ModelChat modelChat)
        {
            var chatId = modelChat.Id;

            var listChatMessageModelManaged = Logic.GetChatMessagesPaged(chatId, 50, "", -1);

            var listModelChatMessage = ConvertChatMessageModelManagedAtModelChatMessage(listChatMessageModelManaged);

            return listModelChatMessage;
        }

        /// <summary> Возвращает список старых сообщений постранично </summary>
        public static List<ModelChatMessage> GetPagedOldModelChatMessage(ModelChatMessage modelChatMessage)
        {
            var chatId = modelChatMessage.IdChat;

            var messageId = modelChatMessage.Id;

            var listChatMessageModelManaged = Logic.GetChatMessagesPaged(chatId, 50, messageId, -1);

            var listModelChatMessage = ConvertChatMessageModelManagedAtModelChatMessage(listChatMessageModelManaged);

            listModelChatMessage.Reverse();

            return listModelChatMessage;
        }

        /// <summary> Возвращает список новых сообщений постранично </summary>
        public static List<ModelChatMessage> GetPagedNewModelChatMessage(ModelChatMessage modelChatMessage)
        {
            var chatId = modelChatMessage.IdChat;

            var messageId = modelChatMessage.Id;

            var listChatMessageModelManaged = Logic.GetChatMessagesPaged(chatId, 50, messageId, 1);

            var listModelChatMessage = ConvertChatMessageModelManagedAtModelChatMessage(listChatMessageModelManaged);

            return listModelChatMessage;
        }

        /// <summary> Возвращает список сообщений по списку Id </summary>
        public static List<ModelChatMessage> GetModelChatMessageById(string[] entityIds)
        {
            return ConvertChatMessageModelManagedAtModelChatMessage(Logic.GetMessagesByIds(entityIds));
        }

        /// <summary> Получить ChatMessageModelManaged из ModelChatMessage </summary>
        private static ChatMessageModelManaged ConvertModelChatMessageFromChatMessageModelManaged(ModelChatMessage modelChatMessage)
        {
            if (modelChatMessage == null) return null;

            var result = new ChatMessageModelManaged
            {
                Id = modelChatMessage.Id,
                ChatId = modelChatMessage.IdChat,
                SendTime = modelChatMessage.SendTime,
                Servered = modelChatMessage.Servered,
                Readed = modelChatMessage.Readed,
                StringContent = modelChatMessage.StringContent,

                Sender = DataSourceContact.GetContactModelManagedFromModelContact(modelChatMessage.Sender),
                ContactData = DataSourceContact.GetContactModelManagedFromModelContact(modelChatMessage.ModelContactData),
                Type = (ChatMessageTypeManaged)modelChatMessage.ModelEnumChatMessageTypeObj.Code
            };


            if (modelChatMessage.ModelNotificationDataObj != null)
            {
                result.NotificationData = new ChatNotificationDataManaged()
                {
                    Contacts = DataSourceContact.ConvertListModelContactFromArrayContactModelManaged(modelChatMessage.ModelNotificationDataObj.ListModelContact).ToArray(),
                    Type = (ChatNotificationTypeManaged)modelChatMessage.ModelNotificationDataObj.ModelEnumChatNotificationTypeObj.Code
                };
            }

            if (modelChatMessage.HaveQuoted)
            {
                result.QuotedMessages = ConvertListModelChatMessageFromListChatMessageModelManaged(modelChatMessage.ListQuotedModelChatMessage.ToList()).ToArray();
            }

            return result;
        }

        /// <summary> Отправить новое сообщение </summary>
        public static bool SendModelChatMessage(ModelChatMessage modelChatMessage)
        {
            var result = true;

            if (modelChatMessage == null) return false;

            modelChatMessage.Id = Logic.PregenerateMessageId();

            var thread = new Thread(obj => Logic.SendTextMessage(modelChatMessage.Id, modelChatMessage.IdChat, modelChatMessage.StringContent));
            thread.Start();

            return result;
        }

        /// <summary> Отправить контакты </summary>
        public static bool SendListModelContactToChat(List<ModelContact> listModelContact, ModelChat modelChat)
        {
            var result = true;

            if (listModelContact == null) return false;

            foreach (var i in listModelContact)
            {
                Logic.SendContactToChat(Logic.PregenerateMessageId(), modelChat.Id, DataSourceContact.GetContactModelManagedFromModelContact(i));
            }

            return result;
        }

        /// <summary> Переименовать чат</summary>
        public static bool RenameModelChat(ModelChat modelChat)
        {
            var result = true;

            if (modelChat == null) return false;

            var thread = new Thread(obj => Logic.RenameChat(modelChat.Id, modelChat.Title));
            thread.Start();

            return result;
        }

        /// <summary> Добавление/удаление собеседников в чат</summary>
        public static bool InviteAndRevokeChatMembers(ModelChat modelChat, List<ModelContact> membersToInvite, List<ModelContact> membersToRevoke)
        {
            if (String.IsNullOrWhiteSpace(modelChat.Id) || membersToInvite == null || membersToRevoke == null)
            {
                return false;
            }

            var resString = Logic.InviteAndRevokeChatMembers(modelChat.Id,
                                                             DataSourceContact.ConvertListModelContactFromArrayContactModelManaged(membersToInvite).ToArray(),
                                                             DataSourceContact.ConvertListModelContactFromArrayContactModelManaged(membersToRevoke).ToArray());

            return !String.IsNullOrWhiteSpace(resString);
        }

        /// <summary> Пометить сообщения как прочитанные (помечаются все сообщения выше переданного) </summary>
        public static bool MarkReadModelChatMessage(ModelChatMessage modelChatMessage)
        {
            return Logic.MarkMessagesAsReaded(modelChatMessage.Id);
        }

        /// <summary> Удалить сообщение </summary>
        public static bool DeleteModelChatMessage(ModelChatMessage modelChatMessage)
        {
            return Logic.DeleteMessages(new string[] { modelChatMessage.Id });
        }

        /// <summary> Разрешение редактировать сообщение </summary>
        public static bool CanEditMessage(ModelChatMessage modelChatMessage)
        {
            return Logic.CanEditMessage(modelChatMessage.Id);
        }

        /// <summary> Редактировать сообщение </summary>
        public static bool EditMessage(ModelChatMessage modelChatMessage, string text)
        {
            return Logic.CorrectMessage(modelChatMessage.Id, text);
        }

        /// <summary> Редактировать сообщение с ответом </summary>
        public static bool EditMessage(ModelChatMessage modelChatMessage, string text, bool shouldDeleteQuote)
        {
            return Logic.CorrectMessage(modelChatMessage.Id, text, shouldDeleteQuote);
        }

        /// <summary> Ответить на сообщение </summary>
        public static bool QuoteAndSendMessages(ModelChatMessage modelChatMessage, List<ModelChatMessage> listQuotedModelChatMessage, bool JustAnswer)
        {
            var arrayQuoteMessages = ConvertListModelChatMessageFromListChatMessageModelManaged(listQuotedModelChatMessage).ToArray();

            modelChatMessage.Id = Logic.PregenerateMessageId();

            return Logic.QuoteAndSendMessages(arrayQuoteMessages, modelChatMessage.Id, modelChatMessage.IdChat, modelChatMessage.StringContentText, JustAnswer);
        }

        /// <summary> Удалить чат (локально) </summary>
        public static bool DeleteModelChat(ModelChat modelChat)
        {
            string[] arrayChatId;

            Logic.ClearChats(new[] { modelChat.Id }, out arrayChatId);

            return !arrayChatId.Contains(modelChat.Id);
        }
    }
}
