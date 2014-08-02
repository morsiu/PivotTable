using System.Windows;
using System.Windows.Controls;

namespace PivotTable.Controls.Layout
{
    internal struct GridSpan
    {
        private readonly int _columns;
        private readonly int _rows;

        public GridSpan(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public void Apply(UIElement element)
        {
            Grid.SetRowSpan(element, _rows);
            Grid.SetColumnSpan(element, _columns);
        }
    }
}
