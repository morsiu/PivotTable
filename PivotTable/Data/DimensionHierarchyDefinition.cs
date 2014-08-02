using System;
using System.Collections.Generic;
using System.Linq;

namespace PivotTable.Data
{
    public sealed class DimensionHierarchyDefinition
    {
        private readonly IReadOnlyList<int> _dimensions;

        public int DimensionCount
        {
            get { return _dimensions.Count; }
        }

        public DimensionHierarchyDefinition(IReadOnlyList<int> dimensions)
        {
            _dimensions = dimensions;
        }

        public DimensionHierarchyDefinition(IReadOnlyList<CubeDimension> dimensions, params object[] dimensionNames)
        {
            var dimensionIndexes = new List<int>();
            foreach (var dimensionName in dimensionNames)
            {
                var dimensionIndex = FindDimension(dimensionName, dimensions);
                dimensionIndexes.Add(dimensionIndex);
            }
            _dimensions = dimensionIndexes;
        }

        private int FindDimension(object dimensionName, IReadOnlyList<CubeDimension> dimensions)
        {
            for (int dimensionIndex = 0; dimensionIndex < dimensions.Count; ++dimensionIndex)
            {
                if (Equals(dimensionName, dimensions[dimensionIndex].Name))
                {
                    return dimensionIndex;
                }
            }
            throw new ArgumentException(string.Format("There is no dimension with name {0}.", dimensionName));
        }

        public FactKey FilterKey(int[] factKey)
        {
            var filteredFactKey = new int[factKey.Length];
            for (int dimensionIndex = 0; dimensionIndex < factKey.Length; ++dimensionIndex)
            {
                var measurementIndex = factKey[dimensionIndex];
                var filteredMeasurementIndex =
                    _dimensions.Contains(dimensionIndex)
                    ? measurementIndex
                    : CubeDimension.UnspecifiedMeasurementIndex;
                filteredFactKey[dimensionIndex] = filteredMeasurementIndex;
            }
            return new FactKey(filteredFactKey);
        }
    }
}
