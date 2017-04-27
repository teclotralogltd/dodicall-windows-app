using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dodicall.Localization;
using dodicall.Window;
using DAL.Enum;
using DAL.ViewModel;
using System.Security;
using DAL.Utility;

namespace dodicall.View
{ 
    /// <summary>
    /// Interaction logic for ViewUserAuthorization.xaml
    /// </summary>
    public partial class ViewUserAuthorization : ILockWindowProcess, IWindowCaption
    {
        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewUserAuthorization_WindowCaption";

        /// <summary> Событие блокировки окна </summary>
        public event EventHandler<bool> LockWindow;

        /// <summary> Кол-во кликов по логотипу </summary>
        private int _clickLogo;

        /// <summary> Автологин </summary>
        public bool Autologin => _viewModelUserAuthorization.CurrentModelLogin.AutoLogin;

        /// <summary> Источник данных ViewModel </summary>
        private readonly ViewModelUserAuthorization _viewModelUserAuthorization = new ViewModelUserAuthorization();

        /// <summary> Конструктор </summary>
        public ViewUserAuthorization()
        {
            InitializeComponent();

            DataContext = _viewModelUserAuthorization;

            _viewModelUserAuthorization.PropertyChanged += ViewModelUserAuthorizationOnPropertyChanged;

            _viewModelUserAuthorization.LockUI += (sender, lockUI) => OnLockWindow(lockUI); 
            // обход ограничений биндинга PasswordBox'а
            PasswordBoxUser.Password = UtilitySecurity.ConvertToString(_viewModelUserAuthorization.CurrentModelLogin.Password);
         
            ComboBoxServerArea.Visibility = _viewModelUserAuthorization.CurrentModelLogin.ModelServerAreaObj == null ||_viewModelUserAuthorization.CurrentModelLogin.ModelServerAreaObj.Id == 0 ? Visibility.Hidden : Visibility.Visible;

            TextBlockVestion.Text = _viewModelUserAuthorization.AppVersion;
        }

        /// <summary> Авторизация пользователя </summary>
        public void Login()
        {
            // вынужденный обход паттерна MVVM, т.к. PasswordBox не позволяет делать биндинг на св-во Password
            // есть вариант решения через attached properties, но мне он показался не кашерным
            // ссылка: http://stackoverflow.com/questions/888466/passwordbox-binding

            _viewModelUserAuthorization.CurrentModelLogin.Password = PasswordBoxUser.SecurePassword;  

            _viewModelUserAuthorization.CommandLogin.Execute(null);
        }

        /// <summary> Обработчик изменения ViewModel </summary>
        private void ViewModelUserAuthorizationOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != @"FailLogin") return;

            if (_viewModelUserAuthorization.FailLogin == EnumResultLogin.AuthFailed)
            {
                TextBlockFailLogin.Visibility = Visibility.Visible;
                TextBlockSystemError.Visibility = Visibility.Hidden;
                TextBlockNoNetwork.Visibility = Visibility.Hidden;
                OnLockWindow(false);
            }

            if (_viewModelUserAuthorization.FailLogin == EnumResultLogin.SystemError)
            {
                TextBlockSystemError.Visibility = Visibility.Visible;
                TextBlockFailLogin.Visibility = Visibility.Hidden;
                TextBlockNoNetwork.Visibility = Visibility.Hidden;
                OnLockWindow(false);
            }

            if (_viewModelUserAuthorization.FailLogin == EnumResultLogin.NoNetwork)
            {
                TextBlockNoNetwork.Visibility = Visibility.Visible;
                TextBlockSystemError.Visibility = Visibility.Hidden;
                TextBlockFailLogin.Visibility = Visibility.Hidden;
                OnLockWindow(false);
            }

            if (_viewModelUserAuthorization.FailLogin == EnumResultLogin.No) Visibility = Visibility.Collapsed;
        }

        /// <summary> Обработчик нажатия мыши на логотип </summary>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickLogo++;

            if (_clickLogo > 4)
            {
                _viewModelUserAuthorization.LoadListModelServerArea();

                ComboBoxServerArea.Visibility = Visibility.Visible;
            }
        }

        /// <summary> Инвокатор события LockUI </summary>
        private void OnLockWindow(bool lockWindow)
        {
            LockWindow?.Invoke(this, lockWindow);
        }

        /// <summary> Обработка нажатия Enter </summary>
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Login();
        }

        /// <summary> Обработка нажатия кнопки входа </summary>
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        /// <summary> Обработка изменения площадки (кастылище !!! но нет времени, потом переделать на подписку события и отписку в IDispose) </summary>
        private void ComboBoxServerArea_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlockVestion.Text = _viewModelUserAuthorization.AppVersion;
        }

        /// <summary> Обработчик двойного клика по TextBoxLogin </summary>
        private void TextBoxLogin_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBoxLogin.SelectAll();
        }
    }
}
