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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dodicall;
using DAL.Abstract;
using DAL.Model;
using DAL.ModelEnum;

namespace DAL.WrapperBridge
{
    internal class DataSourceCall : AbstractDataSource
    {
        /// <summary> Конвертировать CallModelManaged в ModelCall </summary>
        private static ModelCall ConvertCallModelManagedToModelCall(CallModelManaged callModelManaged)
        {
            var result = new ModelCall
            {
                Id = callModelManaged.Id,
                Duration = callModelManaged.Duration,
                Identity = callModelManaged.Identity,
                ModelContactObj = DataSourceContact.GetModelContactFromContactModelManaged(callModelManaged.Contact),
                ModelEnumCallDirectionObj = ModelEnumCallDirection.GetModelEnum((int)callModelManaged.Direction),
                ModelEnumCallAddressTypeObj = ModelEnumCallAddressType.GetModelEnum((int)callModelManaged.AddressType),
                ModelEnumVoipEncryptionObj = ModelEnumVoipEncryption.GetModelEnum((int)callModelManaged.Encription),
                ModelEnumCallStateObj = ModelEnumCallState.GetModelEnum((int)callModelManaged.State)
            };

            return result;
        }

        /// <summary> Конвертировать ModelCall в CallModelManaged </summary>
        private static CallModelManaged ConvertCallModelCallToCallModelManaged(ModelCall modelCall)
        {
            var result = new CallModelManaged
            {
                Id = modelCall.Id,
                Duration = modelCall.Duration,
                Identity = modelCall.Identity,
                Contact = DataSourceContact.GetContactModelManagedFromModelContact(modelCall.ModelContactObj),
                Direction = (CallDirectionManaged)modelCall.ModelEnumCallDirectionObj.Code,
                AddressType = (CallAddressTypeManaged)modelCall.ModelEnumCallAddressTypeObj.Code,
                Encription = (VoipEncryptionModeManaged)modelCall.ModelEnumVoipEncryptionObj.Code,
                State = (CallStateManaged)modelCall.ModelEnumCallStateObj.Code
            };

            return result;
        }

        /// <summary> Конвертировать CallHistoryModelManaged в ModelCallHistory </summary>
        private static ModelCallHistory ConvertCallHistoryModelManagedToModelCallHistory(CallHistoryModelManaged callHistoryModelManaged)
        {
            var result = new ModelCallHistory
            {
                TotalMissed = callHistoryModelManaged.TotalMissed,
                ListModelCallHistoryPeer = new ObservableCollection<ModelCallHistoryPeer>(callHistoryModelManaged.Peers.Select(ConvertCallHistoryPeerModelManagedToModelCallHistoryPeer))
            };

            return result;
        }

        /// <summary> Конвертировать CallHistoryPeerModelManaged в ModelCallHistoryPeer </summary>
        private static ModelCallHistoryPeer ConvertCallHistoryPeerModelManagedToModelCallHistoryPeer(CallHistoryPeerModelManaged callHistoryPeerModelManaged)
        {
            var result = new ModelCallHistoryPeer
            {
                ModelPeerObj = callHistoryPeerModelManaged.Peer != null ? new ModelPeer
                {
                    Id = callHistoryPeerModelManaged.Peer.Id,
                    Identity = callHistoryPeerModelManaged.Peer.Identity,
                    ModelContactObj = DataSourceContact.GetModelContactFromContactModelManaged(callHistoryPeerModelManaged.Peer.Contact),
                    ModelEnumCallAddressTypeObj = ModelEnumCallAddressType.GetModelEnum((int)callHistoryPeerModelManaged.Peer.AddressType)
                } : null,
                ModelCallStatisticsObj = callHistoryPeerModelManaged.Statistics != null ? new ModelCallStatistics
                {
                    HasIncomingEncryptedCall = callHistoryPeerModelManaged.Statistics.HasIncomingEncryptedCall,
                    HasOutgoingEncryptedCall = callHistoryPeerModelManaged.Statistics.HasOutgoingEncryptedCall,
                    NumberOfIncomingSuccessfulCalls = callHistoryPeerModelManaged.Statistics.NumberOfIncomingSuccessfulCalls,
                    NumberOfIncomingUnsuccessfulCalls = callHistoryPeerModelManaged.Statistics.NumberOfIncomingUnsuccessfulCalls,
                    NumberOfOutgoingSuccessfulCalls = callHistoryPeerModelManaged.Statistics.NumberOfOutgoingSuccessfulCalls,
                    NumberOfOutgoingUnsuccessfulCalls = callHistoryPeerModelManaged.Statistics.NumberOfOutgoingUnsuccessfulCalls,
                    NumberOfMissedCalls = callHistoryPeerModelManaged.Statistics.NumberOfMissedCalls,
                    WasConference = callHistoryPeerModelManaged.Statistics.WasConference
                } : null,
                ListModelCallHistoryEntry = callHistoryPeerModelManaged.DetailsList.Select(ConvertCallHistoryEntryModelManagedToModelCallHistoryEntry).ToList()
            };

            return result;
        }

