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
using DAL.ModelEnum;

namespace DAL.Callback
{
    internal class PackageModelContactStatus
    {
        /// <summary> XmppId контакта </summary>
        public string XmppId;

        /// <summary> Статус контакта </summary>
        public ModelEnumUserBaseStatus ModelEnumUserBaseStatusObj;

        /// <summary> Расширенный статус контакта </summary>
        public string UserExtendedStatus;
    }
}
