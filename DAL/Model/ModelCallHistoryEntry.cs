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
using DAL.Localization;
using DAL.ModelEnum;

namespace DAL.Model
{
    public class ModelCallHistoryEntry : AbstractLocalization //AbstractModel<ModelCallHistoryEntry>
    {
        private string _id;
        private DateTime _startTime;
        private int _durationSec;
        private ModelEnumCallHistoryAddressType _modelEnumHistoryAddressTypeObj;
        private ModelEnumCallDirection _modelEnumCallDirectionObj;
        private ModelEnumVoipEncryption _modelEnumVoipEncryptionObj;
        private ModelEnumCallEndMode _modelEnumCallEndModeObj;
        private ModelEnumCallHistorySourceType _modelEnumCallHistorySourceTypeObj;
        private ModelEnumCallHistoryStatusType _modelEnumCallHistoryStatusTypeObj;

        /// <summary> Идентификатор </summary>
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary> Время начала звонка </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime == value) return;
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        /// <summary> Время начала звонка строкой </summary>
        public string StartTimeString
        {
            get
            {
                // потом переделать на словарь ModelCallHistoryPeer в LocalizationApp, а лучше переделать на общий механизм отображения дат

                var result = String.Empty;

                if (StartTime.Date == DateTime.Today) result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChat_Today") + " " + StartTime.Hour + ":" + (StartTime.Minute < 10 ? "0" + StartTime.Minute : StartTime.Minute.ToString());

                if (StartTime.Date == DateTime.Today.AddDays(-1)) result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChat_Yesterday") + " " + StartTime.Hour + ":" + (StartTime.Minute < 10 ? "0" + StartTime.Minute : StartTime.Minute.ToString());

                if (StartTime.Date < DateTime.Today.AddDays(-1)) result = LocalizationApp.GetInstance().GetDateString(StartTime) + " " + StartTime.Hour + ":" + (StartTime.Minute < 10 ? "0" + StartTime.Minute : StartTime.Minute.ToString());

                if (StartTime.Date > DateTime.Today) result = LocalizationApp.GetInstance().GetValueByKey(@"ModelChat_Today") + " " + DateTime.Now.Hour + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute : DateTime.Now.Minute.ToString());

                return result;
            }
        }

        /// <summary> Длительность звонка </summary>
        public int DurationSec
        {
            get { return _durationSec; }
            set
            {
                if (_durationSec == value) return;
                _durationSec = value;
                OnPropertyChanged("DurationSec");
            }
        }

        /// <summary> Длительность звонка строкой </summary>
        public string DurationSecString
        {
            get
            {
                // нужно подумать как вынести в отдельный механизм формирование числительных слов, т.к. писать такую простыню из кода в геттере г-нокод !!!

                // потом переделать на словарь ModelCallHistoryPeer в LocalizationApp, а лучше переделать на общий механизм отображения дат

                var result = String.Empty;

                var second = _durationSec % 60;

                var minutes = (int)Math.Floor((decimal)(_durationSec / 60));

                var hours = Math.Floor((decimal)(_durationSec / 3600));

                var localization = LocalizationApp.GetInstance();

                var unitTime = String.Empty;

                if (hours > 0)
                {
                    if (localization.ModelLanguageObj.CodeName.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                    {
                        unitTime = hours == 1 ? localization.GetValueByKey(@"ModelCallHistoryEntry_Hour1to1") : localization.GetValueByKey(@"ModelCallHistoryEntry_Hour2to9");
                    }

                    if (localization.ModelLanguageObj.CodeName.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var last2Number = hours % 100;

                        if (last2Number > 10 && last2Number < 19)
                        {
                            unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Hour5to9");
                        }
                        else
                        {
                            var last1Number = last2Number % 10;

                            if (last1Number == 1) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Hour1to1");

                            if (last1Number > 1 && last1Number < 5) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Hour2to4");

                            if (last1Number < 1 || last1Number > 4) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Hour5to9");
                        }
                    }

                    result = hours + ":" + (minutes > 9 ? minutes.ToString() : @"0" + minutes) + ":" + (second > 9 ? second.ToString() : @"0" + second) + " " + unitTime;
                }
                else
                {
                    if (minutes > 0)
                    {
                        if (localization.ModelLanguageObj.CodeName.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                        {
                            unitTime = minutes == 1 ? localization.GetValueByKey(@"ModelCallHistoryEntry_Minute1to1") : localization.GetValueByKey(@"ModelCallHistoryEntry_Minute2to9");
                        }

                        if (localization.ModelLanguageObj.CodeName.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var last2Number = minutes % 100;

                            if (last2Number > 10 && last2Number < 19)
                            {
                                unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Minute5to9");
                            }
                            else
                            {
                                var last1Number = last2Number % 10;

                                if (last1Number == 1) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Minute1to1");

                                if (last1Number > 1 && last1Number < 5) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Minute2to4");

                                if (last1Number < 1 || last1Number > 4) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Minute5to9");
                            }
                        }

                        result = (minutes > 9 ? minutes.ToString() : @"0" + minutes) + ":" + (second > 9 ? second.ToString() : @"0" + second) + " " + unitTime;
                    }
                    else
                    {
                        if (localization.ModelLanguageObj.CodeName.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                        {
                            unitTime = second == 1 ? localization.GetValueByKey(@"ModelCallHistoryEntry_Second1to1") : localization.GetValueByKey(@"ModelCallHistoryEntry_Second2to9");
                        }

                        if (localization.ModelLanguageObj.CodeName.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var last2Number = second % 100;

                            if (last2Number > 10 && last2Number < 19)
                            {
                                unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Second5to9");
                            }
                            else
                            {
                                var last1Number = last2Number % 10;

                                if (last1Number == 1) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Second1to1");

                                if (last1Number > 1 && last1Number < 5) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Second2to4");

                                if (last1Number < 1 || last1Number > 4) unitTime = localization.GetValueByKey(@"ModelCallHistoryEntry_Second5to9");
                            }
                        }

                        result = second + " " + unitTime;
                    }
                }

                return result;
            }
        }

        /// <summary> Тип звонка </summary>
        public ModelEnumCallHistoryAddressType ModelEnumHistoryAddressTypeObj
        {
            get { return _modelEnumHistoryAddressTypeObj; }
            set
            {
                if (_modelEnumHistoryAddressTypeObj == value) return;
                _modelEnumHistoryAddressTypeObj = value;
                OnPropertyChanged("ModelEnumHistoryAddressTypeObj");
            }
        }

        /// <summary> Направление звонка </summary>
        public ModelEnumCallDirection ModelEnumCallDirectionObj
        {
            get { return _modelEnumCallDirectionObj; }
            set
            {
                if (_modelEnumCallDirectionObj == value) return;
                _modelEnumCallDirectionObj = value;
                OnPropertyChanged("ModelEnumCallDirectionObj");
            }
        }

        /// <summary> Тип шифрования </summary>
        public ModelEnumVoipEncryption ModelEnumVoipEncryptionObj
        {
            get { return _modelEnumVoipEncryptionObj; }
            set
            {
                if (_modelEnumVoipEncryptionObj == value) return;
                _modelEnumVoipEncryptionObj = value;
                OnPropertyChanged("ModelEnumVoipEncryptionObj");
            }
        }

        /// <summary> Тип окончания вызова </summary>
        public ModelEnumCallEndMode ModelEnumCallEndModeObj
        {
            get { return _modelEnumCallEndModeObj; }
            set
            {
                if (_modelEnumCallEndModeObj == value) return;
                _modelEnumCallEndModeObj = value;
                OnPropertyChanged("ModelEnumCallEndModeObj");
            }
        }

        /// <summary> Тип состояния контакта которому звонили (состоит в друзьях или нет) </summary>
        public ModelEnumCallHistorySourceType ModelEnumCallHistorySourceTypeObj
        {
            get { return _modelEnumCallHistorySourceTypeObj; }
            set
            {
                if (_modelEnumCallHistorySourceTypeObj == value) return;
                _modelEnumCallHistorySourceTypeObj = value;
                OnPropertyChanged("ModelEnumCallHistorySourceTypeObj");
            }
        }

        /// <summary> Тип статуса звонка </summary>
        public ModelEnumCallHistoryStatusType ModelEnumCallHistoryStatusTypeObj
        {
            get { return _modelEnumCallHistoryStatusTypeObj; }
            set
            {
                if (_modelEnumCallHistoryStatusTypeObj == value) return;
                _modelEnumCallHistoryStatusTypeObj = value;
                OnPropertyChanged("ModelEnumCallHistoryStatusTypeObj");
            }
        }

        /// <summary> Тип результата звонка </summary>
        public ModelEnumCallHistoryEntryResult ModelEnumCallHistoryEntryResultObj
        {
            get
            {
                ModelEnumCallHistoryEntryResult result = null;

                if (ModelEnumCallHistoryStatusTypeObj.Code == 32) result = ModelEnumCallHistoryEntryResult.GetModelEnum(3); // Canceled

                if (ModelEnumCallHistoryStatusTypeObj.Code == 64) result = ModelEnumCallHistoryEntryResult.GetModelEnum(1); // Missed

                if (result == null && ModelEnumCallDirectionObj.Code == 0)
                {
                    result = ModelEnumCallHistoryEntryResult.GetModelEnum(2); // Outgoing
                }

                if (result == null && ModelEnumCallDirectionObj.Code == 1)
                {
                    result = ModelEnumCallHistoryEntryResult.GetModelEnum(0); // Incoming
                }

                return result;
            }
        }

        /// <summary> Возвращает глубокую копию объекта </summary>
        public ModelCallHistoryEntry GetDeepCopy()
        {
            var result = new ModelCallHistoryEntry
            {
                StartTime = StartTime,
                DurationSec = DurationSec,
                ModelEnumHistoryAddressTypeObj = ModelEnumHistoryAddressTypeObj,
                ModelEnumCallDirectionObj = ModelEnumCallDirectionObj,
                ModelEnumVoipEncryptionObj = ModelEnumVoipEncryptionObj,
                ModelEnumCallEndModeObj = ModelEnumCallEndModeObj,
                ModelEnumCallHistorySourceTypeObj = ModelEnumCallHistorySourceTypeObj,
                ModelEnumCallHistoryStatusTypeObj = ModelEnumCallHistoryStatusTypeObj
            };

            return result;
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {
            OnPropertyChanged("StartTimeString");
            OnPropertyChanged("DurationSecString");
        }
    }
}
