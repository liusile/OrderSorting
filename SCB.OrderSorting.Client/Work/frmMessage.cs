using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmMessage : Form
    {
        public frmMessage()
        {
            InitializeComponent();
        }
        public frmMessage(string msg):this()
        {
            lb_Msg.Text = msg;
          

        }


        private void ok_Click(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
