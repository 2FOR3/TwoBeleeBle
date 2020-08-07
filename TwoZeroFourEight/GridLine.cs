using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TwoZeroFourEight
{
    public class GridLine : Shape
    {
        private GameBoard gameBoard = null;  //游戏板

        /// <summary>
        /// 可见宽度
        /// </summary>
        public double VisiableWidth
        {
            get { return (double)GetValue(VisiableWidthProperty); }
            set { SetValue(VisiableWidthProperty, value); }
        }

        /// <summary>
        /// 可见高度
        /// </summary>
        public double VisiableHeight
        {
            get { return (double)GetValue(VisiableHeightProperty); }
            set { SetValue(VisiableHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisiableWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisiableWidthProperty =
            DependencyProperty.Register("VisiableWidth", typeof(double), typeof(GridLine), new PropertyMetadata(0.0, DependencyPropertyChanged));

        // Using a DependencyProperty as the backing store for VisiableHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisiableHeightProperty =
            DependencyProperty.Register("VisiableHeight", typeof(double), typeof(GridLine), new PropertyMetadata(0.0, DependencyPropertyChanged));

        private static void DependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridLine gridLine = (GridLine)d;
            gridLine.InvalidateVisual();
        }

        public GridLine(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
            gameBoard.Children.Insert(0, this);

            Binding bindingAW = new Binding();
            bindingAW.Source = gameBoard;
            bindingAW.Path = new PropertyPath("ActualWidth");
            this.SetBinding(VisiableWidthProperty, bindingAW);

            Binding bindingAH = new Binding();
            bindingAH.Source = gameBoard;
            bindingAH.Path = new PropertyPath("ActualHeight");
            this.SetBinding(VisiableHeightProperty, bindingAH);

            SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#BCAD9F"));
            this.Stroke = brush;
            this.StrokeThickness = 5;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryGroup gg = new GeometryGroup();
                StreamGeometry sg = new StreamGeometry();
                using (StreamGeometryContext context = sg.Open())
                {
                    for (int i = 1; i <= gameBoard.RowCount; i++)
                    {
                        context.BeginFigure(new Point(0, VisiableHeight / gameBoard.RowCount * i), true, false);
                        context.LineTo(new Point(VisiableWidth, VisiableHeight / gameBoard.RowCount * i), true, true);
                    }

                    for (int i = 1; i <= gameBoard.ColumnCount; i++)
                    {
                        context.BeginFigure(new Point(VisiableWidth / gameBoard.ColumnCount * i, 0), true, false);
                        context.LineTo(new Point(VisiableWidth / gameBoard.ColumnCount * i, VisiableHeight), true, true);
                    }
                }
                sg.Freeze();
                gg.Children.Add(sg);
                return gg;
            }
        }
    }
}
