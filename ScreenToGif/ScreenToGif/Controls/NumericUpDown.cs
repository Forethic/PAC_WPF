using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ScreenToGif.Controls
{
    public class NumericUpDown : Control
    {
        #region Varibales

        private RepeatButton _UpButton;
        private RepeatButton _DownButton;
        private TextBox _TextBox;

        public readonly static DependencyProperty MaximumProperty;
        public readonly static DependencyProperty MinimumProperty;
        public readonly static DependencyProperty ValueProperty;
        public readonly static DependencyProperty StepProperty;

        #endregion

        #region Properties

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set
            {
                if (value < Value)
                {
                    Value = value;
                }
                SetValue(MaximumProperty, value);
            }
        }

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set
            {
                if (value > Value)
                {
                    Value = value;
                }
                SetValue(MinimumProperty, value);
            }
        }

        [Description("The actual value of the numeric up and down.")]
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set
            {
                SetCurrentValue(ValueProperty, value);
                _TextBox.Text = value.ToString();
            }
        }

        public int StepValue
        {
            get => (int)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        #endregion

        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

            MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(10));
            MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(0));
            StepProperty = DependencyProperty.Register("StepValue", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(5));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(0));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _UpButton = Template.FindName("Part_UpButton", this) as RepeatButton;
            _DownButton = Template.FindName("Part_DownButton", this) as RepeatButton;
            _TextBox = Template.FindName("InternalBox", this) as TextBox;

            Value = Minimum;

            _TextBox.Text = Minimum.ToString();
            _UpButton.Click += UpButton_Click;
            _DownButton.Click += DownButton_Click;

            _TextBox.TextChanged += TextBox_TextChanged;
            _TextBox.PreviewTextInput += TextBox_PreviewTextInput;
            _TextBox.MouseWheel += TextBox_MouseWheel;
            _TextBox.LostFocus += TextBox_LostFocus;

            this.AddHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(PastingEvent));
        }

        #region Event

        private void PastingEvent(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetData(typeof(string)) is string text)
            {
                if (IsTextDisallow(text)) { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox && string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = Value.ToString();
            }
        }

        private void TextBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (e.Delta > 0)
                {
                    if (Value < Maximum)
                        Value = Convert.ToInt32(textbox.Text) + 1;
                }
                else
                {
                    if (Value > Minimum)
                        Value = Convert.ToInt32(textbox.Text) - 1;
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region Changes the value of the Numeric Up and Down

            if (sender is TextBox textbox)
            {
                int newValue = Convert.ToInt32(textbox.Text);

                if (newValue > Maximum) Value = Maximum;
                else if (newValue < Minimum) Value = Minimum;
                else Value = newValue;
            }

            #endregion
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > Minimum)
            {
                Value -= StepValue;
                if (Value < Minimum) Value = Minimum;

                _TextBox.Text = Value.ToString();
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value < Maximum)
            {
                Value += StepValue;
                if (Value > Maximum) Value = Maximum;

                _TextBox.Text = Value.ToString();
            }
        }

        #endregion

        private bool IsTextDisallow(string text)
        {
            var regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }
    }
}
