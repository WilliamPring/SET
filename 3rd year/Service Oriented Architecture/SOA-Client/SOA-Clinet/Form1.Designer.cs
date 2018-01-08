namespace SOA_Clinet
{
    partial class Form1
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
            this.cbServices = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnStart = new System.Windows.Forms.Button();
            this.panControls = new System.Windows.Forms.Panel();
            this.btnSendArgs = new System.Windows.Forms.Button();
            this.bttnClearPanal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbServices
            // 
            this.cbServices.FormattingEnabled = true;
            this.cbServices.Items.AddRange(new object[] {
            "GIORP-TOTAL",
            "PAYROLL",
            "CAR-LOAN",
            "POSTAL"});
            this.cbServices.Location = new System.Drawing.Point(16, 45);
            this.cbServices.Name = "cbServices";
            this.cbServices.Size = new System.Drawing.Size(275, 28);
            this.cbServices.TabIndex = 0;
            this.cbServices.SelectedIndexChanged += new System.EventHandler(this.cbServices_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Which services do you want to execute?";
            // 
            // bttnStart
            // 
            this.bttnStart.Location = new System.Drawing.Point(298, 45);
            this.bttnStart.Name = "bttnStart";
            this.bttnStart.Size = new System.Drawing.Size(225, 35);
            this.bttnStart.TabIndex = 2;
            this.bttnStart.Text = "Get Services";
            this.bttnStart.UseVisualStyleBackColor = true;
            this.bttnStart.Click += new System.EventHandler(this.bttnStart_Click);
            // 
            // panControls
            // 
            this.panControls.Location = new System.Drawing.Point(16, 101);
            this.panControls.Name = "panControls";
            this.panControls.Size = new System.Drawing.Size(588, 403);
            this.panControls.TabIndex = 3;
            // 
            // btnSendArgs
            // 
            this.btnSendArgs.Location = new System.Drawing.Point(16, 510);
            this.btnSendArgs.Name = "btnSendArgs";
            this.btnSendArgs.Size = new System.Drawing.Size(588, 50);
            this.btnSendArgs.TabIndex = 4;
            this.btnSendArgs.Text = "Execute Function";
            this.btnSendArgs.UseVisualStyleBackColor = true;
            this.btnSendArgs.Click += new System.EventHandler(this.btnSendArgs_Click);
            // 
            // bttnClearPanal
            // 
            this.bttnClearPanal.Location = new System.Drawing.Point(529, 45);
            this.bttnClearPanal.Name = "bttnClearPanal";
            this.bttnClearPanal.Size = new System.Drawing.Size(75, 35);
            this.bttnClearPanal.TabIndex = 6;
            this.bttnClearPanal.Text = "CLEAR";
            this.bttnClearPanal.UseVisualStyleBackColor = true;
            this.bttnClearPanal.Click += new System.EventHandler(this.bttnClearPanal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 572);
            this.Controls.Add(this.bttnClearPanal);
            this.Controls.Add(this.btnSendArgs);
            this.Controls.Add(this.panControls);
            this.Controls.Add(this.bttnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbServices);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbServices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttnStart;
        private System.Windows.Forms.Panel panControls;
        private System.Windows.Forms.Button btnSendArgs;
        private System.Windows.Forms.Button bttnClearPanal;
    }
}

