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
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace DAL.Utility
{
    public class UtilityPicture
    {
        /// <summary> Возвращает BitmapImage из byte[] </summary>
        public static BitmapImage GetBitmapImageFromByteArray(byte[] arrayByte)
        {
            BitmapImage bitmapImage = null;

            if (arrayByte == null) return null;

            using (var ms = new MemoryStream(arrayByte))
            {
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        /// <summary> Возвращает byte[] из BitmapImage </summary>
        public static byte[] GetByteArrayFromBitmapImage(BitmapImage bitmapImage)
        {
            byte[] arrayByte = null;

            if (bitmapImage == null) return null;

            var memoryStream = new MemoryStream();
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(memoryStream);
            arrayByte = memoryStream.GetBuffer();

            return arrayByte;
        }

        /// <summary> Возвращает строку с разрешением из BitmapImage </summary>
        public static string GetStringResolutionFromBitmapImage(BitmapImage bitmapImage)
        {
            var result = String.Empty;

            try
            {
                result = bitmapImage.Height + "x" + bitmapImage.Width;
            }
            catch
            {
                result = @"No image";
            }

            return result;
        }

        /// <summary> Возвращает BitmapImage из StringUri (только для приложений WPF) </summary>
        public static BitmapImage GetBitmapImageFromStringUri(string uri)
        {
            return new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        /// <summary> Возвращает BitmapImage из StringUri (только для приложений WPF) (для доступа к ресурсам из кода) </summary>
        public static BitmapImage GetBitmapImageFromStringUriInternalUse(string uri)
        {
            return new BitmapImage(new Uri(@"pack://application:,,," + uri, UriKind.RelativeOrAbsolute));
        }

        /// <summary> Возвращает BitmapImage из StringPathAssembly (Build Action картинки должен быть Embedded Resource) </summary>
        public static BitmapImage GetBitmapImageFromStringPathAssembly(string pathAssembly)
        {
            var result = new BitmapImage();

            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(pathAssembly))
            {
                if (stream == null) throw new InvalidOperationException("Не удалось найти картинку в ресурсах сборки.");

                result.BeginInit();
                result.StreamSource = stream;
                result.EndInit();
            }

            return result;
        }
    }
}
