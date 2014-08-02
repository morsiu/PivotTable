using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PivotTable.Data;

namespace PivotTable.Controls.Data
{
    internal sealed class ItemFactory
    {
        private UIElement CreateItem(object measurement)
        {
            var content = ReferenceEquals(CubeDimension.AggregateMeasurement, measurement)
                ? "Σ"
                : measurement == null ? null : measurement.ToString();
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

        public UIElement CreateHeaderItem(object fact)
        {
            return CreateItem(fact);
        }

        public UIElement CreateFactItem(object fact)
        {
            return CreateItem(fact);
        }
    }
}
