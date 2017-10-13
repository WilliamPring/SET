namespace ClinetConsumingWebservices
{
    partial class fmWebServiceConsume
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
            this.cbWebServices = new System.Windows.Forms.ComboBox();
            this.bttnSubmit = new System.Windows.Forms.Button();
            this.lbFuctionDisplay = new System.Windows.Forms.Label();
            this.lbFunctionName = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // cbWebServices
            // 
            this.cbWebServices.FormattingEnabled = true;
            this.cbWebServices.Items.AddRange(new object[] {
            "TextService",
            "VinniesLoanService",
            "TickerTape"});
            this.cbWebServices.Location = new System.Drawing.Point(12, 43);
            this.cbWebServices.Name = "cbWebServices";
            this.cbWebServices.Size = new System.Drawing.Size(277, 28);
            this.cbWebServices.TabIndex = 0;
            this.cbWebServices.SelectedIndexChanged += new System.EventHandler(this.cbWebServices_SelectedIndexChanged);
            // 
            // bttnSubmit
            // 
            this.bttnSubmit.Location = new System.Drawing.Point(275, 427);
            this.bttnSubmit.Name = "bttnSubmit";
            this.bttnSubmit.Size = new System.Drawing.Size(75, 43);
            this.bttnSubmit.TabIndex = 1;
            this.bttnSubmit.Text = "Submit";
            this.bttnSubmit.UseVisualStyleBackColor = true;
            this.bttnSubmit.Click += new System.EventHandler(this.bttnSubmit_Click);
            // 
            // lbFuctionDisplay
            // 
            this.lbFuctionDisplay.AutoSize = true;
            this.lbFuctionDisplay.Location = new System.Drawing.Point(328, 46);
            this.lbFuctionDisplay.Name = "lbFuctionDisplay";
            this.lbFuctionDisplay.Size = new System.Drawing.Size(125, 20);
            this.lbFuctionDisplay.TabIndex = 2;
            this.lbFuctionDisplay.Text = "Function Name: ";
            // 
            // lbFunctionName
            // 
            this.lbFunctionName.AutoSize = true;
            this.lbFunctionName.Location = new System.Drawing.Point(451, 46);
            this.lbFunctionName.Name = "lbFunctionName";
            this.lbFunctionName.Size = new System.Drawing.Size(0, 20);
            this.lbFunctionName.TabIndex = 3;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 101);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(277, 306);
            this.flowLayoutPanel.TabIndex = 4;
            // 
            // fmWebServiceConsume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 546);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.lbFunctionName);
            this.Controls.Add(this.lbFuctionDisplay);
            this.Controls.Add(this.bttnSubmit);
            this.Controls.Add(this.cbWebServices);
            this.Name = "fmWebServiceConsume";
            this.Text = "SOAPConsume";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbWebServices;
        private System.Windows.Forms.Button bttnSubmit;
        private System.Windows.Forms.Label lbFuctionDisplay;
        private System.Windows.Forms.Label lbFunctionName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}

