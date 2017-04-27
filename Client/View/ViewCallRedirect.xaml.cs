using DAL.Abstract;
using DAL.Model;
using DAL.Utility;
using DAL.ViewModel;
using dodicall.Window;
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
    /// Interaction logic for ViewCallRedirect.xaml
    /// </summary>
    public partial class ViewCallRedirect : UserControl, IUserControlCloseWindow, IWindowCaption, INotifyPropertyChanged
    {
        /// <summary> Объект ViewModelSelectionContact </summary>
        private readonly ViewModelCallRedirect _viewModelCallRedirect;

        /// <summary> Текущий список контактов </summary>
        private ObservableCollection<ExtendedModelContact> _currentListModelContact;

        /// <summary> Текущий список контактов </summary>
        public ObservableCollection<ExtendedModelContact> CurrentListModelContact
        {
            get { return _currentListModelContact; }
            set
            {
                if (_currentListModelContact == value) return;
                _currentListModelContact = value;
                OnPropertyChanged("CurrentListModelContact");
            }
        }

        /// <summary> Развернутый на данный момент контакт</summary>
        private ModelContact _currentExpandContact;
        
        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewCallRedirect_RedirectCall";

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow; 

        /// <summary> Конструктор </summary>
        public ViewCallRedirect(ModelCall currentModelCall)
        {
            InitializeComponent();

            _viewModelCallRedirect = new ViewModelCallRedirect(currentModelCall);

            DataContext = _viewModelCallRedirect;
             
            _viewModelCallRedirect.CommandSelectDialpadList = new Command(obj => ShowComingSoon());
            _viewModelCallRedirect.CommandSelectHistoryList = new Command(obj => ShowComingSoon());

            _viewModelCallRedirect.PropertyChanged += _viewModelCallRedirect_PropertyChanged;

            _currentListModelContact = new ObservableCollection<ExtendedModelContact>(_viewModelCallRedirect.CurrentListModelContact.Select(a => new ExtendedModelContact() { ModelContactObj = a }));

            OnPropertyChanged("CurrentListModelContact");

            _viewModelCallRedirect.CloseView += ViewCallRedirectOnCloseView; 
        }

        /// <summary> Событие клика по "шапке" в элементе list-a для сворачивания и разворачивания номеров </summary>
        private void ListBoxElementHeaderClick(object sender, RoutedEventArgs e)
        {
            var item = sender as Button; 
            if (item != null)
            {
                var currentContact = item.DataContext as ExtendedModelContact;
                if (currentContact != null)
                {
                    if (!currentContact.IsExpand)
                    {
                        var expandedContacts = CurrentListModelContact.Where(a => a.IsExpand);
                        foreach (var contact in expandedContacts)
                        {
                            contact.IsExpand = false;
                        }
                        currentContact.IsExpand = true;
                        _currentExpandContact = currentContact.ModelContactObj;
                        ChengeContactStatus(_currentExpandContact);
                    }
                    else
                    {
                        currentContact.IsExpand = false;
                        _currentExpandContact = null;
                    }
                }
            }
        }

        /// <summary> Событие PropertyChanged в ViewModel </summary>
        private void _viewModelCallRedirect_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName.ToLower();
            if (propertyName == "currentlistmodelcontact")
            {
                CurrentListModelContact = new ObservableCollection<ExtendedModelContact>(_viewModelCallRedirect.CurrentListModelContact.Select(a => new ExtendedModelContact() { ModelContactObj = a }));  
            }
        }

        /// <summary> Показать окно comingsoon </summary>
        private void ShowComingSoon()
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        /// <summary> Обработчик события CloseView </summary>
        private void ViewCallRedirectOnCloseView(object sender, EventArgs eventArgs)
        {
            OnCloseWindow();

            _viewModelCallRedirect.Dispose();
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

        ///<summary> Обработчик нажатия по фильтру списка контактов </summary>
        private void StackPanelFilterContact_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenuFilterContact.IsOpen = true;
        }

        ///<summary> Обработчик нажатия по пункту меню Все контакты </summary>
        private void MenuItemAllContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда 
            _viewModelCallRedirect.CommandApplyFilterAllContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню Dodicall контакты </summary>
        private void MenuItemDodicallContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда 
            _viewModelCallRedirect.CommandApplyFilterDodicallContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню Сохраненные контакты </summary>
        private void MenuItemSavedContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда 
            _viewModelCallRedirect.CommandApplyFilterSavedContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню заблокированные контакты </summary>
        private void MenuItemBlockedContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда 
            _viewModelCallRedirect.CommandApplyFilterBlockedContact.Execute(null);
        }

        ///<summary> Обработчик нажатия по пункту меню белые контакты </summary>
        private void MenuItemWhiteContact_OnClick(object sender, RoutedEventArgs e)
        {
            // в XAML не работает прибинденная команда 
            _viewModelCallRedirect.CommandApplyFilterWhiteContact.Execute(null);
        }

        /// <summary> Обработчик изменения разворачивания/сворачивания информации о контакте </summary>
        private void GridAdditionalData_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var stackPanelContact = (sender as StackPanel);
                var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
                var modelContact = (stackPanelContact.DataContext as ExtendedModelContact)?.ModelContactObj;

                if (modelContact == null) return;

                if (modelContact.IsDodicall && modelContact.ListModelUserContact != null && modelContact.ListModelUserContact.Count > 0)
                {
                    foreach (var modelUserContact in modelContact.ListModelUserContact)
                    {
                        stackPanelContact.Children.Add(CreateContactControl(modelUserContact, modelContact));
                    }

                    if (modelContact.ListModelUserContactExtra.Count != 0)
                    {
                        var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Silver };

                        textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewCallRedirect_AdditionalContacts");
                        textBlock.Margin = new Thickness(20, 10, 10, 10);

                        stackPanelContact.Children.Add(textBlock);

                        foreach (var modelUserContact in modelContact.ListModelUserContactExtra)
                        {
                            stackPanelContact.Children.Add(CreateContactExtraControl(modelUserContact, modelContact));
                        }
                    }
                }

                if (!modelContact.IsDodicall && modelContact.ListModelUserContactExtra != null && modelContact.ListModelUserContactExtra.Count != 0)
                {
                    var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Silver };

                    textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewCallRedirect_AdditionalContacts");
                    textBlock.Margin = new Thickness(20, 10, 10, 10);

                    stackPanelContact.Children.Add(textBlock);

                    foreach (var modelUserContact in modelContact.ListModelUserContactExtra)
                    {
                        stackPanelContact.Children.Add(CreateContactExtraControl(modelUserContact, modelContact));
                    }
                }

                ChengeContactStatus(modelContact);

                modelContact.PropertyChanged += CurrentModelContactOnPropertyChanged;
            }
            else
            {
                var stackPanelContact = (sender as StackPanel);
                stackPanelContact.Children.Clear();
                //В этом списке хранятся иконки трубочек развернутого на данный момент контакта.
                _listImageSip.Clear();
            }
        }

        /// <summary> Обработчик изменения статуса ModelContact </summary>
        private void CurrentModelContactOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName.ToLower();

            var modelContact = (ModelContact)sender;
              
            if (propertyName == "modelenumuserbasestatusobj" || propertyName == "id" || propertyName == "blocked")
            {
                Action action = () => ChengeContactStatus(modelContact);

                GridMain.Dispatcher.Invoke(action);
            }

            if (propertyName == "modelcontactsubscriptionobj")
            {
                Action action = () => ChengeContactStatus(modelContact);

                GridMain.Dispatcher.Invoke(action);
            }
        }

        /// <summary> Изменение статуса контакта </summary>
        private void ChengeContactStatus(ModelContact modelContact)
        {
            if (modelContact == null) return;

            if (_currentExpandContact == null) return;

            if (_currentExpandContact.Id != modelContact.Id) return;

            switch (modelContact.ModelEnumUserBaseStatusObj?.Code)
            {
                case 1:
                    foreach (var image in _listImageSip)
                    {
                        image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/transfer_online.png");
                    }

                    break;
                case 3:
                    foreach (var image in _listImageSip)
                    {
                        image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/transfer_dnd.png");
                    }

                    break;
                default:
                    foreach (var image in _listImageSip)
                    {
                        image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/transfer_default_offline.png");
                    }

                    break;
            }

            if (!modelContact.IsAccessStatus)
            { 
                foreach (var image in _listImageSip)
                {
                    image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/transfer_default_offline.png");
                }
            }
        }

        /// <summary> Создание элемента для моих котактов </summary>
        private Grid CreateContactControl(ModelUserContact modelUserContact, ModelContact currentModelContact)
        {
            var grid = new Grid();

            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };

            grid.Children.Add(stackPanel);

            var imagePhone = new Image { Height = 19, Margin = new Thickness(0, 8, 0, 8) };

            var button = new Button { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style, Content = imagePhone };
            button.Click += (sender, args) => ContactNumberTransferCall(modelUserContact.Identity);

            _listImageSip.Add(imagePhone);

            //      stackPanel.Children.Add(new Image { VerticalAlignment = VerticalAlignment.Center, Height = 19, Margin = new Thickness(5), Source = favouriteBitmapImage });

            stackPanel.Margin = new Thickness(20, 10, 10, 10);

            stackPanel.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = @"d-sip", Foreground = Brushes.Silver });

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(40, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, Text = modelUserContact.IdentityString });

            grid.Children.Add(button);

            return grid;
        }

        /// <summary> Создание элемента для дополнительных котактов </summary>
        private Grid CreateContactExtraControl(ModelUserContact modelUserContact, ModelContact currentModelContact)
        {
            var grid = new Grid();

            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };

            grid.Children.Add(stackPanel);

            var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Silver };

            textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewCallRedirect_Phone");

            stackPanel.Children.Add(textBlock);

            stackPanel.Margin = new Thickness(20, 10, 10, 10);

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(20, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, Text = modelUserContact.IdentityString });

            var imagePhoneTransfer = new Image { Height = 19, Margin = new Thickness(0, 8, 0, 8), Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/transfer_default_offline.png") };

            var button = new Button { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style, Content = imagePhoneTransfer };
            button.Click += (sender, args) => ContactExtraNumberTransferCall(modelUserContact.Identity);

            grid.Children.Add(button);

            return grid;
        }

        /// <summary> Список картинок sip номеров </summary>
        private List<Image> _listImageSip = new List<Image>();

        ///<summary> Перевести звонок по номеру </summary>
        private void ContactNumberTransferCall(string number)
        {
            if (_viewModelCallRedirect.CurrentModelCall != null)
            {
                var result = _viewModelCallRedirect.TransferCall(number);
                if (result)
                    OnCloseWindow();
            }
        }

        ///<summary> Перевести звонок по доплнительному номеру </summary>
        private void ContactExtraNumberTransferCall(string number)
        {
            if (_viewModelCallRedirect.CurrentModelCall != null)
            {
                var result = _viewModelCallRedirect.TransferCall(number);
                if (result)
                    OnCloseWindow();
            }
        } 

        /// <summary> Обработчик нажатия кнопки "Отмена" </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OnCloseWindow();
        }

        /// <summary> Действие перед закрытием окна </summary>
        public bool BeforeCloseWindow()
        {
            return true;
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);

            _viewModelCallRedirect.Dispose();
        }

        /// <summary> Вспомогательный класс для реализации механизма разворачивания/сворачивания эелементов в ListBox-e </summary>
        public class ExtendedModelContact : AbstractModel<ExtendedModelContact>
        {
            /// <summary> Объект ModelContact</summary>
            public ModelContact ModelContactObj { get; set; }

            /// <summary> Флаг "развернутости" контакта </summary>
            private bool _isExpand;

            /// <summary> Флаг "развернутости" контакта </summary>
            public bool IsExpand
            {
                get { return _isExpand; }
                set
                {
                    if (_isExpand == value) return;
                    _isExpand = value;
                    OnPropertyChanged("IsExpand");
                }
            }

            /// <summary> Реализация GetDeepCopy для AbstractModel</summary>
            public override ExtendedModelContact GetDeepCopy()
            {
                return null;
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
