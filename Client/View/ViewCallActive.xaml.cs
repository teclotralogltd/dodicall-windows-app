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
using DAL.Abstract;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewCallActive.xaml
    /// </summary>
    public partial class ViewCallActive : UserControl, IDisposable
    {
        /// <summary> Объект ViewModelCallActive </summary>
        private readonly ViewModelCallActive _viewModelCallActive;

        /// <summary> Конструктор </summary>
        public ViewCallActive(ModelContact modelContact)
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive(modelContact);

            DataContext = _viewModelCallActive;

            if (_viewModelCallActive.CurrentModelCall != null && _viewModelCallActive.CurrentModelCall.IsDodicall
                && _viewModelCallActive.CurrentModelCall.ModelContactObj?.ModelContactSubscriptionObj != null
                && _viewModelCallActive.CurrentModelCall.ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 /* Both */)
            {
                ButtonChat.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonChat.Visibility = Visibility.Hidden;
            }

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;

            _viewModelCallActive.CommandCallTransfer = new Command(obj => ShowCallTransferWindow());
        }

        /// <summary> Конструктор </summary>
        public ViewCallActive(string number)
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive(number);

            DataContext = _viewModelCallActive;

            if (_viewModelCallActive.CurrentModelCall != null && _viewModelCallActive.CurrentModelCall.IsDodicall
                && _viewModelCallActive.CurrentModelCall.ModelContactObj?.ModelContactSubscriptionObj != null
                && _viewModelCallActive.CurrentModelCall.ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 /* Both */)
            {
                ButtonChat.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonChat.Visibility = Visibility.Hidden;
            }

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;

            _viewModelCallActive.CommandCallTransfer = new Command(obj => ShowCallTransferWindow());
        }

        /// <summary> Конструктор </summary>
        public ViewCallActive()
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive();

            DataContext = _viewModelCallActive;

            if (_viewModelCallActive.CurrentModelCall != null && _viewModelCallActive.CurrentModelCall.ModelContactObj?.ModelContactSubscriptionObj != null
                && _viewModelCallActive.CurrentModelCall.ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 /* Both */)
            {
                ButtonChat.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonChat.Visibility = Visibility.Hidden;
            }

            StackPanelVideo.Visibility = Visibility.Collapsed;

            ImageAccept.Visibility = Visibility.Visible;

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;

            _viewModelCallActive.CommandCallTransfer = new Command(obj => ShowCallTransferWindow());
        } 

        /// <summary> Конструктор </summary>
        public ViewCallActive(ViewModelCallActive viewModelCallActive)
        {
            InitializeComponent();

            _viewModelCallActive = viewModelCallActive;

            DataContext = _viewModelCallActive;

            if (_viewModelCallActive.CurrentModelCall != null && _viewModelCallActive.CurrentModelCall.IsDodicall
                && _viewModelCallActive.CurrentModelCall.ModelContactObj?.ModelContactSubscriptionObj != null
                && _viewModelCallActive.CurrentModelCall.ModelContactObj.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj.Code == 3 /* Both */)
            {
                ButtonChat.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonChat.Visibility = Visibility.Hidden;
            }

            if (_viewModelCallActive.CurrentModelCall?.ModelEnumCallDirectionObj.Code == 1) // Incoming
            {
                StackPanelVideo.Visibility = Visibility.Collapsed;
                StackPanelCallTransfer.Visibility = Visibility.Collapsed;
                ImageAccept.Visibility = Visibility.Visible;
            }

            if (_viewModelCallActive.CurrentModelCall?.ModelEnumCallStateObj.Code == 3) // CallStateConversation
            {
                ChangeToActiveCall();
            }

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;

            _viewModelCallActive.CommandCallTransfer = new Command(obj => ShowCallTransferWindow());
        }

        /// <summary> Обработчик события EventViewModel </summary>
        private void ViewModelCallActiveOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == "ActiveCall")
            {
                ChangeToActiveCall();
            } 

            if (e.Key == "ComingSoon")
            {
                ShowComingSoon(null, null);
            }
        }

        /// <summary> Изменить форму на активный вызов </summary>
        private void ChangeToActiveCall()
        {
            if (_viewModelCallActive.CurrentModelCall.ModelEnumVoipEncryptionObj.Code == 1) /* Srtp */
            {
                ImageSrtp.Visibility = Visibility.Visible;
            }

            StackPanelVideo.Visibility = Visibility.Visible;
            StackPanelMicrophone.Visibility = Visibility.Visible;
            StackPanelAddUser.Visibility = Visibility.Visible;

            StackPanelDirection.Visibility = Visibility.Collapsed;
            TextBlockDuration.Visibility = Visibility.Visible;

            ImageAccept.Visibility = Visibility.Collapsed;

            StackPanelCallTransfer.Visibility = Visibility.Visible;

            // тут меняем форму на активный вызов !!!
        }

        /// <summary> Обработчик события CloseView </summary>
        private void ViewModelCallActiveOnCloseView(object sender, EventArgs eventArgs)
        {
            WindowMain.CloseInRightWorkspaceFront();

            _viewModelCallActive.Dispose();
        }

        /// <summary> Показать окно Coming Soon </summary>
        private void ShowComingSoon(object sender, RoutedEventArgs e)
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        /// <summary> Показать окно Перевода вызова</summary>
        private void ShowCallTransferWindow()
        { 
            var windowCallRedirect = FactoryWindow.GetWindowCallRedirect(_viewModelCallActive.CurrentModelCall);

            windowCallRedirect.Owner = WindowMain.CurrentMainWindow;
  
            windowCallRedirect.ShowDialog();
        }

        /// <summary> Исходящий звонок </summary>
        public static void OutgoingCall(ModelContact modelContact)
        {
            if (modelContact == null) return;

            WindowMain.ShowInRightWorkspaceFront(new ViewCallActive(modelContact));
        }

        /// <summary> Исходящий звонок </summary>
        public static void OutgoingCall(string number)
        {
            if (String.IsNullOrWhiteSpace(number)) return;

            WindowMain.ShowInRightWorkspaceFront(new ViewCallActive(number));
        }

        /// <summary> Принять звонок </summary>
        public static void IncomingCall()
        {
            WindowMain.ShowInRightWorkspaceFront(new ViewCallActive());
        }

        /// <summary> показать звонок </summary>
        public static void ShowCall(ViewModelCallActive viewModelCallActive)
        {
            if (viewModelCallActive == null) return;

            WindowMain.ShowInRightWorkspaceFront(new ViewCallActive(viewModelCallActive));
        }

        /// <summary> Обработчик изменения размера GridMain </summary>
        private void GridMain_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBlockTitle.Width = GridMain.ActualWidth - 10; // не забиндил в XAML потому что нужно "-10"
        }

        /// <summary> Обработчик изменения видимости </summary>
        private void ViewCallActive_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                WindowCallActive.Instance?.Hide();
            }
            else
            {
                WindowCallActive.Instance?.Show(); // перенести в Dispose с условием если есть активный то не делать Show.
            }
        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _viewModelCallActive.CloseView -= ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel -= ViewModelCallActiveOnEventViewModel;
        }

        private void StackPanelCallTransfer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                StackPanelCallTransfer.Width = 77;
            }
            else
            {
                StackPanelCallTransfer.Width = 0;
            }
        }
    }
}