        /// <summary> Конвертировать CallHistoryEntryModelManaged в ModelCallHistoryEntry </summary>
        private static ModelCallHistoryEntry ConvertCallHistoryEntryModelManagedToModelCallHistoryEntry(CallHistoryEntryModelManaged callHistoryEntryModelManaged)
        {
            return new ModelCallHistoryEntry
            {
                Id = callHistoryEntryModelManaged.Id,
                StartTime = callHistoryEntryModelManaged.StartTime,
                DurationSec = callHistoryEntryModelManaged.DurationSec,
                ModelEnumHistoryAddressTypeObj = ModelEnumCallHistoryAddressType.GetModelEnum((int)callHistoryEntryModelManaged.AddressType),
                ModelEnumCallDirectionObj = ModelEnumCallDirection.GetModelEnum((int)callHistoryEntryModelManaged.Direction),
                ModelEnumVoipEncryptionObj = ModelEnumVoipEncryption.GetModelEnum((int)callHistoryEntryModelManaged.Encription),
                ModelEnumCallEndModeObj = ModelEnumCallEndMode.GetModelEnum((int)callHistoryEntryModelManaged.EndMode),
                ModelEnumCallHistorySourceTypeObj = ModelEnumCallHistorySourceType.GetModelEnum((int)callHistoryEntryModelManaged.HistorySource),
                ModelEnumCallHistoryStatusTypeObj = ModelEnumCallHistoryStatusType.GetModelEnum((int)callHistoryEntryModelManaged.HistoryStatus)
            };
        }

        /// <summary> Начать исходящий звонок </summary>
        public static bool StartOutgoingCall(ModelContact modelContact)
        {
            var contactModelManaged = DataSourceContact.GetContactModelManagedFromModelContact(modelContact);

            return Logic.StartCallToContact(contactModelManaged, CallOptionsManaged.Default);
        }

        /// <summary> Начать исходящий звонок </summary>
        public static bool StartOutgoingCallUrl(string number)
        {
            return Logic.StartCallToUrl(number, CallOptionsManaged.Default);
        } 

        /// <summary> Переадресация вызова </summary>
        /// <param name="callId">Id перенаправляемого вызова</param>  
        /// <param name="number">номер пользователя которому перенаправляется вызов</param>  
        public static bool TransferCallToUrl(string callId, string number)
        {
            return Logic.TransferCallToUrl(callId, number);
        }

        /// <summary> Начать исходящий звонок </summary>
        public static bool StartOutgoingCallUrl(ModelContact modelContact, string number)
        {
            var contactModelManaged = DataSourceContact.GetContactModelManagedFromModelContact(modelContact);

            var numberForCompare = number.Replace(" ", "").Replace("+", "");

            return Logic.StartCallToContactUrl(contactModelManaged, contactModelManaged.Contacts.First(obj => obj.Identity.Replace(" ", "").Replace("+", "") == numberForCompare), CallOptionsManaged.Default);
        }

        /// <summary> Получить активные вызовы </summary>
        public static List<ModelCall> GetListActiveCall()
        {
            var result = new List<ModelCall>();

            var callsModelManaged = Logic.GetAllCalls();

            if (callsModelManaged != null)
            {
                foreach (var i in callsModelManaged.SingleCalls)
                {
                    result.Add(ConvertCallModelManagedToModelCall(i));
                }
            }

            return result;
        }

        /// <summary> Принять вызов </summary>
        public static bool AcceptCall(ModelCall modelCall)
        {
            return modelCall != null ? Logic.AcceptCall(modelCall.Id, CallOptionsManaged.Default) : false;
        }

        /// <summary> Завершить вызов </summary>
        public static bool HangupCall(ModelCall modelCall)
        {
            return modelCall != null ? Logic.HangupCall(modelCall.Id) : false;
        }

        /// <summary> Проверка состояния микрофона </summary>
        public static bool CheckMuteMicrophone()
        {
            return !Logic.IsMicrophoneEnabled();
        }

        /// <summary> Mute/Unmute микрофона </summary>
        public static void MuteMicrophone(bool mute)
        {
            Logic.EnableMicrophone(!mute);
        }

        /// <summary> Получить весь список истории звонков </summary>
        public static ModelCallHistory GetModelCallHistoryAll()
        {
            return ConvertCallHistoryModelManagedToModelCallHistory(Logic.GetCallHistory(new HistoryFilterModelManaged() { Selector = HistoryFilterSelectorManaged.HistoryFilterAny }, false));
        }

