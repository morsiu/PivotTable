using System;
using System.Windows;
using System.Windows.Controls;

namespace PivotTable.Controls.Layout
{
    internal struct GridPosition
    {
        private readonly int _column;
        private readonly int _row;

        public GridPosition(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public GridPosition MoveDown(int rowOffset)
        {
            return new GridPosition(_row + rowOffset, _column);
        }

        public GridPosition MoveToRowOf(GridPosition other)
        {
            return new GridPosition(other._row, _column);
        }

        public GridPosition MoveToColumnOf(GridPosition other)
        {
            return new GridPosition(_row, other._column);
        }

        public GridPosition MoveRight(int columnOffset)
        {
            return new GridPosition(_row, _column + columnOffset);
        }

        public void Apply(UIElement measurementItem)
        {
            Grid.SetColumn(measurementItem, _column);
            Grid.SetRow(measurementItem, _row);
        }

        public static GridSpan operator -(GridPosition x, GridPosition y)
        {
            return new GridSpan(
                Math.Abs(x._row - y._row) + 1,
                Math.Abs(x._column - y._column) + 1
            );
        }
    }
}
