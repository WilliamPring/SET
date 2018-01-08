namespace UIA2
{
    partial class GenerateReportForms
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStudentID = new System.Windows.Forms.TextBox();
            this.bttnSerach = new System.Windows.Forms.Button();
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AutoFill = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student First Nane: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Student Last Name: ";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(173, 34);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(223, 26);
            this.tbFirstName.TabIndex = 2;
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(173, 68);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(223, 26);
            this.tbLastName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Student Number: ";
            // 
            // tbStudentID
            // 
            this.tbStudentID.Location = new System.Drawing.Point(173, 100);
            this.tbStudentID.Name = "tbStudentID";
            this.tbStudentID.Size = new System.Drawing.Size(223, 26);
            this.tbStudentID.TabIndex = 5;
            // 
            // bttnSerach
            // 
            this.bttnSerach.Location = new System.Drawing.Point(378, 470);
            this.bttnSerach.Name = "bttnSerach";
            this.bttnSerach.Size = new System.Drawing.Size(59, 30);
            this.bttnSerach.TabIndex = 6;
            this.bttnSerach.Text = "Q";
            this.bttnSerach.UseVisualStyleBackColor = true;
            this.bttnSerach.Click += new System.EventHandler(this.bttnSerach_Click);
            // 
            // tbDirectory
            // 
            this.tbDirectory.Location = new System.Drawing.Point(68, 470);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.Size = new System.Drawing.Size(310, 26);
            this.tbDirectory.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 445);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Output Report Directory: ";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // AutoFill
            // 
            this.AutoFill.Location = new System.Drawing.Point(37, 152);
            this.AutoFill.Name = "AutoFill";
            this.AutoFill.Size = new System.Drawing.Size(359, 37);
            this.AutoFill.TabIndex = 9;
            this.AutoFill.Text = "Auto Student Name and Last Name";
            this.AutoFill.UseVisualStyleBackColor = true;
            this.AutoFill.Click += new System.EventHandler(this.AutoFill_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 515);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(344, 515);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 44);
            this.button2.TabIndex = 12;
            this.button2.Text = "Generate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Nice Commenting",
            "Self-explantory Code",
            "Missing file comment",
            "Missing function comment",
            "Missing Header comment",
            "Unnecessary Code",
            "Never reach code",
            "No Goto",
            "Type not assgined"});
            this.checkedListBox1.Location = new System.Drawing.Point(133, 217);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(228, 193);
            this.checkedListBox1.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Include: ";
            // 
            // GenerateReportForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 571);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AutoFill);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDirectory);
            this.Controls.Add(this.bttnSerach);
            this.Controls.Add(this.tbStudentID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.tbFirstName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GenerateReportForms";
            this.Text = "Generate Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStudentID;
        private System.Windows.Forms.Button bttnSerach;
        private System.Windows.Forms.TextBox tbDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AutoFill;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label6;
    }
}