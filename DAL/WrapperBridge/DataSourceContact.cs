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
using System.Windows.Media.Imaging;
using dodicall;
using DAL.Abstract;
using DAL.Model;
using DAL.ModelEnum;
using DAL.Callback;
using DAL.Utility;

namespace DAL.WrapperBridge
{
    internal class DataSourceContact : AbstractDataSource
    {
        /// <summary> Временный аватар </summary>
        public static BitmapImage Avatar = UtilityPicture.GetBitmapImageFromStringPathAssembly(@"DAL.Resources.NoPhoto.png");

        /// <summary> Временный аватар группы </summary>
        public static BitmapImage AvatarGroup = UtilityPicture.GetBitmapImageFromStringPathAssembly(@"DAL.Resources.NoPhotoGroup.png");

        /// <summary> Свой собстенный контакт </summary>
        private static ModelContact _modelContactIam;
        
        /// <summary> Свой собстенный контакт </summary>
        public static ModelContact ModelContactIam => _modelContactIam ?? (_modelContactIam = GetModelContactIam());
        
        /// <summary> Получить ModelContact из ContactModelManaged </summary>
        public static ModelContact GetModelContactFromContactModelManaged(ContactModelManaged contactModelManaged)
        {
            if (contactModelManaged == null) return null;

            var result = new ModelContact
            {
                Id = contactModelManaged.Id,
                Blocked = contactModelManaged.Blocked,
                Iam = contactModelManaged.Iam,
                DodicallId = contactModelManaged.DodicallId,
                NativeId = contactModelManaged.NativeId,
                White = contactModelManaged.White,
                XmppId = contactModelManaged.XmppId,
                FirstName = contactModelManaged.FirstName.Trim(),
                MiddleName = contactModelManaged.MiddleName.Trim(),
                LastName = contactModelManaged.LastName.Trim(),
                ModelContactSubscriptionObj = GetModelContactSubscriptionFromContactSubscriptionModelManaged(contactModelManaged.Subscription),
                ListModelUserContact = new List<ModelUserContact>(),
                ListModelUserContactExtra = new List<ModelUserContact>()
            };

            foreach (var contact in contactModelManaged.Contacts.Where(obj => obj.Type == ContactsContactTypeManaged.Sip))
            {
                var identity = contact.Identity;
                result.ListModelUserContact.Add(new ModelUserContact { Favourite = contact.Favourite, Identity = identity, Manual = contact.Manual, ModelEnumUserContactTypeObj = ModelEnumUserContactType.GetModelEnum((int)contact.Type) });
            }

            foreach (var contact in contactModelManaged.Contacts.Where(obj => obj.Type == ContactsContactTypeManaged.Phone))
            {
                result.ListModelUserContactExtra.Add(new ModelUserContact { Favourite = contact.Favourite, Identity = contact.Identity, Manual = contact.Manual, ModelEnumUserContactTypeObj = ModelEnumUserContactType.GetModelEnum((int)contact.Type) });
            }

            return result;
        }

        /// <summary> Получить ModelContactSubscription из ContactSubscriptionModelManaged </summary>
        private static ModelContactSubscription GetModelContactSubscriptionFromContactSubscriptionModelManaged(ContactSubscriptionModelManaged contactSubscriptionModelManaged)
        {
            return new ModelContactSubscription
            {
                ModelEnumSubscriptionStatusObj = ModelEnumSubscriptionStatus.GetModelEnum((int)contactSubscriptionModelManaged.SubscriptionStatus),
                Ask = contactSubscriptionModelManaged.AskForSubscription,
                ModelEnumSubscriptionStateObj = ModelEnumSubscriptionState.GetModelEnum((int)contactSubscriptionModelManaged.SubscriptionState)
            };
        }

