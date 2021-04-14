using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ScreenToGif.Behaviours
{
    /// <summary>
    /// Regular expression for Textbox with properties
    ///     <see cref="RegularExpression"/>
    ///     <see cref="MaxLength"/>
    ///     <see cref="EmptyValue"/>
    /// </summary>
    public class TextBoxInputBehaviour : Behavior<TextBox>
    {
        #region DependencyProperties

        #region ReguularExpression

        public string RegularExpression
        {
            get => (string)GetValue(RegularExpressionProperty);
            set => SetValue(RegularExpressionProperty, value);
        }

        public static DependencyProperty RegularExpressionProperty = DependencyProperty.Register("TextBoxInputRegExBehaviour", typeof(string), typeof(TextBoxInputBehaviour));

        #endregion

        #region MaxLength

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static DependencyProperty MaxLengthProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxInputBehaviour), new FrameworkPropertyMetadata(int.MinValue));

        #endregion

        #region EmptyValue

        public string EmptyValue
        {
            get => (string)GetValue(EmptyValueProperty);
            set => SetValue(EmptyValueProperty, value);
        }

        public static DependencyProperty EmptyValueProperty = DependencyProperty.Register("EmptyValue", typeof(string), typeof(TextBoxInputBehaviour));

        #endregion

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
            AssociatedObject.PreviewKeyDown += PreviewKeyDownHandler;
            DataObject.AddPastingHandler(AssociatedObject, PastingHandler);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewTextInput -= PreviewTextInputHandler;
            AssociatedObject.PreviewKeyDown -= PreviewKeyDownHandler;
            DataObject.RemovePastingHandler(AssociatedObject, PastingHandler);
        }


        #region Event

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!ValidateText(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(EmptyValue)) return;

            string text = null;

            if (e.Key == Key.Back)
            {
                if (!TreatSelectedText(out text))
                {
                    if (AssociatedObject.SelectionStart > 0)
                        text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart - 1, 1);
                }
            }
            else if (e.Key == Key.Delete)
            {
                if (!TreatSelectedText(out text) && AssociatedObject.Text.Length > AssociatedObject.SelectionStart)
                {
                    text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, 1);
                }
            }

            if (text == string.Empty)
            {
                AssociatedObject.Text = EmptyValue;
                if (e.Key == Key.Back)
                    AssociatedObject.SelectionStart++;
                e.Handled = true;
            }
        }

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            string text;
            if (AssociatedObject.Text.Length < AssociatedObject.CaretIndex)
                text = AssociatedObject.Text;
            else
            {
                text = TreatSelectedText(out var remainTextAfterRemoveSelection)
                    ? remainTextAfterRemoveSelection.Insert(AssociatedObject.SelectionStart, e.Text)
                    : AssociatedObject.Text.Insert(AssociatedObject.CaretIndex, e.Text);
            }

            e.Handled = !ValidateText(text);
        }

        #endregion

        #region Methods

        private bool ValidateText(string text)
        {
            return (new Regex(RegularExpression, RegexOptions.IgnoreCase)).IsMatch(text) && (MaxLength == 0 || text.Length <= MaxLength);
        }

        private bool TreatSelectedText(out string text)
        {
            text = null;

            if (AssociatedObject.SelectionLength > 0)
            {
                text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
                return true;
            }
            return false;
        }

        #endregion
    }
}