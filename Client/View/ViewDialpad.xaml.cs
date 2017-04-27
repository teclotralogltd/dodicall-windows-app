using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using DAL.Utility;
using DAL.ViewModel;

namespace dodicall.View
{
    /// <summary>
    /// Interaction logic for ViewDialpad.xaml
    /// </summary>
    public partial class ViewDialpad : UserControl
    {
        ///<summary> Объект ViewModelDialpad </summary>
        private readonly ViewModelDialpad _viewModelDialpad = new ViewModelDialpad();

        ///<summary> Объект для изменения времени нажатия кнопок </summary>
        private readonly DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) }; // интервал 1 сек

        ///<summary> Объект для сравнения цифр </summary>
        private readonly Regex _regex = new Regex(@"[\+\#\*1234567890]");

        ///<summary> Конструктор </summary>
        public ViewDialpad()
        {
            InitializeComponent();

            DataContext = _viewModelDialpad;

            _viewModelDialpad.EventViewModel += ViewModelDialpadOnEventViewModel;
        }

        /// <summary> Обработчик события ViewModelDialpad </summary>
        private void ViewModelDialpadOnEventViewModel(object sender, ViewModelEventHandlerArgs e)
        {
            if (e.Key == "IncomingCall")
            {
                WindowCallActive.IncomingCall();
            }

            if (e.Key == "CallEnableChanged")
            {
                Dispatcher.Invoke(() =>
                {
                    var isCallEnd = (bool) e.Data;

                    ButtonCall.Source = UtilityPicture.GetBitmapImageFromStringUri("/Resources/" + (isCallEnd ? "start_call.png" : "start_call_disable.png"));
                });
            }
        }

        /// <summary> Обработчик события получения фокуса TextBoxPhoneNumber </summary>
        private void TextBoxPhoneNumber_OnGotFocus(object sender, RoutedEventArgs e)
        {
            RectanglePhoneNumber.Visibility = Visibility.Visible;
        }

        /// <summary> Обработчик события потери фокуса TextBoxPhoneNumber </summary>
        private void TextBoxPhoneNumber_OnLostFocus(object sender, RoutedEventArgs e)
        {
            RectanglePhoneNumber.Visibility = Visibility.Hidden;
        }

        /// <summary> Обработчик события нажатия кнопки ButtonCall </summary>
        private void ButtonCall_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartCall();
        }

        /// <summary> Обработчик события нажатия кнопки 1 </summary>
        private void ButtonNumber1_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("1");
            //_viewModelDialpad.StartSoundSignal('1');
        }

        /// <summary> Обработчик события нажатия кнопки 2 </summary>
        private void ButtonNumber2_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("2");
            //_viewModelDialpad.StartSoundSignal('2');
        }

        /// <summary> Обработчик события нажатия кнопки 3 </summary>
        private void ButtonNumber3_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("3");
            //_viewModelDialpad.StartSoundSignal('3');
        }

        /// <summary> Обработчик события нажатия кнопки 4 </summary>
        private void ButtonNumber4_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("4");
            //_viewModelDialpad.StartSoundSignal('4');
        }

        /// <summary> Обработчик события нажатия кнопки 5 </summary>
        private void ButtonNumber5_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("5");
            //_viewModelDialpad.StartSoundSignal('5');
        }

        /// <summary> Обработчик события нажатия кнопки 6 </summary>
        private void ButtonNumber6_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("6");
            //_viewModelDialpad.StartSoundSignal('6');
        }

        /// <summary> Обработчик события нажатия кнопки 7 </summary>
        private void ButtonNumber7_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("7");
            //_viewModelDialpad.StartSoundSignal('7');
        }

        /// <summary> Обработчик события нажатия кнопки 8 </summary>
        private void ButtonNumber8_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("8");
            //_viewModelDialpad.StartSoundSignal('8');
        }

        /// <summary> Обработчик события нажатия кнопки 9 </summary>
        private void ButtonNumber9_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("9");
            //_viewModelDialpad.StartSoundSignal('9');
        }

        /// <summary> Обработчик события нажатия кнопки * </summary>
        private void ButtonAsterisk_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("*");
            //_viewModelDialpad.StartSoundSignal('*');
        }

        /// <summary> Обработчик события нажатия кнопки # </summary>
        private void ButtonHashtag_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InsertNumberInPhoneNumber("#");
            //_viewModelDialpad.StartSoundSignal('#');
        }

        /// <summary> Вставка цифры в номер </summary>
        private void InsertNumberInPhoneNumber(string number)
        {
            int indexCaret;

            string textNumber = null;

            if (TextBoxPhoneNumber.SelectionLength > 0)
            {
                //TextBoxPhoneNumber.Text = TextBoxPhoneNumber.Text.Remove(TextBoxPhoneNumber.SelectionStart, TextBoxPhoneNumber.SelectionLength);
                textNumber = TextBoxPhoneNumber.Text.Remove(TextBoxPhoneNumber.SelectionStart, TextBoxPhoneNumber.SelectionLength);

                indexCaret = TextBoxPhoneNumber.SelectionStart;
            }
            else
            {
                indexCaret = TextBoxPhoneNumber.CaretIndex;
            }

            //TextBoxPhoneNumber.Text = TextBoxPhoneNumber.Text.Insert(indexCaret, number);
            TextBoxPhoneNumber.Text = textNumber == null ? TextBoxPhoneNumber.Text.Insert(indexCaret, number) : textNumber.Insert(indexCaret, number);
            TextBoxPhoneNumber.CaretIndex = indexCaret + number.Length;
        }

        /// <summary> Обработчик события нажатия кнопки очистки номера </summary>
        private void ButtonClear_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBoxPhoneNumber.CaretIndex > 0)
            {
                _timer.Start();

                _timer.Tick += ButtonClear_OnElapsed;
            }
        }

        /// <summary> Обработчик таймера при нажатии ButtonClear </summary>
        private void ButtonClear_OnElapsed(object sender, EventArgs e)
        {
            ClearOrDeletePhoneNumber(true);
        }

        /// <summary> Обработчик события отжатия кнопки очистки номера </summary>
        private void ButtonClear_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClearOrDeletePhoneNumber(false);
        }

        /// <summary> Очистка или удаление символа из номера </summary>
        private void ClearOrDeletePhoneNumber(bool clearAllPhoneNumber)
        {
            if (!_timer.IsEnabled) return;

            _timer.Stop();

            _timer.Tick -= ButtonClear_OnElapsed;

            if (clearAllPhoneNumber)
            {
                TextBoxPhoneNumber.Text = String.Empty;
            }
            else
            {
                var indexCaret = TextBoxPhoneNumber.CaretIndex;
                TextBoxPhoneNumber.Text = TextBoxPhoneNumber.Text.Remove(indexCaret - 1, 1);
                TextBoxPhoneNumber.CaretIndex = indexCaret - 1;
            }
        }

        /// <summary> Обработчик события покидания курсора кнопки очистки номера </summary>
        private void ButtonClear_OnMouseLeave(object sender, MouseEventArgs e)
        {
            _timer.Stop();

            _timer.Tick -= ButtonClear_OnElapsed;
        }

        /// <summary> Обработчик события нажатия кнопки 0 </summary>
        private void ButtonNumber0_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _timer.Start();

            _timer.Tick += ButtonNumber0_OnElapsed;

            //_viewModelDialpad.StartSoundSignal('0');
        }

        /// <summary> Обработчик таймера при нажатии ButtonClear </summary>
        private void ButtonNumber0_OnElapsed(object sender, EventArgs e)
        {
            ZeroOrPlusPhoneNumber(true);
        }

        /// <summary> Обработчик события отжатия кнопки 0 </summary>
        private void ButtonNumber0_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ZeroOrPlusPhoneNumber(false);
            //_viewModelDialpad.StopSoundSignal();
        }

        /// <summary> Вставка нуля или плюса в номер </summary>
        private void ZeroOrPlusPhoneNumber(bool plus)
        {
            if (!_timer.IsEnabled) return;

            _timer.Stop();

            _timer.Tick -= ButtonNumber0_OnElapsed;

            if (plus)
            {
                if (TextBoxPhoneNumber.CaretIndex == 0)
                {
                    // для того что бы давать вводить "+" только в начале номера
                    TextBoxPhoneNumber.Text = TextBoxPhoneNumber.Text.Insert(0, "+");
                    TextBoxPhoneNumber.CaretIndex = 1;
                }
            }
            else
            {
                InsertNumberInPhoneNumber("0");
            }
        }

        /// <summary> Обработчик события покидания курсора кнопки 0 </summary>
        private void ButtonNumber0_OnMouseLeave(object sender, MouseEventArgs e)
        {
            _timer.Stop();

            _timer.Tick -= ButtonNumber0_OnElapsed;

            //_viewModelDialpad.StopSoundSignal();
        }

        /// <summary> Обработчик события изменения текста TextBoxPhoneNumber </summary>
        private void TextBoxPhoneNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = TextBoxPhoneNumber.Text;

            var m = _regex.Match(text);
            if (!m.Success)
            {
                text = String.Empty;
            }

            TextBoxPhoneNumber.Text = text;

            if (TextBoxPhoneNumber.Text.Length > 19)
            {
                TextBoxPhoneNumber.Margin = new Thickness(18, 3, 26, 0);
                TextBlockDot.Visibility = Visibility.Visible;
            }
            else
            {
                TextBoxPhoneNumber.Margin = new Thickness(3, 3, 26, 0);
                TextBlockDot.Visibility = Visibility.Hidden;
            }
        }

        /// <summary> Обработчик события изменения текста TextBoxPhoneNumber </summary>
        private void TextBoxPhoneNumber_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var m = _regex.Match(e.Text);
            if (!m.Success)
            {
                e.Handled = true;
            }
            else
            {
                if (e.Text == "+")
                {
                    if ((TextBoxPhoneNumber.CaretIndex > 0) || (TextBoxPhoneNumber.Text.Length > 0 && TextBoxPhoneNumber.Text[0] == '+'))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary> Обработчик события нажатия клавиши в поле ввода номера </summary>
        private void TextBoxPhoneNumber_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) TextBoxPhoneNumber.Text = String.Empty;

            if (e.Key == Key.Enter)
            {
                StartCall();
            }
        }

        /// <summary> Обработчик события покидания курсора кнопки со звуковым сигналом </summary>
        private void ButtonWithSoundSignal_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //_viewModelDialpad.StopSoundSignal();
        }

        /// <summary> Обработчик события отжатия кнопки со звуковым сигналом </summary>
        private void ButtonWithSoundSignal_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_viewModelDialpad.StopSoundSignal();
        }

        /// <summary> Обработчик вставки из буффера в TextBoxPhoneNumber </summary>
        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var textClipboard = Clipboard.GetText();

            if (String.IsNullOrWhiteSpace(textClipboard)) return;

            var regex = new Regex(@"[^\+\#\*1234567890]");
            var textNumber = regex.Replace(textClipboard, "");

            if (textNumber.Length > 1) textNumber = textNumber[0] + textNumber.Substring(1, textNumber.Length - 1).Replace("+", "");

            if (TextBoxPhoneNumber.CaretIndex > 0) textNumber = textNumber.Replace("+", "");

            InsertNumberInPhoneNumber(textNumber);
        }

        /// <summary> Метод начала звонка </summary>
        private void StartCall()
        {
            WindowCallActive.OutgoingCall(_viewModelDialpad.PhoneNumber);

            _viewModelDialpad.PhoneNumber = String.Empty;
        }

        /// <summary> Обработчик двойного нажатия мыши TextBoxPhoneNumber </summary>
        private void TextBoxPhoneNumber_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBoxPhoneNumber.SelectAll();
        }
    }
}
