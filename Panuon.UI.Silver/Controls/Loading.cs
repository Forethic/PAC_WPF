using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Panuon.UI.Silver
{
    public class Loading : Control
    {
        #region Construtor

        static Loading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
        }

        #endregion

        #region Property

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public static DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(Loading));

        public Brush StrokeCover
        {
            get => (Brush)GetValue(StrokeCoverProperty);
            set => SetValue(StrokeCoverProperty, value);
        }

        public static DependencyProperty StrokeCoverProperty = DependencyProperty.Register("StrokeCover", typeof(Brush), typeof(Loading));

        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        public static DependencyProperty IsRunningProperty = DependencyProperty.Register("IsRunning", typeof(bool), typeof(Loading));

        public LoadingStyle LoadingStyle
        {
            get => (LoadingStyle)GetValue(LoadingStyleProperty);
            set => SetValue(LoadingStyleProperty, value);
        }

        public static DependencyProperty LoadingStyleProperty = DependencyProperty.Register("LoadingStyle", typeof(LoadingStyle), typeof(Loading));

        #endregion
    }
}