using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmCreateNewSolution : Form
    {
        public frmCreateNewSolution()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSolutionName.Text))
                    return;
                OrderSortService.CreateNewSolution(txtSolutionName.Text);
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
