using System.Windows;
using System.Windows.Controls;
using PivotTable.Controls.Data;
using PivotTable.Controls.Layout;

namespace PivotTable.Controls.Painters
{
    internal sealed class HeaderPainter
    {
        private readonly Grid _grid;
        private readonly ItemFactory _itemFactory;

        public HeaderPainter(Grid grid, ItemFactory itemFactory)
        {
            _grid = grid;
            _itemFactory = itemFactory;
        }

        public void Paint(DimensionHierarchy hierarchy, GridPosition startPosition, GridOrientation orientation)
        {
            var placement = new ZigZagPlacement(startPosition, orientation);
            for (int level = 0; level < hierarchy.LevelCount; ++level)
            {
                var previousMeasurement = new object();
                var currentItem = default(UIElement);
                for (int keyIndex = 0; keyIndex < hierarchy.KeyCount; ++keyIndex)
                {
                    var measurement = hierarchy.Keys[keyIndex].Measurements[level];
                    if (!Equals(previousMeasurement, measurement))
                    {
                        currentItem = _itemFactory.CreateHeaderItem(measurement);
                        _grid.Children.Add(currentItem);
                        if (keyIndex > 0)
                        {
                            placement.NextSlot();
                        }
                        placement.ApplySlot(currentItem);
                    }
                    else
                    {
                        placement.ExtendSlot();
                        placement.ApplySlot(currentItem);
                    }
                    previousMeasurement = measurement;
                }
                placement.NextLevel();
            }
        }
    }
}
