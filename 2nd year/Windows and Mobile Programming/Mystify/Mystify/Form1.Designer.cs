namespace Mystify
{
    partial class Main_Mystify
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
            this.pnScreen = new System.Windows.Forms.Panel();
            this.bnNewStick = new System.Windows.Forms.Button();
            this.bnPause = new System.Windows.Forms.Button();
            this.bnEnd = new System.Windows.Forms.Button();
            this.bnResume = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnScreen
            // 
            this.pnScreen.Location = new System.Drawing.Point(12, 12);
            this.pnScreen.Name = "pnScreen";
            this.pnScreen.Size = new System.Drawing.Size(328, 308);
            this.pnScreen.TabIndex = 0;
            this.pnScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pnScreen_Paint);
            // 
            // bnNewStick
            // 
            this.bnNewStick.Location = new System.Drawing.Point(346, 76);
            this.bnNewStick.Name = "bnNewStick";
            this.bnNewStick.Size = new System.Drawing.Size(75, 23);
            this.bnNewStick.TabIndex = 1;
            this.bnNewStick.Text = "New Stick";
            this.bnNewStick.UseVisualStyleBackColor = true;
            this.bnNewStick.Click += new System.EventHandler(this.bnNewStick_Click);
            // 
            // bnPause
            // 
            this.bnPause.Location = new System.Drawing.Point(346, 120);
            this.bnPause.Name = "bnPause";
            this.bnPause.Size = new System.Drawing.Size(75, 23);
            this.bnPause.TabIndex = 2;
            this.bnPause.Text = "Pause";
            this.bnPause.UseVisualStyleBackColor = true;
            this.bnPause.Click += new System.EventHandler(this.bnPause_Click);
            // 
            // bnEnd
            // 
            this.bnEnd.Location = new System.Drawing.Point(346, 217);
            this.bnEnd.Name = "bnEnd";
            this.bnEnd.Size = new System.Drawing.Size(75, 23);
            this.bnEnd.TabIndex = 3;
            this.bnEnd.Text = "End";
            this.bnEnd.UseVisualStyleBackColor = true;
            // 
            // bnResume
            // 
            this.bnResume.Location = new System.Drawing.Point(346, 169);
            this.bnResume.Name = "bnResume";
            this.bnResume.Size = new System.Drawing.Size(75, 23);
            this.bnResume.TabIndex = 4;
            this.bnResume.Text = "Resume";
            this.bnResume.UseVisualStyleBackColor = true;
            this.bnResume.Click += new System.EventHandler(this.bnResume_Click);
            // 
            // Main_Mystify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 332);
            this.Controls.Add(this.bnResume);
            this.Controls.Add(this.bnEnd);
            this.Controls.Add(this.bnPause);
            this.Controls.Add(this.bnNewStick);
            this.Controls.Add(this.pnScreen);
            this.Name = "Main_Mystify";
            this.Text = "Mystify";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnScreen;
        private System.Windows.Forms.Button bnNewStick;
        private System.Windows.Forms.Button bnPause;
        private System.Windows.Forms.Button bnEnd;
        private System.Windows.Forms.Button bnResume;
    }
}

