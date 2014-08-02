using System.Windows;

namespace PivotTable.Controls.Layout
{
    internal sealed class ZigZagPlacement
    {
        private readonly GridOrientation _orientation;
        private GridPosition _levelStartPosition;
        private GridArea _currentSlot;

        public ZigZagPlacement(GridPosition startPosition, GridOrientation orientation)
        {
            _orientation = orientation;
            _levelStartPosition = startPosition;
            _currentSlot = new GridArea(startPosition, startPosition);
        }

        public void NextSlot()
        {
            _currentSlot = _orientation == GridOrientation.Horizontal
                ? _currentSlot.AdjacentRight()
                : _currentSlot.AdjacentDown();
        }

        public void ExtendSlot()
        {
            _currentSlot = _orientation == GridOrientation.Horizontal
                ? _currentSlot.ExtendRight(1)
                : _currentSlot.ExtendDown(1);
        }

        public void ApplySlot(UIElement element)
        {
            _currentSlot.Apply(element);
        }

        public void NextLevel()
        {
            _levelStartPosition = _orientation == GridOrientation.Horizontal
                ? _levelStartPosition.MoveDown(1)
                : _levelStartPosition.MoveRight(1);
            _currentSlot = new GridArea(_levelStartPosition, _levelStartPosition);
        }
    }
}
