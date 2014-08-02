using PivotTable.Data;

namespace PivotTable.Sample.Data
{
    public static class SampleCubes
    {
        private static readonly object Sum = CubeDimension.AggregateMeasurement;

        public static Cube Sales
        {
            get
            {
                return new CubeBuilder("Year", "Quarter", "Region", "ProductCategory")
                    .AddFact(1000, 2014, 1, "Europe", "Shirts")
                    .AddFact(100, 2014, 1, "Europe", "Trousers")
                    .AddFact(1000, 2014, 2, "Europe", "Shirts")
                    .AddFact(100, 2014, 2, "Europe", "Trousers")
                    .AddFact(1000, 2014, 1, "Asia", "Shirts")
                    .AddFact(100, 2014, 1, "Asia", "Trousers")
                    .AddFact(1000, 2014, 2, "Asia", "Shirts")
                    .AddFact(100, 2014, 2, "Asia", "Trousers")

                    .AddFact(2200, 2014, 1, Sum, Sum)
                    .AddFact(2200, 2014, 2, Sum, Sum)
                    .AddFact(4400, 2014, Sum, Sum, Sum)
                    .Build();
            }
        }
    }
}
