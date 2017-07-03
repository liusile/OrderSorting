using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmPkgReprint : Form
    {
        public frmPkgReprint()
        {
            InitializeComponent();
        }

        private void btnReprintByLatticeId_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLatticeId.Text))
                    return;
                var pkg = OrderSortService.GetPackingLog(txtLatticeId.Text);
                //打印包牌
                if (OrderSortService.GetSystemSettingCache().PrintFormat == 0)
                {
                    new PackingLabelPrintDocument().PrintSetup(pkg);
                }
                else
                {
                    new PackingLabelPrintDocument2().PrintSetup(pkg);
                }
               
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReprintByPkg_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPkg.Text))
                    return;
                var pkg = OrderSortService.GetPackingLog("", txtPkg.Text);
                //打印包牌
                if (OrderSortService.GetSystemSettingCache().PrintFormat == 0)
                {
                    new PackingLabelPrintDocument().PrintSetup(pkg);
                }
                else
                {
                    new PackingLabelPrintDocument2().PrintSetup(pkg);
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
    }
}
