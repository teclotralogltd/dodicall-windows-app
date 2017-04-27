using System;
using System.Collections.Generic;
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
using dodicall.Window;
using DAL.Model;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewCall.xaml
    /// </summary>
    public partial class ViewCall : UserControl, INotifyPropertyChanged
    {
        /// <summary> Объект ViewModelCallHistory </summary>
        private readonly ViewModelCallHistory _viewModelCallHistory = new ViewModelCallHistory();

        ///<summary> Ширина контакта в списке контактов </summary>
        private double _widthChatItem;

        ///<summary> Ширина контакта в списке контактов </summary>
        public double WidthChatItem
        {
            get { return _widthChatItem; }
            set { _widthChatItem = value; OnPropertyChanged("WidthChatItem"); }
        }

        ///<summary> Кол-во пропущеных вызовов </summary>
        private int _countMissedCall;

        ///<summary> Кол-во пропущеных вызовов </summary>
        public int CountMissedCall
        {
            get { return _countMissedCall; }
            set { _countMissedCall = value; OnPropertyChanged("CountMissedCall"); }
        }

        /// <summary> Конструктор </summary>
        public ViewCall()
        {
            InitializeComponent();

            DataContext = _viewModelCallHistory;

            CountMissedCall = _viewModelCallHistory.CurrentModelCallHistory.TotalMissed;

            _viewModelCallHistory.CurrentModelCallHistory.PropertyChanged += (sender, args) => { if (args.PropertyName == @"TotalMissed") CountMissedCall = _viewModelCallHistory.CurrentModelCallHistory.TotalMissed; };
        }

        ///<summary> Обработчик изменения видимости View </summary>
        private void ViewCall_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue == false) _viewModelCallHistory.SetModelCallHistoryReadedAll();
        }

        ///<summary> Обработчик изменения размера ListBoxChat </summary>
        private void ListBoxCallHistory_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                WidthChatItem = e.NewSize.Width - 43;
            }
        }

        ///<summary> Обработчик изменения выбора ListBoxCallHistory </summary>
        private void ListBoxCallHistory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxCallHistory.SelectedItem != null)
            {
                var modelCallHistoryPeer = (ModelCallHistoryPeer)ListBoxCallHistory.SelectedItem;

                _viewModelCallHistory.SetModelCallHistoryReaded(modelCallHistoryPeer);

                WindowMain.ShowInRightWorkspace(new ViewCallHistoryDetail(modelCallHistoryPeer));
            } 
        }

        ///<summary> Обработчик клика по элементу звонка в ListBoxCallHistory </summary>
        private void GridCallItem_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ListBoxCallHistory.SelectedItem != null && !(WindowMain.GetRightWorkspaceContent() is ViewCallHistoryDetail))
            {
                var modelCallHistoryPeer = (ModelCallHistoryPeer)ListBoxCallHistory.SelectedItem;

                _viewModelCallHistory.SetModelCallHistoryReaded(modelCallHistoryPeer);

                WindowMain.ShowInRightWorkspace(new ViewCallHistoryDetail(modelCallHistoryPeer));
            }
        }

        ///<summary> Обработчик нажатия на пункт меню позвонить </summary>
        private void ContextMenuCall_OnClick(object sender, RoutedEventArgs e)
        {
            if (_viewModelCallHistory.CurrentModelCallHistoryPeer.ModelPeerObj.ExistModelContact)
            {
                WindowCallActive.OutgoingCall(_viewModelCallHistory.CurrentModelCallHistoryPeer.ModelPeerObj.ModelContactObj);
            }
            else
            {
                WindowCallActive.OutgoingCall(_viewModelCallHistory.CurrentModelCallHistoryPeer.ModelPeerObj.IdentityString);
            } 
        }

        ///<summary> Обработчик нажатия на пункт меню отправить сообщение</summary>
        private void ContextMenuWriteMessage_OnClick(object sender, RoutedEventArgs e)
        {  
            if (_viewModelCallHistory.CurrentModelCallHistoryPeer.ModelPeerObj.IsDodicall)
                WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(_viewModelCallHistory.CurrentModelCallHistoryPeer.ModelPeerObj.ModelContactObj));
        }

        ///<summary> Обработчик нажатия на пункт меню создать контакт</summary>
        private void ContextMenuCreateContact_OnClick(object sender, RoutedEventArgs e)
        {
            var number = _viewModelCallHistory.CurrentModelCallHistoryPeer.ModelPeerObj.IdentityString;

            var window = new WindowStandard(new ViewContactManual(number)) { Height = 500, Width = 700, Owner = WindowMain.CurrentMainWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner };

            window.ShowDialog();
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
