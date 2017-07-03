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
            this.label2 = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "格号(多个格口，用回车键分隔)：";
            // 
            // txtLatticeId
            // 
            this.txtLatticeId.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtLatticeId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtLatticeId.Location = new System.Drawing.Point(39, 62);
            this.txtLatticeId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLatticeId.Multiline = true;
            this.txtLatticeId.Name = "txtLatticeId";
            this.txtLatticeId.Size = new System.Drawing.Size(467, 155);
            this.txtLatticeId.TabIndex = 1;
            // 
            // btnReprintByLatticeId
            // 
            this.btnReprintByLatticeId.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReprintByLatticeId.Location = new System.Drawing.Point(137, 277);
            this.btnReprintByLatticeId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReprintByLatticeId.Name = "btnReprintByLatticeId";
            this.btnReprintByLatticeId.Size = new System.Drawing.Size(369, 44);
            this.btnReprintByLatticeId.TabIndex = 5;
            this.btnReprintByLatticeId.Text = "打印PKG号";
            this.btnReprintByLatticeId.UseVisualStyleBackColor = true;
            this.btnReprintByLatticeId.Click += new System.EventHandler(this.btnReprintByLatticeId_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(33, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 6;
            this.label2.Text = "打印张数：";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(152, 229);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(100, 25);
            this.txtNum.TabIndex = 7;
            this.txtNum.Text = "1";
            // 
            // frmPkgPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 332);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReprintByLatticeId);
            this.Controls.Add(this.txtLatticeId);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNum;
    }
}