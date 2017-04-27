using System;
using DAL.Model;
using DAL.Utility;
using System.Linq;
using DAL.ViewModel;
using DAL.ModelEnum;
using System.Windows;
using dodicall.Window;
using dodicall.Converter;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewChatMessageDetail.xaml
    /// </summary>
    public partial class ViewChatMessageDetail : UserControl, IDisposable
    {
        ///<summary> Объект ViewModelChatDetail </summary>
        private ViewModelChatDetail _viewModelChatDetail;

        ///<summary> Текущий чат </summary>
        public ModelChat CurrentModelChat => _viewModelChatDetail.CurrentModelChat;

        ///<summary> Список котролов тесктов сообщений (нужен для ресайза) </summary>
        private readonly List<UnitedControlMessage> _listGridMessage = new List<UnitedControlMessage>();

        private List<BubbleMessage> _listBubbleMessage = new List<BubbleMessage>();

        ///<summary> Список котролов тесктов сообщений (нужен для ресайза) </summary>
        private readonly List<TextBlock> _listTextBlockSysMessage = new List<TextBlock>();

        // <summary> Редактируемое сообщение </summary>
        public UnitedControlMessage ControlEditMessage;

        ///<summary> Флаг активности приложения </summary>
        private bool _flagActivatedWindowMain = true;

        ///<summary> Флаг присутствия в чате пососы "Непрочитанные сообщения" </summary>
        private bool _flagDisplayedSeparatorUnreadMessage;

        /// <summary> Контексное меню для текста сообщения </summary>
        private ContextMenu _contextMenuMessage = new ContextMenu();

        /// <summary> Пункт удаление (для пересылки контактов) </summary>
        private MenuItem _menuItemDelete;

        /// <summary> Список пунктов меню для скрытия с течением времени </summary>
        private List<MenuItem> _listMenuItemForCollapsedOfTime = new List<MenuItem>();

        /// <summary> Список пунктов меню для скрытия если чат не активен </summary>
        private List<MenuItem> _listMenuItemForCollapsedAnActive = new List<MenuItem>();

        /// <summary> Список TextBlock-ов которые могут быть отредактированы </summary>
        private List<TextBlock> _listTextBlockCanEditMessage = new List<TextBlock>();

        /// <summary> Последнее сообщение в чате </summary>
        private ModelChatMessage _lastMessage;

        /// <summary> Последнний бабл в чате </summary>
        private BubbleMessage _lastBubble;

        /// <summary> Последнний мой бабл в чате </summary>
        private BubbleMessage _lastMyBubble;

        /// <summary> Флаг массового выбора </summary>
        private bool _isCheckBoxView = false;

        /// <summary> Есть ли сообщения для ответа </summary>
        private bool _haveMessageToReply = false;

        /// <summary> TextBlock-и цитат для ресайза </summary>
        public List<TextBlock>[] ArrayListTextBlockQuotedMessage;

        /// <summary> Флаг возможности отправить сообщение </summary>
        private bool _isPossibleToSend = false;

        /// <summary> Сообщение со вложением это простой ответ или цитирование </summary>
        public bool justAnswerCurrentMessage = true;

        /// <summary> Стиль кнопок </summary>
        private static Style buttonStyle = Application.Current.TryFindResource(@"ImageStyleButton") as Style;

        /// <summary> Флаг загрузки сообщений в дданный момент </summary>
        private bool _isPagingMessageNow = false;

        /// <summary> Высота скрола при предыдущем пейджинге </summary>
        private double _oldScrollHeight;

        /// <summary> День отправки последнего сообщения </summary>
        private DateTime _dateLastMessage;

        /// <summary> День отправки первого сообщения </summary>
        private DateTime _dateFirstMessage;

        /// <summary> Список TextBlock-ов с днями </summary>
        private List<TextBlock> _listDataMessage;

        /// <summary> Список Grid-ов с днями </summary>
        private List<Grid> _listGridDataMessage;

        /// <summary> Индекс TextBlock-а который сейчас отображается в GridTopDateMessage </summary>
        private int _indexTopDataMessage;

        /// <summary> Последнний бабл со статусом отправленно </summary>
        private BubbleMessage _lastBubbleStatusSend;

        /// <summary> Последнний бабл со статусом доставленно до сервера </summary>
        private BubbleMessage _lastBubbleStatusDeliveredToServer;

        /// <summary> Флаг нахождения скрола в низу </summary>
        public bool IsScrollAtBottom => (ScrollViewerListMessage.ExtentHeight - ScrollViewerListMessage.VerticalOffset - ScrollViewerListMessage.ViewportHeight) < .02;

        ///<summary> Конструктор </summary>
        public ViewChatMessageDetail(ModelChat modelChat)
        {
            InitializeComponent();

            _viewModelChatDetail = new ViewModelChatDetail(modelChat);

            InitializationConstructors();
        }

        ///<summary> Конструктор </summary>
        public ViewChatMessageDetail(ModelContact modelContact)
        {
            InitializeComponent();

            _viewModelChatDetail = new ViewModelChatDetail(modelContact);

            InitializationConstructors();
        }

        ///<summary> Конструктор </summary>
        public ViewChatMessageDetail(List<ModelContact> listModelContact)
        {
            InitializeComponent();

            _viewModelChatDetail = new ViewModelChatDetail(listModelContact);

            InitializationConstructors();
        }

        ///<summary> Конструктор </summary>
        public ViewChatMessageDetail(ModelChat modelChat, List<ModelChatMessage> listRedirectMessage)
        {
            InitializeComponent();

            _viewModelChatDetail = new ViewModelChatDetail(modelChat);

            InitializationConstructors();

            QuoteMessage(listRedirectMessage);
        }

        ///<summary> Конструктор </summary>
        public ViewChatMessageDetail(ModelContact modelContact, List<ModelChatMessage> listRedirectMessage)
        {
            InitializeComponent();

            _viewModelChatDetail = new ViewModelChatDetail(modelContact);

            InitializationConstructors();

            QuoteMessage(listRedirectMessage);
        }

        /// <summary> Инициализация конструкторов </summary>
        private void InitializationConstructors()
        {
            ChangeChatActive();

            ChangeChatIsGroup();

            DataContext = _viewModelChatDetail;

            LeaveChatSeparator.SetBinding(Separator.VisibilityProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("CurrentModelChat.IsP2P"), Converter = new ConverterBoolToVisibilityCollapsedInversion() });
            LeaceChatContextMenuItem.SetBinding(Separator.VisibilityProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("CurrentModelChat.IsP2P"), Converter = new ConverterBoolToVisibilityCollapsedInversion() });

            WindowMain.CurrentMainWindow.Deactivated += CurrentMainWindow_Deactivated;
            WindowMain.CurrentMainWindow.Activated += CurrentMainWindow_Activated;

            var flagSeparatorUnreadMessage = true;
            //var isServeredMessages = true;
            _lastMessage = new ModelChatMessage() { SendTime = new DateTime().Date };

            _dateLastMessage = new DateTime().Date;

            _listDataMessage = new List<TextBlock>();
            _listGridDataMessage = new List<Grid>();

            if (_viewModelChatDetail.ListModelChatMessage.Count > 0)
                _dateFirstMessage = _viewModelChatDetail.ListModelChatMessage.First().SendTime.Date;

            foreach (var i in _viewModelChatDetail.ListModelChatMessage)
            {
                if (!DateTime.Equals(_dateLastMessage, i.SendTime.Date))
                {
                    _dateLastMessage = i.SendTime.Date;
                    ShowDateMessage(i);
                }

                if (flagSeparatorUnreadMessage != i.Readed)
                {
                    flagSeparatorUnreadMessage = i.Readed;

                    ShowSeparatorUnreadMessage();
                }
                
                AddMessageControl(i);
            }
            
            ScrollViewerListMessage.ScrollToBottom();

            _viewModelChatDetail.EventViewModel += ViewModelChatDetailOnEventViewModel;

            _viewModelChatDetail.CurrentModelChat.PropertyChanged += CurrentModelChatOnPropertyChanged;

            if (!CurrentModelChat.IsP2P)
            {
                _viewModelChatDetail.RefreshListModelContactStatus();
            }

            if (_viewModelChatDetail.CurrentModelChat.HaveDraft) ShowDraftMessage();

            var MenuItemCopy = new MenuItem();
            MenuItemCopy.SetResourceReference(MenuItem.HeaderProperty, @"ViewChatDetail_CopyMessage");
            MenuItemCopy.Click += MenuItemCopy_Click;

            var MenuItemEdit = new MenuItem();
            MenuItemEdit.SetResourceReference(MenuItem.HeaderProperty, @"ViewChatDetail_EditMessage");
            MenuItemEdit.Click += MenuItemEdit_Click;
            MenuItemEdit.Visibility = Visibility.Collapsed;

            _menuItemDelete = new MenuItem();
            _menuItemDelete.SetResourceReference(MenuItem.HeaderProperty, @"ViewChatDetail_DeleteMessage");
            _menuItemDelete.Click += MenuItemDelete_Click;
            _menuItemDelete.Visibility = Visibility.Collapsed;

            var MenuItemReply = new MenuItem();
            MenuItemReply.SetResourceReference(MenuItem.HeaderProperty, @"ViewChatDetail_AnswerMessage");
            MenuItemReply.Click += MenuItemReply_Click;
            MenuItemReply.Visibility = Visibility.Collapsed;

            _listMenuItemForCollapsedOfTime.Add(MenuItemEdit);
            _listMenuItemForCollapsedOfTime.Add(_menuItemDelete);
            _listMenuItemForCollapsedAnActive.Add(MenuItemReply);

            _contextMenuMessage.Items.Add(MenuItemCopy);
            _contextMenuMessage.Items.Add(MenuItemEdit);
            _contextMenuMessage.Items.Add(_menuItemDelete);
            _contextMenuMessage.Items.Add(MenuItemReply);
        }

        /// <summary> Отобразить черновик </summary>
        private void ShowDraftMessage()
        {
            TextBlockWriteMessage.Visibility = Visibility.Collapsed;

            if (_viewModelChatDetail.CurrentModelChat.DraftMessage.HaveQuoted)
                QuoteMessage(_viewModelChatDetail.CurrentModelChat.DraftMessage.ListQuotedModelChatMessage.ToList());
        }

        /// <summary> Постраничный вывод старых сообщений </summary>
        private void AddPageOldMessages(List<ModelChatMessage> listModelChatMessage)
        {
            var dateLastMessageOfList = listModelChatMessage.Last().SendTime.Date;

            if (DateTime.Equals(_dateFirstMessage, dateLastMessageOfList))
            {
                var grid = _listGridDataMessage.First();

                GridListMessage.Children.Remove(grid);

                _listGridDataMessage.RemoveAt(0);

                _listDataMessage.RemoveAt(0);
            }

            _dateFirstMessage = listModelChatMessage.First().SendTime.Date;

            var dateLastMessage = new DateTime().Date;

            var lastMessage = new ModelChatMessage() { SendTime = new DateTime().Date };

            BubbleMessage lastBubble = null;

            var index = 0;

            var indexDateMessage = 0;

            foreach (var modelChatMessage in listModelChatMessage)
            {
                if (!DateTime.Equals(dateLastMessage, modelChatMessage.SendTime.Date))
                {
                    dateLastMessage = modelChatMessage.SendTime.Date;
                    ShowDateMessage(index, indexDateMessage, modelChatMessage);

                    index++;
                    indexDateMessage++;
                }

                if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 0 || modelChatMessage.ModelEnumChatMessageTypeObj.Code == 4 || modelChatMessage.ModelEnumChatMessageTypeObj.Code == 5) // простое текстовое сообщение и сообщение удаленно
                {
                    var unitedControlMessage = new UnitedControlMessage(modelChatMessage, _viewModelChatDetail);

                    PreDoingForUnitedControlMessage(unitedControlMessage);

                    if (BubbleMessage.CanAttached(lastMessage, lastBubble, unitedControlMessage))
                    {
                        lastBubble.AddNewMessage(unitedControlMessage);

                        index = GridListMessage.Children.IndexOf(lastBubble.RetuntGrid);
                    }
                    else
                    {
                        var bubbleMessage = new BubbleMessage(unitedControlMessage);

                        _listBubbleMessage.Add(bubbleMessage);

                        lastBubble = bubbleMessage;

                        GridListMessage.Children.Insert(index, bubbleMessage.RetuntGrid);
                    }
                }
                else
                {
                    if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 6)
                    {
                        GridListMessage.Children.Insert(index, MessageAboutEncryptedChat());
                    }
                    else
                    {
                        var textBlock = SystemMessage(modelChatMessage);

                        _listTextBlockSysMessage.Add(textBlock);

                        GridListMessage.Children.Insert(index, textBlock);
                    }
                }

                lastMessage = modelChatMessage;

                index++;
            }
        }

        /// <summary> Добавление контрола сообщения </summary>
        private void AddMessageControl(ModelChatMessage modelChatMessage)
        {
            AddMessageControl(GridListMessage.Children.Count, modelChatMessage);
        }

        /// <summary> Добавление контрола сообщения </summary>
        private int AddMessageControl(int index, ModelChatMessage modelChatMessage)
        {
            int resultIndex;

            if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 0 || modelChatMessage.ModelEnumChatMessageTypeObj.Code == 4 || modelChatMessage.ModelEnumChatMessageTypeObj.Code == 5) // простое текстовое сообщение и сообщение удаленно
            {
                var unitedControlMessage = new UnitedControlMessage(modelChatMessage, _viewModelChatDetail);

                PreDoingForUnitedControlMessage(unitedControlMessage);

                if (BubbleMessage.CanAttached(_lastMessage, _lastBubble, unitedControlMessage))
                {
                    _lastBubble.AddNewMessage(unitedControlMessage);

                    resultIndex = GridListMessage.Children.IndexOf(_lastBubble.RetuntGrid);
                }
                else
                {
                    var bubbleMessage = new BubbleMessage(unitedControlMessage);

                    _listBubbleMessage.Add(bubbleMessage);

                    _lastBubble = bubbleMessage;

                    if (modelChatMessage.Sender.Iam)
                    {
                        if (!modelChatMessage.Servered)
                        {
                            if (_lastBubbleStatusSend != null) _lastBubbleStatusSend.IsShowStatusServered = false;

                            bubbleMessage.IsShowStatusServered = true;

                            _lastBubbleStatusSend = bubbleMessage;
                        }
                        else
                        {
                            if (_lastBubbleStatusDeliveredToServer != null) _lastBubbleStatusDeliveredToServer.IsShowStatusServered = false;

                            bubbleMessage.IsShowStatusServered = true;

                            _lastBubbleStatusDeliveredToServer = bubbleMessage;
                        }

                        _lastMyBubble = bubbleMessage;
                    }

                    GridListMessage.Children.Insert(index, bubbleMessage.RetuntGrid);

                    resultIndex = index;
                }
            }
            else
            {
                if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 6)
                {
                    GridListMessage.Children.Insert(index, MessageAboutEncryptedChat());
                }
                else
                {
                    var textBlock = SystemMessage(modelChatMessage);

                    _listTextBlockSysMessage.Add(textBlock);

                    GridListMessage.Children.Insert(index, textBlock);
                }

                resultIndex = index;
            }

            _lastMessage = modelChatMessage;

            return resultIndex;
        }

        /// <summary> Контрол системного сообщения </summary>
        private TextBlock SystemMessage(ModelChatMessage modelChatMessage)
        {
            var textBlock = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10)
            };
            textBlock.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSize") });

            if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 3 || modelChatMessage.ModelEnumChatMessageTypeObj.Code == 1)

            {
                var runUser = new Run();
                runUser.SetBinding(Run.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("ModelSystemMessageObj.Prefix") });
                textBlock.Inlines.Add(runUser);

                textBlock.Inlines.Add(" ");

                var runSender = new Run { FontWeight = FontWeights.Bold };
                runSender.SetBinding(Run.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("ModelSystemMessageObj.Sender") });
                textBlock.Inlines.Add(runSender);

                textBlock.Inlines.Add(" ");

                var runAction = new Run();
                runAction.SetBinding(Run.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("ModelSystemMessageObj.ChatAction") });
                textBlock.Inlines.Add(runAction);

                textBlock.Inlines.Add(" ");

                var runContent = new Run { FontWeight = FontWeights.Bold };
                runContent.SetBinding(Run.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("ModelSystemMessageObj.Content") });
                textBlock.Inlines.Add(runContent);

                textBlock.Inlines.Add(" ");

                var runPostfix = new Run();
                runPostfix.SetBinding(Run.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("ModelSystemMessageObj.Postfix") });
                textBlock.Inlines.Add(runPostfix);
            }
            else
            {
                textBlock.SetBinding(TextBlock.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("StringContentText") });
            }

            return textBlock;
        }

        /// <summary> Контрол сообщения о шифрованости чата </summary>
        private Grid MessageAboutEncryptedChat()
        {
            var rectangle = new Rectangle { RadiusX = 10, RadiusY = 10, Fill = Brushes.White, StrokeThickness = 2, Stroke = (Brush)(new BrushConverter().ConvertFrom("#ececec")) };
            Grid.SetColumn(rectangle, 1);

            var textBlockEncryptedChat = new TextBlock();
            textBlockEncryptedChat.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_EncryptedChat");

            var textBlockTextHyperLinck = new TextBlock();
            textBlockTextHyperLinck.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_More");

            var hyperLinckMoreAboutEncryption = new Hyperlink { Foreground = Brushes.Green };
            hyperLinckMoreAboutEncryption.Inlines.Add(textBlockTextHyperLinck);
            hyperLinckMoreAboutEncryption.Click += HyperLinckMoreAboutEncryption_Click;

            var textBlockСomprisingHyperLinck = new TextBlock { HorizontalAlignment = HorizontalAlignment.Center };
            textBlockСomprisingHyperLinck.Inlines.Add(hyperLinckMoreAboutEncryption);

            var image = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 25,
                Width = 25,
                Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/encrypted.png")
            };

            var stackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(10) };
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlockEncryptedChat);
            stackPanel.Children.Add(textBlockСomprisingHyperLinck);
            Grid.SetColumn(stackPanel, 1);

            var grid = new Grid { ColumnDefinitions = { new ColumnDefinition(), new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition() }, Margin = new Thickness(5) };

            grid.Children.Add(rectangle);
            grid.Children.Add(stackPanel);

            return grid;
        }

        /// <summary> Обработчик нажатия на гиперссылку узнать подробнее про шифрование </summary>
        private void HyperLinckMoreAboutEncryption_Click(object sender, RoutedEventArgs e)
        {
            ShowComingSoon();
        }

        /// <summary> Предварительные действия над UnitedControlMessage </summary>
        private void PreDoingForUnitedControlMessage(UnitedControlMessage unitedControlMessage)
        {
            unitedControlMessage.RightButtonTap += UnitedControlMessage_RightButtonTap;

            unitedControlMessage.LongLeftButtonTap += UnitedControlMessage_LongLeftButtonTap;

            unitedControlMessage.ShortLeftButtonTap += UnitedControlMessage_ShortLeftButtonTap;

            unitedControlMessage.EventCheckedChanged += UnitedControlMessage_EventCheckedChanged;

            _listGridMessage.Add(unitedControlMessage);
        }

        /// <summary> Изменение выбора сообщения при массовом выборе </summary>
        private void UnitedControlMessage_EventCheckedChanged(object sender, EventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)sender;

            if (unitedControlMessage.IsSelected)
            {
                if (!(bool)CheckBoxSelectAll.IsChecked)
                {
                    foreach (var umc in _listGridMessage)
                    {
                        if (!umc.IsSelected)
                            return;
                    }

                    CheckBoxSelectAll.IsChecked = true;
                }
            }
            else
            {
                if ((bool)CheckBoxSelectAll.IsChecked)
                {
                    CheckBoxSelectAll.IsChecked = false;
                }
            }
        }

        /// <summary> Короткий клик по сообщению </summary>
        private void UnitedControlMessage_ShortLeftButtonTap(object sender, EventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)sender;

            if (_isCheckBoxView)
            {
                unitedControlMessage.CheckedChanged();
            }
        }

        /// <summary> Долгий клик по сообщению </summary>
        private void UnitedControlMessage_LongLeftButtonTap(object sender, EventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)sender;

            if (_isCheckBoxView)
            {
                unitedControlMessage.CheckedChanged();
            }
            else
            {
                EnableMultipleChoice();

                unitedControlMessage.IsSelected = true;
            }
        }

        /// <summary> Запустить множественный выбор сообщений </summary>
        public void EnableMultipleChoice()
        {
            foreach (var ucm in _listGridMessage)
            {
                ucm.IsSelected = false;

                ucm.СheckBoxObj.Visibility = Visibility.Visible;
            }

            RectangleMessage.Fill = (Brush)(new BrushConverter().ConvertFrom("#bdb6b8"));

            TopMenuMultipleChoice.Visibility = Visibility.Visible;

            BottomMenuMultipleChoice.Visibility = Visibility.Visible;

            GridSetMessage.Visibility = Visibility.Collapsed;

            _isCheckBoxView = true;
        }

        /// <summary> Обработка клика по тексту сообщения </summary>
        private void UnitedControlMessage_RightButtonTap(object sender, EventArgs e)
        {
            if (_isCheckBoxView)
                return;

            var unitedControlMessage = (UnitedControlMessage)sender;

            var modelMessage = unitedControlMessage.Message;

            _contextMenuMessage.Tag = unitedControlMessage;

            if (_viewModelChatDetail.CurrentModelChat.Active)
            {
                foreach (var menuItem in _listMenuItemForCollapsedAnActive)
                {
                    if (!_haveMessageToReply)
                        menuItem.Visibility = Visibility.Visible;
                }

                if (modelMessage.Sender.Iam && modelMessage.ModelEnumChatMessageTypeObj.Code == 0 && _viewModelChatDetail.CanEditMessage(modelMessage))
                {
                    foreach (var menuItem in _listMenuItemForCollapsedOfTime)
                    {
                        menuItem.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    foreach (var menuItem in _listMenuItemForCollapsedOfTime)
                    {
                        menuItem.Visibility = Visibility.Collapsed;
                    }
                }

                if (modelMessage.Sender.Iam && modelMessage.ModelEnumChatMessageTypeObj.Code == 4 && _viewModelChatDetail.CanEditMessage(modelMessage))
                {
                    _menuItemDelete.Visibility = Visibility.Visible;
                }
            }

            _contextMenuMessage.IsOpen = true;
        }

        /// <summary> Обработка клика по меню копировать сообщение </summary>
        private void MenuItemCopy_Click(object sender, RoutedEventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)((ContextMenu)((MenuItem)sender).Parent).Tag;

            var copyText = string.Empty;

            if (unitedControlMessage.Message.ModelEnumChatMessageTypeObj.Code == 4)
            {
                var contactDetrails = Application.Current.TryFindResource(@"ViewChatDetail_ContactDetails") as string;
                var fullName = unitedControlMessage.Message.ModelContactData.FullName;

                copyText = contactDetrails + " " + fullName;
            }
            else
            {
                copyText = unitedControlMessage.Message.StringContent;
            }

            Clipboard.SetText(copyText, TextDataFormat.UnicodeText);

            foreach (var menuItem in _listMenuItemForCollapsedOfTime)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            foreach (var menuItem in _listMenuItemForCollapsedAnActive)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }
        }

        //может быть как-то по другому сделать
        /// <summary> Обработка клика по меню удалить сообщение </summary>
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)((ContextMenu)((MenuItem)sender).Parent).Tag;

            MarkDeleteMessage(unitedControlMessage);

            foreach (var menuItem in _listMenuItemForCollapsedOfTime)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            foreach (var menuItem in _listMenuItemForCollapsedAnActive)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary> Обработка клика по меню редактировать сообщение </summary>
        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)((ContextMenu)((MenuItem)sender).Parent).Tag;

            ControlEditMessage = unitedControlMessage;

            var windowUserSettings = new WindowStandard(new ViewChatEditMessage(this, _viewModelChatDetail))
            {
                MinHeight = 330,
                Height = 330,
                MinWidth = 530,
                Width = 530,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                Owner = System.Windows.Window.GetWindow(this),
                Style = Application.Current.TryFindResource(@"VS2012MessageBoxStyle") as Style
            };

            windowUserSettings.ShowDialog();

            foreach (var menuItem in _listMenuItemForCollapsedOfTime)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            foreach (var menuItem in _listMenuItemForCollapsedAnActive)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary> Ответить на сообщение </summary>
        private void MenuItemReply_Click(object sender, RoutedEventArgs e)
        {
            var unitedControlMessage = (UnitedControlMessage)((ContextMenu)((MenuItem)sender).Parent).Tag;

            GridQuotedMessage.Visibility = Visibility.Visible;
            RectangleQuotedMessage.Visibility = Visibility.Visible;
            ButtonRemoveQuotedMessage.Visibility = Visibility.Visible;

            var quotedMessage = new QuotedMessage(unitedControlMessage.Message, _viewModelChatDetail);

            var attachQuotedMessage = quotedMessage.ReturnGrid;

            ArrayListTextBlockQuotedMessage = quotedMessage.ArrayListTextBlock;

            GridQuotedMessage.Children.Add(attachQuotedMessage);

            _haveMessageToReply = true;

            justAnswerCurrentMessage = true;

            _viewModelChatDetail.AddToQuotedMessage(unitedControlMessage.Message);

            foreach (var menuItem in _listMenuItemForCollapsedOfTime)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            foreach (var menuItem in _listMenuItemForCollapsedAnActive)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            FocusManager.SetFocusedElement(new DependencyObject(), TextBoxWriteMessage);
        }

        /// <summary> Редактировать сообщение </summary>
        public void EditMessage(UnitedControlMessage unitedControlMessage, string newText)
        {
            _viewModelChatDetail.EditMessage(unitedControlMessage.Message, newText);

            unitedControlMessage.Refresh();
        }

        /// <summary> Редактировать сообщение с ответом </summary>
        public void EditMessage(UnitedControlMessage unitedControlMessage, string newText, bool shouldDeleteQuote)
        {
            _viewModelChatDetail.EditMessage(unitedControlMessage.Message, newText, shouldDeleteQuote);

            var haveMessageText = String.IsNullOrWhiteSpace(unitedControlMessage.TextBlockObj.Text);

            unitedControlMessage.Refresh();
            
            if (shouldDeleteQuote || haveMessageText)
            {
                var indexBubbleMessage = GridListMessage.Children.IndexOf(unitedControlMessage.BubbleMessageObj.RetuntGrid);

                unitedControlMessage.BubbleMessageObj.RetuntGrid.Children.Clear();

                GridListMessage.Children.Remove(unitedControlMessage.BubbleMessageObj.RetuntGrid);

                GridListMessage.Children.Insert(indexBubbleMessage, new BubbleMessage(unitedControlMessage).RetuntGrid);
            }
        }

        /// <summary> Пометить сообщение удаленным </summary>
        private void MarkDeleteMessage(UnitedControlMessage unitedControlMessage)
        {
            unitedControlMessage.Delete();

            _viewModelChatDetail.DeleteModelChatMessage(unitedControlMessage.Message);

            var bubbleMessage = unitedControlMessage.BubbleMessageObj;

            //bubbleMessage.Remove(unitedControlMessage);

            var indexBubbleMessage = GridListMessage.Children.IndexOf(bubbleMessage.RetuntGrid);

            var listBubble = bubbleMessage.GetBubbleAfterRemove(unitedControlMessage);

            GridListMessage.Children.Remove(bubbleMessage.RetuntGrid);

            var plusIndex = 0;

            foreach (var bubble in listBubble)
            {
                GridListMessage.Children.Insert(indexBubbleMessage + plusIndex, bubble.RetuntGrid);

                plusIndex++;
            }
        }

        //сейчас может изменятся только Servered == false -> Servered == true
        /// <summary> Обновить статус доставлености сообщения </summary>
        private void RefreshStatusServered(UnitedControlMessage unitedControlMessage)
        {
            unitedControlMessage.Message.Servered = true;

            var ownBubble = unitedControlMessage.BubbleMessageObj;

            var one = ownBubble.ListUnitedControlMessageInBubbleMessage.Count == 1;

            var isContained = (_lastBubbleStatusSend == null) ? false : _lastBubbleStatusSend.ListUnitedControlMessageInBubbleMessage.Contains(unitedControlMessage);

            var canAttach = false;

            var indexLastMessage = _viewModelChatDetail.ListModelChatMessage.IndexOf(unitedControlMessage.Message) - 1;

            if (indexLastMessage != -1)
            {
                var lastMessage = _viewModelChatDetail.ListModelChatMessage[indexLastMessage];

                canAttach = BubbleMessage.CanAttached(lastMessage, _lastBubbleStatusDeliveredToServer, unitedControlMessage);
            }
            
            if (!one)
            {
                ownBubble.Remove(unitedControlMessage);

                if (canAttach)
                {
                    _lastBubbleStatusDeliveredToServer.AddNewMessage(unitedControlMessage);
                }
                else
                {
                    if (_lastBubbleStatusDeliveredToServer != null)
                        _lastBubbleStatusDeliveredToServer.IsShowStatusServered = false;

                    var bubbleMessage = new BubbleMessage(unitedControlMessage);

                    bubbleMessage.IsShowStatusServered = true;

                    bubbleMessage.Servered = true;

                    _lastBubbleStatusDeliveredToServer = bubbleMessage;

                    _listBubbleMessage.Add(bubbleMessage);

                    var index = GridListMessage.Children.IndexOf(ownBubble.RetuntGrid);

                    GridListMessage.Children.Insert(index, bubbleMessage.RetuntGrid);
                }
            }
            else
            {
                if (canAttach)
                {
                    ownBubble.Remove(unitedControlMessage);

                    _lastBubbleStatusDeliveredToServer.AddNewMessage(unitedControlMessage);

                    _lastBubble = _lastBubbleStatusDeliveredToServer;

                    GridListMessage.Children.Remove(ownBubble.RetuntGrid);
                }
                else
                {
                    if (_lastBubbleStatusDeliveredToServer != null)
                        _lastBubbleStatusDeliveredToServer.IsShowStatusServered = false;

                    ownBubble.Servered = true;

                    ownBubble.IsShowStatusServered = true;

                    _lastBubbleStatusDeliveredToServer = ownBubble;
                }

                if (isContained) _lastBubbleStatusSend = null;
            }
        }

        //переделать хранить эту полосу
        /// <summary> Отрисовка пососы "Непрочитанные сообщения" </summary>
        private void ShowSeparatorUnreadMessage()
        {
            if (_flagDisplayedSeparatorUnreadMessage) return;

            var grid = new Grid { RowDefinitions = { new RowDefinition(), new RowDefinition(), new RowDefinition() }, Margin = new Thickness(5), Name = "SeparatorUnreadMessage" };

            var rectangleSeparatorTop = new Rectangle { Fill = Brushes.LightGray, Height = 1, Margin = new Thickness(4) };

            Grid.SetRow(rectangleSeparatorTop, 0);

            var textBlock = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(9)
            };
            textBlock.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSize") });

            textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_UnreadMessages");

            Grid.SetRow(textBlock, 1);

            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            var rectangle = new Rectangle
            {
                Fill = Brushes.WhiteSmoke,
                RadiusX = 14,
                RadiusY = 14,
                Height = 28,
                Width = textBlock.DesiredSize.Width + 30,
                Margin = new Thickness(4)
            };

            Grid.SetRow(rectangle, 1);

            var rectangleSeparatorDown = new Rectangle { Fill = Brushes.LightGray, Height = 1, Margin = new Thickness(4) };

            Grid.SetRow(rectangleSeparatorDown, 2);

            grid.Children.Add(rectangleSeparatorTop);
            grid.Children.Add(rectangle);
            grid.Children.Add(textBlock);
            grid.Children.Add(rectangleSeparatorDown);

            GridListMessage.Children.Add(grid);

            _flagDisplayedSeparatorUnreadMessage = true;
        }

        /// <summary> Пометить сообщения прочитанными и убрать полосу непрочитанных сообщений </summary>
        private void MarkReadModelMessage()
        {
            _viewModelChatDetail.MarkReadModelMessage();

            if (!_flagDisplayedSeparatorUnreadMessage) return;

            var gridSeparatorUnreadMessage = new UIElement();

            foreach (var childListMessage in GridListMessage.Children)
            {
                if (childListMessage is Grid)
                {
                    if ((childListMessage as Grid).Name == "SeparatorUnreadMessage")
                    {
                        gridSeparatorUnreadMessage = (UIElement)childListMessage;

                        GridListMessage.Children.Remove(gridSeparatorUnreadMessage);

                        break;
                    }
                }
            }

            _flagDisplayedSeparatorUnreadMessage = false;

            _viewModelChatDetail.MarkReadModelMessage();
        }

        /// <summary> Обработчик изменений CurrentModelChat </summary>
        private void CurrentModelChatOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == @"Active")
            {
                ChangeChatActive();
            }

            if (e.PropertyName == @"IsGroup")
            {
                ChangeChatIsGroup();
            }

            if (!CurrentModelChat.IsP2P)
            {
                _viewModelChatDetail.RefreshListModelContactStatus();
            }
        }

        /// <summary> Обработчик изменения активности чата </summary>
        private void ChangeChatActive()
        {
            if (_viewModelChatDetail.CurrentModelChat.Active)
            {
                RectangleMessage.Fill = Brushes.Red;
                GridWriteMessageDisable.Visibility = Visibility.Hidden;
            }
            else
            {
                RectangleMessage.Fill = Brushes.Silver;
                GridWriteMessageDisable.Visibility = Visibility.Visible;
                ScrollViewerListMessage.Focus();
            }

            if (!CurrentModelChat.IsP2P)
            {
                _viewModelChatDetail.RefreshListModelContactStatus();
            }
        }

        /// <summary> Обработчик изменения мультичата чата </summary>
        private void ChangeChatIsGroup()
        {
            if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
            {
                GridModelContactStatus.Visibility = Visibility.Collapsed;
                StackPanelCountModelContact.Visibility = Visibility.Visible;
                ButtonVideo.Visibility = Visibility.Collapsed;
                ButtonPhone.Command = _viewModelChatDetail.CommandComingSoon;
                ButtonPhone.Click -= ButtonPhone_OnClick;
                ImagePhone.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_offline.png");

                _viewModelChatDetail.RefreshListModelContactStatus();
            }
            else
            {
                GridModelContactStatus.Visibility = Visibility.Visible;
                StackPanelCountModelContact.Visibility = Visibility.Hidden;
                ButtonVideo.Visibility = Visibility.Visible;
                ButtonPhone.Command = null;
                ButtonPhone.Click += ButtonPhone_OnClick;

                GridMain.Dispatcher.Invoke(ChangeStatusModelContact);

                if (_viewModelChatDetail.CurrentModelChat.ModelContactChat != null) _viewModelChatDetail.CurrentModelChat.ModelContactChat.PropertyChanged += ModelContactChatOnPropertyChanged;
            }
        }

        /// <summary> Обработчик изменений ModelContactChat </summary>
        private void ModelContactChatOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            GridMain.Dispatcher.Invoke(ChangeStatusModelContact);
        }

        /// <summary> Обработчик события EventViewModel </summary>
        private void ViewModelChatDetailOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == "EditMessage")
            {
                var listEditModelChatMessage = (List<ModelChatMessage>)e.Data;

                foreach (var modelChatMessage in listEditModelChatMessage)
                {
                    var unitedControlMessage = _listGridMessage.FirstOrDefault(obj => obj.Message.Id == modelChatMessage.Id && !modelChatMessage.Sender.Iam);

                    if (unitedControlMessage != null) EditMessage(unitedControlMessage, modelChatMessage.StringContentText);
                }
            }

            if (e.Key == "DeleteMessage")
            {
                var listDeleteModelChatMessage = (List<ModelChatMessage>)e.Data;

                foreach (var modelChatMessage in listDeleteModelChatMessage)
                {
                    var unitedControlMessage = _listGridMessage.FirstOrDefault(obj => obj.Message.Id == modelChatMessage.Id && !modelChatMessage.Sender.Iam);

                    if (unitedControlMessage != null) MarkDeleteMessage(unitedControlMessage);
                }
            }

            if (e.Key == "ServeredMessage")
            {
                var listServeredModelChatMessage = (List<ModelChatMessage>)e.Data;

                foreach (var modelChatMessage in listServeredModelChatMessage)
                {
                    var unitedControlMessage = _listGridMessage.FirstOrDefault(obj => obj.Message.Id == modelChatMessage.Id);

                    unitedControlMessage.Message.Servered = modelChatMessage.Servered;

                    RefreshStatusServered(unitedControlMessage);
                }
            }

            if (e.Key == "ComingSoon")
            {
                ShowComingSoon();

                return;
            }

            if (e.Key == "SendModelMessage")
            {
                var modelChatMessage = (ModelChatMessage)e.Data;
                //здесь может не вычислять дату а запоминать ее при инициализации уже запомнил последнее сообщение нужно из него взять дату
                if (!DateTime.Equals(modelChatMessage.SendTime.Date, _dateLastMessage))
                {
                    _dateLastMessage = modelChatMessage.SendTime.Date;

                    ShowDateMessage(modelChatMessage);
                }

                AddMessageControl(modelChatMessage);

                ScrollViewerListMessage.ScrollToBottom();

                return;
            }

            if (e.Key == "NewMessage")
            {
                var listModelChatMessage = (List<ModelChatMessage>)e.Data;

                foreach (var i in (List<ModelChatMessage>)e.Data)
                {
                    if (!_listGridMessage.Any(obj => obj.Message.Id == i.Id))
                    {
                        if (!DateTime.Equals(i.SendTime.Date, _dateLastMessage))
                        {
                            _dateLastMessage = i.SendTime.Date;

                            ShowDateMessage(i);
                        }

                        if (!IsScrollAtBottom || !_flagActivatedWindowMain)
                        {
                            ShowSeparatorUnreadMessage();
                        }
                        else
                        {
                            MarkReadModelMessage();
                        }

                        AddMessageControl(i);
                    }
                }
            }
        }

        /// <summary> Обработчик изменения статуса собеседника </summary>
        private void ChangeStatusModelContact()
        {
            switch (_viewModelChatDetail.CurrentModelChat.ModelContactChat?.ModelEnumUserBaseStatusObj?.Code)
            {
                case 1:
                    ImagePhone.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_online.png");
                    ImageVideo.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/video_online.png");
                    break;
                case 3:
                    ImagePhone.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_dnd.png");
                    ImageVideo.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/video_dnd.png");
                    break;
                default:
                    ImagePhone.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/phone_offline.png");
                    ImageVideo.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/video_offline.png");
                    break;
            }
        }

        /// <summary> Показать окно Coming Soon </summary>
        private void ShowComingSoon()
        {
            WindowMessageBox.ShowComingSoon(this);
        }

        ///<summary> Обработчик нажатия на кнопку позвонить </summary>
        private void ButtonPhone_OnClick(object sender, RoutedEventArgs e)
        {
            // Кастыль !!! Т.к. из бизнес логики опять возвращается контакт с неправильной подпиской и в большом окне текущего звонка не отображается кнопка чата
            _viewModelChatDetail.CurrentModelChat.ModelContactChat.ModelContactSubscriptionObj.ModelEnumSubscriptionStateObj = ModelEnumSubscriptionState.GetModelEnum(3); // Both

            WindowCallActive.OutgoingCall(_viewModelChatDetail.CurrentModelChat.ModelContactChat);
        }

        /// <summary> Обработчик изменения размера UserControl </summary>
        private void ViewChatMessageDetail_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBoxWriteMessage.Width = ActualWidth - 111; // 111 это мэджик намбер: примерно размер пространства кнопки-скрепки
            ItemsControlChatUserList.Width = ActualWidth - 35;
            GridHeaderChat.Width = ActualWidth - 5;
            ChatTitleTextBox.Width = ActualWidth - 228; // 228 мэджик намбер: премерно размер всех кнопок и картинок около заголовка чата

            if (ArrayListTextBlockQuotedMessage != null)
            {
                for (var j = 0; j < ArrayListTextBlockQuotedMessage.Count(); j++)
                    foreach (var k in ArrayListTextBlockQuotedMessage[j])
                    {
                        k.MaxWidth = ActualWidth - 115;
                    }
            }

            if (_isCheckBoxView)
            {
                foreach (var i in _listGridMessage)
                {
                    if (i.Message.ModelEnumChatMessageTypeObj.Code == 0)
                        i.TextBlockObj.MaxWidth = ActualWidth - 183;

                    //!!!!!!! ЭТО ОЧЕНЬ ПЛОХО
                    if (i.Message.HaveQuoted)
                    {
                        for (var j = 0; j < i.ArrayListTextBlockQuotedMessage.Count(); j++)
                            foreach (var k in i.ArrayListTextBlockQuotedMessage[j])
                            {
                                k.MaxWidth = ActualWidth - 203;
                            }

                    }
                }
            }
            else
            {
                foreach (var i in _listGridMessage)
                {
                    if (i.Message.ModelEnumChatMessageTypeObj.Code == 0)
                        i.TextBlockObj.MaxWidth = ActualWidth - 150;

                    //!!!!!!! ЭТО ОЧЕНЬ ПЛОХО
                    if (i.Message.HaveQuoted)
                    {
                        for (var j = 0; j < i.ArrayListTextBlockQuotedMessage.Count(); j++)
                            foreach (var k in i.ArrayListTextBlockQuotedMessage[j])
                            {
                                k.MaxWidth = ActualWidth - 170;
                            }
                    }
                }
            }

            foreach (var i in _listTextBlockSysMessage)
            {
                i.MaxWidth = ActualWidth - 100;
            }
        }

        /// <summary> Разрешить отправку сообщений </summary>
        private void EnableSendingMessages(bool isPossibleToSend)
        {
            if (isPossibleToSend)
            {
                RectangleSendMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                RectangleSendMessage.Visibility = Visibility.Visible;
                Panel.SetZIndex(RectangleSendMessage, 1);
                RectangleSendMessage.Opacity = 0.5;
            }

            _isPossibleToSend = isPossibleToSend;
        }

        /// <summary> Обработчик ввода символов в TextBoxWriteMessage (провекрка на Control + Enter) </summary>
        private void TextBoxWriteMessage_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_isPossibleToSend || !justAnswerCurrentMessage)
                {
                    MarkReadModelMessage();

                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                    {
                        var textBox = (TextBox)sender;

                        var caretIndex = textBox.CaretIndex;

                        textBox.Text = textBox.Text.Insert(caretIndex, "\n");

                        textBox.CaretIndex = caretIndex + 1;
                    }
                    else
                    {
                        if (_haveMessageToReply)
                            ClearGridQuotedMessage();

                        justAnswerCurrentMessage = true;

                        _viewModelChatDetail.CommandSendModelMessage.Execute(null);

                        EnableSendingMessages(false);
                    }
                }

                e.Handled = true;
            }
        }

        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            _viewModelChatDetail.MarkReadModelMessage();

            _viewModelChatDetail.EventViewModel -= ViewModelChatDetailOnEventViewModel;

            _viewModelChatDetail.CurrentModelChat.PropertyChanged -= CurrentModelChatOnPropertyChanged;

            if (_viewModelChatDetail.CurrentModelChat.ModelContactChat != null)
            {
                _viewModelChatDetail.CurrentModelChat.ModelContactChat.PropertyChanged -= ModelContactChatOnPropertyChanged;
            }

            foreach (var i in _listGridMessage)
            {
                i.RightButtonTap -= UnitedControlMessage_RightButtonTap;

                i.LongLeftButtonTap -= UnitedControlMessage_LongLeftButtonTap;

                i.ShortLeftButtonTap -= UnitedControlMessage_ShortLeftButtonTap;

                i.EventCheckedChanged -= UnitedControlMessage_EventCheckedChanged;

                i.Dispose();
            }
            //нужно будет убрать подписку с Кирилла сообытий
            _viewModelChatDetail.Dispose();

            _viewModelChatDetail = null;

            ButtonPhone.Command = null;
            ButtonPhone.Click -= ButtonPhone_OnClick;

            WindowMain.CurrentMainWindow.Deactivated -= CurrentMainWindow_Deactivated;
            WindowMain.CurrentMainWindow.Activated -= CurrentMainWindow_Activated;
        }

        /// <summary> Обработка прокрутки скролла </summary>
        private void ScrollViewerListMessage_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ShowTopDateMessage();

            if (_isPagingMessageNow)
            {
                ScrollViewerListMessage.ScrollToVerticalOffset(ScrollViewerListMessage.ExtentHeight - _oldScrollHeight);

                _isPagingMessageNow = false;
            }
            else
            {
                if (ScrollViewerListMessage.ExtentHeight > ScrollViewerListMessage.ViewportHeight &&
                    ScrollViewerListMessage.VerticalOffset == 0 && _viewModelChatDetail.GetMoreOldMessage())
                {
                    AddPageOldMessages(_viewModelChatDetail.CurrentListOldMessage);

                    _oldScrollHeight = ScrollViewerListMessage.ExtentHeight;

                    _isPagingMessageNow = true;
                }
            }
        }

        /// <summary> Отрисовка даты в верху листа с сообщениями </summary>
        private void ShowTopDateMessage()
        {
            if (_listDataMessage.Count == 0) return;

            var positionDateMessage = _listDataMessage[_indexTopDataMessage].TransformToAncestor(this).Transform(new Point(0, 0)).Y;
            var positionTopDateMessage = TextBlockTopDateMessage.TransformToAncestor(this).Transform(new Point(0, 0)).Y;
            var differenceСoordinate = positionTopDateMessage - positionDateMessage;

            if (differenceСoordinate >= 0)
            {
                GridTopDateMessage.Visibility = Visibility.Visible;

                TextBlockTopDateMessage.Text = _listDataMessage[_indexTopDataMessage].Text;

                if (_listDataMessage.Count > _indexTopDataMessage + 1)
                {
                    for (int i = _indexTopDataMessage + 1; i < _listDataMessage.Count; i++)
                    {
                        var positionNextDateMessage = _listDataMessage[i].TransformToAncestor(this).Transform(new Point(0, 0)).Y;
                        var differenceСoordinateNextMessage = positionTopDateMessage - positionNextDateMessage;

                        if (differenceСoordinateNextMessage >= 0)
                        {
                            _indexTopDataMessage = i;

                            TextBlockTopDateMessage.Text = _listDataMessage[i].Text;
                        }
                        else break;
                    }
                }
            }
            else
            {
                if (_indexTopDataMessage == 0)
                {
                    GridTopDateMessage.Visibility = Visibility.Hidden;
                }
                else
                {
                    _indexTopDataMessage = _indexTopDataMessage - 1;

                    TextBlockTopDateMessage.Text = _listDataMessage[_indexTopDataMessage].Text;
                }
            }
        }

        /// <summary> Отрисовка даты сообщений </summary>
        private void ShowDateMessage(ModelChatMessage modelChatMessage)
        {
            var grid = new Grid();

            var textBlock = GetDateTextBlockFromMessage(modelChatMessage);
            var rectangle = RectangleForDateMessage();

            grid.Children.Add(rectangle);
            grid.Children.Add(textBlock);

            _listDataMessage.Add(textBlock);
            _listGridDataMessage.Add(grid);

            GridListMessage.Children.Add(grid);
        }

        /// <summary> Отрисовка даты сообщений </summary>
        private void ShowDateMessage(int index, int indexDateMessage, ModelChatMessage modelChatMessage)
        {
            var grid = new Grid();

            var textBlock = GetDateTextBlockFromMessage(modelChatMessage);
            var rectangle = RectangleForDateMessage();

            grid.Children.Add(rectangle);
            grid.Children.Add(textBlock);

            _listDataMessage.Insert(indexDateMessage, textBlock);
            _listGridDataMessage.Insert(indexDateMessage, grid);

            GridListMessage.Children.Insert(index, grid);
        }

        /// <summary> TextBlock с датой сообщения </summary>
        private TextBlock GetDateTextBlockFromMessage(ModelChatMessage modelChatMessage)
        {
            var messageSendDate = modelChatMessage.SendTime.Date;

            var textBlock = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(9)
            };
            textBlock.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSize") });

            if (messageSendDate == DateTime.Today)
            {
                textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_Today");
            }
            else
            {
                if (messageSendDate == DateTime.Today.AddDays(-1))
                {
                    textBlock.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_Yesterday");
                }
                else
                {
                    textBlock.Text = messageSendDate.ToString("d");
                }
            }

            return textBlock;
        }

        /// <summary> Rectangle для сообщения с датой </summary>
        private Rectangle RectangleForDateMessage()
        {
            return new Rectangle { Fill = Brushes.WhiteSmoke, RadiusX = 14, RadiusY = 14, Height = 28, Width = 120, Margin = new Thickness(5) };
        }

        ///<summary> Событие выхода приложения на передний план </summary>
        private void CurrentMainWindow_Activated(object sender, EventArgs e)
        {
            _flagActivatedWindowMain = true;

            if (!CurrentModelChat.IsP2P)
            {
                _viewModelChatDetail.RefreshListModelContactStatus();
            }
        }

        ///<summary> Событие ухода приложения на задний план </summary>
        private void CurrentMainWindow_Deactivated(object sender, EventArgs e)
        {
            _flagActivatedWindowMain = false;

            MarkReadModelMessage();

            if (!CurrentModelChat.IsP2P)
            {
                _viewModelChatDetail.RefreshListModelContactStatus();
            }
        }

        ///<summary> Обработчик нажатия на надпись сообщения </summary>
        private void TextBlockEnterMessage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockWriteMessage.Visibility = Visibility.Collapsed;

            TextBoxWriteMessage.Focus();
        }

        ///<summary> Обработчик получения фокуса поля введения сообщения </summary>
        private void TextBoxWriteMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBlockWriteMessage.Visibility = Visibility.Collapsed;
        }

        ///<summary> Обработчик потери фокуса поля введения сообщения </summary>
        private void TextBoxWriteMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxWriteMessage.Text))
            {
                TextBlockWriteMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary> Обработчик наведения мыши на названия чата </summary>
        private void OnTitleChatMouseEnter(object sender, MouseEventArgs e)
        {
            if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
            {
                ChatTitleEditButton.Visibility = Visibility.Visible;
            }
        }

        /// <summary> Обработчик уведения мыши с названия чата </summary>
        private void OnTitleChatMouseLeave(object sender, MouseEventArgs e)
        {
            if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
            {
                ChatTitleEditButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary> Обработчик ввода символов в ChatTitleTextBox </summary>
        private void ChatTitleTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
            {
                if (e.Key == Key.Escape)
                {
                    ChatTitleTextBox.Text = _viewModelChatDetail.CurrentModelChat.Title;

                    ChatTitleEditButton.Visibility = Visibility.Collapsed;
                    ChatTitleTextBlock.Visibility = Visibility.Visible;

                    ChatTitleTextBox.Visibility = Visibility.Collapsed;
                    ChatTitleEditConfirmButton.Visibility = Visibility.Collapsed;
                    ChatTitleEditAbortButton.Visibility = Visibility.Collapsed;
                    e.Handled = true;
                    return;
                }

                if (e.Key == Key.Enter)
                {
                    ChatTitleChange();
                    e.Handled = true;
                    return;
                }

                if (e.KeyboardDevice.Modifiers == ModifierKeys.Control ||
                    e.KeyboardDevice.Modifiers == ModifierKeys.Alt ||
                    e.KeyboardDevice.Modifiers == ModifierKeys.Windows ||
                    e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.Back ||
                    e.Key == Key.LeftShift || e.Key == Key.RightShift ||
                    e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                {
                    e.Handled = true;
                    return;
                }

                if (ChatTitleTextBox.Text.Length > 30 && ChatTitleTextBox.Text != CurrentModelChat.Title)
                {
                    ChatTitleTextBox.Text = ChatTitleTextBox.Text.Remove(30);
                    ChatTitleTextBox.CaretIndex = ChatTitleTextBox.Text.Length;
                    e.Handled = true;
                }
            }
        }

        /// <summary> Обработчик нажатия иконки для переименования чата </summary>
        public void TitleChatEditMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
            {
                StackPanelTitle.Visibility = Visibility.Collapsed;
                StackPanelEditTitle.Visibility = Visibility.Visible;


                ChatTitleTextBox.Focus();
                ChatTitleTextBox.CaretIndex = ChatTitleTextBox.Text.Length;
                ChatTitleTextBox.SelectAll();
            }
        }

        /// <summary> Обработчик нажатия подтверждения переименования чата </summary>
        private void ChatTitleEditConfirmButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
            {
                ChatTitleChange();
            }
        }

        /// <summary> Изменить название чата </summary>
        private void ChatTitleChange()
        {
            StackPanelTitle.Visibility = Visibility.Visible;

            StackPanelEditTitle.Visibility = Visibility.Collapsed;

            if (ChatTitleTextBox.Text.Equals(_viewModelChatDetail.CurrentModelChat.Title))
            {
                return;
            }

            if (ChatTitleTextBox.Text.Length > 30 && ChatTitleTextBox.Text != CurrentModelChat.Title)
            {
                ChatTitleTextBox.Text = ChatTitleTextBox.Text.Remove(30);
            }

            ChatTitleTextBlock.GetBindingExpression(TextBlock.TextProperty).UpdateSource();
            ChatTitleTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            _viewModelChatDetail.CommandRenameChatModel.Execute(null);
        }

        /// <summary> Отмена изменения названия чата </summary>
        private void TitleChatAbortEditMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChatTitleTextBox.Text = _viewModelChatDetail.CurrentModelChat.Title;

            StackPanelTitle.Visibility = Visibility.Visible;

            StackPanelEditTitle.Visibility = Visibility.Collapsed;
        }

        /// <summary> Обработчик дабл-клика по названию чата </summary>
        private void OnChatTitleTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                TitleChatEditMouseButtonDown(sender, e);
            }
        }

        /// <summary> Обработчик потери фокуса имени чата </summary>
        private void ChatTitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ChatTitleTextBox.Text = _viewModelChatDetail.CurrentModelChat.Title;

            StackPanelTitle.Visibility = Visibility.Visible;

            StackPanelEditTitle.Visibility = Visibility.Collapsed;
        }

        /// <summary> Обработчик нажатия на надпись о количестве собеседников </summary>
        private void StackPanelCountModelContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = ScrollChatUserList.Height,
                Duration = TimeSpan.FromMilliseconds(250)
            };
            animation.To = (ScrollChatUserList.Height > 0) ? 0 : 50;
            ScrollChatUserList.BeginAnimation(ScrollViewer.HeightProperty, animation);
            RotateTransform rt = HideOrOpenUserContactList.RenderTransform as RotateTransform;
            if (rt != null)
            {
                rt.Angle = (rt.Angle > 90) ? 90 : 180;
                HideOrOpenUserContactList.RenderTransform = rt;
            }
        }

        /// <summary> Обработчик удаления собеседника </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _viewModelChatDetail.CommandRemoveFromChat.Execute(((MenuItem)sender).DataContext as ModelContact);

            e.Handled = true;
        }

        /// <summary> Открытие контекстного меню по leftclick "..." </summary>
        private void ContextMenuOpen(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Image;
            var contextMenu = img.ContextMenu;
            contextMenu.IsOpen = true;
            e.Handled = true;
        }

        /// <summary> Обработчик добавления/удаления собеседников </summary>
        public void OnInviteMembers_MouseClick(object sender, RoutedEventArgs e)
        {
            var listModelContact = ViewSelectionContact.ShowInviteAndRevokeChatMembers(CurrentModelChat.ListModelContactNotMe);
            if (listModelContact.Count > 0)
            {
                var contactsToInvite = (new List<ModelContact>()).Concat(listModelContact).ToList();
                foreach (var i in CurrentModelChat.ListModelContactNotMe)
                {
                    contactsToInvite.Remove(contactsToInvite.FirstOrDefault(obj => i.DodicallId == obj.DodicallId));
                }

                var contactsToRevoke = CurrentModelChat.ListModelContactNotMe;
                foreach (var i in listModelContact)
                {
                    contactsToRevoke.Remove(contactsToRevoke.FirstOrDefault(obj => i.DodicallId == obj.DodicallId));
                }
                if (!_viewModelChatDetail.CurrentModelChat.IsP2P)
                {
                    _viewModelChatDetail.InviteAndRevokeChatMembers(contactsToInvite, contactsToRevoke);
                }
                else
                {
                    if (contactsToInvite.Count > 0)
                    {
                        var viewChatMessageDetail = new ViewChatMessageDetail(listModelContact);

                        WindowMain.ShowInRightWorkspace(viewChatMessageDetail);

                        ViewContact.CurrentViewContact.OpenChat(viewChatMessageDetail.CurrentModelChat);
                    }
                }
            }
        }

        /// <summary> Обработчик нажатия иконки для переименования чата </summary>
        private void TitleChatEditMouseButtonDown(object sender, RoutedEventArgs e)
        {
            TitleChatEditMouseButtonDown(sender, null);
        }

        /// <summary> Обработчик выбора всех сообщений </summary>
        private void CheckBoxSelectAll_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckBoxSelectAll.IsChecked)
            {
                foreach (var umc in _listGridMessage)
                {
                    umc.IsSelected = true;
                }
            }
            else
            {
                foreach (var umc in _listGridMessage)
                {
                    umc.IsSelected = false;
                }
            }
        }

        /// <summary> Обработчик нажатия отмены множественного выбора </summary>
        private void ButtonCancelMultipleChoice_Click(object sender, RoutedEventArgs e)
        {
            CloseMultipleChoice();
        }

        /// <summary> Отключить множественный выбор сообщений </summary>
        private void CloseMultipleChoice()
        {
            foreach (var umc in _listGridMessage)
            {
                umc.IsSelected = false;

                umc.СheckBoxObj.Visibility = Visibility.Collapsed;
            }

            RectangleMessage.Fill = Brushes.Red;

            TopMenuMultipleChoice.Visibility = Visibility.Collapsed;

            BottomMenuMultipleChoice.Visibility = Visibility.Collapsed;

            GridSetMessage.Visibility = Visibility.Visible;

            _isCheckBoxView = false;
        }

        /// <summary> Обработчик нажатия по множественному перенаправлению </summary>
        private void ButtonMultipleForwardMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectUMC = _listGridMessage.Where(obj => obj.IsSelected);

            if (selectUMC.Count() == 0) return;

            var listModelMessage = selectUMC.Select(obj => obj.Message).ToList();

            CloseMultipleChoice();

            var windowUserSettings = new WindowStandard(new ViewChatRedirect(listModelMessage))
            {
                MinHeight = 530,
                Height = 530,
                MinWidth = 530,
                Width = 530,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                Owner = System.Windows.Window.GetWindow(this),
                Style = Application.Current.TryFindResource(@"VS2012MessageBoxStyle") as Style
            };

            windowUserSettings.ShowDialog();
        }

        /// <summary> Обработчик нажатия по множественному копированию </summary>
        private void ButtonMultipleCopyMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectUMC = _listGridMessage.Where(obj => obj.IsSelected);

            if (selectUMC.Count() == 0) return;

            ShowComingSoon();

            CloseMultipleChoice();
        }

        /// <summary> Обработчик нажатия по множественному цитированию </summary>
        private void ButtonMultipleCiteMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectUMC = _listGridMessage.Where(obj => obj.IsSelected);

            if (selectUMC.Count() == 0) return;

            var listModelMessage = selectUMC.Select(obj => obj.Message).ToList();

            QuoteMessage(listModelMessage);

            CloseMultipleChoice();

            FocusManager.SetFocusedElement(new DependencyObject(), TextBoxWriteMessage);
        }

        /// <summary> Обработчик нажатия по множественному экспортированию </summary>
        private void ButtonMultipleExportMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectUMC = _listGridMessage.Where(obj => obj.IsSelected);

            if (selectUMC.Count() == 0) return;

            ShowComingSoon();

            CloseMultipleChoice();
        }

        /// <summary> Обработчик нажатия по множественному добавлению в избранное </summary>
        private void ButtonMultipleFavoriteMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectUMC = _listGridMessage.Where(obj => obj.IsSelected);

            if (selectUMC.Count() == 0) return;

            ShowComingSoon();

            CloseMultipleChoice();
        }

        /// <summary> Обработчик нажатия по множественному удалению </summary>
        private void ButtonMultipleDeleteMessage_Click(object sender, RoutedEventArgs e)
        {
            var selectUMC = _listGridMessage.Where(obj => obj.IsSelected);

            if (selectUMC.Count() == 0) return;

            ShowComingSoon();

            CloseMultipleChoice();
        }

        /// <summary> Обработчик нажатия отправить контакт </summary>
        private void Click_ClipSendContact(object sender, RoutedEventArgs e)
        {
            var listModelContact = ViewSelectionContact.ShowSendContacts();
            if (listModelContact.Count > 0)
            {
                _viewModelChatDetail.SendContactListToChat(listModelContact);
            }
        }

        /// <summary> Обработчик нажатия иконки "прикрепить к сообщению" </summary>
        private void Click_ClipButton(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.ContextMenu.IsOpen = true;
            e.Handled = true;
        }

        /// <summary> Очистить поле для цитирования сообщения </summary>
        private void ClearGridQuotedMessage()
        {
            GridQuotedMessage.Children.Clear();

            GridQuotedMessage.Visibility = Visibility.Collapsed;
            RectangleQuotedMessage.Visibility = Visibility.Collapsed;
            ButtonRemoveQuotedMessage.Visibility = Visibility.Collapsed;

            _haveMessageToReply = false;
        }

        /// <summary> Обработчик очистки поля для цитирования </summary>
        private void ButtonRemoveQuotedMessage_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxWriteMessage.Text)) EnableSendingMessages(false);

            justAnswerCurrentMessage = true;

            ClearGridQuotedMessage();

            _viewModelChatDetail.RemoveQuotedMessage();
        }

        /// <summary> Цитировать сообщения </summary>
        private void QuoteMessage(List<ModelChatMessage> listModelMessage)
        {
            GridQuotedMessage.Visibility = Visibility.Visible;
            RectangleQuotedMessage.Visibility = Visibility.Visible;
            ButtonRemoveQuotedMessage.Visibility = Visibility.Visible;

            var quotedMessage = new QuotedMessage(listModelMessage, _viewModelChatDetail);

            var attachQuotedMessage = quotedMessage.ReturnGrid;

            ArrayListTextBlockQuotedMessage = quotedMessage.ArrayListTextBlock;

            GridQuotedMessage.Children.Add(attachQuotedMessage);

            _haveMessageToReply = true;

            EnableSendingMessages(true);

            justAnswerCurrentMessage = false;

            _viewModelChatDetail.AddToQuotedMessage(listModelMessage);

            foreach (var menuItem in _listMenuItemForCollapsedOfTime)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            foreach (var menuItem in _listMenuItemForCollapsedAnActive)
            {
                menuItem.Visibility = Visibility.Collapsed;
            }

            ImageSendMessage.Opacity = 1;
        }

        /// <summary> Обработчик нажатия отправки сообщения </summary>
        private void ImageSendMessage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_haveMessageToReply)
                ClearGridQuotedMessage();

            if (_isPossibleToSend)
            {
                _viewModelChatDetail.CommandSendModelMessage.Execute(null);

                justAnswerCurrentMessage = true;

                EnableSendingMessages(false);
            }
        }

        /// <summary> Обработчик изменения текста в TextBoxWriteMessage (провекрка пустое сообщение) </summary>
        private void TextBoxWriteMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBoxWriteMessage.Text) || !justAnswerCurrentMessage) EnableSendingMessages(true);
            else EnableSendingMessages(false);
        }

        /// <summary> Обработчик клика по множественному выбору </summary>
        private void MultipleChoiceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EnableMultipleChoice();
        }
    }

    /// <summary> Контрол цитируемого сообщения </summary>
    public class QuotedMessage
    {
        /// <summary> Объект ViewModelChatDetail </summary>
        private ViewModelChatDetail _viewModelChatDetail;

        /// <summary> Флаг цитаты </summary>
        private bool _isQuoted;

        /// <summary> Флаг прикрепленности </summary>
        private static bool _isAttached;

        /// <summary> TextBlock-и цитат для ресайза </summary>
        private List<TextBlock>[] _arrayListTextBlock = new List<TextBlock>[5];

        /// <summary> Возвращаемый Grid </summary>
        public Grid ReturnGrid;

        /// <summary> TextBlock-и цитат для ресайза </summary>
        public List<TextBlock>[] ArrayListTextBlock;

        /// <summary> Конструктор ответа </summary>
        public QuotedMessage(ModelChatMessage modelChatMessage, ViewModelChatDetail viewModelChatDetail)
        {
            _viewModelChatDetail = viewModelChatDetail;

            if (String.IsNullOrWhiteSpace(modelChatMessage.StringContent))
            {
                _isQuoted = true;
            }
            else
            {
                _isQuoted = false;
            }

            _isAttached = true;

            _arrayListTextBlock[0] = new List<TextBlock>();
            var i = _arrayListTextBlock.Count();

            ReturnGrid = CreateSingleQuotedMessage(modelChatMessage, 0);

            _isAttached = false;

            SetArrayListTextBlock();
        }

        /// <summary> Конструктор прикрепленных сообщений </summary>
        public QuotedMessage(List<ModelChatMessage> listModelChatMessage, ViewModelChatDetail viewModelChatDetail)
        {
            _viewModelChatDetail = viewModelChatDetail;

            _isQuoted = true;

            _isAttached = true;

            if (listModelChatMessage.Count() > 1)
            {
                var result = new Grid();

                var stackPanelTextCount = new StackPanel { Orientation = Orientation.Horizontal };

                var textBlockText = new TextBlock();
                textBlockText.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_CountQuotingMessage");
                var rectangleEmpty = new Rectangle { Width = 5, Opacity = 1 };
                var textBlockCount = new TextBlock { Text = listModelChatMessage.Count().ToString() };

                stackPanelTextCount.Children.Add(textBlockText);
                stackPanelTextCount.Children.Add(rectangleEmpty);
                stackPanelTextCount.Children.Add(textBlockCount);

                result.Children.Add(stackPanelTextCount);

                result.VerticalAlignment = VerticalAlignment.Center;

                ReturnGrid = result;
            }
            else
            {
                _arrayListTextBlock[0] = new List<TextBlock>();

                ReturnGrid = CreateSingleQuotedMessage(listModelChatMessage.First(), 0);
            }

            _isAttached = false;

            SetArrayListTextBlock();
        }

        /// <summary> Конструктор цитированых сообщений </summary>
        public QuotedMessage(ObservableCollection<ModelChatMessage> listModelChatMessage, ViewModelChatDetail viewModelChatDetail)
        {
            _viewModelChatDetail = viewModelChatDetail;

            _isQuoted = true;

            ReturnGrid = CreateListQuotedMessage(listModelChatMessage, 0);

            SetArrayListTextBlock();
        }

        /// <summary> Лист цитированых сообщений </summary>
        private Grid CreateListQuotedMessage(ObservableCollection<ModelChatMessage> listModelChatMessage, int depth)
        {
            var result = new Grid();

            var index = 0;

            _arrayListTextBlock[depth] = new List<TextBlock>();

            foreach (var i in listModelChatMessage)
            {
                var quotedMessage = CreateSingleQuotedMessage(i, depth);

                Grid.SetRow(quotedMessage, index);

                result.RowDefinitions.Add(new RowDefinition());

                result.Children.Add(quotedMessage);

                index++;
            }

            return result;
        }

        /// <summary> Одно цитированое сообщение </summary>
        private Grid CreateSingleQuotedMessage(ModelChatMessage modelChatMessage, int depth)
        {
            var columnRectangle = new ColumnDefinition { Width = GridLength.Auto };
            var columnContent = new ColumnDefinition { Width = GridLength.Auto };
            var columnButtonCancel = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };

            var returnGrid = new Grid
            {
                ColumnDefinitions = { columnRectangle, columnContent, columnButtonCancel }
            };

            var rectangle = new Rectangle { Width = 2, Fill = (Brush)(new BrushConverter().ConvertFrom("#b3b0b1")) };

            returnGrid.Children.Add(rectangle);

            var stackPanelContent = new StackPanel { Orientation = Orientation.Vertical };

            var textBlockContact = new TextBlock { Text = modelChatMessage.Sender.FullName };

            textBlockContact.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSizeLittle") });

            var stackPanelDateTime = new StackPanel { Orientation = Orientation.Horizontal };

            var textBlockDate = new TextBlock { Text = modelChatMessage.SendTime.GetDateTimeFormats('d')[0] };

            textBlockDate.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSizeLittle") });

            //спросить у Андрея
            var rectangleEmptyLeft = new Rectangle { Width = 3, Opacity = 1 };

            var rectangleEmptyRight = new Rectangle { Width = 3, Opacity = 1 };

            var textBlockAt = new TextBlock();
            textBlockAt.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_At");
            textBlockAt.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSizeLittle") });

            var textBlockTime = new TextBlock { Text = modelChatMessage.SendTime.GetDateTimeFormats('t')[0] };
            textBlockTime.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSizeLittle") });

            stackPanelDateTime.Children.Add(textBlockDate);
            stackPanelDateTime.Children.Add(rectangleEmptyLeft);
            stackPanelDateTime.Children.Add(textBlockAt);
            stackPanelDateTime.Children.Add(rectangleEmptyRight);
            stackPanelDateTime.Children.Add(textBlockTime);

            stackPanelContent.Children.Add(textBlockContact);
            stackPanelContent.Children.Add(stackPanelDateTime);

            if (modelChatMessage.HaveQuoted && _isQuoted && depth < 4)
            {
                var gridContent = CreateListQuotedMessage(modelChatMessage.ListQuotedModelChatMessage, depth + 1);
                stackPanelContent.Children.Add(gridContent);
            }

            if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 0 && !String.IsNullOrWhiteSpace(modelChatMessage.StringContent))
            {
                var TextBlockText = new TextBlock { Text = modelChatMessage.StringContent };

                TextBlockText.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSize") });

                if (_isAttached)
                {
                    TextBlockText.TextTrimming = TextTrimming.CharacterEllipsis;
                }
                else
                {
                    TextBlockText.TextWrapping = TextWrapping.Wrap;
                }

                _arrayListTextBlock[depth].Add(TextBlockText);

                stackPanelContent.Children.Add(TextBlockText);
            }

            if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 4)
            {
                var stackPanelContactDetails = new StackPanel { Orientation = Orientation.Horizontal };

                var textBlockContactDetails = new TextBlock { TextWrapping = TextWrapping.Wrap };
                textBlockContactDetails.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSize") });
                textBlockContactDetails.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_ContactDetails");

                var textBlockContactFullName = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = " " + modelChatMessage.ModelContactData?.FullName };
                textBlockContactFullName.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = _viewModelChatDetail, Path = new PropertyPath("FontSize") });

                stackPanelContactDetails.Children.Add(textBlockContactDetails);
                stackPanelContactDetails.Children.Add(textBlockContactFullName);

                stackPanelContent.Children.Add(stackPanelContactDetails);
            }

            stackPanelContent.Margin = new Thickness(2);
            Grid.SetColumn(stackPanelContent, 1);

            returnGrid.Children.Add(stackPanelContent);

            return returnGrid;
        }

        /// <summary> Заполнить лист TextBlock-ов </summary>
        private void SetArrayListTextBlock()
        {
            ArrayListTextBlock = new List<TextBlock>[] { };

            for (var i = 0; i < 5; i++)
            {
                if (_arrayListTextBlock[i] != null)
                {
                    Array.Resize(ref ArrayListTextBlock, i + 1);

                    ArrayListTextBlock[i] = _arrayListTextBlock[i];
                }
            }
        }
    }

    /// <summary> Контрол одного конкретного сообщения </summary>
    public class UnitedControlMessage : IDisposable
    {
        ///<summary> Объект для изменения времени зажатия </summary>
        private readonly DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) }; // интервал 1 сек

        /// <summary> Выбранно сообщение или нет </summary>
        private bool _isSelected;

        /// <summary> Редактировано сообщение или нет </summary>
        public bool Changed { get; set; }

        /// <summary> TextBlock-и цитат для ресайза </summary>
        public List<TextBlock>[] ArrayListTextBlockQuotedMessage;

        /// <summary> Ссылка на бабл </summary>
        public BubbleMessage BubbleMessageObj { get; set; }

        /// <summary> Сообщение </summary>
        public ModelChatMessage Message { get; set; }

        /// <summary> TextBlock с сообщением </summary>
        public TextBlock TextBlockObj { get; set; }

        /// <summary> Отправляемый контакт </summary>
        public Grid GridSendContact { get; set; }

        /// <summary> Задняя полоса сообщения </summary>
        public Grid GridBackground { get; set; }

        /// <summary> Карандашик отредактированно сообщение или нет </summary>
        public Image ImageEditMessage { get; set; }

        /// <summary> CheckBox выбора сообщения </summary>
        public CheckBox СheckBoxObj { get; set; }

        /// <summary> Список цитируемых сообщений </summary>
        public Grid ListGridQuotedMessages { get; set; }

        /// <summary> Список цитируемых сообщений </summary>
        public List<QuotedMessage> ListQuotedMessages { get; set; }

        /// <summary> Объект ViewModelChatDetail </summary>
        public ViewModelChatDetail ViewModelChatDetailObj { get; set; }

        //переделать на binding
        /// <summary> Выбранно сообщение или нет </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (Message.ModelEnumChatMessageTypeObj.Code == 5)
                {
                    _isSelected = false;
                }
                else
                {
                    if (value != IsSelected)
                    {
                        if (value)
                        {
                            GridBackground.Background = (Brush)(new BrushConverter().ConvertFrom("#f2f2f2"));
                        }
                        else
                        {
                            GridBackground.Background = Brushes.White;
                        }

                        СheckBoxObj.IsChecked = value;

                        _isSelected = value;

                        OnEventCheckedChanged();
                    }
                }
            }
        }

        /// <summary> Событие изменения выбора сообщения </summary>
        public event EventHandler EventCheckedChanged;

        /// <summary> Событие долгого нажатия </summary>
        public event EventHandler LongLeftButtonTap;

        /// <summary> Событие короткого нажатия </summary>
        public event EventHandler ShortLeftButtonTap;

        /// <summary> Событие нажатия на текст правой кнопкой </summary>
        public event EventHandler RightButtonTap;

        /// <summary> Конструктор </summary>
        public UnitedControlMessage(ModelChatMessage modelChatMessage, ViewModelChatDetail viewModelChatDetail)
        {
            ViewModelChatDetailObj = viewModelChatDetail;

            Message = modelChatMessage;

            if (Message.ModelEnumChatMessageTypeObj.Code == 4)
            {
                GridSendContact = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(18) }, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) } }
                };
                var gridMessageContactAvatar = new Grid { VerticalAlignment = VerticalAlignment.Top, Height = 18, Width = 18 };
                gridMessageContactAvatar.Children.Add(new Image { Source = modelChatMessage.ModelContactData?.Avatar });
                gridMessageContactAvatar.Children.Add(
                    new Image
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Height = 7,
                        Width = 7,
                        Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/IconTray_small.png")
                    });
                Grid.SetColumn(gridMessageContactAvatar, 0);
                var textBlock = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = modelChatMessage.ModelContactData?.FullName };
                textBlock.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = viewModelChatDetail, Path = new PropertyPath("FontSizeLittle") });
                Grid.SetColumn(textBlock, 1);
                GridSendContact.Children.Add(gridMessageContactAvatar);
                GridSendContact.Children.Add(textBlock);
                GridSendContact.DataContext = modelChatMessage.ModelContactData;

                GridSendContact.MouseLeftButtonUp += _textBlockObj_MouseLeftButtonUp;
                GridSendContact.MouseLeftButtonDown += _textBlockObj_MouseLeftButtonDown;
                GridSendContact.MouseRightButtonDown += _textBlockObj_MouseRightButtonDown;
            }
            else
            {
                TextBlockObj = new TextBlock { TextWrapping = TextWrapping.Wrap };
                TextBlockObj.MouseLeftButtonUp += _textBlockObj_MouseLeftButtonUp;
                TextBlockObj.MouseLeftButtonDown += _textBlockObj_MouseLeftButtonDown;
                if (modelChatMessage.ModelEnumChatMessageTypeObj.Code != 5)
                    TextBlockObj.MouseRightButtonDown += _textBlockObj_MouseRightButtonDown;

                if (modelChatMessage.Encrypted)
                {
                    TextBlockObj.SetResourceReference(TextBlock.TextProperty, @"ViewChatDetail_EncryptedMessage");
                }
                else
                {
                    PlaceToTextBlockFromString(Message.StringContent);
                }

                TextBlockObj.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = ViewModelChatDetailObj, Path = new PropertyPath("FontSize") });
            }

            GridBackground = new Grid { Background = Brushes.White };
            GridBackground.MouseLeftButtonDown += _gridBackground_MouseLeftButtonDown;
            GridBackground.MouseLeftButtonUp += _gridBackground_MouseLeftButtonUp;
            GridBackground.MouseRightButtonDown += GridBackground_MouseRightButtonDown;

            СheckBoxObj = new CheckBox();
            if (Message.ModelEnumChatMessageTypeObj.Code == 5)
            {
                СheckBoxObj.IsEnabled = false;
            }
            else
            {
                СheckBoxObj.Click += _checkBoxObj_Click;
            }

            СheckBoxObj.Visibility = Visibility.Collapsed;

            ImageEditMessage = new Image
            {
                Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/windows_edited.png"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 16,
                Height = 16,
                Visibility = Visibility.Collapsed
            };

            if (Message.Changed)
                ImageEditMessage.Visibility = Visibility.Visible;


            if (modelChatMessage.ModelEnumChatMessageTypeObj.Code == 5)
            {
                TextBlockObj.SetBinding(TextBlock.TextProperty, new Binding { Source = modelChatMessage, Path = new PropertyPath("StringContentText") });
                TextBlockObj.FontStyle = FontStyles.Italic;

                ImageEditMessage.Visibility = Visibility.Collapsed;
            }

            if (modelChatMessage.HaveQuoted)
            {
                var quotedMessage = new QuotedMessage(Message.ListQuotedModelChatMessage, ViewModelChatDetailObj);

                ListGridQuotedMessages = quotedMessage.ReturnGrid;

                ArrayListTextBlockQuotedMessage = quotedMessage.ArrayListTextBlock;

                ListGridQuotedMessages.MouseLeftButtonDown += ListGridQuotedMessages_MouseLeftButtonDown;
                ListGridQuotedMessages.MouseLeftButtonUp += ListGridQuotedMessages_MouseLeftButtonUp;
                ListGridQuotedMessages.MouseRightButtonDown += ListGridQuotedMessages_MouseRightButtonDown;
            }

            _isSelected = false;
        }

        /// <summary> Обработка нажатия правой кнопкой по GridQuotedMessages </summary>
        private void ListGridQuotedMessages_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnRightButtonTap();
        }

        /// <summary> Обработка нажатия правой кнопкой по Background-у </summary>
        private void GridBackground_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnRightButtonTap();
        }

        /// <summary> Обработка отжатия от GridQuotedMessages </summary>
        private void ListGridQuotedMessages_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RecognizeLongOrShort(false);
        }

        //Спросить у Андрея нужно делать один обработчик для длинных и коротких сообщений или для каждого контрола свой
        /// <summary> Обработка нажатия по GridQuotedMessages </summary>
        private void ListGridQuotedMessages_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _timer.Start();

            _timer.Tick += _timer_OnElapsed;
        }

        /// <summary> Обработка нажатия правой кнопкой по TextBlock-у </summary>
        private void _textBlockObj_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnRightButtonTap();
        }

        /// <summary> Прикрепить TextBlock-у текст </summary>
        public void PlaceToTextBlockFromString(string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return;

            TextBlockObj.Inlines.Clear();

            var https = @"https://";
            var http = @"http://";
            var www = @"www.";

            if (!(text.Contains(https) || text.Contains(http) || text.Contains(www)))
                TextBlockObj.Text = text;
            else
            {
                char[] arr = new char[] { ' ', ',', '(', ')', ':' };
                string textAll = text;

                while (textAll != "")
                {
                    bool flagHttpOrHttps = true;

                    int indexBeginHyperlink = textAll.Count();
                    if (textAll.ToLower().IndexOf(https) != -1 && textAll.ToLower().IndexOf(https) < indexBeginHyperlink)
                        indexBeginHyperlink = textAll.ToLower().IndexOf(https);

                    if (textAll.ToLower().IndexOf(http) != -1 && textAll.ToLower().IndexOf(http) < indexBeginHyperlink)
                        indexBeginHyperlink = textAll.ToLower().IndexOf(http);

                    if (textAll.ToLower().IndexOf(www) != -1 && textAll.ToLower().IndexOf(www) < indexBeginHyperlink)
                    {
                        indexBeginHyperlink = textAll.ToLower().IndexOf(www);
                        flagHttpOrHttps = false;
                    }

                    if (indexBeginHyperlink == textAll.Count())
                    {
                        TextBlockObj.Inlines.Add(textAll);
                        break;//нет гиперссылки
                    }
                    else
                    {
                        int searchIndexEndHyperlink = indexBeginHyperlink + ((flagHttpOrHttps) ? 7 : 4);

                        int indexEndHyperlink = textAll.Count();

                        if (textAll.IndexOfAny(arr, searchIndexEndHyperlink) != -1 && textAll.IndexOfAny(arr, searchIndexEndHyperlink) < indexEndHyperlink)
                            indexEndHyperlink = textAll.IndexOfAny(arr, searchIndexEndHyperlink);

                        if (textAll.IndexOf("\n", searchIndexEndHyperlink) != -1 && textAll.IndexOf("\n", searchIndexEndHyperlink) < indexEndHyperlink)
                            indexEndHyperlink = textAll.IndexOf("\n", searchIndexEndHyperlink);

                        var textBeforeHyperlink = textAll.Substring(0, indexBeginHyperlink);
                        var textHyperlink = textAll.Substring(indexBeginHyperlink, indexEndHyperlink - indexBeginHyperlink);
                        var textAfterHyperlink = textAll.Substring(indexEndHyperlink);

                        var HyperlinkInTextBlock = new Hyperlink();
                        HyperlinkInTextBlock.Inlines.Add(textHyperlink);
                        HyperlinkInTextBlock.DataContext = textHyperlink;
                        HyperlinkInTextBlock.Click += HyperlinkInTextBlock_Click;

                        TextBlockObj.Inlines.Add(textBeforeHyperlink);
                        TextBlockObj.Inlines.Add(HyperlinkInTextBlock);

                        textAll = textAfterHyperlink;
                    }
                }
            }
        }

        //!!! добавил обработчик клика в нутрь контрола типо инкапсуляция нужно проверить
        /// <summary> Обработчик нажатия на гиперссылку </summary>
        private void HyperlinkInTextBlock_Click(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            var uri = hyperlink.DataContext as string;
            UtilityWeb.OpenUrl(uri);
        }

        /// <summary> Обработка нажатия по TextBlock-у </summary>
        private void _textBlockObj_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _timer.Start();

            _timer.Tick += _timer_OnElapsed;
        }

        /// <summary> Обработка отжатия от TextBlock-у </summary>
        private void _textBlockObj_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Message.ModelEnumChatMessageTypeObj.Code == 4)
            {
                if (!_timer.IsEnabled) return;

                _timer.Stop();

                _timer.Tick -= _timer_OnElapsed;

                OnMessageContactMouseDown(sender, null);
            }
            else
            {
                RecognizeLongOrShort(false);
            }
        }

        //зачем здесь e
        /// <summary> Обработчик нажатия на прикрепленный к сообщению контакт </summary>
        private void OnMessageContactMouseDown(object sender, RoutedEventArgs e)
        {
            var modelContact = (sender as Grid).DataContext as ModelContact;

            if (modelContact == null) return;

            if (modelContact.IsFriend)
            {
                WindowMain.ShowInRightWorkspace(new ViewContactDetail(modelContact));
            }
            else
            {
                WindowMain.ShowInRightWorkspace(new ViewContactDetail(modelContact));
            }
        }

        /// <summary> Обработка отжатия от Background-у </summary>
        private void _gridBackground_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RecognizeLongOrShort(false);
        }

        /// <summary> Определитель долгого или короткого клика </summary>
        private void RecognizeLongOrShort(bool IsLong)
        {
            if (!_timer.IsEnabled) return;

            _timer.Stop();

            _timer.Tick -= _timer_OnElapsed;

            if (IsLong)
            {
                OnLongLeftButtonTap();
            }
            else
            {
                OnShortLeftButtonTap();
            }
        }

        /// <summary> Обработка нажатия по Background-у </summary>
        private void _gridBackground_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _timer.Start();

            _timer.Tick += _timer_OnElapsed;
        }

        /// <summary> Обработчик таймера </summary>
        private void _timer_OnElapsed(object sender, EventArgs e)
        {
            RecognizeLongOrShort(true);
        }

        /// <summary> Инвокатор события долгого нажатия </summary>
        private void OnLongLeftButtonTap()
        {
            LongLeftButtonTap.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Инвокатор события короткого нажатия </summary>
        private void OnShortLeftButtonTap()
        {
            ShortLeftButtonTap.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Обработка клика по СheckBox </summary>
        private void _checkBoxObj_Click(object sender, RoutedEventArgs e)
        {
            CheckedChanged();
        }

        /// <summary> Инвокатор нажатия на текст правой кнопкой </summary>
        private void OnRightButtonTap()
        {
            RightButtonTap.Invoke(this, EventArgs.Empty);
        }

        /// <summary> Инвокатор изменения выбора сообщения </summary>
        private void OnEventCheckedChanged()
        {
            EventCheckedChanged.Invoke(this, EventArgs.Empty);
        }

        //поменять на CheckedChange
        /// <summary> Изменение выбора </summary>
        public void CheckedChanged()
        {
            IsSelected = !IsSelected;
        }

        /// <summary> Удалить сообщение </summary>
        public void Delete()
        {
            if (Message.ModelEnumChatMessageTypeObj.Code == 4)
            {
                GridSendContact.MouseRightButtonDown -= _textBlockObj_MouseRightButtonDown;
            }
            else
            {
                TextBlockObj.MouseRightButtonDown -= _textBlockObj_MouseRightButtonDown;
            }
        }

        /// <summary> Обновить контролл </summary>
        public void Refresh()
        {
            if (Message.ModelEnumChatMessageTypeObj.Code == 5)
            {
                TextBlockObj = new TextBlock { TextWrapping = TextWrapping.Wrap };
                TextBlockObj.SetBinding(TextBlock.FontSizeProperty, new Binding { Source = ViewModelChatDetailObj, Path = new PropertyPath("FontSize") });
                TextBlockObj.SetBinding(TextBlock.TextProperty, new Binding { Source = Message, Path = new PropertyPath("StringContentText") });
                TextBlockObj.FontStyle = FontStyles.Italic;

                ImageEditMessage.Visibility = Visibility.Hidden;
            }

            if (Message.ModelEnumChatMessageTypeObj.Code == 0)
            {
                PlaceToTextBlockFromString(Message.StringContent);

                if (Message.Changed)
                    ImageEditMessage.Visibility = Visibility.Visible;
            }
        }
        //может лучше не различать GridSendContact и TextBlockObj а сделать просто UIElement?
        /// <summary> Освобождение ресурсов </summary>
        public void Dispose()
        {
            if (Message.ModelEnumChatMessageTypeObj.Code == 4)
            {
                GridSendContact.MouseLeftButtonUp -= _textBlockObj_MouseLeftButtonUp;
                GridSendContact.MouseLeftButtonDown -= _textBlockObj_MouseLeftButtonDown;
                GridSendContact.MouseRightButtonDown -= _textBlockObj_MouseRightButtonDown;
            }
            else
            {
                TextBlockObj.MouseLeftButtonUp -= _textBlockObj_MouseLeftButtonUp;
                TextBlockObj.MouseLeftButtonDown -= _textBlockObj_MouseLeftButtonDown;
                TextBlockObj.MouseRightButtonDown -= _textBlockObj_MouseRightButtonDown;

                if (Message.HaveQuoted)
                {
                    ListGridQuotedMessages.MouseLeftButtonDown -= ListGridQuotedMessages_MouseLeftButtonDown;
                    ListGridQuotedMessages.MouseLeftButtonUp -= ListGridQuotedMessages_MouseLeftButtonUp;
                }
            }

            GridBackground.MouseLeftButtonDown -= _gridBackground_MouseLeftButtonDown;
            GridBackground.MouseLeftButtonUp -= _gridBackground_MouseLeftButtonUp;
        }
    }

    /// <summary> Бабл с сообщениями </summary>
    public class BubbleMessage
    {
        /// <summary> Возвращаемый Grid </summary>
        private Grid _retuntGrid;

        /// <summary> Список контролов сообщени </summary>
        private List<UnitedControlMessage> _listUnitedControlMessageInBubbleMessage = new List<UnitedControlMessage>();

        /// <summary> Апендикс </summary>
        private Image _apendix;

        /// <summary> Аватат </summary>
        private Grid _gridAvatar;

        /// <summary> Отправитель </summary>
        private ModelContact _sender;

        /// <summary> Дата отправления </summary>
        private DateTime _sendTime;

        /// <summary> Статус контакта </summary>
        private StackPanel _stackPanelStatus;

        /// <summary> Фон бабла </summary>
        private Rectangle _rectangle = new Rectangle { RadiusX = 10, RadiusY = 10 };

        /// <summary> Цвет бабла моих сообщений </summary>
        private Brush _colorMyMessage = (Brush)(new BrushConverter().ConvertFrom("#bdb6b8"));

        /// <summary> Цвет бабла чужих сообщений </summary>
        private Brush _colorOtherMessage = (Brush)(new BrushConverter().ConvertFrom("#e8e4e4"));

        //в ddcl нужно будет определять по сообщению
        /// <summary> Время сообщения </summary>
        private TextBlock _textBlockTime;

        /// <summary> Нижняя полоса бабла </summary>
        private StackPanel _botomRow;

        /// <summary> Время сообщения </summary>
        private Image _imageCrypted;

        /// <summary> P2P чат или нет </summary>
        private bool _isP2P;

        /// <summary> Отступ для всего бабла </summary>
        private Thickness _marginBubble;

        /// <summary> Можно ли прикреплять новые сообщения к этому баблу </summary>
        public bool CanAttachedMessage;

        /// <summary> Отправитель </summary>
        public ModelContact Sender { get { return _sender; } }

        /// <summary> Признак доставки сообщений в бабле на сервер </summary>
        public bool _servered;

        /// <summary> Признак доставки сообщений в бабле на сервер </summary>
        public bool Servered
        {
            get { return _servered; }

            set
            {
                _servered = value;

                if (IsShowStatusServered)
                {
                    if (value)
                    {
                        IconCheckDeliveredToServer.Visibility = Visibility.Visible;
                        IconCheckSend.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        IconCheckSend.Visibility = Visibility.Visible;
                        IconCheckDeliveredToServer.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary> Показывать статус доставки </summary>
        private bool _isShowStatusServered;

        /// <summary> Показывать статус доставки </summary>
        public bool IsShowStatusServered
        {
            get { return _isShowStatusServered; }

            set
            {
                if (value)
                {
                    if (Servered) IconCheckDeliveredToServer.Visibility = Visibility.Visible;
                    else IconCheckSend.Visibility = Visibility.Visible;
                }
                else
                {
                    IconCheckDeliveredToServer.Visibility = Visibility.Collapsed;
                    IconCheckSend.Visibility = Visibility.Collapsed;
                }

                _isShowStatusServered = value;
            }
        }

        /// <summary> Отступ для всего бабла </summary>
        private Thickness MarginBubble
        {
            get
            {
                return _marginBubble;
            }

            set
            {
                _marginBubble = value;
            }
        }

        /// <summary> Изображение галочек сообщение отправленно </summary>
        public StackPanel IconCheckSend;

        /// <summary> Изображение галочек сообщение доставленно до сервера </summary>
        public StackPanel IconCheckDeliveredToServer;

        /// <summary> Дата отправления </summary>
        public DateTime SendTime
        {
            get
            {
                return _sendTime;
            }
        }

        /// <summary> Возвращаемый Grid </summary>
        public Grid RetuntGrid
        {
            get
            {
                return _retuntGrid;
            }
        }

        /// <summary> Список контролов сообщени </summary>
        public List<UnitedControlMessage> ListUnitedControlMessageInBubbleMessage
        {
            get
            {
                return _listUnitedControlMessageInBubbleMessage;
            }
        }

        /// <summary> Конструктор </summary>
        public BubbleMessage(UnitedControlMessage unitedControlMessage)
        {
            _isP2P = unitedControlMessage.ViewModelChatDetailObj.CurrentModelChat.IsP2P;

            _sender = unitedControlMessage.Message?.Sender;

            _sendTime = unitedControlMessage.Message.SendTime;

            unitedControlMessage.BubbleMessageObj = this;
            _listUnitedControlMessageInBubbleMessage.Add(unitedControlMessage);

            CanAttachedMessage = true;

            Servered = unitedControlMessage.Message.Servered;

            if (_sender.Iam)
            {
                var columnForCheckBox = new ColumnDefinition { Width = GridLength.Auto };
                var columnResidue = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                var columnMessage = new ColumnDefinition { Width = GridLength.Auto };
                var columnAvatar = new ColumnDefinition { Width = new GridLength(36) };

                var rowTop = new RowDefinition { Height = GridLength.Auto };
                var rowCentr = new RowDefinition { Height = GridLength.Auto };
                var rowBottom = new RowDefinition { Height = GridLength.Auto };

                _retuntGrid = new Grid
                {
                    ColumnDefinitions = { columnForCheckBox, columnResidue, columnMessage, columnAvatar },
                    RowDefinitions = { rowTop, rowCentr, rowBottom },
                    Margin = new Thickness(0, 0, 0, 10)
                };

                if (unitedControlMessage.ViewModelChatDetailObj.CurrentModelChat.Secured &&
                    unitedControlMessage.Message.ModelEnumChatMessageSecurityLevelObj.Code == 0)
                {
                    _imageCrypted = new Image
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/no_encryption.png"),
                        Height = 24,
                        Width = 24,
                        Margin = new Thickness(-12, 0, 0, 0)
                    };

                    rowTop.Height = new GridLength(10);

                    Grid.SetColumn(_imageCrypted, 2);
                    Grid.SetRow(_imageCrypted, 0);
                    Grid.SetRowSpan(_imageCrypted, 2);
                    _retuntGrid.Children.Add(_imageCrypted);
                }

                Grid.SetColumnSpan(unitedControlMessage.GridBackground, 4);
                Grid.SetRowSpan(unitedControlMessage.GridBackground, 2);
                Panel.SetZIndex(unitedControlMessage.GridBackground, -2);
                _retuntGrid.Children.Add(unitedControlMessage.GridBackground);

                unitedControlMessage.СheckBoxObj.Margin = new Thickness(0, 3, 0, 3);
                unitedControlMessage.СheckBoxObj.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(unitedControlMessage.СheckBoxObj, 0);
                Grid.SetRow(unitedControlMessage.СheckBoxObj, 1);
                _retuntGrid.Children.Add(unitedControlMessage.СheckBoxObj);

                _gridAvatar = new Grid { VerticalAlignment = VerticalAlignment.Top, Height = 36, Width = 36 };
                _gridAvatar.Children.Add(new Image { Source = _sender?.Avatar });
                _gridAvatar.Children.Add(new Image
                {
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Height = 15,
                    Width = 15,
                    Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/IconTray_small.png")
                });

                Grid.SetColumn(_gridAvatar, 3);
                Grid.SetRow(_gridAvatar, 1);
                Grid.SetRowSpan(_gridAvatar, 2);
                _retuntGrid.Children.Add(_gridAvatar);

                MarginBubble = new Thickness(0, 0, 7, 0);

                _apendix = new Image
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Width = 16,
                    Height = 16
                };
                Panel.SetZIndex(_apendix, -1);

                _apendix.Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/apendix_right.png");
                Grid.SetColumn(_apendix, 2);
                Grid.SetRow(_apendix, 1);
                _retuntGrid.Children.Add(_apendix);

                _textBlockTime = new TextBlock
                {
                    Text = _sendTime.ToShortTimeString(),
                    Foreground = Brushes.Gray,
                    FontSize = 12
                };

                _botomRow = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(_marginBubble.Left, _marginBubble.Top, _marginBubble.Right + 4, _marginBubble.Bottom + 3)
                };

                IconCheckSend = StackPanelCheckServered(1, @"ViewChatDetail_Send");
                IconCheckDeliveredToServer = StackPanelCheckServered(2, @"ViewChatDetail_DeliveredToServer");

                _botomRow.Children.Add(IconCheckSend);
                _botomRow.Children.Add(IconCheckDeliveredToServer);
                _botomRow.Children.Add(_textBlockTime);

                //Grid.SetColumn(_botomRow, 2);
                Grid.SetColumn(_botomRow, 0);
                Grid.SetColumnSpan(_botomRow, 3);
                Grid.SetRow(_botomRow, 2);
                _retuntGrid.Children.Add(_botomRow);

                unitedControlMessage.ImageEditMessage.HorizontalAlignment = HorizontalAlignment.Right;
                unitedControlMessage.ImageEditMessage.VerticalAlignment = VerticalAlignment.Center;
                unitedControlMessage.ImageEditMessage.Margin = new Thickness(0, 0, 5, 0);
                Grid.SetColumn(unitedControlMessage.ImageEditMessage, 1);
                Grid.SetRow(unitedControlMessage.ImageEditMessage, 1);
                _retuntGrid.Children.Add(unitedControlMessage.ImageEditMessage);

                if (unitedControlMessage.Message.ModelEnumChatMessageTypeObj.Code == 5)
                    CanAttachedMessage = false;

                if (unitedControlMessage.Message.ModelEnumChatMessageTypeObj.Code == 4)
                {
                    Grid.SetColumn(unitedControlMessage.GridSendContact, 2);
                    Grid.SetRow(unitedControlMessage.GridSendContact, 1);
                    unitedControlMessage.GridSendContact.Margin = new Thickness(MarginBubble.Left + 10, MarginBubble.Top + 3, MarginBubble.Right + 7, MarginBubble.Bottom + 5);
                    _retuntGrid.Children.Add(unitedControlMessage.GridSendContact);

                    CanAttachedMessage = false;
                }
                else
                {
                    Grid.SetColumn(unitedControlMessage.TextBlockObj, 2);
                    Grid.SetRow(unitedControlMessage.TextBlockObj, 1);
                    unitedControlMessage.TextBlockObj.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                    _retuntGrid.Children.Add(unitedControlMessage.TextBlockObj);
                }

                if (unitedControlMessage.Message.HaveQuoted)
                {
                    _apendix.Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/apendix_gray_right.png");

                    if (String.IsNullOrWhiteSpace(unitedControlMessage.Message.StringContent))
                    {
                        var onlyTopQuotedMessages = OnlyTopQuotedMessages();

                        Panel.SetZIndex(onlyTopQuotedMessages, -1);
                        Grid.SetColumn(onlyTopQuotedMessages, 2);
                        Grid.SetRow(onlyTopQuotedMessages, 1);

                        onlyTopQuotedMessages.Margin = new Thickness(MarginBubble.Left, MarginBubble.Top, MarginBubble.Right, MarginBubble.Bottom + 1);

                        _retuntGrid.Children.Add(onlyTopQuotedMessages);

                        var gridListQuotedMessages = unitedControlMessage.ListGridQuotedMessages;
                        Grid.SetColumn(gridListQuotedMessages, 2);
                        Grid.SetRow(gridListQuotedMessages, 1);
                        gridListQuotedMessages.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                        _retuntGrid.Children.Add(gridListQuotedMessages);

                        Grid.SetRow(_botomRow, 2);
                    }
                    else
                    {
                        _retuntGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        Grid.SetRow(unitedControlMessage.ImageEditMessage, 2);
                        Grid.SetRow(_botomRow, 3);
                        Grid.SetRow(unitedControlMessage.СheckBoxObj, 2);
                        Grid.SetRowSpan(unitedControlMessage.GridBackground, 3);
                        Grid.SetRow(unitedControlMessage.TextBlockObj, 2);

                        var topQuotedMessages = TopQuotedMessages();

                        Panel.SetZIndex(topQuotedMessages, -1);
                        Grid.SetColumn(topQuotedMessages, 2);
                        Grid.SetRow(topQuotedMessages, 1);

                        topQuotedMessages.Margin = new Thickness(MarginBubble.Left, MarginBubble.Top, MarginBubble.Right, MarginBubble.Bottom + 1);
                        _retuntGrid.Children.Add(topQuotedMessages);

                        var gridListQuotedMessages = unitedControlMessage.ListGridQuotedMessages;
                        Grid.SetColumn(gridListQuotedMessages, 2);
                        Grid.SetRow(gridListQuotedMessages, 1);
                        gridListQuotedMessages.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                        _retuntGrid.Children.Add(gridListQuotedMessages);

                        var bottomQuotedMessages = BottomQuotedMessages(true);

                        Panel.SetZIndex(bottomQuotedMessages, -1);
                        Grid.SetColumn(bottomQuotedMessages, 2);
                        Grid.SetRow(bottomQuotedMessages, 2);

                        bottomQuotedMessages.Margin = new Thickness(MarginBubble.Left, MarginBubble.Top, MarginBubble.Right, MarginBubble.Bottom);
                        _retuntGrid.Children.Add(bottomQuotedMessages);
                    }

                    CanAttachedMessage = false;

                }
                else
                {
                    _rectangle = new Rectangle { RadiusX = 10, RadiusY = 10 };
                    _rectangle.Margin = MarginBubble;
                    _rectangle.Fill = _colorMyMessage;
                    Grid.SetColumn(_rectangle, 2);
                    Grid.SetRow(_rectangle, 1);
                    Grid.SetRowSpan(_rectangle, 1);
                    Panel.SetZIndex(_rectangle, -1);
                    _retuntGrid.Children.Add(_rectangle);
                }
            }
            else
            {
                var columnForCheckBox = new ColumnDefinition { Width = GridLength.Auto };
                var columnAvatar = new ColumnDefinition { Width = new GridLength(36) };
                var columnMessage = new ColumnDefinition { Width = GridLength.Auto };
                var columnResidue = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };

                var rowTop = new RowDefinition { Height = GridLength.Auto };
                var rowCentr = new RowDefinition { Height = GridLength.Auto };
                var rowBottom = new RowDefinition { Height = GridLength.Auto };

                _retuntGrid = new Grid
                {
                    ColumnDefinitions = { columnForCheckBox, columnAvatar, columnMessage, columnResidue },
                    RowDefinitions = { rowTop, rowCentr, rowBottom },
                    Margin = new Thickness(0, 0, 0, 10)
                };

                if (unitedControlMessage.ViewModelChatDetailObj.CurrentModelChat.Secured &&
                    unitedControlMessage.Message.ModelEnumChatMessageSecurityLevelObj.Code == 0)
                {
                    _imageCrypted = new Image
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/no_encryption.png"),
                        Height = 24,
                        Width = 24,
                        Margin = new Thickness(0, 0, -12, 0)
                    };

                    rowTop.Height = new GridLength(10);

                    Grid.SetColumn(_imageCrypted, 2);
                    Grid.SetRow(_imageCrypted, 0);
                    Grid.SetRowSpan(_imageCrypted, 2);
                    _retuntGrid.Children.Add(_imageCrypted);
                }

                Grid.SetColumnSpan(unitedControlMessage.GridBackground, 4);
                Grid.SetRowSpan(unitedControlMessage.GridBackground, 2);
                Panel.SetZIndex(unitedControlMessage.GridBackground, -2);
                _retuntGrid.Children.Add(unitedControlMessage.GridBackground);

                unitedControlMessage.СheckBoxObj.Margin = new Thickness(0, 3, 0, 3);
                unitedControlMessage.СheckBoxObj.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(unitedControlMessage.СheckBoxObj, 0);
                Grid.SetRow(unitedControlMessage.СheckBoxObj, 1);
                _retuntGrid.Children.Add(unitedControlMessage.СheckBoxObj);

                _gridAvatar = new Grid { VerticalAlignment = VerticalAlignment.Top, Height = 36, Width = 36 };
                _gridAvatar.Children.Add(new Image { Source = _sender?.Avatar });
                _gridAvatar.Children.Add(new Image
                {
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Height = 15,
                    Width = 15,
                    Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/IconTray_small.png")
                });

                Grid.SetColumn(_gridAvatar, 1);
                Grid.SetRow(_gridAvatar, 1);
                Grid.SetRowSpan(_gridAvatar, 2);
                _retuntGrid.Children.Add(_gridAvatar);

                MarginBubble = new Thickness(7, 0, 0, 0);

                _apendix = new Image
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 16,
                    Height = 16
                };
                Panel.SetZIndex(_apendix, -1);

                _apendix.Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/apendix_left.png");
                Grid.SetColumn(_apendix, 2);
                Grid.SetRow(_apendix, 1);
                _retuntGrid.Children.Add(_apendix);

                _textBlockTime = new TextBlock
                {
                    Text = _sendTime.ToShortTimeString(),
                    Foreground = Brushes.Gray,
                    FontSize = 12
                };

                _botomRow = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(_marginBubble.Left, _marginBubble.Top, _marginBubble.Right + 4, _marginBubble.Bottom + 3)
                };

                _botomRow.Children.Add(_textBlockTime);

                Grid.SetColumn(_botomRow, 2);
                Grid.SetRow(_botomRow, 2);
                _retuntGrid.Children.Add(_botomRow);

                unitedControlMessage.ImageEditMessage.HorizontalAlignment = HorizontalAlignment.Left;
                unitedControlMessage.ImageEditMessage.VerticalAlignment = VerticalAlignment.Center;
                unitedControlMessage.ImageEditMessage.Margin = new Thickness(5, 0, 0, 0);
                Grid.SetColumn(unitedControlMessage.ImageEditMessage, 3);
                Grid.SetRow(unitedControlMessage.ImageEditMessage, 1);
                _retuntGrid.Children.Add(unitedControlMessage.ImageEditMessage);

                if (!_isP2P)
                {
                    _stackPanelStatus = new StackPanel { Orientation = Orientation.Horizontal };

                    var ellipseStatus = new Ellipse { Margin = new Thickness(0, 5, 5, 5), Height = 6, Width = 6, Fill = _sender.ModelEnumUserBaseStatusObj?.Color };
                    ellipseStatus.SetBinding(Ellipse.FillProperty, new Binding { Source = _sender, Path = new PropertyPath("ModelEnumUserBaseStatusObj.Color") });

                    ellipseStatus.SetBinding(Ellipse.VisibilityProperty, new Binding { Converter = new ConverterBoolToVisibilityCollapsed(), Source = _sender, Path = new PropertyPath("IsAccessStatus") });

                    _stackPanelStatus.Children.Add(ellipseStatus);
                    _stackPanelStatus.Children.Add(new TextBlock { Text = _sender.FullName });
                    Grid.SetColumn(_stackPanelStatus, 2);
                    Grid.SetRow(_stackPanelStatus, 0);
                    _retuntGrid.Children.Add(_stackPanelStatus);
                }

                if (unitedControlMessage.Message.ModelEnumChatMessageTypeObj.Code == 5)
                    CanAttachedMessage = false;

                if (unitedControlMessage.Message.ModelEnumChatMessageTypeObj.Code == 4)
                {
                    Grid.SetColumn(unitedControlMessage.GridSendContact, 2);
                    Grid.SetRow(unitedControlMessage.GridSendContact, 1);
                    unitedControlMessage.GridSendContact.Margin = new Thickness(MarginBubble.Left + 10, MarginBubble.Top + 3, MarginBubble.Right + 7, MarginBubble.Bottom + 5);
                    _retuntGrid.Children.Add(unitedControlMessage.GridSendContact);

                    CanAttachedMessage = false;
                }
                else
                {
                    Grid.SetColumn(unitedControlMessage.TextBlockObj, 2);
                    Grid.SetRow(unitedControlMessage.TextBlockObj, 1);
                    unitedControlMessage.TextBlockObj.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                    _retuntGrid.Children.Add(unitedControlMessage.TextBlockObj);
                }

                if (unitedControlMessage.Message.HaveQuoted)
                {
                    if (String.IsNullOrWhiteSpace(unitedControlMessage.Message.StringContent))
                    {
                        var onlyTopQuotedMessages = OnlyTopQuotedMessages();

                        Panel.SetZIndex(onlyTopQuotedMessages, -1);
                        Grid.SetColumn(onlyTopQuotedMessages, 2);
                        Grid.SetRow(onlyTopQuotedMessages, 1);

                        onlyTopQuotedMessages.Margin = new Thickness(MarginBubble.Left, MarginBubble.Top, MarginBubble.Right, MarginBubble.Bottom + 1);

                        _retuntGrid.Children.Add(onlyTopQuotedMessages);

                        var gridListQuotedMessages = unitedControlMessage.ListGridQuotedMessages;
                        Grid.SetColumn(gridListQuotedMessages, 2);
                        Grid.SetRow(gridListQuotedMessages, 1);
                        gridListQuotedMessages.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                        _retuntGrid.Children.Add(gridListQuotedMessages);

                        Grid.SetRow(_botomRow, 2);
                    }
                    else
                    {
                        _retuntGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        Grid.SetRow(unitedControlMessage.ImageEditMessage, 2);
                        Grid.SetRow(_botomRow, 3);
                        Grid.SetRow(unitedControlMessage.СheckBoxObj, 2);
                        Grid.SetRowSpan(unitedControlMessage.GridBackground, 3);
                        Grid.SetRow(unitedControlMessage.TextBlockObj, 2);

                        var topQuotedMessages = TopQuotedMessages();

                        Panel.SetZIndex(topQuotedMessages, -1);
                        Grid.SetColumn(topQuotedMessages, 2);
                        Grid.SetRow(topQuotedMessages, 1);

                        topQuotedMessages.Margin = new Thickness(MarginBubble.Left, MarginBubble.Top, MarginBubble.Right, MarginBubble.Bottom + 1);
                        _retuntGrid.Children.Add(topQuotedMessages);

                        var gridListQuotedMessages = unitedControlMessage.ListGridQuotedMessages;
                        Grid.SetColumn(gridListQuotedMessages, 2);
                        Grid.SetRow(gridListQuotedMessages, 1);
                        gridListQuotedMessages.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                        _retuntGrid.Children.Add(gridListQuotedMessages);

                        var bottomQuotedMessages = BottomQuotedMessages(false);

                        Panel.SetZIndex(bottomQuotedMessages, -1);
                        Grid.SetColumn(bottomQuotedMessages, 2);
                        Grid.SetRow(bottomQuotedMessages, 2);

                        bottomQuotedMessages.Margin = new Thickness(MarginBubble.Left, MarginBubble.Top, MarginBubble.Right, MarginBubble.Bottom);
                        _retuntGrid.Children.Add(bottomQuotedMessages);
                    }

                    CanAttachedMessage = false;
                }
                else
                {
                    _rectangle = new Rectangle { RadiusX = 10, RadiusY = 10 };
                    _rectangle.Margin = MarginBubble;
                    _rectangle.Fill = _colorOtherMessage;
                    Grid.SetColumn(_rectangle, 2);
                    Grid.SetRow(_rectangle, 1);
                    Grid.SetRowSpan(_rectangle, 1);
                    Panel.SetZIndex(_rectangle, -1);
                    _retuntGrid.Children.Add(_rectangle);
                }
            }
        }

        /// <summary> Баббл цитированого сообщения без ответа </summary>
        private Grid OnlyTopQuotedMessages()
        {
            var result = new Grid();

            var rectangleTop = new Rectangle { RadiusX = 10, RadiusY = 10 };
            //нужно как-то хранить в unitedControlMessage чьи сообщения на которые я отвечаю
            rectangleTop.Fill = _colorOtherMessage;
            Panel.SetZIndex(rectangleTop, -1);
            result.Children.Add(rectangleTop);

            return result;
        }

        /// <summary> Верхняя часть баббла цитированого сообщения </summary>
        private Grid TopQuotedMessages()
        {
            var result = new Grid();

            var rectangleTop = new Rectangle { RadiusX = 10, RadiusY = 10 };
            //нужно как-то хранить в unitedControlMessage чьи сообщения на которые я отвечаю
            rectangleTop.Fill = _colorOtherMessage;
            Panel.SetZIndex(rectangleTop, -1);
            result.Children.Add(rectangleTop);

            var rectangleBottom = new Rectangle { Height = 10, VerticalAlignment = VerticalAlignment.Bottom };
            rectangleBottom.Fill = _colorOtherMessage;
            Panel.SetZIndex(rectangleBottom, -1);
            result.Children.Add(rectangleBottom);

            return result;
        }

        /// <summary> Нижняя часть баббла цитированого сообщения </summary>
        private Grid BottomQuotedMessages(bool isIam)
        {
            var result = new Grid();

            var rectangleTop = new Rectangle { Height = 10, VerticalAlignment = VerticalAlignment.Top };
            rectangleTop.Fill = (isIam) ? _colorMyMessage : _colorOtherMessage;
            Panel.SetZIndex(rectangleTop, -1);
            result.Children.Add(rectangleTop);

            var rectangleBottom = new Rectangle { RadiusX = 10, RadiusY = 10 };
            rectangleBottom.Fill = (isIam) ? _colorMyMessage : _colorOtherMessage;
            Panel.SetZIndex(rectangleBottom, -1);
            result.Children.Add(rectangleBottom);

            return result;
        }

        /// <summary> Запись о статусе сообщения </summary>
        private StackPanel StackPanelCheckServered(int count, string key)
        {
            var result = new StackPanel { Orientation = Orientation.Horizontal, Visibility = Visibility.Collapsed };

            for (var i = 0; i < count; i++)
                result.Children.Add(new Image { Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/check_green.png"), Width = 11, Height = 9 });

            for (var i = 4 - count; i > 0; i--)
                result.Children.Add(new Image { Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/check_gray.png"), Width = 11, Height = 9 });

            var textblockCheck = new TextBlock { TextWrapping = TextWrapping.Wrap, FontSize = 12, Margin = new Thickness(3, 0, 3, 0) };
            textblockCheck.SetResourceReference(TextBlock.TextProperty, key);

            result.Children.Add(textblockCheck);

            return result;
        }

        /// <summary> Можно ли добавить сообщение к баблу </summary>
        public static bool CanAttached(ModelChatMessage lastMessage, BubbleMessage lastBubble, UnitedControlMessage newMessage)
        {
            if (lastBubble == null) return false;

            var canAttachToBubble = lastBubble.CanAttachedMessage;

            var sameServeredStatus = lastBubble.Servered == newMessage.Message.Servered;

            var sameSender = newMessage.Message.Sender.DodicallId == lastMessage.Sender.DodicallId;

            var sameTime = newMessage.Message.SendTime.ToShortTimeString() == lastMessage.SendTime.ToShortTimeString();

            var isMessageText = newMessage.Message.ModelEnumChatMessageTypeObj.Code == 0 && !newMessage.Message.HaveQuoted;

            var isLastObjectInChat = lastBubble.ListUnitedControlMessageInBubbleMessage.LastOrDefault(obj => obj.Message == lastMessage) != null;

            var sameSecurityLevel = lastMessage.ModelEnumChatMessageSecurityLevelObj.Code == newMessage.Message.ModelEnumChatMessageSecurityLevelObj.Code;

            var result = isLastObjectInChat && canAttachToBubble && sameSender && sameTime && isMessageText && sameSecurityLevel && sameServeredStatus;

            return result;
        }

        /// <summary> Добавить сообщение к баблу </summary>
        public void AddNewMessage(UnitedControlMessage unitedControlMessage)
        {
            var newCount = _listUnitedControlMessageInBubbleMessage.Count + 1;

            var lastUnitedControlMessage = _listUnitedControlMessageInBubbleMessage.LastOrDefault();

            _retuntGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid.SetRow(unitedControlMessage.GridBackground, newCount);
            Grid.SetColumnSpan(unitedControlMessage.GridBackground, 4);
            Panel.SetZIndex(unitedControlMessage.GridBackground, -2);
            _retuntGrid.Children.Add(unitedControlMessage.GridBackground);

            unitedControlMessage.СheckBoxObj.Margin = new Thickness(0, 3, 0, 3);
            unitedControlMessage.СheckBoxObj.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(unitedControlMessage.СheckBoxObj, 0);
            Grid.SetRow(unitedControlMessage.СheckBoxObj, newCount);
            _retuntGrid.Children.Add(unitedControlMessage.СheckBoxObj);

            unitedControlMessage.TextBlockObj.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top - 2, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
            Grid.SetRow(unitedControlMessage.TextBlockObj, newCount);
            Grid.SetColumn(unitedControlMessage.TextBlockObj, 2);

            unitedControlMessage.BubbleMessageObj = this;

            _retuntGrid.Children.Add(unitedControlMessage.TextBlockObj);

            if (_sender.Iam)
            {
                Grid.SetColumn(unitedControlMessage.ImageEditMessage, 1);
                unitedControlMessage.ImageEditMessage.HorizontalAlignment = HorizontalAlignment.Right;
                unitedControlMessage.ImageEditMessage.Margin = new Thickness(0, 0, 5, 0);
            }
            else
            {
                Grid.SetColumn(unitedControlMessage.ImageEditMessage, 3);
                unitedControlMessage.ImageEditMessage.HorizontalAlignment = HorizontalAlignment.Left;
                unitedControlMessage.ImageEditMessage.Margin = new Thickness(5, 0, 0, 0);
            }

            unitedControlMessage.ImageEditMessage.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(unitedControlMessage.ImageEditMessage, newCount);
            _retuntGrid.Children.Add(unitedControlMessage.ImageEditMessage);

            Grid.SetRow(_botomRow, newCount + 1);
            Grid.SetRowSpan(_rectangle, newCount);

            _listUnitedControlMessageInBubbleMessage.Add(unitedControlMessage);
        }

        /// <summary> Удалить сообщение из бабла </summary>
        public void Remove(UnitedControlMessage unitedControlMessage)
        {
            if (ListUnitedControlMessageInBubbleMessage.Count > 1)
            {
                var count = _listUnitedControlMessageInBubbleMessage.Count;

                var indexDelete = _listUnitedControlMessageInBubbleMessage.IndexOf(unitedControlMessage);

                if (indexDelete == 0)
                {
                    var item1 = _listUnitedControlMessageInBubbleMessage[1];
                    item1.TextBlockObj.Margin = new Thickness(_marginBubble.Left + 10, _marginBubble.Top + 3, _marginBubble.Right + 7, _marginBubble.Bottom + 5);
                }

                _retuntGrid.Children.Remove(unitedControlMessage.GridBackground);
                _retuntGrid.Children.Remove(unitedControlMessage.СheckBoxObj);
                _retuntGrid.Children.Remove(unitedControlMessage.TextBlockObj);
                _retuntGrid.Children.Remove(unitedControlMessage.ImageEditMessage);

                _retuntGrid.RowDefinitions.RemoveAt(indexDelete);

                _listUnitedControlMessageInBubbleMessage.RemoveAt(indexDelete);

                Grid.SetRow(_botomRow, count);

                Grid.SetRowSpan(_rectangle, count - 1);

                for (var i = indexDelete + 1; i < count; i++)
                {
                    var item = _listUnitedControlMessageInBubbleMessage[i - 1];

                    Grid.SetRow(item.GridBackground, i);
                    Grid.SetRow(item.СheckBoxObj, i);
                    Grid.SetRow(item.TextBlockObj, i);
                    Grid.SetRow(item.ImageEditMessage, i);
                }
            }
            else
            {
                _retuntGrid.Children.Clear();
            }
        }

        /// <summary> Баблы после удаления сообщения </summary>
        public List<BubbleMessage> GetBubbleAfterRemove(UnitedControlMessage deleteUnitedControlMessage)
        {
            var resultArray = new BubbleMessage[3];
            var resultList = new List<BubbleMessage>();

            var index = 0;
            var deleteIndex = -1;

            foreach (var unitedControlMessage in ListUnitedControlMessageInBubbleMessage)
            {
                _retuntGrid.Children.Clear();

                if (unitedControlMessage == deleteUnitedControlMessage)
                {
                    deleteIndex = index;

                    deleteUnitedControlMessage.Refresh();

                    resultArray[1] = new BubbleMessage(unitedControlMessage);
                }
                else
                {
                    if (deleteIndex == -1)
                    {
                        if (index == 0)
                        {
                            resultArray[0] = new BubbleMessage(unitedControlMessage);
                        }
                        else
                        {
                            resultArray[0].AddNewMessage(unitedControlMessage);
                        }
                    }
                    else
                    {
                        if (index == deleteIndex + 1)
                        {
                            resultArray[2] = new BubbleMessage(unitedControlMessage);
                        }
                        else
                        {
                            resultArray[2].AddNewMessage(unitedControlMessage);
                        }
                    }
                }

                index++;
            }

            for (var i = 0; i < 3; i++)
            {
                if (resultArray[i] != null)
                    resultList.Add(resultArray[i]);
            }

            return resultList;
        }
    }
}
