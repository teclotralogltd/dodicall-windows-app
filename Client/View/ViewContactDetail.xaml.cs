using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using dodicall.Window;
using DAL.Model;
using DAL.Utility;
using DAL.ViewModel;
using System.Diagnostics;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewContactDetail.xaml
    /// </summary>
    public partial class ViewContactDetail : UserControl, IDisposable
    {
        /// <summary> Текущий отображаемый контакт </summary>
        public readonly ModelContact CurrentModelContact;

        /// <summary> Список картинок sip номеров </summary>
        private List<Image> _listImageSip = new List<Image>();

        /// <summary> Объект ViewModelContactDetail</summary>
        private ViewModelContactDetail _viewModelContactDetail;
         
        ///<summary> Флаг отказа от заявки (кастыль, но нет времени переделывать на проброс Event) </summary>
        public bool Deny;

        /// <summary> Конструктор </summary>
        public ViewContactDetail(ModelContact modelContact)
        {
            InitializeComponent();

            CurrentModelContact = modelContact;
             
            _viewModelContactDetail = new ViewModelContactDetail(modelContact);

            if (modelContact.Id != 0)
            {
                if (modelContact.ModelContactSubscriptionObj?.ModelEnumSubscriptionStateObj.Code == 0) _viewModelContactDetail.InviteRead();
            }
            else
            {
                //Если пользователь отправил нам приглашение у него id = 0 и ModelEnumSubscriptionStateObj.Code = 2 (To)
                if (modelContact.ModelContactSubscriptionObj?.ModelEnumSubscriptionStateObj.Code == 2) _viewModelContactDetail.InviteRead();
            }
              
            _viewModelContactDetail.BlockedChanged += OnBlockedChanged;

            _viewModelContactDetail.WhiteChanged += _viewModelContactDetail_WhiteChanged;

            _viewModelContactDetail.DeleteModelContactSuccessful += OnDeleteModelContactSuccessful;

            _viewModelContactDetail.SendRequestSuccessful += OnSendRequestSuccessful;

            _viewModelContactDetail.ApplyDenyInviteSuccessful += OnApplyDenyInviteSuccessful;

            DataContext = _viewModelContactDetail;

            if (modelContact.IsDodicall && modelContact.ListModelUserContact != null && modelContact.ListModelUserContact.Count > 0)
            {
                foreach (var modelUserContact in modelContact.ListModelUserContact)
                {
                    StackPanelContact.Children.Add(CreateContactControl(modelUserContact));
                } 
            }

            if (CurrentModelContact.Id != 0)
            {
                var modelContactFromViewContact = ViewContact.CurrentViewContact.ListModelContact.FirstOrDefault(obj => obj.Id == CurrentModelContact.Id);

                if (modelContactFromViewContact != null)
                {
                    _viewModelContactDetail.ShowContactDetailCard = true; 
                }
            }
            else
            {
                if (_viewModelContactDetail.IsInvite)
                    _viewModelContactDetail.ShowContactInviteCard = true;
                else
                    _viewModelContactDetail.ShowContactRequestCard = true;
            } 

            if (modelContact.ListModelUserContactExtra.Count != 0)
            { 
                foreach (var modelUserContact in modelContact.ListModelUserContactExtra)
                {
                    StackPanelExtraContact.Children.Add(CreateContactExtraControl(modelUserContact));
                } 
            } 

            ChengeContactStatus(CurrentModelContact);  

            CurrentModelContact.PropertyChanged += CurrentModelContactOnPropertyChanged;
        }

        /// <summary> Обработчик изменения добавления/удаления из белых контактов </summary>
        private void _viewModelContactDetail_WhiteChanged(object sender, EventArgs e)
        {
            MenuItemAddToWhite.Visibility = CurrentModelContact.White ? Visibility.Collapsed : Visibility.Visible;

            MenuItemDeleteFromWhite.Visibility = CurrentModelContact.White ? Visibility.Visible : Visibility.Collapsed;

            ViewContact.CurrentViewContact.ApplyFilterListModelContact();
        }  

        /// <summary> Создание элемента для моих котактов </summary>
        private Grid CreateContactControl(ModelUserContact modelUserContact)
        {
            var grid = new Grid();

            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };

            grid.Children.Add(stackPanel);

            var favouriteBitmapImage = UtilityPicture.GetBitmapImageFromStringUri(modelUserContact.Favourite ? "/Resources/fav.png" : "/Resources/not_fav.png");

            var imagePhone = new Image { Height = 19, Margin = new Thickness(0,8,0,8)};

            var button = new Button { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style, Content = imagePhone };
            button.Click += (sender, args) => ContactNumberButtonCall(modelUserContact.Identity);

            _listImageSip.Add(imagePhone);

            stackPanel.Children.Add(new Image { VerticalAlignment = VerticalAlignment.Center, Height = 19, Margin = new Thickness(5), Source = favouriteBitmapImage });

            stackPanel.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = @"d-sip", Foreground = Brushes.Gray });

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold, Text = modelUserContact.IdentityString });

            grid.Children.Add(button);

            return grid;
        }

        /// <summary> Создание элемента для дополнительных котактов </summary>
        private Grid CreateContactExtraControl(ModelUserContact modelUserContact)
        {
            var grid = new Grid();

            var stackPanel = new StackPanel { Height = 36, Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };

            grid.Children.Add(stackPanel);

            var textBlock = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.Gray };

            textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewContactDetail_Phone");

            stackPanel.Children.Add(textBlock);

            stackPanel.Children.Add(new TextBlock { Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold, Text = modelUserContact.IdentityString });

            var imagePhonePsnt = new Image { Height = 24, Margin = new Thickness(0, 8, 0, 8), Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_pstn.png") };

            var button = new Button { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style, Content = imagePhonePsnt };
            button.Click += (sender, args) => ContactExtraNumberButtonCall(modelUserContact.Identity);

            grid.Children.Add(button);

            return grid;
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
                GridUserStatus.Visibility = modelContact.IsAccessStatus ? Visibility.Visible : Visibility.Collapsed;
                 
                Action action = () => ChengeContactStatus(modelContact);

                GridMain.Dispatcher.Invoke(action);
            }

            if (propertyName == "id" || propertyName == "blocked")
            {  
                ChengeContactStatus(modelContact);
            }
        } 

        /// <summary> Изменение статуса контакта </summary>
        private void ChengeContactStatus(ModelContact modelContact)
        {
            if (modelContact == null) return;  

            switch (modelContact.ModelEnumUserBaseStatusObj?.Code)
            {
                case 1: 
                    foreach (var image in _listImageSip)
                    {
                        image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_online.png");
                    }

                    break;
                case 3:
                    foreach (var image in _listImageSip)
                    {
                        image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_dnd.png");
                    }

                    break;
                default:
                    foreach (var image in _listImageSip)
                    {
                        image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_offline.png");
                    }

                    break;
            }

            if (!modelContact.IsAccessStatus)
            {
                foreach (var image in _listImageSip)
                {
                    image.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_offline.png");
                }
            }
            else
            {
                TextBlockSentRequest.Visibility = Visibility.Hidden;
            }
        }  

        /// <summary> Обработчик нажатия по картинке дополнительных сервисов </summary>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;

            if (image?.ContextMenu != null)
            {
                image.ContextMenu.PlacementTarget = image;
                image.ContextMenu.IsOpen = true;
                e.Handled = true;
            }
        }

        /// <summary> Показать окно Coming Soon </summary>
        private void ShowComingSoon(object sender, RoutedEventArgs e)
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        ///<summary> Изменение размера GridUserStatus </summary>
        private void GridUserStatus_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBlockUserExtendedStatus.Width = ColumnDefinitionStatus.ActualWidth - 10; // не биндил в XAML потому что "-10"
        }

        ///<summary> Изменение размера StackPanelBaseStatus </summary>
        private void StackPanelBaseStatus_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBlockUserExtendedStatus.Width = ColumnDefinitionStatus.ActualWidth - 10; // не биндил в XAML потому что "-10"
        }

        ///<summary> Изменение размера GridMain </summary>
        private void GridMain_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBlockFullName.Width = ColumnDefinitionFullName.ActualWidth - 10; // не биндил в XAML потому что "-10"
        }

        ///<summary> Позвонить по номеру </summary>
        private void ContactNumberButtonCall(string number)
        { 
            WindowCallActive.OutgoingCall(CurrentModelContact, number);
        }

        ///<summary> Позвонить по доплнительному номеру номеру </summary>
        private void ContactExtraNumberButtonCall(string number)
        { 
            WindowCallActive.OutgoingCall(CurrentModelContact, number);
        }

        ///<summary> Обработчик нажатия на кнопку позвонить (из трех сервисов под аватаром) </summary>
        private void ButtonPhone_OnClick(object sender, RoutedEventArgs e)
        {
            WindowCallActive.OutgoingCall(CurrentModelContact);
        }

        ///<summary> Обработчик нажатия на кнопку чат (из трех сервисов под аватаром) </summary>
        private void ButtonChat_OnClick(object sender, RoutedEventArgs e)
        {
            WindowMain.ShowInRightWorkspace(new ViewChatMessageDetail(CurrentModelContact));
        }

        /// <summary> Обработчик изменения блокировки контакта </summary>
        public void OnBlockedChanged(object sender, EventArgs e)
        {
            ViewContact.CurrentViewContact.ApplyFilterListModelContact();
        }

        /// <summary> Обработчик удаления контакта </summary>
        private void OnDeleteModelContactSuccessful(object sender, ModelContact contact)
        {
            ViewContact.CurrentViewContact.DeleteContactListBoxContact(contact);
        }

        /// <summary> Обработчик отправки запроса на добавления в друзья </summary>
        private void OnSendRequestSuccessful(object sender, EventArgs e)
        {
            ViewContact.CurrentViewContact.AddContactListBoxContact(CurrentModelContact, false);

            //Кастыль, что бы отобразить "Ваш запрос отправлен" сразу, а не по приходу callback-a
            TextBlockSentRequest.Visibility = Visibility.Visible;

            _viewModelContactDetail.ShowContactDetailCard = true;
        }

        /// <summary> Обработчик приема/отказа приглашения в друзья </summary>
        private void OnApplyDenyInviteSuccessful(object sender, bool e)
        {
            if (e)
            {
                ViewContact.CurrentViewContact.AddContactListBoxContact(CurrentModelContact, false);
                _viewModelContactDetail.ShowContactDetailCard = true;

                //Кастыль, что бы скрыть "Ваш запрос отправлен" сразу, а не по приходу callback-a
                TextBlockSentRequest.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlockDeny.Visibility = Visibility.Visible; 
                Deny = true;
            }
        } 

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _viewModelContactDetail.BlockedChanged -= OnBlockedChanged;

            _viewModelContactDetail.WhiteChanged -= _viewModelContactDetail_WhiteChanged; 

            _viewModelContactDetail.DeleteModelContactSuccessful -= OnDeleteModelContactSuccessful;

            _viewModelContactDetail.SendRequestSuccessful -= OnSendRequestSuccessful;

            _viewModelContactDetail.ApplyDenyInviteSuccessful -= OnApplyDenyInviteSuccessful;  
        }
    }
} 