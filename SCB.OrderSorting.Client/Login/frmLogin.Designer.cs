namespace SCB.OrderSorting.Client
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errMessLB = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.chbSaveUser = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoLogin = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(35, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "帐号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(35, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 27);
            this.label2.TabIndex = 6;
            this.label2.Text = "密码:";
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtPwd.Location = new System.Drawing.Point(114, 131);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtPwd.MaxLength = 30;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(315, 34);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.Tag = "";
            this.toolTip1.SetToolTip(this.txtPwd, "输入您的密码");
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSubmit.Location = new System.Drawing.Point(360, 20);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(112, 50);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "登录";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(227)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.errMessLB);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Location = new System.Drawing.Point(-2, 224);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 85);
            this.panel1.TabIndex = 6;
            // 
            // errMessLB
            // 
            this.errMessLB.AutoSize = true;
            this.errMessLB.ForeColor = System.Drawing.Color.Red;
            this.errMessLB.Location = new System.Drawing.Point(22, 16);
            this.errMessLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.errMessLB.Name = "errMessLB";
            this.errMessLB.Size = new System.Drawing.Size(0, 18);
            this.errMessLB.TabIndex = 4;
            this.errMessLB.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbUsers);
            this.panel2.Controls.Add(this.chbSaveUser);
            this.panel2.Controls.Add(this.checkBoxAutoLogin);
            this.panel2.Controls.Add(this.txtPwd);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(-2, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(492, 227);
            this.panel2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(109, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 27);
            this.label4.TabIndex = 12;
            this.label4.Text = "删除登录帐户";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cmbUsers
            // 
            this.cmbUsers.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(114, 42);
            this.cmbUsers.Margin = new System.Windows.Forms.Padding(4);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(315, 35);
            this.cmbUsers.TabIndex = 11;
            this.cmbUsers.Leave += new System.EventHandler(this.cmbUsers_Leave);
            // 
            // chbSaveUser
            // 
            this.chbSaveUser.AutoSize = true;
            this.chbSaveUser.Checked = true;
            this.chbSaveUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSaveUser.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.chbSaveUser.Location = new System.Drawing.Point(114, 185);
            this.chbSaveUser.Margin = new System.Windows.Forms.Padding(4);
            this.chbSaveUser.Name = "chbSaveUser";
            this.chbSaveUser.Size = new System.Drawing.Size(138, 31);
            this.chbSaveUser.TabIndex = 10;
            this.chbSaveUser.Text = "记住用户名";
            this.chbSaveUser.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoLogin
            // 
            this.checkBoxAutoLogin.AutoSize = true;
            this.checkBoxAutoLogin.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxAutoLogin.Location = new System.Drawing.Point(306, 185);
            this.checkBoxAutoLogin.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutoLogin.Name = "checkBoxAutoLogin";
            this.checkBoxAutoLogin.Size = new System.Drawing.Size(118, 31);
            this.checkBoxAutoLogin.TabIndex = 8;
            this.checkBoxAutoLogin.Text = "自动登陆";
            this.checkBoxAutoLogin.UseVisualStyleBackColor = true;
            this.checkBoxAutoLogin.Visible = false;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 294);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 350);
            this.MinimumSize = new System.Drawing.Size(512, 350);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录分拣架控制台";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label errMessLB;
        private System.Windows.Forms.CheckBox checkBoxAutoLogin;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.CheckBox chbSaveUser;
        private System.Windows.Forms.Label label4;
    }
}