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
using dodicall;
using DAL.Abstract;
using DAL.Callback;
using DAL.Model;
using DAL.ModelEnum;

namespace DAL.WrapperBridge
{
    internal class DataSourceUser : AbstractDataSource
    {
        /// <summary> Получить текущий ModelUser </summary>
        public static ModelUser GetModelUser()
        {
            var accountData = Logic.GetAccountData();

            var modelUser = ModelUser.GetInstance();

            modelUser.FirstName = accountData.FirstName.Trim();
            modelUser.LastName = accountData.LastName.Trim();
            modelUser.ModelEnumCurrencyObj = ModelEnumCurrency.GetModelEnum((int)Logic.GetBalance().BalanceCurrency);
            modelUser.ModelServerAreaObj = DataSourceLogin.CurrentModelServerArea;

            var packageModelContactStatus = GetModelUserStatus();

            modelUser.ModelEnumUserBaseStatusObj = packageModelContactStatus.ModelEnumUserBaseStatusObj;
            modelUser.UserExtendedStatus = packageModelContactStatus.UserExtendedStatus;

            RefreshModelUserBalance(modelUser);

            RefreshModelUserContact(modelUser);

            return modelUser;
        }

        /// <summary> Возвращает собственный статус </summary>
        public static PackageModelContactStatus GetModelUserStatus()
        {
            var result = new PackageModelContactStatus();

            var presenceStatus = Logic.GetPresenceStatusesByXmppIds(new[] { "My" })?.FirstOrDefault();

            result.ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(Convert.ToInt32(presenceStatus?.BaseStatus));
            result.UserExtendedStatus = presenceStatus?.ExtStatus;

            return result;
        }

        /// <summary> Обновить баланс y ModelUser </summary>
        public static void RefreshModelUserBalance(ModelUser modelUser)
        {
            modelUser.HasBalance = Logic.GetBalance().Success && Logic.GetBalance().HasBalance;
            modelUser.BalanceValue = Logic.GetBalance().BalanceValue;
        }

        /// <summary> Обновить контакты y ModelUser </summary>
        public static void RefreshModelUserContact(ModelUser modelUser)
        {
            var accountData = Logic.GetAccountData();

            if (accountData != null)
            {
                var listModelUserContact = new List<ModelUserContact>();

                foreach (var contact in accountData.Contacts.Where(obj => obj.Type == ContactsContactTypeManaged.Sip))
                {
                    var identity = contact.Identity.Substring(0, contact.Identity.LastIndexOf('@')); // приводим к формату согласно ТЗ
                    listModelUserContact.Add(new ModelUserContact { Favourite = contact.Favourite, Identity = identity, Manual = contact.Manual });
                }

                modelUser.ListModelUserContactMy = listModelUserContact;

                listModelUserContact = new List<ModelUserContact>();

                foreach (var contact in accountData.Contacts.Where(obj => obj.Type == ContactsContactTypeManaged.Phone))
                {
                    listModelUserContact.Add(new ModelUserContact { Favourite = contact.Favourite, Identity = contact.Identity, Manual = contact.Manual });
                }

                modelUser.ListModelUserContactExtra = listModelUserContact;
            }
        }
    }
}
