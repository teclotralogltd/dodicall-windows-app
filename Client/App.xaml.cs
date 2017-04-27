using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using dodicall.Localization;
using dodicall.Window;
using DAL.Abstract;
using DAL.Localization;
using DAL.ModelEnum;
using DAL.WrapperBridge;

namespace dodicall
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary> Объект Mutex (для запрета повторного запуска приложения) </summary>
        public static Mutex Mutex;

        /// <summary> Обработчик запуска приложение </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // подписка на необрабатываемые исключения в приложении
            DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);

            // подписка на необрабатываемые исключения в приложении
            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            // Инициализация всех ModelEnum в главном потоке, в котором стартует GUI
            CommonModelEnum.InitializeModelEnum();

            // привязка смены языка интерфейса к смене языка в приложении
            LocalizationApp.GetInstance().LocalizationChanged += LocalizationUI.ChangeLanguage;

            bool success;

            var userName = WindowsIdentity.GetCurrent().Name.Replace(@"\", "-");

            Mutex = new Mutex(true, "DCBF3DE8-032E-4DCA-8158-AE0CF30A15AC" /*userName*/, out success);

            if (!success)
            {
                var allProcess = System.Diagnostics.Process.GetProcesses();
                var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                foreach (var process in allProcess)
                {
                    if (currentProcess.ProcessName == process.ProcessName && currentProcess.Id != process.Id)
                    {
                        var hWndStartup = FindWindow(null, "DCBF3DE8-032E-4DCA-8158-AE0CF30A15AC" + userName);

                        if (hWndStartup != IntPtr.Zero)
                        {
                            ShowWindow(hWndStartup, 1);
                            SetForegroundWindow(hWndStartup);
                        }
                        else
                        {
                            WindowMessageBox.Show("Application is run");
                        }
                    }
                }

                Environment.Exit(0);
            }
        }

        /// <summary> Обработчик необрабатываемых исключений </summary>
        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledExceptionCatch((Exception)e.ExceptionObject);
        }

        /// <summary> Обработчик необрабатываемых исключений </summary>
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            UnhandledExceptionCatch(e.Exception);

            e.Handled = true;
        }

        /// <summary> Вывод на экран исключений и логирование </summary>
        private void UnhandledExceptionCatch(Exception exception)
        {
            DataSourceLogScope.WriteExceptionToLog(exception);
            WindowException.ShowException(exception);

            if (exception.InnerException != null)
            {
                DataSourceLogScope.WriteExceptionToLog(exception.InnerException);
                WindowException.ShowException(exception.InnerException);
            }
        }

        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);
    }
}
