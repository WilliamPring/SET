﻿namespace IAD
{
    partial class GameUI
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
            this.displayPnl = new System.Windows.Forms.Panel();
            this.bttnSendRandMove = new System.Windows.Forms.Button();
            this.BttnRandomize = new System.Windows.Forms.CheckBox();
            this.colorDialogFilling = new System.Windows.Forms.ColorDialog();
            this.bttnFill = new System.Windows.Forms.Button();
            this.bttnBorder = new System.Windows.Forms.Button();
            this.bttnLeft = new System.Windows.Forms.Button();
            this.bttnRight = new System.Windows.Forms.Button();
            this.bttnUp = new System.Windows.Forms.Button();
            this.bttnDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // displayPnl
            // 
            this.displayPnl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.displayPnl.Location = new System.Drawing.Point(12, 12);
            this.displayPnl.Name = "displayPnl";
            this.displayPnl.Size = new System.Drawing.Size(656, 376);
            this.displayPnl.TabIndex = 4;
            this.displayPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPnl_Paint);
            // 
            // bttnSendRandMove
            // 
            this.bttnSendRandMove.Location = new System.Drawing.Point(513, 464);
            this.bttnSendRandMove.Name = "bttnSendRandMove";
            this.bttnSendRandMove.Size = new System.Drawing.Size(121, 23);
            this.bttnSendRandMove.TabIndex = 5;
            this.bttnSendRandMove.Text = "Send Random Move";
            this.bttnSendRandMove.UseVisualStyleBackColor = true;
            this.bttnSendRandMove.Click += new System.EventHandler(this.bttnSendRandMove_Click);
            // 
            // BttnRandomize
            // 
            this.BttnRandomize.AutoSize = true;
            this.BttnRandomize.Location = new System.Drawing.Point(513, 422);
            this.BttnRandomize.Name = "BttnRandomize";
            this.BttnRandomize.Size = new System.Drawing.Size(79, 17);
            this.BttnRandomize.TabIndex = 6;
            this.BttnRandomize.Text = "Randomize";
            this.BttnRandomize.UseVisualStyleBackColor = true;
            // 
            // bttnFill
            // 
            this.bttnFill.Location = new System.Drawing.Point(12, 405);
            this.bttnFill.Name = "bttnFill";
            this.bttnFill.Size = new System.Drawing.Size(143, 48);
            this.bttnFill.TabIndex = 7;
            this.bttnFill.Text = "Fill";
            this.bttnFill.UseVisualStyleBackColor = true;
            this.bttnFill.Click += new System.EventHandler(this.bttnFill_Click);
            // 
            // bttnBorder
            // 
            this.bttnBorder.Location = new System.Drawing.Point(12, 464);
            this.bttnBorder.Name = "bttnBorder";
            this.bttnBorder.Size = new System.Drawing.Size(143, 48);
            this.bttnBorder.TabIndex = 8;
            this.bttnBorder.Text = "Border";
            this.bttnBorder.UseVisualStyleBackColor = true;
            this.bttnBorder.Click += new System.EventHandler(this.bttnBorder_Click);
            // 
            // bttnLeft
            // 
            this.bttnLeft.Location = new System.Drawing.Point(222, 447);
            this.bttnLeft.Name = "bttnLeft";
            this.bttnLeft.Size = new System.Drawing.Size(75, 23);
            this.bttnLeft.TabIndex = 0;
            this.bttnLeft.Text = "LEFT";
            this.bttnLeft.UseVisualStyleBackColor = true;
            this.bttnLeft.Click += new System.EventHandler(this.bttnLeft_Click);
            // 
            // bttnRight
            // 
            this.bttnRight.Location = new System.Drawing.Point(338, 447);
            this.bttnRight.Name = "bttnRight";
            this.bttnRight.Size = new System.Drawing.Size(75, 23);
            this.bttnRight.TabIndex = 1;
            this.bttnRight.Text = "RIGHT";
            this.bttnRight.UseVisualStyleBackColor = true;
            this.bttnRight.Click += new System.EventHandler(this.bttnRight_Click);
            // 
            // bttnUp
            // 
            this.bttnUp.Location = new System.Drawing.Point(285, 418);
            this.bttnUp.Name = "bttnUp";
            this.bttnUp.Size = new System.Drawing.Size(75, 23);
            this.bttnUp.TabIndex = 2;
            this.bttnUp.Text = "UP";
            this.bttnUp.UseVisualStyleBackColor = true;
            this.bttnUp.Click += new System.EventHandler(this.bttnUp_Click);
            // 
            // bttnDown
            // 
            this.bttnDown.Location = new System.Drawing.Point(285, 476);
            this.bttnDown.Name = "bttnDown";
            this.bttnDown.Size = new System.Drawing.Size(75, 23);
            this.bttnDown.TabIndex = 3;
            this.bttnDown.Text = "DOWN";
            this.bttnDown.UseVisualStyleBackColor = true;
            this.bttnDown.Click += new System.EventHandler(this.bttnDown_Click);
            // 
            // GameUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 521);
            this.Controls.Add(this.bttnBorder);
            this.Controls.Add(this.bttnFill);
            this.Controls.Add(this.BttnRandomize);
            this.Controls.Add(this.bttnSendRandMove);
            this.Controls.Add(this.displayPnl);
            this.Controls.Add(this.bttnDown);
            this.Controls.Add(this.bttnUp);
            this.Controls.Add(this.bttnRight);
            this.Controls.Add(this.bttnLeft);
            this.Name = "GameUI";
            this.Text = "Moving";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameUI_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel displayPnl;
        private System.Windows.Forms.Button bttnSendRandMove;
        private System.Windows.Forms.CheckBox BttnRandomize;
        private System.Windows.Forms.ColorDialog colorDialogFilling;
        private System.Windows.Forms.Button bttnFill;
        private System.Windows.Forms.Button bttnBorder;
        private System.Windows.Forms.Button bttnDown;
        private System.Windows.Forms.Button bttnUp;
        private System.Windows.Forms.Button bttnRight;
        private System.Windows.Forms.Button bttnLeft;
    }
}

