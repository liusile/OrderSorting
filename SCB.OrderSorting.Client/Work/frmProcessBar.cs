using SCB.OrderSorting.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client.Work
{
    public partial class frmProcessBar : Form
    {
        private delegate void SetPos(int ipos, string vinfo);
        public frmProcessBar()
        {
            InitializeComponent();
        }
        private void SetTextMesssage(int ipos, string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.label1.Text = ipos.ToString() + "/100";
                this.progressBar1.Value = Convert.ToInt32(ipos);
                this.textBox1.AppendText(vinfo);
            }
        }
        private void btn_Go_Click(object sender, EventArgs e)
        {
            Thread fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
        }
        private void SleepT()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    this.textBox1.Clear();
                });
               
                FileTool file = new FileTool();
                var data = file.Read();
                SetTextMesssage(100 * 1 / 5, "开始发送联机（更新）信号" + "\r\n");
                /*****************************2.发送联机（更新）信号 校验码 4F9 0xF9, 0x4*****************************/
                var write2 = new byte[] { 0x55, 0xAA, 0x1C, 0xC1 };
                write2 = Enumerable.Concat(write2, CLCData(write2)).ToArray();
                byte[] result2 = OrderSortService.DoloadBoard(write2);
                if (result2[0] != 0x5F || result2[1] != 0x01 || result2[2] != 0x00 || result2[3] != 0xC1)
                {
                    throw new Exception($"下载数据失败：联机信号应答失败");
                }
                SetTextMesssage(100 * 2 / 5, "发送联机（更新）信号成功" + "\r\n");
                SetTextMesssage(100 * 2 / 5, "开始发送文件大小" + "\r\n");
                /******************************3.发送文件大小 ******************************/
                byte[] fileSize = BitConverter.GetBytes(Convert.ToInt16(data.Length));
                var update3 = new byte[] { 0x55, 0xAA, 0x2C, 0xC2 };
                update3=Enumerable.Concat(update3, fileSize).ToArray();
                update3=Enumerable.Concat(update3, CLCData(update3)).ToArray();
                byte[] result3 = OrderSortService.DoloadBoard(update3);
                if (result3[0] != 0x5F || result3[1] != 0x01 || result3[2] != 0x00 || result3[3] != 0xC2)
                {
                    throw new Exception($"下载数据失败：发送文件大小应答失败");
                }
                SetTextMesssage(100 * 3 / 5, $"发送文件大小成功" + "\r\n");
                /****************************** 4文件传输******************************/
                //调整数据为1024的倍数
                var dataRe = data.Length % 1024;
                if (dataRe != 0)
                {
                    var dataEx = new byte[1024 - data.Length % 1024];

                    data=Enumerable.Concat(data, dataEx).ToArray();
                }
                int dataLen = data.Length / 1024;

                SetTextMesssage(100 * 3 / 5, $"开始传输文件数据，共{dataLen}步" + "\r\n");
                //5发送数据
                for (int i = 1; i <= dataLen; i++)
                {
                    var writeData = new byte[] { 0x55, 0xAA, 0x3C, 0xC3 };
                    var partData = GetData(data, i);
                    writeData=Enumerable.Concat(writeData, partData).ToArray();
                    var clcData = CLCData(writeData);
                    writeData=Enumerable.Concat(writeData, clcData).ToArray();

                    byte[] result4 = OrderSortService.DoloadBoard(writeData);
                    if (result4[0] != 0x5F || result4[1] != 0x01 || result4[2] != 0x00 || result4[3] != 0xC3)
                    {
                        throw new Exception($"下载数据失败：接收传输数据应答失败:第{i}步");
                    }
                    SetTextMesssage(100 * 3 / 5 + 40 / (data.Length / 1024) * i, $"第{i}步传输文件数据成功" + "\r\n"); 
                }
                SetTextMesssage(100 * 5 / 5, "更新成功！" + "\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] GetData(byte[] data, int start, int count = 1024)
        {
            var resultData = new byte[count];
            var index = (start - 1) * count + 1;
            for (int i = 0; i < resultData.Length; i++)
            {
                resultData[i] = data[index + i];
            }
            return resultData;
        }
        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] CLCData(byte[] data)
        {
            int result = Convert.ToInt16(data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                result = result ^ data[i];
            }
            return new byte[] { BitConverter.GetBytes(result)[0] };
        }
    }
}
