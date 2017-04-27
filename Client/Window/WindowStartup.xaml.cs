using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using dodicall.View;
using DAL.Localization;
using DAL.Model;
using DAL.ModelEnum;
using DAL.Utility;
using DAL.ViewModel;
using DAL.WrapperBridge;
using Application = System.Windows.Application;

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowStartup.xaml
    /// </summary>
    public partial class WindowStartup : System.Windows.Window
    {
        public static WindowStartup Instance { get; private set; }

        private WindowMain _windowMain;

        public NotifyIcon NotifyIcon = new NotifyIcon { Icon = Properties.Resources.dodicall_offline, Visible = true };

        private ToolStripMenuItem _itemOpen = new ToolStripMenuItem();
        private ToolStripMenuItem _itemStatus = new ToolStripMenuItem { Enabled = false };
        private ToolStripMenuItem _itemExit = new ToolStripMenuItem();
        private ToolStripMenuItem _itemOnline = new ToolStripMenuItem { Image = Properties.Resources.online };
        private ToolStripMenuItem _itemDnd = new ToolStripMenuItem { Image = Properties.Resources.dnd };
        private ToolStripMenuItem _itemHidden = new ToolStripMenuItem { Image = Properties.Resources.offline };
        private ToolStripMenuItem _itemOffline = new ToolStripMenuItem { Image = Properties.Resources.offline };
        private ToolStripMenuItem _itemSetStatus = new ToolStripMenuItem();

        private ViewModelTray _viewModelTray = new ViewModelTray();

        /// <summary> Конструктор </summary>
        public WindowStartup()
        {
            InitializeComponent();

            Title = "DCBF3DE8-032E-4DCA-8158-AE0CF30A15AC" + WindowsIdentity.GetCurrent().Name.Replace(@"\", "-");

            Instance = this;

            _windowMain = FactoryWindow.GetWindowMain();

            // привязка смены языка интерфейса к смене языка в приложении
            LocalizationApp.GetInstance().LocalizationChanged += (sender, language) => OnLocalizationChanged();
            ModelUser.GetInstance().UserStatusChanged += (sender, args) => OnUserStatusChanged();

            OnLocalizationChanged();

            _itemStatus.DropDownItems.Add(_itemOnline);
            _itemStatus.DropDownItems.Add(_itemDnd);
            _itemStatus.DropDownItems.Add(_itemHidden);
            _itemStatus.DropDownItems.Add(_itemOffline);
            _itemStatus.DropDownItems.Add(new ToolStripSeparator());
            _itemStatus.DropDownItems.Add(_itemSetStatus);

            _itemOnline.Click += (sender, args) => { ModelUser.GetInstance().ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(1); _viewModelTray.SaveStatusModelUser(); };
            _itemDnd.Click += (sender, args) => { ModelUser.GetInstance().ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(3); _viewModelTray.SaveStatusModelUser(); };
            _itemHidden.Click += (sender, args) => { ModelUser.GetInstance().ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(2); _viewModelTray.SaveStatusModelUser(); };
            _itemOffline.Click += (sender, args) => { ModelUser.GetInstance().ModelEnumUserBaseStatusObj = ModelEnumUserBaseStatus.GetModelEnum(0); _viewModelTray.SaveStatusModelUser(); };
            _itemSetStatus.Click += (sender, args) => SetUserStatus();
            _itemOpen.Click += (sender, args) => WindowDispacher.GetInstance.ShowAllWindow();
            _itemExit.Click += (sender, args) => ApplicationExit();

            var ctxmenustrip = new ContextMenuStrip();

            ctxmenustrip.Items.Add(_itemStatus);
            ctxmenustrip.Items.Add(_itemOpen);
            ctxmenustrip.Items.Add(new ToolStripSeparator());
            ctxmenustrip.Items.Add(_itemExit);

            NotifyIcon.ContextMenuStrip = ctxmenustrip;
            NotifyIcon.DoubleClick += (sender, args) => { WindowDispacher.GetInstance.ShowAllWindow(); };

            _windowMain.ViewUserAuthorizationClosed += WindowMainOnViewUserAuthorizationClosed;

            _windowMain.Show();
        }

        /// <summary> Обработчик закрытия формы логина </summary>
        private void WindowMainOnViewUserAuthorizationClosed(object sender, EventArgs eventArgs)
        {
            _itemStatus.Enabled = true;
        }

        /// <summary> Обработчик клика по установке статуса </summary>
        private void SetUserStatus()
        {
            var windowUserSettings = new WindowStandard(new ViewUserStatus())
            {
                MinHeight = 185,
                Height = 185,
                MinWidth = 530,
                Width = 530,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                Style = Application.Current.TryFindResource(@"VS2012MessageBoxStyle") as Style
            };

            windowUserSettings.ShowDialog();
        }

        /// <summary> Обработчик изменения языка приложения </summary>
        private void OnLocalizationChanged()
        {
            _itemOpen.Text = Application.Current.TryFindResource(@"WindowStartup_Open") as string;
            _itemStatus.Text = Application.Current.TryFindResource(@"WindowStartup_Status") as string;
            _itemExit.Text = Application.Current.TryFindResource(@"WindowStartup_Exit") as string;
            _itemOnline.Text = Application.Current.TryFindResource(@"WindowStartup_Online") as string;
            _itemDnd.Text = Application.Current.TryFindResource(@"WindowStartup_Dnd") as string;
            _itemHidden.Text = Application.Current.TryFindResource(@"WindowStartup_Hidden") as string;
            _itemOffline.Text = Application.Current.TryFindResource(@"WindowStartup_Offline") as string;
            _itemSetStatus.Text = Application.Current.TryFindResource(@"WindowStartup_SetStatus") as string;
        }

        /// <summary> Обработчик изменения статуса пользователя </summary>
        private void OnUserStatusChanged()
        {
            switch (ModelUser.GetInstance().ModelEnumUserBaseStatusObj.Code)
            {
                case 1:
                    NotifyIcon.Icon = Properties.Resources.dodicall_online;
                    break;
                case 3:
                    NotifyIcon.Icon = Properties.Resources.dodicall_dnd;
                    break;
                default:
                    NotifyIcon.Icon = Properties.Resources.dodicall_offline;
                    break;
            }
        }

        /// <summary> Обработчик изменения состояния окна </summary>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            // этот обработчик нуже для того что бы при повторном запуске приложения мы находили нанное окно по Guid и через WinAPI отправлели 
            // ему команду [DllImport("user32.dll")]ShowWindow() что позволяет перехватить событие изменения состояния окна и показать основное окно,
            // т.к. при закрытом WindowMain найти его через WinAPI кроме как через Title не возможно, а так как Title у WindowMain постоянно меняется придется
            // искать по списку возможных Title, да и вообще искать окно по неуникальному Title это плохая идея, при окрытой в проводнике папке dodicall
            // и таком же заголовке окна WindowMain в зависимости что открыто было первым открыться может окно проводника

            if (WindowState != WindowState.Minimized)
            {
                WindowState = WindowState.Minimized;

                WindowDispacher.GetInstance.ShowAllWindow();
            }
        }

        /// <summary> Показать сообщение в трее </summary>
        public void ShowMessageTray(string text)
        {
            NotifyIcon.ShowBalloonTip(5000, "dodicall", text, ToolTipIcon.Info);
        }

        /// <summary> Выйти из приложения </summary>
        public void ApplicationExit()
        {
            DataSourceLogin.Logout();

            App.Mutex.ReleaseMutex();
            App.Mutex.Dispose();

            NotifyIcon.Visible = false;

            Environment.Exit(0);
        }
    }
}
