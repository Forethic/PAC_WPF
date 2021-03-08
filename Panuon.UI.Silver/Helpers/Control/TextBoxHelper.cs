using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Silver
{
    public class TextBoxHelper
    {
        #region TextBoxStyle

        public static TextBoxStyle GetTextBoxStyle(DependencyObject obj) => (TextBoxStyle)obj.GetValue(TextBoxStyleProperty);
        public static void SetTextBoxStyle(DependencyObject obj, TextBoxStyle value) => obj.SetValue(TextBoxStyleProperty, value);
        public static DependencyProperty TextBoxStyleProperty = DependencyProperty.RegisterAttached("TextBoxStyle", typeof(TextBoxStyle), typeof(TextBoxHelper), new PropertyMetadata(TextBoxStyle.Standard));

        #endregion

        #region FocusedBorderBrush

        public static Brush GetFocusedBorderBrush(DependencyObject obj) => (Brush)obj.GetValue(FocusedBorderBrushProperty);
        public static void SetFocusedBorderBrush(DependencyObject obj, Brush value) => obj.SetValue(FocusedBorderBrushProperty, value);
        public static DependencyProperty FocusedBorderBrushProperty = DependencyProperty.RegisterAttached("FocusedBorderBrush", typeof(Brush), typeof(TextBoxHelper));

        #endregion

        #region FocusedShadowColor

        public static Color GetFocusedShadowColor(DependencyObject obj) => (Color)obj.GetValue(FocusedShadowColorProperty);
        public static void SetFocusedShadowColor(DependencyObject obj, Color value) => obj.SetValue(FocusedShadowColorProperty, value);
        public static DependencyProperty FocusedShadowColorProperty = DependencyProperty.RegisterAttached("FocusedShadowColor", typeof(Color), typeof(TextBoxHelper));

        #endregion

        #region CornerRadius

        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(TextBoxHelper));

        #endregion

        #region Watermark

        public static string GetWatermark(DependencyObject obj) => (string)obj.GetValue(WatermarkProperty);
        public static void SetWatermark(DependencyObject obj, string value) => obj.SetValue(WatermarkProperty, value);
        public static DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(TextBoxHelper));

        #endregion

        #region Icon

        public static object GetIcon(DependencyObject obj) => (object)obj.GetValue(IconProperty);
        public static void SetIcon(DependencyObject obj, object value) => obj.SetValue(IconProperty, value);
        public static DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(object), typeof(TextBoxHelper));

        #endregion
    }
}