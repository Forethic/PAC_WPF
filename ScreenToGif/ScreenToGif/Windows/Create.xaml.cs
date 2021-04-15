using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ScreenToGif.Windows
{
    /// <summary>
    /// Create.xaml 的交互逻辑
    /// </summary>
    public partial class Create : Window
    {
        #region Properties

        public int HeightValue { get; set; }

        public int WidthValue { get; set; }

        public Brush BrushValue { get; set; }

        #endregion

        #region Constructors

        public Create()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation

            if (!int.TryParse(WidthText.Text, out var width)) { return; }

            if (!int.TryParse(HeightText.Text, out var height)) { return; }

            var selected = BackCombo.SelectedItem;
            if (selected == null) return;

            #endregion

            HeightValue = height;
            WidthValue = width;
            BrushValue = ((selected as StackPanel).Children[0] as Border).Background;

            DialogResult = true;
            Close();
        }

        #region Input Event

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
            {
                e.Handled = true;
                return;
            }

            if (IsTextDisallow(e.Text))
            {
                e.Handled = true;
                return;
            }

            if (string.IsNullOrEmpty(e.Text))
            {
                e.Handled = true;
                return;
            }
        }

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetData(typeof(string)) is string text)
            {
                if (IsTextDisallow(text)) { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textbox && textbox.Text == string.Empty)
            {
                textbox.Text = "50";
            }
        }

        private bool IsTextDisallow(string text)
        {
            var regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        #endregion

        #endregion
    }
}