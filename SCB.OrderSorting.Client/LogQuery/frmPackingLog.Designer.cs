namespace SCB.OrderSorting.Client
{
    partial class frmPackingLog
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.pagerMain = new SCB.FDS.Client.Pager.WinFormPager();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dgv_PackNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_CabinetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_LatticeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PostTypeNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_CountryNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OperationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OperationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSearch.Location = new System.Drawing.Point(1009, 13);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
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
            this.dgv_PackNumber,
            this.dgv_CabinetId,
            this.dgv_LatticeId,
            this.dgv_PostTypeNames,
            this.dgv_CountryNames,
            this.dgv_UserName,
            this.dgv_OrderQty,
            this.dgv_OperationType,
            this.dgv_OperationTime});
            this.dgvContent.Location = new System.Drawing.Point(12, 58);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.ReadOnly = true;
            this.dgvContent.RowTemplate.Height = 27;
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvContent.Size = new System.Drawing.Size(1110, 565);
            this.dgvContent.TabIndex = 84;
            this.dgvContent.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvContent_ColumnHeaderMouseClick);
            this.dgvContent.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvContent_DataBindingComplete);
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
            this.pagerMain.Location = new System.Drawing.Point(11, 626);
            this.pagerMain.Margin = new System.Windows.Forms.Padding(2);
            this.pagerMain.Name = "pagerMain";
            this.pagerMain.RecordCount = 0;
            this.pagerMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pagerMain.Size = new System.Drawing.Size(1111, 30);
            this.pagerMain.TabIndex = 85;
            this.pagerMain.Tag = "";
            this.pagerMain.TextImageRalitions = SCB.FDS.Client.Pager.WinFormPager.TextImageRalitionEnum.图片显示在文字上方;
            this.pagerMain.PageIndexChanged += new SCB.FDS.Client.Pager.WinFormPager.EventHandler(this.pagerMain_PageIndexChanged);
            this.pagerMain.PageSizeChanged += new SCB.FDS.Client.Pager.WinFormPager.EventHandler(this.pagerMain_PageSizeChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpStartDate.Location = new System.Drawing.Point(107, 19);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(150, 21);
            this.dtpStartDate.TabIndex = 88;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2);
            this.label2.Size = new System.Drawing.Size(83, 24);
            this.label2.TabIndex = 90;
            this.label2.Text = "开始时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(273, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2);
            this.label3.Size = new System.Drawing.Size(83, 24);
            this.label3.TabIndex = 92;
            this.label3.Text = "结束时间：";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpEndDate.Location = new System.Drawing.Point(363, 19);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(150, 21);
            this.dtpEndDate.TabIndex = 91;
            // 
            // dgv_PackNumber
            // 
            this.dgv_PackNumber.HeaderText = "包牌号";
            this.dgv_PackNumber.Name = "dgv_PackNumber";
            this.dgv_PackNumber.ReadOnly = true;
            this.dgv_PackNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_PackNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dgv_PackNumber.Width = 120;
            // 
            // dgv_CabinetId
            // 
            this.dgv_CabinetId.HeaderText = "柜号";
            this.dgv_CabinetId.Name = "dgv_CabinetId";
            this.dgv_CabinetId.ReadOnly = true;
            this.dgv_CabinetId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CabinetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_LatticeId
            // 
            this.dgv_LatticeId.HeaderText = "格号";
            this.dgv_LatticeId.Name = "dgv_LatticeId";
            this.dgv_LatticeId.ReadOnly = true;
            this.dgv_LatticeId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_LatticeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_PostTypeNames
            // 
            this.dgv_PostTypeNames.HeaderText = "渠道";
            this.dgv_PostTypeNames.Name = "dgv_PostTypeNames";
            this.dgv_PostTypeNames.ReadOnly = true;
            this.dgv_PostTypeNames.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dgv_PostTypeNames.Width = 130;
            // 
            // dgv_CountryNames
            // 
            this.dgv_CountryNames.HeaderText = "地区";
            this.dgv_CountryNames.Name = "dgv_CountryNames";
            this.dgv_CountryNames.ReadOnly = true;
            this.dgv_CountryNames.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dgv_CountryNames.Width = 130;
            // 
            // dgv_UserName
            // 
            this.dgv_UserName.HeaderText = "操作员";
            this.dgv_UserName.Name = "dgv_UserName";
            this.dgv_UserName.ReadOnly = true;
            this.dgv_UserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_OrderQty
            // 
            this.dgv_OrderQty.HeaderText = "订单数量";
            this.dgv_OrderQty.Name = "dgv_OrderQty";
            this.dgv_OrderQty.ReadOnly = true;
            this.dgv_OrderQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dgv_OperationType
            // 
            this.dgv_OperationType.HeaderText = "操作类型";
            this.dgv_OperationType.Name = "dgv_OperationType";
            this.dgv_OperationType.ReadOnly = true;
            this.dgv_OperationType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_OperationType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
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
            // frmPackingLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 661);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.pagerMain);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.btnSearch);
            this.Name = "frmPackingLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "装包记录查询";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvContent;
        private FDS.Client.Pager.WinFormPager pagerMain;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PackNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_CabinetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_LatticeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PostTypeNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_CountryNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OperationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OperationTime;
    }
}