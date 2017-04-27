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
using DAL.Localization;

namespace DAL.Utility
{
    public class UtilityDate
    {
        /// <summary> Возвращает дату в строковом коротком формате с локализацией (сделано что бы выглядело как на iOS) </summary>
        public static string ConvertShortDateString(DateTime dateTime)
        {
            string result;

            var today = DateTime.Today;

            if (dateTime.Date == today)
            {
                result = dateTime.Hour + ":" + (dateTime.Minute < 10 ? "0" + dateTime.Minute : dateTime.Minute.ToString());
            }
            else
            {
                if (dateTime.Year == today.Year)
                {
                    result = (dateTime.Day < 10 ? "0" + dateTime.Day : dateTime.Day.ToString()) + " ";

                    switch (dateTime.Month)
                    {
                        case 1:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Jan");
                            break;
                        case 2:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Feb");
                            break;
                        case 3:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Mar");
                            break;
                        case 4:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Apr");
                            break;
                        case 5:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_May");
                            break;
                        case 6:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Jun");
                            break;
                        case 7:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Jul");
                            break;
                        case 8:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Aug");
                            break;
                        case 9:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Sep");
                            break;
                        case 10:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Oct");
                            break;
                        case 11:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Nov");
                            break;
                        case 12:
                            result += LocalizationApp.GetInstance().GetValueByKey("UtilityDate_Dec");
                            break;
                    }
                }
                else
                {
                    result = (dateTime.Month < 10 ? "0" + dateTime.Month : dateTime.Month.ToString()) + "." + dateTime.Year;
                }
            }

            return result;
        }
    }
}
