using System.Windows;
using PivotTable.Data;
using PivotTable.Sample.Data;

namespace PivotTable.Sample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PivotTable.Cube = SampleCubes.Sales;
            PivotTable.HorizontalHierarchy = new DimensionHierarchyDefinition(PivotTable.Cube.Dimensions, "Year", "Quarter");
            PivotTable.VerticalHierarchy = new DimensionHierarchyDefinition(PivotTable.Cube.Dimensions, "ProductCategory", "Region");
            PivotTable.Loaded += (s, e) => { PivotTable.Refresh(); };
        }
    }
}
