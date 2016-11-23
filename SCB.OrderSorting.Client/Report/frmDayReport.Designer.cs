namespace SCB.OrderSorting.Client
{
    partial class frmDayReport
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
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.dgv_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_waitPut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_over = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_LactionError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_RepeatError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pagerMain = new SCB.FDS.Client.Pager.WinFormPager();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.Location = new System.Drawing.Point(319, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(2);
            this.label4.Size = new System.Drawing.Size(83, 24);
            this.label4.TabIndex = 100;
            this.label4.Text = "结束时间：";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpEndDate.Location = new System.Drawing.Point(410, 10);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(180, 21);
            this.dtpEndDate.TabIndex = 99;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.Location = new System.Drawing.Point(20, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(2);
            this.label5.Size = new System.Drawing.Size(83, 24);
            this.label5.TabIndex = 98;
            this.label5.Text = "开始时间：";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpStartDate.Location = new System.Drawing.Point(110, 10);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 21);
            this.dtpStartDate.TabIndex = 97;
            // 
            // dgvContent
            // 
            this.dgvContent.AllowUserToAddRows = false;
            this.dgvContent.AllowUserToDeleteRows = false;
            this.dgvContent.AllowUserToOrderColumns = true;
            this.dgvContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvContent.BackgroundColor = System.Drawing.Color.White;
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_Time,
            this.dgv_waitPut,
            this.dgv_over,
            this.dgv_LactionError,
            this.dgv_RepeatError,
            this.dgv_sum});
            this.dgvContent.Location = new System.Drawing.Point(12, 58);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.ReadOnly = true;
            this.dgvContent.RowTemplate.Height = 27;
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvContent.Size = new System.Drawing.Size(1099, 558);
            this.dgvContent.TabIndex = 101;
            // 
            // dgv_Time
            // 
            this.dgv_Time.HeaderText = "时间";
            this.dgv_Time.Name = "dgv_Time";
            this.dgv_Time.ReadOnly = true;
            // 
            // dgv_waitPut
            // 
            this.dgv_waitPut.HeaderText = "待投递";
            this.dgv_waitPut.Name = "dgv_waitPut";
            this.dgv_waitPut.ReadOnly = true;
            // 
            // dgv_over
            // 
            this.dgv_over.HeaderText = "已投递";
            this.dgv_over.Name = "dgv_over";
            this.dgv_over.ReadOnly = true;
            // 
            // dgv_LactionError
            // 
            this.dgv_LactionError.HeaderText = "投递异常";
            this.dgv_LactionError.Name = "dgv_LactionError";
            this.dgv_LactionError.ReadOnly = true;
            // 
            // dgv_RepeatError
            // 
            this.dgv_RepeatError.HeaderText = "重复扫描";
            this.dgv_RepeatError.Name = "dgv_RepeatError";
            this.dgv_RepeatError.ReadOnly = true;
            // 
            // dgv_sum
            // 
            this.dgv_sum.HeaderText = "总计";
            this.dgv_sum.Name = "dgv_sum";
            this.dgv_sum.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSearch.Location = new System.Drawing.Point(1006, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 103;
            this.btnSearch.Text = "查找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pagerMain
            // 
            this.pagerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerMain.AppendPageSizeItems.AddRange(new object[] {
            "30"});
            this.pagerMain.BackColor = System.Drawing.SystemColors.Control;
            this.pagerMain.BtnTextNext = "下页";
            this.pagerMain.BtnTextPrevious = "上页";
            this.pagerMain.DisplayStyle = SCB.FDS.Client.Pager.WinFormPager.DisplayStyleEnum.文字;
            this.pagerMain.Location = new System.Drawing.Point(9, 620);
            this.pagerMain.Margin = new System.Windows.Forms.Padding(2);
            this.pagerMain.Name = "pagerMain";
            this.pagerMain.RecordCount = 0;
            this.pagerMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pagerMain.Size = new System.Drawing.Size(1100, 30);
            this.pagerMain.TabIndex = 104;
            this.pagerMain.Tag = "";
            this.pagerMain.TextImageRalitions = SCB.FDS.Client.Pager.WinFormPager.TextImageRalitionEnum.图片显示在文字上方;
            this.pagerMain.PageIndexChanged += new SCB.FDS.Client.Pager.WinFormPager.EventHandler(this.pagerMain_PageIndexChanged);
            this.pagerMain.PageSizeChanged += new SCB.FDS.Client.Pager.WinFormPager.EventHandler(this.pagerMain_PageSizeChanged);
            // 
            // frmDayReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 661);
            this.Controls.Add(this.pagerMain);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpStartDate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmDayReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拣统计报表";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_waitPut;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_over;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_LactionError;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_RepeatError;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_sum;
        private System.Windows.Forms.Button btnSearch;
        private FDS.Client.Pager.WinFormPager pagerMain;
    }
}