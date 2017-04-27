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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using dodicall.Window;
using DAL.Enum;
using DAL.Utility;
using DAL.ViewModel;

namespace dodicall.Notification
{
    public class PushNotification
    {
        ///<summary> Иконка главного окна без нотификация из ресурсов </summary>
        private static BitmapImage _bitmapImageIcon = UtilityPicture.GetBitmapImageFromStringUriInternalUse("/Resources/ddcall_icon32.png");

        ///<summary> Иконка главного окна без нотификация из ресурсов приложения </summary>
        private static Bitmap _bitmapIcon = UtilitySystem.GetVersionOS() == EnumVersionOS.Window10 ? Properties.Resources.ddcall_icon24.ToBitmap() : Properties.Resources.ddcall_icon32;

        ///<summary> Размер шрифта </summary>
        private static float _fontSize = UtilitySystem.GetVersionOS() == EnumVersionOS.Window10 ? 8 : 9;

        ///<summary> Координата Y текста </summary>
        private static float _y = UtilitySystem.GetVersionOS() == EnumVersionOS.Window10 ? 2 : 3;

        ///<summary> Флаг проигрывания звуковой нотификации </summary>
        private static bool _soundNotificationPlaying;

        ///<summary> Текущее кол-во нотификаций </summary>
        private static int _countNotification;

        ///<summary> Звуковой проигрыватель нотификации </summary>
        private static SoundPlayer _soundPlayer = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + @"sounds\Notification.wav");

        ///<summary> Установить нотификацию на иконке главного окна </summary>
        public static void IconNotification(int countNotification)
        {
            if (countNotification > 0)
            {
                if (countNotification == _countNotification) return;

                if (countNotification > _countNotification && !ViewModelCallActive.ExistCall && !_soundNotificationPlaying)
                {
                    _soundNotificationPlaying = true;

                    new Task(PlaySoundNotification).Start();
                }

                _countNotification = countNotification;

                var bitmap = (Bitmap)_bitmapIcon.Clone();

                var graphics = Graphics.FromImage(bitmap);

                var text = countNotification > 99 ? "99" : countNotification.ToString();

                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

                var stringFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

                graphics.DrawString(text, new Font("Segoe UI", _fontSize, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.White, new RectangleF(0, _y, 20, 13), stringFormat);

                var memoryStream = new MemoryStream();

                if (UtilitySystem.GetVersionOS() == EnumVersionOS.Window10)
                {
                    var hicon = bitmap.GetHicon();
                    var newIcon = Icon.FromHandle(hicon);

                    newIcon.Save(memoryStream);

                    WindowMain.CurrentMainWindow.Icon = BitmapFrame.Create(memoryStream);
                }
                else
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    var result = new BitmapImage();

                    result.BeginInit();

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    result.StreamSource = memoryStream;

                    result.EndInit();

                    WindowMain.CurrentMainWindow.Icon = result;
                }
            }
            else
            {
                if (UtilitySystem.GetVersionOS() == EnumVersionOS.Window10)
                {
                    var memoryStream = new MemoryStream();

                    var hicon = _bitmapIcon.GetHicon();
                    var newIcon = Icon.FromHandle(hicon);

                    newIcon.Save(memoryStream);

                    WindowMain.CurrentMainWindow.Icon = BitmapFrame.Create(memoryStream);
                }
                else
                {
                    WindowMain.CurrentMainWindow.Icon = _bitmapImageIcon;
                }
            }
        }

        ///<summary> Проиграть звуковую нотификацию </summary>
        private static void PlaySoundNotification()
        {
            _soundPlayer.PlaySync();

            _soundNotificationPlaying = false;
        }
    }
}
