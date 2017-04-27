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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Model;

namespace DAL.Localization
{
    public class LocalizationApp : AbstractNotifyPropertyChanged
    {
        /// <summary> Событие изменения языка приложения </summary>
        public event EventHandler<ModelLanguage> LocalizationChanged;

        /// <summary> Объект LocalizationApp </summary>
        private static LocalizationApp _instance;

        /// <summary> Язык приложения </summary>
        private ModelLanguage _modelLanguageObj;

        /// <summary> Язык приложения </summary>
        public ModelLanguage ModelLanguageObj
        {
            get { return _modelLanguageObj; }
            internal set
            {
                if (_modelLanguageObj == value) return;
                _modelLanguageObj = value;
                OnPropertyChanged("ModelLanguageObj");
                OnLocalizationChanged(_modelLanguageObj);
            }
        }

        /// <summary> Словарь en </summary>
        private Dictionary<string, string> _dictionaryEn;

        /// <summary> Словарь ru </summary>
        private Dictionary<string, string> _dictionaryRu;

        /// <summary> Получить объект LocalizationApp </summary>
        public static LocalizationApp GetInstance()
        {
            if (_instance == null) _instance = new LocalizationApp();

            return _instance;
        }

        /// <summary> Конструктор </summary>
        private LocalizationApp()
        {
            FillDictionaries();

            _modelLanguageObj = ModelLanguage.GetModelLanguage(@"en");
        }

