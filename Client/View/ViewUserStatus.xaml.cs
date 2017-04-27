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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewUserStatus.xaml
    /// </summary>
    public partial class ViewUserStatus : UserControl, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewUserStatus_Title";

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Конструктор </summary>
        public ViewUserStatus()
        {
            InitializeComponent();

            var viewModelUserStatus = new ViewModelUserStatus();

            viewModelUserStatus.CloseView += (sender, args) => OnCloseWindow();

            DataContext = viewModelUserStatus;
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
        }
    }
}
