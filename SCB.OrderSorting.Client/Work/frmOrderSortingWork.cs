using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmOrderSortingWork : Form
    {

        #region 字段
        private UserInfo _UserInfo;
        /// <summary>
        /// 目标规柜格
        /// </summary>
        private LatticeSetting _TargetLattice;
        /// <summary>
        /// 投入柜格
        /// </summary>
        private LatticeSetting _ResultLattice;
        /// <summary>
        /// 架子按钮柜格
        /// </summary>
        private LatticeSetting _ButtonLattice;
        /// <summary>
        /// 阻挡的柜格
        /// </summary>
        private LatticeSetting _BlockLattice;
        /// <summary>
        /// 订单信息
        /// </summary>
        private OrderInfo _Orderinfo;
        /// <summary>
        /// 按钮集合
        /// </summary>
        private List<Button> _ButtonList;
        /// <summary>
        /// 从机信息
        /// </summary>
        private List<SlaveConfig> _SlaveConfig;
        /// <summary>
        /// 分拣架设置信息
        /// </summary>
        private List<LatticeSetting> _LatticesettingList;
        /// <summary>
        /// 某格口内的订单
        /// </summary>
        private List<LatticeOrdersCache> _LatticeOrdersList;
        /// <summary>
        /// 是否有光栅被阻挡
        /// </summary>
        private bool _IsBlocked = false;       
        /// <summary>
        /// 是否已加载完
        /// </summary>
        private bool _IsLoaded = false;
        /// <summary>
        /// 重复扫描错误
        /// </summary>
        private bool _IsRepeatError = false;
        /// <summary>
        /// 是否未扫描就投递了
        /// </summary>
        private bool _IsNotScanAndPut = false;
        /// <summary>
        /// 短时间内同一个光栅检查到有物体阻挡的次数
        /// </summary>
        private int _CheckBlockNumber = 0;
        private int _CheckUnblockNumber = 0;
        /// <summary>
        /// 上次分拣投递成功的订单号
        /// </summary>
        private string _LastSuccessOrderId = string.Empty;
        /// <summary>
        /// 上一个扫描的订单号
        /// </summary>
        private string _LastOrderId = string.Empty;
        /// <summary>
        /// CheckUnblockTimer是否在执行
        /// </summary>
        public bool _IsCheckUnblockRunning = false;
        /// <summary>
        /// CheckBlockTimer是否在执行
        /// </summary>
        private bool _IsCheckBlockRunning = false;
        /// <summary>
        /// CheckGratingTimer是否在执行
        /// </summary>
        private bool _IsCheckGratingRunning = false;
        /// <summary>
        /// 分拣架按钮动作是否在运行
        /// </summary>
        private bool _IsCheckButtonRunning = false;
        /// <summary>
        /// 是否暂停执行CheckGrating
        /// </summary>
        private bool IsCheckGratingSuspending
        {
            get
            {
                return _IsBlocked || _IsRepeatError || _TargetLattice == null
                    || _IsCheckGratingRunning || _IsCheckButtonRunning|| _IsNotScanAndPut;
            }
        }
        /// <summary>
        /// 是否暂停执行CheckButton
        /// </summary>
        private bool IsCheckButtonSuspending
        {
            get
            {
                return _IsBlocked || _IsRepeatError || _TargetLattice != null || _ResultLattice != null
                  || _IsCheckGratingRunning || _IsCheckButtonRunning || _IsCheckUnblockRunning || _IsCheckBlockRunning || _IsNotScanAndPut;
            }
        }
        /// <summary>
        /// 是否暂停执行CheckBlock
        /// </summary>
        private bool IsCheckBlockSuspending
        {
            get
            {
                return _IsBlocked || _IsRepeatError || _TargetLattice != null || _ResultLattice != null
                  || _IsCheckGratingRunning || _IsCheckButtonRunning || _IsCheckUnblockRunning || _IsCheckBlockRunning || _IsNotScanAndPut;
            }
        }
        /// <summary>
        /// 是否暂停执行CheckUnblock
        /// </summary>
        private bool IsCheckUnblockSuspending
        {
            get
            {
                return !_IsBlocked || _IsRepeatError || _TargetLattice != null || _ResultLattice != null || _BlockLattice == null
                  || _IsCheckGratingRunning || _IsCheckButtonRunning || _IsCheckUnblockRunning || _IsCheckBlockRunning || _IsNotScanAndPut;
            }
        }
       

        #endregion

        #region 初始化Initialize

        public frmOrderSortingWork(UserInfo userInfo)
        {
            try
            {
                this._UserInfo = userInfo;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

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
                }).ContinueWith(cw => { _IsLoaded = true; });
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetLabelTest()
        {
            lblOrderId.Text = "";
        }

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

        #region 窗体按钮
        /// <summary>
        /// 打开格口设置窗口
        /// </summary>
        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsRepeatError || _TargetLattice != null)
                {
                    MessageBox.Show("分拣作业运行中，禁止修改格口设置！");
                    return;
                }
                SetTimerEnabled(false);
                var btn = sender as Button;
                var frm = new frmLatticeSettingEdit(btn.TabIndex);
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                _LatticesettingList = OrderSortService.LoadLatticeSetting();
                var ls = _LatticesettingList.First(l => l.ID == Convert.ToInt32(btn.Name));
                btn.Text = OrderSortService.GetLatticeNewText(ls);
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// GetColor（status：0绿灯，1红灯，2黄,>=3恢复）
        /// </summary>
        /// <param name="status">0绿灯，1红灯，2黄</param>
        /// <returns></returns>
        private Color GetColor(int status)
        {
            switch (status)
            {
                case 0:
                    return Color.Lime;
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Yellow;
                default:
                    return Color.Gainsboro;
            }
        }

        /// <summary>
        /// 设置定时器的启用停用
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetTimerEnabled(bool isEnabled)
        {
            try
            {
                this.CheckGratingTimer.Enabled = isEnabled;
                this.CheckButtonTimer.Enabled = isEnabled;
                this.CheckBlockTimer.Enabled = isEnabled;
                this.CheckUnblockTimer.Enabled = isEnabled;
                txtOrderId.Enabled = isEnabled;
                txtOrderId.Focus();
                lblOrderId.Text = isEnabled ? "" : "停止分拣";
                if (!isEnabled)
                {
                    _ButtonList.ForEach(b => b.BackColor = GetColor(3));
                    _TargetLattice = null;
                    _ResultLattice = null;
                    _BlockLattice = null;
                    _IsBlocked = false;
                    _ButtonLattice = null;
                    _Orderinfo = null;
                    _IsCheckButtonRunning = false;
                    _CheckBlockNumber = 0;
                    _CheckUnblockNumber = 0;
                    _IsRepeatError = false;
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 检查订单信息CheckOrderId
        private void txtOrderId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (string.IsNullOrWhiteSpace(txtOrderId.Text) || _IsBlocked|| _IsNotScanAndPut)
            {
                txtOrderId.Text = "";
                return;
            }
            if (_IsRepeatError)
            {
                //针对重复扫的异常处理
                CheckErrorAsync();
            }
            else
            {
                //正常扫码
                CheckOrderIdAsync();
            }
        }

        /// <summary>
        /// 针对重复扫的异常处理
        /// </summary>
        private Task CheckErrorAsync()
        {
            return Task.Run(() =>
            {
                Invoke((MethodInvoker)delegate ()
                {
                    try
                    {
                        var orderid = txtOrderId.Text.Trim();
                        txtOrderId.Text = "";
                        if (_LatticeOrdersList.Exists(lo => lo.OrderId == orderid))
                        {
                            lblMsg.Text = string.Format("此订单:{0}已入格口！", orderid);
                            return;
                        }
                           
                        //根据订单号获取订单信息，并获取目标格口
                        if (!CheckOrderinfo(orderid))
                            return;
                        ////投递错误格口红灯熄灭
                        //var ErrorLatticesettingId = _LatticeOrdersList.FirstOrDefault().LatticesettingId;
                        ////获取投递错误格口信息
                        //var ErrorLatticesetting = OrderSortService.GetLatticeSettingById(ErrorLatticesettingId);
                        //if (ErrorLatticesetting != null)
                        //{
                        //    //投递错误格口红灯熄灭
                        //    OrderSortService.SetLED(ErrorLatticesetting, 0);
                        //}
                       
                        //创建扫描记录，亮灯
                        CreateOrderSortingLogAndTurnOnGreenLight();

                    }
                    catch (Exception ex)
                    {
                        SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                        MessageBox.Show(ex.Message);
                    }
                });
            });
        }

        /// <summary>
        /// 创建扫描记录，亮灯
        /// </summary>
        private void CreateOrderSortingLogAndTurnOnGreenLight()
        {
            //创建扫描记录
            if (OrderSortService.CreateOrderSortingLog(_Orderinfo, _TargetLattice, null, _UserInfo, 1, 1))
            {
                OrderSortService.SetLED(_TargetLattice, 0);
                //重置计数器
                OrderSortService.ReSetGratingOrButton(3);
                //重置警示灯
                OrderSortService.ReSetWarningLight();
                lblOrderId.Text = _Orderinfo.OrderId;
                //_buttonList.ForEach(b => b.BackColor = GetColor(3));
                _ButtonList.FirstOrDefault(b => b.TabIndex == _TargetLattice.ID).BackColor = GetColor(0);
                _IsRepeatError = false;
                _LatticeOrdersList = null;
                _LastOrderId = _Orderinfo.OrderId;
                lblMsg.Text = "";
            }
        }

        /// <summary>
        /// 检查订单信息
        /// </summary>
        private Task CheckOrderIdAsync()
        {
            return Task.Run(() =>
            {
                Invoke((MethodInvoker)delegate ()
                {
                    try
                    {
                        var orderid = txtOrderId.Text.Trim();
                        txtOrderId.Text = "";
                        if (!string.IsNullOrWhiteSpace(lblOrderId.Text) && lblOrderId.Text != orderid)
                        {
                            if (_TargetLattice != null && !string.IsNullOrWhiteSpace(_LastOrderId) && !_LastOrderId.Equals(orderid))
                            {
                                //禁止同时扫两个订单
                                OrderSortService.SetLED(_TargetLattice, 3);
                                _ButtonList.FirstOrDefault(b => b.TabIndex == _TargetLattice.ID).BackColor = GetColor(3);
                                _TargetLattice = null;
                                //_LastOrderId = string.Empty;
                                //ResetLabelTest();
                                lblMsg.Text =string.Format( "不可同时扫描两个订单：{0}，{1}", lblOrderId.Text, orderid);
                            }

                            return;
                        }
                        else {
                            OrderSortService.ReSetLED();
                        }
                        //根据订单号获取订单信息，并获取目标格口
                        if (!CheckOrderinfo(orderid))
                            return;
                        //是否连续两次重复投递
                        //var lastOrderLog = OrderSortService.GetTheLastOrderSortingLog();
                        //if (lastOrderLog != null && lastOrderLog.OrderId == orderid && lastOrderLog.Status == 2)
                        if (_LastSuccessOrderId.Equals(orderid))
                        {
                            //创建扫描记录
                            if (OrderSortService.CreateOrderSortingLog(_Orderinfo, _TargetLattice, null, _UserInfo, 1, 4))
                            {
                                OrderSortService.SetLED(_TargetLattice, 1);
                                //准备进行重复扫描异常处理
                                _LatticeOrdersList = OrderSortService.GetLatticeOrdersListByLatticesettingId(_TargetLattice.ID);
                                if (_LatticeOrdersList != null && _LatticeOrdersList.Count > 0)
                                {
                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _TargetLattice.ID).BackColor = GetColor(1);
                                    _IsRepeatError = true;
                                    _TargetLattice = null;
                                    lblMsg.Text = string.Format("不可重复扫描,订单号：{0}", orderid);
                                    return;
                                }
                            }
                            return;
                        }
                        //创建扫描记录，亮灯
                        CreateOrderSortingLogAndTurnOnGreenLight();
                    }
                    catch (Exception ex)
                    {
                        SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                        MessageBox.Show(ex.Message);
                    }
                });
            });
        }

        /// <summary>
        /// 根据订单号获取订单信息，并获取目标格口
        /// </summary>
        private bool CheckOrderinfo(string orderid)
        {
            var msg = string.Empty;
            //根据订单号获取订单信
            _Orderinfo = OrderSortService.GetOrderInfoById(orderid, _UserInfo, ref msg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                ResetLabelTest();
                lblMsg.Text = string.Format("获取订单数据:{0}失败,{1}",orderid, msg);
                return false;
            }
            //获取目标格口
            _TargetLattice = OrderSortService.GetLatticeSettingByOrderinfo(_Orderinfo);
            if (_TargetLattice == null)
            {
                ResetLabelTest();
                lblMsg.Text = string.Format("获取目标格口信息失败，订单号:{0}", orderid);
                return false;
            }
            //判断格口是否已满
            if (OrderSortService.IsFullLattice(_TargetLattice.ID))
            {
                int cabinetId = _TargetLattice.CabinetId;
                int index = _TargetLattice.LEDIndex;
                _TargetLattice = null;
                OrderSortService.SetLED(cabinetId, index, 1);
                Thread.Sleep(500);
                OrderSortService.SetLED(cabinetId, index, 3);
                lblMsg.Text = string.Format("格口:{0} 已满", _TargetLattice.CabinetId);
                return false;
            }
            return true;
        }
        #endregion




        #region 检查光栅状态CheckGrating
        private void CheckGratingTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsCheckGratingSuspending)
                    return;
                CheckGratingAsync();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 检查光栅状态，检查每一个格口是否有物体通过
        /// </summary>
        private Task CheckGratingAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (IsCheckGratingSuspending)
                    {
                        return;
                    }
                    _IsCheckGratingRunning = true;
                    if (_SlaveConfig == null) {
                        SaveErrLogHelper.SaveErrorLog(string.Empty, "检查光栅状态时出现异常：_SlaveConfig为Null");
                    }
                    foreach (var slave in _SlaveConfig)
                    {
                        var registersGrating = OrderSortService.ReadGratingRegisters(slave.SlaveAddress);
                        if (registersGrating == null) {
                            SaveErrLogHelper.SaveErrorLog(string.Empty, "检查光栅状态时出现异常：registersGrating为Null");
                            continue;
                        }
                        
                        if (_TargetLattice == null) {
                            SaveErrLogHelper.SaveErrorLog(string.Empty, "检查光栅状态时出现异常：_TargetLattice为Null");
                            continue;
                        }
                        
                        for (int i = 0; i < registersGrating.Length; i++)
                        {
                            //是否有包裹投入
                            if (registersGrating[i] < 1)
                            {
                                continue;
                            }
                            
                            //投放错误
                            if (_TargetLattice.GratingIndex != i || slave.CabinetId != _TargetLattice.CabinetId)
                            {
                                if (_LatticesettingList == null)
                                {
                                    SaveErrLogHelper.SaveErrorLog(string.Empty, "检查光栅状态时出现异常：_LatticesettingList为Null");
                                }
                                _ResultLattice = _LatticesettingList.Find(lsc => lsc.GratingIndex == i && lsc.CabinetId == slave.CabinetId);
                                //创建分拣记录
                                if (OrderSortService.CreateOrderSortingLog(_Orderinfo, _TargetLattice, _ResultLattice, _UserInfo, 2, 3))
                                {
                                    OrderSortService.SetWarningLight(4, 1);
                                    //亮红灯
                                    OrderSortService.SetLED(_ResultLattice, 1);
                                    _LastOrderId = string.Empty;
                                    _TargetLattice = null;
                                    _BlockLattice = null;
                                    //按钮变红色
                                    //_buttonList.ForEach(b => b.BackColor = GetColor(3));
                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _ResultLattice.ID).BackColor = GetColor(1);
                                    Invoke((MethodInvoker)delegate ()
                                    {
                                        lblMsg.Text = "投放错误";
                                    });
                                }
                            }
                            else
                            {
                                //创建分拣记录
                                if (OrderSortService.CreateOrderSortingLog(_Orderinfo, _TargetLattice, _TargetLattice, _UserInfo, 2, 2))
                                {
                                    OrderSortService.ReSetLED();
                                    OrderSortService.ReSetWarningLight();
                                    _LastSuccessOrderId = _Orderinfo.OrderId;
                                    Invoke((MethodInvoker)delegate ()
                                    {
                                        //更新格口统计信息
                                        _ButtonList.FirstOrDefault(b => b.TabIndex == _TargetLattice.ID).Text = OrderSortService.GetLatticeNewText(_TargetLattice);
                                    });
                                    if (OrderSortService.IsFullLattice(_TargetLattice.ID))
                                    {
                                        int cabinetId = _TargetLattice.CabinetId;
                                        int index = _TargetLattice.LEDIndex;
                                        OrderSortService.SetLED(cabinetId, index, 1);
                                        Thread.Sleep(500);
                                        OrderSortService.SetLED(cabinetId, index, 3);
                                    }
                                    _LastOrderId = string.Empty;
                                    _ResultLattice = null;
                                    _BlockLattice = null;
                                    _TargetLattice = null;
                                    //按钮颜色恢复
                                    _ButtonList.ForEach(b => b.BackColor = GetColor(3));
                                    Invoke((MethodInvoker)delegate () { ResetLabelTest(); });
                                    OrderSortService.ReSetGratingOrButton(1);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                }
                finally
                {
                    _IsCheckGratingRunning = false;
                }
            });
        }
        #endregion

        #region 检查分拣架按钮状态CheckButton
        private void CheckButtonTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsCheckButtonSuspending)
                    return;
                CheckButtonAsync();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 检查分拣架后的按钮是否有按压动作
        /// </summary>
        /// <returns></returns>
        private Task CheckButtonAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (IsCheckButtonSuspending)
                    {
                        return;
                    }
                    _IsCheckButtonRunning = true;
                    foreach (var slave in _SlaveConfig)
                    {
                        var registersButton = OrderSortService.ReadButtonRegisters(slave.SlaveAddress);
                        for (int i = 0; i < registersButton.Length; i++)
                        {
                            //是否有点击按钮
                            if (registersButton[i] < 15)
                                continue;
                            _ButtonLattice = _LatticesettingList.Find(lsc => lsc.CabinetId == slave.CabinetId && lsc.ButtonIndex == i);
                            //创建打包记录
                            var packingLog = OrderSortService.CreatePackingLog(_ButtonLattice, _UserInfo, 3);
                            if (packingLog != null)
                            {
                                //打印包牌
                                new PackingLabelPrintDocument().PrintSetup(packingLog);
                                //按钮颜色恢复
                                //_ButtonList.ForEach(b => b.BackColor = GetColor(3));
                                Invoke((MethodInvoker)delegate ()
                                {
                                    //更新格口统计信息
                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _ButtonLattice.ID).Text = OrderSortService.GetLatticeNewText(_ButtonLattice);
                                });
                                //_TargetLattice = null;
                                _ResultLattice = null;
                                _BlockLattice = null;
                                _ButtonLattice = null;
                                _CheckBlockNumber = 0;
                            }
                            //重置按钮计数器
                            OrderSortService.ReSetGratingOrButton(2);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _IsCheckButtonRunning = false;
                }
            });
        }
        #endregion

        #region 检查光栅是否遇到阻挡CheckBlock
        private void CheckBlockTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsCheckBlockSuspending)
                    return;
                CheckBlockAsync();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 判断光栅是否遇到阻挡，如果一个格口连续2次检测到阻挡，则警告提示
        /// </summary>
        private Task CheckBlockAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (IsCheckBlockSuspending)
                    {
                        return;
                    }
                    _IsCheckBlockRunning = true;
                    foreach (var slave in _SlaveConfig)
                    {
                        var registersGrating = OrderSortService.ReadGratingRegisters(slave.SlaveAddress);
                        for (int gratingIndex = 0; gratingIndex < registersGrating.Length; gratingIndex++)
                        {
                            
                            //是否有阻挡
                            if (registersGrating[gratingIndex] < 1)
                            {
                                //未扫描且投递
                                if (!_IsCheckGratingRunning&&_CheckBlockNumber >= 1 && _BlockLattice != null &&_BlockLattice.CabinetId == slave.CabinetId && _BlockLattice.GratingIndex == gratingIndex) {
                                    _IsNotScanAndPut = true;
                                    if (OrderSortService.SetLED(_BlockLattice, 1))
                                    {
                                        OrderSortService.SetWarningLight(4, 1);
                                        _ButtonList.FirstOrDefault(b => b.TabIndex == _BlockLattice.ID).BackColor = GetColor(1);

                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            frmMessage frmMessage = new frmMessage("请先扫描后再投递！");
                                            DialogResult result=frmMessage.ShowDialog();
                                            if (result == DialogResult.OK)
                                            {
                                                _IsNotScanAndPut = false;
                                                txtOrderId.Focus();
                                                if (OrderSortService.SetLED(_BlockLattice, 3))
                                                {
                                                    OrderSortService.ReSetWarningLight();
                                                    OrderSortService.ReSetGratingOrButton(1);
                                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _BlockLattice.ID).BackColor = GetColor(3);
                                                    _CheckBlockNumber = 0;
                                                    _BlockLattice = null;
                                                }
                                            }
                                        });
                                    }
                                }
                                continue;
                            }
                            if (_BlockLattice == null || _CheckBlockNumber < 2)
                            {
                                if (registersGrating[gratingIndex] > 10)
                                {
                                    if (_BlockLattice != null && _BlockLattice.CabinetId == slave.CabinetId && _BlockLattice.GratingIndex == gratingIndex)
                                    {
                                        _CheckBlockNumber++;
                                    }
                                    else
                                    {
                                        _CheckBlockNumber = 1;
                                        _BlockLattice = _LatticesettingList.FirstOrDefault(s => s.CabinetId == slave.CabinetId && s.GratingIndex == gratingIndex && s.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase));
                                        if (_BlockLattice == null)
                                            continue;
                                    }
                                }
                                OrderSortService.ReSetGratingOrButton(1);
                            }
                            else
                            {
                                if (_BlockLattice.CabinetId == slave.CabinetId && _BlockLattice.GratingIndex == gratingIndex)
                                {
                                    _IsBlocked = true;
                                    //闪红灯
                                    if (OrderSortService.SetLED(_BlockLattice, 2))
                                    {
                                        OrderSortService.SetWarningLight(4, 1);
                                        _CheckBlockNumber = 0;
                                       
                                        //按钮变黄
                                        _ButtonList.FirstOrDefault(b => b.TabIndex == _BlockLattice.ID).BackColor = GetColor(2);
                                        Invoke((MethodInvoker)delegate ()
                                        {
                                            lblMsg.Text = string.Format("{0}柜{1}已满格！", _BlockLattice.CabinetId, _BlockLattice.LatticeId);
                                            lblOrderId.Text = string.Format("{0}柜{1}已满格！", _BlockLattice.CabinetId, _BlockLattice.LatticeId);
                                        });
                                    }
                                }
                            }
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                }
                finally
                {
                    _IsCheckBlockRunning = false;
                   
                }
            });
        }

        #endregion

        #region 检查格子是否已解除阻挡CheckUnblock
        private void CheckUnblockTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //有阻挡的时候才跑
                if (IsCheckUnblockSuspending)
                    return;
                CheckUnblockAsync();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 针对阻挡的格口，检测对应光栅是否有感应到有物体阻挡，如果连续两次没有感应到阻挡，则解除警报
        /// </summary>
        private Task CheckUnblockAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (IsCheckUnblockSuspending)
                        return;
                    _IsCheckUnblockRunning = true;
                    if (_CheckUnblockNumber < 1)
                    {
                        _CheckUnblockNumber++;
                        OrderSortService.ReSetGratingOrButton(1);
                        return;
                    }
                    var registersGrating = OrderSortService.ReadGratingRegisters(_SlaveConfig.Find(sc => sc.CabinetId == _BlockLattice.CabinetId).SlaveAddress);
                    if (registersGrating[_BlockLattice.GratingIndex] > 1)
                    {
                        OrderSortService.ReSetGratingOrButton(1);
                        return;
                    }
                    _CheckUnblockNumber++;
                    if (_CheckUnblockNumber > 1)
                    {
                        if (OrderSortService.ReSetLED())
                        {
                            OrderSortService.ReSetWarningLight();
                            //_TargetLattice = null;
                            _ResultLattice = null;
                            _BlockLattice = null;
                            _IsBlocked = false;
                            //按钮颜色恢复
                            _ButtonList.ForEach(b => b.BackColor = GetColor(3));
                            Invoke((MethodInvoker)delegate () { ResetLabelTest(); });
                            OrderSortService.ReSetGratingOrButton(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                }
                finally
                {
                    _IsCheckUnblockRunning = false;
                }
            });
        }
        #endregion

        #region 菜单按钮

        #region 开始分拣
        private void 开始分拣ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SetTimerEnabled(true);
                OrderSortService.ReSetGratingOrButton(3);
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
                //获取最新的一条分拣记录
                var log = OrderSortService.GetTheLastOrderSortingLog();
                if (log != null && log.Status == 3)
                {
                    switch (log.Status)
                    {
                        case 3://3投递异常
                            Invoke((MethodInvoker)delegate ()
                            {
                                //目标柜格，亮绿灯
                                _TargetLattice = _LatticesettingList.Find(s => s.CabinetId == Convert.ToInt32(log.TargetCabinetId) && s.LatticeId == log.TargetLatticeId);
                                if (OrderSortService.SetLED(_TargetLattice, 0))
                                {
                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _TargetLattice.ID).BackColor = GetColor(0);
                                }
                                //投入柜格，亮红灯
                                _ResultLattice = _LatticesettingList.Find(s => s.CabinetId == Convert.ToInt32(log.ResultCabinetId) && s.LatticeId == log.ResultLatticeId);
                                if (OrderSortService.SetLED(_ResultLattice, 1))
                                {
                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _ResultLattice.ID).BackColor = GetColor(1);
                                }
                                lblOrderId.Text = log.OrderId;
                                _TargetLattice = null;
                            });
                            break;
                        case 4://4重复扫描
                            Invoke((MethodInvoker)delegate ()
                            {
                                _LastSuccessOrderId = log.OrderId;
                                _LastOrderId = string.Empty;
                                //目标柜格，亮绿灯
                                _TargetLattice = _LatticesettingList.Find(s => s.CabinetId == Convert.ToInt32(log.TargetCabinetId) && s.LatticeId == log.TargetLatticeId);
                                OrderSortService.SetLED(_TargetLattice, 1);
                                //准备进行重复扫描异常处理
                                _LatticeOrdersList = OrderSortService.GetLatticeOrdersListByLatticesettingId(_TargetLattice.ID);
                                if (_LatticeOrdersList != null && _LatticeOrdersList.Count > 0)
                                {
                                    _ButtonList.FirstOrDefault(b => b.TabIndex == _TargetLattice.ID).BackColor = GetColor(1);
                                    _IsRepeatError = true;
                                    _TargetLattice = null;
                                    return;
                                }
                            });
                            break;
                    }
                }
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
            //OrderSortService.ReSetGratingOrButton(3);
            //OrderSortService.ReSetLED();
            //OrderSortService.ReSetWarningLight();
            ////按钮颜色恢复
            //_ButtonList.ForEach(b => b.BackColor = GetColor(3));
            //_TargetLattice = null;
            //_ResultLattice = null;
            //_BlockLattice = null;
            //_IsBlocked = false;
            //_ButtonLattice = null;
            //_Orderinfo = null;
            //_IsCheckButtonRunning = false;
            //_CheckBlockNumber = 0;
            //_CheckUnblockNumber = 0;
            //_IsRepeatError = false;
            //_IsCheckGratingRunning = false;
            //_IsCheckUnblockRunning = false;
            //_IsCheckBlockRunning = false;
            ////Invoke((MethodInvoker)delegate () { ResetLabelTest(); });
            //ResetLabelTest();
            ////});
        }

        #endregion

        #region PKG条码重打
        private void pKG条码重打ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsRepeatError || _TargetLattice != null)
                {
                    MessageBox.Show("分拣作业运行中，禁止重打！");
                    return;
                }
                SetTimerEnabled(false);
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
                if (_IsRepeatError || _TargetLattice != null)
                {
                    MessageBox.Show("分拣作业运行中，禁止新建方案！");
                    return;
                }
                SetTimerEnabled(false);
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

        private void frmOrderSortingWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetTimerEnabled(false);
        }

        private void 方案重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsRepeatError || _TargetLattice != null)
                {
                    MessageBox.Show("分拣作业运行中，禁止方案重命名！");
                    return;
                }
                SetTimerEnabled(false);
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

        private void 分拣撤回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsRepeatError || _TargetLattice != null)
                {
                    MessageBox.Show("分拣作业运行中，禁止方案重命名！");
                    return;
                }
                SetTimerEnabled(false);
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

        private void pKG标签打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsRepeatError || _TargetLattice != null)
                {
                    MessageBox.Show("分拣作业运行中，禁止重打！");
                    return;
                }
                SetTimerEnabled(false);
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
    }
}
