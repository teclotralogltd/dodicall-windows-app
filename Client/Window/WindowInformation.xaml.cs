using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using DAL.ViewModel;
using DAL.WrapperBridge;

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowInformation.xaml
    /// </summary>
    public partial class WindowInformation : System.Windows.Window
    {
        /// <summary> Показать окно о программе </summary>
        public static void ShowAbout()
        {
            var viewModelInformation = new ViewModelInformation();

            var windowAbout = new WindowInformation
            {
                DataContext = viewModelInformation,
                WebBrowserMain = { Source = new Uri(Application.Current.TryFindResource(@"WindowInformation_AboutUrl") as string ?? "") },
                StackPanelVersion = { Visibility = Visibility.Visible }
            };
            
            windowAbout.ShowDialog();
        }

        /// <summary> Показать окно что нового </summary>
        public static void ShowNews()
        {
            var windowNews = new WindowInformation();

            windowNews.Title = Application.Current.TryFindResource(@"WindowInformation_NewsTitle") as string;

            windowNews.WebBrowserMain.Source = new Uri(Application.Current.TryFindResource(@"WindowInformation_NewsUrl") as string ?? "");

            windowNews.ShowDialog();
        }

        /// <summary> Показать окно известные проблемы </summary>
        public static void ShowProblems()
        {
            var windowProblems = new WindowInformation();

            windowProblems.Title = Application.Current.TryFindResource(@"WindowInformation_ProblemsTitle") as string;

            windowProblems.WebBrowserMain.Source = new Uri(Application.Current.TryFindResource(@"WindowInformation_ProblemsUrl") as string ?? "");

            windowProblems.ShowDialog();
        }

        /// <summary> Конструктор </summary>
        private WindowInformation()
        {
            InitializeComponent();
        }

        /// <summary> Обработчика нажатия кнопки Ok </summary>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
