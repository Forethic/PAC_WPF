using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using Charts.Charts;
using Charts.Series;

namespace Charts
{
    public static class Methods
    {
        /// <summary>
        /// Scales a graph value to screen according to an axis.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="axis"></param>
        /// <param name="chart"></param>
        /// <returns></returns>
        public static double ToPlotArea(double value, AxisTags axis, Chart chart)
        {
            var p1 = axis == AxisTags.X
                ? new Point(chart.Max.X, chart.PlotArea.Width + chart.PlotArea.X)
                : new Point(chart.Max.Y, chart.PlotArea.Y);
            var p2 = axis == AxisTags.Y
                ? new Point(chart.Min.X, chart.PlotArea.X)
                : new Point(chart.Min.Y, chart.PlotArea.Height + chart.PlotArea.Y);
            var m = (p2.Y - p1.Y) / (p2.X - p1.X);
            return m * (value - p1.X) + p1.Y;
        }

        /// <summary>
        /// Scales a graph point to screen
        /// </summary>
        /// <param name="value"></param>
        /// <param name="chart"></param>
        /// <returns></returns>
        public static Point ToPlotArea(Point value, Chart chart)
        {
            return new Point(ToPlotArea(value.X, AxisTags.X, chart), ToPlotArea(value.Y, AxisTags.Y, chart));
        }
    }

    public class Axis
    {

    }

    public class HoverableShape
    {
        /// <summary>
        /// Point of this area
        /// </summary>
        public Point Value { get; set; }
        /// <summary>
        /// Shape that fires hover
        /// </summary>
        public Shape Shape { get; set; }
        /// <summary>
        /// Shape that that changes style on hover
        /// </summary>
        public Shape Target { get; set; }
        /// <summary>
        /// serie that contains thos point
        /// </summary>
        public Serie Serie { get; set; }
        /// <summary>
        /// Point label
        /// </summary>
        public string Label { get; set; }
    }

    public enum AxisTags
    {
        X, Y
    }

    public enum ShapeHoverBehavior
    {
        Dot, Shape
    }
}