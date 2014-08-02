using System.Windows;
using System.Windows.Controls;
using PivotTable.Controls.Data;
using PivotTable.Controls.Layout;
using PivotTable.Controls.Painters;
using PivotTable.Data;

namespace PivotTable.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PivotTable"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PivotTable;assembly=PivotTable"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class PivotTable : Control
    {
        static PivotTable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PivotTable), new FrameworkPropertyMetadata(typeof(PivotTable)));
        }

        public static readonly DependencyProperty HorizontalHierarchyProperty = DependencyProperty.Register("HorizontalHierarchy", typeof(DimensionHierarchyDefinition), typeof(PivotTable));

        public static readonly DependencyProperty VerticalHierarchyProperty = DependencyProperty.Register("VerticalHierarchy", typeof(DimensionHierarchyDefinition), typeof(PivotTable));

        public static readonly DependencyProperty CubeProperty = DependencyProperty.Register("Cube", typeof(Cube), typeof(PivotTable));

        public DimensionHierarchyDefinition HorizontalHierarchy
        {
            get { return (DimensionHierarchyDefinition)GetValue(HorizontalHierarchyProperty); }
            set { SetValue(HorizontalHierarchyProperty, value); }
        }

        public DimensionHierarchyDefinition VerticalHierarchy
        {
            get { return (DimensionHierarchyDefinition)GetValue(VerticalHierarchyProperty); }
            set { SetValue(VerticalHierarchyProperty, value); }
        }

        public Cube Cube
        {
            get { return (Cube)GetValue(CubeProperty); }
            set { SetValue(CubeProperty, value); }
        }

        public void Refresh()
        {
            var grid = (Grid)GetTemplateChild("PART_Content");
            var horizontalHierarchy = new DimensionHierarchyBuilder(Cube, HorizontalHierarchy).Build();
            var verticalHierarchy = new DimensionHierarchyBuilder(Cube, VerticalHierarchy).Build();
            var spaceAllocator = new GridSpaceAllocator(grid, horizontalHierarchy, verticalHierarchy);
            spaceAllocator.AllocateSpace();
            var itemFactory = new ItemFactory();
            var headerPainter = new HeaderPainter(grid, itemFactory);
            headerPainter.Paint(horizontalHierarchy, spaceAllocator.HorizontalHierarchyPosition, GridOrientation.Horizontal);
            headerPainter.Paint(verticalHierarchy, spaceAllocator.VerticalHierarchyPosition, GridOrientation.Vertical);
            var factPainter = new FactPainter(grid, Cube, itemFactory);
            factPainter.Paint(horizontalHierarchy, verticalHierarchy, spaceAllocator.FactTablePosition);
        }
    }
}
