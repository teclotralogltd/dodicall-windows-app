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
using dodicall.Enum;
using DAL.ViewModel;
using DAL.Utility;
using DAL.Enum;
using dodicall.Window;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewPasswordBox.xaml
    /// </summary>
    public partial class ViewPasswordBox : UserControl, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Ключ ресурса для заголовка окна </summary> 
        public string WindowCaptionResourceKey { get; set; } 

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Объект ViewModelPasswordBox </summary>
        private readonly ViewModelPasswordBox _viewModelPasswordBox;

        /// <summary> Результат показа окна </summary>
        public bool Result;

        /// <summary> Конструктор </summary>
        public ViewPasswordBox(PasswordBoxTypeEnum windowType)
        {
            InitializeComponent();

            _viewModelPasswordBox = new ViewModelPasswordBox();

            var windowCaptionResourceKey = String.Empty;
            var buttonNameResourceKey = String.Empty;

            switch (windowType)
            {
                case PasswordBoxTypeEnum.Export:
                    windowCaptionResourceKey = @"ViewPasswordBox_ExportEncryptionKey";
                    buttonNameResourceKey = @"ViewPasswordBox_Export";
                    break;
                case PasswordBoxTypeEnum.Import:
                    windowCaptionResourceKey = @"ViewPasswordBox_ImportEncryptionKey";
                    buttonNameResourceKey = @"ViewPasswordBox_Import";
                    break;
                case PasswordBoxTypeEnum.ExportToClipboard:
                    windowCaptionResourceKey = @"ViewPasswordBox_ExportEncryptionKey";
                    buttonNameResourceKey = @"ViewPasswordBox_Export";
                    break;
                case PasswordBoxTypeEnum.RegenerateKey:
                    windowCaptionResourceKey = @"ViewPasswordBox_CreateNewKeyTitle";
                    buttonNameResourceKey = @"ViewPasswordBox_CreateNewKey";
                    break;
            }

            WindowCaptionResourceKey = windowCaptionResourceKey;
            EventButton.SetResourceReference(Button.ContentProperty, buttonNameResourceKey);

            DataContext = _viewModelPasswordBox;

            //Для установки focus-a на passwordbox при запуске окна с ViewPasswordBox. методы focus вызываемые внутри конструктора usercontrol-a не работают
            Dispatcher.BeginInvoke(new Action(() => { Keyboard.Focus(PasswordBoxUser); }), System.Windows.Threading.DispatcherPriority.Loaded);
             
            _viewModelPasswordBox.EventViewModel += ViewModelPasswordBoxOnEventViewModel;

            PasswordBoxUser.PasswordChanged += PasswordBoxUserOnPasswordChanged;
        }

        /// <summary> Обработчик события изменение пароля </summary>
        private void PasswordBoxUserOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModelPasswordBox.Password = PasswordBoxUser.SecurePassword;
        }

        /// <summary> Обработчик события ViewModel </summary>
        private void ViewModelPasswordBoxOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == "CheckPassword")
            {
                Result = (bool)e.Data;

                if (Result)
                {
                    OnCloseWindow();
                }
                else
                {
                    TextBlockIncorrectPassword.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary> Действие перед закрытием окна </summary>
        public bool BeforeCloseWindow()
        {
            return true;
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);

            _viewModelPasswordBox.EventViewModel -= ViewModelPasswordBoxOnEventViewModel;

            PasswordBoxUser.PasswordChanged -= PasswordBoxUserOnPasswordChanged;

            _viewModelPasswordBox.Dispose();
        }
    }
}
