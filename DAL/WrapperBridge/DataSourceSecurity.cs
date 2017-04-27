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
using System.Security;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Utility;

namespace DAL.WrapperBridge
{
    internal class DataSourceSecurity : AbstractDataSource
    {
        /// <summary> Возвращает ключ шифрования залогиненого пользователя </summary>
        public static SecureString GetUserSecretKey()
        {
            return Logic.GetUserKeys();
        }

        /// <summary> Возвращает ключ шифрования залогиненого пользователя для экспорта </summary>
        public static SecureString GetUserPrivateKey()
        {
             return Logic.GetUserPrivateKey();
        }

        /// <summary> Импортировать ключ шифрования </summary>
        public static bool ImportUserPrivateKey(SecureString key)
        {
            return Logic.ImportUserPrivateKey(key);
        }

        /// <summary> Удалить ключ шифрования </summary>
        public static bool RemoveUserKey()
        {
            return Logic.RemoveUserKey();
        }

        /// <summary> Перегенерировать ключ шифрования </summary>
        public static bool RegenerateKeyPair()
        {
            return Logic.RegenerateKeyPair();
        }

        /// <summary> Проверка пароля </summary>
        public static bool CheckPassword(string password)
        {
            return CheckPassword(UtilitySecurity.ToSecureString(password));
        }

        /// <summary> Проверка пароля </summary>
        public static bool CheckPassword(SecureString password)
        {
            var modelLogin = DataSourceLogin.GetLastModelLogin();
            var login = modelLogin.Login;

            var secureString = UtilitySecurity.LoadPasswordFromIsolatedStorage(login);

            return UtilitySecurity.IsEqual(secureString, password);
        }
    }
}
