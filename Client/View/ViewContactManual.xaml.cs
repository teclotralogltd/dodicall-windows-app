using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ViewContactManual.xaml
    /// </summary>
    public partial class ViewContactManual : UserControl, IUserControlCloseWindow, IWindowCaption
    {
        /// <summary> Объект ViewModelManualContact </summary>
        private readonly ViewModelManualContact _viewModelManualContact;

        ///<summary> Объект для сравнения цифр </summary>
        private readonly Regex _regex = new Regex(@"[1234567890\+]");

        /// <summary> Ключ ресурса для заголовка окна </summary>
        public string WindowCaptionResourceKey { get; set; } = @"ViewManualContact_Title";

        /// <summary> Событие закрытия окна </summary>
        public event EventHandler CloseWindow; 

        /// <summary> Конструктор </summary>
        public ViewContactManual()
        {
            InitializeComponent();

            _viewModelManualContact = new ViewModelManualContact();

            _viewModelManualContact.CloseView += (sender, args) => OnCloseWindow();

            // кастыль для демо
            _viewModelManualContact.AddManualContactSuccessful += (sender, args) =>
            {
                if (ViewContact.CurrentViewContact.IsVisible) ViewContact.CurrentViewContact.AddContactListBoxContact(args, true);
            };

            _viewModelManualContact.ChangedListModelUserContactExtra += (sender, args) => RefreshListModelUserContact();

            _viewModelManualContact.CurrentModelContact.PropertyChanged += (sender, args) => ModelContactOnPropertyChanged(sender as ModelContact);

            DataContext = _viewModelManualContact;

            RefreshListModelUserContact();
        }

        /// <summary> Конструктор </summary>
        public ViewContactManual(string number)
        {
            InitializeComponent();

            _viewModelManualContact = new ViewModelManualContact(number);

            _viewModelManualContact.CloseView += (sender, args) => OnCloseWindow();

            // кастыль для демо
            _viewModelManualContact.AddManualContactSuccessful += (sender, args) =>
            {
                if (ViewContact.CurrentViewContact.IsVisible) ViewContact.CurrentViewContact.AddContactListBoxContact(args, true);
            };

            _viewModelManualContact.ChangedListModelUserContactExtra += (sender, args) => RefreshListModelUserContact();

            _viewModelManualContact.CurrentModelContact.PropertyChanged += (sender, args) => ModelContactOnPropertyChanged(sender as ModelContact);

            DataContext = _viewModelManualContact;

            RefreshListModelUserContact();
        }

        /// <summary> Действие перед закрытием окна </summary>
        public bool BeforeCloseWindow()
        {
            return true;
        }

        /// <summary> Обновление элемента списка номеров телефонов </summary>
        private void RefreshListModelUserContact()
        {
            StackPanelNumberPhone.Children.Clear();

            foreach (var modelUserContact in _viewModelManualContact.CurrentModelContact.ListModelUserContactExtra)
            {
                StackPanelNumberPhone.Children.Add(CreateContactControl(modelUserContact));
            }
        }

        /// <summary> Создание элемента для котактных номеров </summary>
        private Grid CreateContactControl(ModelUserContact modelUserContact)
        {
            var grid = new Grid { Margin = new Thickness(0,0,0,10), ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto}, new ColumnDefinition(), new ColumnDefinition { Width = GridLength.Auto } } };

            var comboBox = new ComboBox { Margin = new Thickness(0,0,10,0), Width = 180, ItemsSource = _viewModelManualContact.ListModelEnumUserContactType, DisplayMemberPath = @"Name" };

            var bindingComboBox = new Binding { Source = modelUserContact, Path = new PropertyPath("ModelEnumUserContactTypeObj") };
            comboBox.SetBinding(ComboBox.SelectedItemProperty, bindingComboBox);

            var textBox = new TextBox();

            var bindingTextBox = new Binding { Source = modelUserContact, Path = new PropertyPath("Identity") };
            textBox.SetBinding(TextBox.TextProperty, bindingTextBox);
            textBox.PreviewTextInput += TextBoxPreviewTextInput;
            textBox.TextChanged += TextBoxOnTextChanged;
            textBox.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand)); // нужно для запрета вставки по Ctrl+V если оставить обработчик пустым

            Grid.SetColumn(textBox, 1);

            var image = new Image { Margin = new Thickness(0, 1, 0, 0), Height = 11, Source = UtilityPicture.GetBitmapImageFromStringUri(@"/Resources/cancel.png") };
            var button = new Button { Margin = new Thickness(5,0,0,0), Content = image, VerticalContentAlignment = VerticalAlignment.Center, Style = Application.Current.TryFindResource(@"ButtonStyleTransparent") as Style };
            button.DataContext = modelUserContact;
            button.Click += (sender, args) => _viewModelManualContact.RemoveUserContact((sender as Button)?.DataContext as ModelUserContact);

            Grid.SetColumn(button, 2);

            grid.Children.Add(comboBox);
            grid.Children.Add(textBox);
            grid.Children.Add(button);

            return grid;
        }

        /// <summary> Обработчик изменения текста в TextBox </summary>
        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;

            var text = textBox.Text;

            //var r = new Regex(@"\d"); // Соответствует любая цифра
            var m = _regex.Match(text);
            if (!m.Success)
            {
                text = String.Empty;
            }

            if (text.Length > 20)
            {
                textBox.Text = text.Substring(0, 20);
                textBox.CaretIndex = textBox.Text.Length;
            }
            else
            {
                textBox.Text = text;
            }

            /* -------------- заготовка под FormatPhone --------------  */

            //var textBox = sender as TextBox;

            //if (textBox == null) return;

            //var str = new StringBuilder();

            //foreach (char charText in textBox.Text)
            //{
            //    if (char.IsDigit(charText)) str.Append(charText);
            //}

            //var caretIndex = textBox.CaretIndex;

            //textBox.Text = _viewModelManualContact.FormatPhone(@"+" + str);

            //textBox.CaretIndex = caretIndex;
        }

        /// <summary> Обработчик ввода в TextBox номера телефона (запрет ввода букв) </summary>
        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //var r = new Regex(@"\d"); // Соответствует любая цифра
            var m = _regex.Match(e.Text);
            if (!m.Success)
            {
                e.Handled = true;
            }

            var textBox = (TextBox)sender;

            if (textBox?.Text.Length >= 20)
            {
                e.Handled = true;
            }
        }

        /// <summary> Обработчик вставки из буффера в TextBox </summary>
        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            var textClipboard = Clipboard.GetText();

            if (String.IsNullOrWhiteSpace(textClipboard)) return;

            var regex = new Regex(@"[^\+\#\*1234567890]");
            var textNumber = regex.Replace(textClipboard, "");

            if (textBox.CaretIndex > 0) textNumber = textNumber.Replace("+", "");

            var indexCaret = textBox.CaretIndex;
            textBox.Text = textBox.Text.Insert(indexCaret, textNumber);
            textBox.CaretIndex = indexCaret + textNumber.Length;
        }

        /// <summary> Обработчик изменения ModelContact </summary>
        private void ModelContactOnPropertyChanged(ModelContact modelContact)
        {
            if (String.IsNullOrWhiteSpace(modelContact?.FirstName) && String.IsNullOrWhiteSpace(modelContact?.LastName))
            {
                ButtonOk.IsEnabled = false;
            }
            else
            {
                ButtonOk.IsEnabled = true;
            }
        }

        /// <summary> Обработчик ввода в TextBox Имени и Фамилии </summary>
        private void FirstLastName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox?.Text.Length >= 40)
            {
                e.Handled = true;
            }
        }

        /// <summary> Обработчик ввода в TextBox Имени и Фамилии </summary>
        private void FirstLastName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox?.Text.Length > 40)
            {
                textBox.Text = textBox.Text.Substring(0, 40);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        /// <summary> Инвокатор события закрытия окна </summary>
        private void OnCloseWindow()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }
    }
}
