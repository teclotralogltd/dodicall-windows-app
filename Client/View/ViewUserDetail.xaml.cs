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
    /// Interaction logic for ViewUserDetail.xaml
    /// </summary>
    public partial class ViewUserDetail : UserControl
    {
        /// <summary> Конструктор </summary>
        public ViewUserDetail(ModelUser modelUser)
        {
            InitializeComponent();

            DataContext = new ViewModelUserDetail(modelUser);

            foreach (var modelUserContact in modelUser.ListModelUserContactMy)
            {
                StackPanelMyContact.Children.Add(CreateContactMyControl(modelUserContact));
            }

            if (modelUser.ListModelUserContactExtra.Count == 0)
            {
                TextBlockExtraContact.Visibility = Visibility.Collapsed;
                RectangleExtraContact.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var modelUserContact in modelUser.ListModelUserContactExtra)
                {
                    StackPanelExtraContact.Children.Add(CreateExtraContactControl(modelUserContact));
                }
            }
        }

        /// <summary> Обработчик клика по статусу </summary>
        private void ChangeUserStatus(object sender, MouseButtonEventArgs e)
        {
            var windowUserSettings = new WindowStandard(new ViewUserStatus())
            {
                MinHeight = 185,
                Height = 185,
                MinWidth = 530,
                Width = 530,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                Owner = System.Windows.Window.GetWindow(this),
                Style = Application.Current.TryFindResource(@"VS2012MessageBoxStyle") as Style
            };

            windowUserSettings.ShowDialog();
        }

        /// <summary> Создание элемента для моих котактов </summary>
        private StackPanel CreateContactMyControl(ModelUserContact modelUserContact)
        {
            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal };

            stackPanel.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = @"d-sip", Foreground = Brushes.Gray });

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(10,0,0,0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold, Text = modelUserContact.IdentityString });

            return stackPanel;
        }

        /// <summary> Создание элемента для дополнительных котактов </summary>
        private StackPanel CreateExtraContactControl(ModelUserContact modelUserContact)
        {
            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal };

            var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Gray };

            textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewUserDetail_Phone");

            stackPanel.Children.Add(textBlock);

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold, Text = modelUserContact.IdentityString });

            return stackPanel;
        }

        ///<summary> Изменение размера GridUserStatus </summary>
        private void GridUserStatus_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ColumnDefinitionStatus.ActualWidth > 10) TextBlockUserExtendedStatus.Width = ColumnDefinitionStatus.ActualWidth - 10;
        }

        ///<summary> Изменение размера StackPanel для BaseStatus </summary>
        private void StackPanelBaseStatus_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // нужно для динамического изменения размера, т.к. в GridUserStatus_OnSizeChanged мы хардкодим размер
            GridUserStatus_OnSizeChanged(null, null);
        }
    }
}
