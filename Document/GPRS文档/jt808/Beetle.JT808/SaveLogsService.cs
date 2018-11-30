using System;
using System.IO;

namespace Beetle.JT808
{
    public class SaveLogsService
    {
        public static void WriteLog(string str)
        {
            var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logfile = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            var dir = Environment.CurrentDirectory + "\\Logs";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            str = dt + " >>> " + str;
            Console.WriteLine(str);
            var filePath = dir + "\\" + logfile;
            using (var fs = new FileStream(filePath, FileMode.Append))
            {
                var sw = new StreamWriter(fs);
                //开始写入
                sw.Write(str);
                sw.Write("\r\n");
                sw.Write(System.Environment.NewLine);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
        }
    }
}