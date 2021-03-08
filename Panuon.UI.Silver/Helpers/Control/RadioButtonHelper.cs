﻿using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Silver
{
    public class RadioButtonHelper
    {
        #region RadioButtonStyle

        public static RadioButtonStyle GetRadioButtonStyle(DependencyObject obj) => (RadioButtonStyle)obj.GetValue(RadioButtonStyleProperty);
        public static void SetRadioButtonStyle(DependencyObject obj, RadioButtonStyle value) => obj.SetValue(RadioButtonStyleProperty, value);
        public static DependencyProperty RadioButtonStyleProperty = DependencyProperty.RegisterAttached("RadioButtonStyle", typeof(RadioButtonStyle), typeof(RadioButtonHelper));

        #endregion

        #region CheckedBackground

        public static Brush GetCheckedBackground(DependencyObject obj) => (Brush)obj.GetValue(CheckedBackgroundProperty);
        public static void SetCheckedBackground(DependencyObject obj, Brush value) => obj.SetValue(CheckedBackgroundProperty, value);
        public static DependencyProperty CheckedBackgroundProperty = DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(RadioButtonHelper));

        #endregion

        #region GlyphBrush

        public static Brush GetGlyphBrush(DependencyObject obj) => (Brush)obj.GetValue(GlyphBrushProperty);
        public static void SetGlyphBrush(DependencyObject obj, Brush value) => obj.SetValue(GlyphBrushProperty, value);
        public static DependencyProperty GlyphBrushProperty = DependencyProperty.RegisterAttached("GlyphBrush", typeof(Brush), typeof(RadioButtonHelper));

        #endregion

        #region CheckedGlyphBrush

        public static Brush GetCheckedGlyphBrush(DependencyObject obj) => (Brush)obj.GetValue(CheckedGlyphBrushProperty);
        public static void SetCheckedGlyphBrush(DependencyObject obj, Brush value) => obj.SetValue(CheckedGlyphBrushProperty, value);
        public static DependencyProperty CheckedGlyphBrushProperty = DependencyProperty.RegisterAttached("CheckedGlyphBrush", typeof(Brush), typeof(RadioButtonHelper));

        #endregion

        #region BoxHeight

        public static double GetBoxHeight(DependencyObject obj) => (double)obj.GetValue(BoxHeightProperty);
        public static void SetBoxHeight(DependencyObject obj, double value) => obj.SetValue(BoxHeightProperty, value);
        public static DependencyProperty BoxHeightProperty = DependencyProperty.RegisterAttached("BoxHeight", typeof(double), typeof(RadioButtonHelper));

        #endregion

        #region BoxWidth

        public static double GetBoxWidth(DependencyObject obj) => (double)obj.GetValue(BoxWidthProperty);
        public static void SetBoxWidth(DependencyObject obj, double value) => obj.SetValue(BoxWidthProperty, value);
        public static DependencyProperty BoxWidthProperty = DependencyProperty.RegisterAttached("BoxWidth", typeof(double), typeof(RadioButtonHelper));

        #endregion

        #region CornerRadius

        public static double GetCornerRadius(DependencyObject obj) => (double)obj.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject obj, double value) => obj.SetValue(CornerRadiusProperty, value);
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(double), typeof(RadioButtonHelper));

        #endregion

        #region CheckedContent

        public static object GetCheckedContent(DependencyObject obj) => (object)obj.GetValue(CheckedContentProperty);
        public static void SetCheckedContent(DependencyObject obj, object value) => obj.SetValue(CheckedContentProperty, value);
        public static DependencyProperty CheckedContentProperty = DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(RadioButtonHelper));

        #endregion
    }
}