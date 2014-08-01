using System.Collections.Generic;

namespace PivotTable.Data
{
    public sealed class Cube
    {
        private readonly IReadOnlyList<int[]> _factKeys;
        private readonly IReadOnlyList<object> _facts;
        private readonly IReadOnlyList<CubeDimension> _dimensions;

        public Cube(
            IReadOnlyList<CubeDimension> dimensions,
            IReadOnlyList<int[]> factKeys,
            IReadOnlyList<object> facts)
        {
            _dimensions = dimensions;
            _factKeys = factKeys;
            _facts = facts;
        }

        public IReadOnlyList<int[]> FactKeys
        {
            get { return _factKeys; }
        }

        public IReadOnlyList<CubeDimension> Dimensions
        {
            get { return _dimensions; }
        }
    }
}
