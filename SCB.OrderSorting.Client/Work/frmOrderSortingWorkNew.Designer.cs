namespace SCB.OrderSorting.Client
{
    partial class frmOrderSortingWorkNew
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始分拣ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.断电恢复ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部清除重扫ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pKG条码重打ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分拣撤回ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.方案管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.方案重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pKG标签打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始分拣ToolStripMenuItem,
            this.断电恢复ToolStripMenuItem,
            this.全部清除重扫ToolStripMenuItem,
            this.pKG条码重打ToolStripMenuItem,
            this.分拣撤回ToolStripMenuItem1,
            this.方案管理ToolStripMenuItem,
            this.pKG标签打印ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1002, 35);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始分拣ToolStripMenuItem
            // 
            this.开始分拣ToolStripMenuItem.Enabled = false;
            this.开始分拣ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.开始分拣ToolStripMenuItem.Name = "开始分拣ToolStripMenuItem";
            this.开始分拣ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.开始分拣ToolStripMenuItem.Text = "开始分拣";
            this.开始分拣ToolStripMenuItem.Click += new System.EventHandler(this.开始分拣ToolStripMenuItem_Click);
            // 
            // 断电恢复ToolStripMenuItem
            // 
            this.断电恢复ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.断电恢复ToolStripMenuItem.Name = "断电恢复ToolStripMenuItem";
            this.断电恢复ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.断电恢复ToolStripMenuItem.Text = "断电恢复";
            this.断电恢复ToolStripMenuItem.Click += new System.EventHandler(this.断电恢复ToolStripMenuItem_Click);
            // 
            // 全部清除重扫ToolStripMenuItem
            // 
            this.全部清除重扫ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.全部清除重扫ToolStripMenuItem.Name = "全部清除重扫ToolStripMenuItem";
            this.全部清除重扫ToolStripMenuItem.Size = new System.Drawing.Size(144, 31);
            this.全部清除重扫ToolStripMenuItem.Text = "全部清除重扫";
            this.全部清除重扫ToolStripMenuItem.Visible = false;
            this.全部清除重扫ToolStripMenuItem.Click += new System.EventHandler(this.全部清除重扫ToolStripMenuItem_Click);
            // 
            // pKG条码重打ToolStripMenuItem
            // 
            this.pKG条码重打ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.pKG条码重打ToolStripMenuItem.Name = "pKG条码重打ToolStripMenuItem";
            this.pKG条码重打ToolStripMenuItem.Size = new System.Drawing.Size(146, 31);
            this.pKG条码重打ToolStripMenuItem.Text = "PKG条码重打";
            this.pKG条码重打ToolStripMenuItem.Click += new System.EventHandler(this.pKG条码重打ToolStripMenuItem_Click);
            // 
            // 分拣撤回ToolStripMenuItem1
            // 
            this.分拣撤回ToolStripMenuItem1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.分拣撤回ToolStripMenuItem1.Name = "分拣撤回ToolStripMenuItem1";
            this.分拣撤回ToolStripMenuItem1.Size = new System.Drawing.Size(104, 31);
            this.分拣撤回ToolStripMenuItem1.Text = "分拣撤回";
            this.分拣撤回ToolStripMenuItem1.Click += new System.EventHandler(this.分拣撤回ToolStripMenuItem_Click);
            // 
            // 方案管理ToolStripMenuItem
            // 
            this.方案管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建方案ToolStripMenuItem,
            this.方案重命名ToolStripMenuItem});
            this.方案管理ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.方案管理ToolStripMenuItem.Name = "方案管理ToolStripMenuItem";
            this.方案管理ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.方案管理ToolStripMenuItem.Text = "方案管理";
            // 
            // 新建方案ToolStripMenuItem
            // 
            this.新建方案ToolStripMenuItem.Name = "新建方案ToolStripMenuItem";
            this.新建方案ToolStripMenuItem.Size = new System.Drawing.Size(190, 32);
            this.新建方案ToolStripMenuItem.Text = "新建方案";
            this.新建方案ToolStripMenuItem.Click += new System.EventHandler(this.新建方案ToolStripMenuItem_Click);
            // 
            // 方案重命名ToolStripMenuItem
            // 
            this.方案重命名ToolStripMenuItem.Name = "方案重命名ToolStripMenuItem";
            this.方案重命名ToolStripMenuItem.Size = new System.Drawing.Size(190, 32);
            this.方案重命名ToolStripMenuItem.Text = "方案重命名";
            this.方案重命名ToolStripMenuItem.Click += new System.EventHandler(this.方案重命名ToolStripMenuItem_Click);
            // 
            // pKG标签打印ToolStripMenuItem
            // 
            this.pKG标签打印ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.pKG标签打印ToolStripMenuItem.Name = "pKG标签打印ToolStripMenuItem";
            this.pKG标签打印ToolStripMenuItem.Size = new System.Drawing.Size(146, 31);
            this.pKG标签打印ToolStripMenuItem.Text = "PKG标签打印";
            this.pKG标签打印ToolStripMenuItem.Click += new System.EventHandler(this.pKG标签打印ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(22, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "订单号：";
            // 
            // txtOrderId
            // 
            this.txtOrderId.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderId.Enabled = false;
            this.txtOrderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOrderId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOrderId.Location = new System.Drawing.Point(130, 40);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(111, 34);
            this.txtOrderId.TabIndex = 9;
            this.txtOrderId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderId_KeyDown);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(248, 45);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(167, 30);
            this.lblMsg.TabIndex = 10;
            this.lblMsg.Text = "您还未开始分拣";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(876, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "测试调试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmOrderSortingWorkNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 584);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmOrderSortingWorkNew";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拣架作业";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrderSortingWorkNew_FormClosing);
            this.Load += new System.EventHandler(this.frmLatticeSettingSearch_Load);
            this.SizeChanged += new System.EventHandler(this.frmLatticeSettingSearch_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始分拣ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 断电恢复ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部清除重扫ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pKG条码重打ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分拣撤回ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 方案管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 方案重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pKG标签打印ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button button1;
    }
}