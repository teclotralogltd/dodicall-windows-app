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
using dodicall.Window;
using DAL.Model;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewUser.xaml
    /// </summary>
    public partial class ViewUser : UserControl
    {
        /// <summary> Текущий пользователь </summary>
        private ModelUser _modelUser;

        /// <summary> Объект ViewModelUser </summary>
        private ViewModelUser _viewModelUser;

        /// <summary> Конструктор </summary>
        public ViewUser()
        {
            InitializeComponent();

            _viewModelUser = new ViewModelUser();

            _modelUser = _viewModelUser.CurrentModelUser;

            DataContext = _viewModelUser;

            // не перересовывается панель с статусом пользователя по этоум приходится руками сворачивать панель баланса
            StackPanelBalance.Visibility = _modelUser.HasBalance ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary> Обработчик клика по статусу </summary>
        private void ChangeUserStatus(object sender, MouseButtonEventArgs e)
        {
            var windowUserSettings = new WindowStandard(new ViewUserStatus())
            {
                MinHeight = 185, Height = 185, MinWidth = 530, Width = 530, WindowStartupLocation = WindowStartupLocation.CenterOwner, ResizeMode = ResizeMode.NoResize,
                Owner = System.Windows.Window.GetWindow(this), Style = Application.Current.TryFindResource(@"VS2012MessageBoxStyle") as Style
            };

            windowUserSettings.ShowDialog();
        }

        /// <summary> Обработчик клика по профилю </summary>
        private void OpenUserDetail(object sender, MouseButtonEventArgs e)
        {
            var viewUserDetail = new ViewUserDetail(_modelUser);

            viewUserDetail.IsVisibleChanged += (o, args) => { Background = (bool)args.NewValue ? Brushes.WhiteSmoke : Brushes.Transparent; };

            WindowMain.ShowInRightWorkspace(viewUserDetail);
        }

        /// <summary> Обработчик клика по балансу </summary>
        private void Balance_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModelUser.CommandOpenUrl.Execute(null);
        }
    }
}
