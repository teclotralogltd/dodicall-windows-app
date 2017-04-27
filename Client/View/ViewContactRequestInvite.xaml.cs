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
using DAL.Utility;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewContactRequestInvite.xaml
    /// </summary>
    public partial class ViewContactRequestInvite : UserControl, INotifyPropertyChanged, IDisposable
    {
        ///<summary> Объект ViewModelContactDirectory </summary>
        private readonly ViewModelContactRequestInvite _viewModelContactRequestInvite = new ViewModelContactRequestInvite();

        /// <summary> Объект отображаемой в запросах и приглашениях карточки контакта </summary>
        private ViewContactDetail _viewContactDetail;

        ///<summary> Конструктор </summary>
        public ViewContactRequestInvite()
        {
            InitializeComponent();

            _viewModelContactRequestInvite.ApplyInviteSuccessful += (sender, args) => ViewContact.CurrentViewContact.AddContactListBoxContact(args, false);

            _viewModelContactRequestInvite.PropertyChanged += ViewModelContactRequestInviteOnPropertyChanged;

            if (_viewModelContactRequestInvite.ListModelContactInvite.Count == 0) StackPanelInvite.Visibility = Visibility.Collapsed;

            if (_viewModelContactRequestInvite.ListModelContactRequest.Count == 0) StackPanelRequest.Visibility = Visibility.Collapsed;

            DataContext = _viewModelContactRequestInvite;

            ChangeCountInvateUnread();

            foreach (var i in _viewModelContactRequestInvite.ListModelContactInvite)
            {
                StackPanelInviteControl.Children.Add(CreateInvateControl(i));
            }

            foreach (var i in _viewModelContactRequestInvite.ListModelContactRequest)
            {
                StackPanelRequestControl.Children.Add(CreateRequestControl(i));
            }
        }

        ///<summary> Обработчик изменения ViewModelContactRequestInvite </summary>
        private void ViewModelContactRequestInviteOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CountInvateUnread", StringComparison.InvariantCultureIgnoreCase))
            {
                ChangeCountInvateUnread();

                StackPanelInviteControl.Children.Clear();

                foreach (var i in _viewModelContactRequestInvite.ListModelContactInvite)
                {
                    StackPanelInviteControl.Children.Add(CreateInvateControl(i));
                }

                StackPanelRequestControl.Children.Clear();

                foreach (var i in _viewModelContactRequestInvite.ListModelContactRequest)
                {
                    StackPanelRequestControl.Children.Add(CreateRequestControl(i));
                }
            }
        }

        ///<summary> Обработчик изменения CountInvateUnread </summary>
        public void ChangeCountInvateUnread()
        {
            GridCountInvateUnread.Visibility = _viewModelContactRequestInvite.CountInvateUnread > 0 ? Visibility.Visible : Visibility.Hidden;

            RectangleGridCountInvateUnread.Width = _viewModelContactRequestInvite.CountInvateUnread > 9 ? 25 : 16;
        }

        ///<summary> Создание контрола приглашений </summary>
        private Grid CreateInvateControl(ModelContact modelContact)
        {
            var result = new Grid { Height = 53, ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(58) }, new ColumnDefinition(), new ColumnDefinition { Width = new GridLength(56) } } };
            result.MouseEnter += (sender, args) => result.Background = Brushes.WhiteSmoke;
            result.MouseLeave += (sender, args) => result.Background = Brushes.Transparent;

            var grid = new Grid { Height = 36, Width = 36, Margin = new Thickness(8, 0, 10, 0) };
            Grid.SetColumn(grid, 0);

            grid.Children.Add(new Image { Source = modelContact.Avatar });
            grid.Children.Add(new Image
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 15,
                Width = 15,
                Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/IconTray_small.png")
            });

            var textBlock = new TextBlock
            {
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                TextTrimming = TextTrimming.CharacterEllipsis,
                VerticalAlignment = VerticalAlignment.Center,
                Text = modelContact.FullName,
                Foreground = modelContact.ModelContactSubscriptionObj.ModelEnumSubscriptionStatusObj.Code == 0 ? Brushes.Red : Brushes.Black
            };
            Grid.SetColumn(textBlock, 1);

            var button = new Button
            {
                Margin = new Thickness(5, 5, 8, 5),
                Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style,
                Content = new Image { Height = 24, Width = 23, Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/send_request.png") }
            };
            Grid.SetColumn(button, 2);
            button.Click += ButtonApplyInvite_OnClick;

            result.Children.Add(grid);
            result.Children.Add(textBlock);
            result.Children.Add(button);

            result.MouseLeftButtonDown += (sender, args) => { textBlock.Foreground = Brushes.Black; OpenModelContactDetail(modelContact); };
            result.DataContext = modelContact;

            return result;
        }

        ///<summary> Создание контрола запросов </summary>
        private Grid CreateRequestControl(ModelContact modelContact)
        {
            var result = new Grid { Height = 53, ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(58) }, new ColumnDefinition(), new ColumnDefinition { Width = new GridLength(56) } } };
            result.MouseEnter += (sender, args) => result.Background = Brushes.WhiteSmoke;
            result.MouseLeave += (sender, args) => result.Background = Brushes.Transparent;

            var grid = new Grid { Height = 36, Width = 36, Margin = new Thickness(8, 0, 10, 0) };
            Grid.SetColumn(grid, 0);

            grid.Children.Add(new Image { Source = modelContact.Avatar });
            grid.Children.Add(new Image
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 15,
                Width = 15,
                Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/IconTray_small.png")
            });

            var textBlock = new TextBlock
            {
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                TextTrimming = TextTrimming.CharacterEllipsis,
                VerticalAlignment = VerticalAlignment.Center,
                Text = modelContact.FullName
            };
            Grid.SetColumn(textBlock, 1);

            var image = new Image {Margin = new Thickness(5, 5, 8, 5), Height = 17, Width = 16, Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/sent_request.png")};
            Grid.SetColumn(image, 2);

            result.Children.Add(grid);
            result.Children.Add(textBlock);
            result.Children.Add(image);

            result.MouseLeftButtonDown += (sender, args) => OpenModelContactDetail(modelContact);
            result.DataContext = modelContact;

            return result;
        }

        ///<summary> Обработчик нажатия на приглашение </summary>
        private void OpenModelContactDetail(ModelContact modelContact)
        {
            if (modelContact != null)
            {
                _viewContactDetail = new ViewContactDetail(modelContact);

                GridContactRequestInviteDetail.Children.Add(_viewContactDetail);

                Grid.SetRow(_viewContactDetail, 1);

                GridContactRequestInvite.Visibility = Visibility.Hidden;
                GridContactRequestInviteDetail.Visibility = Visibility.Visible;
            }
        }

        private void ApplyInvite(ModelContact modelContact)
        {
            if (modelContact == null || modelContact.Id > 0) return;

            _viewModelContactRequestInvite.CommandApplyInvite.Execute(modelContact);

            //if (modelContact.Id > 0) ((Button)sender).Visibility = Visibility.Collapsed;
        }

        ///<summary> Обработчик нажатия на кнопку принятия заявки </summary>
        private void ButtonApplyInvite_OnClick(object sender, RoutedEventArgs e)
        {
            var modelContact = (sender as Button)?.DataContext as ModelContact;

            ApplyInvite(modelContact);
        }

        ///<summary> Обработчик кнопки возврата </summary>
        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            GridContactRequestInviteDetail.Children.Remove(_viewContactDetail);

            //ListBoxContactInvite.SelectedItem = null;
            //ListBoxContactRequest.SelectedItem = null;

            GridContactRequestInviteDetail.Visibility = Visibility.Hidden;
            GridContactRequestInvite.Visibility = Visibility.Visible;

            if (_viewContactDetail.Deny || _viewContactDetail.CurrentModelContact.Id > 0) _viewModelContactRequestInvite.ListModelContactInvite.Remove(_viewContactDetail.CurrentModelContact);
            
            ViewContact.CurrentViewContact.RecalculateUnreadInvite();
            _viewModelContactRequestInvite.RefreshRequestInvite();
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
