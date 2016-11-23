using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Linq;
using System.Windows.Forms;
using SCB.OrderSorting.BLL.Model;

namespace SCB.OrderSorting.Client
{
    public partial class frmPkgPrint : Form
    {
        private UserInfo _UserInfo;

        public frmPkgPrint(UserInfo _UserInfo)
        {
            this._UserInfo = _UserInfo;
            InitializeComponent();
        }

        private void btnReprintByLatticeId_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLatticeId.Text))
                    return;
                var latticeIdArray = txtLatticeId.Text.Split(Environment.NewLine.ToArray());
                var pkg = OrderSortService.CreatePackingLog(latticeIdArray, _UserInfo);
                if (pkg != null)
                {
                    //打印包牌
                    new PackingLabelPrintDocument().PrintSetup(pkg);
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
