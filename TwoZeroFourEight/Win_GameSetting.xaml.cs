using System;
using System.Windows;

namespace TwoZeroFourEight
{
    /// <summary>
    /// Win_GameSetting.xaml 的交互逻辑
    /// </summary>
    public partial class Win_GameSetting : Window
    {
        public Win_GameSetting()
        {
            InitializeComponent();
            InitializeGameSlider();
        }

        /// <summary>
        /// 初始化游戏参数
        /// </summary>
        private void InitializeGameSlider()
        {
            txt_GridContent.Text = GameSetting.gridContent.ToString();
            txt_GridColor.Text = GameSetting.gridColor.ToString();
            txt_RowCount.Text = GameSetting.gridRowCount.ToString();
            txt_ColumnCount.Text = GameSetting.gridColumnCount.ToString();
        }

        /// <summary>
        /// 保存游戏参数
        /// </summary>
        private void SaveGameSlider()
        {
            if (string.IsNullOrEmpty(txt_GridContent.Text) ||
                string.IsNullOrEmpty(txt_GridColor.Text) ||
                string.IsNullOrEmpty(txt_RowCount.Text) ||
                string.IsNullOrEmpty(txt_ColumnCount.Text))
            {
                MessageBox.Show("游戏参数相关设置项不可为空！");
                return;
            }
                
            string gridContent = txt_GridContent.Text.ToString();
            string gridColor = txt_GridColor.Text.ToString();
            int gridRowCount = Convert.ToInt32(txt_RowCount.Text.ToString());
            int gridColumnCount = Convert.ToInt32(txt_ColumnCount.Text.ToString());
            GameSetting.Change(gridContent, gridColor, gridRowCount, gridColumnCount);
            MessageBox.Show("相关设置已经修改，下次启动游戏生效！ \r\n (可以点击重新开始生效而无需重启)");
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            SaveGameSlider();
            Close();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
