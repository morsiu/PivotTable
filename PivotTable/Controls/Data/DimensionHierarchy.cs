using System.Collections.Generic;
using PivotTable.Data;

namespace PivotTable.Controls.Data
{
    public sealed class DimensionHierarchy
    {
        private readonly DimensionHierarchyDefinition _definition;
        private readonly IReadOnlyList<DimensionHierarchyKey> _keys;

        public DimensionHierarchy(DimensionHierarchyDefinition definition, IReadOnlyList<DimensionHierarchyKey> keys)
        {
            _definition = definition;
            _keys = keys;
        }

        public int LevelCount
        {
            get { return _definition.DimensionCount; }
        }

        public int KeyCount
        {
            get { return _keys.Count; }
        }

        public IReadOnlyList<DimensionHierarchyKey> Keys
        {
            get { return _keys; }
        }
    }
}
