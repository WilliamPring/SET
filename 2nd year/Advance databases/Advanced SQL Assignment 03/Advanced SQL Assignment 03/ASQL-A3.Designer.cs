﻿namespace Advanced_SQL_Assignment_03
{
    partial class ASQL
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
            this.submit = new System.Windows.Forms.Button();
            this.sourceTable = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.destinationTableLbl = new System.Windows.Forms.Label();
            this.destinationTable = new System.Windows.Forms.TextBox();
            this.errorMessage = new System.Windows.Forms.Label();
            this.ConnectionStringSourceBtn = new System.Windows.Forms.Button();
            this.ConnectionStringDestinationBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(19, 152);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(189, 82);
            this.submit.TabIndex = 4;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // sourceTable
            // 
            this.sourceTable.Location = new System.Drawing.Point(108, 15);
            this.sourceTable.Name = "sourceTable";
            this.sourceTable.Size = new System.Drawing.Size(100, 20);
            this.sourceTable.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Souce Table";
            // 
            // destinationTableLbl
            // 
            this.destinationTableLbl.AutoSize = true;
            this.destinationTableLbl.Location = new System.Drawing.Point(12, 88);
            this.destinationTableLbl.Name = "destinationTableLbl";
            this.destinationTableLbl.Size = new System.Drawing.Size(90, 13);
            this.destinationTableLbl.TabIndex = 15;
            this.destinationTableLbl.Text = "Destination Table";
            // 
            // destinationTable
            // 
            this.destinationTable.Location = new System.Drawing.Point(108, 85);
            this.destinationTable.Name = "destinationTable";
            this.destinationTable.Size = new System.Drawing.Size(100, 20);
            this.destinationTable.TabIndex = 13;
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(12, 239);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(0, 13);
            this.errorMessage.TabIndex = 21;
            // 
            // ConnectionStringSourceBtn
            // 
            this.ConnectionStringSourceBtn.Location = new System.Drawing.Point(108, 41);
            this.ConnectionStringSourceBtn.Name = "ConnectionStringSourceBtn";
            this.ConnectionStringSourceBtn.Size = new System.Drawing.Size(100, 35);
            this.ConnectionStringSourceBtn.TabIndex = 24;
            this.ConnectionStringSourceBtn.Text = "Connection String Source";
            this.ConnectionStringSourceBtn.UseVisualStyleBackColor = true;
            this.ConnectionStringSourceBtn.Click += new System.EventHandler(this.ConnectionStringSourceBtn_Click);
            // 
            // ConnectionStringDestinationBtn
            // 
            this.ConnectionStringDestinationBtn.Location = new System.Drawing.Point(108, 111);
            this.ConnectionStringDestinationBtn.Name = "ConnectionStringDestinationBtn";
            this.ConnectionStringDestinationBtn.Size = new System.Drawing.Size(100, 35);
            this.ConnectionStringDestinationBtn.TabIndex = 25;
            this.ConnectionStringDestinationBtn.Text = "Connection String Destination";
            this.ConnectionStringDestinationBtn.UseVisualStyleBackColor = true;
            this.ConnectionStringDestinationBtn.Click += new System.EventHandler(this.ConnectionStringDestinationBtn_Click);
            // 
            // ASQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 241);
            this.Controls.Add(this.ConnectionStringDestinationBtn);
            this.Controls.Add(this.ConnectionStringSourceBtn);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.destinationTableLbl);
            this.Controls.Add(this.destinationTable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sourceTable);
            this.Controls.Add(this.submit);
            this.Name = "ASQL";
            this.Text = "ASQL-A3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.TextBox sourceTable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label destinationTableLbl;
        private System.Windows.Forms.TextBox destinationTable;
        private System.Windows.Forms.Label errorMessage;
        private System.Windows.Forms.Button ConnectionStringSourceBtn;
        private System.Windows.Forms.Button ConnectionStringDestinationBtn;
    }
}

