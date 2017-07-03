using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmDeleteOrderCache : Form
    {
        public frmDeleteOrderCache()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtOrderId.Text))
                    return;
                string [] orderIdArray = txtOrderId.Text.Split(Environment.NewLine.ToArray());
                string[] scan = OrderSortService.GetSystemSettingCache().Scanner?.Split(',');
                if (scan.Length > 1)
                {
                    orderIdArray = orderIdArray.Where(o=>o!="").Select(o => o.Substring(1)).ToArray();
                }
                string result = OrderSortService.DeleteOrderCacheByOrderId(orderIdArray);
                MessageBox.Show(result);
                if (result.IndexOf("失败") < 0)
                {
                    txtOrderId.Text = string.Empty;
                }
                txtOrderId.Focus();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRetract_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLatticeId.Text))
                    return;
                MessageBox.Show(OrderSortService.DeleteOrderCacheByLatticeId(txtLatticeId.Text));
                txtLatticeId.Text = string.Empty;
                txtLatticeId.Focus();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
    }
}
