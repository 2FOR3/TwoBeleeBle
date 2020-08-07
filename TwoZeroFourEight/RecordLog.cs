using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoZeroFourEight
{
    /// <summary>
    /// 记录日志类
    /// </summary>
    public class RecordLog
    {
        public static void WriteLog(string strLog)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\RecordLog\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            
            string fileName = path + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Append))
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
