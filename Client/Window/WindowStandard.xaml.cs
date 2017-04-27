using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using dodicall.View;

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowStandard.xaml
    /// </summary>
    public partial class WindowStandard : System.Windows.Window, IWindow
    {
        /// <summary> Состояние окна до сворачивания </summary>
        private WindowState _windowStateBeforeMinimized;

        /// <summary> Отображаемый UserControl по интерфейсу IUserControlCloseWindow </summary>
        private readonly IUserControlCloseWindow _iUserControlCloseWindow;

        /// <summary> Отображаемый UserControl </summary>
        public readonly UserControl ViewUserControl;

        /// <summary> Конструктор </summary>
        public WindowStandard(IUserControlCloseWindow iUserControlCloseWindow)
        {
            InitializeComponent();

            _iUserControlCloseWindow = iUserControlCloseWindow;

            _iUserControlCloseWindow.CloseWindow += (sender, args) => Close();

            var iWindowCaption = iUserControlCloseWindow as IWindowCaption;

            if (iWindowCaption != null)
            {
                this.SetResourceReference(TitleProperty, iWindowCaption.WindowCaptionResourceKey);
            }

            var iLockWindowProcess = iUserControlCloseWindow as ILockWindowProcess;

            if (iLockWindowProcess != null)
            {
                iLockWindowProcess.LockWindow += ViewOnLockWindow;
            }

            ViewUserControl = _iUserControlCloseWindow as UserControl;

            if (ViewUserControl != null) GridMain.Children.Add(ViewUserControl);

            _windowStateBeforeMinimized = WindowState;
        }

        /// <summary> Обработчик блокировки формы </summary>
        public void ViewOnLockWindow(object sender, bool lockResult)
        {
            if (lockResult)
            {
                LockWindow();
            }
            else
            {
                UnlockWindow();
            }
        }

        /// <summary> Обработчик закрытия формы </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_iUserControlCloseWindow.BeforeCloseWindow() == false)
            {
                e.Cancel = true;
            }
        }

        /// <summary> Обработчик изменения состояния окна </summary>
        private void WindowMain_OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState != WindowState.Minimized) _windowStateBeforeMinimized = WindowState;
        }

        /// <summary> Блокировка формы </summary>
        public void LockWindow()
        {
            ViewProcessMain.WaitingAnimationStart();
        }

        /// <summary> Разблокировка формы </summary>
        public void UnlockWindow()
        {
            ViewProcessMain.WaitingAnimationStop();
        }

        /// <summary> Показать окно </summary>
        public void ShowWindow()
        {
            Show();

            if (WindowState == WindowState.Minimized) WindowState = _windowStateBeforeMinimized;

            Activate();
        }
    }
}
