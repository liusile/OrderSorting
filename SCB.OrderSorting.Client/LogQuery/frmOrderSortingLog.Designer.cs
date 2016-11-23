namespace SCB.OrderSorting.Client
{
    partial class frmOrderSortingLog
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
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.dgv_OrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TargetCabinetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TargetLatticeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ResultCabinetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ResultLatticeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OperationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OperationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerMain = new SCB.FDS.Client.Pager.WinFormPager();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOperationType = new System.Windows.Forms.ComboBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOrderId
            // 
            this.txtOrderId.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtOrderId.Location = new System.Drawing.Point(133, 4);
            this.txtOrderId.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(265, 29);
            this.txtOrderId.TabIndex = 81;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(12, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label2.Size = new System.Drawing.Size(101, 27);
            this.label2.TabIndex = 80;
            this.label2.Text = "订单编号：";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSearch.Location = new System.Drawing.Point(1364, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(133, 38);
            this.btnSearch.TabIndex = 83;
            this.btnSearch.Text = "查找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
            this.dgv_OrderId,
            this.dgv_TargetCabinetId,
            this.dgv_TargetLatticeId,
            this.dgv_ResultCabinetId,
            this.dgv_ResultLatticeId,
            this.dgv_OperationType,
            this.dgv_Status,
            this.dgv_OperationTime});
            this.dgvContent.Location = new System.Drawing.Point(16, 72);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.ReadOnly = true;
            this.dgvContent.RowTemplate.Height = 27;
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvContent.Size = new System.Drawing.Size(1484, 706);
            this.dgvContent.TabIndex = 84;
            this.dgvContent.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvContent_ColumnHeaderMouseClick);
            this.dgvContent.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvContent_DataBindingComplete);
            // 
            // dgv_OrderId
            // 
            this.dgv_OrderId.HeaderText = "订单号";
            this.dgv_OrderId.Name = "dgv_OrderId";
            this.dgv_OrderId.ReadOnly = true;
            this.dgv_OrderId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_OrderId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dgv_OrderId.Width = 150;
            // 
            // dgv_TargetCabinetId
            // 
            this.dgv_TargetCabinetId.HeaderText = "目标柜号";
            this.dgv_TargetCabinetId.Name = "dgv_TargetCabinetId";
            this.dgv_TargetCabinetId.ReadOnly = true;
            this.dgv_TargetCabinetId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_TargetCabinetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_TargetLatticeId
            // 
            this.dgv_TargetLatticeId.HeaderText = "目标格号";
            this.dgv_TargetLatticeId.Name = "dgv_TargetLatticeId";
            this.dgv_TargetLatticeId.ReadOnly = true;
            this.dgv_TargetLatticeId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_TargetLatticeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_ResultCabinetId
            // 
            this.dgv_ResultCabinetId.HeaderText = "投入柜号";
            this.dgv_ResultCabinetId.Name = "dgv_ResultCabinetId";
            this.dgv_ResultCabinetId.ReadOnly = true;
            this.dgv_ResultCabinetId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ResultCabinetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_ResultLatticeId
            // 
            this.dgv_ResultLatticeId.HeaderText = "投入格号";
            this.dgv_ResultLatticeId.Name = "dgv_ResultLatticeId";
            this.dgv_ResultLatticeId.ReadOnly = true;
            this.dgv_ResultLatticeId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ResultLatticeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_OperationType
            // 
            this.dgv_OperationType.HeaderText = "操作类型";
            this.dgv_OperationType.Name = "dgv_OperationType";
            this.dgv_OperationType.ReadOnly = true;
            this.dgv_OperationType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_OperationType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_Status
            // 
            this.dgv_Status.HeaderText = "状态";
            this.dgv_Status.Name = "dgv_Status";
            this.dgv_Status.ReadOnly = true;
            this.dgv_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_OperationTime
            // 
            this.dgv_OperationTime.HeaderText = "操作时间";
            this.dgv_OperationTime.Name = "dgv_OperationTime";
            this.dgv_OperationTime.ReadOnly = true;
            this.dgv_OperationTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_OperationTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dgv_OperationTime.Width = 150;
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
            this.pagerMain.Location = new System.Drawing.Point(15, 782);
            this.pagerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pagerMain.Name = "pagerMain";
            this.pagerMain.RecordCount = 0;
            this.pagerMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pagerMain.Size = new System.Drawing.Size(1481, 38);
            this.pagerMain.TabIndex = 85;
            this.pagerMain.Tag = "";
            this.pagerMain.TextImageRalitions = SCB.FDS.Client.Pager.WinFormPager.TextImageRalitionEnum.图片显示在文字上方;
            this.pagerMain.PageIndexChanged += new SCB.FDS.Client.Pager.WinFormPager.EventHandler(this.pagerMain_PageIndexChanged);
            this.pagerMain.PageSizeChanged += new SCB.FDS.Client.Pager.WinFormPager.EventHandler(this.pagerMain_PageSizeChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(411, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label1.Size = new System.Drawing.Size(101, 27);
            this.label1.TabIndex = 86;
            this.label1.Text = "操作类型：";
            // 
            // cbOperationType
            // 
            this.cbOperationType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbOperationType.FormattingEnabled = true;
            this.cbOperationType.Location = new System.Drawing.Point(532, 4);
            this.cbOperationType.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbOperationType.Name = "cbOperationType";
            this.cbOperationType.Size = new System.Drawing.Size(239, 31);
            this.cbOperationType.TabIndex = 87;
            // 
            // cbStatus
            // 
            this.cbStatus.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(867, 4);
            this.cbStatus.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(239, 31);
            this.cbStatus.TabIndex = 89;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(783, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label3.Size = new System.Drawing.Size(67, 27);
            this.label3.TabIndex = 88;
            this.label3.Text = "状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.Location = new System.Drawing.Point(411, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label4.Size = new System.Drawing.Size(101, 27);
            this.label4.TabIndex = 96;
            this.label4.Text = "结束时间：";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpEndDate.Location = new System.Drawing.Point(532, 41);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(239, 25);
            this.dtpEndDate.TabIndex = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.Location = new System.Drawing.Point(13, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label5.Size = new System.Drawing.Size(101, 27);
            this.label5.TabIndex = 94;
            this.label5.Text = "开始时间：";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpStartDate.Location = new System.Drawing.Point(133, 41);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(265, 25);
            this.dtpStartDate.TabIndex = 93;
            // 
            // frmOrderSortingLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 826);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOperationType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pagerMain);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmOrderSortingLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拣记录查询";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvContent;
        private FDS.Client.Pager.WinFormPager pagerMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOperationType;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TargetCabinetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TargetLatticeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ResultCabinetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ResultLatticeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OperationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OperationTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
    }
}