        /// <summary> Получить весь список истории звонков с заполненной детализацией </summary>
        public static ModelCallHistory GetModelCallHistoryAllDetail()
        {
            return ConvertCallHistoryModelManagedToModelCallHistory(Logic.GetCallHistory(new HistoryFilterModelManaged() { Selector = HistoryFilterSelectorManaged.HistoryFilterAny }, true));
        }

        /// <summary> Получить кол-во пропущеных звонков (используют ее только андроидщики, хз зачем, видимо руки не от туда растут, iOS ее не используют + АР сказал что у андроидщиков с ней какие то проблемы были) </summary>
        public static int GetCountMissedCalls()
        {
            return Logic.GetNumberOfMissedCalls();
        }

        /// <summary> Получить измененный ModelCallHistory </summary>
        public static ModelCallHistory GetChangedModelCallHistory(string[] arrayIdPeer)
        {
            return ConvertCallHistoryModelManagedToModelCallHistory(Logic.GetCallHistory(new HistoryFilterModelManaged { Peers = arrayIdPeer, Selector = HistoryFilterSelectorManaged.HistoryFilterAny }, false));
        }

        /// <summary> Получить измененный ModelCallHistory с историей вызовов </summary>
        public static ModelCallHistory GetChangedModelCallHistoryDetail(ModelCallHistoryPeer modelCallHistoryPeer)
        {
            var test = Logic.GetCallHistory(new HistoryFilterModelManaged { Peers = new[] { modelCallHistoryPeer.ModelPeerObj.Id }, Selector = HistoryFilterSelectorManaged.HistoryFilterAny }, true);

            return ConvertCallHistoryModelManagedToModelCallHistory(test);
        }

        /// <summary> Сравнение идентификаторов ModelCallHistoryPeer (идиотизм, но так сделана история вызовов в бизнес логике) </summary>
        public static int CompareModelCallHistoryPeerId(ModelCallHistoryPeer modelCallHistoryPeer1, ModelCallHistoryPeer modelCallHistoryPeer2)
        {
            return Logic.CompareCallHistoryPeerIds(modelCallHistoryPeer1.ModelPeerObj.Id, modelCallHistoryPeer2.ModelPeerObj.Id);
        }

        /// <summary> Пометить все пропущенные звонки прочитанными </summary>
        public static void SetCallHistoryReadedAll()
        {
            Logic.SetCallHistoryReaded(new HistoryFilterModelManaged());
        }

        /// <summary> Пометить пропущенные звонки прочитанными </summary>
        public static void SetCallHistoryReaded(ModelCallHistoryPeer modelCallHistoryPeer)
        {
            Logic.SetCallHistoryReaded(new HistoryFilterModelManaged { Peers = new[] { modelCallHistoryPeer.ModelPeerObj.Id } });
        }

        /// <summary> Получить статистическую запись с полной историей вызовов (возвращает новый объект) </summary>
        public static ModelCallHistoryPeer GetModelCallHistoryPeerFull(ModelCallHistoryPeer modelCallHistoryPeer)
        { 
            var numberOrId = modelCallHistoryPeer.ModelPeerObj.ModelContactObj == null ? modelCallHistoryPeer.ModelPeerObj.Identity : modelCallHistoryPeer.ModelPeerObj.Id;

            var historyFilterModelManaged = new HistoryFilterModelManaged
            {
                Peers = new[] { numberOrId }
            }; 
            var callHistoryModelManaged = Logic.GetCallHistory(historyFilterModelManaged, true);

            // делается так по ебанутому потому что в логике возвращается весь список истории с заполенными дитализацями по всем Peer'ам и нет нормальной функции
            // для получения детализации 1-ого Peer
            // Кастыль для случая получения из истории записи для созданного вручную контакта, у этой записи будет новый ИД и иднтифицировать запись истории по этому ИД не получится.
            var callHistoryPeerModelManaged = callHistoryModelManaged.Peers.FirstOrDefault(obj => Logic.CompareCallHistoryPeerIds(obj.Peer.Id, modelCallHistoryPeer.ModelPeerObj.Id) == 0);
            if (callHistoryPeerModelManaged != null)
            {
                return ConvertCallHistoryPeerModelManagedToModelCallHistoryPeer(callHistoryPeerModelManaged);
            } 
            else
            {
                return modelCallHistoryPeer;
            } 
        }

        /// <summary> Начать тоновый сигнал </summary>
        public static void StartSoundSignalShort(char charSignal)
        {
            if (charSignal == 43) charSignal = '0';

            Logic.PlayDtmf((sbyte)charSignal);
            Logic.StopDtmf();
        }

        /// <summary> Начать тоновый сигнал </summary>
        public static void StartSoundSignal(char charSignal)
        {
            Logic.PlayDtmf((sbyte)charSignal);
        }

        /// <summary> Начать тоновый сигнал </summary>
        public static void StopSoundSignal()
        {
            Logic.StopDtmf();
        }
    }
}
