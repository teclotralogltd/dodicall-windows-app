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
using System.Windows;
using dodicall.View;
using DAL.Abstract;

namespace dodicall.Window
{
    public class ViewModelMain : AbstractNotifyPropertyChanged
    {
        /// <summary> Команда открытия настроек пользователя </summary>
        public Command CommandOpenUserSettings { get; set; }

        /// <summary> Команда открытия  о программе </summary>
        public Command CommandOpenAbout { get; set; }

        /// <summary> Команда создания сохраненного контакта </summary>
        public Command CommandCreateManualContact { get; set; }

        /// <summary> Команда создания мульти чата </summary>
        public Command CommandCreateMultiChat { get; set; }

        /// <summary> Конструктор </summary>
        public ViewModelMain()
        {
            CommandOpenUserSettings = new Command(obj => OpenUserSettings());

            CommandOpenAbout = new Command(obj => OpenAbout());

            CommandCreateManualContact = new Command(obj => CreateManualContact());
            CommandCreateMultiChat = new Command(obj => CreateMultiChat());
        }

        /// <summary> Метод открытия настроек пользователя </summary>
        private void OpenUserSettings()
        {
            var windowUserSettings = FactoryWindow.GetWindowUserSettings();

            windowUserSettings.Show();

            windowUserSettings.Activate(); // потому что окно может быть уже открыто => нужно вывести его на первый план
        }

        /// <summary> Метод открытия о программе </summary>
        private void OpenAbout()
        {
            WindowInformation.ShowAbout();
        }

        /// <summary> Метод создания сохраненного контакта </summary>
        private void CreateManualContact()
        {
            var windowManualContact = FactoryWindow.GetWindowManualContact();

            windowManualContact.Owner = WindowMain.CurrentMainWindow;

            windowManualContact.ShowDialog();
        }

        /// <summary> Метод создания мульти чата </summary>
        private void CreateMultiChat()
        {
            var listModelContact = ViewSelectionContact.ShowSelectionContact();

            if (listModelContact.Count > 0)
            {
                var viewChatMessageDetail = new ViewChatMessageDetail(listModelContact);

                WindowMain.ShowInRightWorkspace(viewChatMessageDetail);

                ViewContact.CurrentViewContact.OpenChat(viewChatMessageDetail.CurrentModelChat);
            }
        }
    }
}
