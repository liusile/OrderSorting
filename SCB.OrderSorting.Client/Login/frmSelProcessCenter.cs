using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmSelProcessCenter : Form
    {

        private string SelProcessCenterID { get; set; }

        public frmSelProcessCenter()
        {
            InitializeComponent();
        }

        public string GetProcessCenterID()
        {
            return SelProcessCenterID;
        }

        private void frmSelProcessCenter_Load(object sender, EventArgs e)
        {
            var processCenters = new Dictionary<string, string>();
            processCenters.Add("广州处理中心", "7");
            processCenters.Add("东莞处理中心", "934");
            processCenters.Add("杭州处理中心", "722");
            processCenters.Add("会江服装处理中心", "619");
            processCenters.Add("东莞塘厦处理中心", "1039");
            processCenters.Add("香港处理中心", "1040");
            processCenters.Add("广州品牌处理中心", "1063");
            lbProcessCenter.DisplayMember = "key";
            lbProcessCenter.ValueMember = "value";
            lbProcessCenter.DataSource = processCenters.ToList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbProcessCenter.SelectedValue != null)
            {
                SelProcessCenterID = lbProcessCenter.SelectedValue.ToString();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void lbProcessCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lbProcessCenter.SelectedValue != null)
                {
                    SelProcessCenterID = lbProcessCenter.SelectedValue.ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
