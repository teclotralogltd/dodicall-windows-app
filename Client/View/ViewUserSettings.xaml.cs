using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using dodicall.Enum;
using dodicall.Localization;
using dodicall.Window;
using DAL.Model;
using DAL.ModelEnum;
using DAL.ViewModel;
using Microsoft.Win32;
using System.IO;
using DAL.Utility;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewUserSettings.xaml
    /// </summary>
    public partial class ViewUserSettings : UserControl, IUserControlCloseWindow, IWindowCaption, IDisposable
    {
        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewUserSettings_Title";

        /// <summary> Источник данных ViewModel </summary>
        private readonly ViewModelUserSettings _viewModelUserSettings = new ViewModelUserSettings();

        /// <summary> Словарь визуального переключения групп настроек </summary>
        private Dictionary<ModelEnumUserSettingsGroup, Grid> _dictionaryUserSettingsGroup;

        /// <summary> Конструктор </summary>
        public ViewUserSettings()
        {
            InitializeComponent();

            DataContext = _viewModelUserSettings;

            var list = _viewModelUserSettings.ListModelEnumUserSettingsGroup;

            _dictionaryUserSettingsGroup = new Dictionary<ModelEnumUserSettingsGroup, Grid>
            {
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"common", StringComparison.InvariantCultureIgnoreCase)), GridCommon },
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"security", StringComparison.InvariantCultureIgnoreCase)), GridSecurity },
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"telecommunication", StringComparison.InvariantCultureIgnoreCase)), GridTelecommunication },
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"chat", StringComparison.InvariantCultureIgnoreCase)), GridChat },
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"guisettings", StringComparison.InvariantCultureIgnoreCase)), GridGuiSettings },
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"information", StringComparison.InvariantCultureIgnoreCase)), GridInformation },
                { list.FirstOrDefault(obj => obj.CodeName.Equals(@"trace", StringComparison.InvariantCultureIgnoreCase)), GridTrace }
            };

            _viewModelUserSettings.PropertyChanged += ViewModelUserSettingsOnPropertyChanged;

            _viewModelUserSettings.CloseUserSettings += (sender, args) => OnCloseWindow();

            _viewModelUserSettings.EventViewModel += ViewModelUserSettingsOnEventViewModel;

            _viewModelUserSettings.CurrentModelEnumUserSettingsGroup = list.FirstOrDefault();
                
            foreach (var modelCodecSettings in _viewModelUserSettings.CurrentModelUserSettings.ListModelCodecSettingsAudioWifi)
            {
                StackPanelCodecSettingsAudioWifi.Children.Add(CreateCodecSettingsControl(modelCodecSettings));
            }

            foreach (var modelCodecSettings in _viewModelUserSettings.CurrentModelUserSettings.ListModelCodecSettingsAudioCell)
            {
                StackPanelCodecSettingsAudioCell.Children.Add(CreateCodecSettingsControl(modelCodecSettings));
            }

            foreach (var modelCodecSettings in _viewModelUserSettings.CurrentModelUserSettings.ListModelCodecSettingsVideo)
            {
                StackPanelCodecSettingsVideo.Children.Add(CreateCodecSettingsControl(modelCodecSettings));
            } 
            
            CheckBoxTrace.IsChecked = _viewModelUserSettings.CurrentModelUserSettings.TraceMode; 
        }

        /// <summary> Обработчик смены группы настроек </summary>
        private void ViewModelUserSettingsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName.Equals(@"CurrentModelEnumUserSettingsGroup", StringComparison.InvariantCultureIgnoreCase))
            {
                foreach (var i in _dictionaryUserSettingsGroup.Values)
                {
                    i.Visibility = Visibility.Collapsed;
                }

                _dictionaryUserSettingsGroup[_viewModelUserSettings.CurrentModelEnumUserSettingsGroup].Visibility = Visibility.Visible;
            }
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
            Dispose();
        }

        /// <summary> Действие перед закрытием окна </summary>
        public bool BeforeCloseWindow()
        {
            var result = true;

            if (_viewModelUserSettings.CurrentModelUserSettingsChanged)
            {
                var resource = Application.Current.TryFindResource(@"ViewUserSettings_DontSaveSettingsMessage");
                var message = resource as string ?? String.Empty;

                var windowMessageBoxButon = WindowMessageBox.Show(this, message, WindowMessageBoxButonEnum.YesNoCancel, WindowMessageBoxTypeEnum.Question);

                if (windowMessageBoxButon == WindowMessageBoxButonEnum.Yes) _viewModelUserSettings.CommandSave.Execute(true);

                if (windowMessageBoxButon == WindowMessageBoxButonEnum.No) _viewModelUserSettings.ResetLanguage();

                if (windowMessageBoxButon == WindowMessageBoxButonEnum.Cancel) result = false;
            }

            return result;
        }

        /// <summary> Обработчик нажания кнопки отмена </summary>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCloseWindow();
        }

        /// <summary> Обработчик нажания кнопки с не реализованным функционалом </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowMessageBox.Show(this, Application.Current.TryFindResource(@"Common_ComingSoon") as string);
        }

        /// <summary> Создание элемента для настроек кодеков и привязка значения </summary>
        private Grid CreateCodecSettingsControl(ModelCodecSettings modelCodecSettings)
        {
            var grid = new Grid { Height = 50 };
            grid.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = modelCodecSettings.Name });

            var checkBox = new CheckBox { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right };
            var bind = new Binding { Path = new PropertyPath("Enabled") };
            checkBox.SetBinding(CheckBox.IsCheckedProperty, bind);
            checkBox.DataContext = modelCodecSettings;

            grid.Children.Add(checkBox);

            return grid;
        }

        /// <summary> Обработчик нажания кнопки очистки логов </summary>
        private void ButtonClearLogs_Click(object sender, RoutedEventArgs e)
        {
            WindowMessageBox.Show(this, Application.Current.TryFindResource(@"ViewUserSettings_LogClearSuccessful") as string);
        }

        /// <summary> Обработчик нажания кнопки отправить отчет об ошибке </summary>
        private void ButtonErrorSend_Click(object sender, RoutedEventArgs e)
        {
            var windowUserSettings = new WindowStandard(new ViewErrorReport())
            {
                MinHeight = 550, Height = 550, Width = 700, ResizeMode = ResizeMode.NoResize, WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = System.Windows.Window.GetWindow(this), Style = Application.Current.TryFindResource(@"VS2012MessageBoxStyle") as Style
            };

            windowUserSettings.ShowDialog();
        }

        /// <summary> Обработчик нажания ссылки о программе </summary>
        private void HyperlinkAbout_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation.ShowAbout();
        }

        /// <summary> Обработчик нажания ссылки что нового </summary>
        private void HyperlinkNews_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation.ShowNews();
        }

        /// <summary> Обработчик нажания ссылки известные проблемы </summary>
        private void HyperlinkProblems_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation.ShowProblems();
        }

        /// <summary> Обработчик нажания галочки CheckBoxTrace </summary>
        private void CheckBoxTrace_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return;

            var resource = Application.Current.TryFindResource(@"ViewUserSettings_InformationTraceMode");
            var message = resource as string ?? String.Empty;

            var windowMessageBoxButon = WindowMessageBox.Show(this, message, WindowMessageBoxButonEnum.YesNo, WindowMessageBoxTypeEnum.Question);

            if (windowMessageBoxButon == null || windowMessageBoxButon == WindowMessageBoxButonEnum.No)
            {
                CheckBoxTrace.IsChecked = false;
            }
            else
            {
                _viewModelUserSettings.CurrentModelUserSettings.TraceMode = true;
            }
        }

        /// <summary> Экспортировать ключ </summary>
        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            var windowPasswordBox = FactoryWindow.GetWindowPasswordBox(PasswordBoxTypeEnum.Export);

            windowPasswordBox.Owner = WindowMain.CurrentMainWindow;

            windowPasswordBox.ShowDialog();

            if ((windowPasswordBox.ViewUserControl as ViewPasswordBox).Result)
            {
                var encryptionkey = _viewModelUserSettings.GetUserPrivateKey();

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files | *.txt";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.FileName = "Dodicall_key.txt";

                var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var pathDownload = System.IO.Path.Combine(pathUser, "Downloads");

                saveFileDialog.InitialDirectory = pathDownload;
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, encryptionkey);
                    WindowStartup.Instance.ShowMessageTray(Application.Current.TryFindResource(@"ViewUserSettings_ExportSucced") as string);
                }
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
                var encryptionkey = _viewModelUserSettings.GetUserPrivateKey();
                Clipboard.Clear();
                Clipboard.SetText(encryptionkey);

                WindowStartup.Instance.ShowMessageTray(Application.Current.TryFindResource(@"ViewUserSettings_ExportSucced") as string);
            }
        }
         
        /// <summary> Обработчик нажатия кнопки Импортировать </summary>
        private void ButtonImport_Click(object sender, RoutedEventArgs e)
        {
            var windowPasswordBox = FactoryWindow.GetWindowPasswordBox(PasswordBoxTypeEnum.Import);

            windowPasswordBox.Owner = WindowMain.CurrentMainWindow;

            windowPasswordBox.ShowDialog();

            if ((windowPasswordBox.ViewUserControl as ViewPasswordBox).Result)
            { 
                var resultImport = _viewModelUserSettings.ImportAndSaveCryptKey();

                if (resultImport)
                {
                    ImportedPanel.Visibility = Visibility.Visible; 
                }
                else
                {
                    WindowMessageBox.Show(this, Application.Current.TryFindResource(@"ViewUserSettings_ImportFail") as string);
                } 
            }
        }

        /// <summary> Обработчик нажатия на кнопку перегенерации ключа шифрования </summary>
        private void ButtonRegenerateEncryptionKey_Click(object sender, RoutedEventArgs e)
        {
            if (WindowMessageBox.Show(FactoryWindow.GetWindowUserSettings(), Application.Current.TryFindResource(@"ViewUserSettings_CreateNewKey") as string, Application.Current.TryFindResource(@"ViewUserSettings_AlertLostCorrespondence") as string, WindowMessageBoxButonEnum.ContinueCancel, WindowMessageBoxTypeEnum.Warning) != WindowMessageBoxButonEnum.Continue) return;

            var windowPasswordBox = FactoryWindow.GetWindowPasswordBox(PasswordBoxTypeEnum.RegenerateKey);

            windowPasswordBox.Owner = System.Windows.Window.GetWindow(this);

            windowPasswordBox.ShowDialog();

            if ((windowPasswordBox.ViewUserControl as ViewPasswordBox).Result)
            {
                var resultRegenerate = _viewModelUserSettings.RegenerateKeyPair();

                if (resultRegenerate) 
                {
                    _viewModelUserSettings.ExportAviable = _viewModelUserSettings.CheckEncryptionkey(); 

                    _viewModelUserSettings.LocalSavePublicKey();

                    //Показ окна о генерации экрана, когда будет переделан механизм "растягивания" view на главном окне приложения, заменить здесь.
                    WindowMain.CurrentMainWindow.GridSecurityKeyGeneratedWinidow.Visibility = Visibility.Visible;
                } 

                OnCloseWindow();
            }
        }

        /// <summary> Показать окно Coming Soon </summary>
        private void ShowComingSoon()
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        /// <summary> Обработчик события EventViewModel </summary>
        private void ViewModelUserSettingsOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        { 
            if (e.Key == "ComingSoon")
            {
                ShowComingSoon();
            }
        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _viewModelUserSettings.PropertyChanged -= ViewModelUserSettingsOnPropertyChanged;
             
            _viewModelUserSettings.EventViewModel -= ViewModelUserSettingsOnEventViewModel; 
        }

        /// <summary> Обработчик отжания галочки CheckBoxTrace </summary>
        private void CheckBoxTrace_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _viewModelUserSettings.CurrentModelUserSettings.TraceMode = false;
        }

        /// <summary> Обработчик нажатия по ссылке PrivacyPolicy </summary>
        private void HyperlinkPrivacyPolicy_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            UtilityWeb.OpenUrl(e.Uri.AbsoluteUri);
        }

        /// <summary> Обработчик нажатия галочки Белый список </summary>
        private void CheckBoxDoNotDesturb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)CheckBoxDoNotDesturb.IsChecked) return;

            if(_viewModelUserSettings.CountWhiteContact == 0)
            {
                var message = Application.Current.TryFindResource(@"ViewUserSettings_MustAddContactsToTheWhitelist") as string;

                var windowMessageBoxButon = WindowMessageBox.Show(this, message, WindowMessageBoxButonEnum.Ok, WindowMessageBoxTypeEnum.Information);
            }
            else
            {
                var message = Application.Current.TryFindResource(@"ViewUserSettings_ReceiveIncomingCallsOnlyWhitelist") as string;

                var windowMessageBoxButon = WindowMessageBox.Show(this, message, WindowMessageBoxButonEnum.OkCancel, WindowMessageBoxTypeEnum.Question);

                if (windowMessageBoxButon == WindowMessageBoxButonEnum.Ok) _viewModelUserSettings.CommandEnableWriteList.Execute(true);
            }
        }
    }
}
