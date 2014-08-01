using System;
using System.Collections.Generic;
using System.Linq;

namespace PivotTable.Data
{
    public sealed class CubeBuilder
    {
        private readonly IReadOnlyList<CubeDimensionBuilder> _dimensions;
        private readonly FactTableBuilder _factTable;

        public CubeBuilder(params object[] dimensionNames)
        {
            _dimensions = dimensionNames.Select(dimensionName => new CubeDimensionBuilder(dimensionName)).ToArray();
            _factTable = new FactTableBuilder(new FactKeyBuilder(_dimensions));
        }

        public CubeBuilder AddFact(object fact, params object[] measurements)
        {
            _factTable.AddFact(fact, measurements);
            return this;
        }

        public Cube Build()
        {
            return new Cube(
                _dimensions.Select(dimension => dimension.Build()).ToArray(),
                _factTable.BuildFactKeys(),
                _factTable.BuildFacts());
        }

        private sealed class FactTableBuilder
        {
            private readonly FactKeyBuilder _factKeyBuilder;
            private readonly List<int[]> _factKeys;
            private readonly List<object> _facts;

            public FactTableBuilder(FactKeyBuilder factKeyBuilder)
            {
                _factKeys = new List<int[]>();
                _facts = new List<object>();
                _factKeyBuilder = factKeyBuilder;
            }

            public void AddFact(object fact, params object[] measurements)
            {
                var factKey = _factKeyBuilder.CreateFactKey(measurements);
                _factKeys.Add(factKey);
                _facts.Add(fact);
            }

            public IReadOnlyList<int[]> BuildFactKeys()
            {
                return _factKeys;
            }

            public IReadOnlyList<object> BuildFacts()
            {
                return _facts;
            }
        }

        private sealed class FactKeyBuilder
        {
            private readonly IReadOnlyList<CubeDimensionBuilder> _dimensions;

            public FactKeyBuilder(IReadOnlyList<CubeDimensionBuilder> dimensions)
            {
                _dimensions = dimensions;
            }

            public int[] CreateFactKey(IReadOnlyList<object> measurements)
            {
                ValidateDimensionCount(measurements);
                var factKey = new int[_dimensions.Count];
                for (var dimensionIndex = 0; dimensionIndex < _dimensions.Count; ++dimensionIndex)
                {
                    var dimension = _dimensions[dimensionIndex];
                    var measurement = measurements[dimensionIndex];
                    var measurementIndex = GetMeasurementIndex(measurement, dimension);
                    factKey[dimensionIndex] = measurementIndex;
                }
                return factKey;
            }

            private int GetMeasurementIndex(object measurement, CubeDimensionBuilder dimension)
            {
                if (ReferenceEquals(measurement, CubeDimension.UnspecifiedMeasurement))
                {
                    return CubeDimension.UnspecifiedMeasurementIndex;
                }
                return dimension.AddMeasurement(measurement);
            }

            private void ValidateDimensionCount(IReadOnlyList<object> measurements)
            {
                if (measurements.Count != _dimensions.Count)
                {
                    throw new ArgumentException("Number of measurements is different from number of dimensions.");
                }
            }
        }

        private sealed class CubeDimensionBuilder
        {
            private readonly List<object> _measurements;
            private readonly object _name;

            public CubeDimensionBuilder(object name)
            {
                _name = name;
                _measurements = new List<object>();
            }

            public int AddMeasurement(object measurement)
            {
                var measurementIndex = _measurements.IndexOf(measurement);
                if (measurementIndex != -1)
                {
                    return measurementIndex;
                }
                _measurements.Add(measurement);
                return _measurements.Count - 1;
            }

            public CubeDimension Build()
            {
                return new CubeDimension(_name, _measurements);
            }
        }
    }
}
