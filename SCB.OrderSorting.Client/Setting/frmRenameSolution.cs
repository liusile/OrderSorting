using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmRenameSolution : Form
    {
        public frmRenameSolution()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSolutionName.Text))
                    return;
                OrderSortService.RenameSolution(txtSolutionName.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
    }
}
