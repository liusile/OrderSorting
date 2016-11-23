using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SCB.OrderSorting.BLL.Common
{
    /// <summary>
    /// 保存错误记录类
    /// </summary>
    public class SaveErrLogHelper
    {
        #region 启动对象位置
        /// <summary>
        /// 启动对象位置
        /// </summary>
        private static string AppPath { get; set; }
        #endregion

        private static Dictionary<string, string> _errorMsgLog;

        public static Dictionary<string, string> ErrorMsgLog
        {
            get
            {
                if (_errorMsgLog == null)
                {
                    _errorMsgLog = new Dictionary<string, string>();
                }
                return _errorMsgLog;
            }
            set { _errorMsgLog = value; }
        }

        #region 分割线
        /// <summary>
        /// 分割线
        /// </summary>
        private static string SplitLine { get; set; }
        #endregion

        //==============================Motheds======================================
        #region 静态构造函数
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SaveErrLogHelper()
        {
            AppPath = Application.StartupPath + "\\ErrorLog";
            SplitLine = "===============================================================================\r\n";
        }
        #endregion

        #region 创建记录文件夹
        /// <summary>
        /// 创建记录文件夹
        /// </summary>
        /// <returns></returns>
        private static string CreatePath()
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string path = AppPath + "\\" + date + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            #region 删除4天前的历史
            {
                try
                {
                    string[] paths = Directory.GetDirectories(AppPath);//删除4天前的历史
                    int intDate = 0;
                    int.TryParse(date, out intDate);
                    foreach (var item in paths)
                    {
                        string itemFileName = Path.GetFileName(item);
                        int tempDate;
                        int.TryParse(itemFileName, out tempDate);
                        if ((intDate - tempDate) > 4)
                        {
                            Directory.Delete(item, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            #endregion
            return path;
        }
        #endregion

        #region 保存错误历史
        /// <summary>
        /// 保存错误历史
        /// </summary>
        /// <param name="err"></param>
        public static void SaveErrorLog(string orderid, string err)
        {
            try
            {
                Write(orderid + "\r\n" + err);
            }
            catch (Exception ex)
            {
                Write(ex.ToString());
            }

        }
        public static void SaveErrorLog(string orderid, string err,string PathName)
        {
            try
            {
                Write(orderid + "\r\n" + err,PathName);
            }
            catch (Exception ex)
            {
                Write(ex.ToString());
            }

        }
        #endregion

        #region 写txt
        /// <summary>
        /// 写txt
        /// </summary>
        /// <param name="err"></param>
        private static void Write(string err)
        {
            string path = SaveErrLogHelper.CreatePath() + DateTime.Now.ToString("dd") + ".txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ff"));
                sw.WriteLine(err);
                sw.WriteLine(SplitLine);
            }
        }
        private static void Write(string err,string PathName)
        {
            string path = SaveErrLogHelper.CreatePath() + PathName + ".txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ff"));
                sw.WriteLine(err);
                sw.WriteLine(SplitLine);
            }
        }
        #endregion

        /// <summary>
        /// 写入HTML
        /// </summary>
        /// <param name="err"></param>
        private static void Writehtml(string err)
        {
            string path = CreatePath() + DateTime.Now.ToString("dd") + ".html";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine("<br/><hr/>");
                sw.WriteLine("<p>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                sw.WriteLine("<p>" + err + "</p>");
            }
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        public static void SaveWarningData()
        {
            try
            {
                List<WarningData> temp = Warns;
                Warns = new List<WarningData>();
                WarnNo = 0;
                Writehtml(JsonConvert.SerializeObject(temp));

            }
            catch (Exception ex)
            {
                Write(ex.ToString());
            }
        }

        /// <summary>
        /// 清除警告数据
        /// </summary>
        public static void ClearWarningData()
        {
            Warns = new List<WarningData>();
        }

        /// <summary>
        /// 新增警告数据
        /// </summary>
        /// <param name="msg"></param>
        public static void AddWarningData(object msg)
        {
            if (Warns.Count > 0)
            {
                DateTime first = Warns.Last().Now;

                Warns.Add(new WarningData(msg));

                DateTime last = Warns.Last().Now;

                TimeSpan cha = last - first;
                if (cha.TotalSeconds > 10)
                {
                    Warns.Last().StyleStart = "<font color='red'>";
                    Warns.Last().StyleEnd = "</font>";
                }
                else if (cha.TotalSeconds > 3)
                {
                    Warns.Last().StyleStart = "<font color='blue'>";
                    Warns.Last().StyleEnd = "</font>";
                }
                else if (cha.TotalSeconds > 0.2)
                {
                    Warns.Last().StyleStart = "<font color='green'>";
                    Warns.Last().StyleEnd = "</font>";
                }
            }
            else
            {
                Warns.Add(new WarningData(msg));
            }

        }

        /// <summary>
        /// 日志列表
        /// </summary>
        private static List<WarningData> Warns = new List<WarningData>();
        private static int WarnNo = 0;
        /// <summary>
        /// 警告日志对象
        /// </summary>
        class WarningData
        {
            public WarningData(object msg)
            {
                No = ++WarnNo;
                Now = DateTime.Now;
                WarningMsg = msg;
            }
            public int No { get; set; }
            public string StyleStart { get; set; }
            public DateTime Now { get; set; }
            public object WarningMsg { get; set; }
            public string StyleEnd { get; set; }

        }
    }
}