        /// <summary> Получить ContactModelManaged из ModelContact </summary>
        public static ContactModelManaged GetContactModelManagedFromModelContact(ModelContact modelContact)
        {
            if (modelContact == null) return null;

            var result = new ContactModelManaged
            {
                Id = modelContact.Id,
                Blocked = modelContact.Blocked,
                DodicallId = modelContact.DodicallId,
                NativeId = modelContact.NativeId,
                White = modelContact.White,
                FirstName = modelContact.FirstName?.Trim(),
                MiddleName = modelContact.MiddleName?.Trim(),
                LastName = modelContact.LastName?.Trim(),
                Subscription = modelContact.ModelContactSubscriptionObj != null ? new ContactSubscriptionModelManaged
                {
                    AskForSubscription = modelContact.ModelContactSubscriptionObj.Ask,
                    SubscriptionStatus = (ContactSubscriptionStatusManaged)modelContact.ModelContactSubscriptionObj.ModelEnumSubscriptionStatusObj.Code,
                    SubscriptionState = (ContactSubscriptionStateManaged)modelContact.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code
                } : null,
            };

            if (!String.IsNullOrWhiteSpace(modelContact.XmppId))
            {
                result.Contacts = new ContactsContactModelManaged[(modelContact.ListModelUserContact?.Count ?? 0) + (modelContact.ListModelUserContactExtra?.Count ?? 0) + 1];

                result.Contacts[result.Contacts.Length - 1] = new ContactsContactModelManaged { Type = ContactsContactTypeManaged.Xmpp, Identity = modelContact.XmppId, Manual = false, Favourite = false };
            }
            else
            {
                result.Contacts = new ContactsContactModelManaged[(modelContact.ListModelUserContact?.Count ?? 0) + (modelContact.ListModelUserContactExtra?.Count ?? 0)];
            }

            var index = 0;

            if (modelContact.ListModelUserContact != null)
            {
                foreach (var modelUserContact in modelContact.ListModelUserContact)
                {
                    var contactsContactModelManaged = new ContactsContactModelManaged { Favourite = modelUserContact.Favourite, Identity = modelUserContact.Identity, Manual = modelUserContact.Manual, Type = (ContactsContactTypeManaged)(modelUserContact.ModelEnumUserContactTypeObj?.Code ?? 0) };
                    result.Contacts[index] = contactsContactModelManaged;
                    index++;
                }
            }

            if (modelContact.ListModelUserContactExtra != null)
            {
                foreach (var modelUserContact in modelContact.ListModelUserContactExtra)
                {
                    var contactsContactModelManaged = new ContactsContactModelManaged { Favourite = modelUserContact.Favourite, Identity = modelUserContact.Identity, Manual = modelUserContact.Manual, Type = (ContactsContactTypeManaged)(modelUserContact.ModelEnumUserContactTypeObj?.Code ?? 0) };
                    result.Contacts[index] = contactsContactModelManaged;
                    index++;
                }
            }

            return result;
        }

        /// <summary> Получить список ModelContact из массива ContactModelManaged </summary>
        public static List<ModelContact> GetListModelContactFromArrayContactModelManaged(ContactModelManaged[] arrayContactModelManaged)
        {
            if (arrayContactModelManaged == null) return null;

            var result = new List<ModelContact>();

            foreach (var contact in arrayContactModelManaged)
            {
                result.Add(GetModelContactFromContactModelManaged(contact));
            }

            return result;
        }

        /// <summary> Получить массив ContactModelManaged из списка ModelContact </summary>
        public static ContactModelManaged[] GetArrayContactModelManagedFromListModelContact(List<ModelContact> listContactModelManaged)
        {
            if (listContactModelManaged == null) return null;

            var result = new List<ContactModelManaged>();

            foreach (var i in listContactModelManaged)
            {
                result.Add(GetContactModelManagedFromModelContact(i));
            }

            return result.ToArray();
        }

        /// <summary> Получить список ContactModelManaged из списка ModelContact </summary>
        public static List<ContactModelManaged> ConvertListModelContactFromArrayContactModelManaged(List<ModelContact> listModelContact)
        {
            if (listModelContact == null) return null;

            List<ContactModelManaged> result = new List<ContactModelManaged>();

            foreach (var contact in listModelContact)
            {
                result.Add(GetContactModelManagedFromModelContact(contact));
            }

            return result;
        }

