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
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;

namespace DAL.Utility
{
    [IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByRoamingUser)]
    public class UtilitySecurity
    {
        /// <summary> Сохранение ключа шифрования в изолирвоанном хранилище в XML файле в защищенном(DPAPI) виде </summary> 
        public static bool SavePrivateCryptKeyToIsolatedStorage(SecureString cryptkey, string userlogin, string serverArea)
        {
            var result = false;
            try
            {
                var document = new XmlDocument();
                var loadExisting = false;
                var protectedCryptKey = Protect(cryptkey, userlogin);

                using (var isolatedStream = new IsolatedStorageFileStream("AssemblyData.xml", FileMode.OpenOrCreate, IsolatedStorageFile.GetUserStoreForAssembly()))
                {
                    if (isolatedStream.Length != 0)
                    {
                        var readerSettings = new XmlReaderSettings();
                        using (var reader = XmlReader.Create(isolatedStream, readerSettings))
                        {
                            try
                            {
                                document.Load(reader);
                                loadExisting = true;
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                if (loadExisting)
                {
                    var path = "/root/userid[@userid='{0}'and @serverArea='{1}']";
                    path = string.Format(path, userlogin, serverArea);
                    var userNode = document.SelectSingleNode(path);

                    if (userNode != null)
                    {
                        userNode.Attributes["cryptkey"].Value = protectedCryptKey;
                    }
                    else
                    {
                        var rootNode = document.ChildNodes[1];
                        var userNodeElement = document.CreateElement("userid");
                        userNodeElement.SetAttribute("userid", userlogin);
                        userNodeElement.SetAttribute("cryptkey", protectedCryptKey);
                        userNodeElement.SetAttribute("serverArea", serverArea);
                        rootNode.AppendChild(userNodeElement);
                    }
                }
                else
                {
                    var docNode = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                    document.AppendChild(docNode);
                    var rootNode = document.CreateElement("root");
                    var userNode = document.CreateElement("userid");
                    userNode.SetAttribute("userid", userlogin);
                    userNode.SetAttribute("cryptkey", protectedCryptKey);
                    userNode.SetAttribute("serverArea", serverArea);
                    rootNode.AppendChild(userNode);
                    document.AppendChild(rootNode);
                }

                using (var isolatedStream = new IsolatedStorageFileStream("AssemblyData.xml", FileMode.Truncate, IsolatedStorageFile.GetUserStoreForAssembly()))
                {
                    document.Save(isolatedStream);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            
            return result;
        }

        /// <summary> Загрузка ключа шифрования из изолирвоанного хранилища </summary>
        public static SecureString LoadPrivateCryptKeyFromIsolatedStorage(string userlogin, string serverArea)
        {
            var result = new SecureString();
            try
            {
                var isolatedStore = IsolatedStorageFile.GetUserStoreForAssembly();

                if (isolatedStore.FileExists("AssemblyData.xml"))
                {
                    using (var isolatedStream = new IsolatedStorageFileStream("AssemblyData.xml", FileMode.Open, isolatedStore))
                    {
                        if (isolatedStream.Length != 0)
                        {
                            var readerSettings = new XmlReaderSettings();
                            using (var reader = XmlReader.Create(isolatedStream, readerSettings))
                            {
                                var document = new XmlDocument();
                                document.Load(reader);
                                var path = "/root/userid[@userid='{0}'and @serverArea='{1}']";
                                path = string.Format(path, userlogin, serverArea);
                                var userNode = document.SelectSingleNode(path);

                                if (userNode != null)
                                {
                                    var protectedKey = userNode.Attributes["cryptkey"].Value;

                                    result = Unprotect(protectedKey, userlogin);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                result = new SecureString();
            } 
            return result;
        }

        /// <summary> Сохранение строки в изолирвоанном хранилище </summary>
        public static bool SavePasswordToIsolatedStorage(SecureString password, string userlogin)
        { 
            var result = false;

            try
            {
                using (var s = new IsolatedStorageFileStream("AssemblyData", FileMode.Create, IsolatedStorageFile.GetUserStoreForAssembly()))
                { 
                    using (var sw = new StreamWriter(s))
                    {
                        sw.Write(Protect(password, userlogin));
                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        } 

        /// <summary> Загрузка строки из изолирвоанного хранилища </summary>
        public static SecureString LoadPasswordFromIsolatedStorage(string userlogin)
        {
            SecureString result;

            try
            {
                using (var s = new IsolatedStorageFileStream("AssemblyData", FileMode.Open, IsolatedStorageFile.GetUserStoreForAssembly()))
                { 
                    using (var sr = new StreamReader(s))
                    {
                        result = Unprotect(sr.ReadLine(), userlogin);
                    }
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary> Шифрование SecureString методом .NET ProtectData </summary>
        /// <param name="optionalEntropy">передавать логин текущего пользователя для лучшей безопасности</param>  
        public static string Protect(SecureString clearText, string optionalEntropy, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (clearText == null) throw new ArgumentNullException("clearText");

            var entropyBytes = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.UTF8.GetBytes(optionalEntropy);
            var encryptedBytes = ProtectedData.Protect(Encoding.UTF8.GetBytes(ConvertToString(clearText)), entropyBytes, scope);

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary> Расшифровывание строки методом .NET ProtectData </summary>
        /// <param name="optionalEntropy">передавать логин текущего пользователя для лучшей безопасности</param>  
        public static SecureString Unprotect(string encryptedText, string optionalEntropy, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (encryptedText == null) throw new ArgumentNullException("encryptedText");

            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var entropyBytes = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.UTF8.GetBytes(optionalEntropy);

            return ToSecureString(Encoding.UTF8.GetString(ProtectedData.Unprotect(encryptedBytes, entropyBytes, scope)));
        }

        /// <summary> Конвертирование строки в SecureString </summary>
        public static SecureString ToSecureString(string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return null;
            
            var result = new SecureString();

            foreach (char c in source)
            {
                result.AppendChar(c);
            }

            return result;
        }

        /// <summary> Конвертирование SecureString в строку </summary>
        public static string ConvertToString(SecureString value)
        {
            if (value == null) return String.Empty;

            var valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);

                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary> Сравнение двух экземпляров SecureString </summary>
        public static bool IsEqual(SecureString ss1, SecureString ss2)
        {
            var bstr1 = IntPtr.Zero;
            var bstr2 = IntPtr.Zero;

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                var length1 = Marshal.ReadInt32(bstr1, -4);
                var length2 = Marshal.ReadInt32(bstr2, -4);

                if (length1 == length2)
                {
                    for (var x = 0; x < length1; ++x)
                    {
                        var b1 = Marshal.ReadByte(bstr1, x);
                        var b2 = Marshal.ReadByte(bstr2, x);

                        if (b1 != b2) return false;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch 
            {
                return false;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }
    } 
}
