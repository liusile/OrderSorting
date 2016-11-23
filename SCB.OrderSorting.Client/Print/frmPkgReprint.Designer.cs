namespace SCB.OrderSorting.Client
{
    partial class frmPkgReprint
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
            this.btnReprintByPkg = new System.Windows.Forms.Button();
            this.txtPkg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReprintByLatticeId = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(25, 22);
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
            this.txtLatticeId.Location = new System.Drawing.Point(103, 20);
            this.txtLatticeId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLatticeId.Name = "txtLatticeId";
            this.txtLatticeId.Size = new System.Drawing.Size(277, 29);
            this.txtLatticeId.TabIndex = 1;
            // 
            // btnReprintByPkg
            // 
            this.btnReprintByPkg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReprintByPkg.Location = new System.Drawing.Point(103, 153);
            this.btnReprintByPkg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReprintByPkg.Name = "btnReprintByPkg";
            this.btnReprintByPkg.Size = new System.Drawing.Size(277, 35);
            this.btnReprintByPkg.TabIndex = 2;
            this.btnReprintByPkg.Text = "重新打印";
            this.btnReprintByPkg.UseVisualStyleBackColor = true;
            this.btnReprintByPkg.Click += new System.EventHandler(this.btnReprintByPkg_Click);
            // 
            // txtPkg
            // 
            this.txtPkg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtPkg.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPkg.Location = new System.Drawing.Point(103, 113);
            this.txtPkg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPkg.Name = "txtPkg";
            this.txtPkg.Size = new System.Drawing.Size(277, 29);
            this.txtPkg.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(25, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "PKG号：";
            // 
            // btnReprintByLatticeId
            // 
            this.btnReprintByLatticeId.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReprintByLatticeId.Location = new System.Drawing.Point(103, 60);
            this.btnReprintByLatticeId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReprintByLatticeId.Name = "btnReprintByLatticeId";
            this.btnReprintByLatticeId.Size = new System.Drawing.Size(277, 35);
            this.btnReprintByLatticeId.TabIndex = 5;
            this.btnReprintByLatticeId.Text = "根据格号重新打印";
            this.btnReprintByLatticeId.UseVisualStyleBackColor = true;
            this.btnReprintByLatticeId.Click += new System.EventHandler(this.btnReprintByLatticeId_Click);
            // 
            // frmPkgReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 208);
            this.Controls.Add(this.btnReprintByLatticeId);
            this.Controls.Add(this.txtPkg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReprintByPkg);
            this.Controls.Add(this.txtLatticeId);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmPkgReprint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PKG条码重打";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLatticeId;
        private System.Windows.Forms.Button btnReprintByPkg;
        private System.Windows.Forms.TextBox txtPkg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReprintByLatticeId;
    }
}