using System.Windows.Controls;
using PivotTable.Controls.Data;

namespace PivotTable.Controls.Layout
{
    internal sealed class GridSpaceAllocator
    {
        private readonly Grid _grid;
        private readonly DimensionHierarchy _horizontalHierarchy;
        private readonly DimensionHierarchy _verticalHierarchy;

        public GridSpaceAllocator(Grid grid, DimensionHierarchy horizontalHierarchy, DimensionHierarchy verticalHierarchy)
        {
            _grid = grid;
            _horizontalHierarchy = horizontalHierarchy;
            _verticalHierarchy = verticalHierarchy;
        }

        public GridPosition HorizontalHierarchyPosition
        {
            get { return new GridPosition(0, _verticalHierarchy.LevelCount); }
        }

        public GridPosition VerticalHierarchyPosition
        {
            get { return new GridPosition(_horizontalHierarchy.LevelCount, 0); }
        }

        public void AllocateSpace()
        {
            AllocateRows();
            AllocateColumns();
        }

        private void AllocateColumns()
        {
            var columnCount = _verticalHierarchy.LevelCount + _horizontalHierarchy.KeyCount;
            _grid.ColumnDefinitions.Clear();
            for (var index = 0; index < columnCount; ++index)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private int AllocateRows()
        {
            var rowCount = _horizontalHierarchy.LevelCount + _verticalHierarchy.KeyCount;
            _grid.RowDefinitions.Clear();
            for (var index = 0; index < rowCount; ++index)
            {
                _grid.RowDefinitions.Add(new RowDefinition());
            }
            return rowCount;
        }
    }
}
