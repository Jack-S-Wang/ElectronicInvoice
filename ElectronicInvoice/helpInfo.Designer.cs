namespace ElectronicInvoice
{
    partial class helpInfo
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
            this.lb_help = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_help
            // 
            this.lb_help.ForeColor = System.Drawing.Color.Lime;
            this.lb_help.FormattingEnabled = true;
            this.lb_help.ItemHeight = 22;
            this.lb_help.Location = new System.Drawing.Point(8, 20);
            this.lb_help.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.lb_help.Name = "lb_help";
            this.lb_help.Size = new System.Drawing.Size(986, 334);
            this.lb_help.TabIndex = 0;
            // 
            // helpInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 375);
            this.Controls.Add(this.lb_help);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "helpInfo";
            this.Text = "帮助";
            this.Load += new System.EventHandler(this.helpInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_help;
    }
}