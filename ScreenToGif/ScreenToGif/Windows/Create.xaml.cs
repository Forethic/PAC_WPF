using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ScreenToGif.Controls;

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

        private void SizeBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is NumericTextBox textbox)
            {
                textbox.Value = Convert.ToInt32(textbox.Text);
                textbox.Value = e.Delta > 0 ? textbox.Value + 1 : textbox.Value - 1;
            }
        }

        private void WidthText_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckValues(sender);
        }

        private void WidthText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { CheckValues(sender); }
        }

        private void CheckValues(object sender)
        {
            if (sender is NumericTextBox textbox)
            {
                var value = Convert.ToInt32(textbox.Text);

                if (value > textbox.MaxValue) { textbox.Value = textbox.MaxValue; }
                else if (value < textbox.MinValue) { textbox.Value = textbox.MinValue; }
            }
        }

        #endregion

        #endregion
    }
}