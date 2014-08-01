using System.Collections;
using System.Collections.Generic;

namespace PivotTable.Data
{
    public sealed class FactKey
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
    }
}
