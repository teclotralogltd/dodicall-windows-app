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
using DAL.Model;
using System.Diagnostics;
using dodicall.Window;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewChatRedirect.xaml
    /// </summary>
    public partial class ViewChatRedirect : UserControl, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Объект ViewModelChatRedirect </summary>
        private ViewModelChatRedirect _viewModelChatRedirect;

        /// <summary> Список пересылаемых сообщений </summary>
        private List<ModelChatMessage> _listRedirectModelMessage;

        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewChatRedirect_Title";

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Конструктор </summary>
        public ViewChatRedirect(List<ModelChatMessage> listModelMessage)
        {
            InitializeComponent();

            _listRedirectModelMessage = listModelMessage;

            _viewModelChatRedirect = new ViewModelChatRedirect();

            DataContext = _viewModelChatRedirect;
        }

        /// <summary> Обработчик закрытия окна </summary>
        public bool BeforeCloseWindow()
        {
            return true;
        }

        /// <summary> Обработчик нажатия на иконку контакты </summary>
        private void ImageContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxContact.Visibility = Visibility.Visible;
            ListBoxChat.Visibility = Visibility.Collapsed;
        }

        /// <summary> Обработчик нажатия на иконку чаты </summary>
        private void ImageChat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxContact.Visibility = Visibility.Collapsed;
            ListBoxChat.Visibility = Visibility.Visible;
        }

        /// <summary> Обработчик выбора чата </summary>
        private void ItemChat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var modelChat = ListBoxChat.SelectedItem as ModelChat;

            WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(modelChat, _listRedirectModelMessage));

            ViewChat.CurrentViewChat.SetModelChat(modelChat);
            
            OnCloseWindow();
        }

        /// <summary> Обработчик выбора контакта </summary>
        private void ItemContact_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var modelContact = ListBoxContact.SelectedItem as ModelContact;

            WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(modelContact, _listRedirectModelMessage));

            ViewChat.CurrentViewChat.SetModelChat(modelContact);

            OnCloseWindow();
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Обработчк нажатия отмены </summary>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCloseWindow();
        }

        ///<summary> Обработчик получения фокуса поля поиска </summary>
        private void TextBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBlockSearch.Visibility = Visibility.Collapsed;
        }

        ///<summary> Обработчик потери фокуса поля поиска </summary>
        private void TextBoxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxSearch.Text)) TextBlockSearch.Visibility = Visibility.Visible;
        }
    }
}
