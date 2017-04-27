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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace dodicall.Window
{
    public static class FlashWindow
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FlashInfo flashInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct FlashInfo
        {
            /// <summary> The size of the structure in bytes </summary>
            public uint Size;

            /// <summary> A Handle to the Window to be Flashed. The window can be either opened or minimized </summary>
            public IntPtr Hwnd;

            /// <summary> The Flash Status </summary>
            public uint Flags;

            /// <summary> The number of times to Flash the window </summary>
            public uint Count;

            /// <summary> The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate </summary>
            public uint Timeout;
        }

        /// <summary> Подсветить окно на панели задач пока окно не получит фокус </summary>
        public static void Flash(System.Windows.Window window)
        {
            var flashInfo = new FlashInfo
            {
                Hwnd = new WindowInteropHelper(window).Handle,
                Flags = 15,
                Count = uint.MaxValue,
                Timeout = 0
            };
            flashInfo.Size = Convert.ToUInt32(Marshal.SizeOf(flashInfo));

            FlashWindowEx(ref flashInfo);
        }
    }
}
