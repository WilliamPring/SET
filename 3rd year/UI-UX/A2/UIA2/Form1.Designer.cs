namespace UIA2
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("File 1");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("Folder 1", new System.Windows.Forms.TreeNode[] {
            treeNode41});
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("File 2");
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("Folder 2", new System.Windows.Forms.TreeNode[] {
            treeNode43});
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("File 3");
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("Folder 3", new System.Windows.Forms.TreeNode[] {
            treeNode45});
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("File 4");
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("Folder 4", new System.Windows.Forms.TreeNode[] {
            treeNode47});
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("Projects", new System.Windows.Forms.TreeNode[] {
            treeNode42,
            treeNode44,
            treeNode46,
            treeNode48});
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("Soultions", new System.Windows.Forms.TreeNode[] {
            treeNode49});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView = new System.Windows.Forms.TreeView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bttnClear = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.bttnCompile = new System.Windows.Forms.Button();
            this.bttnRunTestCase = new System.Windows.Forms.Button();
            this.bttnCommentScanner = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLoad = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLoadCompile = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLoadRuns = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLoadCommentScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLoadIntergarty = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bttnLoadTest = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bttnGenReport = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 51);
            this.treeView.Name = "treeView";
            treeNode41.Name = "Node8";
            treeNode41.Text = "File 1";
            treeNode42.Name = "Node4";
            treeNode42.Text = "Folder 1";
            treeNode43.Name = "Node9";
            treeNode43.Text = "File 2";
            treeNode44.Name = "Node5";
            treeNode44.Text = "Folder 2";
            treeNode45.Name = "Node14";
            treeNode45.Text = "File 3";
            treeNode46.Name = "Node12";
            treeNode46.Text = "Folder 3";
            treeNode47.Name = "Node15";
            treeNode47.Text = "File 4";
            treeNode48.Name = "Node13";
            treeNode48.Text = "Folder 4";
            treeNode49.Name = "Node3";
            treeNode49.Text = "Projects";
            treeNode50.Name = "Node0";
            treeNode50.Text = "Soultions";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode50});
            this.treeView.Size = new System.Drawing.Size(211, 418);
            this.treeView.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(241, 51);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(659, 418);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(651, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(636, 360);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(651, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(636, 360);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(651, 385);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "File 3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(3, 3);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(636, 360);
            this.textBox3.TabIndex = 12;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(651, 385);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "File 4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(3, 3);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(636, 360);
            this.textBox4.TabIndex = 12;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(245, 509);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(429, 195);
            this.textBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 486);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Comment";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 710);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bttnClear
            // 
            this.bttnClear.Location = new System.Drawing.Point(330, 710);
            this.bttnClear.Name = "bttnClear";
            this.bttnClear.Size = new System.Drawing.Size(75, 38);
            this.bttnClear.TabIndex = 4;
            this.bttnClear.Text = "Clear";
            this.bttnClear.UseVisualStyleBackColor = true;
            this.bttnClear.Click += new System.EventHandler(this.bttnClear_Click);
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
            this.checkedListBox1.Location = new System.Drawing.Point(690, 509);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(220, 235);
            this.checkedListBox1.TabIndex = 5;
            // 
            // bttnCompile
            // 
            this.bttnCompile.Location = new System.Drawing.Point(12, 534);
            this.bttnCompile.Name = "bttnCompile";
            this.bttnCompile.Size = new System.Drawing.Size(227, 38);
            this.bttnCompile.TabIndex = 6;
            this.bttnCompile.Text = "Compile";
            this.bttnCompile.UseVisualStyleBackColor = true;
            this.bttnCompile.Click += new System.EventHandler(this.bttnCompile_Click);
            // 
            // bttnRunTestCase
            // 
            this.bttnRunTestCase.Location = new System.Drawing.Point(12, 578);
            this.bttnRunTestCase.Name = "bttnRunTestCase";
            this.bttnRunTestCase.Size = new System.Drawing.Size(227, 38);
            this.bttnRunTestCase.TabIndex = 7;
            this.bttnRunTestCase.Text = "Run test case";
            this.bttnRunTestCase.UseVisualStyleBackColor = true;
            this.bttnRunTestCase.Click += new System.EventHandler(this.bttnRunTestCase_Click);
            // 
            // bttnCommentScanner
            // 
            this.bttnCommentScanner.Location = new System.Drawing.Point(12, 622);
            this.bttnCommentScanner.Name = "bttnCommentScanner";
            this.bttnCommentScanner.Size = new System.Drawing.Size(227, 38);
            this.bttnCommentScanner.TabIndex = 8;
            this.bttnCommentScanner.Text = "Run Comment Scanner";
            this.bttnCommentScanner.UseVisualStyleBackColor = true;
            this.bttnCommentScanner.Click += new System.EventHandler(this.bttnCommentScanner_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 666);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(227, 38);
            this.button5.TabIndex = 9;
            this.button5.Text = "Integrity Check";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLoad,
            this.toolStripLoadCompile,
            this.toolStripLoadRuns,
            this.toolStripLoadCommentScore,
            this.toolStripLoadIntergarty});
            this.statusStrip1.Location = new System.Drawing.Point(0, 757);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(922, 30);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripLoad
            // 
            this.toolStripLoad.Name = "toolStripLoad";
            this.toolStripLoad.Size = new System.Drawing.Size(92, 25);
            this.toolStripLoad.Text = "Loads: No";
            // 
            // toolStripLoadCompile
            // 
            this.toolStripLoadCompile.Name = "toolStripLoadCompile";
            this.toolStripLoadCompile.Size = new System.Drawing.Size(119, 25);
            this.toolStripLoadCompile.Text = "Compiles: No";
            // 
            // toolStripLoadRuns
            // 
            this.toolStripLoadRuns.Name = "toolStripLoadRuns";
            this.toolStripLoadRuns.Size = new System.Drawing.Size(84, 25);
            this.toolStripLoadRuns.Text = "Runs: No";
            // 
            // toolStripLoadCommentScore
            // 
            this.toolStripLoadCommentScore.Name = "toolStripLoadCommentScore";
            this.toolStripLoadCommentScore.Size = new System.Drawing.Size(179, 25);
            this.toolStripLoadCommentScore.Text = "Comment Score: 0 %";
            // 
            // toolStripLoadIntergarty
            // 
            this.toolStripLoadIntergarty.Name = "toolStripLoadIntergarty";
            this.toolStripLoadIntergarty.Size = new System.Drawing.Size(303, 25);
            this.toolStripLoadIntergarty.Text = "Intergarty Check : Pass SET Standard ";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // bttnLoadTest
            // 
            this.bttnLoadTest.Location = new System.Drawing.Point(12, 490);
            this.bttnLoadTest.Name = "bttnLoadTest";
            this.bttnLoadTest.Size = new System.Drawing.Size(227, 38);
            this.bttnLoadTest.TabIndex = 11;
            this.bttnLoadTest.Text = "Load Project Test";
            this.bttnLoadTest.UseVisualStyleBackColor = true;
            this.bttnLoadTest.Click += new System.EventHandler(this.bttnLoadTest_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(922, 31);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.newToolStripButton.Text = "&New";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.saveToolStripButton.Text = "&Save";
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // bttnGenReport
            // 
            this.bttnGenReport.Location = new System.Drawing.Point(12, 710);
            this.bttnGenReport.Name = "bttnGenReport";
            this.bttnGenReport.Size = new System.Drawing.Size(227, 38);
            this.bttnGenReport.TabIndex = 13;
            this.bttnGenReport.Text = "Generate Report";
            this.bttnGenReport.UseVisualStyleBackColor = true;
            this.bttnGenReport.Click += new System.EventHandler(this.bttnGenReport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 787);
            this.Controls.Add(this.bttnGenReport);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.bttnLoadTest);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.bttnCommentScanner);
            this.Controls.Add(this.bttnRunTestCase);
            this.Controls.Add(this.bttnCompile);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.bttnClear);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bttnClear;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button bttnCompile;
        private System.Windows.Forms.Button bttnRunTestCase;
        private System.Windows.Forms.Button bttnCommentScanner;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoad;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoadCompile;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoadRuns;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoadCommentScore;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLoadIntergarty;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button bttnLoadTest;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.Button bttnGenReport;
    }
}

