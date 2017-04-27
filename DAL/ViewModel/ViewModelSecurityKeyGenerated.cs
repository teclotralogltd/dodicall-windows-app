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
using DAL.Model;
using DAL.ModelEnum;
using DAL.WrapperBridge;
using DAL.Utility;
using System.Security;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using DAL.Callback;

namespace DAL.ViewModel
{
    public class ViewModelSecurityKeyGenerated : AbstractViewModel, IDisposable
    {   
        /// <summary> Текущий ключ шифрования </summary>
        private SecureString _currentUserCryptKey;

        /// <summary> Конструктор </summary>
        public ViewModelSecurityKeyGenerated()
        { 
            CallbackRouter.Instance.SecretKeyGenerated += OnSecretKeyGenerated;

            // кусок из старого DoCallback, видимо нужная заметка для будущей реализации, по этому оставил на всякий случай
            //
            //        if (e.EntityIds.Contains("Needed"))
            //        {
            //            //TODO
            //            //3.2.Если entityIds = ["Needed"], следует проинформировать пользователя о том, 
            //            //что требуется произвести импорт на устройство секретного ключа пользователя, или выполнить перегенерирование ключа.
            //        }
        }

        /// <summary> Обработчик генерации секретного ключа </summary>
        private void OnSecretKeyGenerated(string login, int serverAreaCode, SecureString secretKey)
        {
            var saveResult = false;

            _currentUserCryptKey = secretKey;

            if (_currentUserCryptKey != null)
            {
                saveResult = UtilitySecurity.SavePrivateCryptKeyToIsolatedStorage(_currentUserCryptKey, login, serverAreaCode.ToString());
            }

            if (saveResult)
            {
                Action action = () =>
                {
                    OnEventViewModel("SecretKeyGenerated");
                };

                CurrentDispatcher.BeginInvoke(action); 
            }
        }

        /// <summary> Возвращает ключ шифрования залогиненого пользователя для экспорта </summary>
        public string GetUserPrivateKey()
        {
            return UtilitySecurity.ConvertToString(DataSourceSecurity.GetUserPrivateKey()); 
        } 

        /// <summary> Обработчик изменения языка приложения </summary>
        protected override void OnChangeLocalizationApp()
        {

        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            CallbackRouter.Instance.SecretKeyGenerated -= OnSecretKeyGenerated;
        }
    }
}
