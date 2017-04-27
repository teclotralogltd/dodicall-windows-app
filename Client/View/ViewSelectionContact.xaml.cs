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
using DAL.Utility;
using DAL.ViewModel;
using DAL.Localization;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewSelectionContact.xaml
    /// </summary>
    public partial class ViewSelectionContact : UserControl, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Флаг нажатия кнопки ОК </summary>
        private bool _okPress;

        /// <summary> Объект ViewModelSelectionContact </summary>
        private readonly ViewModelSelectionContact _viewModelSelectionContact = new ViewModelSelectionContact();

        /// <summary> Список выбранных контактов </summary>
        public List<ModelContact> ListSeleModelContact => _viewModelSelectionContact.SelectedListModelContact;

        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewSelectionContact_CreateMultiChat";

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Конструктор (без сохраненных контактов и заблокированных) </summary>
        public ViewSelectionContact()
        {
            InitializeComponent();

            DataContext = _viewModelSelectionContact;

            RefreshListModelContact();

            TextBlockCountSelected.Text = _viewModelSelectionContact.SelectedListModelContact.Count.ToString();

            ButtonOk.IsEnabled = _viewModelSelectionContact.SelectedListModelContact.Count > 0;

            _viewModelSelectionContact.EventViewModel += ViewModelSelectionContactOnEventViewModel;
        }

        ///<summary> Обработчик события ViewModel </summary>
        private void ViewModelSelectionContactOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == @"ChangedList")
            {
                RefreshListModelContact();
            }

            if (e.Key == @"ChangedCountSelected")
            {
                TextBlockCountSelected.Text = _viewModelSelectionContact.SelectedListModelContact.Count.ToString();

                ButtonOk.IsEnabled = _viewModelSelectionContact.SelectedListModelContact.Count > 0;
            }
        }

        ///<summary> Обновить список контактов </summary>
        private void RefreshListModelContact()
        {
            StackPanelInviteControl.Children.Clear();

            foreach (var i in _viewModelSelectionContact.CurrentListModelContact)
            {
                StackPanelInviteControl.Children.Add(CreateModelContactControl(i));
            }
        }

        ///<summary> Создание контрола запросов </summary>
        private Grid CreateModelContactControl(ModelContact _modelContact)
        {
            ModelContact modelContact = _modelContact.GetDeepCopy();
            var result = new Grid { Height = 53/*, ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(45) }, new ColumnDefinition { Width = new GridLength(48) }, new ColumnDefinition() } */};
            result.MouseEnter += (sender, args) => result.Background = Brushes.WhiteSmoke;
            result.MouseLeave += (sender, args) => result.Background = Brushes.Transparent;

            var checkBox = new CheckBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
            var selectedContact = _viewModelSelectionContact.SelectedListModelContact.FirstOrDefault(obj => modelContact.Id == obj.Id);
            if(selectedContact != null)
            {
                checkBox.IsChecked = true;
            }
            //if (_viewModelSelectionContact.SelectedListModelContact.Contains(modelContact)) checkBox.IsChecked = true;
            checkBox.Checked += (sender, args) => _viewModelSelectionContact.SelectModelContact(modelContact);
            checkBox.Unchecked += (sender, args) => _viewModelSelectionContact.UnselectModelContact(modelContact);

            var gridAvatar = new Grid { Height = 36, Width = 36, Margin = new Thickness(0, 0, 10, 0) };
            Grid.SetColumn(gridAvatar, 0);

            
            gridAvatar.Children.Add(new Image { Source = modelContact.Avatar });
            gridAvatar.Children.Add(new Image
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 15,
                Width = 15,
                Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/IconTray_small.png")
            });

            var gridUserNameStatus = new Grid { RowDefinitions = { new RowDefinition(), new RowDefinition() } };
            Grid.SetColumn(gridUserNameStatus, 1);

            var textBlockFullName = new TextBlock
            {
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                TextTrimming = TextTrimming.CharacterEllipsis,
                VerticalAlignment = VerticalAlignment.Center,
                Text = modelContact.FullName,
                Margin = new Thickness(0,8,0,0)
            };

            var gridStatus = new Grid { ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition() } };
            Grid.SetRow(gridStatus, 1);

            var stackPanelBaseStatus = new StackPanel { Orientation = Orientation.Horizontal };

            var ellipseStatus = new Ellipse { Margin = new Thickness(3,7,3,5), Height = 6, Width = 6/*, Fill = modelContact.ModelEnumUserBaseStatusObj.Color*/ };
            ellipseStatus.SetBinding(Ellipse.FillProperty, new Binding { Source = _modelContact, Path = new PropertyPath("ModelEnumUserBaseStatusObj.Color") });
            var textBlockStatus = new TextBlock { Margin = new Thickness(5,6,0,5), TextTrimming = TextTrimming.CharacterEllipsis, FontSize = 12 };
            textBlockStatus.SetBinding(TextBlock.TextProperty, new Binding { Source = _modelContact, Path = new PropertyPath("ModelEnumUserBaseStatusObj.Name") });

            stackPanelBaseStatus.Children.Add(ellipseStatus);
            stackPanelBaseStatus.Children.Add(textBlockStatus);

            var textBlockExtStatus = new TextBlock { Margin = new Thickness(0,5,10,5), TextTrimming = TextTrimming.CharacterEllipsis, FontSize = 12 };
            textBlockExtStatus.SetBinding(TextBlock.TextProperty, new Binding { Source = _modelContact, Path = new PropertyPath("UserExtendedStatusForFullStatus") });
            Grid.SetColumn(textBlockExtStatus, 1);

            gridStatus.Children.Add(stackPanelBaseStatus);
            gridStatus.Children.Add(textBlockExtStatus);

            gridUserNameStatus.Children.Add(textBlockFullName);
            gridUserNameStatus.Children.Add(gridStatus);

            var rectangle = new Rectangle { Opacity = 1, Width = 550 };
            Grid.SetColumnSpan(rectangle, 2);
            Panel.SetZIndex(rectangle, -1);

            var gridContent = new Grid { ColumnDefinitions = {  new ColumnDefinition { Width = new GridLength(48) }, new ColumnDefinition() } };
            gridContent.Children.Add(gridAvatar);
            gridContent.Children.Add(gridUserNameStatus);
            gridContent.Children.Add(rectangle);
            checkBox.Content = gridContent;

            result.Children.Add(checkBox);
            /*result.Children.Add(gridAvatar);
            result.Children.Add(gridUserNameStatus);*/

            result.DataContext = modelContact;
            return result;
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

        /// <summary> Действие перед закрытием окна </summary>
        public bool BeforeCloseWindow()
        {
            if (!_okPress) ListSeleModelContact.Clear();

            return true;
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        ///<summary> Обработчик нажатия на кнопку Ok </summary>
        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            _okPress = true;

            OnCloseWindow();
        }

        ///<summary> Открывает модальное окно и возвращает список выбранных пользователей (некрасивое решение, надо бы переделать потом) </summary>
        public static List<ModelContact> ShowSelectionContact()
        {
            var view = new ViewSelectionContact();

            var windowStandard = new WindowStandard(view)
            {
                MinHeight = 450,
                Height = 500,
                MinWidth = 500,
                Width = 550,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = WindowMain.CurrentMainWindow
            };
            windowStandard.ShowDialog();

            return view.ListSeleModelContact;
        }

        /// <summary> Отобразить окно с учасниками чата </summary>
        public static List<ModelContact> ShowInviteAndRevokeChatMembers(List<ModelContact> chatMembers)
        {
            var view = new ViewSelectionContact();
            view._viewModelSelectionContact.SelectListModelContact(chatMembers);
            view.RefreshListModelContact();
            view.ButtonOk.SetResourceReference(Button.ContentProperty, "ViewSelectionContact_Save");
            view.WindowCaptionResourceKey = @"ViewSelectionContact_ChatMembers";
            var windowStandard = new WindowStandard(view)
            {
                MinHeight = 450,
                Height = 500,
                MinWidth = 500,
                Width = 550,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = WindowMain.CurrentMainWindow
            };
            windowStandard.ShowDialog();

            return view.ListSeleModelContact;
        }

        /// <summary> Отобразить окно отправки контакта </summary>
        public static List<ModelContact> ShowSendContacts()
        {
            var view = new ViewSelectionContact();
            view.RefreshListModelContact();
            view.ButtonOk.SetResourceReference(Button.ContentProperty, "ViewSelectionContact_Send");
            view.WindowCaptionResourceKey = @"ViewSelectionContact_SendContact";
            var windowStandard = new WindowStandard(view)
            {
                MinHeight = 450,
                Height = 500,
                MinWidth = 500,
                Width = 550,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = WindowMain.CurrentMainWindow
            };
            windowStandard.ShowDialog();

            return view.ListSeleModelContact;
        }

        /// <summary> Обработчик удаления контакта из списка выбранных </summary>
        private void RevokeCheckedChatMember_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var modelContact = (ModelContact)((Image)sender).DataContext;
            _viewModelSelectionContact.UnselectModelContact(modelContact);
            RefreshListModelContact();
        }
    }
}
