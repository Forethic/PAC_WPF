using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Charts.Series;

namespace Charts.Charts
{
    public abstract class Chart : UserControl
    {
        public Rect PlotArea;
        public Canvas Canvas;
        public Point Max;
        public Point Min;
        public Point S;
        protected Border CurrentToolTip;
        protected List<TextBlock> AxisShapes = new List<TextBlock>();
        protected List<TextBlock> AxisLabels = new List<TextBlock>();
        protected double CurrentScale;
        protected ShapeHoverBehavior ShapeHoverBehavior;
        protected double LabelOffset;
        public List<HoverableShape> HoverableShapes = new List<HoverableShape>();
        private Point _PanOrigin;
        private bool _IsDragging;

        private readonly DispatcherTimer _ResizeTimer;
        private readonly DispatcherTimer _SeriesChangedTimer;

        static Chart()
        {
            Colors = new List<Color>
            {
                Color.FromRgb(41,127,184),
                Color.FromRgb(230,76,60),
                Color.FromRgb(240,195,15),
                Color.FromRgb(26,187,155),
                Color.FromRgb(87,213,140),
                Color.FromRgb(154,89,181),
                Color.FromRgb(92,109,126),
                Color.FromRgb(22,159,132),
                Color.FromRgb(39,173,96),
                Color.FromRgb(92,171,225),
                Color.FromRgb(141,68,172),
                Color.FromRgb(229,126,34),
                Color.FromRgb(210,84,0),
                Color.FromRgb(191,57,43)
            };
        }

        protected Chart()
        {
            var b = new Border { ClipToBounds = true };
            Canvas = new Canvas { RenderTransform = new TranslateTransform(0, 0) };
            b.Child = Canvas;
            Content = b;

            PointHoverColor = System.Windows.Media.Colors.White;

            // it requieres a background so it detect mouse down/up events
            Background = Brushes.Transparent;

            SizeChanged += Chart_OnSizeChanged;
            MouseWheel += MouseWheelOnRoll;
            MouseLeftButtonDown += MouseDownForPan;
            MouseLeftButtonUp += MouseUpForPan;
            MouseMove += MouseMoveForPan;

            _ResizeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100), };
            _ResizeTimer.Tick += (sender, e) =>
            {
                ClearAndPlot();
                _ResizeTimer.Stop();
            };

            _SeriesChangedTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            _SeriesChangedTimer.Tick += UpdateModifiedDataSeries;

            CurrentScale = 1;
        }

        abstract protected void Scale();
        abstract protected bool ScaleChanged { get; }

        public static List<Color> Colors { get; set; }

        public static readonly DependencyProperty ZoomingProperty = DependencyProperty.Register("Zooming", typeof(bool), typeof(Chart));
        /// <summary>
        /// Indicates weather user can zoom graph with mouse wheel.
        /// </summary>
        public bool Zooming
        {
            get => (bool)GetValue(ZoomingProperty);
            set => SetValue(ZoomingProperty, value);
        }

        public static readonly DependencyProperty HoverableProperty = DependencyProperty.Register("Hoverable", typeof(bool), typeof(Chart));
        /// <summary>
        /// Indicates weather points should be visible or not.
        /// </summary>
        public bool Hoverable
        {
            get => (bool)GetValue(HoverableProperty);
            set => SetValue(HoverableProperty, value);
        }

        public static readonly DependencyProperty PointHoverColorProperty = DependencyProperty.Register("PointHoverColor", typeof(Color), typeof(Chart));
        /// <summary>
        /// Indicates Point hover color.
        /// </summary>
        public Color PointHoverColor
        {
            get => (Color)GetValue(PointHoverColorProperty);
            set => SetValue(PointHoverColorProperty, value);
        }

        public static readonly DependencyProperty PrimaryAxisProperty = DependencyProperty.Register("PrimaryAxis", typeof(Axis), typeof(Chart));
        /// <summary>
        /// Configures Horizontal Axes and its labels.
        /// </summary>
        public Axis PrimaryAxis
        {
            get => (Axis)GetValue(PrimaryAxisProperty);
            set => SetValue(PrimaryAxisProperty, value);
        }

        public static readonly DependencyProperty SecondaryAxisProperty = DependencyProperty.Register("SecondaryAxis", typeof(Axis), typeof(Chart));
        /// <summary>
        /// Configures Vertical Axes and its labels.
        /// </summary>
        public Axis SecondaryAxis
        {
            get => (Axis)GetValue(SecondaryAxisProperty);
            set => SetValue(SecondaryAxisProperty, value);
        }

        public static readonly DependencyProperty DisableAnimationProperty = DependencyProperty.Register("DisableAnimation", typeof(bool), typeof(Chart));
        /// <summary>
        /// Indicates weather to show animation or not.
        /// </summary>
        public bool DisableAnimation
        {
            get => (bool)GetValue(DisableAnimationProperty);
            set => SetValue(DisableAnimationProperty, value);
        }

        public static readonly DependencyProperty SeriesProperty = DependencyProperty.Register("Series", typeof(IEnumerable<Serie>), typeof(Chart));
        /// <summary>
        /// Collection of series to be drawn.
        /// </summary>
        public ObservableCollection<Serie> Series
        {
            get => GetValue(SeriesProperty) as ObservableCollection<Serie>;
            set
            {
                SetValue(SeriesProperty, value);
                value.CollectionChanged += OnSeriesCollectionChanged;
                var index = 0;
                foreach (var serie in value)
                {
                    serie.ColorId = index;
                    serie.PrimaryValues.CollectionChanged += OnDataSeriesChanged;
                    serie.Chart = this;
                    index++;
                }
                ClearAndPlot();
            }
        }

        private void OnSeriesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateModifiedDataSeries(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseMoveForPan(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseUpForPan(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseDownForPan(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseWheelOnRoll(object sender, MouseWheelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Chart_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}