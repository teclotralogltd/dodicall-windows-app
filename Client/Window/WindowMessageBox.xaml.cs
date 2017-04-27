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

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowMessageBox.xaml
    /// </summary>
    public partial class WindowMessageBox : System.Windows.Window
    {
        /// <summary> Результат показа окна </summary>
        private WindowMessageBoxButonEnum? _result;

        /// <summary> Конструктор </summary>
        private WindowMessageBox()
        { 
            InitializeComponent();
        }

        /// <summary> Показать окно с текстом по центру экрана </summary>
        public static void Show(string text)
        {
            var windowMessageBox = new WindowMessageBox
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                TextBlockText = { Text = text },
                Title = Application.Current.TryFindResource(@"WindowMessageBox_Information")?.ToString()
            };

            windowMessageBox.ButtonOk.Visibility = Visibility.Visible;

            windowMessageBox.ImageInformation.Visibility = Visibility.Visible;

            windowMessageBox.ShowDialog();
        }

        /// <summary> Показать окно Coming soon </summary>
        public static void ShowComingSoon(UserControl userControl)
        {
            Show(userControl, Application.Current.TryFindResource(@"Common_ComingSoon") as string);
        }

        /// <summary> Показать окно Coming soon </summary>
        public static void ShowComingSoon(System.Windows.Window window)
        {
            Show(window, Application.Current.TryFindResource(@"Common_ComingSoon") as string);
        }

        /// <summary> Показать окно </summary>
        public static WindowMessageBoxButonEnum? Show(UserControl userControl, string text)
        {
            var window = System.Windows.Window.GetWindow(userControl);

            return Show(window, text, WindowMessageBoxButonEnum.Ok, WindowMessageBoxTypeEnum.Information);
        }

        /// <summary> Показать окно </summary>
        public static WindowMessageBoxButonEnum? Show(System.Windows.Window window, string text)
        {
            return Show(window, text, WindowMessageBoxButonEnum.Ok, WindowMessageBoxTypeEnum.Information);
        }

        /// <summary> Показать окно </summary>
        public static WindowMessageBoxButonEnum? Show(UserControl userControl, string text, WindowMessageBoxButonEnum windowMessageBoxButonEnum, WindowMessageBoxTypeEnum windowMessageBoxTypeEnum)
        {
            var window = System.Windows.Window.GetWindow(userControl);

            return Show(window, text, windowMessageBoxButonEnum, windowMessageBoxTypeEnum);
        }

        /// <summary> Показать окно </summary>
        public static WindowMessageBoxButonEnum? Show(System.Windows.Window window, string text, WindowMessageBoxButonEnum windowMessageBoxButonEnum, WindowMessageBoxTypeEnum windowMessageBoxTypeEnum)
        {
            object resource;

            var header = String.Empty;

            switch (windowMessageBoxTypeEnum)
            {
                case WindowMessageBoxTypeEnum.Information:
                    resource = Application.Current.TryFindResource(@"WindowMessageBox_Information");
                    header = resource as string ?? String.Empty;
                    break;
                case WindowMessageBoxTypeEnum.Warning:
                    resource = Application.Current.TryFindResource(@"WindowMessageBox_Warning");
                    header = resource as string ?? String.Empty;
                    break;
                case WindowMessageBoxTypeEnum.Error:
                    resource = Application.Current.TryFindResource(@"WindowMessageBox_Error");
                    header = resource as string ?? String.Empty;
                    break;
                case WindowMessageBoxTypeEnum.Question:
                    resource = Application.Current.TryFindResource(@"WindowMessageBox_Question");
                    header = resource as string ?? String.Empty;
                    break;
            }

            return Show(window, header, text, windowMessageBoxButonEnum, windowMessageBoxTypeEnum);
        }

        /// <summary> Показать окно </summary>
        public static WindowMessageBoxButonEnum? Show(System.Windows.Window window, string header, string text, WindowMessageBoxButonEnum windowMessageBoxButonEnum, WindowMessageBoxTypeEnum windowMessageBoxTypeEnum)
        {
            var windowMessageBox = new WindowMessageBox
            {
                Owner = window,
                TextBlockText = {Text = text},
                Title = header
            };

            switch (windowMessageBoxButonEnum)
            {
                case WindowMessageBoxButonEnum.Ok:
                    windowMessageBox.ButtonOk.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxButonEnum.OkCancel:
                    windowMessageBox.ButtonOk.Visibility = Visibility.Visible;
                    windowMessageBox.ButtonCancel.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxButonEnum.Yes:
                    windowMessageBox.ButtonYes.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxButonEnum.YesCancel:
                    windowMessageBox.ButtonYes.Visibility = Visibility.Visible;
                    windowMessageBox.ButtonCancel.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxButonEnum.YesNo:
                    windowMessageBox.ButtonYes.Visibility = Visibility.Visible;
                    windowMessageBox.ButtonNo.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxButonEnum.YesNoCancel:
                    windowMessageBox.ButtonYes.Visibility = Visibility.Visible;
                    windowMessageBox.ButtonNo.Visibility = Visibility.Visible;
                    windowMessageBox.ButtonCancel.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxButonEnum.ContinueCancel:
                    windowMessageBox.ButtonContinue.Visibility = Visibility.Visible; 
                    windowMessageBox.ButtonCancel.Visibility = Visibility.Visible;
                    break; 
            }

            switch (windowMessageBoxTypeEnum)
            {
                case WindowMessageBoxTypeEnum.Information:
                    windowMessageBox.ImageInformation.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxTypeEnum.Warning:
                    windowMessageBox.ImageWarning.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxTypeEnum.Error:
                    windowMessageBox.ImageError.Visibility = Visibility.Visible;
                    break;
                case WindowMessageBoxTypeEnum.Question:
                    windowMessageBox.ImageQuestion.Visibility = Visibility.Visible;
                    break;
            }

            windowMessageBox.ShowDialog();

            return windowMessageBox._result;
        }

        /// <summary> Показать окно Exception </summary>
        public static void Show(Exception exception)
        {
            var windowMessageBox = new WindowMessageBox
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                TextBlockText = { Text = exception.Message },
                Title = "Unhandled Exception"
            };

            windowMessageBox.ButtonOk.Visibility = Visibility.Visible;

            windowMessageBox.ImageError.Visibility = Visibility.Visible;

            windowMessageBox.ShowDialog();
        }

        /// <summary> Обработчик кнопки ButtonOk </summary>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            _result = WindowMessageBoxButonEnum.Ok;

            Close();
        }

        /// <summary> Обработчик кнопки ButtonYes </summary>
        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            _result = WindowMessageBoxButonEnum.Yes;

            Close();
        }

        /// <summary> Обработчик кнопки ButtonNo </summary>
        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            _result = WindowMessageBoxButonEnum.No;

            Close();
        }

        /// <summary> Обработчик кнопки ButtonContinue </summary>
        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            _result = WindowMessageBoxButonEnum.Continue;

            Close();
        }

        /// <summary> Обработчик кнопки ButtonCancel </summary>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            _result = WindowMessageBoxButonEnum.Cancel;

            Close();
        }
    }
}
