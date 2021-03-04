using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Silver
{
    public class SliderHelper
    {
        #region SliderStyle

        public static SliderStyle GetSliderStyle(DependencyObject obj) => (SliderStyle)obj.GetValue(SliderStyleProperty);
        public static void SetSliderStyle(DependencyObject obj, SliderStyle value) => obj.SetValue(SliderStyleProperty, value);
        public static DependencyProperty SliderStyleProperty = DependencyProperty.RegisterAttached("SliderStyle", typeof(SliderStyle), typeof(SliderHelper));

        #endregion

        #region ThemeBrush

        public static Brush GetThemBrush(DependencyObject obj) => (Brush)obj.GetValue(ThemBrushProperty);
        public static void SetThemBrush(DependencyObject obj, Brush value) => obj.SetValue(ThemBrushProperty, value);
        public static DependencyProperty ThemBrushProperty = DependencyProperty.RegisterAttached("ThemBrush", typeof(Brush), typeof(SliderHelper), new PropertyMetadata());

        #endregion

        #region TrackBrush

        public static Brush GetTrackBrush(DependencyObject obj) => (Brush)obj.GetValue(TrackBrushProperty);
        public static void SetTrackBrush(DependencyObject obj, Brush value) => obj.SetValue(TrackBrushProperty, value);
        public static DependencyProperty TrackBrushProperty = DependencyProperty.RegisterAttached("TrackBrush", typeof(Brush), typeof(SliderHelper));

        #endregion

        #region IsTickValueVisible

        public static bool GetIsTickValueVisible(DependencyObject obj) => (bool)obj.GetValue(IsTickValueVisibleProperty);
        public static void SetIsTickValueVisible(DependencyObject obj, bool value) => obj.SetValue(IsTickValueVisibleProperty, value);
        public static DependencyProperty IsTickValueVisibleProperty = DependencyProperty.RegisterAttached("IsTickValueVisible", typeof(bool), typeof(SliderHelper));

        #endregion
    }
}