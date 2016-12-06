using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.Client.Model;
using SCB.OrderSorting.Client.Work;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SCB.OrderSorting.Client.Model.SortingOrder_EnumManager;

namespace SCB.OrderSorting.Client
{
    /// <summary>
    /// 可以重构，代码太多可以新建类库进行模块分类
    /// </summary>
    public partial class frmOrderSortingWorkNew : Form
    {
        #region 字段
        #region 基本字段
        private static UserInfo _UserInfo=new UserInfo {UserId=123,UserName="刘思乐" };//用户信息
        private static bool _IsLoaded = false;//是否加载完成
        private SoundType_Enum SoundType;
        /// <summary>
        /// 按钮集合
        /// </summary>
        private static List<Button> _ButtonList;
        /// <summary>
        /// 从机信息
        /// </summary>
        private static List<SlaveConfig> _SlaveConfig;
        /// <summary>
        /// 分拣架设置信息
        /// </summary>
        private static List<LatticeSetting> _LatticesettingList;
        #endregion
        #region 线程
        BlockingCollection<ThreadWriteMsg> QueueWrite = new BlockingCollection<Model.ThreadWriteMsg>();
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        Task WriteTask;
        Task ReadTask;
        private static bool _ThreadStop;//是否停止所有线程
        private static int _ThreadSleepTime = 30;//线程休息间隔：30毫秒
        private static ThreadSortOrderManager _ThreadSortOrderManager;
        private object asyncLock=new object();
        List<LatticeSetting> RepeatLacttingList;
        #endregion
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化过于分散，可以重构，统一入口点
        /// </summary>
        public frmOrderSortingWorkNew()
        {
            InitializeComponent();
            _UserInfo = new UserInfo { UserId = 123, UserName = "刘思乐" };
        }
        public frmOrderSortingWorkNew(UserInfo userInfo)
        {
            try
            {
                // this._UserInfo = userInfo;
                _UserInfo = new UserInfo { UserId = 123, UserName = "刘思乐" };
                SoundType = new SoundType_Enum();
                Init_ThreadSortOrderManager();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 初始化_ThreadSortOrderManager
        /// </summary>
        private void Init_ThreadSortOrderManager()
        {
            _ThreadSortOrderManager = new ThreadSortOrderManager();
            string[] result = ConfigurationManager.AppSettings["Scanner"]?.Split(',');
            foreach (string value in result)
            {
                _ThreadSortOrderManager.Add(value, new ThreadSortOrder
                {
                    SortStatus = SortStatus_Enum.None,
                    SortOrderNo = string.Empty,
                    IsStop=false,
                    WaitPutColor = value == "A" ? LED_Enum.Green : LED_Enum.Red
                });
            }
           
        }
        /// <summary>
        /// 加载完成后初始化基础信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLatticeSettingSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() =>
                {
                    //获取从机信息
                    _SlaveConfig = OrderSortService.GetSlaveConfig();
                    //获取格口信息
                    _LatticesettingList = OrderSortService.GetLatticeSettingList();
                    //获取界面按钮
                    _ButtonList = OrderSortService.CreateButtonList(Width, Height);
                }).ContinueWith(cw =>
                {
                    //把按钮显示到界面上
                    _ButtonList.ForEach(btn =>
                    {
                        btn.Click += new EventHandler(button_Click);
                        Invoke((MethodInvoker)delegate () { Controls.Add(btn); });
                    });
                }).ContinueWith(cw => {
                    _IsLoaded = true;
                    Invoke((MethodInvoker)delegate ()
                    {
                        开始分拣ToolStripMenuItem.Enabled = true;
                    });
                });
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 线程
        /// <summary>
        /// 线程启动
        /// </summary>
        private void ThreadRun()
        {
            try
            {
                _ThreadStop = false;
                WriteTask = Task.Factory.StartNew(ThreadWrite, cancelTokenSource.Token);
                ReadTask = Task.Factory.StartNew(ThreadRead, cancelTokenSource.Token);
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 读串口数据线程
        /// </summary>
     
        private  void ThreadRead()
        {
            while (!_ThreadStop)
            {
                if (cancelTokenSource.IsCancellationRequested)
                {
                    break;
                   
                }
                //锁定
                if (QueueWrite.Count > 0 || !_IsLoaded || _SlaveConfig == null || _SlaveConfig.Count < 1)
                {
                    //Invoke((MethodInvoker)delegate ()
                    //{
                    //    lbl_WaitQuq.Text = QueueWrite.Count.ToString();
                    //});
                    Thread.Sleep(_ThreadSleepTime);
                    continue;
                }
                else
                {
                    //Invoke((MethodInvoker)delegate ()
                    //{
                    //    lbl_WaitQuq.Text = QueueWrite.Count.ToString();
                    //});
                }
                try
                {
                    //读光栅
                    foreach (SlaveConfig slave in _SlaveConfig)
                    {
                        var registersGrating = OrderSortService.SerialPortService.ReadGratingRegisters(slave.SlaveAddress);
                        ThreadReadGratingMsg(registersGrating, slave);

                        // 读按钮 先暂时关闭
                        ushort[] registersButton = OrderSortService.SerialPortService.ReadButtonRegisters(slave.SlaveAddress);
                        //if (registersButton.Max() > 0)
                        //{
                        //    SaveErrLogHelper.SaveErrorLog($"按钮数据：", string.Join(",", registersButton.Select(o => o.ToString())));
                        //}
                        ThreadReadButtonMsg(registersButton, slave);
                    }
                }
                catch (Exception ex)
                {
                    SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                    Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
                }
            }
        }
        /// <summary>
        /// 写串口数据线程
        /// </summary>
        private void ThreadWrite()
        {
            try
            {
                foreach (var WriteMsg in QueueWrite.GetConsumingEnumerable())
                {
                    if (cancelTokenSource.IsCancellationRequested)
                    {
                        break;

                    }
                    ThreadWriteMsg(WriteMsg);
                    UpdateSortStatus(WriteMsg.FinishStatus);
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                 Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
            }
        }
        #endregion

        #region 事件
        #region 开始分拣
        private void 开始分拣ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!isCollect())
                {
                    OrderSortService.SoundAsny(SoundType.ConllectError);
                    MessageBox.Show("请检查分拣架设备是否连接正确！");
                    return;
                }
              
                if (WriteTask==null)
                {
                    //启动线程
                    this.ThreadRun();
                }
                SetQueueCount(ReSetCounterType_Enum.Grating2Button);
                SetQueueLED(LED_Enum.None);
                SetQueueWarningLight(LightOperStatus_Enum.Off);
                Init_ThreadSortOrderManager();
                txtOrderId.Enabled = true;
                txtOrderId.Focus();
                SetTipMsg();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 扫描枪扫描事件
        /// <summary>
        /// 扫描枪扫描事件
        /// 可重构，把验证分离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrderId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                #region 获取数据
                //获取订单信息时的返回结果
                string GetOrderInfoResult = string.Empty;
                string orderID2Scaner = txtOrderId.Text;
                #endregion
                #region 验证
                //操作不正确
                if (e.KeyCode != Keys.Enter || string.IsNullOrWhiteSpace(orderID2Scaner) || orderID2Scaner.Length <= 1)
                {
                    txtOrderId.Select();
                    return;
                }
               // ClearOrderId();
                txtOrderId.Text = "";
                Task.Run(delegate ()
                {
                    string orderID = orderID2Scaner.Substring(1, orderID2Scaner.Length - 1);
                    string Scaner = orderID2Scaner.Substring(0, 1);
                    ThreadSortOrder ThreadSortOrder = _ThreadSortOrderManager.Get(Scaner);
                    lock (ThreadSortOrder)//防止同时扫描多个
                    {
                        //找不到扫描枪
                        if (ThreadSortOrder == null)
                        {
                            OrderSortService.SoundAsny(SoundType.ScanSettingError);
                            return;
                        }
                        //繁忙中
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPuting)
                        {
                            OrderSortService.SoundAsny(SoundType.Waiting);
                            return;
                        }
                        //禁止连续扫同一订单
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPut && ThreadSortOrder.SortOrderNo == orderID && ThreadSortOrder.IsStop == false)
                        {
                            //防呆
                            SetQueueLED(ThreadSortOrder.TargetLattice, ThreadSortOrder.WaitPutColor);
                            OrderSortService.SoundAsny(SoundType.OrderScanOver);
                            return;
                        }
                        //禁止同时扫两个订单
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPut && ThreadSortOrder.SortOrderNo != orderID)
                        {
                            OrderSortService.SoundAsny(SoundType.OrderOnlyOne);
                            return;
                        }
                        //重复投递
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.Success && ThreadSortOrder.SortOrderNo == orderID)
                        {
                            RepeatLacttingList = ThreadSortOrder.TargetLattice;
                            SetQueueLED(ThreadSortOrder.TargetLattice, LED_Enum.Yellow, 0, new FinishStatus { SortStatus_Enum = SortStatus_Enum.RepeatError, ThreadSortOrder = ThreadSortOrder });
                            OrderSortService.SoundAsny(SoundType.RepeatError);
                            return;
                        }
                        //重复投递后防止找错（找到未投的订单）
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.RepeatError)
                        {
                            foreach (var TargetLattice in ThreadSortOrder.TargetLattice)
                            {
                                var LatticeOrders = OrderSortService.GetLatticeOrdersListByLatticesettingId(TargetLattice.ID);
                                if (LatticeOrders.Exists(lo => lo.OrderId == orderID))
                                {
                                    OrderSortService.SoundAsny(SoundType.RepeatFindError);
                                    return;
                                }
                            }
                            //暂停解除（重复投递出错的）
                            ThreadSortOrder.IsStop = false;
                        }
                        //投错格口
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.LocationError && ThreadSortOrder.SortOrderNo != orderID)
                        {
                            OrderSortService.SoundAsny(SoundType.LocationError);
                            return;
                        }
                        //格口格档
                        if (ThreadSortOrder.SortStatus == SortStatus_Enum.Blocked)
                        {
                            OrderSortService.SoundAsny(SoundType.Blocked);
                            return;
                        }
                        //暂停解除出错（扫了非对应的解除暂停的订单）
                        if (ThreadSortOrder.IsStop && ThreadSortOrder.SortOrderNo != orderID)
                        {
                            OrderSortService.SoundAsny(SoundType.ScanLockError);
                            return;
                        }
                        //暂停解除（投递位置出错的）
                        if (ThreadSortOrder.IsStop && ThreadSortOrder.SortOrderNo == orderID && ThreadSortOrder.SortStatus == SortStatus_Enum.LocationError)
                        {
                            ThreadSortOrder.IsStop = false;
                            ThreadSortOrder.SortStatus = SortStatus_Enum.WaitPut;
                            OrderSortService.SoundAsny(SoundType.ScanUnLock);
                        }
                        //暂停解除（因当时未投递而被锁的）
                        if (ThreadSortOrder.IsStop && ThreadSortOrder.SortOrderNo == orderID && ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPut)
                        {
                            ThreadSortOrder.IsStop = false;
                            OrderSortService.SoundAsny(SoundType.ScanUnLock);
                            return;
                        }
                        //判断是否都已解除暂停
                        if (_ThreadSortOrderManager.GetOther(Scaner).Exists(o => (o.IsStop == true)))
                        {
                            OrderSortService.SoundAsny(SoundType.ScanLockWait);
                            return;
                        }
                        //获取订单信息(可优化，可改成异步获取)
                        ThreadSortOrder.OrderInfo = OrderSortService.GetOrderInfoById(orderID, _UserInfo, ref GetOrderInfoResult);
                        if (!string.IsNullOrWhiteSpace(GetOrderInfoResult))
                        {
                            OrderSortService.SoundAsny(SoundType.GetOrderError);
                            return;
                        }
                        //获取目标格口
                        ThreadSortOrder.TargetLattice = OrderSortService.GetLatticeSettingByOrderinfoList(ThreadSortOrder.OrderInfo);
                        if (ThreadSortOrder.TargetLattice == null || ThreadSortOrder.TargetLattice.Count < 1)
                        {
                            OrderSortService.SoundAsny(SoundType.GetOrderError);
                            return;
                        }
                        //是否存在其它人没有解锁
                        if (_ThreadSortOrderManager.Get().Exists(o => (o.IsStop == true)  && o.CabinetId != ThreadSortOrder.CabinetId))
                        {
                            OrderSortService.SoundAsny(SoundType.ScanLockWait);
                            return;
                        }
                        //禁止扫其它扫描枪进行中的格口
                        if (_ThreadSortOrderManager.Get().Exists(o => (o.SortStatus == SortStatus_Enum.WaitPut) && o.CabinetId != ThreadSortOrder.CabinetId && o.TargetLattice.Exists(tg => ThreadSortOrder.TargetLattice.Exists(p => p.CabinetId == tg.CabinetId && p.LatticeId == tg.LatticeId))))
                        {
                            OrderSortService.SoundAsny(SoundType.LatticeWait);
                            return;
                        }
                        //格口未满的格口
                        List<LatticeSetting> LatticeSettingNotOver = new List<LatticeSetting>();
                        foreach (LatticeSetting ls in ThreadSortOrder.TargetLattice)
                        {
                            if (!OrderSortService.IsFullLattice(ls.ID))
                            {
                                LatticeSettingNotOver.Add(ls);
                            }
                        }
                        if (LatticeSettingNotOver == null || LatticeSettingNotOver.Count < 1)
                        {
                            SetQueueLED(ThreadSortOrder.TargetLattice, LED_Enum.YellowFlash);
                            OrderSortService.SoundAsny(SoundType.LatticeOver);
                            return;
                        }
                        #endregion