        /// <summary> Получить список контактов </summary>
        public static List<ModelContact> GetListModelContact()
        {
            var result = new List<ModelContact>();

            var listContact = Logic.GetAllContacts();

            foreach (var contact in listContact.Where(obj => obj.Subscription.SubscriptionState != ContactSubscriptionStateManaged.To && obj.Iam == false))
            {
                result.Add(GetModelContactFromContactModelManaged(contact));
            }

            RefreshModelContactStatus(result);

            return result;
        }

        /// <summary> Обновить статусы контактов </summary>
        public static void RefreshModelContactStatus(List<ModelContact> listModelContact)
        {
            var presenceStatus = Logic.GetPresenceStatusesByXmppIds(listModelContact.Select(modelContact => modelContact?.XmppId).ToArray());

            foreach (var modelContact in listModelContact)
            {
                if(modelContact != null)
                {
                    modelContact.ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(Convert.ToInt32(presenceStatus.FirstOrDefault(obj => obj.XmppId == modelContact?.XmppId)?.BaseStatus));
                    modelContact.UserExtendedStatus = presenceStatus.FirstOrDefault(obj => obj.XmppId == modelContact.XmppId)?.ExtStatus;
                }
            }
        }

        /// <summary> Обновить статус контакта </summary>
        public static void RefreshModelContactStatus(ModelContact modelContact)
        {
            var presenceStatus = Logic.GetPresenceStatusesByXmppIds(new [] { modelContact.XmppId }); 
            modelContact.ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(Convert.ToInt32(presenceStatus.FirstOrDefault(obj => obj.XmppId == modelContact?.XmppId)?.BaseStatus));
            modelContact.UserExtendedStatus = presenceStatus.FirstOrDefault(obj => obj.XmppId == modelContact.XmppId)?.ExtStatus;
        }

        /// <summary> Возвращает статусы контактов </summary>
        public static List<PackageModelContactStatus> GetListModelContactStatus(string[] arrayXmppId)
        {
            var result = new List<PackageModelContactStatus>();

            var presenceStatus = Logic.GetPresenceStatusesByXmppIds(arrayXmppId);

            foreach (var contactPresenceStatusModelManaged in presenceStatus)
            {
                var packageModelContactStatus = new PackageModelContactStatus
                {
                    XmppId = contactPresenceStatusModelManaged.XmppId,
                    ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(Convert.ToInt32(contactPresenceStatusModelManaged.BaseStatus)),
                    UserExtendedStatus = contactPresenceStatusModelManaged.ExtStatus
                };

                result.Add(packageModelContactStatus);
            }

            return result;
        }

        /// <summary> Возвращает список добавленных/обновленных и удаленных </summary>
        public static void GetListChangedModelContact(List<ModelContact> listChangedModelContact, List<ModelContact> listDeletedModelContact)
        {
            ContactModelManaged[] updated, deleted;
            Logic.RetrieveChangedContacts(out updated, out deleted); 

            listChangedModelContact.AddRange(updated.Select(GetModelContactFromContactModelManaged));
            RefreshModelContactStatus(listChangedModelContact);
            
            // тут подзапрашивать статус подписки
            //var dictionaryModelContactSubscription = GetDictionaryModelContactSubscriptionByArrayXmppId(listChangedModelContact.Select(obj => obj.XmppId).ToArray());
            //foreach (var i in listChangedModelContact)
            //{
            //    i.ModelContactSubscriptionObj = dictionaryModelContactSubscription.FirstOrDefault(obj => obj.Key == i.XmppId).Value;
            //}

            listDeletedModelContact.AddRange(deleted.Select(GetModelContactFromContactModelManaged));
        }  

        /// <summary> Возвращает кол-во белых контактов </summary>
        public static int GetCountWhiteContact()
        {
            var listContact = Logic.GetAllContacts();

            return listContact.Any(obj => obj.White) ? listContact.Where(obj => obj.White).Count() : 0;
        }

        /// <summary> Поиск конткатов в директории </summary>
        public static List<ModelContact> FindModelContactsInDirectory(string filter)
        {
            ContactModelManaged[] arrayContactModelManaged;

            Logic.FindContactsInDirectory(out arrayContactModelManaged, filter);

            return GetListModelContactFromArrayContactModelManaged(arrayContactModelManaged);
        }

