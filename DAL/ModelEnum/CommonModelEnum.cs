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

namespace DAL.ModelEnum
{
    public class CommonModelEnum
    {
        /// <summary> Инициализация всех списков ModelEnum </summary>
        public static void InitializeModelEnum()
        {
            ModelEnumCallAddressType.Initialize();

            ModelEnumCallDirection.Initialize();

            ModelEnumCallEndMode.Initialize();

            ModelEnumCallHistoryAddressType.Initialize();

            ModelEnumCallHistoryEntryResult.Initialize();

            ModelEnumCallHistorySourceType.Initialize();

            ModelEnumCallHistoryStatusType.Initialize();

            ModelEnumCallState.Initialize();

            ModelEnumChatMessageQuoteType.Initialize();

            ModelEnumChatMessageSecurityLevel.Initialize();

            ModelEnumChatMessageType.Initialize();

            ModelEnumChatNotificationType.Initialize();

            ModelEnumCurrency.Initialize();

            ModelEnumEchoCancellationMode.Initialize();

            ModelEnumSubscriptionState.Initialize();

            ModelEnumSubscriptionStatus.Initialize();

            ModelEnumTheme.Initialize();

            ModelEnumUserBaseStatus.Initialize();

            ModelEnumUserContactType.Initialize();

            ModelEnumUserSettingsGroup.Initialize();

            ModelEnumVideoSizeCell.Initialize();

            ModelEnumVideoSizeWifi.Initialize();

            ModelEnumVoipEncryption.Initialize();

            ModelEnumNetworkTechnology.Initialize();

            ModelEnumServerConnectionState.Initialize();
        }
    }
}
