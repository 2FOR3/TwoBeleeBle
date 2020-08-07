using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoZeroFourEight
{
    /// <summary>
    /// 检查日志类
    /// </summary>
    public class CheckLog
    {
        public static void WriteLog(string strLog)
        {
            string pathError = AppDomain.CurrentDomain.BaseDirectory + "\\CheckLog\\";
            if (!Directory.Exists(pathError)) Directory.CreateDirectory(pathError); 

            string path = pathError + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    strLog = string.Format("{0}    {1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strLog);
                    byte[] myByte = Encoding.UTF8.GetBytes(strLog); 
                    fs.Write(myByte, 0, myByte.Length);
                }
            }
            catch (Exception) { }
        }
    }
}
