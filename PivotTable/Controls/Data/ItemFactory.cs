using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PivotTable.Data;

namespace PivotTable.Controls.Data
{
    internal sealed class ItemFactory
    {
        public UIElement CreateHeaderItem(object measurement)
        {
            var content = ReferenceEquals(CubeDimension.AggregateMeasurement, measurement)
                ? "Σ"
                : measurement.ToString();
            var border = new Border
            {
                Child =
                    new TextBlock
                    {
                        Text = content,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        TextAlignment = TextAlignment.Center,
                    },
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            return border;
        }
    }
}
