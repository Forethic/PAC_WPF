using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Charts.Charts;

namespace Charts.Series
{
    public abstract class Serie
    {
        private Color? _Color;
        protected List<Shape> Shapes = new List<Shape>();
        private Chart _Chart;

        protected Serie()
        {
            StrokeThickness = 2.5;
            PointRadius = 4;
            ColorId = -1;
        }

        public abstract ObservableCollection<double> PrimaryValues { get; set; }
        public abstract void Plot(bool animate = true);
        public virtual void Erase()
        {
            foreach (var s in Shapes)
            {
                Chart.Canvas.Children.Remove(s);
            }
            Shapes.Clear();

            var hoverableShapes = Chart.HoverableShapes.Where(x => x.Serie == this).ToList();
            foreach (var hs in hoverableShapes)
            {
                Chart.Canvas.Children.Remove(hs.Shape);
                Chart.HoverableShapes.Remove(hs);
            }
        }

        public Chart Chart
        {
            get => _Chart;
            set
            {
                if (_Chart != null) throw new Exception("can't set chart property twice.");
                _Chart = value;
            }
        }

        public int ColorId { get; set; }
        public double StrokeThickness { get; set; }
        public double PointRadius { get; set; }
        public Color Color
        {
            get
            {
                if (_Color != null) { return _Color.Value; }
                return Chart.Colors[
                    (int)(ColorId - Chart.Colors.Count * Math.Truncate(ColorId / (decimal)Chart.Colors.Count))];
            }
            set => _Color = value;
        }

        protected Color GetColorByIndex(int index)
        {
            return Chart.Colors[
                (int)(index - Chart.Colors.Count * Math.Truncate(index / (decimal)Chart.Colors.Count))];
        }

        /// <summary>
        /// Scales a graph value to screen according to an axis.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        protected double ToPlotArea(double value, AxisTags axis)
        {
            return Methods.ToPlotArea(value, axis, Chart);
        }

        /// <summary>
        /// Scales a graph point to screen
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected Point ToPlotArea(Point value)
        {
            return new Point(ToPlotArea(value.X, AxisTags.X), ToPlotArea(value.Y, AxisTags.Y));
        }

    }
}