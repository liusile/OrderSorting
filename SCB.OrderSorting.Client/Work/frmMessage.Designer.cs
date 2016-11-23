namespace SCB.OrderSorting.Client
{
    partial class frmMessage
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
            this.ok = new System.Windows.Forms.Button();
            this.lb_Msg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(74, 123);
            this.ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(56, 23);
            this.ok.TabIndex = 1;
            this.ok.Text = "确定";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ok_Click);
            // 
            // lb_Msg
            // 
            this.lb_Msg.AutoSize = true;
            this.lb_Msg.Location = new System.Drawing.Point(57, 44);
            this.lb_Msg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Msg.Name = "lb_Msg";
            this.lb_Msg.Size = new System.Drawing.Size(0, 12);
            this.lb_Msg.TabIndex = 0;
            this.lb_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 202);
            this.Controls.Add(this.lb_Msg);
            this.Controls.Add(this.ok);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label lb_Msg;
    }
}