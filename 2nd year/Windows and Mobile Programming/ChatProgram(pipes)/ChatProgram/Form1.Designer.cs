namespace ChatProgram
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
            this.ChatScreen = new System.Windows.Forms.RichTextBox();
            this.TextScreen = new System.Windows.Forms.RichTextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalComputer = new System.Windows.Forms.TextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Send = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.HostButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChatScreen
            // 
            this.ChatScreen.Location = new System.Drawing.Point(12, 16);
            this.ChatScreen.Name = "ChatScreen";
            this.ChatScreen.Size = new System.Drawing.Size(630, 318);
            this.ChatScreen.TabIndex = 0;
            this.ChatScreen.Text = "";
            // 
            // TextScreen
            // 
            this.TextScreen.Location = new System.Drawing.Point(13, 353);
            this.TextScreen.Name = "TextScreen";
            this.TextScreen.Size = new System.Drawing.Size(630, 96);
            this.TextScreen.TabIndex = 1;
            this.TextScreen.Text = "";
            this.TextScreen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextScreen_KeyDown);
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(664, 130);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(100, 20);
            this.UserName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(661, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(662, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pipe Name";
            // 
            // LocalComputer
            // 
            this.LocalComputer.Location = new System.Drawing.Point(665, 180);
            this.LocalComputer.Name = "LocalComputer";
            this.LocalComputer.Size = new System.Drawing.Size(100, 20);
            this.LocalComputer.TabIndex = 5;
            this.LocalComputer.TextChanged += new System.EventHandler(this.LocalComputer_TextChanged);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(664, 232);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(100, 38);
            this.Connect.TabIndex = 6;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click_1);
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(649, 353);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(128, 96);
            this.Send.TabIndex = 7;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(664, 293);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(100, 38);
            this.Disconnect.TabIndex = 8;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(661, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Pipe Server Name";
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(664, 32);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(100, 20);
            this.ServerName.TabIndex = 10;
            // 
            // HostButton
            // 
            this.HostButton.Location = new System.Drawing.Point(664, 64);
            this.HostButton.Name = "HostButton";
            this.HostButton.Size = new System.Drawing.Size(100, 38);
            this.HostButton.TabIndex = 11;
            this.HostButton.Text = "Host";
            this.HostButton.UseVisualStyleBackColor = true;
            this.HostButton.Click += new System.EventHandler(this.Host);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 458);
            this.Controls.Add(this.HostButton);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.LocalComputer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.TextScreen);
            this.Controls.Add(this.ChatScreen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChatScreen;
        private System.Windows.Forms.RichTextBox TextScreen;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LocalComputer;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.Button HostButton;
    }
}

