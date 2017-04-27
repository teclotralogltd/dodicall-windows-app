using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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
using dodicall.Window;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewChat.xaml
    /// </summary>
    public partial class ViewChat : UserControl, INotifyPropertyChanged
    {
        ///<summary> Текущий экземпляр </summary>
        public static ViewChat CurrentViewChat { get; private set; }

        /// <summary> Событие изменения кол-ва непрочитанных </summary>
        public event EventHandler<int> CountUnreadMessageChanged;

        ///<summary> Объект ViewModelChat </summary>
        private readonly ViewModelChat _viewModelChat = new ViewModelChat();

        ///<summary> Ширина контакта в списке контактов </summary>
        private double _widthChatItem;

        ///<summary> Ширина контакта в списке контактов </summary>
        public double WidthChatItem
        {
            get { return _widthChatItem; }
            set
            {
                _widthChatItem = value;
                OnPropertyChanged("WidthChatItem");
            }
        }

        ///<summary> Конструктор </summary>
        public ViewChat()
        {
            InitializeComponent();

            _viewModelChat.PropertyChanged += ViewModelChatOnPropertyChanged;

            DataContext = _viewModelChat;

            CurrentViewChat = this;
        }

        ///<summary> Обработчик изменения ViewModelChat </summary>
        private void ViewModelChatOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentListModelChat", StringComparison.InvariantCultureIgnoreCase))
            {
                int count;

                var viewChatMessageDetail = WindowMain.GetRightWorkspaceContent() as ViewChatMessageDetail;

                if (viewChatMessageDetail != null && viewChatMessageDetail.IsScrollAtBottom)
                {
                    var chatId = viewChatMessageDetail.CurrentModelChat.Id;

                    var list = _viewModelChat.CurrentListModelChat.Where(obj => obj.Id != chatId);

                    count = list.Sum(obj => obj.NewMessagesCount);
                }
                else
                {
                    count = _viewModelChat.CurrentListModelChat.Sum(obj => obj.NewMessagesCount);
                }

                OnCountUnreadMessageChanged(count);
            }
        }

        ///<summary> Обработчик изменения размера ListBoxChat </summary>
        private void ListBoxChat_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                WidthChatItem = e.NewSize.Width - 43;
            }
        }

        ///<summary> Выбрать чат </summary>
        public void SetModelChat(ModelChat modelChat)
        {
            var modelChatFromList = _viewModelChat.GetChatFromList(modelChat);

            ListBoxChat.SelectedItem = modelChatFromList;
        }

        ///<summary> Выбрать чат </summary>
        public void SetModelChat(ModelContact modelContact)
        {
            var modelChat = _viewModelChat.GetChatFromList(modelContact);

            ListBoxChat.SelectedItem = modelChat;
        }

        /// <summary> Инвокатор события CountUnreadMessageChanged </summary>
        private void OnCountUnreadMessageChanged(int count)
        {
            CountUnreadMessageChanged?.Invoke(this, count);
        }
        
        ///<summary> Обработчик смены текущего чата </summary>
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var modelChat = ListBoxChat.SelectedItem as ModelChat;

            if (modelChat == null) return;

            WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(modelChat));
        }

        ///<summary> Обработчик нажатия на пункт меню позвонить </summary>
        private void ContextMenuCall_OnClick(object sender, RoutedEventArgs e)
        {
            var modelContactChat = _viewModelChat.CurrentModelChat.ModelContactChat;

            if (modelContactChat != null)
            {
                WindowCallActive.OutgoingCall(modelContactChat);
            } 
        }

        ///<summary> Обработчик нажатия на пункт меню переименовать </summary>
        private void ContextMenuRename_OnClick(object sender, RoutedEventArgs e)
        {
            var modelChat = ListBoxChat.SelectedItem as ModelChat;

            if (modelChat == null) return;

            //var rightWorkspaceContent = WindowMain.GetRightWorkspaceContent();
            //var existChatMessageDetail = rightWorkspaceContent is ViewChatMessageDetail;

            //var chatMessageDetail = existChatMessageDetail ? rightWorkspaceContent as ViewChatMessageDetail : null;

            //var createNewViewChatMessageDetail = chatMessageDetail != null ? chatMessageDetail.CurrentModelChat.Id != _viewModelChat.CurrentModelChat.Id : true;

            var chatMessageDetail = WindowMain.GetRightWorkspaceContent() as ViewChatMessageDetail;

            if (chatMessageDetail?.CurrentModelChat.Id != _viewModelChat.CurrentModelChat.Id)
            {
                var viewChatMessageDetail = new ViewChatMessageDetail(modelChat);
                WindowMain.ShowInRightWorkspace(viewChatMessageDetail);
                viewChatMessageDetail.TitleChatEditMouseButtonDown(null, null);
            }
            else
            {
                chatMessageDetail?.TitleChatEditMouseButtonDown(null, null);
            }
        }

        ///<summary> Обработчик нажатия на пункт меню учасники чата </summary>
        private void ContextMenuChatMembers_OnClick(object sender, RoutedEventArgs e)
        {
            var modelChat = ListBoxChat.SelectedItem as ModelChat;

            if (modelChat == null) return;

            //var rightWorkspaceContent = WindowMain.GetRightWorkspaceContent();
            //var existChatMessageDetail = rightWorkspaceContent != null ? rightWorkspaceContent is ViewChatMessageDetail : true;

            //var chatMessageDetail = existChatMessageDetail ? rightWorkspaceContent as ViewChatMessageDetail : null;

            //var createNewViewChatMessageDetail = chatMessageDetail != null ? chatMessageDetail.CurrentModelChat.Id != _viewModelChat.CurrentModelChat.Id: false;

            var chatMessageDetail = WindowMain.GetRightWorkspaceContent() as ViewChatMessageDetail;

            if (chatMessageDetail?.CurrentModelChat.Id != _viewModelChat.CurrentModelChat.Id)
            {
                var viewChatMessageDetail = new ViewChatMessageDetail(modelChat);
                WindowMain.ShowInRightWorkspace(viewChatMessageDetail);
                viewChatMessageDetail.OnInviteMembers_MouseClick(null, null);
            }
            else
            {
                chatMessageDetail?.OnInviteMembers_MouseClick(null, null);
            }
        }

        ///<summary> Обработчик нажатия на пункт меню удалить </summary>
        private void ContextMenuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelChat.CommandDeleteChat.Execute(null); 
        }

        ///<summary> Обработчик нажатия на пункт меню удалить </summary>
        private void ContextMenuMarkReadAll_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModelChat.CommandMarkReadAll.Execute(null);
        }

        #region Реализация INotifyPropertyChanged, т.к. множественное наследование в C# запрещено и отнаследовать UserControl от своего класса не возможно

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        #endregion
    }
}
