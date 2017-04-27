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
    /// Interaction logic for ViewContactDirectoryDetail.xaml
    /// </summary>
    public partial class ViewContactDirectoryDetail : UserControl, IUserControlClose
    {
        ///<summary> Событие закрытия </summary>
        public event EventHandler Close;

        ///<summary> Текущий отображаемый контакт </summary>
        public readonly ModelContact CurrentModelContact;

        ///<summary> Конструктор </summary>
        public ViewContactDirectoryDetail(ModelContact modelContact)
        {
            InitializeComponent();

            CurrentModelContact = modelContact;

            if (CurrentModelContact.Id != 0)
            {
                var modelContactFromViewContact = ViewContact.CurrentViewContact.ListModelContact.FirstOrDefault(obj => obj.Id == CurrentModelContact.Id);

                if (modelContactFromViewContact != null)
                {
                    GridRequestInviteCard.Visibility = Visibility.Hidden;

                    ScrollViewerMain.Content = new ViewContactDetail(modelContactFromViewContact);
                }
            }
            else
            {
                var viewModelContactDirectoryDetail = new ViewModelContactDirectoryDetail(modelContact);

                viewModelContactDirectoryDetail.SendRequestSuccessful += (sender, args) =>
                {
                    ViewContact.CurrentViewContact.AddContactListBoxContact(CurrentModelContact, false);

                    GridRequestInviteCard.Visibility = Visibility.Hidden;

                    ScrollViewerMain.Content = new ViewContactDetail(CurrentModelContact);
                };

                DataContext = viewModelContactDirectoryDetail;

                CurrentModelContact.PropertyChanged += CurrentModelContactOnPropertyChanged;

                if (modelContact.ListModelUserContact.Count == 0)
                {
                    LabelContact.Visibility = Visibility.Collapsed;
                    RectangleContact.Visibility = Visibility.Collapsed;
                }
                else
                {
                    foreach (var modelUserContact in modelContact.ListModelUserContact)
                    {
                        StackPanelContact.Children.Add(CreateContactControl(modelUserContact));
                    }
                }

                if (modelContact.ListModelUserContactExtra.Count == 0)
                {
                    LabelContactExtra.Visibility = Visibility.Collapsed;
                    RectangleContactExtra.Visibility = Visibility.Collapsed;
                }
                else
                {
                    foreach (var modelUserContact in modelContact.ListModelUserContactExtra)
                    {
                        StackPanelContactExtra.Children.Add(CreateContactExtraControl(modelUserContact));
                    }
                }

                ChengeContactStatus(CurrentModelContact);
            }
        }

        ///<summary> Обработчик изменений CurrentModelContact </summary>
        private void CurrentModelContactOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName.ToLower();

            if (propertyName == "id" || propertyName == "blocked")
            {
                var modelContact = sender as ModelContact;

                ChengeContactStatus(modelContact);
            }
        }

        ///<summary> Метод изменения статуса пользователя </summary>
        private void ChengeContactStatus(ModelContact modelContact)
        {
            if (modelContact == null) return;

            StackPanelBlocked.Visibility = Visibility.Hidden;
            TextBlockSentRequest.Visibility = Visibility.Hidden;
            ButtonSendRequest.Visibility = Visibility.Hidden;
            MenuItemBlock.Visibility = Visibility.Collapsed;
            MenuItemUnblock.Visibility = Visibility.Collapsed;

            if (modelContact.Blocked)
            {
                StackPanelBlocked.Visibility = Visibility.Visible;
                MenuItemUnblock.Visibility = Visibility.Visible;
            }
            else
            {
                MenuItemBlock.Visibility = Visibility.Visible;

                if (modelContact.Id > 0)
                {
                    TextBlockSentRequest.Visibility = Visibility.Visible;
                }
                else
                {
                    ButtonSendRequest.Visibility = Visibility.Visible;
                }
            }
        }

        ///<summary> Создание элемента для котактов </summary>
        private Grid CreateContactControl(ModelUserContact modelUserContact)
        {
            var grid = new Grid();

            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };

            grid.Children.Add(stackPanel);

            stackPanel.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = @"d-sip", Foreground = Brushes.Gray });

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold, Text = modelUserContact.IdentityString });

            var imagePhone = new Image { Height = 19, Margin = new Thickness(0, 8, 0, 8), Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_offline.png") };

            var button = new Button { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style, Content = imagePhone };
            button.Click += (sender, args) => ContactNumberButtonCall(modelUserContact.Identity);

            grid.Children.Add(button);

            return grid;
        }

        ///<summary> Создание элемента для дополнительных котактов </summary>
        private Grid CreateContactExtraControl(ModelUserContact modelUserContact)
        {
            var grid = new Grid();

            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };

            grid.Children.Add(stackPanel);

            var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Gray };

            textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewContactDetail_Phone");

            stackPanel.Children.Add(textBlock);

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold, Text = modelUserContact.IdentityString });

            var imagePhone = new Image { Height = 24, Margin = new Thickness(0, 8, 0, 8), Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_pstn.png") };

            var button = new Button { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style, Content = imagePhone };
            button.Click += (sender, args) => ContactNumberButtonCall(modelUserContact.Identity);

            grid.Children.Add(button);

            return grid;
        }

        ///<summary> Обработчик кнопки возврата к результатам поиска </summary>
        private void ButtonBackToSearchResult_OnClick(object sender, RoutedEventArgs e)
        {
            OnCloseWindow();
        }

        /// <summary> Обработчик нажатия по картинке дополнительных сервисов </summary>
        private void ImageAdditionalService_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;

            if (image?.ContextMenu != null)
            {
                image.ContextMenu.PlacementTarget = image;
                image.ContextMenu.IsOpen = true;
                e.Handled = true;
            }
        }

        ///<summary> Позвонить по доплнительному номеру номеру </summary>
        private void ContactNumberButtonCall(string number)
        {
            //ViewCallActive.OutgoingCall(number);
            WindowCallActive.OutgoingCall(CurrentModelContact, number);
        }

        ///<summary> Инвокатор события закрытия </summary>
        private void OnCloseWindow()
        {
            if (BeforeClose()) Close?.Invoke(this, EventArgs.Empty);
        }

        ///<summary> Действие перед закрытием </summary>
        public bool BeforeClose()
        {
            return true;
        }
    }
}
