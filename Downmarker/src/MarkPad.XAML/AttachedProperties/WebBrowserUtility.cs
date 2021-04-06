using System.Windows;
using System.Windows.Controls;

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
            if (d is WebBrowser browser)
            {
                var content = e.NewValue as string;

                if (string.IsNullOrEmpty(content))
                    content = " ";

                browser.NavigateToString(content);
            }
        }

        #endregion
    }
}