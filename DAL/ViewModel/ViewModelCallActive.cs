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
using System.Timers;
using DAL.Abstract;
using DAL.Callback;
using DAL.Model;
using DAL.WrapperBridge;
using System.Windows; 

namespace DAL.ViewModel
{
    public class ViewModelCallActive : AbstractViewModel, IDisposable
    {
        /// <summary> Флаг начала вызова </summary>
        public static bool ExistCall;

        /// <summary> Флаг активного вызова </summary>
        private bool _isActiveCall;

        /// <summary> Объект Timer </summary>
        private Timer _timer;

        /// <summary> Текущий звонок </summary>
        private ModelCall _currentModelCall;

        /// <summary> Текущий звонок </summary>
        public ModelCall CurrentModelCall
        {
            get { return _currentModelCall; }
            set
            {
                if (_currentModelCall == value) return;
                _currentModelCall = value;
                OnPropertyChanged("CurrentModelCall");
            }
        }

        /// <summary> Mute состояние </summary>
        private bool _mute;

        /// <summary> Mute состояние </summary>
        public bool Mute
        {
            get { return _mute; }
            set
            {
                if (_mute == value) return;
                _mute = value;
                OnPropertyChanged("Mute");
            }
        }

        /// <summary> Продолжительность звонка </summary>
        private int _duration;

        /// <summary> Продолжительность звонка </summary>
        public string Duration
        {
            get
            {
                var minutes = Math.Floor((decimal) (_duration / 60));

                var second = _duration%60;

                return $@"{(minutes > 9 ? minutes.ToString() : @"0" + minutes)}:{(second > 9 ? second.ToString() : @"0" + second)}";
            }
        }

        /// <summary> Команда принятия звонка </summary>
        public Command CommandAcceptCall { get; set; }

        /// <summary> Команда завершения звонка </summary>
        public Command CommandHangupCall { get; set; }

        /// <summary> Команда Mute </summary>
        public Command CommandMuteMicrophone { get; set; }

