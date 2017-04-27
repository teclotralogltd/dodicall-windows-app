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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using dodicall.View;
using DAL.Model;

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : System.Windows.Window, IWindow
    {
        /// <summary> Главная форма приложения </summary>
        public static WindowMain CurrentMainWindow { get; private set; }

        /// <summary> Главная форма приложения </summary>
        private bool _firstHideWindow = true;

        /// <summary> Состояние окна до сворачивания </summary>
        private WindowState _windowStateBeforeMinimized;

        /// <summary> Объект ViewUserAuthorization </summary>
        private ViewUserAuthorization _viewUserAuthorization = new ViewUserAuthorization();

        /// <summary> Объект ViewSecurityKeyGenerated </summary>
        private ViewSecurityKeyGenerated _viewSecurityKeyGenerated = new ViewSecurityKeyGenerated();

        /// <summary> Объект ViewSecurityKeyGenerated </summary>
        private ViewSecurityKeyChatAccess _viewSecurityKeyChatAccess = new ViewSecurityKeyChatAccess();

        /// <summary> Событие закрытия окна логина (потом переделать все на это событие по нормальному) </summary>
        public event EventHandler ViewUserAuthorizationClosed;

        /// <summary> Конструктор </summary>
        public WindowMain()
        {
            InitializeComponent();

            _viewUserAuthorization.IsVisibleChanged += ViewUserAuthorizationOnIsVisibleChanged;

            _viewSecurityKeyChatAccess.IsVisibleChanged += _viewSecurityKeyChatAccess_IsVisibleChanged;

            _viewUserAuthorization.LockWindow += ViewOnLockWindow;

            this.SetResourceReference(TitleProperty, _viewUserAuthorization.WindowCaptionResourceKey);
             
            GridSecurityKeyGeneratedWinidow.IsVisibleChanged += GridSecurityKeyGenerated_IsVisibleChanged;

            //Кастыль
            GridSecurityKeyGeneratedWinidow.Children.Add(_viewSecurityKeyGenerated);
            GridSecurityKeyGeneratedWinidow.Visibility = Visibility.Collapsed;

            //Кастыль 2
            GridSecurityKeyChatAccessWinidow.Children.Add(_viewSecurityKeyChatAccess);
            GridSecurityKeyChatAccessWinidow.Visibility = Visibility.Collapsed;
             
            GridAuthorization.Children.Add(_viewUserAuthorization);

            DataContext = new ViewModelMain();

            CurrentMainWindow = this;

            _windowStateBeforeMinimized = WindowState;

            // нужно для организации правой рабочей облачти и рабочей области поверх правой (область для активного вызова)
            GridRightWorkspace.Children.Add(new UIElement());
            //GridRightWorkspace.Children.Add(new UIElement()); // вроде как уже не нужно
        }
         
        /// <summary> Подсветить иконку в панели задач </summary>
        public void HighlightIconTaskPanel()
        {
            FlashWindow.Flash(this);
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
         
        //Кастыль
        /// <summary> Обработчик закрытия формы о первичной генерации ключа </summary>
        private void GridSecurityKeyGenerated_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue as bool? == false)
            {
                ShowMenus();

                GridSecurityKeyGeneratedWinidow.Visibility = Visibility.Collapsed; 
            }
            else
            {
                HideMenus(); 
            }
        }

        //Кастыль
        /// <summary> Обработчик закрытия формы о импорте ключа </summary>
        private void _viewSecurityKeyChatAccess_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue as bool? == false)
            {
                ShowMenus();

                GridSecurityKeyChatAccessWinidow.Visibility = Visibility.Collapsed;
            }
            else
            {
                HideMenus();
            }
        }

        /// <summary> Показать меню на главном окне </summary>
        private void ShowMenus()
        {
            MenuMain.Visibility = Visibility.Visible;

            GridUser.Visibility = Visibility.Visible;

            RectangleUser.Visibility = Visibility.Visible;

            GridMain.Visibility = Visibility.Visible;

            GridLeftWorkspace.Visibility = Visibility.Visible;
        }

        /// <summary> Спрятать меню на главном окне </summary>
        private void HideMenus()
        {
            MenuMain.Visibility = Visibility.Collapsed;

            GridUser.Visibility = Visibility.Collapsed;

            RectangleUser.Visibility = Visibility.Collapsed;

            GridMain.Visibility = Visibility.Collapsed;

            GridLeftWorkspace.Visibility = Visibility.Collapsed;
        } 

        /// <summary> Обработчик закрытия формы логина </summary>
        private void ViewUserAuthorizationOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (_viewUserAuthorization.Visibility != Visibility.Collapsed) return;

            if (dependencyPropertyChangedEventArgs.NewValue as bool? == false)
            {
                _viewUserAuthorization.IsVisibleChanged -= ViewUserAuthorizationOnIsVisibleChanged;

                GridAuthorization.Children.Remove(_viewUserAuthorization);

                GridAuthorization.Visibility = Visibility.Hidden;

                _viewUserAuthorization = null;

                MenuMain.Visibility = Visibility.Visible;

                GridUser.Children.Add(new ViewUser());

                RectangleUser.Visibility = Visibility.Visible;

                GridMain.Visibility = Visibility.Visible;

                GridLeftWorkspace.Children.Add(new ViewContact());

                Title = String.Empty;

                OnViewUserAuthorizationClosed();

                this.Title = "- " + ModelUser.GetInstance().FullName;

                UnlockWindow(); //----------------------------------------------------------------
            }
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

        /// <summary> Обработчик события отрисовки содержимого окна на экране </summary>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // сделал тут потому что если вызывать это в конструкторе, то форма блокировки экрана вызывается раньше чем показывается сама форма
            if (_viewUserAuthorization.Autologin) _viewUserAuthorization.Login();
        }

        /// <summary> Обработчик нажатия кнопки закрыть в меню </summary>
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            HideWindow();
        }

        /// <summary> Отобразить на правой рабочей области </summary>
        public static void ShowInRightWorkspace(UserControl userControl)
        {
            //CurrentMainWindow.GridRightWorkspace.Children.Clear();

            //if (userControl != null) CurrentMainWindow.GridRightWorkspace.Children.Add(userControl);

            var uiCollection = CurrentMainWindow.GridRightWorkspace.Children;

            if (uiCollection.Count > 0)
            {
                (uiCollection[0] as IDisposable)?.Dispose();

                uiCollection.RemoveAt(0);
            }

            if (userControl != null)
            {
                uiCollection.Insert(0, userControl);

                CloseInRightWorkspaceFront(); // кастыль, т.к. концепция с окном поверх всего вдруг резко поменялась в течении спринта !!!
            }
            else
            {
                uiCollection.Insert(0, new UIElement());
            }

            GC.Collect();
        } 

        //На будущее, нужно вернуть мехнизм истории показа экранов в правой части
        /// <summary> Отобразить на правой рабочей области поверх всего </summary>
        public static void ShowInRightWorkspaceFront(UserControl userControl)
        {
            var uiCollection = CurrentMainWindow.GridRightWorkspace.Children;

            if (uiCollection.Count > 1) uiCollection.RemoveAt(1);

            if (userControl != null) uiCollection.Insert(1, userControl);
        }

        /// <summary> Закрыть правую рабочую область поверх всего </summary>
        public static void CloseInRightWorkspaceFront()
        {
            var uiCollection = CurrentMainWindow.GridRightWorkspace.Children;

            if (uiCollection.Count > 1)
            {
                ((IDisposable)uiCollection[1]).Dispose();
                uiCollection.RemoveAt(1);
            }
        }

        /// <summary> Возвращает UserControl на правой рабочей области </summary>
        public static UserControl GetRightWorkspaceContent()
        {
            return CurrentMainWindow.GridRightWorkspace.Children.Count != 0 ? CurrentMainWindow.GridRightWorkspace.Children[0] as UserControl : null;
        }

        /// <summary> Возвращает UserControl на правой рабочей области (поверх всего) </summary>
        public static UserControl GetRightWorkspaceFrontContent()
        {
            return CurrentMainWindow.GridRightWorkspace.Children.Count > 1 ? CurrentMainWindow.GridRightWorkspace.Children[1] as UserControl : null;
        }

        /// <summary> Инвокатор события ViewUserAuthorizationClosed </summary>
        private void OnViewUserAuthorizationClosed()
        {
            ViewUserAuthorizationClosed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Обработчик изменения состояния окна </summary>
        private void WindowMain_OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                _windowStateBeforeMinimized = WindowState;

                var uiCollection = CurrentMainWindow.GridRightWorkspace.Children;

                // кастыль, т.к. зае...ли менять требования по отображению активного звонка
                if (uiCollection.Count > 1)
                {
                    WindowCallActive.Instance?.Hide();
                }
                else
                {
                    WindowCallActive.Instance?.Show();
                }
            }
            else
            {
                // кастыль, т.к. зае...ли менять требования по отображению активного звонка
                WindowCallActive.Instance?.Show();
            }
        }

        /// <summary> Показать окно </summary>
        public void ShowWindow()
        {
            Show();

            if (WindowState == WindowState.Minimized) WindowState = _windowStateBeforeMinimized;

            Activate();
        }

        /// <summary> Скрыть окно </summary>
        public void HideWindow()
        {
            Hide();

            if (_firstHideWindow)
            {
                _firstHideWindow = false;
                WindowStartup.Instance.ShowMessageTray(Application.Current.TryFindResource(@"WindowMain_ApplicationWork") as string);
            }
        }

        /// <summary> Обработчик закрытия окна </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            HideWindow();
        }

        /// <summary> Обработчик изменения вызова окна </summary>
        private void WindowMain_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_viewUserAuthorization == null) return;

            if (_viewUserAuthorization.Visibility == Visibility.Visible && Visibility == Visibility.Hidden) WindowStartup.Instance.ApplicationExit();
        }
    }
}
