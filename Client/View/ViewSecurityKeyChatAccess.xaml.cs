using DAL.Abstract;
using DAL.Enum;
using DAL.ViewModel;
using dodicall.Window;
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

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewSecurityKeyChatAccess.xaml
    /// </summary>
    public partial class ViewSecurityKeyChatAccess : UserControl, IDisposable
    {
        /// <summary> Объект ViewModelSecurityKeyChatAccess </summary>
        private readonly ViewModelSecurityKeyChatAccess _viewModelSecurityKeyChatAccess = new ViewModelSecurityKeyChatAccess();

        /// <summary> Конструктор </summary>
        public ViewSecurityKeyChatAccess()
        {
            InitializeComponent();
            DataContext = _viewModelSecurityKeyChatAccess;

            _viewModelSecurityKeyChatAccess.CommandContinue = new Command(obj => Continue());

            _viewModelSecurityKeyChatAccess.CommandCopyFromClipBoard = new Command(obj => CopyFromClipBoard()); 

            _viewModelSecurityKeyChatAccess.EventViewModel += _viewModelSecurityKeyChatAccess_EventViewModel;
        } 

        /// <summary> Событие в ViewModelSecurityKeyChatAccessWinidow</summary>
        private void _viewModelSecurityKeyChatAccess_EventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == "SecretKeyMissing")
            {
                WindowMain.CurrentMainWindow.GridSecurityKeyChatAccessWinidow.Visibility = Visibility.Visible;
            }
            if (e.Key == "ComingSoon")
            {
                WindowMessageBox.ShowComingSoon(this);
            }
        }

        /// <summary> Открыть окно настроек с вкладкой на импорт ключа шифрования </summary>
        private void CopyFromClipBoard()
        {
            var windowUserSettings = FactoryWindow.GetWindowUserSettings();

            if (windowUserSettings.ViewUserControl.DataContext is ViewModelUserSettings)
            {
                var viewModel = (ViewModelUserSettings)windowUserSettings.ViewUserControl.DataContext;

                viewModel.CurrentModelEnumUserSettingsGroup = viewModel.ListModelEnumUserSettingsGroup.FirstOrDefault(a => a.CodeName == "security");

                viewModel.ShowImportPanel = true;

                windowUserSettings.Show();

                windowUserSettings.Activate(); // потому что окно может быть уже открыто => нужно вывести его на первый план 

                Visibility = Visibility.Collapsed;
            } 
        }

        /// <summary> Закрыть форму </summary>
        private void Continue()
        {
            Visibility = Visibility.Collapsed;
        } 

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        { 
            _viewModelSecurityKeyChatAccess.EventViewModel -= _viewModelSecurityKeyChatAccess_EventViewModel;
        }
    }
}
