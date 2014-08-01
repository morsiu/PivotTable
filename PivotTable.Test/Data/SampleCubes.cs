using PivotTable.Data;

namespace PivotTable.Sample.Data
{
    public static class SampleCubes
    {
        private static readonly object Any = CubeDimension.AggregateMeasurement;

        public static Cube Sales
        {
            get
            {
                return new CubeBuilder("Year", "Quarter", "Division", "ProductCategory")
                    .AddFact(1000, 2014, 1, "Europe", "Shirts")
                    .AddFact(100, 2014, 1, "Europe", "Trousers")
                    .AddFact(1000, 2014, 2, "Europe", "Shirts")
                    .AddFact(100, 2014, 2, "Europe", "Trousers")
                    .AddFact(1000, 2014, 1, "Asia", "Shirts")
                    .AddFact(100, 2014, 1, "Asia", "Trousers")
                    .AddFact(1000, 2014, 2, "Asia", "Shirts")
                    .AddFact(100, 2014, 2, "Asia", "Trousers")

                    .AddFact(2200, 2014, 1, Any, Any)
                    .AddFact(2200, 2014, 2, Any, Any)
                    .AddFact(4400, 2014, Any, Any, Any)
                    .Build();
            }
        }
    }
}
