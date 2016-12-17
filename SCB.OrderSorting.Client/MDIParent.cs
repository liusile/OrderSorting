using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.Client.Work;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class MDIParent : Form
    {
        private UserInfo _UserInfo;
        private frmLogin _frmLgn;

        private bool IsLogin { get; set; }

        public MDIParent()
        {
            InitializeComponent();
        }

        public MDIParent(frmLogin frmLogin)
        {
            this._frmLgn = frmLogin;
            InitializeComponent();
        }

        private void 分拣记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrderSortingLog frm = new frmOrderSortingLog();
            frm.ShowDialog();
        }

        private void 装包记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPackingLog frm = new frmPackingLog();
            frm.ShowDialog();
        }

        private void 分拣架设置newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmOrderSortingWork frm = new frmOrderSortingWork(_UserInfo);
            frmOrderSortingWorkNew frm = new frmOrderSortingWorkNew(_UserInfo);
            frm.ShowDialog();
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemSetting frm = new frmSystemSetting();
            frm.ShowDialog();
        }

        /// <summary>
        /// 选择处理中心
        /// </summary>
        //private void SelProcessCenter()
        //{
        //    frmSelProcessCenter processCenter = new frmSelProcessCenter();
        //    if (processCenter.ShowDialog() == DialogResult.OK)
        //    {
        //        //Task.Run(() =>
        //        //{
        //        //OrderSortService.ReSetLED();
        //        //OrderSortService.ReSetWarningLight();
        //        //OrderSortService.ReSetGratingOrButton(3);
        //        //});
        //        LoginService.SetProcessCenterID(processCenter.GetProcessCenterID());
        //    }
        //    else
        //    {
        //        Close();
        //    }
        //}

        private void MDIParent_Load(object sender, EventArgs e)
        {
            try
            {
                //frmLogin frmLgn = new frmLogin();
                //if (frmLgn.ShowDialog() == DialogResult.OK)
                //{
                //测试注释掉
                _UserInfo = _frmLgn.GetUserInfo();
                LoginService.SetProcessCenterID(_UserInfo.Pcid);
               // SelProcessCter();
                Task.Run(() =>
                {
                    try
                    {
                        //OrderSortService.TCPPortManage.ClearLED();
                        //OrderSortService.TCPPortManage.ClearGrating2Button();
                        //OrderSortService.TCPPortManage.SetWarningAll(0);
                        //LoginService.SetProcessCenterID(_UserInfo.Pcid, _UserInfo.ReceivePointId);
                    }
                    catch (Exception ex)
                    {
                        SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                        MessageBox.Show(ex.Message);
                    }
                }).ContinueWith(cw => { IsLogin = true; });
                //}
                //else
                //{
                //    Close();
                //}
                //SelProcessCenter();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            //// 测试注释掉
            
            if (!IsLogin)
                return;
            toolStripStatusLabelname.Text = " 当前时间：" + DateTime.Now.ToString();
            string processeName = string.Empty;
            if (string.IsNullOrWhiteSpace(_UserInfo.PcName))
            {
                switch (_UserInfo.Pcid)
                {
                    case "7":
                        processeName = "广州处理中心";
                        break;
                    case "722":
                        processeName = "杭州处理中心";
                        break;
                    case "934":
                        processeName = "东莞处理中心";
                        break;
                    case "619":
                        processeName = "会江服装处理中心";
                        break;
                    case "1039":
                        processeName = "东莞塘厦处理中心";
                        break;
                    case "1040":
                        processeName = "香港处理中心";
                        break;
                    case "1063":
                        processeName = "广州品牌处理中心";
                        break;
                    case "918":
                        processeName = "英国伯明翰处理中心";
                        break;
                    case "1022":
                        processeName = "英国2号仓处理中心";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                processeName = _UserInfo.PcName;
            }
            string tip = "  登录帐号: " + _UserInfo.UserName;
            if (!string.IsNullOrEmpty(processeName))
                tip += "   发货中心：" + processeName;
            if (!string.IsNullOrWhiteSpace(_UserInfo.RepName))
                tip += "   收货点：" + _UserInfo.RepName;
            toolStripStatusLabel1.Text = tip;
        }

        private void 测试订单标签打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestOrderPrint frm = new frmTestOrderPrint(_UserInfo);
            frm.ShowDialog();
        }

        private void 分拣统计报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDayReport frm = new frmDayReport(_UserInfo);
            frm.ShowDialog();
        }

        private void 底层更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProcessBar myProcessBar = new frmProcessBar();
            myProcessBar.ShowDialog();
            return;
           

        }
       

       
    }
}
