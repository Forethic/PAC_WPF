using System.Windows;
using Awesomium.Windows.Controls;

namespace MarkPad.XAML.AttachedProperties
{
    public static class WebBrowserUtility
    {
        #region BindableContent

        public static string GetBindableContent(DependencyObject obj) => (string)obj.GetValue(BindableContentProperty);

        public static void SetBindableContent(DependencyObject obj, string value) => obj.SetValue(BindableContentProperty, value);

        public static DependencyProperty BindableContentProperty = DependencyProperty.RegisterAttached("BindableContent", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableContentPropertyChanged));

        public static void BindableContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WebControl webControl)
            {
                var content = e.NewValue as string;

                if (string.IsNullOrEmpty(content))
                    content = " ";

                webControl.LoadHTML(content);
            }
        }

        #endregion
    }
}