        /// <summary> Команда CallTransfer</summary>
        public Command CommandCallTransfer { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelCallActive(ModelContact modelContact)
        {
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;

            if (DataSourceCall.StartOutgoingCall(modelContact))
            {
                CurrentModelCall = DataSourceCall.GetListActiveCall()?.FirstOrDefault();
            }
            else
            {
                OnCloseView();
            }

            CommandHangupCall = new Command(obj => HangupCall());
            CommandMuteMicrophone = new Command(obj => MuteMicrophone()); 

            Mute = DataSourceCall.CheckMuteMicrophone();

            ExistCall = true;
        }

        /// <summary> Конструктор </summary>
        public ViewModelCallActive(string number)
        {
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;

            if (DataSourceCall.StartOutgoingCallUrl(number))
            {
                CurrentModelCall = DataSourceCall.GetListActiveCall()?.FirstOrDefault();
            }
            else
            {
                OnCloseView();
            }

            CommandHangupCall = new Command(obj => HangupCall());
            CommandMuteMicrophone = new Command(obj => MuteMicrophone());

            ExistCall = true;
        }

        /// <summary> Конструктор </summary>
        public ViewModelCallActive(ModelContact modelContact, string number)
        {
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;

            if (DataSourceCall.StartOutgoingCallUrl(modelContact, number))
            {
                CurrentModelCall = DataSourceCall.GetListActiveCall()?.FirstOrDefault();
            }
            else
            {
                OnCloseView();
            }

            CommandHangupCall = new Command(obj => HangupCall());
            CommandMuteMicrophone = new Command(obj => MuteMicrophone());

            ExistCall = true;
        }

        /// <summary> Конструктор </summary>
        public ViewModelCallActive()
        {
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;

            CurrentModelCall = DataSourceCall.GetListActiveCall()?.FirstOrDefault();

            if (CurrentModelCall == null)
            {
                OnCloseView();
            }

            CommandHangupCall = new Command(obj => HangupCall());
            CommandAcceptCall = new Command(obj => AcceptCall());
            CommandMuteMicrophone = new Command(obj => MuteMicrophone());

            ExistCall = true;
        }

        /// <summary> Конструктор </summary>
        public ViewModelCallActive(ModelCall modelCall)
        {
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;

            if (modelCall != null)
            {
                CurrentModelCall = modelCall;
            }
            else
            {
                OnCloseView();
            }

            CommandAcceptCall = new Command(obj => AcceptCall());
            CommandHangupCall = new Command(obj => HangupCall()); 

            Mute = DataSourceCall.CheckMuteMicrophone();

            ExistCall = true;
        }

        /// <summary> Метод принятия звонка </summary>
        public void AcceptCall()
        {
            if (DataSourceCall.AcceptCall(CurrentModelCall))
            {
                StartDurationTimer();
                OnEventViewModel("ActiveCall");
            }
        }

        /// <summary> Метод завершения звонка </summary>
        public void HangupCall()
        {
            if (DataSourceCall.HangupCall(CurrentModelCall))
            {
                OnCloseView();
            }
        }

        /// <summary> Метод выключения микрофона </summary>
        public void MuteMicrophone()
        {
            Mute = !Mute;

            DataSourceCall.MuteMicrophone(Mute);

            OnEventViewModel("Mute");
        } 

        /// <summary> Таймер продолжительности звонка </summary>
        private void StartDurationTimer()
        {
            if (_timer == null)
            {
                _duration = CurrentModelCall.Duration;

                _timer = new Timer { Interval = 1000 }; // 1 секунду

                _timer.Elapsed += RefreshDuration;

                _timer.Enabled = true;
            }
        }

        /// <summary> Обновление продолжительности звонка </summary>
        private void RefreshDuration(object sender, ElapsedEventArgs e)
        {
            _duration += 1;

            OnPropertyChanged("Duration");
        }

        /// <summary> Обработчик изменения звонка </summary>
        private void OnModelCallChanged(object sender, ModelCall modelCall)
        {
            Action action = () =>
            {
                CurrentModelCall = modelCall?.GetDeepCopy();

                if (CurrentModelCall != null && CurrentModelCall.ModelEnumCallStateObj.Code == 3 && !_isActiveCall) // Conversation
                {
                    _isActiveCall = true;
                    StartDurationTimer();
                    OnEventViewModel("ActiveCall");
                }

                if (CurrentModelCall == null || CurrentModelCall.ModelEnumCallStateObj.Code == 6) // Ended (почему то иногда прилетает null, видимо в разных потоках уже закрытый звонок удаляется из списка активных в бизнес логике быстрее чем прилетает данный колбек в этом потоке)
                {
                    OnCloseView();
                }
            };

            CurrentDispatcher.BeginInvoke(action);
        }

        ///// <summary> Обработчик изменения модели внутри логики C++ </summary>
        //public void DoCallback(object sender, DoCallbackArgs e)
        //{
        //    if (e.ModelName == "Calls")
        //    {
        //        Action action = () =>
        //        {
        //            CurrentModelCall = _dataSourceCall.GetListActiveCall()?.FirstOrDefault();

        //            if (CurrentModelCall != null && CurrentModelCall.ModelEnumCallStateObj.Code == 3 && !_isActiveCall) // Conversation
        //            {
        //                _isActiveCall = true;
        //                StartDurationTimer();
        //                OnEventViewModel("ActiveCall");
        //            }
                    
        //            if (CurrentModelCall == null || CurrentModelCall.ModelEnumCallStateObj.Code == 6) // Ended (почему то иногда прилетает null, видимо в разных потоках уже закрытый звонок удаляется из списка активных в бизнес логике быстрее чем прилетает данный колбек в этом потоке)
        //            {
        //                OnCloseView();
        //            }
        //        };

        //        CurrentDispatcher.BeginInvoke(action);
        //    }
        //}

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            //_dataSourceCall.DeleteDoCallbackListener(this);
            CallbackRouter.Instance.ModelCallChanged -= OnModelCallChanged;

            if (_timer != null)
            {
                _timer.Enabled = false;

                _timer.Elapsed -= RefreshDuration;

                _timer.Close();

                _timer = null;
            }

            ExistCall = false;
        }
    }
}
