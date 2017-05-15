using SCB.OrderSorting.BLL;

namespace SCB.OrderSorting.Client
{
    partial class MDIParent
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            { 
                _frmLgn.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.分拣架设置newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分拣记录查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.装包记录查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分拣统计报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试订单标签打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.底层更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelname = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerShow = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分拣架设置newToolStripMenuItem,
            this.分拣记录查询ToolStripMenuItem,
            this.装包记录查询ToolStripMenuItem,
            this.分拣统计报表ToolStripMenuItem,
            this.系统设置ToolStripMenuItem,
            this.底层更新ToolStripMenuItem,
            this.测试订单标签打印ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1117, 35);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // 分拣架设置newToolStripMenuItem
            // 
            this.分拣架设置newToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.分拣架设置newToolStripMenuItem.Name = "分拣架设置newToolStripMenuItem";
            this.分拣架设置newToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.分拣架设置newToolStripMenuItem.Text = "分拣作业";
            this.分拣架设置newToolStripMenuItem.Click += new System.EventHandler(this.分拣架设置newToolStripMenuItem_Click);
            // 
            // 分拣记录查询ToolStripMenuItem
            // 
            this.分拣记录查询ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.分拣记录查询ToolStripMenuItem.Name = "分拣记录查询ToolStripMenuItem";
            this.分拣记录查询ToolStripMenuItem.Size = new System.Drawing.Size(144, 31);
            this.分拣记录查询ToolStripMenuItem.Text = "分拣记录查询";
            this.分拣记录查询ToolStripMenuItem.Click += new System.EventHandler(this.分拣记录查询ToolStripMenuItem_Click);
            // 
            // 装包记录查询ToolStripMenuItem
            // 
            this.装包记录查询ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.装包记录查询ToolStripMenuItem.Name = "装包记录查询ToolStripMenuItem";
            this.装包记录查询ToolStripMenuItem.Size = new System.Drawing.Size(144, 31);
            this.装包记录查询ToolStripMenuItem.Text = "装包记录查询";
            this.装包记录查询ToolStripMenuItem.Click += new System.EventHandler(this.装包记录查询ToolStripMenuItem_Click);
            // 
            // 分拣统计报表ToolStripMenuItem
            // 
            this.分拣统计报表ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.分拣统计报表ToolStripMenuItem.Name = "分拣统计报表ToolStripMenuItem";
            this.分拣统计报表ToolStripMenuItem.Size = new System.Drawing.Size(144, 31);
            this.分拣统计报表ToolStripMenuItem.Text = "分拣统计报表";
            this.分拣统计报表ToolStripMenuItem.Click += new System.EventHandler(this.分拣统计报表ToolStripMenuItem_Click);
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // 测试订单标签打印ToolStripMenuItem
            // 
            this.测试订单标签打印ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.测试订单标签打印ToolStripMenuItem.Name = "测试订单标签打印ToolStripMenuItem";
            this.测试订单标签打印ToolStripMenuItem.Size = new System.Drawing.Size(184, 31);
            this.测试订单标签打印ToolStripMenuItem.Text = "测试订单标签打印";
            this.测试订单标签打印ToolStripMenuItem.Visible = false;
            this.测试订单标签打印ToolStripMenuItem.Click += new System.EventHandler(this.测试订单标签打印ToolStripMenuItem_Click);
            // 
            // 底层更新ToolStripMenuItem
            // 
            this.底层更新ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.底层更新ToolStripMenuItem.Name = "底层更新ToolStripMenuItem";
            this.底层更新ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.底层更新ToolStripMenuItem.Text = "底层更新";
            this.底层更新ToolStripMenuItem.Click += new System.EventHandler(this.底层更新ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelname});
            this.statusStrip.Location = new System.Drawing.Point(0, 708);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(1117, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelname
            // 
            this.toolStripStatusLabelname.Name = "toolStripStatusLabelname";
            this.toolStripStatusLabelname.Size = new System.Drawing.Size(0, 17);
            // 
            // timerShow
            // 
            this.timerShow.Enabled = true;
            this.timerShow.Interval = 1000;
            this.timerShow.Tick += new System.EventHandler(this.timerShow_Tick);
            // 
            // MDIParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 730);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "MDIParent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 分拣架控制台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MDIParent_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem 分拣记录查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 装包记录查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分拣架设置newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Timer timerShow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelname;
        private System.Windows.Forms.ToolStripMenuItem 测试订单标签打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分拣统计报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 底层更新ToolStripMenuItem;
    }
}



