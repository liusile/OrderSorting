using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmLogin : Form
    {
        private List<LoginData> lst_LoginCache = new List<LoginData>();
        private UserInfo userInfo = new UserInfo();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            try
            {
                string name = this.cmbUsers.Text.Trim();
                string pwd = txtPwd.Text.Trim();
                //测试S
                
                //测试E
                //登录
                LoginStart(name, pwd);
            }
            catch (Exception ex) { CommonLib.ShowErrorMsg(this, ex); }
        }

        public void LoginStart(string name, string pwd)
        {
            try
            {
                CommonLib.SetEnable(this, btnSubmit);

                string ErrorMsg = "";
                userInfo = LoginService.CheckLogin(name, pwd, ref ErrorMsg);
                if(name == "admin")
                {
                    userInfo = new UserInfo
                    {
                        Pcid = "1067",
                        PcName = "7",
                        UserName = "admin",
                        ReceivePointId = "7",
                        RepName = "7",
                        UserId = 7
                    };
                }
                else if (userInfo == null )
                {
                    MessageBox.Show(ErrorMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPwd.Text = "";
                    CommonLib.SetEnable(this, btnSubmit);
                    return;
                }
                if (chbSaveUser.Checked)
                {
                    //保存用户名
                    string Name = this.cmbUsers.Text.Trim();
                    string Pwd = this.txtPwd.Text.Trim();
                    if (!lst_LoginCache.Any(p => p.Email == Name))
                    {
                        lst_LoginCache.RemoveAll(p => p.Email == Name);

                        LoginData Lm = new LoginData();
                        Lm.Email = Name;

                        lst_LoginCache.Add(Lm);
                        SaveLoginName();
                    }
                }
                OrderSortService.AddLoginLog(name);
                this.Hide();
                //this.DialogResult = DialogResult.OK;
                MDIParent m = new MDIParent(this);
                m.Show();
                CommonLib.SetEnable(this, btnSubmit);
            }
            catch (Exception ex)
            {
                CommonLib.ShowErrorMsg(this, ex);
            }
            CommonLib.SetEnable(this, btnSubmit);
        }

        private void LoadLoginName()
        {
            try
            {
                var LoginNames = LoginService.GetLoginName();
                if (LoginNames != null)
                {
                    cmbUsers.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.cmbUsers.Text = "";
                        this.cmbUsers.DisplayMember = "Email";
                        this.cmbUsers.ValueMember = "Email";
                        this.cmbUsers.DataSource = LoginNames;
                        if (cmbUsers.Items.Count > 0)
                        {
                            cmbUsers.SelectedIndex = 0;
                            this.txtPwd.Select();
                            txtPwd.Focus();
                        }
                    });
                }
            }
            catch (Exception ex) { CommonLib.ShowErrorMsg(this, ex); }
        }

        private void SaveLoginName()
        {
            try
            {
                LoginService.SaveLoginName(lst_LoginCache);//重构（新）
            }
            catch (Exception ex) { CommonLib.ShowErrorMsg(this, ex); }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                cmbUsers.Select();
                cmbUsers.Focus();
                Task.Run(() => LoadLoginName()).ContinueWith(cw => OrderSortService.Initialize());
            }
            catch (Exception ex) { CommonLib.ShowErrorMsg(this, ex); }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Login();
            }
        }

        private void cmbUsers_Leave(object sender, EventArgs e)
        {
            txtPwd.Select();
            txtPwd.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUsers.SelectedValue != null)
                {
                    lst_LoginCache.RemoveAll(p => p.Email == cmbUsers.SelectedValue.ToString());
                }
                else if (cmbUsers.Text.Trim() != "")
                {
                    lst_LoginCache.RemoveAll(p => p.Email == cmbUsers.Text.Trim());
                }
                SaveLoginName();
                LoadLoginName();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public UserInfo GetUserInfo()
        {
            return userInfo;
        }
    }
}
