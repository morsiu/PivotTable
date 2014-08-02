using System.Collections.Generic;

namespace PivotTable.Data
{
    public sealed class CubeDimension
    {
        private readonly IReadOnlyList<object> _measurements;
        private readonly object _name;

        public static readonly object UnspecifiedMeasurement = new object();
        public const int UnspecifiedMeasurementIndex = -1;
        public static readonly object AggregateMeasurement = new object();
        public const int AggregateMeasurementIndex = -2;

        public CubeDimension(
            object name,
            IReadOnlyList<object> measurements)
        {
            _name = name;
            _measurements = measurements;
        }

        public IReadOnlyList<object> Measurements
        {
            get { return _measurements; }
        }

        public object Name
        {
            get { return _name; }
        }
    }
}