                        ThreadSortOrder.TargetLattice = LatticeSettingNotOver;
                        if (CreateOrderSortingLog(ThreadSortOrder.OrderInfo, ThreadSortOrder.TargetLattice, null, OrderSortingLog_OperationType_Enum.扫描, OrderSortingLog_Status_Enum.待投递))
                        {

                            if (ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPut)
                            {
                                SetQueueCount(ReSetCounterType_Enum.Grating);
                                var latticesetting = _LatticesettingList.Find(o => o.CabinetId == ThreadSortOrder.ResultLattice.CabinetId && o.LatticeId == ThreadSortOrder.ResultLattice.LatticeId);
                                SetQueueLED(latticesetting, LED_Enum.None);
                                SetQueueWarningLight(_SlaveConfig.First().CabinetId, LightOperStatus_Enum.Off);
                            }
                            else if (ThreadSortOrder.SortStatus == SortStatus_Enum.RepeatError)
                            {
                                SetQueueCount(ReSetCounterType_Enum.Grating);
                                SetQueueLED(RepeatLacttingList, LED_Enum.None);
                                SetQueueWarningLight(_SlaveConfig.First().CabinetId, LightOperStatus_Enum.Off);
                            }
                            

                                ThreadSortOrder.SortStatus = SortStatus_Enum.WaitPuting;
                                SetQueueLED(ThreadSortOrder.TargetLattice, ThreadSortOrder.WaitPutColor, 0, new FinishStatus { SortStatus_Enum = SortStatus_Enum.WaitPut, ThreadSortOrder = ThreadSortOrder });
                                ThreadSortOrder.SortOrderNo = orderID;
                                ThreadSortOrder.ResultLattice = null;
                                // SetTipMsg(string.Format("订单号（{0}）已扫描，等待投递...", orderID));
                            
                        }
                        else
                        {
                            OrderSortService.SoundAsny(SoundType.CreateOrderLogError);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //日志记录可异步，可优化
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                txtOrderId.Text = "";
                OrderSortService.SoundAsny(SoundType.NotFindError);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 打开格口设置窗口
        /// <summary>
        /// 打开格口设置窗口
        /// </summary>
        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSortRun())
                {
                    MessageBox.Show("分拣作业运行中，禁止修改格口设置！");
                    return;
                }
                var btn = sender as Button;
                var frm = new frmLatticeSettingEdit(btn.TabIndex);
                txtOrderId.Enabled = false;
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    txtOrderId.Enabled = true;
                    txtOrderId.Focus();
                    _LatticesettingList = OrderSortService.LoadLatticeSetting();
                    var ls = _LatticesettingList.First(l => l.ID == Convert.ToInt32(btn.Name));
                    btn.Text = OrderSortService.GetLatticeNewText(ls);
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 断电恢复
        private void 断电恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否恢复关闭前的状态？") != DialogResult.OK)
                    return;
                RestoringFromPowerOff();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 断电恢复
        /// </summary>
        private void RestoringFromPowerOff()
        {
            try
            {
                ////获取最新的一条分拣记录
                //var log = OrderSortService.GetTheLastOrderSortingLog();
                //if (log != null && log.Status == 3)
                //{
                //    switch (log.Status)
                //    {
                //        case 3://3投递异常
                //            //目标柜格，亮绿灯
                //            _ThreadSortOrderManager.Get().First().TargetLattice = _LatticesettingList.FindAll(s => s.CabinetId == Convert.ToInt32(log.TargetCabinetId) && s.LatticeId == log.TargetLatticeId);
                //            SetQueueLED(_ThreadSortOrderManager.TargetLattice, LED_Enum.Green);
                //            //投入柜格，亮红灯
                //            _ThreadSortOrderManager.ResultLattice = _LatticesettingList.Find(s => s.CabinetId == Convert.ToInt32(log.ResultCabinetId) && s.LatticeId == log.ResultLatticeId);
                //            SetQueueLED(_ThreadSortOrderManager.TargetLattice, LED_Enum.RedNoFlash,0,SortStatus_Enum.LocationError);

                //            _ThreadSortOrderManager.SortOrderNo = log.OrderId;
                //            //_ThreadSortOrderManager.SortStatus = SortStatus_Enum.LocationError;
                //            SetTipMsg(string.Format("订单号({0})投放错误!请取出重新扫描！", _ThreadSortOrderManager.SortOrderNo));
                //            break;
                //        case 4://4重复扫描
                //            _ThreadSortOrderManager.SortOrderNo = log.OrderId;
                //            //目标柜格，亮绿灯
                //            _ThreadSortOrderManager.TargetLattice = _LatticesettingList.FindAll(s => s.CabinetId == Convert.ToInt32(log.TargetCabinetId) && s.LatticeId == log.TargetLatticeId);
                //             SetQueueLED(_ThreadSortOrderManager.TargetLattice, LED_Enum.RedNoFlash,0,SortStatus_Enum.RepeatError);
                //            //_ThreadSortOrderManager.SortStatus = SortStatus_Enum.RepeatError;
                //            break;
                //        default:
                //            break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 全部清除重扫
        private void 全部清除重扫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("请先把分拣架内的货物全部取回！") != DialogResult.OK)
            //    return;
            //OrderSortService.ClearLatticeOrdersCache();
            //SetQueueCount(ReSetCounterType_Enum.Grating2Button);
            //SetQueueLEDClear();
            //SetQueueWarningLight(LightOperStatus_Enum.Off);
            //Init_ThreadSortOrderManager();
            //SetTipMsg("清除重扫成功,正等待扫描枪扫入...");
            //txtOrderId.Text = "";
            //txtOrderId.Focus();
        }
        #endregion
        #region PKG条码重打
        private void pKG条码重打ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (isSortRun())
                {
                    MessageBox.Show("分拣作业运行中，禁止重打！");
                    return;
                }
                new frmPkgReprint().ShowDialog();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 新建方案
        private void 新建方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSortRun())
                {
                    MessageBox.Show("分拣作业运行中，禁止新建方案！");
                    return;
                }
                if (new frmCreateNewSolution().ShowDialog() == DialogResult.OK)
                {
                    _LatticesettingList = OrderSortService.LoadLatticeSetting();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 方案重命名
        private void 方案重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSortRun())
                {
                    MessageBox.Show("分拣作业运行中，禁止方案重命名！");
                    return;
                }
                if (new frmRenameSolution().ShowDialog() == DialogResult.OK)
                {
                    _LatticesettingList = OrderSortService.LoadLatticeSetting();
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 分拣撤回
        private void 分拣撤回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSortRun())
                {
                    MessageBox.Show("分拣作业运行中，禁止方案重命名！");
                    return;
                }
                new frmDeleteOrderCache().ShowDialog();
                //更新格口统计信息
                _ButtonList.ForEach(btn =>
                {
                    var lattice = _LatticesettingList.Find(ls => ls.ID == btn.TabIndex);
                    btn.Text = OrderSortService.GetLatticeNewText(lattice);
                });
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region pKG标签打印
        private void pKG标签打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSortRun())
                {
                    MessageBox.Show("分拣作业运行中，禁止重打！");
                    return;
                }
                new frmPkgPrint(_UserInfo).ShowDialog();
                //更新格口统计信息
                _ButtonList.ForEach(btn =>
                {
                    var lattice = _LatticesettingList.Find(ls => ls.ID == btn.TabIndex);
                    btn.Text = OrderSortService.GetLatticeNewText(lattice);
                });
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 窗口关闭事件
        private void frmOrderSortingWorkNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ThreadStop = true;
            cancelTokenSource.Cancel();
           
        }
        #endregion
        #region 窗口大小改变事件
        private void frmLatticeSettingSearch_SizeChanged(object sender, EventArgs e)
        {
            if (_IsLoaded && _ButtonList != null)
            {
                //窗口大小改变后，重新设定按钮的大小
                var buttonPointList = OrderSortService.CreateButtonPointModelList(Width, Height);
                _ButtonList.ForEach(btn =>
                {
                    var bpc = buttonPointList.Find(bp => bp.LatticesettingId == Convert.ToInt32(btn.Name));
                    btn.Location = bpc.ButtonLocation;
                    btn.Size = bpc.ButtonSize;
                    btn.Font = bpc.ButtonFont;
                });
            }
        }
        #endregion
        #endregion

        #region 方法
        private bool isCollect()
        {
            try
            {
               lblMsg.Text = "连接设备中....";

                bool isSuccess= OrderSortService.isCollect();
                if (isSuccess)
                {
                    return true;
                }else
                {
                    lblMsg.Text = "连接失败....";
                    return false;
                }
            }
            catch
            {
                lblMsg.Text = "连接失败....";
                return false;
            }
        }
        #region 串口操作
        /// <summary>
        /// 重置计数器
        /// </summary>
        /// <param name="OperType">重置计数器类型</param>
        private void SetPortCount(ReSetCounterType_Enum CounterType, SlaveConfig slave, ushort Index,bool isCheck)
        {
            try
            {
                switch (CounterType)
                {
                    case ReSetCounterType_Enum.Grating:
                        //if (_ThreadSortOrderManager.Get().Exists(o => o.SortStatus == SortStatus_Enum.Blocked))
                        //{
                        //    OrderSortService.TCPPortManage.ClearGrating2Button();
                        //    return;
                        //}
                         if (slave == null)
                        {
                            OrderSortService.SerialPortService.ClearGratingRegister();
                        }
                        else if (Index != ushort.MaxValue)
                        {
                             OrderSortService.SerialPortService.ClearGratingRegister(slave.SlaveAddress, Index , isCheck);
                        }
                        else
                        {
                             OrderSortService.SerialPortService.ClearGratingRegister(slave.SlaveAddress);
                        }
                        break;
                    case ReSetCounterType_Enum.Button:
                        if (slave == null)
                        {
                            OrderSortService.SerialPortService.ClearButtonRegister();
                        }
                        else if (Index != ushort.MaxValue)
                        {
                            OrderSortService.SerialPortService.ClearButtonRegister(slave.SlaveAddress, Index);
                        }
                        else
                        {
                            OrderSortService.SerialPortService.ClearButtonRegister(slave.SlaveAddress);
                        }
                        break;
                    case ReSetCounterType_Enum.Grating2Button:
                        if (slave == null)
                        {
                            OrderSortService.SerialPortService.ClearGrating2Button();
                        }
                        else
                        {
                            OrderSortService.SerialPortService.ClearGrating2Button(slave.SlaveAddress);
                        }
                       
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
            }
        }
        /// <summary>
        /// 设置LED颜色
        /// </summary>
        /// <param name="LatticeSetting"></param>
        /// <param name="LED"></param>
        private void SetPortLED(LatticeSetting LatticeSetting, LED_Enum LED)
        {
            OrderSortService.SerialPortService.SetLED(GetSlaveAddress(LatticeSetting.CabinetId), LatticeSetting.LEDIndex, (ushort)LED);
        }

      
        private void SetPortLED(int cabinetId, int LEDIndex, LED_Enum LED)
        {
            OrderSortService.SerialPortService.SetLED(GetSlaveAddress(cabinetId), LEDIndex, (ushort)LED);
        }
        private void SetPortLED(int cabinetId, List<LEDChange> LEDChangeList)
        {
            OrderSortService.SerialPortService.SetLED(GetSlaveAddress(cabinetId), LEDChangeList.Select(o => new KeyValuePair<int, ushort>(o.LEDIndex, (ushort)o.LED)).ToList());
        }
        private void SetPortWarningLight(WarningLight_Enum WarningLight, LightOperStatus_Enum LightOperStatus)
        {
            //switch (WarningLight)
            //{
            //    case WarningLight_Enum.Green:
            //        OrderSortService.SerialPortService.SetWarningLightGreen((ushort)LightOperStatus);
            //        break;
            //    case WarningLight_Enum.Red:
            //        OrderSortService.SerialPortService.SetWarningLightRed((ushort)LightOperStatus);
            //        break;
            //    case WarningLight_Enum.WarningSound:
            //        OrderSortService.SerialPortService.SetWarningTwinkle((ushort)LightOperStatus);
            //        break;
            //    case WarningLight_Enum.Yellow:
            //        OrderSortService.SerialPortService.SetWarningLightYellow((ushort)LightOperStatus);
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion
        #region 队列
        /// <summary>
        /// 设置队列警示灯
        /// </summary>
        /// <param name="status"></param>
        private void SetQueueWarningLight( LightOperStatus_Enum status, FinishStatus FinishStatus = null)
        {
            //_ThreadManageList.ForEach(o => 
            // SetQueueWarningLight(o.SlaveConfig.CabinetId,  status,  FinishStatus)
            //);
           
        }
        private void SetQueueWarningLight(int CabinetId, LightOperStatus_Enum status, FinishStatus FinishStatus = null)
        {
            
               // var QueueWrite = _ThreadManageList.Find(o => o.SlaveConfig.CabinetId == CabinetId).QueueWrite;
                QueueWrite.Add(
                    new ThreadWriteMsg
                    {
                        FinishStatus = FinishStatus,
                        WriteType = ThreadWriteType_Enum.WarningLight,
                        WarningLight = new WriteWarningLight
                        {
                            WarningLight = WarningLight_Enum.WarningSound,
                            LightOperStatus = status
                        }
                    });
            
            //if (FinishStatus != null) QueueWrite.CompleteAdding();
        }
        /// <summary>
        /// 设置队列LED
        /// </summary>
        /// <param name="LatticeSetting"></param>
        /// <param name="status"></param>
        private void SetQueueLED(LED_Enum status, int milliseconds = 0, FinishStatus FinishStatus = null)
        {
            SetQueueLED(_LatticesettingList, status, milliseconds, FinishStatus);
        }
        private void SetQueueLED(LatticeSetting LatticeSetting, LED_Enum status, int milliseconds = 0, FinishStatus FinishStatus = null)
        {
            List<LatticeSetting> LatticeSettingList = new List<DAL.LatticeSetting> { LatticeSetting };
            SetQueueLED( LatticeSettingList, status, milliseconds, FinishStatus);
        }
        private void SetQueueLED( List<LatticeSetting> LatticeSettingList, LED_Enum status, int milliseconds = 0, FinishStatus FinishStatus = null)
        {
                if (LatticeSettingList == null || LatticeSettingList.Count < 1) return;
                //格式化数据
                var LatticeSettingFormat = LatticeSettingList.GroupBy(o => o.CabinetId)
                              .Select(o => new WriteLED
                              {
                                  CabinetId = o.FirstOrDefault().CabinetId,
                                  LEDChangeList = o.Select(p =>
                                      new LEDChange
                                      {
                                          ID = p.ID,
                                          LED = status,
                                          LEDIndex = p.LEDIndex
                                      }).ToList()
                              }).ToList();

                LatticeSettingFormat.ForEach(o =>
                  QueueWrite.Add(
                     new ThreadWriteMsg
                     {
                         FinishStatus = FinishStatus,
                         WriteType = ThreadWriteType_Enum.LED,
                         ThreadSleep = milliseconds,
                         LED = new WriteLED
                         {
                             CabinetId = o.CabinetId,
                             ClearAll = false,
                             LEDChangeList = o.LEDChangeList
                         }
                     })
                );
            
          //  if (FinishStatus != null) _ThreadManageList.Find(p => p.SlaveConfig.CabinetId == LatticeSettingList.First().CabinetId).QueueWrite.CompleteAdding();
        }
        /// <summary>
        /// 设置队列计数器
        /// </summary>
        /// <param name="status"></param>
        private void SetQueueCount(ReSetCounterType_Enum status, bool isCheck = true, FinishStatus FinishStatus = null)
        {
            _SlaveConfig.ForEach(o =>
                SetQueueCount(status, o, isCheck, FinishStatus)
            );
            
        }
        private void SetQueueCount( ReSetCounterType_Enum status, SlaveConfig slave,bool isCheck=true, FinishStatus FinishStatus = null)
        {
            SetQueueCount(status, slave, ushort.MaxValue, isCheck, FinishStatus);
        }
        private void SetQueueCount(ReSetCounterType_Enum status, SlaveConfig slave, ushort Index, bool isCheck = true, FinishStatus FinishStatus = null)
        {
           
                if (slave == null) return;
                    QueueWrite.Add(
                         new ThreadWriteMsg
                         {
                             FinishStatus = FinishStatus,
                             WriteType = ThreadWriteType_Enum.ReSetCount,
                             ReSetCount = new WriteReSetCount
                             {
                                 ReSetCounterType = status,
                                 Slave = slave,
                                 Index = Index,
                                 isCheck= isCheck
                             }
                         });
          //  if (FinishStatus != null) QueueWrite.QueueWrite.CompleteAdding();
        }
        #endregion
        #region 线程读写信息
        /// <summary>
        /// 线程写数据到串口
        /// </summary>
        /// <param name="msg"></param>
        private void ThreadWriteMsg(ThreadWriteMsg msg)
        {
            try
            {
                switch (msg.WriteType) 
                {
                    case ThreadWriteType_Enum.LED:
                        if (msg.LED.ClearAll)
                        {
                            //作废
                        }
                        else
                        {
                            SetPortLED(msg.LED.CabinetId, msg.LED.LEDChangeList);
                            //UI线程卡 暂时关闭
                            Invoke((MethodInvoker)delegate ()
                            {
                                _ButtonList.Where(o => msg.LED.LEDChangeList.Select(p => p.ID)
                                           .Contains(o.TabIndex))
                                           .ToList()
                                           .ForEach(b => b.BackColor = GetColor(msg.LED.LEDChangeList.First().LED));
                            });
                        }
                        break;
                    case ThreadWriteType_Enum.ReSetCount:
                        SetPortCount(msg.ReSetCount.ReSetCounterType, msg.ReSetCount.Slave, msg.ReSetCount.Index, msg.ReSetCount.isCheck);
                        break;
                    case ThreadWriteType_Enum.WarningLight:
                        SetPortWarningLight(msg.WarningLight.WarningLight, msg.WarningLight.LightOperStatus);
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
            }
        }
        /// <summary>
        /// 线程读串口数据

        /// </summary>
        private void ThreadReadGratingMsg(ushort[] registers, SlaveConfig slave)
        {
            try
            {
               
                foreach (ThreadSortOrder ThreadSortOrder in _ThreadSortOrderManager.Get())
                {
                    if (ThreadSortOrder.SortStatus == SortStatus_Enum.Blocked && ThreadSortOrder.CabinetId != slave.CabinetId)
                    {
                        return;
                    }
                    //解除阻挡
                    if (ThreadSortOrder.SortStatus == SortStatus_Enum.Blocked && registers.Max() > 0 && ThreadSortOrder.CabinetId == slave.CabinetId)
                    {

                       // SaveErrLogHelper.SaveErrorLog("解除阻挡",string.Join(",", registers.Select(o=>o.ToString())));
                        SetQueueCount(ReSetCounterType_Enum.Grating, slave,false);
                        return;
                    }
                    if (ThreadSortOrder.SortStatus == SortStatus_Enum.Blocked && registers.Max() < 1 && ThreadSortOrder.CabinetId == slave.CabinetId)
                    {
                       // SaveErrLogHelper.SaveErrorLog("", "已解除");
                        SetQueueLED(ThreadSortOrder.ResultLattice,LED_Enum.None,0, new FinishStatus { SortStatus_Enum = SortStatus_Enum.BackPreStatus, ThreadSortOrderList = _ThreadSortOrderManager.Get().ToList() });
                        SetQueueWarningLight(slave.CabinetId, LightOperStatus_Enum.Off);
                        ThreadSortOrder.CabinetId = 0;
                        // SetTipMsg(string.Format("{0}柜{1}已解除格挡", ThreadSortOrder.ResultLattice.CabinetId, ThreadSortOrder.ResultLattice.LatticeId));
                        OrderSortService.SoundAsny(SoundType.UnBlocked);
                       // ClearOrderId();
                        return;
                    }
                }
                for (int i = 0; i < registers.Length; i++)
                {
                    //是否有包裹投入
                    if (registers[i] < 1)
                    {
                        continue;
                    }
                   
                    //未扫描投递--投递成功时投递到了格口边挡住了，但线程在500ms内执行时会判断认为未投递
                    //if (registers[i] < 500 && _ThreadSortOrderManager.SortStatus == SortStatus_Enum.None || _ThreadSortOrderManager.SortStatus == SortStatus_Enum.Success || _ThreadSortOrderManager.SortStatus == SortStatus_Enum.OverWeight)
                    //{
                    //    _ThreadSortOrderManager.SortStatus = SortStatus_Enum.NotScanAndPut;
                    //    _ThreadSortOrderManager.ResultLattice = _LatticesettingList.Find(lsc => lsc.GratingIndex == i && lsc.CabinetId == slave.CabinetId);
                    //    SetQueueLED(_ThreadSortOrderManager.ResultLattice, LED_Enum.RedNoFlash);
                    //    SetQueueWarningLight(LightOperStatus_Enum.On);
                    //    Thread.Sleep(30);
                    //    Invoke((MethodInvoker)delegate ()
                    //    {
                    //        frmMessage frmMessage = new frmMessage("请先扫描后再投递！");
                    //        SetTipMsg("请先扫描后再投递！");

                    //        DialogResult result = frmMessage.ShowDialog();
                    //        if (result == DialogResult.OK)
                    //        {
                    //            SetTipMsg();
                    //            _ThreadSortOrderManager.SortStatus = SortStatus_Enum.Success;
                    //            ClearOrderId();
                    //            txtOrderId.Focus();
                    //            SetQueueLED(_ThreadSortOrderManager.ResultLattice, LED_Enum.None);
                    //            SetQueueWarningLight(LightOperStatus_Enum.Off);
                    //            SetQueueCount(ReSetCounterType_Enum.Grating);
                    //        }
                    //    });
                    //}
                    //阻挡
                    else if ( registers[i] >= 200)
                    {
                        ThreadSortOrder CurThreadSortOrder = _ThreadSortOrderManager.Get().Find(o => 
                            !(o.SortStatus == SortStatus_Enum.Success|| o.SortStatus == SortStatus_Enum.None)
                            && o.TargetLattice!=null 
                            &&o.TargetLattice.Exists(p => p.GratingIndex == i && p.CabinetId == slave.CabinetId));
                        if (CurThreadSortOrder != null) continue;
                       
                        bool isBlock = false;
                        foreach (ThreadSortOrder ThreadSortOrder in _ThreadSortOrderManager.Get())
                        {
                            if (ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPut || ThreadSortOrder.SortStatus == SortStatus_Enum.None || ThreadSortOrder.SortStatus == SortStatus_Enum.Success || ThreadSortOrder.SortStatus == SortStatus_Enum.OverWeight)
                            {
                                ThreadSortOrder.ResultLattice = _LatticesettingList.Find(lsc => lsc.GratingIndex == i && lsc.CabinetId == slave.CabinetId);
                                if (!isBlock)
                                {
                                    SetQueueLED(ThreadSortOrder.ResultLattice, LED_Enum.Yellow);
                                    SetQueueWarningLight(slave.CabinetId,LightOperStatus_Enum.On, new FinishStatus { SortStatus_Enum = SortStatus_Enum.Blocked, ThreadSortOrderList = _ThreadSortOrderManager.Get().ToList() });
                                }
                                ThreadSortOrder.CabinetId = slave.CabinetId;
                               // SetTipMsg(string.Format("{0}柜{1}被阻挡！", ThreadSortOrder.ResultLattice.CabinetId, ThreadSortOrder.ResultLattice.LatticeId));
                                OrderSortService.SoundAsny(SoundType.Blocked);
                               // ClearOrderId();
                                isBlock = true;
                            }
                        }
                        // _ThreadSortOrderManager.SortStatus = SortStatus_Enum.Blocked;
                        // SetQueueCount(ReSetCounterType_Enum.Grating);
                    }
                    
                    //正确投递
                    else if (_ThreadSortOrderManager.Get().Exists(o => (o.SortStatus == SortStatus_Enum.WaitPut || !_ThreadSortOrderManager.Get().Exists(p => p.SortStatus != SortStatus_Enum.LocationError)) && o.TargetLattice.Exists(p => p.GratingIndex == i && p.CabinetId == slave.CabinetId)))
                    {
                        Debug.WriteLine("正确投递："+string.Join(",", registers.Select(o => o.ToString())));
                        //sw.Restart();
                        var ResultLattice = _LatticesettingList.Find(lsc => lsc.GratingIndex == i && lsc.CabinetId == slave.CabinetId);
                        
                        ThreadSortOrder ThreadSortOrder = _ThreadSortOrderManager.Get().Find(o => o.SortStatus == SortStatus_Enum.WaitPut && o.TargetLattice.Exists(p => p.GratingIndex == i && p.CabinetId == slave.CabinetId));
                        ThreadSortOrder.ResultLattice = ResultLattice;

                        // SaveErrLogHelper.SaveErrorLog("issuccess", sw.ElapsedMilliseconds.ToString());

                        //  SaveErrLogHelper.SaveErrorLog("clear led", sw.ElapsedMilliseconds.ToString());



                       
                        SetQueueCount(ReSetCounterType_Enum.Grating, slave, (ushort)i,true, new FinishStatus { SortStatus_Enum = SortStatus_Enum.Success, ThreadSortOrder = ThreadSortOrder });

                        SetQueueLED(ThreadSortOrder.TargetLattice, LED_Enum.None, 0);
                        //  SaveErrLogHelper.SaveErrorLog("clear count", sw.ElapsedMilliseconds.ToString());
                        UpdateButtonList(ResultLattice);
                        //SetQueueCount(ReSetCounterType_Enum.Grating);
                        // _ThreadSortOrderManager.SortStatus = SortStatus_Enum.Success;
                       // SetTipMsg(string.Format("订单号（{0}）投递成功！", ThreadSortOrder.SortOrderNo));
                       // ClearOrderId();
                        bool isSuccess = CreateOrderSortingLog(ThreadSortOrder.OrderInfo, ResultLattice, ResultLattice, OrderSortingLog_OperationType_Enum.投递, OrderSortingLog_Status_Enum.已投递);
                        if (isSuccess)
                        {
                            
                        }
                        else
                        {
                            // SetTipMsg(string.Format("创建扫描投递记录失败！请联系客服", ThreadSortOrder.SortOrderNo));
                            OrderSortService.SoundAsny(SoundType.CreateOrderSortingLogError);
                          //  ClearOrderId();
                        }
                    }
                    //投递错误
                    else if (_ThreadSortOrderManager.Get().Exists(o => o.SortStatus == SortStatus_Enum.WaitPut) && !_ThreadSortOrderManager.Get().Exists(o => o.SortStatus == SortStatus_Enum.WaitPut
                            &&  o.TargetLattice.Exists(p => p.GratingIndex == i && p.CabinetId == slave.CabinetId))
                            )
                    {
                        Debug.WriteLine("投递出错："+string.Join(",", registers.Select(o => o.ToString())));

                        var ResultLattice = _LatticesettingList.Find(lsc => lsc.GratingIndex == i && lsc.CabinetId == slave.CabinetId);
                        _ThreadSortOrderManager.SetResultLatticeAll(ResultLattice);
                        List<ThreadSortOrder> ThreadSortOrderList = _ThreadSortOrderManager.Get().FindAll(o => o.SortStatus == SortStatus_Enum.WaitPut);
                        bool isSuccess = CreateErrorOrderSortingLog(ThreadSortOrderList, OrderSortingLog_OperationType_Enum.投递);
                        if (isSuccess)
                        {
                            //SetQueueWarningLight(slave.CabinetId, LightOperStatus_Enum.On);
                            SetQueueCount( ReSetCounterType_Enum.Grating, slave, (ushort)i);
                            SetQueueLED(ResultLattice, LED_Enum.Yellow, 0, new FinishStatus { SortStatus_Enum = SortStatus_Enum.LocationError, ThreadSortOrderList = ThreadSortOrderList });
                            //_ThreadSortOrderManager.SortStatus = SortStatus_Enum.LocationError;
                            // SetTipMsg(string.Format("投放错误或被格挡!请重新扫描！"));
                            OrderSortService.SoundAsny(SoundType.LocationingError);
                          //  ClearOrderId();
                        }
                        else
                        {
                           // SetTipMsg(string.Format("创建扫描投递记录失败！请联系客服"));
                            OrderSortService.SoundAsny(SoundType.CreateErrorOrderSortingLogError);
                          //  ClearOrderId();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
                OrderSortService.SoundAsny(SoundType.NotFindError); 
                //SetTipMsg("发生了未知异常。ThreadReadMsg：" + string.Join(",", registers.Select(s => s.ToString()).ToArray()));
                ClearOrderId();
                SetQueueCount( ReSetCounterType_Enum.Grating);
            }

        }
        private void ThreadReadButtonMsg(ushort[] registers, SlaveConfig slave)
        {
            try
            {
                for (int i = 0; i < registers.Length; i++)
                {
                    //是否有包裹投入
                    if (registers[i] < 10)
                    {
                        continue;
                    }


                    var resultLatticesetting = _LatticesettingList.Find(lsc => lsc.CabinetId == slave.CabinetId && lsc.ButtonIndex == i);
                    //1.打印的格口处于待投递或重复投递状态时
                    //2.投递错误且投到了该格口
                    if (_ThreadSortOrderManager.Get().Exists(o => (o.SortStatus == SortStatus_Enum.WaitPut || o.SortStatus == SortStatus_Enum.RepeatError) && o.TargetLattice.Exists(p => p.CabinetId == resultLatticesetting.CabinetId && p.LatticeId == resultLatticesetting.LatticeId)) || _ThreadSortOrderManager.Get().Exists(o => o.SortStatus == SortStatus_Enum.LocationError && o.ResultLattice.CabinetId == resultLatticesetting.CabinetId && o.ResultLattice.LatticeId == resultLatticesetting.LatticeId))
                    {
                        //SetTipMsg("分拣作业运行中，请先操作完成后再打印！");
                        // SetMessageBox("分拣作业运行中，请先操作完成后再打印！");
                        OrderSortService.SoundAsny(SoundType.ButtonWait);
                        SetQueueCount(ReSetCounterType_Enum.Button, slave, (ushort)i);
                        return;
                    }
                    ThreadSortOrder ThreadSortOrder = GetThreadSortOrder(resultLatticesetting);
                    //创建打包记录

                    if (ThreadSortOrder == null || ThreadSortOrder.SortStatus == SortStatus_Enum.None || ThreadSortOrder.SortStatus == SortStatus_Enum.Success || ThreadSortOrder.SortStatus == SortStatus_Enum.OverWeight)
                    {
                        var packingLog = OrderSortService.CreatePackingLog(ThreadSortOrder.ResultLattice, _UserInfo, 3);
                        if (packingLog != null)
                        {
                            new PackingLabelPrintDocument().PrintSetup(packingLog);
                            UpdateButtonList(ThreadSortOrder.ResultLattice);
                        }
                        else
                        {
                            
                          //  SetTipMsg(string.Format("创建打包记录失败！请联系客服"));
                            OrderSortService.SoundAsny(SoundType.CreatePackingLogError);
                        }
                    }
                    else
                    {
                       // SetTipMsg(string.Format("请先操作完成后在打包"));
                         OrderSortService.SoundAsny(SoundType.ButtonWait);
                    }
                    SetQueueCount(ReSetCounterType_Enum.Button, slave, (ushort)i);
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
                SetQueueCount(ReSetCounterType_Enum.Button, slave);
                //SetTipMsg("发生了未知异常。ThreadReadButtonMsg：" + string.Join(",", registers.Select(s => s.ToString()).ToArray()));
            }
            finally
            {

            }
        }

        private ThreadSortOrder GetThreadSortOrder(LatticeSetting latticeSetting)
        {
            return _ThreadSortOrderManager.Get().Find(o => o.TargetLattice.Exists(p => p.LatticeId == latticeSetting.LatticeId && p.CabinetId == latticeSetting.CabinetId));
        }
        #endregion
        #region 创建扫描投递记录
        /// <summary>
        /// 创建扫描投递记录
        /// </summary>
        /// <param name="OrderInfo"></param>
        /// <param name="targetLattice"></param>
        /// <param name="resultLattice"></param>
        /// <param name="operationType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool CreateOrderSortingLog(OrderInfo OrderInfo, LatticeSetting targetLattice, LatticeSetting resultLattice, OrderSortingLog_OperationType_Enum operationType, OrderSortingLog_Status_Enum status)
        {
            return OrderSortService.CreateOrderSortingLog(OrderInfo, targetLattice, resultLattice, _UserInfo, (int)operationType, (int)status);
        }
        private bool CreateErrorOrderSortingLog(List<ThreadSortOrder> ThreadSortOrderList, OrderSortingLog_OperationType_Enum operationType)
        {
            return OrderSortService.CreateErrorOrderSortingLog(ThreadSortOrderList.Select(o => new SCB.OrderSorting.BLL.Model.CacheModel.ThreadSortOrder
            {
                OrderInfo = o.OrderInfo,
                ResultLattice = o.ResultLattice,
                TargetLattice = o.TargetLattice
            }).ToList(), _UserInfo, (int)operationType);
        }

        private bool CreateOrderSortingLog(OrderInfo OrderInfo, List<LatticeSetting> targetLatticeList, LatticeSetting resultLattice, OrderSortingLog_OperationType_Enum operationType, OrderSortingLog_Status_Enum status)
        {
            foreach (LatticeSetting ls in targetLatticeList)
            {
                if (!OrderSortService.CreateOrderSortingLog(OrderInfo, ls, resultLattice, _UserInfo, (int)operationType, (int)status))
                {
                    return false;
                }
            }
            return true;
        }
       
        #endregion
        #region 设置提示消息
        /// <summary>
        /// 设置提示消息  
        /// </summary>
        /// <param name="msg">消息内容</param>
        private void SetTipMsg(string msg = "正等待扫描枪扫入....")
        {
            Invoke((MethodInvoker)delegate ()
            {
                lblMsg.Text = msg;

            });
        }
        private void SetMessageBox(string msg)
        {
            Invoke((MethodInvoker)delegate ()
            {
                MessageBox.Show(msg);
            });
        }
        #endregion
        #region 更新格口统计信息
        /// <summary>
        /// 更新格口统计信息
        /// </summary>
        private void UpdateButtonList(LatticeSetting LatticeSetting)
        {
            if (LatticeSetting == null) return;
            Invoke((MethodInvoker)delegate ()
            {
                _ButtonList.FirstOrDefault(b => b.TabIndex == LatticeSetting.ID).Text = OrderSortService.GetLatticeNewText(LatticeSetting);
            });
        }

        #endregion
        #region 获取颜色
        /// <summary>
        /// GetColor（status：0绿灯，1红灯，2黄,>=3恢复）
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private Color GetColor(LED_Enum LED)
        {
            switch (LED)
            {
                case LED_Enum.Green:
                    return Color.Lime;
                case LED_Enum.Yellow:
                case LED_Enum.YellowFlash:
                    return Color.Yellow;
                case LED_Enum.Red:
                    return Color.Red;
                case LED_Enum.None:
                    return Color.Gainsboro;
                default:
                    return Color.Gainsboro;
            }
        }
        #endregion
        #region 根据从机号获取从机地址
        private byte GetSlaveAddress(int CabinetId)
        {
            return _SlaveConfig.Find(o => o.CabinetId == CabinetId).SlaveAddress;
        }
        #endregion
        #region 清除订单号
        private void ClearOrderId()
        {
            Invoke((MethodInvoker)delegate ()
            {
                txtOrderId.Text = "";
            });
        }
        #endregion
        #region 分拣作业是否运行中
        /// <summary>
        /// 分拣作业是否运行中
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool isSortRun()
        {
            foreach (ThreadSortOrder ThreadSortOrder in _ThreadSortOrderManager.Get())
            {
                if (ThreadSortOrder.SortStatus == SortStatus_Enum.Blocked || ThreadSortOrder.SortStatus == SortStatus_Enum.LocationError || ThreadSortOrder.SortStatus == SortStatus_Enum.NotScanAndPut || ThreadSortOrder.SortStatus == SortStatus_Enum.RepeatError || ThreadSortOrder.SortStatus == SortStatus_Enum.WaitPut|| ThreadSortOrder.SortStatus == SortStatus_Enum.Stop)
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region 更新分拣状态
        /// <summary>
        /// 更新分拣状态
        /// </summary>
        /// <param name="SortStatus_Enum"></param>
        private void UpdateSortStatus(FinishStatus FinishStatus)
        {
            if (FinishStatus == null) return;

            List<ThreadSortOrder> ThreadList = new List<ThreadSortOrder>();
            List<ThreadSortOrder> ThreadListOther = new List<ThreadSortOrder>();
            if (FinishStatus.ThreadSortOrderList != null)
            {
                ThreadList = _ThreadSortOrderManager.Get().FindAll(o => FinishStatus.ThreadSortOrderList.Exists(p => p.SortOrderNo == o.SortOrderNo));
                ThreadListOther = _ThreadSortOrderManager.Get().FindAll(o => FinishStatus.ThreadSortOrderList.Exists(p => p.SortOrderNo != o.SortOrderNo));
            }
            else if (FinishStatus.ThreadSortOrder != null)
            {
                ThreadList = _ThreadSortOrderManager.Get().FindAll(o => o.SortOrderNo == FinishStatus.ThreadSortOrder?.SortOrderNo);
                ThreadListOther = _ThreadSortOrderManager.Get().FindAll(o => o.SortOrderNo != FinishStatus.ThreadSortOrder?.SortOrderNo);
            }

            switch (FinishStatus.SortStatus_Enum)
            {
                case SortStatus_Enum.Success:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.Success;
                    });
                  
                    break;
                case SortStatus_Enum.LocationError:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.LocationError;
                        o.IsStop = true;
                    });
                    ThreadListOther.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.IsStop =  o.SortStatus== SortStatus_Enum.LocationError;
                    });
                   // SaveErrLogHelper.SaveErrorLog(string.Empty, "LocationError");
                    break;
                case SortStatus_Enum.OverWeight:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.OverWeight;
                    });
                    break;
                case SortStatus_Enum.RepeatError:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.RepeatError;
                        o.IsStop = ThreadListOther.Exists(p => p.SortStatus == SortStatus_Enum.WaitPut);
                    });
                    ThreadListOther.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.IsStop= o.SortStatus==SortStatus_Enum.WaitPut;
                    });
                    break;
                case SortStatus_Enum.None:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.None;
                    });
                    break;
                case SortStatus_Enum.Blocked:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                       
                        // o.IsStop = o.SortStatus == SortStatus_Enum.WaitPut;
                    });
                    ThreadList.ForEach(o => {
                       
                        o.SortStatus = SortStatus_Enum.Blocked;
                       // o.IsStop = o.SortStatus == SortStatus_Enum.WaitPut;
                    });
                   
                   
                    break;
                case SortStatus_Enum.NotScanAndPut:
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.NotScanAndPut;
                    });
                    ThreadListOther.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.Stop;
                    });
                    break;
                case SortStatus_Enum.WaitPut:
                    
                    ThreadList.ForEach(o => {
                        o.PreSortStatus = o.SortStatus;
                        o.SortStatus = SortStatus_Enum.WaitPut;
                    });
                    
                    // SaveErrLogHelper.SaveErrorLog("on led", sw.ElapsedMilliseconds.ToString());
                    break;
                case SortStatus_Enum.BackPreStatus:
                    ThreadList.ForEach(o => {
                        o.SortStatus = o.PreSortStatus;
                    });
                    break;
                default:
                    break;
            }
           
        }
        #endregion

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            _ThreadSortOrderManager.Get().ForEach(
                o => {
                    result += $"当前订单：{o.SortOrderNo};状态：{o.SortStatus};之前状态:{o.PreSortStatus};是否暂停：{o.IsStop},结果格口：{o.ResultLattice?.ButtonIndex},目标格口:";
                    result += o.TargetLattice == null ? "" : string.Join(",", o.TargetLattice?.Select(p => p.ButtonIndex.ToString()));
                    result += ".........";
                });
            MessageBox.Show(result);
        }
    }
}
