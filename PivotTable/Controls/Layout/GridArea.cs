using System.Windows;

namespace PivotTable.Controls.Layout
{
    internal struct GridArea
    {
        private readonly GridPosition _bottomRight;
        private readonly GridPosition _topLeft;

        public GridArea(GridPosition topLeft, GridPosition bottomRight)
        {
            _topLeft = topLeft;
            _bottomRight = bottomRight;
        }

        public GridArea AdjacentRight()
        {
            var nextTopLeft = _bottomRight.MoveToRowOf(_topLeft).MoveRight(1);
            var nextBottomRight = _bottomRight.MoveRight(1);
            return new GridArea(nextTopLeft, nextBottomRight);
        }

        public GridArea AdjacentDown()
        {
            var nextTopLeft = _bottomRight.MoveToColumnOf(_topLeft).MoveDown(1);
            var nextBottomRight = _bottomRight.MoveDown(1);
            return new GridArea(nextTopLeft, nextBottomRight);
        }

        public GridArea ExtendRight(int columnOffset)
        {
            var topLeft = _topLeft;
            var bottomRight = _bottomRight.MoveRight(columnOffset);
            return new GridArea(topLeft, bottomRight);
        }

        public GridArea ExtendDown(int rowOffset)
        {
            var topLeft = _topLeft;
            var bottomRight = _bottomRight.MoveDown(rowOffset);
            return new GridArea(topLeft, bottomRight);
        }

        public void Apply(UIElement measurementItem)
        {
            _topLeft.Apply(measurementItem);
            var span = _bottomRight - _topLeft;
            span.Apply(measurementItem);
        }
    }
}
