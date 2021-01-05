namespace FSArduino
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ErrorBox = new System.Windows.Forms.ListBox();
            this.APMaster = new System.Windows.Forms.Button();
            this.NavButton = new System.Windows.Forms.Button();
            this.VNavButton = new System.Windows.Forms.Button();
            this.LocButton = new System.Windows.Forms.Button();
            this.AprButton = new System.Windows.Forms.Button();
            this.FlcButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(12, 34);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ErrorBox
            // 
            this.ErrorBox.FormattingEnabled = true;
            this.ErrorBox.Location = new System.Drawing.Point(12, 150);
            this.ErrorBox.Name = "ErrorBox";
            this.ErrorBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ErrorBox.Size = new System.Drawing.Size(322, 108);
            this.ErrorBox.TabIndex = 1;
            // 
            // APMaster
            // 
            this.APMaster.Location = new System.Drawing.Point(12, 63);
            this.APMaster.Name = "APMaster";
            this.APMaster.Size = new System.Drawing.Size(75, 23);
            this.APMaster.TabIndex = 2;
            this.APMaster.Text = "AP";
            this.APMaster.UseVisualStyleBackColor = true;
            this.APMaster.Click += new System.EventHandler(this.APMaster_Click);
            // 
            // NavButton
            // 
            this.NavButton.Location = new System.Drawing.Point(12, 92);
            this.NavButton.Name = "NavButton";
            this.NavButton.Size = new System.Drawing.Size(75, 23);
            this.NavButton.TabIndex = 3;
            this.NavButton.Text = "NAV";
            this.NavButton.UseVisualStyleBackColor = true;
            this.NavButton.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // VNavButton
            // 
            this.VNavButton.Location = new System.Drawing.Point(12, 121);
            this.VNavButton.Name = "VNavButton";
            this.VNavButton.Size = new System.Drawing.Size(75, 23);
            this.VNavButton.TabIndex = 4;
            this.VNavButton.Text = "VNAV";
            this.VNavButton.UseVisualStyleBackColor = true;
            // 
            // LocButton
            // 
            this.LocButton.Location = new System.Drawing.Point(93, 63);
            this.LocButton.Name = "LocButton";
            this.LocButton.Size = new System.Drawing.Size(75, 23);
            this.LocButton.TabIndex = 5;
            this.LocButton.Text = "LOC";
            this.LocButton.UseVisualStyleBackColor = true;
            // 
            // AprButton
            // 
            this.AprButton.Location = new System.Drawing.Point(93, 92);
            this.AprButton.Name = "AprButton";
            this.AprButton.Size = new System.Drawing.Size(75, 23);
            this.AprButton.TabIndex = 6;
            this.AprButton.Text = "APP";
            this.AprButton.UseVisualStyleBackColor = true;
            // 
            // FlcButton
            // 
            this.FlcButton.Location = new System.Drawing.Point(93, 121);
            this.FlcButton.Name = "FlcButton";
            this.FlcButton.Size = new System.Drawing.Size(75, 23);
            this.FlcButton.TabIndex = 7;
            this.FlcButton.Text = "FLC";
            this.FlcButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 275);
            this.Controls.Add(this.FlcButton);
            this.Controls.Add(this.AprButton);
            this.Controls.Add(this.LocButton);
            this.Controls.Add(this.VNavButton);
            this.Controls.Add(this.NavButton);
            this.Controls.Add(this.APMaster);
            this.Controls.Add(this.ErrorBox);
            this.Controls.Add(this.ConnectButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ListBox ErrorBox;
        private System.Windows.Forms.Button APMaster;
        private System.Windows.Forms.Button NavButton;
        private System.Windows.Forms.Button VNavButton;
        private System.Windows.Forms.Button LocButton;
        private System.Windows.Forms.Button AprButton;
        private System.Windows.Forms.Button FlcButton;
    }
}

