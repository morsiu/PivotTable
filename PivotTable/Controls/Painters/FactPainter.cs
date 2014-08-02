using System.Windows.Controls;
using PivotTable.Controls.Data;
using PivotTable.Controls.Layout;
using PivotTable.Data;

namespace PivotTable.Controls.Painters
{
    internal sealed class FactPainter
    {
        private readonly Cube _cube;
        private readonly Grid _grid;
        private readonly ItemFactory _itemFactory;

        public FactPainter(Grid grid, Cube cube, ItemFactory itemFactory)
        {
            _grid = grid;
            _cube = cube;
            _itemFactory = itemFactory;
        }

        public void Paint(
            DimensionHierarchy horizontalHierarchy,
            DimensionHierarchy verticalHierarchy,
            GridPosition startPosition)
        {
            var placement = new ZigZagPlacement(startPosition, GridOrientation.Horizontal);
            foreach (var verticalFactKey in verticalHierarchy.Keys)
            {
                foreach (var horizontalFactKey in horizontalHierarchy.Keys)
                {
                    var factKey = horizontalFactKey.Merge(verticalFactKey);
                    var fact = FindFact(factKey);
                    var factItem = _itemFactory.CreateFactItem(fact);
                    _grid.Children.Add(factItem);
                    placement.ApplySlot(factItem);
                    placement.NextSlot();
                }
                placement.NextLevel();
            }
        }

        private object FindFact(FactKey factKey)
        {
            for (var factIndex = 0; factIndex < _cube.FactKeys.Count; ++factIndex)
            {
                var otherFactKey = new FactKey(_cube.FactKeys[factIndex]);
                if (factKey.Equals(otherFactKey))
                {
                    return _cube.Facts[factIndex];
                }
            }
            return null;
        }
    }
}