        /// <summary> Сохраняет контакт в бд </summary>
        private static ContactModelManaged SaveContactModelManaged(ContactModelManaged contactModelManaged)
        {
            return Logic.SaveContact(contactModelManaged);
        }

        /// <summary> Сохраняет новый сохраненный контакт </summary>
        public static bool SaveModelContact(ModelContact modelContact)
        {
            var result = false;

            var contactModelManaged = GetContactModelManagedFromModelContact(modelContact);

            var contactModelManagedSaved = SaveContactModelManaged(contactModelManaged);

            if (contactModelManagedSaved != null && contactModelManagedSaved.Id != 0)
            {
                modelContact.Id = contactModelManagedSaved.Id;

                result = true;
            }

            return result;
        }

        /// <summary> Сохраняет новый сохраненный контакт и возвращает сохраненный контакт (новый объект) </summary>
        public static ModelContact SaveModelContactResultModelContact(ModelContact modelContact)
        {
            var contactModelManaged = GetContactModelManagedFromModelContact(modelContact);

            return GetModelContactFromContactModelManaged(SaveContactModelManaged(contactModelManaged));
        }

        /// <summary> Отправить заявку контакту </summary>
        public static bool SendRequestModelContact(ModelContact modelContact)
        {
            var result = false;

            var contactModelManaged = GetContactModelManagedFromModelContact(modelContact);

            var contactModelManagedSaved = Logic.SaveContact(contactModelManaged);

            if (contactModelManagedSaved != null && contactModelManagedSaved.Id != 0)
            {
                contactModelManagedSaved.Subscription.AskForSubscription = true; // кастыль, потому что бизнес логика возвращает его false, СВ сказал пока вставить кастыль

                var modelContactSaved = GetModelContactFromContactModelManaged(contactModelManagedSaved);

                modelContact.Id = modelContactSaved.Id;
                modelContact.Avatar = modelContactSaved.Avatar;
                modelContact.Blocked = modelContactSaved.Blocked;
                modelContact.DodicallId = modelContactSaved.DodicallId;
                modelContact.FirstName = modelContactSaved.FirstName;
                modelContact.MiddleName = modelContactSaved.MiddleName;
                modelContact.LastName = modelContactSaved.LastName;
                modelContact.NativeId = modelContactSaved.NativeId;
                modelContact.UserExtendedStatus = modelContactSaved.UserExtendedStatus;
                modelContact.White = modelContactSaved.White;
                modelContact.XmppId = modelContactSaved.XmppId;

                modelContact.ListModelUserContact = modelContactSaved.ListModelUserContact;
                modelContact.ListModelUserContactExtra = modelContactSaved.ListModelUserContactExtra;
                modelContact.ModelContactSubscriptionObj = modelContactSaved.ModelContactSubscriptionObj;
                modelContact.ModelEnumUserBaseStatusObj = modelContactSaved.ModelEnumUserBaseStatusObj;

                result = true;
            }

            return result;
        }

        /// <summary> Заблокировать контакт </summary>
        public static bool BlockModelContact(ModelContact modelContact)
        {
            var result = false;

            var contactModelManaged = GetContactModelManagedFromModelContact(modelContact);

            contactModelManaged.Blocked = true;

            var contactModelManagedSaved = SaveContactModelManaged(contactModelManaged);

            if (contactModelManagedSaved != null && contactModelManagedSaved.Id != 0)
            {
                modelContact.Blocked = true;
                result = true;
            }

            return result;
        }

        /// <summary> Разблокировать контакт </summary>
        public static bool UnblockModelContact(ModelContact modelContact)
        {
            var result = false;

            var contactModelManaged = GetContactModelManagedFromModelContact(modelContact);

            contactModelManaged.Blocked = false;

            var contactModelManagedSaved = SaveContactModelManaged(contactModelManaged);

            if (contactModelManagedSaved != null && contactModelManagedSaved.Id != 0)
            {
                modelContact.Id = contactModelManagedSaved.Id;

                modelContact.Blocked = false;
                result = true;
            }

            return result;
        }

