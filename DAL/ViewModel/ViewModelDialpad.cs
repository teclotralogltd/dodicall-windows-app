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
using DAL.Callback;
using DAL.Model;
using DAL.WrapperBridge;

namespace DAL.ViewModel
{
    public class ViewModelDialpad : AbstractViewModel //, IDoCallback
    {
        /// <summary> Номер телефона </summary>
        private string _phoneNumber = String.Empty;

        /// <summary> Номер телефона </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value.Length > _phoneNumber.Length && value.Length - _phoneNumber.Length == 1) DataSourceCall.StartSoundSignalShort(value.Last());

                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        /// <summary> Конструктор </summary>
        public ViewModelDialpad()
        {
            CallbackRouter.Instance.ModelCallChanged += OnModelCallChanged;
        }

        /// <summary> Обработчик изменения звонка </summary>
        private void OnModelCallChanged(object sender, ModelCall modelCall)
        {
            // хз какой брать currentModelCall.ModelEnumCallStateObj.Code потому что СВ в отпуске, а в дебаггере приходит Ringing, наверно его надо брать
            if (modelCall != null && modelCall.ModelEnumCallDirectionObj.Code == 1 /* Incoming */ && modelCall.ModelEnumCallStateObj.Code == 2 /* Ringing */)
            {
                Action action = () => OnEventViewModel("IncomingCall");

                CurrentDispatcher.BeginInvoke(action);
            }

            // Ended (почему то иногда прилетает null, видимо в разных потоках уже закрытый звонок удаляется из списка активных в бизнес логике быстрее чем прилетает данный колбек в этом потоке)
            if (modelCall == null || modelCall.ModelEnumCallStateObj.Code == 6)
            {
                OnEventViewModel("CallEnableChanged", true);
            }
            else
            {
                OnEventViewModel("CallEnableChanged", false);
            }
        }

        /// <summary> Начать тоновый сигнал </summary>
        public void StartSoundSignal(char charSignal)
        {
            DataSourceCall.StartSoundSignal(charSignal);
        }

        /// <summary> Закончить тоновый сигнал </summary>
        public void StopSoundSignal()
        {
            DataSourceCall.StopSoundSignal();
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }
    }
}
