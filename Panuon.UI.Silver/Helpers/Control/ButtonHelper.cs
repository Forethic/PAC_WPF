using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Silver
{
    public class ButtonHelper
    {
        #region ButtonStyle

        public static ButtonStyle GetButtongStyle(DependencyObject obj) => (ButtonStyle)obj.GetValue(ButtongStyleProperty);
        public static void SetButtongStyle(DependencyObject obj, ButtonStyle value) => obj.SetValue(ButtongStyleProperty, value);
        public static DependencyProperty ButtongStyleProperty = DependencyProperty.RegisterAttached("ButtongStyle", typeof(ButtonStyle), typeof(ButtonHelper), new PropertyMetadata(ButtonStyle.Standard));

        #endregion

        #region ClickStyle

        public static ClickStyle GetClickStyle(DependencyObject obj) => (ClickStyle)obj.GetValue(ClickStyleProperty);
        public static void SetClickStyle(DependencyObject obj, ClickStyle value) => obj.SetValue(ClickStyleProperty, value);
        public static DependencyProperty ClickStyleProperty = DependencyProperty.RegisterAttached("ClickStyle", typeof(ClickStyle), typeof(ButtonHelper), new PropertyMetadata(ClickStyle.None));

        #endregion

        #region HorverBrush

        public static Brush GetHorverBrush(DependencyObject obj) => (Brush)obj.GetValue(HorverBrushProperty);
        public static void SetHorverBrush(DependencyObject obj, Brush value) => obj.SetValue(HorverBrushProperty, value);
        public static DependencyProperty HorverBrushProperty = DependencyProperty.RegisterAttached("HorverBrush", typeof(Brush), typeof(ButtonHelper));

        #endregion

        #region CornerRadius

        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ButtonHelper));

        #endregion

        #region IsWaiting

        public static bool GetIsWaiting(DependencyObject obj) => (bool)obj.GetValue(IsWaitingProperty);
        public static void SetIsWaiting(DependencyObject obj, bool value) => obj.SetValue(IsWaitingProperty, value);
        public static DependencyProperty IsWaitingProperty = DependencyProperty.RegisterAttached("IsWaiting", typeof(bool), typeof(ButtonHelper));

        #endregion

        #region WaitingContent

        public static object GetWaitingContent(DependencyObject obj) => (object)obj.GetValue(WaitingContentProperty);
        public static void SetWaitingContent(DependencyObject obj, object value) => obj.SetValue(WaitingContentProperty, value);
        public static DependencyProperty WaitingContentProperty = DependencyProperty.RegisterAttached("WaitingContent", typeof(object), typeof(ButtonHelper));

        #endregion

        #region Icon

        public static object GetIcon(DependencyObject obj) => (object)obj.GetValue(IconProperty);
        public static void SetIcon(DependencyObject obj, object value) => obj.SetValue(IconProperty, value);
        public static DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(object), typeof(ButtonHelper));

        #endregion

        #region ClickCoverBrush

        public static Brush GetClickCoverBrush(DependencyObject obj) => (Brush)obj.GetValue(ClickCoverBrushProperty);
        public static void SetClickCoverBrush(DependencyObject obj, Brush value) => obj.SetValue(ClickCoverBrushProperty, value);
        public static DependencyProperty ClickCoverBrushProperty = DependencyProperty.RegisterAttached("ClickCoverBrush", typeof(Brush), typeof(ButtonHelper));

        #endregion
    }
}