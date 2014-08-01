using System.Collections.Generic;

namespace PivotTable.Data
{
    public sealed class DimensionHierarchy
    {
        private readonly IReadOnlyList<DimensionHierarchyKey> _keys;

        public DimensionHierarchy(IReadOnlyList<DimensionHierarchyKey> keys)
        {
            _keys = keys;
        }
    }
}
