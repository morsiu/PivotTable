using System;
using System.Collections;
using System.Collections.Generic;

namespace PivotTable.Data
{
    public sealed class DimensionHierarchyKey : IComparable<DimensionHierarchyKey>
    {
        private readonly FactKey _factKey;
        private readonly IReadOnlyList<object> _measurements;

        public DimensionHierarchyKey(FactKey factKey, IReadOnlyList<object> measurements)
        {
            _factKey = factKey;
            _measurements = measurements;
        }

        public int CompareTo(DimensionHierarchyKey other)
        {
            if (_measurements.Count != other._measurements.Count)
            {
                throw new ArgumentException("Other key has different number of measurements.");
            }
            for (int dimensionIndex = 0; dimensionIndex < _measurements.Count; ++dimensionIndex)
            {
                var measurement = _measurements[dimensionIndex];
                var otherMeasurement = other._measurements[dimensionIndex];
                if (IsAggregate(measurement))
                {
                    return IsAggregate(otherMeasurement) ? 0 : 1;
                }
                if (IsAggregate(otherMeasurement))
                {
                    return IsAggregate(measurement) ? 1 : 0;
                }
                var result = Comparer.Default.Compare(measurement, otherMeasurement);
                if (result == 0) continue;
                return result;
            }
            return 0;
        }

        private static bool IsAggregate(object measurement)
        {
            return ReferenceEquals(measurement, CubeDimension.AggregateMeasurement);
        }
    }
}