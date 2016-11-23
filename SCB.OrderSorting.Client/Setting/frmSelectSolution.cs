using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.DAL;
using System;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmSelectSolution : Form
    {
        private string sortingSolution;

        private string selectedSolution;

        public frmSelectSolution()
        {
            InitializeComponent();
        }

        public frmSelectSolution(string sortingSolution)
        {
            this.sortingSolution = sortingSolution;
            InitializeComponent();
        }

        private void frmSelectSolution_Load(object sender, EventArgs e)
        {
            try
            {
                DataLoad();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void DataLoad()
        {
            lbSolution.DataSource = OrderSortService.GetSortingSolutionsList();
            lbSolution.ValueMember = "Id";
            lbSolution.DisplayMember = "Name";
            lbSolution.SelectedValue = sortingSolution;
        }

        public string GetSelectedSolution()
        {
            return selectedSolution;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbSolution.SelectedValue != null)
                {
                    selectedSolution = lbSolution.SelectedValue.ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbSolution.SelectedValue != null && !sortingSolution.Equals(lbSolution.SelectedValue.ToString()))
                {
                    var solution = lbSolution.SelectedItem as SortingSolutions;
                    if (MessageBox.Show("是否要删除方案" + solution.Name, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        OrderSortService.DeleteSortingSolutionsById(solution.Id);
                        DataLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
