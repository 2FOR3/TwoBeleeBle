using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TwoZeroFourEight
{
    /// <summary>
    /// 颜色块类
    /// </summary>
    public class ColorBlock : Shape, IMovable
    {
        #region 属性
        private int _XIndex = 0;  //x索引
        private int _YIndex = 0;  //y索引
        private string strDisplayText = GameSetting.arrayContent[0];  //展示的文本
        private double dWidth = 100;  //宽度
        private double dHeight = 100;  //高度
        private double dCenterX = 0;  //中心点x坐标
        private double dCenterY = 0;  //中心点y坐标
        private SolidColorBrush foreGroundBrush = Brushes.White;  //前景画刷
        private GameBoard gameBoard = null;  //游戏板对象
        #endregion

        #region 字段
        /// <summary>
        /// x索引
        /// </summary>
        public int XIndex
        {
            get
            {
                return _XIndex;
            }
        }

        /// <summary>
        /// y索引
        /// </summary>
        public int YIndex
        {
            get
            {
                return _YIndex;
            }
        }
        #endregion

        #region 依赖属性及回调方法
        /// <summary>
        /// 画板宽度(游戏板)
        /// </summary>
        public double BoardWidth
        {
            get { return (double)GetValue(BoardWidthProperty); }
            set { SetValue(BoardWidthProperty, value); }
        }

        /// <summary>
        /// 画板高度(游戏板)
        /// </summary>
        public double BoardHeight
        {
            get { return (double)GetValue(BoardHeightProperty); }
            set { SetValue(BoardHeightProperty, value); }
        }

        /// <summary>
        /// 文本索引
        /// </summary>
        public int TextIndex
        {
            get { return (int)GetValue(TextIndexProperty); }
            set { SetValue(TextIndexProperty, value); }
        }

        /// <summary>
        /// x坐标
        /// </summary>
        public double XCoo
        {
            get { return (double)GetValue(XCooProperty); }
            set { SetValue(XCooProperty, value); }
        }

        /// <summary>
        /// y坐标
        /// </summary>
        public double YCoo
        {
            get { return (double)GetValue(YCooProperty); }
            set { SetValue(YCooProperty, value); }
        }

        public static readonly DependencyProperty BoardWidthProperty =
            DependencyProperty.Register("BoardWidth", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty BoardHeightProperty =
            DependencyProperty.Register("BoardHeight", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty TextIndexProperty =
            DependencyProperty.Register("TextIndex", typeof(int), typeof(ColorBlock), new PropertyMetadata(2, OnValueChanged));

        public static readonly DependencyProperty XCooProperty =
            DependencyProperty.Register("XCoo", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnCooValueChanged));

        public static readonly DependencyProperty YCooProperty =
            DependencyProperty.Register("YCoo", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnCooValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorBlock line = (ColorBlock)d;
            line.CalcCoo();
            line.Refresh();
        }

        private static void OnCooValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorBlock line = (ColorBlock)d;
            if (e.Property == XCooProperty)
                line.RefreshLocation((double)e.NewValue, line.YCoo);
            else if (e.Property == YCooProperty)
                line.RefreshLocation(line.XCoo, (double)e.NewValue);
            line.Refresh();
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="board"></param>
        public ColorBlock(GameBoard board)
        {
            this.gameBoard = board;
            board.Children.Add(this);
            InitStyle();

            Binding binding1 = new Binding();
            binding1.Source = board;
            binding1.Path = new PropertyPath("ActualWidth"); 
            this.SetBinding(BoardWidthProperty, binding1);
            Binding binding2 = new Binding();
            binding2.Source = board;
            binding2.Path = new PropertyPath("ActualHeight");
            this.SetBinding(BoardHeightProperty, binding2);

            InitLocation();
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            FormattedText formattedText = new FormattedText(
                strDisplayText,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                Math.Min(dWidth, dHeight) / 4,
                foreGroundBrush);
            dCenterX = dWidth / 2 - formattedText.Width / 2 + 3;
            dCenterY = dHeight / 2 - formattedText.Height / 2 + 3;
            drawingContext.DrawText(formattedText, new Point(dCenterX, dCenterY));
        }

        /// <summary>
        /// 获取定义的几何形状
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryGroup gg = new GeometryGroup();
                dWidth = BoardWidth / gameBoard.ColumnCount - 6;
                dHeight = BoardHeight / gameBoard.RowCount - 6;
                RectangleGeometry rect = new RectangleGeometry(new Rect(3, 3, dWidth, dHeight), 5, 5);
                gg.Children.Add(rect);
                return gg;
            }
        }

        /// <summary>
        /// 初始化样式(随机取设置的文本块数组中第一个或第二个)
        /// </summary>
        private void InitStyle()
        {
            if (GetRandom() > 0.5)
            {
                this.Stroke = Brushes.Green;
                this.Fill = GameSetting.arrayColor[1];
                this.StrokeThickness = 0;
                this.strDisplayText = GameSetting.arrayContent[1];
                this.TextIndex = 4;
            }
            else
            {
                this.Stroke = Brushes.Green;
                this.Fill = GameSetting.arrayColor[0];
                this.StrokeThickness = 0;
            }
        }

        /// <summary>
        /// 初始化定位
        /// </summary>
        private void InitLocation()
        {
            Random rd = new Random(GetRandomSeed());
            _XIndex = rd.Next(gameBoard.ColumnCount);
            _YIndex = rd.Next(gameBoard.RowCount);

            while (gameBoard.GetBlock(_XIndex, _YIndex) > 0)
            {
                _XIndex = rd.Next(gameBoard.ColumnCount);
                _YIndex = rd.Next(gameBoard.RowCount);
            }

            gameBoard.SetBlock(_XIndex, _YIndex, this);
            CalcCoo();
            Refresh();
            Canvas.SetLeft(this, this.XCoo);
            Canvas.SetTop(this, this.YCoo);
        }

        /// <summary>
        /// 计算坐标
        /// </summary>
        private void CalcCoo()
        {
            XCoo = BoardWidth / gameBoard.ColumnCount * _XIndex;
            YCoo = BoardHeight / gameBoard.RowCount * _YIndex;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            this.InvalidateVisual();
        }

        /// <summary>
        /// 刷新定位
        /// </summary>
        /// <param name="offsetLeft">距左边偏移值</param>
        /// <param name="offsetUp">距上边偏移值</param>
        private void RefreshLocation(double offsetLeft, double offsetUp)
        {
            Canvas.SetLeft(this, offsetLeft);
            Canvas.SetTop(this, offsetUp);
            this.InvalidateVisual();
        }

        /// <summary>
        /// 向左移动
        /// </summary>
        /// <returns></returns>
        public bool MoveLeft()
        {
            if (_XIndex == 0)  //到达左边界
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex - 1, _YIndex) && !IsSame(Direction.Left))
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex - 1, _YIndex) && IsSame(Direction.Left))
            {
                //合并
                bool result = gameBoard.MergeBlock(_XIndex, _YIndex, Direction.Left);
                if (!result) return true;

                MoveLeftAnime();
                MoveLeft();
                Refresh();
            }
            if (!gameBoard.IsLocationFilled(_XIndex - 1, _YIndex) && _XIndex != 0)
            {
                MoveLeftAnime();
                MoveLeft();
                Refresh();
            }
            return true;
        }

        /// <summary>
        /// 向右移动
        /// </summary>
        /// <returns></returns>
        public bool MoveRight()
        {
            if (_XIndex == gameBoard.RowCount)  //到达右边界
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex + 1, _YIndex) && !IsSame(Direction.Right))
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex + 1, _YIndex) && IsSame(Direction.Right))
            {
                //合并
                bool result = gameBoard.MergeBlock(_XIndex, _YIndex, Direction.Right);
                if (!result) return true;

                MoveRightAnime();
                MoveRight();
                Refresh();
            }
            if (!gameBoard.IsLocationFilled(_XIndex + 1, _YIndex) && _XIndex != gameBoard.RowCount)
            {
                MoveRightAnime();
                MoveRight();
                Refresh();
            }
            return true;
        }

        /// <summary>
        /// 向上移动
        /// </summary>
        /// <returns></returns>
        public bool MoveUp()
        {
            if (_YIndex == 0)  //到达上边界
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex, _YIndex - 1) && !IsSame(Direction.Up))
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex, _YIndex - 1) && IsSame(Direction.Up))
            {
                bool result = gameBoard.MergeBlock(_XIndex, _YIndex, Direction.Up);
                if (!result) return true;

                MoveUpAnime();
                MoveUp();
                Refresh();
            }
            if (!gameBoard.IsLocationFilled(_XIndex, _YIndex - 1) && _YIndex != 0)
            {
                //上移
                gameBoard.SetBlock(_XIndex, _YIndex, null);
                _YIndex--;
                gameBoard.SetBlock(_XIndex, _YIndex, this);

                MoveUpAnime();
                MoveUp();
                Refresh();
            }
            return true;
        }

        /// <summary>
        /// 向下移动
        /// </summary>
        /// <returns></returns>
        public bool MoveDown()
        {
            if (_YIndex == gameBoard.RowCount)  //到达下边界
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex, _YIndex + 1) && !IsSame(Direction.Down))
            {
                return false;
            }
            if (gameBoard.IsLocationFilled(_XIndex, _YIndex + 1) && IsSame(Direction.Down))
            {
                //合并
                bool result = gameBoard.MergeBlock(_XIndex, _YIndex, Direction.Down);
                if (!result) return true;

                MoveDownAnime();
                MoveDown();
                Refresh();
            }
            if (!gameBoard.IsLocationFilled(_XIndex, _YIndex + 1) && _YIndex != gameBoard.RowCount)
            {
                MoveDownAnime();
                MoveDown();
                Refresh();
            }
            return true;
        }

        /// <summary>
        /// 判断是否为同一个块
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private bool IsSame(Direction dir)
        {
            bool result = false;
            switch (dir)
            {
                case Direction.Left:
                    result = gameBoard.GetBlock(_XIndex - 1, _YIndex) == TextIndex;
                    break;
                case Direction.Right:
                    result = gameBoard.GetBlock(_XIndex + 1, _YIndex) == TextIndex;
                    break;
                case Direction.Up:
                    result = gameBoard.GetBlock(_XIndex, _YIndex - 1) == TextIndex;
                    break;
                case Direction.Down:
                    result = gameBoard.GetBlock(_XIndex, _YIndex + 1) == TextIndex;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 左移动画
        /// </summary>
        private void MoveLeftAnime()
        {
            //左移增加块
            gameBoard.SetBlock(_XIndex, _YIndex, null);
            _XIndex--;
            gameBoard.SetBlock(_XIndex, _YIndex, this);

            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = BoardWidth / gameBoard.ColumnCount * _XIndex;
            double yCoo1 = BoardHeight / gameBoard.RowCount * _YIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldXCoo;
            da.To = xCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(XCooProperty, da);
        }

        /// <summary>
        /// 右移动画
        /// </summary>
        private void MoveRightAnime()
        {
            //右移增加块
            gameBoard.SetBlock(_XIndex, _YIndex, null);
            _XIndex++;
            gameBoard.SetBlock(_XIndex, _YIndex, this);

            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = BoardWidth / gameBoard.ColumnCount * _XIndex;
            double yCoo1 = BoardHeight / gameBoard.RowCount * _YIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldXCoo;
            da.To = xCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(XCooProperty, da);
        }

        /// <summary>
        /// 上移动画
        /// </summary>
        private void MoveUpAnime()
        {
            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = BoardWidth / gameBoard.ColumnCount * _XIndex;
            double yCoo1 = BoardHeight / gameBoard.RowCount * _YIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldYCoo;
            da.To = yCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(YCooProperty, da);
        }

        /// <summary>
        /// 下移动画
        /// </summary>
        private void MoveDownAnime()
        {
            //下移增加块
            gameBoard.SetBlock(_XIndex, _YIndex, null);
            _YIndex++;
            gameBoard.SetBlock(_XIndex, _YIndex, this);

            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = BoardWidth / gameBoard.ColumnCount * _XIndex;
            double yCoo1 = BoardHeight / gameBoard.RowCount * _YIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldYCoo;
            da.To = yCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(YCooProperty, da);
        }

        /// <summary>
        /// 时间线播放完毕时的触发事件(重新计算坐标并刷新定位)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void da_Completed(object sender, EventArgs e)
        {
            CalcCoo();
            RefreshLocation(XCoo, YCoo);
            this.Refresh();
        }

        /// <summary>
        /// 改变文本内容
        /// </summary>
        /// <returns>是否结束</returns>
        public bool ChangeText()
        {
            this.TextIndex = this.TextIndex * 2;
            strDisplayText = GameSetting.arrayContent[((int)Math.Log(TextIndex, 2) - 1) % GameSetting.arrayContent.Length];
            int brushIndex = ((int)Math.Log(TextIndex, 2) - 1) % GameSetting.arrayContent.Length;
            
            if (brushIndex > GameSetting.arrayColor.Length - 1)
            {
                this.Fill = GameSetting.arrayColor[brushIndex % (GameSetting.arrayColor.Length - 1)];
            }
            else
            {
                this.Fill = GameSetting.arrayColor[brushIndex];
            }

            this.InvalidateVisual();

            if (TextIndex >= Math.Pow(2, GameSetting.arrayContent.Length))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取0-1之间的随机数
        /// </summary>
        /// <returns></returns>
        private double GetRandom()
        {
            int seed = Guid.NewGuid().GetHashCode();
            Random r = new Random(seed);
            int i = r.Next(0, 1000000);
            return (double)i / 1000000;
        }

        /// <summary>
        /// 获取一个随机数种子
        /// </summary>
        /// <returns></returns>
        private int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
