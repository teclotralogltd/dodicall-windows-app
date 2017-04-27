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
using dodicall.View;
using DAL.Model;
using DAL.ViewModel;

namespace dodicall.Window
{
    /// <summary>
    /// Interaction logic for WindowCallActive.xaml
    /// </summary>
    public partial class WindowCallActive
    {
        /// <summary> Объект WindowCallActive </summary>
        public static WindowCallActive Instance { get; private set; }

        /// <summary> Объект ViewModelCallActive </summary>
        private readonly ViewModelCallActive _viewModelCallActive;

        /// <summary> Конструктор </summary>
        private WindowCallActive(ModelContact modelContact)
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive(modelContact);

            DataContext = _viewModelCallActive;

            Instance = this;

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;
        }

        /// <summary> Конструктор </summary>
        private WindowCallActive(string number)
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive(number);

            DataContext = _viewModelCallActive;

            Instance = this;

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;
        }

        /// <summary> Конструктор </summary>
        private WindowCallActive(ModelContact modelContact, string number)
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive(modelContact, number);

            DataContext = _viewModelCallActive;

            Instance = this;

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;
        }

        /// <summary> Конструктор </summary>
        private WindowCallActive()
        {
            InitializeComponent();

            _viewModelCallActive = new ViewModelCallActive();

            DataContext = _viewModelCallActive;

            Instance = this;

            ImageAccept.Visibility = Visibility.Visible;

            _viewModelCallActive.CloseView += ViewModelCallActiveOnCloseView;

            _viewModelCallActive.EventViewModel += ViewModelCallActiveOnEventViewModel;
        }

        /// <summary> Обработчик события EventViewModel </summary>
        private void ViewModelCallActiveOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == "ActiveCall")
            {
                if (_viewModelCallActive.CurrentModelCall.ModelEnumVoipEncryptionObj.Code == 1) /* Srtp */
                {
                    ImageSrtp.Visibility = Visibility.Visible;
                }

                ImageMicrophoneOn.Visibility = Visibility.Visible;

                TextBlockDirection.Visibility = Visibility.Collapsed;
                TextBlockDuration.Visibility = Visibility.Visible;

                ImageAccept.Visibility = Visibility.Collapsed;

                // тут меняем форму на активный вызов !!!
            }

            if (e.Key == "Mute")
            {
                ImageMicrophoneOn.Visibility = _viewModelCallActive.Mute ? Visibility.Hidden : Visibility.Visible;
                ImageMicrophoneOff.Visibility = _viewModelCallActive.Mute ? Visibility.Visible : Visibility.Hidden;
            }
        }

        /// <summary> Обработчик события CloseView </summary>
        private void ViewModelCallActiveOnCloseView(object sender, EventArgs eventArgs)
        {
            Instance = null;

            _viewModelCallActive.Dispose();

            Close();
        }

        /// <summary> Исходящий звонок (для контакта) </summary>
        public static void OutgoingCall(ModelContact modelContact)
        {
            if (modelContact == null || ViewModelCallActive.ExistCall) return;

            if (Instance == null)
            {
                var windowCall = new WindowCallActive(modelContact);

                windowCall.Show();
            }
        }

        /// <summary> Исходящий звонок (для номера) </summary>
        public static void OutgoingCall(string number)
        {
            if (String.IsNullOrWhiteSpace(number) || ViewModelCallActive.ExistCall) return;

            if (Instance == null)
            {
                var windowCall = new WindowCallActive(number);

                windowCall.Show();
            }
        }

        /// <summary> Исходящий звонок (для контакта и его конкретного номера) </summary>
        public static void OutgoingCall(ModelContact modelContact, string number)
        {
            if (String.IsNullOrWhiteSpace(number) || modelContact == null || ViewModelCallActive.ExistCall) return;

            if (Instance == null)
            {
                var windowCall = new WindowCallActive(modelContact, number);

                windowCall.Show();
            }
        }

        /// <summary> Принять звонок </summary>
        public static void IncomingCall()
        {
            if (Instance == null)
            {
                var windowCall = new WindowCallActive();

                windowCall.Show();
            }
        }

        /// <summary> Обработчик двойного клика по окну </summary>
        private void WindowCall_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var windowMain = WindowMain.CurrentMainWindow;

            windowMain.ShowWindow();

            ViewCallActive.ShowCall(_viewModelCallActive);
        }
    }
}
