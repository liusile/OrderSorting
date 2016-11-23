namespace SCB.OrderSorting.Client
{
    partial class frmPkgPrint
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLatticeId = new System.Windows.Forms.TextBox();
            this.btnReprintByLatticeId = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "格号：";
            // 
            // txtLatticeId
            // 
            this.txtLatticeId.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtLatticeId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtLatticeId.Location = new System.Drawing.Point(29, 50);
            this.txtLatticeId.Margin = new System.Windows.Forms.Padding(2);
            this.txtLatticeId.Multiline = true;
            this.txtLatticeId.Name = "txtLatticeId";
            this.txtLatticeId.Size = new System.Drawing.Size(351, 125);
            this.txtLatticeId.TabIndex = 1;
            // 
            // btnReprintByLatticeId
            // 
            this.btnReprintByLatticeId.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReprintByLatticeId.Location = new System.Drawing.Point(103, 188);
            this.btnReprintByLatticeId.Margin = new System.Windows.Forms.Padding(2);
            this.btnReprintByLatticeId.Name = "btnReprintByLatticeId";
            this.btnReprintByLatticeId.Size = new System.Drawing.Size(277, 35);
            this.btnReprintByLatticeId.TabIndex = 5;
            this.btnReprintByLatticeId.Text = "打印PKG号";
            this.btnReprintByLatticeId.UseVisualStyleBackColor = true;
            this.btnReprintByLatticeId.Click += new System.EventHandler(this.btnReprintByLatticeId_Click);
            // 
            // frmPkgPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 234);
            this.Controls.Add(this.btnReprintByLatticeId);
            this.Controls.Add(this.txtLatticeId);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPkgPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PKG标签打印";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLatticeId;
        private System.Windows.Forms.Button btnReprintByLatticeId;
    }
}