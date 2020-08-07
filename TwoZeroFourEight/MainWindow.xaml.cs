using System;
using System.Windows;
using System.Windows.Input;

namespace TwoZeroFourEight
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridLine gridLine = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region All Event For MainWindow.
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridLine = new GridLine(gameBoard);
            InitGame();
        }

        /// <summary>
        /// 游戏选项按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Option_Click(object sender, RoutedEventArgs e)
        {
            Win_GameSetting win_GameSetting = new Win_GameSetting();
            win_GameSetting.ShowDialog();
        }

        /// <summary>
        /// 重新开始按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NewGame_Click(object sender, RoutedEventArgs e)
        {
            RecordLog.WriteLog("Board Size:" + GameSetting.gridRowCount + "*" + GameSetting.gridColumnCount
                + "; Game Score:" + txb_Score.Text.ToString()
                + "; Step Count:" + txb_StepCount.Text.ToString() + ".");

            txb_Score.Text = "0";
            txb_StepCount.Text = "0";
            txt_Record.Text = "";
            gameBoard.Reset();
            gameBoard.Children.Clear();
            gridLine = new GridLine(gameBoard);
            InitGame();
        }

        /// <summary>
        /// 退出游戏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗口键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                case Key.Left:
                    txt_Record.Text += "Prush Left. \r\n";
                    gameBoard.ToLeft();
                    break;
                case Key.D:
                case Key.Right:
                    txt_Record.Text += "Prush Right. \r\n";
                    gameBoard.ToRight();
                    break;
                case Key.W:
                case Key.Up:
                    txt_Record.Text += "Prush Up. \r\n";
                    gameBoard.ToUp();
                    break;
                case Key.S:
                case Key.Down:
                    txt_Record.Text += "Prush Down. \r\n";
                    gameBoard.ToDown();
                    break;
                default:
                    txt_Record.Text += "Prush Error! \r\n";
                    break;
            }
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            RecordLog.WriteLog("Board Size:" + GameSetting.gridRowCount + "*" + GameSetting.gridColumnCount
                + "; Game Score:" + txb_Score.Text.ToString()
                + "; Step Count:" + txb_StepCount.Text.ToString() + ".");
        }
        #endregion

        /// <summary>
        /// 初始化游戏
        /// </summary>
        private void InitGame()
        {
            gameBoard.Initialize();
            gameBoard.OnScoreChange += ScoreChange;
            gameBoard.OnStepChange += StepChange;
            gameBoard.OnGameOver += GameOver;
        }

        /// <summary>
        /// 分数改变
        /// </summary>
        /// <param name="score"></param>
        private void ScoreChange(int score)
        {
            txb_Score.Text = score.ToString();
        }

        /// <summary>
        /// 步数改变
        /// </summary>
        /// <param name="step"></param>
        private void StepChange(int step)
        {
            txb_StepCount.Text = step.ToString();
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        /// <param name="isOver"></param>
        private void GameOver(bool isOver)
        {
            if (isOver == true)
            {
                MessageBox.Show("不能合并，请尝试其他方式，若无策略请重新开始!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
