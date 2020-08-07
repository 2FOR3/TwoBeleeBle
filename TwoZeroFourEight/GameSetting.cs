using System;
using System.Windows.Media;

namespace TwoZeroFourEight
{
    /// <summary>
    /// 游戏设置类
    /// </summary>
    public class GameSetting
    {
        /// <summary>
        /// 格子内容
        /// </summary>
        public static string gridContent;
        /// <summary>
        /// 格子颜色
        /// </summary>
        public static string gridColor;
        /// <summary>
        /// 格子行数
        /// </summary>
        public static int gridRowCount;
        /// <summary>
        /// 格子列数
        /// </summary>
        public static int gridColumnCount;
        /// <summary>
        /// 内容数组
        /// </summary>
        public static string[] arrayContent;
        /// <summary>
        /// 颜色数组[Brush]
        /// </summary>
        public static Brush[] arrayColor;
        /// <summary>
        /// 颜色数组[string]
        /// </summary>
        public static string[] arrayColorString;

        private static IniFile runIniFile;

        /// <summary>
        /// 载入
        /// </summary>
        public static void Load()
        {
            ReadSystemConfig();
            arrayContent = gridContent.Split(',');
            arrayColorString = gridColor.Split(',');
            BrushConverter brushConverter = new BrushConverter();
            arrayColor = new Brush[arrayColorString.Length];
            for (int i = 0; i < arrayColorString.Length; i++)
            {
                arrayColor[i] = (Brush)brushConverter.ConvertFromString(arrayColorString[i]);
            }
        }

        /// <summary>
        /// 改变
        /// </summary>
        /// <param name="arrContent">内容串</param>
        /// <param name="arrColor">颜色串</param>
        /// <param name="rowCount">行数</param>
        /// <param name="colCount">列数</param>
        public static void Change(string arrContent, string arrColor, int rowCount, int colCount)
        {
            gridContent = arrContent;
            gridColor = arrColor;
            gridRowCount = rowCount;
            gridColumnCount = colCount;
            WriteSystemConfig();
        }

        /// <summary>
        /// 读取系统配置
        /// </summary>
        private static void ReadSystemConfig()
        {
            runIniFile = new IniFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "SystemConfig.ini");
            try
            {
                gridContent = runIniFile.IniReadValue("SystemConfig", "GridContent");
                gridColor = runIniFile.IniReadValue("SystemConfig", "GridColor");
                gridRowCount = int.Parse(runIniFile.IniReadValue("SystemConfig", "GridRowCount"));
                gridColumnCount = int.Parse(runIniFile.IniReadValue("SystemConfig", "GridColumnCount"));
            }
            catch (Exception ex)
            {
                CheckLog.WriteLog("读取配置文件时出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 写入系统配置
        /// </summary>
        private static void WriteSystemConfig()
        {
            try
            {
                runIniFile.IniWriteValue("SystemConfig", "GridContent", gridContent);
                runIniFile.IniWriteValue("SystemConfig", "GridColor", gridColor);
                runIniFile.IniWriteValue("SystemConfig", "GridRowCount", gridRowCount.ToString());
                runIniFile.IniWriteValue("SystemConfig", "GridColumnCount", gridColumnCount.ToString());
            }
            catch (Exception ex)
            {
                CheckLog.WriteLog("写入配置文件时出错：" + ex.Message);
            }
        }
    }
}
