using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client.Work
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage("请先扫描后再投递！");
            DialogResult result = frmMessage.ShowDialog();
            if (result == DialogResult.OK)
            {
                label1.Text = "back";
            }
        }
    }
}
