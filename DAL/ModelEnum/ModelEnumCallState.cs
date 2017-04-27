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

using System.Collections.Generic;
using System.Linq;
using DAL.Abstract;

namespace DAL.ModelEnum
{
    public class ModelEnumCallState : AbstractModelEnum<ModelEnumCallState>
    {
        ///// <summary> Список типов сообщений чатов </summary>
        //private static List<ModelEnumCallState> _listModelEnumCallState;

        ///// <summary> Список типов сообщений чатов </summary>
        //public static List<ModelEnumCallState> ListModelEnumCallState
        //{
        //    get
        //    {
        //        if (_listModelEnumCallState == null)
        //        {
        //            _listModelEnumCallState = new List<ModelEnumCallState>
        //            {
        //                new ModelEnumCallState {Code = 0, _keyName = @"ModelEnumCallState_CallStateInitialized", _keyDescription = @"ModelEnumCallState_CallStateInitialized"}, // исходящий звонок инициирован (только нажата кнопка позвонить, гудков еще нет)
        //                new ModelEnumCallState {Code = 1, _keyName = @"ModelEnumCallState_CallStateDialing", _keyDescription = @"ModelEnumCallState_CallStateDialing"}, // тоже самое что и выше (временно тоже самое для GUI) (пошел запрос на соединение)
        //                new ModelEnumCallState {Code = 2, _keyName = @"ModelEnumCallState_CallStateRinging", _keyDescription = @"ModelEnumCallState_CallStateRinging"}, // начались гудки
        //                new ModelEnumCallState {Code = 3, _keyName = @"ModelEnumCallState_CallStateConversation", _keyDescription = @"ModelEnumCallState_CallStateConversation"}, // снял трубку и пошел разговор
        //                new ModelEnumCallState {Code = 4, _keyName = @"ModelEnumCallState_CallStateEarlyMedia", _keyDescription = @"ModelEnumCallState_CallStateEarlyMedia"}, // автоответчик после CallStateDialing если абонент недоступен
        //                new ModelEnumCallState {Code = 5, _keyName = @"ModelEnumCallState_CallStatePaused", _keyDescription = @"ModelEnumCallState_CallStatePaused"}, // звонок поставлен на паузу
        //                new ModelEnumCallState {Code = 6, _keyName = @"ModelEnumCallState_CallStateEnded", _keyDescription = @"ModelEnumCallState_CallStateEnded"} // звонок завершен
        //            };
        //        }

        //        return _listModelEnumCallState;
        //    }
        //}

        ///// <summary> Получить GetModelEnumCallState по коду </summary>
        //public static ModelEnumCallState GetModelEnumCallState(int code)
        //{
        //    return ListModelEnumCallState.FirstOrDefault(obj => obj.Code == code);
        //}

        /// <summary> Инициализация списка </summary>
        public static void Initialize() // этот метод вызывать руками в общем класссе в методе инициализации всех ModelEnumCommon перед стартом приложения
        {
            ListModelEnum = new List<ModelEnumCallState>
            {
                new ModelEnumCallState {Code = 0, _keyName = @"ModelEnumCallState_CallStateInitialized", _keyDescription = @"ModelEnumCallState_CallStateInitialized"}, // исходящий звонок инициирован (только нажата кнопка позвонить, гудков еще нет)
                new ModelEnumCallState {Code = 1, _keyName = @"ModelEnumCallState_CallStateDialing", _keyDescription = @"ModelEnumCallState_CallStateDialing"}, // тоже самое что и выше (временно тоже самое для GUI) (пошел запрос на соединение)
                new ModelEnumCallState {Code = 2, _keyName = @"ModelEnumCallState_CallStateRinging", _keyDescription = @"ModelEnumCallState_CallStateRinging"}, // начались гудки
                new ModelEnumCallState {Code = 3, _keyName = @"ModelEnumCallState_CallStateConversation", _keyDescription = @"ModelEnumCallState_CallStateConversation"}, // снял трубку и пошел разговор
                new ModelEnumCallState {Code = 4, _keyName = @"ModelEnumCallState_CallStateEarlyMedia", _keyDescription = @"ModelEnumCallState_CallStateEarlyMedia"}, // автоответчик после CallStateDialing если абонент недоступен
                new ModelEnumCallState {Code = 5, _keyName = @"ModelEnumCallState_CallStatePaused", _keyDescription = @"ModelEnumCallState_CallStatePaused"}, // звонок поставлен на паузу
                new ModelEnumCallState {Code = 6, _keyName = @"ModelEnumCallState_CallStateEnded", _keyDescription = @"ModelEnumCallState_CallStateEnded"} // звонок завершен
            };
        }

        /// <summary> Конструктор </summary>
        private ModelEnumCallState()
        {
            // пустой для сокрытия создания объектов
        }
    }
}