        /// <summary> Удаляет контакт из книги контактов пользователя </summary>
        public static bool DeleteModelContact(ModelContact modelContact)
        {
            return Logic.DeleteContact(GetContactModelManagedFromModelContact(modelContact));
        }

        /// <summary> Возвращает номер телефона в форматированном виде </summary>
        public static string FormatPhone(string phone)
        {
            return Logic.FormatPhone(phone);
        }

        /// <summary> Добавить контакт в белый список </summary>
        public static bool AddToWhiteModelContact(ModelContact modelContact)
        {
            var result = false;

            modelContact.White = true;

            if (SaveModelContact(modelContact))
            {
                result = true;
            }
            else
            {
                modelContact.White = false;
            }

            return result;
        }

        /// <summary> Удалить контакт из белого списока </summary>
        public static bool DeleteFromWhiteModelContact(ModelContact modelContact)
        {
            var result = false;

            modelContact.White = false;

            if (SaveModelContact(modelContact))
            {
                result = true;
            }
            else
            {
                modelContact.White = true;
            }

            return result;
        }

        /// <summary> Возвращает подписки контактов </summary>
        public static List<PackageModelContactSubscription> GetListModelContactSubscription(string[] arrayXmppId)
        {
            var result = new List<PackageModelContactSubscription>();

            var dicrionary = Logic.GetSubscriptionStatusesByXmppIds(arrayXmppId);

            foreach (var i in dicrionary)
            {
                result.Add(new PackageModelContactSubscription { XmppId = i.Key, ModelContactSubscriptionObj = GetModelContactSubscriptionFromContactSubscriptionModelManaged(i.Value) });
            }

            return result;
        }

        /// <summary> Возвращает список контактов отправивших приглашение </summary>
        public static List<ModelContact> GetInviteListModelContact()
        {
            var arrayContactModelManaged = Logic.GetAllContacts().Where(c => c.Subscription.SubscriptionState == ContactSubscriptionStateManaged.To && c.Id == 0).ToArray();

            return GetListModelContactFromArrayContactModelManaged(arrayContactModelManaged);
        }

        /// <summary> Возвращает список контактов которым отправлена заявка </summary>
        public static List<ModelContact> GetRequestListModelContact()
        {
            var arrayContactModelManaged = Logic.GetAllContacts().Where(c => c.Subscription.AskForSubscription).ToArray();

            return GetListModelContactFromArrayContactModelManaged(arrayContactModelManaged);
        }

        /// <summary> Пометить приглашение как прочитанное </summary>
        public static bool ReadInvite(ModelContact modelContact)
        {
            var result = Logic.MarkSubscriptionAsOld(modelContact.XmppId);

            if (result) modelContact.ModelContactSubscriptionObj.ModelEnumSubscriptionStatusObj = ModelEnumSubscriptionStatus.GetModelEnum(2);

            return result;
        }

        /// <summary> Принять приглашение </summary>
        public static bool ApplyInviteModelContact(ModelContact modelContact)
        {
            return SendRequestModelContact(modelContact); // потому что вроде как пока механизм одинаковый
        }

        /// <summary> Отклонить приглашение </summary>
        public static bool DenyInviteModelContact(ModelContact modelContact)
        {
            var contactModelManaged = GetContactModelManagedFromModelContact(modelContact);

            var test = Logic.AnswerSubscriptionRequest(contactModelManaged, false);

            return test;
        }

        /// <summary> Возвращает кол-во непрочитанных приглашений </summary>
        public static int GetCountInviteUnread()
        {
            var arrayContactModelManaged = Logic.GetAllContacts().Where(obj => obj.Subscription.SubscriptionState == ContactSubscriptionStateManaged.To && obj.Id == 0 && obj.Subscription.SubscriptionStatus == ContactSubscriptionStatusManaged.New).ToArray();

            var result = arrayContactModelManaged.Length;

            return result;
        }

        /// <summary> Возвращает свой собственный конакт </summary>
        public static ModelContact GetModelContactIam()
        { 
            return GetModelContactFromContactModelManaged(Logic.GetAccountData());
        }
    }
}

