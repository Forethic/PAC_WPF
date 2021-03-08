using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Panuon.UI.Silver
{
    public class PasswordBoxHelper
    {
        #region PasswordBoxStyle

        public static PasswordBoxStyle GetPasswordBoxStyle(DependencyObject obj) => (PasswordBoxStyle)obj.GetValue(PasswordBoxStyleProperty);
        public static void SetPasswordBoxStyle(DependencyObject obj, PasswordBoxStyle value) => obj.SetValue(PasswordBoxStyleProperty, value);
        public static DependencyProperty PasswordBoxStyleProperty = DependencyProperty.RegisterAttached("PasswordBoxStyle", typeof(PasswordBoxStyle), typeof(PasswordBoxHelper));

        #endregion

        #region FocusedBorderBrush

        public static Brush GetFocusedBorderBrush(DependencyObject obj) => (Brush)obj.GetValue(FocusedBorderBrushProperty);
        public static void SetFocusedBorderBrush(DependencyObject obj, Brush value) => obj.SetValue(FocusedBorderBrushProperty, value);
        public static DependencyProperty FocusedBorderBrushProperty = DependencyProperty.RegisterAttached("FocusedBorderBrush", typeof(Brush), typeof(PasswordBoxHelper));

        #endregion

        #region FocusedShadowColor

        public static Color GetFocusedShadowColor(DependencyObject obj) => (Color)obj.GetValue(FocusedShadowColorProperty);
        public static void SetFocusedShadowColor(DependencyObject obj, Color value) => obj.SetValue(FocusedShadowColorProperty, value);
        public static DependencyProperty FocusedShadowColorProperty = DependencyProperty.RegisterAttached("FocusedShadowColor", typeof(Color), typeof(PasswordBoxHelper));

        #endregion

        #region CornerRadius

        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(PasswordBoxHelper));

        #endregion

        #region Watermark

        public static string GetWatermark(DependencyObject obj) => (string)obj.GetValue(WatermarkProperty);
        public static void SetWatermark(DependencyObject obj, string value) => obj.SetValue(WatermarkProperty, value);
        public static DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(PasswordBoxHelper));

        #endregion

        #region Icon

        public static object GetIcon(DependencyObject obj) => (object)obj.GetValue(IconProperty);
        public static void SetIcon(DependencyObject obj, object value) => obj.SetValue(IconProperty, value);
        public static DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(object), typeof(PasswordBoxHelper));

        #endregion

        #region Password

        public static string GetPassword(DependencyObject obj) => (string)obj.GetValue(PasswordProperty);
        public static void SetPassword(DependencyObject obj, string value) => obj.SetValue(PasswordProperty, value);
        public static DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(OnPasswordChanged));

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            var password = e.NewValue as string;

            if (password != passwordBox.Password)
            {
                passwordBox.Password = password;
            }
        }

        #endregion

        #region (Internal) PasswordHook (Default is true)

        public static bool GetPasswordHook(DependencyObject obj) => (bool)obj.GetValue(PasswordHookProperty);
        public static void SetPasswordHook(DependencyObject obj, bool value) => obj.SetValue(PasswordHookProperty, value);
        public static DependencyProperty PasswordHookProperty = DependencyProperty.RegisterAttached("PasswordHook", typeof(bool), typeof(PasswordBoxHelper),
                      new PropertyMetadata(OnPasswordHookChanged));

        private static void OnPasswordHookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            SetPassword(passwordBox, passwordBox.Password);
        }

        #endregion
    }
}