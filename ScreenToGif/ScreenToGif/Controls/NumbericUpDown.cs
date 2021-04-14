using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ScreenToGif.Controls
{
    public class NumbericUpDown : Control
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

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetCurrentValue(ValueProperty, value);
        }

        public int StepValue
        {
            get => (int)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        #endregion

        static NumbericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumbericUpDown), new FrameworkPropertyMetadata(typeof(NumbericUpDown)));

            MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(NumbericUpDown), new UIPropertyMetadata(10));
            MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(NumbericUpDown), new UIPropertyMetadata(0));
            StepProperty = DependencyProperty.Register("StepValue", typeof(int), typeof(NumbericUpDown), new FrameworkPropertyMetadata(5));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumbericUpDown), new FrameworkPropertyMetadata(0));
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
        }

        #region Event

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
    }
}
