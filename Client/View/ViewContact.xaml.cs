using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dodicall.Notification;
using dodicall.Window;
using DAL.Abstract;
using DAL.Model;
using DAL.Utility;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewContact.xaml
    /// </summary>
    public partial class ViewContact : UserControl,  INotifyPropertyChanged
    {
        ///<summary> Текущий экземпляр </summary>
        public static ViewContact CurrentViewContact { get; private set; }

        ///<summary> Кол-во непрочитанных сообщений (кастыль для нотификации, потом переделать на единый механизм) </summary>
        public int _countUnreadMessage;

        ///<summary> Кол-во пропущеных звонков (кастыль для нотификации, потом переделать на единый механизм) </summary>
        public int _countMissedCall;

        ///<summary> Кол-во пропущеных звонков (кастыль для нотификации, потом переделать на единый механизм) </summary>
        public int _countInvateUnread;

        /// <summary> Текущий список контактов </summary>
        public List<ModelContact> ListModelContact => _viewModelContact.ListModelContact;

        ///<summary> Ширина контакта в списке контактов </summary>
        private double _widthContactItem;

        ///<summary> Ширина контакта в списке контактов </summary>
        public double WidthContactItem
        {
            get { return _widthContactItem; }
            set { _widthContactItem = value; OnPropertyChanged("WidthContactItem"); }
        }

        ///<summary> Объект ViewModelContact </summary>
        private readonly ViewModelContact _viewModelContact = new ViewModelContact();

        ///<summary> Конструктор </summary>
        public ViewContact()
        {
            InitializeComponent();

            CurrentViewContact = this;

            DataContext = _viewModelContact;

            _viewModelContact.PropertyChanged += ViewModelContactOnPropertyChanged;

            ContextMenuFilterContact.PlacementTarget = ImageFilterContact;
            ContextMenuFilterContact.HorizontalOffset = 3;
            ContextMenuFilterContact.VerticalOffset = 3;
            ContextMenuFilterContact.Placement = PlacementMode.RelativePoint;

            GridContact_Click(null, null);

            ChangeCountInvateUnread();

            var viewChat = new ViewChat();

            viewChat.CountUnreadMessageChanged += ViewChatOnCountUnreadMessageChanged;

            GridChat.Children.Add(viewChat);

            var viewCall = new ViewCall();

            viewCall.PropertyChanged += ViewCallOnPropertyChanged;

            GridCall.Children.Add(viewCall);

            ViewChatOnCountUnreadMessageChanged(null, ((ViewModelChat)viewChat.DataContext).CurrentListModelChat.Sum(obj => obj.NewMessagesCount));

            ViewCallOnPropertyChanged(viewCall, new PropertyChangedEventArgs("CountMissedCall"));

            GridDialpad.Children.Add(new ViewDialpad());

            _viewModelContact.DeleteModelContactSuccessful += (sender, contact) => DeleteContactListBoxContact(contact);
        }

        ///<summary> Обработчик изменения ViewCall </summary>
        private void ViewCallOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == @"CountMissedCall")
            {
                var viewCall = (ViewCall)sender;

                if (_countMissedCall < viewCall.CountMissedCall) WindowMain.CurrentMainWindow.HighlightIconTaskPanel();

                _countMissedCall = viewCall.CountMissedCall;

                GridCountUnreadCall.Visibility = viewCall.CountMissedCall > 0 ? Visibility.Visible : Visibility.Collapsed;

                RectangleGridCountUnreadCall.Width = viewCall.CountMissedCall > 9 ? 23 : 16;

                LabelGridCountUnreadCall.Content = viewCall.CountMissedCall > 99 ? @"99+" : viewCall.CountMissedCall.ToString();

                Notify();
            }
        }

        ///<summary> Обработчик изменения CountUnreadMessageChanged </summary>
        private void ViewChatOnCountUnreadMessageChanged(object sender, int countUnreadMessage)
        {
            if (_countUnreadMessage < countUnreadMessage) WindowMain.CurrentMainWindow.HighlightIconTaskPanel();

            _countUnreadMessage = countUnreadMessage;

            GridCountUnreadMessage.Visibility = countUnreadMessage > 0 ? Visibility.Visible : Visibility.Collapsed;

            RectangleGridCountUnreadMessage.Width = countUnreadMessage > 9 ? 23 : 16;

            LabelGridCountUnreadMessage.Content = countUnreadMessage > 99 ? @"99+" : countUnreadMessage.ToString();

            Notify();
        }

        ///<summary> Обработчик изменения ViewModelContact </summary>
        private void ViewModelContactOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CountInvateUnread", StringComparison.InvariantCultureIgnoreCase)) ChangeCountInvateUnread();
        }

        ///<summary> Обработчик изменения CountInvateUnread </summary>
        private void ChangeCountInvateUnread()
        {
            if (_countInvateUnread < _viewModelContact.CountInvateUnread) WindowMain.CurrentMainWindow.HighlightIconTaskPanel();

            _countInvateUnread = _viewModelContact.CountInvateUnread;

            DockPanelCountInvateUnread.Visibility = _viewModelContact.CountInvateUnread > 0 ? Visibility.Visible : Visibility.Hidden;
            GridCountInvateUnread.Visibility = _viewModelContact.CountInvateUnread > 0 ? Visibility.Visible : Visibility.Collapsed;

            RectangleDockPanelCountInvateUnread.Width = _viewModelContact.CountInvateUnread > 9 ? 25 : 16;
            RectangleGridCountInvateUnread.Width = _viewModelContact.CountInvateUnread > 9 ? 25 : 16;

            Notify();
        }

        ///<summary> Обработчик нотификаций пропущеных событий </summary>
        private void Notify()
        {
            var notificationCount = _countUnreadMessage + _countMissedCall + _viewModelContact.CountInvateUnread;

            PushNotification.IconNotification(notificationCount);
        }

        ///<summary> Обработчик нажатия кнопки контактов </summary>
        private void GridContact_Click(object sender, RoutedEventArgs e)
        {
            RectangleContact.Fill = Brushes.Red;
            RectangleHistory.Fill = Brushes.Transparent;
            RectangleChat.Fill = Brushes.Transparent;
            RectangleDialpad.Fill = Brushes.Transparent;
            //TextBlockContact.Foreground = Brushes.Black;
            //TextBlockHistory.Foreground = Brushes.Silver;
            //TextBlockChat.Foreground = Brushes.Silver;
            ImageContact.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/contacts_active.png");
            ImageHistory.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/history_inactive.png");
            ImageChat.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/chat_inactive.png");
            ImageDialpad.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/dialpad_inactive.png");

            _viewModelContact.ContactList = true;
            _viewModelContact.HistoryList = false;
            _viewModelContact.ChatList = false;
            _viewModelContact.DialpadList = false;
        }

        ///<summary> Обработчик нажатия кнопки истории </summary>
        private void GridHistory_Click(object sender, RoutedEventArgs e)
        {
            RectangleContact.Fill = Brushes.Transparent;
            RectangleHistory.Fill = Brushes.Red;
            RectangleChat.Fill = Brushes.Transparent;
            RectangleDialpad.Fill = Brushes.Transparent;
            //TextBlockContact.Foreground = Brushes.Silver;
            //TextBlockHistory.Foreground = Brushes.Black;
            //TextBlockChat.Foreground = Brushes.Silver;
            ImageContact.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/contacts_inactive.png");
            ImageHistory.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/history_active.png");
            ImageChat.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/chat_inactive.png");
            ImageDialpad.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/dialpad_inactive.png");

            _viewModelContact.ContactList = false;
            _viewModelContact.HistoryList = true;
            _viewModelContact.ChatList = false;
            _viewModelContact.DialpadList = false;
        }

        ///<summary> Обработчик нажатия кнопки чаты </summary>
        private void GridChat_Click(object sender, RoutedEventArgs e)
        {
            RectangleContact.Fill = Brushes.Transparent;
            RectangleHistory.Fill = Brushes.Transparent;
            RectangleChat.Fill = Brushes.Red;
            RectangleDialpad.Fill = Brushes.Transparent;
            //TextBlockContact.Foreground = Brushes.Silver;
            //TextBlockHistory.Foreground = Brushes.Silver;
            //TextBlockChat.Foreground = Brushes.Black;
            ImageContact.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/contacts_inactive.png");
            ImageHistory.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/history_inactive.png");
            ImageChat.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/chat_active.png");
            ImageDialpad.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/dialpad_inactive.png");

            _viewModelContact.ContactList = false;
            _viewModelContact.HistoryList = false;
            _viewModelContact.ChatList = true;
            _viewModelContact.DialpadList = false;
        }

        ///<summary> Обработчик нажатия кнопки диалпада </summary>
        private void GridDialpad_Click(object sender, RoutedEventArgs e)
        {
            RectangleContact.Fill = Brushes.Transparent;
            RectangleHistory.Fill = Brushes.Transparent;
            RectangleChat.Fill = Brushes.Transparent;
            RectangleDialpad.Fill = Brushes.Red;
            //TextBlockContact.Foreground = Brushes.Silver;
            //TextBlockHistory.Foreground = Brushes.Silver;
            //TextBlockChat.Foreground = Brushes.Black;
            ImageContact.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/contacts_inactive.png");
            ImageHistory.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/history_inactive.png");
            ImageChat.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/chat_inactive.png");
            ImageDialpad.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/dialpad_active.png");

            _viewModelContact.ContactList = false;
            _viewModelContact.HistoryList = false;
            _viewModelContact.ChatList = false;
            _viewModelContact.DialpadList = true;
        }

        ///<summary> Открыть вкладку контактов и выбрать контакт </summary>
        public void OpenContact(ModelContact modelContact)
        {
            GridContact_Click(null, null);

            var modelContactFromList = _viewModelContact.CurrentListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

            if (modelContactFromList != null) _viewModelContact.CurrentModelContact = modelContactFromList;
        }

        ///<summary> Открыть вкладку вызовов и выбрать вызов </summary>
        public void OpenCall(ModelCall modelCall)
        {
            GridHistory_Click(null, null);
        }

        ///<summary> Открыть вкладку чатов и выбрать чат </summary>
        public void OpenChat(ModelChat modelChat)
        {
            GridChat_Click(null, null);

            ViewChat.CurrentViewChat.SetModelChat(modelChat);
        }

        ///<summary> Обработчик нажатия на надпись поиска </summary>
        private void TextBlockSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockSearch.Visibility = Visibility.Collapsed;

            TextBoxSearch.Focus();
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

        ///<summary> Обработчик нажатия по пункту меню Все контакты </summary>
        private void MenuItemAllContact_OnClick(object sender, RoutedEventArgs e)
        {  
            // в XAML не работает прибинденная команда, хз почему, нет времени разбераться, по этому сделал так !!!
            _viewModelContact.CommandApplyFilterAllContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню Dodicall контакты </summary>
        private void MenuItemDodicallContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда, хз почему, нет времени разбераться, по этому сделал так !!!
            _viewModelContact.CommandApplyFilterDodicallContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню Сохраненные контакты </summary>
        private void MenuItemSavedContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда, хз почему, нет времени разбераться, по этому сделал так !!!
            _viewModelContact.CommandApplyFilterSavedContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню заблокированные контакты </summary>
        private void MenuItemBlockedContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда, хз почему, нет времени разбераться, по этому сделал так !!!
            _viewModelContact.CommandApplyFilterBlockedContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню белые контакты </summary>
        private void MenuItemWhiteContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда, хз почему, нет времени разбераться, по этому сделал так !!!
            _viewModelContact.CommandApplyFilterWhiteContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по фильтру списка контактов </summary>
        private void StackPanelFilterContact_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenuFilterContact.IsOpen = true;
        }

        ///<summary> Пересчитать непрочитанные приглашения </summary>
        public void RecalculateUnreadInvite()
        {
            _viewModelContact.ChangedInviteUnread();
        }

        ///<summary> Обработчик нажатия на пункт меню позвонить </summary>
        private void ContextMenuCall_OnClick(object sender, RoutedEventArgs e)
        {
            WindowCallActive.OutgoingCall(_viewModelContact.CurrentModelContact);
        }

        ///<summary> Обработчик нажатия на пункт меню отправить сообщение </summary>
        private void ContextMenuWriteMessage_OnClick(object sender, RoutedEventArgs e)
        {
            WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(_viewModelContact.CurrentModelContact));
        }

        ///<summary> Обработчик нажатия на пункт меню удалить </summary>
        private void ContextMenuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelContact.CommandDeleteModelContact.Execute(null);
        }

        ///<summary> Обработчик нажатия на пункт меню Разблокировать </summary>
        private void ContextMenuUnblock_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelContact.CommandUnblockModelContact.Execute(null);
        }

        ///<summary> Обработчик нажатия на пункт меню Заблокировать </summary>
        private void ContextMenuBlock_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelContact.CommandBlockModelContact.Execute(null);
        }

        ///<summary> Обработчик нажатия на пункт меню Удалить из белого списка </summary>
        private void ContextMenuDeleteFromWhite_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelContact.CommandDeleteFromWhiteModelContact.Execute(null);
        }

        ///<summary> Обработчик нажатия на пункт меню добавить в белый список </summary>
        private void ContextMenuAddToWhite_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelContact.CommandAddToWhiteModelContact.Execute(null);
        }
         
        ///<summary> Обработчик нажатия по контакту в списке контактов </summary>
        private void GridContactItem_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var modelContact = (sender as Grid)?.DataContext as ModelContact;

            OpenModelContactDetail(modelContact);
        }

        ///<summary> Открыть карточку контакта </summary>
        private void OpenModelContactDetail(ModelContact modelContact)
        {
            if (modelContact == null) return;

            var userControl = WindowMain.GetRightWorkspaceContent();

            if (userControl != null && (userControl as ViewContactDetail)?.CurrentModelContact.Id == modelContact.Id)
            {
                var userControlFront = WindowMain.GetRightWorkspaceFrontContent();

                // проверить на null и попробовать привести к ViewCallActive
                if (userControlFront != null && userControlFront is ViewCallActive)
                {
                    WindowMain.CloseInRightWorkspaceFront();
                }

                return;
            }

            WindowMain.ShowInRightWorkspace(new ViewContactDetail(modelContact));
        }

        ///<summary> Обработчик смены текущего выделенного контакта </summary>
        private void ListBoxContact_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var modelContact = ListBoxContact.SelectedItem as ModelContact;

            OpenModelContactDetail(modelContact);
        }

        ///<summary> Обработчик изменения размера ListBoxContact </summary>
        private void ListBoxContact_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                WidthContactItem = e.NewSize.Width - 43;
            }
        }

        ///<summary> Обработчик нажатия на кнопку поиска новых контактов </summary>
        private void ButtonFindNewContact_OnClick(object sender, RoutedEventArgs e)
        {
            var viewContactDirectory = new ViewContactDirectory();

            WindowMain.ShowInRightWorkspace(viewContactDirectory);

            GridFindNewContact.Background = Brushes.WhiteSmoke;

            viewContactDirectory.IsVisibleChanged += ViewContactDirectoryOnIsVisibleChanged;
        }

        ///<summary> Обработчик изменения видимости поиска контактов в директории </summary>
        private void ViewContactDirectoryOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(WindowMain.GetRightWorkspaceContent() is ViewContactDirectory))
            {
                GridFindNewContact.Background = Brushes.Transparent;
            }
        }

        ///<summary> Обработчик нажатия на кнопку приглашений и запросов </summary>
        private void ButtonRequestInvite_OnClick(object sender, RoutedEventArgs e)
        {
            var viewContactRequestInvite = new ViewContactRequestInvite();

            WindowMain.ShowInRightWorkspace(viewContactRequestInvite);

            GridRequestInvite.Background = Brushes.WhiteSmoke;

            viewContactRequestInvite.IsVisibleChanged += ViewContactRequestInviteOnIsVisibleChanged;
        }

        ///<summary> Обработчик изменения видимости приглашений и запросов </summary>
        private void ViewContactRequestInviteOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(WindowMain.GetRightWorkspaceContent() is ViewContactRequestInvite))
            {
                GridRequestInvite.Background = Brushes.Transparent;
            }
        }

        ///<summary> Установить выделение на контакте (Временный кастыль для демо !!!) </summary>
        private void SetSelectedListBoxContact(ModelContact modelContact)
        {
            var modelContactFromList = _viewModelContact.CurrentListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

            if (modelContactFromList != null) _viewModelContact.CurrentModelContact = modelContactFromList;
        }

        ///<summary> Добавить контакт в список (Временный кастыль для демо !!!) </summary>
        public void AddContactListBoxContact(ModelContact modelContact, bool showDetailInRightWorkspace)
        {
            var modelContactFromList = _viewModelContact.CurrentListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

            if (modelContactFromList == null)
            {
                _viewModelContact.ListModelContact.Add(modelContact);
                _viewModelContact.ApplyFilter();
            }

            if (showDetailInRightWorkspace)
            {
                SetSelectedListBoxContact(modelContact);
                WindowMain.ShowInRightWorkspace(new ViewContactDetail(modelContact));
            }
        }

        public void ApplyFilterListModelContact()
        {
            _viewModelContact.ApplyFilter();
        }

        ///<summary> Удалить контакт из списока (Временный кастыль для демо !!!) </summary>
        public void DeleteContactListBoxContact(ModelContact modelContact)
        {
            var modelContactFromList = _viewModelContact.CurrentListModelContact.FirstOrDefault(obj => obj.Id == modelContact.Id);

            if (modelContactFromList != null)
            {
                _viewModelContact.ListModelContact.Remove(modelContact);
                _viewModelContact.ApplyFilter();
            }

            var userControl = WindowMain.GetRightWorkspaceContent() as ViewContactDetail;

            if (userControl != null && userControl.CurrentModelContact.Id == modelContact.Id)
            {
                WindowMain.ShowInRightWorkspace(null);
            }
        }

        #region Реализация INotifyPropertyChanged, т.к. множественное наследование в C# запрещено и отнаследовать UserControl от своего класса не возможно

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            CheckPropertyName(e.PropertyName);

            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        [Conditional("DEBUG")]
        private void CheckPropertyName(string propertyName)
        {
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];
            if (propertyDescriptor == null)
            {
                throw new InvalidOperationException($@"Свойства с именем '{propertyName}' не существует.");
            }
        }

        #endregion
    }
}
