using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScreenToGif.Controls
{
    public class NumericTextBox : TextBox
    {
        #region Varibales

        private TextBox _TextBox;

        public readonly static DependencyProperty MaxValueProperty;
        public readonly static DependencyProperty MinValueProperty;
        public readonly static DependencyProperty ValueProperty;

        public static readonly RoutedEvent ValueChangedEvent;

        public event RoutedEventHandler ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }

        void RaiseValueChangedEvent() => RaiseEvent(new RoutedEventArgs(ValueChangedEvent));

        #endregion

        #region Properties

        [Description("The maximum value of the numeric text box.")]
        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetCurrentValue(MaxValueProperty, value);
        }

        [Description("The minimum value of the numeric text box.")]
        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetCurrentValue(MinValueProperty, value);
        }

        [Description("The actual value of the numeric text box.")]
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set
            {
                SetCurrentValue(ValueProperty, value);
                RaiseValueChangedEvent();
            }
        }

        #endregion

        static NumericTextBox()
        {
            MaxValueProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(NumericTextBox), new UIPropertyMetadata(10));
            MinValueProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(NumericTextBox), new UIPropertyMetadata(0));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericTextBox), new FrameworkPropertyMetadata(0));

            ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericTextBox));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PreviewTextInput += TextBox_PreviewTextInput;
            ValueChanged += NumericTextBox_ValueChanged;
            AddHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(PastingEvent));
        }

        #region Event

        private void NumericTextBox_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                ValueChanged -= NumericTextBox_ValueChanged;

                if (Value > MaxValue) Value = MaxValue;
                else if (Value < MinValue) Value = MinValue;

                textbox.Text = Value.ToString();

                ValueChanged += NumericTextBox_ValueChanged;

                textbox.Text = Value.ToString();
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

        private void PastingEvent(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetData(typeof(string)) is string text)
            {
                if (IsTextDisallow(text)) { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        #endregion

        private bool IsTextDisallow(string text)
        {
            var regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }
    }
}
