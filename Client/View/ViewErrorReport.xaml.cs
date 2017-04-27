using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dodicall.Enum;
using dodicall.Window;
using DAL.Model;
using DAL.WrapperBridge;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewErrorReport.xaml
    /// </summary>
    public partial class ViewErrorReport : UserControl, ILockWindowProcess, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewErrorReport_Title";

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Событие блокировки окна </summary>
        public event EventHandler<bool> LockWindow;

        /// <summary> Объект потока для асинхронной отправки отчета об ошибке </summary>
        private Thread _thread;

        /// <summary> Объект ModelLogScope (источник данных) </summary>
        private ModelLogScope _modelLogScope = new ModelLogScope();

        /// <summary> Конструктор </summary>
        public ViewErrorReport()
        {
            InitializeComponent();

            DataContext = _modelLogScope;

            _thread = new Thread(SendTroubleTicket);

            _modelLogScope.PropertyChanged += (sender, args) => ModelLogScopeOnPropertyChanged();

            ModelLogScopeOnPropertyChanged();
        }

        /// <summary> Обработчик изменения ModelLogScope </summary>
        private void ModelLogScopeOnPropertyChanged()
        {
            if (String.IsNullOrWhiteSpace(_modelLogScope.Subject) &&
                String.IsNullOrWhiteSpace(_modelLogScope.Description) &&
                !_modelLogScope.LogChat && !_modelLogScope.LogDatabase &&
                !_modelLogScope.LogRequest && !_modelLogScope.LogVoip &&
                !_modelLogScope.LogHistoryCall && !_modelLogScope.LogQualityCall &&
                !_modelLogScope.LogTrace && !_modelLogScope.LogGui)
            {
                ButtonSend.IsEnabled = false;
            }
            else
            {
                ButtonSend.IsEnabled = true;
            }
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Действие перед закрытием окна </summary>
        public bool BeforeCloseWindow()
        {
            _thread.Abort();

            return true;
        }

        /// <summary> Обработчик нажания кнопки отмена </summary>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCloseWindow();
        }

        /// <summary> Обработчик нажания кнопки отправить </summary>
        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_modelLogScope.Subject) || String.IsNullOrWhiteSpace(_modelLogScope.Description))
            {
                var message = String.Empty;

                if (String.IsNullOrWhiteSpace(_modelLogScope.Subject)) message = Application.Current.TryFindResource(@"ViewErrorReport_SubjectEmpty") + "\n";
                if (String.IsNullOrWhiteSpace(_modelLogScope.Description)) message += Application.Current.TryFindResource(@"ViewErrorReport_DescriptionEmpty") + "\n";

                message += Application.Current.TryFindResource(@"ViewErrorReport_ApplyQuestion")?.ToString();

                if (WindowMessageBox.Show(this, message, WindowMessageBoxButonEnum.YesCancel, WindowMessageBoxTypeEnum.Question) != WindowMessageBoxButonEnum.Yes) return;
            }

            OnLockWindow(true);

            _thread.Start();
        }

        /// <summary> Отправить отчет об ошибке </summary>
        private void SendTroubleTicket()
        {
            try
            {
                if (DataSourceLogScope.SendTroubleTicket(_modelLogScope))
                {
                    Dispatcher.Invoke(() =>
                    {
                        WindowMessageBox.Show(this, Application.Current.TryFindResource(@"ViewErrorReport_SendSuccessful") as string + " " + _modelLogScope.IssueId + ".");

                        OnCloseWindow();
                    });
                }
                else
                {
                    Dispatcher.Invoke(() => WindowMessageBox.Show(this, Application.Current.TryFindResource(@"ViewErrorReport_SendFail") as string, WindowMessageBoxButonEnum.Ok, WindowMessageBoxTypeEnum.Error));
                }
            }
            finally
            {
                OnLockWindow(false);
            }
        }

        /// <summary> Инвокатор события LockUI </summary>
        private void OnLockWindow(bool lockWindow)
        {
            LockWindow?.Invoke(this, lockWindow);
        }
    }
}
