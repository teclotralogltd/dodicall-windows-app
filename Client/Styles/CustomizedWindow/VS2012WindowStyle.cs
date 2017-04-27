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
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using dodicall.Window;

namespace dodicall.Styles.CustomizedWindow
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromChild(this object childDependencyObject, Action<System.Windows.Window> action)
        {
            var element = childDependencyObject as DependencyObject;
            while (element != null)
            {
                element = VisualTreeHelper.GetParent(element);
                if (element is System.Windows.Window) { action(element as System.Windows.Window); break; }
            }
        }

        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<System.Windows.Window> action)
        {
            System.Windows.Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as System.Windows.Window;
            if (window != null) action(window);
        }

        public static IntPtr GetWindowHandle(this System.Windows.Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return helper.Handle;
        }
    }

    public partial class VS2012WindowStyle
    {
        #region sizing event handlers

        void OnSizeSouth(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.South); }
        void OnSizeNorth(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.North); }
        void OnSizeEast(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.East); }
        void OnSizeWest(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.West); }
        void OnSizeNorthWest(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.NorthWest); }
        void OnSizeNorthEast(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.NorthEast); }
        void OnSizeSouthEast(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.SouthEast); }
        void OnSizeSouthWest(object sender, MouseButtonEventArgs e) { OnSize(sender, SizingAction.SouthWest); }

        void OnSize(object sender, SizingAction action)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                sender.ForWindowFromTemplate(w =>
                    {
                        if (w.WindowState == WindowState.Normal && w.ResizeMode != ResizeMode.NoResize)
                            DragSize(w.GetWindowHandle(), action);
                    });
            }
        }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Window)sender).StateChanged += WindowStateChanged;
        }

        

        void IconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ClickCount > 1)
            //{
            //    sender.ForWindowFromTemplate(w => w.Close());
            //}
            //else
            //{
            //    sender.ForWindowFromTemplate(w =>
            //        SendMessage(w.GetWindowHandle(), WM_SYSCOMMAND, (IntPtr)SC_KEYMENU, (IntPtr)' '));
            //}
        }

        void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.Close());
        }

        void HideButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => Hide(w));
        }

        void Hide(System.Windows.Window window)
        {
            var windowMain = window as WindowMain;

            if (windowMain != null)
            {
                windowMain.HideWindow();
            }
            else
            {
                window.Hide();
            }
        }

        void MinButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.WindowState = WindowState.Minimized);
        }

        void MaxButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.WindowState = (w.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized);
        }

        void TitleBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                MaxButtonClick(sender, e);
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                sender.ForWindowFromTemplate(w => w.DragMove());
            }
        }

        void TitleBarMouseLeftButtonDownWithoutMax(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                sender.ForWindowFromTemplate(w => w.DragMove());
            }
        }

        void TitleBarMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                sender.ForWindowFromTemplate(w =>
                    {
                        if (w.WindowState == WindowState.Maximized)
                        {
                            w.BeginInit();
                            double adjustment = 40.0;
                            var mouse1 = e.MouseDevice.GetPosition(w);
                            var width1 = Math.Max(w.ActualWidth - 2 * adjustment, adjustment);
                            w.WindowState = WindowState.Normal;
                            var width2 = Math.Max(w.ActualWidth - 2 * adjustment, adjustment);
                            w.Left = (mouse1.X - adjustment) * (1 - width2 / width1);
                            w.Top = -7;
                            w.EndInit();
                            w.DragMove();
                        }
                    });
            }
        }

        private Thickness _containerBorderPadding;
        private Thickness _containerBorderBorderThickness;

        void WindowStateChanged(object sender, EventArgs e)
        {
            var w = ((System.Windows.Window)sender);
            var handle = w.GetWindowHandle();
            var containerBorder = (Border)w.Template.FindName("PART_Border", w);

            if (w.WindowState == WindowState.Maximized)
            {
                // Make sure window doesn't overlap with the taskbar.
                var screen = System.Windows.Forms.Screen.FromHandle(handle);
                if (screen.Primary)
                {
                    _containerBorderPadding = containerBorder.Padding;
                    _containerBorderBorderThickness = containerBorder.BorderThickness;

                    containerBorder.BorderThickness = new Thickness(0);
                    containerBorder.Padding = new Thickness(
                        SystemParameters.WorkArea.Left,
                        SystemParameters.WorkArea.Top,
                        SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right,
                        SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom);
                }
            }
            else
            {
                containerBorder.Padding = _containerBorderPadding;
                containerBorder.BorderThickness = _containerBorderBorderThickness;
            }
        }

        #endregion

        #region P/Invoke

        const int WM_SYSCOMMAND = 0x112;
        const int SC_SIZE = 0xF000;
        const int SC_KEYMENU = 0xF100;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        void DragSize(IntPtr handle, SizingAction sizingAction)
        {
            SendMessage(handle, WM_SYSCOMMAND, (IntPtr)(SC_SIZE + sizingAction), IntPtr.Zero);
            SendMessage(handle, 514, IntPtr.Zero, IntPtr.Zero);
        }

        public enum SizingAction
        {
            North = 3,
            South = 6,
            East = 2,
            West = 1,
            NorthEast = 5,
            NorthWest = 4,
            SouthEast = 8,
            SouthWest = 7
        }

        #endregion
    }
}