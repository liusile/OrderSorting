namespace SCB.OrderSorting.Client.Setting
{
    partial class 格口设置
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
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cmb_qu = new System.Windows.Forms.ComboBox();
            this.cbIsEnable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPostTypes = new System.Windows.Forms.TextBox();
            this.btnPostTypes = new System.Windows.Forms.Button();
            this.txtPostTypeId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 23);
            this.label2.TabIndex = 38;
            this.label2.Text = "区：";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Save.Location = new System.Drawing.Point(176, 154);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 34);
            this.btn_Save.TabIndex = 40;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Cancel.Location = new System.Drawing.Point(7, 154);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(80, 34);
            this.btn_Cancel.TabIndex = 41;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // cmb_qu
            // 
            this.cmb_qu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_qu.FormattingEnabled = true;
            this.cmb_qu.Location = new System.Drawing.Point(62, 56);
            this.cmb_qu.Name = "cmb_qu";
            this.cmb_qu.Size = new System.Drawing.Size(250, 23);
            this.cmb_qu.TabIndex = 42;
            // 
            // cbIsEnable
            // 
            this.cbIsEnable.AutoSize = true;
            this.cbIsEnable.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbIsEnable.Location = new System.Drawing.Point(7, 92);
            this.cbIsEnable.Margin = new System.Windows.Forms.Padding(4);
            this.cbIsEnable.Name = "cbIsEnable";
            this.cbIsEnable.Size = new System.Drawing.Size(100, 27);
            this.cbIsEnable.TabIndex = 43;
            this.cbIsEnable.Text = "是否启用";
            this.cbIsEnable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(4, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 23);
            this.label1.TabIndex = 44;
            this.label1.Text = "渠道：";
            // 
            // txtPostTypes
            // 
            this.txtPostTypes.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtPostTypes.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPostTypes.Location = new System.Drawing.Point(62, 11);
            this.txtPostTypes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPostTypes.Name = "txtPostTypes";
            this.txtPostTypes.ReadOnly = true;
            this.txtPostTypes.Size = new System.Drawing.Size(250, 29);
            this.txtPostTypes.TabIndex = 45;
            // 
            // btnPostTypes
            // 
            this.btnPostTypes.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnPostTypes.Location = new System.Drawing.Point(281, 11);
            this.btnPostTypes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPostTypes.Name = "btnPostTypes";
            this.btnPostTypes.Size = new System.Drawing.Size(31, 29);
            this.btnPostTypes.TabIndex = 46;
            this.btnPostTypes.Text = "..";
            this.btnPostTypes.UseVisualStyleBackColor = true;
            this.btnPostTypes.Click += new System.EventHandler(this.btnPostTypes_Click);
            // 
            // txtPostTypeId
            // 
            this.txtPostTypeId.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtPostTypeId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPostTypeId.Location = new System.Drawing.Point(7, 214);
            this.txtPostTypeId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPostTypeId.Name = "txtPostTypeId";
            this.txtPostTypeId.ReadOnly = true;
            this.txtPostTypeId.Size = new System.Drawing.Size(194, 29);
            this.txtPostTypeId.TabIndex = 47;
            this.txtPostTypeId.Visible = false;
            // 
            // 格口设置
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 243);
            this.Controls.Add(this.txtPostTypeId);
            this.Controls.Add(this.btnPostTypes);
            this.Controls.Add(this.txtPostTypes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbIsEnable);
            this.Controls.Add(this.cmb_qu);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label2);
            this.Name = "格口设置";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "格口设置";
            this.Load += new System.EventHandler(this.格口设置_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cmb_qu;
        private System.Windows.Forms.CheckBox cbIsEnable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPostTypes;
        private System.Windows.Forms.Button btnPostTypes;
        private System.Windows.Forms.TextBox txtPostTypeId;
    }
}