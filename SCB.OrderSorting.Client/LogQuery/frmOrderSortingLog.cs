using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmOrderSortingLog : Form
    {
        //控制连续查询
        private static bool SearchStatus = false;
        List<KeyValuePair<int, string>> statusList = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> typeList = new List<KeyValuePair<int, string>>();

        public frmOrderSortingLog()
        {
            InitializeComponent();
            //初始化控件默认值
            InitializeControl();
        }

        private void InitializeControl()
        {
            //初始化问题类型选项   
            var enumArray = Enum.GetValues(typeof(OrderSortingLog_Status_Enum));
            statusList.Add(new KeyValuePair<int, string>(-1, "全部"));
            foreach (var enumItem in enumArray)
            {
                statusList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
            }
            cbStatus.DataSource = statusList;
            cbStatus.DisplayMember = "Value";
            cbStatus.ValueMember = "Key";
            cbStatus.SelectedValue = -1;

            //初始化状态选项
            var enumArray2 = Enum.GetValues(typeof(OrderSortingLog_OperationType_Enum));
            typeList.Add(new KeyValuePair<int, string>(-1, "全部"));
            foreach (var enumItem in enumArray2)
            {
                typeList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
            }
            cbOperationType.DataSource = typeList;
            cbOperationType.DisplayMember = "Value";
            cbOperationType.ValueMember = "Key";
            cbOperationType.SelectedValue = -1;

            //dgvContent启用双缓冲技术
            dgvContent.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvContent, true, null);

            //初始化dgvContent的列绑定
            dgvContent.AutoGenerateColumns = false;
            dgv_OrderId.DataPropertyName = "订单号";
            dgv_TargetCabinetId.DataPropertyName = "目标柜号";
            dgv_TargetLatticeId.DataPropertyName = "目标格号";
            dgv_ResultCabinetId.DataPropertyName = "投入柜号";
            dgv_ResultLatticeId.DataPropertyName = "投入格号";
            dgv_OperationType.DataPropertyName = "操作类型";
            dgv_Status.DataPropertyName = "状态";
            dgv_OperationTime.DataPropertyName = "操作时间";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pagerMain.cmbPages.Text = "1";
            Thread.CurrentThread.Join(100);
            if (SearchStatus)
            {
                return;
            }
            LoadOrderSortingLogList(1);
        }

        private void LoadOrderSortingLogList(int pageIndex)
        {
            try
            {
                if (SearchStatus)
                    return;
                SearchStatus = true;
                Thread thLoad = new Thread(ThreadLoadData);
                thLoad.IsBackground = true;
                thLoad.Start(pageIndex);
                dgvContent.DataSource = null;
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
            }
            finally
            {
                SearchStatus = false;
            }
        }

        private void ThreadLoadData(object pageIndex)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    pagerMain.PageIndex = (int)pageIndex;
                    //订单号
                    var orderId = txtOrderId.Text.Trim();
                    //问题类型
                    var operationType = (int)cbOperationType.SelectedValue;
                    //状态
                    var status = (int)cbStatus.SelectedValue;
                    var startDate = dtpStartDate.Value;
                    var endDate = dtpEndDate.Value;
                    var recordCount = 0;

                    //获取数据
                    var orderResult = OrderSortService.GetOrderSortingLogByPageSize(orderId, operationType, status, startDate, endDate, pagerMain.PageIndex, pagerMain.PageSize, ref recordCount);

                    if (orderResult == null || !orderResult.Any())
                    {
                        MessageBox.Show("查询无数据");
                        return;
                    }
                    pagerMain.RecordCount = recordCount;
                    var data = (from os in orderResult
                                orderby os.OperationTime
                                select new OrderSortingLogViewModel
                                {
                                    订单号 = os.OrderId,
                                    目标柜号 = os.TargetCabinetId,
                                    目标格号 = os.TargetLatticeId,
                                    投入柜号 = os.ResultCabinetId,
                                    投入格号 = os.ResultLatticeId,
                                    操作类型 = typeList.FirstOrDefault(t => t.Key == os.OperationType).Value,
                                    状态 = statusList.FirstOrDefault(t => t.Key == os.Status).Value,
                                    操作时间 = os.OperationTime.ToString("yyyy/MM/dd HH:mm:ss")
                                });

                    dgvContent.DataSource = new SortableBindingList<OrderSortingLogViewModel>(data.OrderByDescending(d => d.操作时间).ToList());
                    pagerMain.SetBtnEnabled();
                    pagerMain.Refresh();
                    dgvContent.Focus();
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 分页控件页码变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerMain_PageIndexChanged(object sender, EventArgs e)
        {
            LoadOrderSortingLogList(pagerMain.PageIndex);
        }

        /// <summary>
        /// 分页控件页大小变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerMain_PageSizeChanged(object sender, EventArgs e)
        {
            LoadOrderSortingLogList(1);
        }

        private void dgvContent_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dgvContent.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dgvContent.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dgvContent.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dgvContent.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }

        private void dgvContent_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dgvContent.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
    }
}