        /// <summary> Заполнение словарей </summary>
        private void FillDictionaries()
        {
            if (_dictionaryEn == null)
            {
                _dictionaryEn = new Dictionary<string, string>
                {
                    {@"ModelServerArea_IndustrialServer", @"Industrial server"},
                    {@"ModelServerArea_TestServer", @"Test server"},

                    {@"ModelEnumEchoCancellationMode_Off", @"Off"},
                    {@"ModelEnumEchoCancellationMode_Soft", @"Soft"},
                    {@"ModelEnumEchoCancellationMode_Hard", @"Hard"},

                    {@"ModelEnumUserBaseStatus_Online", @"Online"},
                    {@"ModelEnumUserBaseStatus_Offline", @"Offline"},
                    {@"ModelEnumUserBaseStatus_Hidden", @"Hidden"},
                    {@"ModelEnumUserBaseStatus_Dnd", @"Do not disturb"},

                    {@"ModelEnumTheme_default", @"Default"},

                    {@"ModelEnumUserSettingsGroup_User", @"Profile"},
                    {@"ModelEnumUserSettingsGroup_Common", @"Common"},
                    {@"ModelEnumUserSettingsGroup_Security", @"Security"},
                    {@"ModelEnumUserSettingsGroup_Telecommunication", @"Telecommunication"},
                    {@"ModelEnumUserSettingsGroup_Chat", @"Chat"},
                    {@"ModelEnumUserSettingsGroup_GuiSettings", @"UI Settings"},
                    {@"ModelEnumUserSettingsGroup_Information", @"Information"},
                    {@"ModelEnumUserSettingsGroup_Trace", @"Trace"},

                    {@"ViewModelUserSettings_AboutUri", @"http://"},
                    {@"ViewModelUserSettings_NewsUri", @"http://"},
                    {@"ViewModelUserSettings_ProblemsUri", @"http://"},
                    {@"ViewModelUserSettings_PrivacyPolicyUri", @"https://"},
                    {@"ViewModelUserSettings_HelpUri", @"http://"},

                    {@"ModelEnumVoipEncryption_None", @"None"},
                    {@"ModelEnumVoipEncryption_Srtp", @"Srtp"},

                    {@"ViewModelContact_AllContact", @"All contacts"},
                    {@"ViewModelContact_DodicallContact", @"dodicall"},
                    {@"ViewModelContact_SavedContact", @"Saved contacts"},
                    {@"ViewModelContact_BlockedContact", @"Blocked contacts"},
                    {@"ViewModelContact_WhiteContact", @"White contacts"},

                    {@"ViewModelUserSettings_WhiteContact1to1", @"contact"},
                    {@"ViewModelUserSettings_WhiteContact2to9", @"contacts"},

                    {@"ModelEnumUserContactType_Sip", @"sip"},
                    {@"ModelEnumUserContactType_Xmpp", @"xmpp"},
                    {@"ModelEnumUserContactType_Phone", @"phone"},

                    {@"ViewModelManualContact_ContactNotSaved", @"Contact was not saved"},

                    {@"ViewModelUserSettings_LogEmpty", @"Log is empty"},

                    {@"ModelEnumSubscriptionStatus_Confirmed", @"Confirmed"},
                    {@"ModelEnumSubscriptionStatus_New", @"New"},
                    {@"ModelEnumSubscriptionStatus_Readed", @"Read"},

                    {@"ModelEnumChatMessageType_TextMessage", @"Text message"},
                    {@"ModelEnumChatMessageType_Subject", @"has changed the subject to"},
                    {@"ModelEnumChatMessageType_Subject_Description", @"Subject was modified"},
                    {@"ModelEnumChatMessageType_AudioMessage", @"Audio message"},
                    {@"ModelEnumChatMessageType_Notification", @"Notification"},
                    {@"ModelEnumChatMessageType_Contact", @"Card contact"},
                    {@"ModelEnumChatMessageType_Deleter", @"Message was deleted"},
                    {@"ModelEnumChatMessageType_Secured", @"Protected chat"},
                    {@"ModelEnumChatMessageType_Draft", @"Message is draft"},

                    {@"ModelEnumChatMessageSecurityLevel_None", @"Message is not encrypted"},
                    {@"ModelEnumChatMessageSecurityLevel_Crypted", @"Message is encrypted"},

                    {@"ModelEnumChatMessageQuoteType_Answer", @"Answer"},
                    {@"ModelEnumChatMessageQuoteType_Quote", @"Quote"},
                    {@"ModelEnumChatMessageQuoteType_Forward", @"Forward"},

                    {@"ModelEnumChatNotificationType_Create", @"create chat"},
                    {@"ModelEnumChatNotificationType_Invite", @"invite contact(s)"},
                    {@"ModelEnumChatNotificationType_Revoke", @"remove contact(s)"},
                    {@"ModelEnumChatNotificationType_Leave", @"leave chat"},
                    {@"ModelEnumChatNotificationType_Remove", @"delete chat"},

                    {@"ModelChat_Today", @"Today"},
                    {@"ModelChat_Yesterday", @"Yesterday"},

                    {@"ModelEnumCallDirection_Outgoing", @"Outgoing call"},
                    {@"ModelEnumCallDirection_Incoming", @"Incoming call"},

                    {@"ModelEnumCallAddressType_CallAddressPhone", @"Phone"},
                    {@"ModelEnumCallAddressType_CallAddressDodicall", @"d-sip"},

                    {@"ModelEnumCallState_CallStateInitialized", @"Initialized call"},
                    {@"ModelEnumCallState_CallStateDialing", @"Dialing call"},
                    {@"ModelEnumCallState_CallStateRinging", @"Ringing call"},
                    {@"ModelEnumCallState_CallStateConversation", @"Conversation"},
                    {@"ModelEnumCallState_CallStateEarlyMedia", @"Autoresponder"},
                    {@"ModelEnumCallState_CallStatePaused", @"Paused call"},
                    {@"ModelEnumCallState_CallStateEnded", @"Ended call"},

                    {@"ModelChat_Participant1to1", @"participant"},
                    {@"ModelChat_Participant2to9", @"participants"},

                    {@"ModelChatMessage_User", @"User"},
                    {@"ModelChatMessage_ToChat", @"to chat"},
                    {@"ModelChatMessage_FromChat", @"from chat"},
                    {@"ModelChatMessage_Untitled", @"Untitled"},
                    {@"ModelChatMessage_Quoted", @"Quoted message"},

                    {@"ViewModelSelectionContact_User1to1", @"user"},
                    {@"ViewModelSelectionContact_User2to9", @"users"},

                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypeAny", @"All calls"},
                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypePhone", @"Phone call"},
                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip11", @"dodicall call long"},
                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip4", @"dodicall call short"},

                    {@"ModelEnumCallEndMode_CallEndModeNormalManaged", @"Call successful"},
                    {@"ModelEnumCallEndMode_CallEndModeCancelManaged", @"Call fail"},

                    {@"ModelEnumCallHistorySourceType_HistorySourcePhoneBook", @"Call to friend"},
                    {@"ModelEnumCallHistorySourceType_HistorySourceOthers", @"Call to not friend"},
                    {@"ModelEnumCallHistorySourceType_HistorySourceAny", @"All calls"},

                    {@"ModelEnumCallHistoryStatusType_HistoryStatusSuccess", @"Call successful"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusAborted", @"Call aborted"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusMissed", @"Call missed"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusDeclined", @"Call declined (not use in GUI)"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusAny", @"All calls"},

                    {@"ModelEnumCallHistoryEntryResult_Incomming", @"Incoming"},
                    {@"ModelEnumCallHistoryEntryResult_IncomingFail", @"Missed"},
                    {@"ModelEnumCallHistoryEntryResult_Outgoing", @"Outgoing"},
                    {@"ModelEnumCallHistoryEntryResult_OutgoingFail", @"Cancelled"},

                    {@"ModelCallHistoryEntry_Hour1to1", @"hour"},
                    {@"ModelCallHistoryEntry_Hour2to9", @"hours"},
                    {@"ModelCallHistoryEntry_Minute1to1", @"minute"},
                    {@"ModelCallHistoryEntry_Minute2to9", @"minutes"},
                    {@"ModelCallHistoryEntry_Second1to1", @"second"},
                    {@"ModelCallHistoryEntry_Second2to9", @"seconds"},

                    {@"ModelEnumCurrency_Rub", @"₽"},
                    {@"ModelEnumCurrency_Usd", @"$"},
                    {@"ModelEnumCurrency_Eur", @"€"},
                    {@"ModelEnumCurrency_Pound", @"£"},

                    {@"ModelEnumVideoSizeCell_QVGA", @"QVGA"},
                    {@"ModelEnumVideoSizeCell_VGA", @"VGA"},
                    {@"ModelEnumVideoSizeCell_p720", @"p720"},

                    {@"ModelEnumVideoSizeWifi_QVGA", @"QVGA"},
                    {@"ModelEnumVideoSizeWifi_VGA", @"VGA"},
                    {@"ModelEnumVideoSizeWifi_p720", @"p720"},

                    {@"ModelNetworkTechnology_None", @"Not connected"},
                    {@"ModelNetworkTechnology_Wifi", @"WiFi"},
                    {@"ModelNetworkTechnology_g2", @"2G"},
                    {@"ModelNetworkTechnology_g3", @"3G"},
                    {@"ModelNetworkTechnology_g4", @"LTE(4G)"},

                    {@"ModelEnumServerConnectionState_None", @"None"},
                    {@"ModelEnumServerConnectionState_Failed", @"Disconnected"},
                    {@"ModelEnumServerConnectionState_Progress", @"Connecting"},
                    {@"ModelEnumServerConnectionState_Success", @"Connected"},
 
                    {@"ViewModelCallRedirect_AllContact", @"All contacts"},
                    {@"ViewModelCallRedirect_DodicallContact", @"dodicall"},
                    {@"ViewModelCallRedirect_SavedContact", @"Saved contacts"},
                    {@"ViewModelCallRedirect_BlockedContact", @"Blocked contacts"},
                    {@"ViewModelCallRedirect_WhiteContact", @"White contacts"},
 
                    {@"UtilityDate_Jan", @"Jan"},
                    {@"UtilityDate_Feb", @"Feb"},
                    {@"UtilityDate_Mar", @"Mar"},
                    {@"UtilityDate_Apr", @"Apr"},
                    {@"UtilityDate_May", @"May"},
                    {@"UtilityDate_Jun", @"Jun"},
                    {@"UtilityDate_Jul", @"Jul"},
                    {@"UtilityDate_Aug", @"Aug"},
                    {@"UtilityDate_Sep", @"Sep"},
                    {@"UtilityDate_Oct", @"Oct"},
                    {@"UtilityDate_Nov", @"Nov"},
                    {@"UtilityDate_Dec", @"Dec"} 
                };
            }

            if (_dictionaryRu == null)
            {
                _dictionaryRu = new Dictionary<string, string>
                {
                    {@"ModelServerArea_IndustrialServer", @"Промышленный сервер"},
                    {@"ModelServerArea_TestServer", @"Тестовый сервер"},

                    {@"ModelEnumEchoCancellationMode_Off", @"Выкл"},
                    {@"ModelEnumEchoCancellationMode_Soft", @"Мягкое"},
                    {@"ModelEnumEchoCancellationMode_Hard", @"Жесткое"},

                    {@"ModelEnumUserBaseStatus_Online", @"В сети"},
                    {@"ModelEnumUserBaseStatus_Offline", @"Не в сети"},
                    {@"ModelEnumUserBaseStatus_Hidden", @"Невидимый"},
                    {@"ModelEnumUserBaseStatus_Dnd", @"Не беспокоить"},

                    {@"ModelEnumTheme_default", @"По умолчанию"},

                    {@"ModelEnumUserSettingsGroup_User", @"Профиль"},
                    {@"ModelEnumUserSettingsGroup_Common", @"Общие"},
                    {@"ModelEnumUserSettingsGroup_Security", @"Безопасность"},
                    {@"ModelEnumUserSettingsGroup_Telecommunication", @"Телефония"},
                    {@"ModelEnumUserSettingsGroup_Chat", @"Чат"},
                    {@"ModelEnumUserSettingsGroup_GuiSettings", @"Настройки интерфейса"},
                    {@"ModelEnumUserSettingsGroup_Information", @"Информация"},
                    {@"ModelEnumUserSettingsGroup_Trace", @"Отладка"},

                    {@"ViewModelUserSettings_AboutUri", @"http://"},
                    {@"ViewModelUserSettings_NewsUri", @"http://"},
                    {@"ViewModelUserSettings_ProblemsUri", @"http://"},
                    {@"ViewModelUserSettings_PrivacyPolicyUri", @"https://"},
                    {@"ViewModelUserSettings_HelpUri", @"http://"},

                    {@"ModelEnumVoipEncryption_None", @"Без шифрования"},
                    {@"ModelEnumVoipEncryption_Srtp", @"Srtp"},

                    {@"ViewModelContact_AllContact", @"Все контакты"},
                    {@"ViewModelContact_DodicallContact", @"dodicall"},
                    {@"ViewModelContact_SavedContact", @"Сохраненные"},
                    {@"ViewModelContact_BlockedContact", @"Заблокированные"},
                    {@"ViewModelContact_WhiteContact", @"Белые"},

                    {@"ViewModelUserSettings_WhiteContact1to1", @"контакт"},
                    {@"ViewModelUserSettings_WhiteContact2to4", @"контакта"},
                    {@"ViewModelUserSettings_WhiteContact5to9", @"контактов"},

                    {@"ModelEnumUserContactType_Sip", @"sip"},
                    {@"ModelEnumUserContactType_Xmpp", @"xmpp"},
                    {@"ModelEnumUserContactType_Phone", @"телефон"},

                    {@"ViewModelManualContact_ContactNotSaved", @"Контакт не был сохранен"},

                    {@"ViewModelUserSettings_LogEmpty", @"Лог пуст"},

                    {@"ModelEnumSubscriptionStatus_Confirmed", @"Подтверждено"},
                    {@"ModelEnumSubscriptionStatus_New", @"Новый"},
                    {@"ModelEnumSubscriptionStatus_Readed", @"Прочитано"},

                    {@"ModelEnumChatMessageType_TextMessage", @"Текстовое сообщение"},
                    {@"ModelEnumChatMessageType_Subject", @"изменил тему чата на"},
                    {@"ModelEnumChatMessageType_Subject_Description", @"Тема была изменена"},
                    {@"ModelEnumChatMessageType_AudioMessage", @"Аудио сообщение"},
                    {@"ModelEnumChatMessageType_Notification", @"Извещение"},
                    {@"ModelEnumChatMessageType_Contact", @"Карточка контакта"},
                    {@"ModelEnumChatMessageType_Deleter", @"Сообщение удалено"},
                    {@"ModelEnumChatMessageType_Secured", @"C этого момента чат защищён"},
                    {@"ModelEnumChatMessageType_Draft", @"Черновик"},

                    {@"ModelEnumChatMessageSecurityLevel_None", @"Сообщение не зашифровано"},
                    {@"ModelEnumChatMessageSecurityLevel_Crypted", @"Сообщение зашифровано"},

                    {@"ModelEnumChatMessageQuoteType_Answer", @"Ответ"},
                    {@"ModelEnumChatMessageQuoteType_Quote", @"Цитированое сообщение"},
                    {@"ModelEnumChatMessageQuoteType_Forward", @"Пересланое сообщение"},

                    {@"ModelEnumChatNotificationType_Create", @"создал чат"},
                    {@"ModelEnumChatNotificationType_Invite", @"добавил пользователя(ей)"},
                    {@"ModelEnumChatNotificationType_Revoke", @"удалил пользователя(ей)"},
                    {@"ModelEnumChatNotificationType_Leave", @"покинул чат"},
                    {@"ModelEnumChatNotificationType_Remove", @"удалил чат"},

                    {@"ModelChat_Today", @"Сегодня"},
                    {@"ModelChat_Yesterday", @"Вчера"},

                    {@"ModelEnumCallDirection_Outgoing", @"Исходящий вызов"},
                    {@"ModelEnumCallDirection_Incoming", @"Входящий вызов"},

                    {@"ModelEnumCallAddressType_CallAddressPhone", @"Телефон"},
                    {@"ModelEnumCallAddressType_CallAddressDodicall", @"d-sip"},

                    {@"ModelEnumCallState_CallStateInitialized", @"Вызов инициализирован"},
                    {@"ModelEnumCallState_CallStateDialing", @"Вызов дозванивается"},
                    {@"ModelEnumCallState_CallStateRinging", @"Вызов гудки"},
                    {@"ModelEnumCallState_CallStateConversation", @"Разговор"},
                    {@"ModelEnumCallState_CallStateEarlyMedia", @"Автоответчик"},
                    {@"ModelEnumCallState_CallStatePaused", @"Вызов на паузе"},
                    {@"ModelEnumCallState_CallStateEnded", @"Вызов завершен"},

                    {@"ModelChat_Participant1to1", @"участник"},
                    {@"ModelChat_Participant2to4", @"участника"},
                    {@"ModelChat_Participant5to9", @"участников"},

                    {@"ModelChatMessage_User", @"Пользователь"},
                    {@"ModelChatMessage_ToChat", @"в чат"},
                    {@"ModelChatMessage_FromChat", @"из чата"},
                    {@"ModelChatMessage_Untitled", @"Без названия"},
                    {@"ModelChatMessage_Quoted", @"Цитированое сообщение"},

                    {@"ViewModelSelectionContact_User1to1", @"пользователь"},
                    {@"ViewModelSelectionContact_User2to4", @"пользователя"},
                    {@"ViewModelSelectionContact_User5to9", @"пользователей"},

                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypeAny", @"Все звонки"},
                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypePhone", @"Телефонный звонок"},
                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip11", @"dodicall звонок длинный"},
                    {@"ModelEnumCallHistoryAddressType_HistoryAddressTypeSip4", @"dodicall звонок короткий"},

                    {@"ModelEnumCallEndMode_CallEndModeNormalManaged", @"Звонок состоялся"},
                    {@"ModelEnumCallEndMode_CallEndModeCancelManaged", @"Звонок не состоялся"},

                    {@"ModelEnumCallHistorySourceType_HistorySourcePhoneBook", @"Звонок другу"},
                    {@"ModelEnumCallHistorySourceType_HistorySourceOthers", @"Звонок не добавленному в друзья"},
                    {@"ModelEnumCallHistorySourceType_HistorySourceAny", @"все звонки"},

                    {@"ModelEnumCallHistoryStatusType_HistoryStatusSuccess", @"Звонок состоялся"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusAborted", @"Звонок не состоялся"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusMissed", @"Звонок пропущен"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusDeclined", @"Звонок отклонен на другой стороне (не используется в GUI)"},
                    {@"ModelEnumCallHistoryStatusType_HistoryStatusAny", @"Все звонки"},

                    {@"ModelEnumCallHistoryEntryResult_Incomming", @"Входящий"},
                    {@"ModelEnumCallHistoryEntryResult_IncomingFail", @"Пропущенный"},
                    {@"ModelEnumCallHistoryEntryResult_Outgoing", @"Исходящий"},
                    {@"ModelEnumCallHistoryEntryResult_OutgoingFail", @"Несостоявшийся"},

                    {@"ModelCallHistoryEntry_Hour1to1", @"час"},
                    {@"ModelCallHistoryEntry_Hour2to4", @"часа"},
                    {@"ModelCallHistoryEntry_Hour5to9", @"часов"},
                    {@"ModelCallHistoryEntry_Minute1to1", @"минута"},
                    {@"ModelCallHistoryEntry_Minute2to4", @"минуты"},
                    {@"ModelCallHistoryEntry_Minute5to9", @"минут"},
                    {@"ModelCallHistoryEntry_Second1to1", @"секунда"},
                    {@"ModelCallHistoryEntry_Second2to4", @"секунды"},
                    {@"ModelCallHistoryEntry_Second5to9", @"секунд"},

                    {@"ModelEnumCurrency_Rub", @"₽"},
                    {@"ModelEnumCurrency_Usd", @"$"},
                    {@"ModelEnumCurrency_Eur", @"€"},
                    {@"ModelEnumCurrency_Pound", @"£"},

                    {@"ModelEnumVideoSizeCell_QVGA", @"QVGA"},
                    {@"ModelEnumVideoSizeCell_VGA", @"VGA"},
                    {@"ModelEnumVideoSizeCell_p720", @"p720"},

                    {@"ModelEnumVideoSizeWifi_QVGA", @"QVGA"},
                    {@"ModelEnumVideoSizeWifi_VGA", @"VGA"},
                    {@"ModelEnumVideoSizeWifi_p720", @"p720"},

                    {@"ModelNetworkTechnology_None", @"Нет подключения"},
                    {@"ModelNetworkTechnology_Wifi", @"WiFi"},
                    {@"ModelNetworkTechnology_g2", @"2G"},
                    {@"ModelNetworkTechnology_g3", @"3G"},
                    {@"ModelNetworkTechnology_g4", @"LTE(4G)"},

                    {@"ModelEnumServerConnectionState_None", @"Нет данных"},
                    {@"ModelEnumServerConnectionState_Failed", @"Отключено"},
                    {@"ModelEnumServerConnectionState_Progress", @"Идет подключение"},
                    {@"ModelEnumServerConnectionState_Success", @"Подключено"}, 
                     
                    {@"ViewModelCallRedirect_AllContact", @"Все контакты"},
                    {@"ViewModelCallRedirect_DodicallContact", @"dodicall"},
                    {@"ViewModelCallRedirect_SavedContact", @"Сохраненные"},
                    {@"ViewModelCallRedirect_BlockedContact", @"Заблокированные"},
                    {@"ViewModelCallRedirect_WhiteContact", @"Белые"},

                    {@"UtilityDate_Jan", @"янв"},
                    {@"UtilityDate_Feb", @"февр"},
                    {@"UtilityDate_Mar", @"март"},
                    {@"UtilityDate_Apr", @"апр"},
                    {@"UtilityDate_May", @"май"},
                    {@"UtilityDate_Jun", @"июнь"},
                    {@"UtilityDate_Jul", @"июль"},
                    {@"UtilityDate_Aug", @"авг"},
                    {@"UtilityDate_Sep", @"сент"},
                    {@"UtilityDate_Oct", @"окт"},
                    {@"UtilityDate_Nov", @"нояб"},
                    {@"UtilityDate_Dec", @"дек"} 
                };
            }
        }

        /// <summary> Возвращает значение по ключу для текущего языка приложения </summary>
        public string GetValueByKey(string key)
        {
            var result = String.Empty;

            try
            {
                if (ModelLanguageObj.CodeName.Equals(@"en", StringComparison.InvariantCultureIgnoreCase)) result = _dictionaryEn[key];

                if (ModelLanguageObj.CodeName.Equals(@"ru", StringComparison.InvariantCultureIgnoreCase)) result = _dictionaryRu[key];
            }
            catch
            {
                if (ModelLanguageObj.CodeName.Equals(@"en", StringComparison.InvariantCultureIgnoreCase)) result = @"(No translate)";

                if (ModelLanguageObj.CodeName.Equals(@"ru", StringComparison.InvariantCultureIgnoreCase)) result = @"(Нет перевода)";
            }

            return result;
        }

        /// <summary> Возвращает объект форматирования чисел для текущего языка приложения </summary>
        public NumberFormatInfo GetNumberFormatInfo()
        {
            var result = new NumberFormatInfo();

            if (ModelLanguageObj.CodeName.Equals(@"en", StringComparison.InvariantCultureIgnoreCase)) result = new CultureInfo("en-US", false).NumberFormat;

            if (ModelLanguageObj.CodeName.Equals(@"ru", StringComparison.InvariantCultureIgnoreCase)) result = new CultureInfo("ru-RU", false).NumberFormat;

            return result;
        }

        /// <summary> Возвращает строку даты в форматированном виде в зависимости от текущего языка </summary>
        public string GetDateString(DateTime dateTime)
        {
            var result = String.Empty;

            var month = dateTime.Month < 10 ? "0" + dateTime.Month : dateTime.Month.ToString();

            var day = dateTime.Day < 10 ? "0" + dateTime.Day : dateTime.Day.ToString();

            if (ModelLanguageObj.CodeName.ToLower() == "en") result = month + "/" + day + "/" + dateTime.Year;

            if (ModelLanguageObj.CodeName.ToLower() == "ru") result = day + "." + month + "." + dateTime.Year;

            return result;
        }

        /// <summary> Инвокатор события LocalizationChanged </summary>
        private void OnLocalizationChanged(ModelLanguage modelLanguage)
        {
            LocalizationChanged?.Invoke(this, modelLanguage);
        }
    }
}
