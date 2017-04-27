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
using DAL.Model;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewChatEditMessage.xaml
    /// </summary>
    public partial class ViewChatEditMessage : UserControl, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewChatEditMessage_Title";
        
        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow;

        /// <summary> Текущий экземпряр ViewChatMessageDetail </summary>
        private ViewChatMessageDetail _viewChatMessageDetail;

        /// <summary> Редактируемое сообщение </summary>
        private UnitedControlMessage _editMessageControl;

        /// <summary> Начальный текст TextBlock </summary>
        private string _earlyTextFromTextBlock;
        
        /// <summary> TextBlock-и цитат для ресайза </summary>
        public List<TextBlock>[] ArrayListTextBlockQuotedMessage;
        
        private Grid _attachQuotedMessages;

        private bool _shouldDeleteQuote;

        /// <summary> Конструктор </summary>
        public ViewChatEditMessage(ViewChatMessageDetail viewChatMessageDetail, ViewModelChatDetail viewModelChatDetail)
        {
            InitializeComponent();

            _viewChatMessageDetail = viewChatMessageDetail;

            _editMessageControl = _viewChatMessageDetail.ControlEditMessage;

            _earlyTextFromTextBlock = _editMessageControl.Message.StringContent;

            _shouldDeleteQuote = false;

            TextEditMessage.Text = _earlyTextFromTextBlock;

            if(_editMessageControl.Message.HaveQuoted)
            {
                var quotedMessage = new QuotedMessage(_editMessageControl.Message.ListQuotedModelChatMessage.ToList(), viewModelChatDetail);

                _attachQuotedMessages = quotedMessage.ReturnGrid;

                ArrayListTextBlockQuotedMessage = quotedMessage.ArrayListTextBlock;

                GridQuotedMessage.Children.Add(_attachQuotedMessages);

                RectangleQuotedMessage.Visibility = Visibility.Visible;
                GridVisibilityQuotedMessage.Visibility = Visibility.Visible;

            }
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
        }

        /// <summary> Отработка клика по кнопке "Отменить" </summary>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCloseWindow();
        }

        /// <summary> Отработка клика по кнопке "Сохранить" </summary>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveText();
        }

        /// <summary> Обработчик ввода символов </summary>
        private void TextEditMessage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                {
                    var textBox = (TextBox)sender;

                    var caretIndex = textBox.CaretIndex;

                    textBox.Text = textBox.Text.Insert(caretIndex, "\n");

                    textBox.CaretIndex = caretIndex + 1;
                }
                else
                {
                    SaveText();
                }

                e.Handled = true;
            }
        }

        /// <summary> Отработка изменения текста </summary>
        private void TextEditMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextEditMessage.Text))
            {
                ButtonSave.IsEnabled = false;
            }
            else
            {
                ButtonSave.IsEnabled = true;
            }
        }

        /// <summary> Сохранить текст сообщения </summary>
        private void SaveText()
        {
            if (string.IsNullOrWhiteSpace(TextEditMessage.Text))
                return;

            if (_editMessageControl.Message.HaveQuoted)
            {
                _viewChatMessageDetail.EditMessage(_editMessageControl, TextEditMessage.Text, _shouldDeleteQuote);
            }
            else
            {
                if (_earlyTextFromTextBlock != TextEditMessage.Text)
                    _viewChatMessageDetail.EditMessage(_editMessageControl, TextEditMessage.Text);
            }
            
            OnCloseWindow();
        }
        
        /// <summary> Обработчик удаления цитированых сообщений </summary>
        private void ButtonRemoveQuotedMessage_Click(object sender, RoutedEventArgs e)
        {
            GridQuotedMessage.Children.Remove(_attachQuotedMessages);
            
            RectangleQuotedMessage.Visibility = Visibility.Collapsed;
            GridVisibilityQuotedMessage.Visibility = Visibility.Collapsed;

            _shouldDeleteQuote = true;
        }

        private void ViewChatEditMessage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ArrayListTextBlockQuotedMessage != null)
            {
                for (var j = 0; j < ArrayListTextBlockQuotedMessage.Count(); j++)
                    foreach (var k in ArrayListTextBlockQuotedMessage[j])
                    {
                        k.MaxWidth = ActualWidth - 115;
                    }
            }
        }
    }
}
