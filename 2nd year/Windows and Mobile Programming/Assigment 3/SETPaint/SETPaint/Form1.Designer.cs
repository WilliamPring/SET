namespace SETPaint
{
    partial class FrmSetPaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetPaint));
            this.LineThickness = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.gbShapes = new System.Windows.Forms.GroupBox();
            this.rbEllipse = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.lineColourButton = new System.Windows.Forms.Button();
            this.ssMouseCoordinate = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fillColourButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileOption = new System.Windows.Forms.ToolStripMenuItem();
            this.openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.pnDrawScreen = new SETPaint.newPanel();
            ((System.ComponentModel.ISupportInitialize)(this.LineThickness)).BeginInit();
            this.gbShapes.SuspendLayout();
            this.ssMouseCoordinate.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LineThickness
            // 
            this.LineThickness.AccessibleDescription = "";
            this.LineThickness.AccessibleName = "";
            this.LineThickness.Location = new System.Drawing.Point(414, 29);
            this.LineThickness.Name = "LineThickness";
            this.LineThickness.Size = new System.Drawing.Size(96, 45);
            this.LineThickness.TabIndex = 1;
            this.LineThickness.Value = 5;
            this.LineThickness.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(427, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Line Thick";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // gbShapes
            // 
            this.gbShapes.Controls.Add(this.rbEllipse);
            this.gbShapes.Controls.Add(this.rbRectangle);
            this.gbShapes.Controls.Add(this.rbLine);
            this.gbShapes.Location = new System.Drawing.Point(417, 80);
            this.gbShapes.Name = "gbShapes";
            this.gbShapes.Size = new System.Drawing.Size(94, 104);
            this.gbShapes.TabIndex = 5;
            this.gbShapes.TabStop = false;
            this.gbShapes.Text = "Shapes";
            // 
            // rbEllipse
            // 
            this.rbEllipse.AutoSize = true;
            this.rbEllipse.Location = new System.Drawing.Point(6, 65);
            this.rbEllipse.Name = "rbEllipse";
            this.rbEllipse.Size = new System.Drawing.Size(55, 17);
            this.rbEllipse.TabIndex = 5;
            this.rbEllipse.TabStop = true;
            this.rbEllipse.Text = "Ellipse";
            this.rbEllipse.UseVisualStyleBackColor = true;
            this.rbEllipse.CheckedChanged += new System.EventHandler(this.rbEllipse_CheckedChanged);
            // 
            // rbRectangle
            // 
            this.rbRectangle.AutoSize = true;
            this.rbRectangle.Location = new System.Drawing.Point(6, 42);
            this.rbRectangle.Name = "rbRectangle";
            this.rbRectangle.Size = new System.Drawing.Size(74, 17);
            this.rbRectangle.TabIndex = 4;
            this.rbRectangle.TabStop = true;
            this.rbRectangle.Text = "Rectangle";
            this.rbRectangle.UseVisualStyleBackColor = true;
            this.rbRectangle.CheckedChanged += new System.EventHandler(this.rbRectangle_CheckedChanged);
            // 
            // rbLine
            // 
            this.rbLine.AutoSize = true;
            this.rbLine.Location = new System.Drawing.Point(6, 19);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(50, 17);
            this.rbLine.TabIndex = 3;
            this.rbLine.TabStop = true;
            this.rbLine.Text = "Lines";
            this.rbLine.UseVisualStyleBackColor = true;
            this.rbLine.CheckedChanged += new System.EventHandler(this.rbLine_CheckedChanged);
            // 
            // lineColourButton
            // 
            this.lineColourButton.Location = new System.Drawing.Point(416, 199);
            this.lineColourButton.Name = "lineColourButton";
            this.lineColourButton.Size = new System.Drawing.Size(94, 23);
            this.lineColourButton.TabIndex = 6;
            this.lineColourButton.Text = "Line Colour";
            this.lineColourButton.UseVisualStyleBackColor = true;
            this.lineColourButton.Click += new System.EventHandler(this.lineColourButton_Click);
            // 
            // ssMouseCoordinate
            // 
            this.ssMouseCoordinate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.ssMouseCoordinate.Location = new System.Drawing.Point(0, 368);
            this.ssMouseCoordinate.Name = "ssMouseCoordinate";
            this.ssMouseCoordinate.Size = new System.Drawing.Size(511, 22);
            this.ssMouseCoordinate.TabIndex = 7;
            this.ssMouseCoordinate.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click_1);
            // 
            // fillColourButton
            // 
            this.fillColourButton.Location = new System.Drawing.Point(416, 242);
            this.fillColourButton.Name = "fillColourButton";
            this.fillColourButton.Size = new System.Drawing.Size(94, 23);
            this.fillColourButton.TabIndex = 8;
            this.fillColourButton.Text = "Fill Colour";
            this.fillColourButton.UseVisualStyleBackColor = true;
            this.fillColourButton.Click += new System.EventHandler(this.fillColourButton_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOption,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(511, 24);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // fileOption
            // 
            this.fileOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFile,
            this.saveFile});
            this.fileOption.Name = "fileOption";
            this.fileOption.Size = new System.Drawing.Size(37, 20);
            this.fileOption.Text = "File";
            // 
            // openFile
            // 
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(114, 22);
            this.openFile.Text = "Open";
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // saveFile
            // 
            this.saveFile.Name = "saveFile";
            this.saveFile.Size = new System.Drawing.Size(114, 22);
            this.saveFile.Text = "Save As";
            this.saveFile.Click += new System.EventHandler(this.saveAsFile_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(415, 287);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.clearScreenButton);
            // 
            // pnDrawScreen
            // 
            this.pnDrawScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnDrawScreen.Location = new System.Drawing.Point(12, 27);
            this.pnDrawScreen.Name = "pnDrawScreen";
            this.pnDrawScreen.Size = new System.Drawing.Size(398, 319);
            this.pnDrawScreen.TabIndex = 4;
            this.pnDrawScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pnDrawScreen_Paint);
            this.pnDrawScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnDrawScreen_MouseDown);
            this.pnDrawScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnDrawScreen_MouseMove);
            this.pnDrawScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnDrawScreen_MouseUp);
            // 
            // FrmSetPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 390);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fillColourButton);
            this.Controls.Add(this.ssMouseCoordinate);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.lineColourButton);
            this.Controls.Add(this.gbShapes);
            this.Controls.Add(this.pnDrawScreen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LineThickness);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(16, 39);
            this.Name = "FrmSetPaint";
            this.Text = "SETPaint";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LineThickness)).EndInit();
            this.gbShapes.ResumeLayout(false);
            this.gbShapes.PerformLayout();
            this.ssMouseCoordinate.ResumeLayout(false);
            this.ssMouseCoordinate.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar LineThickness;
        private System.Windows.Forms.Label label1;
        private newPanel pnDrawScreen;
        private System.Windows.Forms.GroupBox gbShapes;
        private System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.RadioButton rbEllipse;
        private System.Windows.Forms.Button lineColourButton;
        private System.Windows.Forms.StatusStrip ssMouseCoordinate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button fillColourButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileOption;
        private System.Windows.Forms.ToolStripMenuItem openFile;
        private System.Windows.Forms.ToolStripMenuItem saveFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

