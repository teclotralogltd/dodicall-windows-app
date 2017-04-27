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

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowException.xaml
    /// </summary>
    public partial class WindowException : System.Windows.Window
    {
        /// <summary> Показать окно с ошибкой </summary>
        public static void ShowException(Exception exception)
        {
            var windowException = new WindowException(exception);

            windowException.ShowDialog();
        }

        /// <summary> Конструктор </summary>
        private WindowException(Exception exception)
        {
            InitializeComponent();

            DataContext = exception;
        }

        /// <summary> Обработчик нажатия на кнопку Ok </summary>
        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
