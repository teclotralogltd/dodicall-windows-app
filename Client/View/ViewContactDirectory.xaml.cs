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
using DAL.Model;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewContactDirectory.xaml
    /// </summary>
    public partial class ViewContactDirectory : UserControl, INotifyPropertyChanged, IDisposable
    {
        ///<summary> Объект ViewModelContactDirectory </summary>
        private readonly ViewModelContactDirectory _viewModelContactDirectory = new ViewModelContactDirectory();

        /// <summary> Объект отображаемой в директории карточки контакта </summary>
        private ViewContactDetail _viewContactDetail;
             
        ///<summary> Ширина контакта в списке контактов </summary>
        private double _widthContactItem;

        ///<summary> Ширина контакта в списке контактов </summary>
        public double WidthContactItem
        {
            get { return _widthContactItem; }
            set { _widthContactItem = value; OnPropertyChanged("WidthContactItem"); }
        }

        ///<summary> Конструктор </summary>
        public ViewContactDirectory()
        {
            InitializeComponent();

            _viewModelContactDirectory.SendRequestSuccessful += (sender, args) => ViewContact.CurrentViewContact.AddContactListBoxContact(args, false);

            _viewModelContactDirectory.PropertyChanged += ViewModelContactDirectoryOnPropertyChanged;

            _viewModelContactDirectory.LockUI += OnLockUi;

            DataContext = _viewModelContactDirectory;
        }

        ///<summary> Обработчик ожидания </summary>
        private void OnLockUi(object sender, bool result)
        {
            if (result)
            {
                ViewProcessMain.WaitingAnimationStart();
            }
            else
            {
                ViewProcessMain.WaitingAnimationStop();
            }
        }

        ///<summary> Обработчик изменений ViewModelContactDirectory </summary>
        private void ViewModelContactDirectoryOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_viewModelContactDirectory.ListModelContact != null && _viewModelContactDirectory.ListModelContact.Count > 0)
            {
                ImageSearchInDodicall.Visibility = Visibility.Hidden;
                ListBoxContact.Visibility = Visibility.Visible;
            }
            else
            {
                ListBoxContact.Visibility = Visibility.Hidden;
                ImageSearchInDodicall.Visibility = Visibility.Visible;
            }
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

        ///<summary> Обработчик нажатия на надпись поиска </summary>
        private void TextBlockSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockSearch.Visibility = Visibility.Collapsed;

            TextBoxSearch.Focus();
        }

        ///<summary> Обработчик изменения размера ListBoxContact </summary>
        private void ListBoxContact_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                WidthContactItem = e.NewSize.Width - 43;
            }
        }

        ///<summary> Обработчик нажатия на кнопку отправки заявки приглашения </summary>
        private void ButtonSendRequest_OnClick(object sender, RoutedEventArgs e)
        {
            var modelContact = (sender as Button)?.DataContext as ModelContact;

            if (modelContact == null) return;

            _viewModelContactDirectory.CommandSendRequest.Execute(modelContact);

            if (modelContact.ModelContactSubscriptionObj.Ask) ((Button)sender).Visibility = Visibility.Collapsed;
        }

        ///<summary> Обработчик изменения выбора контакта в списке контактов </summary>
        private void ListBoxContact_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            var modelContact = e.AddedItems[0] as ModelContact;

            if (modelContact != null)
            {
                _viewContactDetail = new ViewContactDetail(modelContact); // но придется реализовывать вверху кнопку назад в поиск

                GridContactDirectoryDitail.Children.Add(_viewContactDetail);

                Grid.SetRow(_viewContactDetail, 1);

                GridSearch.Visibility = Visibility.Hidden;
                GridContactDirectoryDitail.Visibility = Visibility.Visible;
            }
        }

        ///<summary> Обработчик кнопки возврата к результатам поиска </summary>
        private void ButtonBackToSearchResult_OnClick(object sender, RoutedEventArgs e)
        {
            GridContactDirectoryDitail.Children.Remove(_viewContactDetail);

            ListBoxContact.SelectedItem = null;

            GridContactDirectoryDitail.Visibility = Visibility.Hidden;
            GridSearch.Visibility = Visibility.Visible;
        }

        ///<summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _viewContactDetail?.Dispose();
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
