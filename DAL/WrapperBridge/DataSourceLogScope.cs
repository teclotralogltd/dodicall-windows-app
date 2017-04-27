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
using dodicall;
using DAL.Abstract;
using DAL.Model;

namespace DAL.WrapperBridge
{
    public class DataSourceLogScope : AbstractDataSource
    {
        /// <summary> Получить лог чата </summary>
        public static string[] GetChatLog()
        {
            return Logic.GetChatLog();
        }

        /// <summary> Получить лог базы данных </summary>
        public static string[] GetDatabaseLog()
        {
            return Logic.GetDatabaseLog();
        }

        /// <summary> Получить лог запросов </summary>
        public static string[] GetRequestsLog()
        {
            return Logic.GetRequestsLog();
        }

        /// <summary> Получить лог телефонии </summary>
        public static string[] GetVoipLog()
        {
            return Logic.GetVoipLog();
        }

        /// <summary> Получить лог качества звонков </summary>
        public static string[] GetCallQualityLog()
        {
            return Logic.GetCallQualityLog();
        }

        /// <summary> Получить лог истории звонков </summary>
        public static string[] GetCallHistoryLog()
        {
            return Logic.GetCallHistoryLog();
        }

        /// <summary> Получить лог приложения </summary>
        public static string[] GetGuiLog()
        {
            return Logic.GetGuiLog();
        }

        /// <summary> Очистить логи </summary>
        public static void ClearLogs()
        {
            Logic.ClearLogs();
        }

        /// <summary> Отправить сообщение об ошибке </summary>
        public static bool SendTroubleTicket(ModelLogScope modelLogScope)
        {
            if (modelLogScope == null) return false;

            var logScopeManaged = new LogScopeManaged
            {
                ChatLog = modelLogScope.LogChat,
                DatabaseLog = modelLogScope.LogDatabase,
                RequestsLog = modelLogScope.LogRequest,
                VoipLog = modelLogScope.LogVoip,
                TraceLog = modelLogScope.LogTrace,
                GuiLog = modelLogScope.LogGui,
                CallHistoryLog = modelLogScope.LogHistoryCall,
                CallQualityLog = modelLogScope.LogQualityCall
            };

            var troubleTicketResult = Logic.SendTroubleTicket(modelLogScope.Subject, modelLogScope.Description, logScopeManaged);

            if (troubleTicketResult.Success) modelLogScope.IssueId = troubleTicketResult.IssueId;

            return troubleTicketResult.Success;
        }

        /// <summary> Записать в лог информацию об ошибке </summary>
        public static void WriteExceptionToLog(Exception exception)
        {
            var exceptionString = "\n---------------------------------------------------------------------------------------\n";
            exceptionString += "Method generate exception:\n" + exception.TargetSite.Name + "\n";
            exceptionString += "Message:\n" + exception.Message + "\n";
            exceptionString += "Source:\n" + exception.Source + "\n";
            exceptionString += "StackTrace:\n" + exception.StackTrace + "\n";

            Logic.WriteGuiLog(LogLevelManaged.Error, exceptionString);
        }
    }
}
