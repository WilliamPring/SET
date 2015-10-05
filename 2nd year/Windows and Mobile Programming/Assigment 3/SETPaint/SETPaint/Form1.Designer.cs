namespace SETPaint
{
    partial class SETPaint
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
            this.LineThickness = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pnDrawScreen = new System.Windows.Forms.Panel();
            this.gbShapes = new System.Windows.Forms.GroupBox();
            this.rbEllipse = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.bnLineColour = new System.Windows.Forms.Button();
            this.ssMouseCoordinate = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btFillColour = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LineThickness)).BeginInit();
            this.gbShapes.SuspendLayout();
            this.ssMouseCoordinate.SuspendLayout();
            this.SuspendLayout();
            // 
            // LineThickness
            // 
            this.LineThickness.AccessibleDescription = "";
            this.LineThickness.AccessibleName = "";
            this.LineThickness.Location = new System.Drawing.Point(410, 29);
            this.LineThickness.Name = "LineThickness";
            this.LineThickness.Size = new System.Drawing.Size(96, 45);
            this.LineThickness.TabIndex = 1;
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
            // pnDrawScreen
            // 
            this.pnDrawScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnDrawScreen.Location = new System.Drawing.Point(12, 12);
            this.pnDrawScreen.Name = "pnDrawScreen";
            this.pnDrawScreen.Size = new System.Drawing.Size(392, 326);
            this.pnDrawScreen.TabIndex = 4;
            this.pnDrawScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pnDrawScreen_Paint);
            this.pnDrawScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnDrawScreen_MouseDown);
            this.pnDrawScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnDrawScreen_MouseMove);
            this.pnDrawScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnDrawScreen_MouseUp);
            // 
            // gbShapes
            // 
            this.gbShapes.Controls.Add(this.rbEllipse);
            this.gbShapes.Controls.Add(this.rbRectangle);
            this.gbShapes.Controls.Add(this.rbLine);
            this.gbShapes.Location = new System.Drawing.Point(410, 80);
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
            // 
            // bnLineColour
            // 
            this.bnLineColour.Location = new System.Drawing.Point(410, 199);
            this.bnLineColour.Name = "bnLineColour";
            this.bnLineColour.Size = new System.Drawing.Size(94, 23);
            this.bnLineColour.TabIndex = 6;
            this.bnLineColour.Text = "Line Colour";
            this.bnLineColour.UseVisualStyleBackColor = true;
            this.bnLineColour.Click += new System.EventHandler(this.bnLineColour_Click);
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
            // btFillColour
            // 
            this.btFillColour.Location = new System.Drawing.Point(410, 243);
            this.btFillColour.Name = "btFillColour";
            this.btFillColour.Size = new System.Drawing.Size(94, 23);
            this.btFillColour.TabIndex = 8;
            this.btFillColour.Text = "Fill Colour";
            this.btFillColour.UseVisualStyleBackColor = true;
            this.btFillColour.Click += new System.EventHandler(this.btFillColour_Click);
            // 
            // SETPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 390);
            this.Controls.Add(this.btFillColour);
            this.Controls.Add(this.ssMouseCoordinate);
            this.Controls.Add(this.bnLineColour);
            this.Controls.Add(this.gbShapes);
            this.Controls.Add(this.pnDrawScreen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LineThickness);
            this.MinimumSize = new System.Drawing.Size(16, 39);
            this.Name = "SETPaint";
            this.Text = "SETPaint";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LineThickness)).EndInit();
            this.gbShapes.ResumeLayout(false);
            this.gbShapes.PerformLayout();
            this.ssMouseCoordinate.ResumeLayout(false);
            this.ssMouseCoordinate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar LineThickness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnDrawScreen;
        private System.Windows.Forms.GroupBox gbShapes;
        private System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.RadioButton rbEllipse;
        private System.Windows.Forms.Button bnLineColour;
        private System.Windows.Forms.StatusStrip ssMouseCoordinate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btFillColour;
    }
}

