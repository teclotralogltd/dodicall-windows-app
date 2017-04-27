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
using DAL.Utility;
using DAL.ViewModel;
using DAL.Localization;
using System.ComponentModel;
using DAL.Abstract;
using DAL.Enum;
using Microsoft.Win32;
using System.IO;
using dodicall.Enum;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewSecurityKeyGenerated.xaml
    /// </summary>
    public partial class ViewSecurityKeyGenerated : UserControl
    {
        /// <summary> Объект ViewModelSelectionContact </summary>
        private readonly ViewModelSecurityKeyGenerated _viewModelSecurityKeyGenerated = new ViewModelSecurityKeyGenerated();

        /// <summary> Конструктор </summary>
        public ViewSecurityKeyGenerated()
        {
            InitializeComponent(); 
            DataContext = _viewModelSecurityKeyGenerated;  

            _viewModelSecurityKeyGenerated.EventViewModel += _viewModelSelectionContact_EventViewModel;
        } 

        /// <summary> Экспортировать в файл с вызовом окна пароля </summary>
        private void SaveAs()
        {
            var windowPasswordBox = FactoryWindow.GetWindowPasswordBox(PasswordBoxTypeEnum.Export);

            windowPasswordBox.Owner = WindowMain.CurrentMainWindow;

            windowPasswordBox.ShowDialog();
        }  

        /// <summary> Событие в ViewModelSecurityKeyGenerated </summary>
        private void _viewModelSelectionContact_EventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            WindowMain.CurrentMainWindow.GridSecurityKeyGeneratedWinidow.Visibility = Visibility.Visible; 
        } 

        /// <summary> Показать окно Coming Soon </summary>
        private void ShowComingSoon(object sender, RoutedEventArgs e)
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        /// <summary> Закрыть форму </summary>
        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            WindowMain.CurrentMainWindow.GridSecurityKeyGeneratedWinidow.Visibility = Visibility.Collapsed;
        }

        /// <summary> Экспортировать ключ </summary>
        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            var windowPasswordBox = FactoryWindow.GetWindowPasswordBox(PasswordBoxTypeEnum.Export);

            windowPasswordBox.Owner = WindowMain.CurrentMainWindow;

            windowPasswordBox.ShowDialog();

            if ((windowPasswordBox.ViewUserControl as ViewPasswordBox).Result)
            {
                var encryptionkey = _viewModelSecurityKeyGenerated.GetUserPrivateKey();

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files | *.txt";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.FileName = "Dodicall_key.txt";
               
                var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var pathDownload = System.IO.Path.Combine(pathUser, "Downloads");

                saveFileDialog.InitialDirectory = pathDownload;

                if (saveFileDialog.ShowDialog() != true) return;
                File.WriteAllText(saveFileDialog.FileName, encryptionkey);
                WindowStartup.Instance.ShowMessageTray(Application.Current.TryFindResource(@"ViewSecurityKeyGenerated_ExportSucced") as string);
            }
        }
          
        /// <summary> Копировать ключ в буфер обмена </summary>
        private void ButtonCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            var windowPasswordBox = FactoryWindow.GetWindowPasswordBox(PasswordBoxTypeEnum.ExportToClipboard);

            windowPasswordBox.Owner = WindowMain.CurrentMainWindow;

            windowPasswordBox.ShowDialog();

            if ((windowPasswordBox.ViewUserControl as ViewPasswordBox).Result)
            {
                var encryptionkey = _viewModelSecurityKeyGenerated.GetUserPrivateKey();
                Clipboard.Clear();
                Clipboard.SetText(encryptionkey);
               
                WindowStartup.Instance.ShowMessageTray(Application.Current.TryFindResource(@"ViewSecurityKeyGenerated_ExportSucced") as string);
            }
        }
    }
}
