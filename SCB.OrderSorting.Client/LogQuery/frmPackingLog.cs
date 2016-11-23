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
    public partial class frmPackingLog : Form
    {
        //控制连续查询
        private static bool SearchStatus = false;
        List<KeyValuePair<int, string>> typeList = new List<KeyValuePair<int, string>>();

        public frmPackingLog()
        {
            InitializeComponent();
            //初始化控件默认值
            InitializeControl();
        }

        private void InitializeControl()
        {
            //初始化类型选项   
            var enumArray = Enum.GetValues(typeof(PackingLog_OperationType_Enum));
            typeList.Add(new KeyValuePair<int, string>(-1, "全部"));
            foreach (var enumItem in enumArray)
            {
                typeList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
            }

            //dgvContent启用双缓冲技术
            dgvContent.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvContent, true, null);
            //初始化dgvContent的列绑定
            dgvContent.AutoGenerateColumns = false;
            dgv_PackNumber.DataPropertyName = "包牌号";
            dgv_CabinetId.DataPropertyName = "柜号";
            dgv_LatticeId.DataPropertyName = "格号";
            dgv_PostTypeNames.DataPropertyName = "渠道";
            dgv_CountryNames.DataPropertyName = "地区";
            dgv_UserName.DataPropertyName = "操作员";
            dgv_OrderQty.DataPropertyName = "订单数量";
            dgv_OperationType.DataPropertyName = "操作类型";
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
            LoadPackingLogList(1);
        }

        private void LoadPackingLogList(int pageIndex)
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
                    var startDate = dtpStartDate.Value;
                    var endDate = dtpEndDate.Value;
                    var recordCount = 0;

                    //获取数据
                    var orderResult = OrderSortService.GetPackingLogByPageSize( startDate, endDate, pagerMain.PageIndex, pagerMain.PageSize, ref recordCount);

                    if (orderResult == null || !orderResult.Any())
                    {
                        MessageBox.Show("查询无数据");
                        return;
                    }
                    pagerMain.RecordCount = recordCount;
                    var data = (from os in orderResult
                                orderby os.OperationTime
                                select new PackingLogViewModel
                                {
                                    包牌号 = os.PackNumber,
                                    柜号 = os.CabinetId,
                                    格号 = os.LatticeId,
                                    渠道 = os.PostTypeNames,
                                    地区 = os.CountryNames,
                                    操作员 = os.UserName,
                                    订单数量 = os.OrderQty + "Kg",
                                    操作类型 = typeList.FirstOrDefault(t => t.Key == os.OperationType).Value,
                                    操作时间 = os.OperationTime.ToString("yyyy/MM/dd HH:mm:ss")
                                });

                    dgvContent.DataSource = new SortableBindingList<PackingLogViewModel>(data.OrderByDescending(d => d.操作时间).ToList());
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
            LoadPackingLogList(pagerMain.PageIndex);
        }

        /// <summary>
        /// 分页控件页大小变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerMain_PageSizeChanged(object sender, EventArgs e)
        {
            LoadPackingLogList(1);
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
