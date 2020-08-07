using System;
using System.Windows.Controls;

namespace TwoZeroFourEight
{
    public class GameBoard : Canvas, IControlable
    {
        #region 属性
        private int _RowCount = 4;  //行数
        private int _ColumnCount = 4;  //列数
        private ColorBlock[,] _FillBlock;  //填充块

        private int iScore = 0;  //分数
        private int iStep = 0;  //步数
        #endregion

        #region 字段
        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount
        {
            get
            {
                return _RowCount;
            }
            set
            {
                _RowCount = value;
            }
        }
 
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return _ColumnCount;
            }
            set
            {
                _ColumnCount = value;
            }
        }

        /// <summary>
        /// 填充块
        /// </summary>
        public ColorBlock[,] FillBlock
        {
            get
            {
                return _FillBlock;
            }
        }
        #endregion

        /// <summary>
        /// 分数改变
        /// </summary>
        public Action<int> OnScoreChange;  
        /// <summary>
        /// 步数改变
        /// </summary>
        public Action<int> OnStepChange;
        /// <summary>
        /// 游戏结束
        /// </summary>
        public Action<bool> OnGameOver;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameBoard()
        {
            this.Focusable = true;
            this.Focus();
            this.Reset();
        }

        /// <summary>
        /// 增加步数
        /// </summary>
        public void AddStep()
        {
            iStep++;
            if (OnStepChange != null)
                OnStepChange(iStep);
        }

        /// <summary>
        /// 增加分数
        /// </summary>
        /// <param name="score">增加的分数</param>
        public void AddScore(int score)
        {
            iScore += score;
            if (OnScoreChange != null)
            {
                OnScoreChange(iScore);
            }
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        /// <param name="isOver">是否结束</param>
        public void GameOver(bool isOver)
        {
            if (OnGameOver != null)
            {
                OnGameOver(isOver);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            GameSetting.Load();
            RowCount = GameSetting.gridRowCount;
            ColumnCount = GameSetting.gridColumnCount;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            _FillBlock = new ColorBlock[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _FillBlock[i, j] = null;
                }
            }
            GameSetting.Load();
            ColorBlock block1 = new ColorBlock(this);
            ColorBlock block2 = new ColorBlock(this);
        }

        /// <summary>
        /// 增加块
        /// </summary>
        public void AddBlock()
        {
            if (IsOverflow())
            {
                GameOver(true);
                return;
            }
            ColorBlock block = new ColorBlock(this);
        }

        /// <summary>
        /// 移除块
        /// </summary>
        /// <param name="xIndex">x坐标</param>
        /// <param name="yIndex">y坐标</param>
        public void RemoveBlock(int xIndex, int yIndex)
        {
            if (FillBlock[yIndex, xIndex] != null)
            {
                this.Children.Remove(FillBlock[yIndex, xIndex]);
                FillBlock[yIndex, xIndex] = null;
            }
        }

        /// <summary>
        /// 左移
        /// </summary>
        public void ToLeft()
        {
            bool isAdd = false;
            for (int i = 0; i < ColumnCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    if (FillBlock[j, i] != null)
                    {
                        isAdd |= FillBlock[j, i].MoveLeft();
                    }
                }
            }

            if (isAdd)
            {
                AddBlock();
                AddStep();
            }
            else
            {
                if (IsOverflow())
                {
                    GameOver(true);
                    return;
                }
            }
        }

        /// <summary>
        /// 右移
        /// </summary>
        public void ToRight()
        {
            bool isAdd = false;
            for (int i = ColumnCount - 1; i >= 0; i--)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    if (FillBlock[j, i] != null)
                    {
                        isAdd |= FillBlock[j, i].MoveRight();
                    }
                }
            }

            if (isAdd)
            {
                AddBlock();
                AddStep();
            }
            else
            {
                if (IsOverflow())
                {
                    GameOver(true);
                    return;
                }
            }
        }

        /// <summary>
        /// 上移
        /// </summary>
        public void ToUp()
        {
            bool isAdd = false;
            for (int i = 0; i < ColumnCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    if (FillBlock[j, i] != null)
                    {
                        isAdd |= FillBlock[j, i].MoveUp();
                    }
                }
            }

            if (isAdd)
            {
                AddBlock();
                AddStep();
            }
            else
            {
                if (IsOverflow())
                {
                    GameOver(true);
                    return;
                }
            }
        }

        /// <summary>
        /// 下移
        /// </summary>
        public void ToDown()
        {
            bool isAdd = false;
            for (int i = 0; i < ColumnCount; i++)
            {
                for (int j = RowCount - 1; j >= 0; j--)
                {
                    if (FillBlock[j, i] != null)
                    {
                        isAdd |= FillBlock[j, i].MoveDown();
                    }
                }
            }

            if (isAdd)
            {
                AddBlock();
                AddStep();
            }
            else
            {
                if (IsOverflow())
                {
                    GameOver(true);
                    return;
                }
            }
        }


        /// <summary>
        /// 合并块
        /// </summary>
        /// <param name="xIndex">x坐标</param>
        /// <param name="yIndex">y坐标</param>
        /// <param name="dir">方向</param>
        /// <returns>是否继续</returns>
        public bool MergeBlock(int xIndex, int yIndex, Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    RemoveBlock(xIndex - 1, yIndex);
                    break;
                case Direction.Right:
                    RemoveBlock(xIndex + 1, yIndex);
                    break;
                case Direction.Up:
                    RemoveBlock(xIndex, yIndex - 1);
                    break;
                case Direction.Down:
                    RemoveBlock(xIndex, yIndex + 1);
                    break;
                default:
                    break;
            }
            bool result = FillBlock[yIndex, xIndex].ChangeText();
            if (result)
            {
                GameOver(true);
                return false;
            }
            AddScore(FillBlock[yIndex, xIndex].TextIndex);
            return true;
        }

        /// <summary>
        /// 获取块的文本索引
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public int GetBlock(int xIndex, int yIndex)
        {
            if (xIndex < 0 || xIndex > _ColumnCount - 1)
                return 0;
            if (yIndex < 0 || yIndex > _RowCount - 1)
                return 0;
            if (FillBlock[yIndex, xIndex] == null)
                return 0;
            return FillBlock[yIndex, xIndex].TextIndex;
        }

        /// <summary>
        /// 是否溢出(返回值为true说明已没有位置可生成块继续)
        /// </summary>
        /// <returns></returns>
        public bool IsOverflow()
        {
            return this.Children.Count == this.RowCount * this.ColumnCount + 1;
        }

        /// <summary>
        /// 判断当前位置是否被填充
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public bool IsLocationFilled(int xIndex, int yIndex)
        {
            if (xIndex < 0 || xIndex > _ColumnCount - 1)
                return true;
            if (yIndex < 0 || yIndex > _RowCount - 1)
                return true;
            if (FillBlock[yIndex, xIndex] == null)
                return false;
            return FillBlock[yIndex, xIndex].TextIndex > 0;
        }

        /// <summary>
        /// 设置块
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="block"></param>
        public void SetBlock(int xIndex, int yIndex, ColorBlock block)
        {
            FillBlock[yIndex, xIndex] = block;
        }
    }
}
