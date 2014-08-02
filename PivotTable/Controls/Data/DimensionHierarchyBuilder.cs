using System.Collections.Generic;
using System.Linq;
using PivotTable.Data;

namespace PivotTable.Controls.Data
{
    public sealed class DimensionHierarchyBuilder
    {
        private readonly Cube _cube;
        private readonly DimensionHierarchyDefinition _definition;

        public DimensionHierarchyBuilder(Cube cube, DimensionHierarchyDefinition definition)
        {
            _cube = cube;
            _definition = definition;
        }

        public DimensionHierarchy Build()
        {
            var keys = BuildKeys();
            return new DimensionHierarchy(_definition, keys);
        }

        private IReadOnlyList<DimensionHierarchyKey> BuildKeys()
        {
            var filteredFactKeys = GetFilteredFactKeys();
            var hierarchyKeys = GetHierarchyKeys(filteredFactKeys);
            return hierarchyKeys.ToList();
        }

        private SortedSet<DimensionHierarchyKey> GetHierarchyKeys(HashSet<FactKey> filteredFactKeys)
        {
            var hierarchyKeys = new SortedSet<DimensionHierarchyKey>();
            foreach (var filteredFactKey in filteredFactKeys)
            {
                var measurements = filteredFactKey.GetMeasurements(_cube, _definition.Dimensions);
                var hierarchyKey = new DimensionHierarchyKey(filteredFactKey, measurements);
                hierarchyKeys.Add(hierarchyKey);
            }
            return hierarchyKeys;
        }

        private HashSet<FactKey> GetFilteredFactKeys()
        {
            var filteredFactKeys = new HashSet<FactKey>();
            foreach (var factKey in _cube.FactKeys)
            {
                var filteredFactKey = _definition.FilterKey(factKey);
                filteredFactKeys.Add(filteredFactKey);
            }
            return filteredFactKeys;
        }
    }
}
