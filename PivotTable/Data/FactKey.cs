using System;
using System.Collections;
using System.Collections.Generic;

namespace PivotTable.Data
{
    public struct FactKey
    {
        private readonly IReadOnlyList<int> _measurements;

        public FactKey(IReadOnlyList<int> measurements)
        {
            _measurements = measurements;
        }

        public bool Equals(FactKey other)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(_measurements, other._measurements);
        }

        public object[] GetMeasurements(Cube _cube)
        {
            var measurements = new List<object>();
            for (int dimensionIndex = 0; dimensionIndex < _measurements.Count; ++dimensionIndex)
            {
                var measurementIndex = _measurements[dimensionIndex];
                if (measurementIndex != CubeDimension.UnspecifiedMeasurementIndex)
                {
                    var measurement =
                        measurementIndex != CubeDimension.AggregateMeasurementIndex
                        ? _cube.Dimensions[dimensionIndex].Measurements[measurementIndex]
                        : CubeDimension.AggregateMeasurement;
                    measurements.Add(measurement);
                }
            }
            return measurements.ToArray();
        }

        public FactKey Merge(FactKey other)
        {
            if (_measurements.Count != other._measurements.Count)
            {
                throw new ArgumentException("Cannot merge keys with different measurement count.");
            }
            var mergedKey = new int[_measurements.Count];
            for (var dimensionIndex = 0; dimensionIndex < _measurements.Count; ++dimensionIndex)
            {
                mergedKey[dimensionIndex] = MergeMeasurements(_measurements[dimensionIndex], other._measurements[dimensionIndex]);
            }
            return new FactKey(mergedKey);
        }

        private int MergeMeasurements(int x, int y)
        {
            if (x == CubeDimension.UnspecifiedMeasurementIndex) return y;
            if (y == CubeDimension.UnspecifiedMeasurementIndex) return x;
            if (x == y) return x;
            return CubeDimension.UnspecifiedMeasurementIndex;
        }
    }
}
