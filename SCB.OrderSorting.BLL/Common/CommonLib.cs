using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SCB.OrderSorting.BLL.Common
{
    public static class CommonLib
    {
        [DllImport("ShowWindow.dll")]
        private static extern void ShowPopWindow(string Title, string Content);

        public static void SetWindowsFocus(Form frmWindow)
        {
            if (frmWindow != null)
            {
                frmWindow.Activate();
            }
        }

        public static void SetEnable(Form form, Control ct)
        {
            try
            {
                if (ct != null && form != null)
                {

                    form.Invoke((EventHandler)delegate (object ss, EventArgs ex)
                    {
                        if (ct.Enabled)
                        {
                            ct.Enabled = false;
                        }
                        else
                        {

                            ct.Enabled = true;
                        }
                    });
                }
            }
            catch { }
        }

        public static void SetFocus(Form form, Control ct)
        {

            try
            {
                if (ct != null && form != null)
                {

                    form.BeginInvoke((EventHandler)delegate (object ss, EventArgs ex)
                    {

                        ct.Focus();


                    });
                }
            }
            catch { }
        }

        public static void SetVisible(Form form, Control ct)
        {

            try
            {
                if (ct != null && form != null)
                {

                    form.BeginInvoke((EventHandler)delegate (object ss, EventArgs ex)
                    {
                        if (ct.Visible)
                        {
                            ct.Visible = false;
                        }
                        else
                        {

                            ct.Visible = true;
                        }
                    });
                }
            }
            catch { }
        }

        public static void ShowErrorMsg(Form form, Exception ex)
        {
            if (form.InvokeRequired)
            {
                form.BeginInvoke((MethodInvoker)delegate ()
                {
                    MessageBox.Show(ex.ToString());
                });
            }
            else
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 运行命令
        /// </summary>
        /// <param name="strShellCommand">命令字符串</param>
        /// <returns>命令运行时间</returns>
        private static double RunShell(string strShellCommand)
        {

            double spanMilliseconds = 0;

            DateTime beginTime = DateTime.Now;

            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";

            cmd.StartInfo.UseShellExecute = false;

            cmd.StartInfo.CreateNoWindow = true;

            cmd.StartInfo.Arguments = String.Format(@"/c {0}", strShellCommand);

            cmd.Start();

            cmd.WaitForExit();

            DateTime endTime = DateTime.Now;

            TimeSpan timeSpan = endTime - beginTime;

            spanMilliseconds = timeSpan.TotalMilliseconds;
            return spanMilliseconds;

        }

        /// <summary>
        /// 产品条码转换，拆分为产品条码、供应商条码、 类别
        /// 格式为: 原有产品条码 + "-" + 供应商条码 + 类别
        ///供应商条码: 四位， 由26个字母和10个数字组成, 36进制；  
        ///类别: 1位;  
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>产品条码、供应商条码、类别</returns>
        public static string[] ConvertProductSKU(string productSku)
        {
            string[] array = new string[3] { "", "", "" };
            if (string.IsNullOrEmpty(productSku))
                return array;
            try
            {
                var strArr = productSku.Trim().Split(new char[] { '-', '|' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (strArr != null && strArr.Length > 1)
                {
                    array[0] = strArr[0];
                    var supplierCode = strArr[1];
                    if (!string.IsNullOrEmpty(supplierCode) && productSku.Contains("-"))
                    {
                        supplierCode = supplierCode.Trim();
                        array[1] = supplierCode.Substring(0, supplierCode.Length - 1); // 供应商条码: 四位 由26个字母和10个数字组成, 36进制； 
                        array[2] = supplierCode.Substring(supplierCode.Length - 1, 1); // 类别: 最后1位; 
                    }
                }
                else if (productSku.Contains('-') || productSku.Contains('|'))   //如果没有供应商条码，则把分隔符去掉，否则查不到数据
                    array[0] = productSku.Replace("-", "").Replace("|", "");
                else array[0] = productSku;
                return array;
            }
            catch (Exception)
            {
                array[0] = productSku;
                return array;
            }
        }

        public static List<KeyValuePair<double, double>> GetPacketWeightRange(Dictionary<string, string> dict)
        {
            //关键字
            const string sKey = "PackingWeightRange";
            List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();
            if (dict == null || dict.Count <= 0 || !dict.ContainsKey(sKey))
            {
                return null;
            }
            string values = dict[sKey].ToString();
            var valueArray = values.Split(',');
            if (valueArray.Length > 0)
            {
                foreach (var str in valueArray)
                {
                    string[] valueTemp = str.Split(':');
                    if (valueTemp.Length == 2)
                    {
                        list.Add(new KeyValuePair<double, double>(
                            double.Parse(valueTemp[0]), double.Parse(valueTemp[1])));
                    }
                }
            }
            return list;
        }

    }
}
