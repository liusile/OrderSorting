namespace SCB.OrderSorting.Client
{
    partial class frmTestOrderPrint
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
            this.txtOrderid = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnBatchOutboundTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOrderid
            // 
            this.txtOrderid.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtOrderid.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOrderid.Location = new System.Drawing.Point(12, 12);
            this.txtOrderid.Multiline = true;
            this.txtOrderid.Name = "txtOrderid";
            this.txtOrderid.Size = new System.Drawing.Size(290, 278);
            this.txtOrderid.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPrint.Location = new System.Drawing.Point(227, 296);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnBatchOutboundTest
            // 
            this.btnBatchOutboundTest.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnBatchOutboundTest.Location = new System.Drawing.Point(13, 297);
            this.btnBatchOutboundTest.Name = "btnBatchOutboundTest";
            this.btnBatchOutboundTest.Size = new System.Drawing.Size(178, 31);
            this.btnBatchOutboundTest.TabIndex = 2;
            this.btnBatchOutboundTest.Text = "BatchOutbound测试";
            this.btnBatchOutboundTest.UseVisualStyleBackColor = true;
            this.btnBatchOutboundTest.Visible = false;
            this.btnBatchOutboundTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTestOrderPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 340);
            this.Controls.Add(this.btnBatchOutboundTest);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtOrderid);
            this.Name = "frmTestOrderPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测试订单标签打印";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOrderid;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnBatchOutboundTest;
    }
}