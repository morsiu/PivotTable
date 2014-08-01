using Microsoft.VisualStudio.TestTools.UnitTesting;
using PivotTable.Controls.Data;
using PivotTable.Data;
using PivotTable.Sample.Data;

namespace PivotTable.Test
{
    [TestClass]
    public class DimensionHierarchyBuilderTest
    {
        [TestMethod]
        public void RunBuilder()
        {
            var cube = SampleCubes.Sales;
            var hierarchyDefinition = new DimensionHierarchyDefinition(cube.Dimensions, "Year", "Quarter");
            var hierarchyBuilder = new DimensionHierarchyBuilder(cube, hierarchyDefinition);

            var hierarchy = hierarchyBuilder.Build();
        }
    }
}
