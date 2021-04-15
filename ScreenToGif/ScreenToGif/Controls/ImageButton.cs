using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ScreenToGif.Controls
{
    public class ImageButton : Button
    {
        #region Variables

        public Viewbox Viewbox;
        public TextBlock Label;

        public readonly static DependencyProperty ChildProperty;
        public readonly static DependencyProperty TextProperty;
        public readonly static DependencyProperty MaxSizeProperty;

        #endregion

        #region Properties

        [Description("The Image of the button.")]
        public UIElement Child
        {
            get => (UIElement)GetValue(ChildProperty);
            set => SetValue(ChildProperty, value);
        }

        [Description("The text of the button.")]
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        [Description("The maximum size of the image.")]
        public double MaxSize
        {
            get => (double)GetValue(MaxSizeProperty);
            set => SetValue(MaxSizeProperty, value);
        }

        #endregion

        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));

            ChildProperty = DependencyProperty.Register("Child", typeof(UIElement), typeof(ImageButton), new FrameworkPropertyMetadata());
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ImageButton), new FrameworkPropertyMetadata("Button"));
            MaxSizeProperty = DependencyProperty.Register("MaxSize", typeof(double), typeof(ImageButton), new FrameworkPropertyMetadata(26.0));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Viewbox = Template.FindName("ViewBoxInternal", this) as Viewbox;
            Label = Template.FindName("TextBlockInternal", this) as TextBlock;
        }
    }
}
