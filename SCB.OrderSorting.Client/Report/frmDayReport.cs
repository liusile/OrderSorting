using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Model;

namespace SCB.OrderSorting.Client
{
    public partial class frmDayReport : Form
    {
        private static bool SearchStatus = false;
        private UserInfo UserInfo { get; set; }
        public frmDayReport()
        {
            InitializeComponent();
            //初始化控件默认值
            InitializeControl();
        }
        public frmDayReport(UserInfo userInfo):this()
        {
            UserInfo = userInfo;
        }

        private void InitializeControl()
        {
            //dgvContent启用双缓冲技术
            dgvContent.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvContent, true, null);

            //初始化dgvContent的列绑定
            dgvContent.AutoGenerateColumns = false;
            dgv_LactionError.DataPropertyName = "投递异常";
            dgv_over.DataPropertyName = "已投递";
            dgv_RepeatError.DataPropertyName = "重复扫描";
            dgv_sum.DataPropertyName = "总计";
            dgv_Time.DataPropertyName = "操作时间";
            dgv_waitPut.DataPropertyName = "待投递";
            
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
                    
                    var startDate = dtpStartDate.Value;
                    var endDate = dtpEndDate.Value;
                    var recordCount = 0;

                    //获取数据
                    var orderResult = OrderSortService.GetDayReportByPageSize(startDate, endDate, pagerMain.PageIndex, pagerMain.PageSize, ref recordCount);
                    
                    if (orderResult == null || !orderResult.Any())
                    {
                        MessageBox.Show("查询无数据");
                        return;
                    }
                    pagerMain.RecordCount = recordCount;
                    var data = (from os in orderResult
                                orderby os.操作时间
                                select new DayReportViewModel
                                {
                                    操作时间 = os.操作时间,
                                    待投递 = os.待投递,
                                    已投递 = os.已投递,
                                    投递异常 = os.投递异常,
                                    重复扫描 = os.重复扫描,
                                    总计 = os.总计
                                });

                    dgvContent.DataSource = new SortableBindingList<DayReportViewModel>(data.OrderByDescending(d => d.操作时间).ToList());
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
