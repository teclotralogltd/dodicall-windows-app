using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewCallHistoryDetail.xaml
    /// </summary>
    public partial class ViewCallHistoryDetail : UserControl, IDisposable
    {
        /// <summary> Объект ViewModelCallHistoryDetail </summary>
        private readonly ViewModelCallHistoryDetail _viewModelCallHistoryDetail;

        /// <summary> Объект отображаемой в истории карточки контакта </summary>
        private ViewContactDetail _viewContactDetail;

        /// <summary> Конструктор </summary>
        public ViewCallHistoryDetail(ModelCallHistoryPeer modelCallHistoryPeer)
        {
            InitializeComponent();

            _viewModelCallHistoryDetail = new ViewModelCallHistoryDetail(modelCallHistoryPeer);

            DataContext = _viewModelCallHistoryDetail;
        }
        
        /// <summary> Показать окно Coming Soon </summary>
        private void ShowComingSoon(object sender, RoutedEventArgs e)
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        ///<summary> Обработчик нажатия на кнопку позвонить (из трех сервисов) </summary>
        private void ButtonPhone_OnClick(object sender, RoutedEventArgs e)
        { 
            WindowCallActive.OutgoingCall(_viewModelCallHistoryDetail.ModelContactObj);
        }

        ///<summary> Обработчик нажатия на кнопку позвонить (трубка PSNT) </summary>
        private void ButtonPhonePSNT_OnClick(object sender, RoutedEventArgs e)
        {
            WindowCallActive.OutgoingCall(_viewModelCallHistoryDetail.ModelCallHistoryPeerObj.ModelPeerObj.Identity);
        }

        ///<summary> Обработчик нажатия на кнопку чат (из трех сервисов) </summary>
        private void ButtonChat_OnClick(object sender, RoutedEventArgs e)
        {
            WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(_viewModelCallHistoryDetail.ModelContactObj));
        }

        ///<summary> Обработчик нажатия на кнопку добавить  </summary>
        private void ButtonSaveAsNew_OnClick(object sender, RoutedEventArgs e)
        {
            var number = _viewModelCallHistoryDetail.ModelCallHistoryPeerObj.ModelPeerObj.IdentityString;

            var window = new WindowStandard(new ViewContactManual(number)) { Height = 500, Width = 700, Owner = WindowMain.CurrentMainWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner };

            window.ShowDialog();
        }

        ///<summary> Обработчик нажатия на header в детализации истории </summary>
        private void HeaderContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var modelContact = _viewModelCallHistoryDetail.ModelContactObj;

            if (modelContact != null)
            {
                _viewContactDetail = new ViewContactDetail(modelContact);

                GridHistoryDitailContact.Children.Add(_viewContactDetail);

                Grid.SetRow(_viewContactDetail, 1);

                GridHistoryDetail.Visibility = Visibility.Hidden;
                GridHistoryDitailContact.Visibility = Visibility.Visible;
            } 
        }

        ///<summary> Обработчик кнопки возврата </summary>
        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            GridHistoryDitailContact.Children.Remove(_viewContactDetail);

            GridHistoryDitailContact.Visibility = Visibility.Hidden;
            GridHistoryDetail.Visibility = Visibility.Visible;
        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _viewModelCallHistoryDetail.Dispose();

            _viewContactDetail?.Dispose();
        }
    }
}
