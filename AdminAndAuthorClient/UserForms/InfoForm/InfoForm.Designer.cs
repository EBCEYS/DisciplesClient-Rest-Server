namespace AdminAndAuthorClient.UserForms.InfoForm
{
    partial class InfoForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UpdateInfoTextBox = new System.Windows.Forms.TextBox();
            this.UpdateInfoButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CurrentInfoTextBox = new System.Windows.Forms.TextBox();
            this.GetInfoButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UpdateInfoButton);
            this.groupBox1.Controls.Add(this.UpdateInfoTextBox);
            this.groupBox1.Location = new System.Drawing.Point(295, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 333);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Info:";
            // 
            // UpdateInfoTextBox
            // 
            this.UpdateInfoTextBox.AcceptsReturn = true;
            this.UpdateInfoTextBox.AcceptsTab = true;
            this.UpdateInfoTextBox.Location = new System.Drawing.Point(6, 22);
            this.UpdateInfoTextBox.Multiline = true;
            this.UpdateInfoTextBox.Name = "UpdateInfoTextBox";
            this.UpdateInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UpdateInfoTextBox.Size = new System.Drawing.Size(273, 276);
            this.UpdateInfoTextBox.TabIndex = 0;
            // 
            // UpdateInfoButton
            // 
            this.UpdateInfoButton.Location = new System.Drawing.Point(6, 304);
            this.UpdateInfoButton.Name = "UpdateInfoButton";
            this.UpdateInfoButton.Size = new System.Drawing.Size(96, 23);
            this.UpdateInfoButton.TabIndex = 1;
            this.UpdateInfoButton.Text = "Update info";
            this.UpdateInfoButton.UseVisualStyleBackColor = true;
            this.UpdateInfoButton.Click += new System.EventHandler(this.UpdateInfoButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GetInfoButton);
            this.groupBox2.Controls.Add(this.CurrentInfoTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 333);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current info:";
            // 
            // CurrentInfoTextBox
            // 
            this.CurrentInfoTextBox.AcceptsReturn = true;
            this.CurrentInfoTextBox.AcceptsTab = true;
            this.CurrentInfoTextBox.Location = new System.Drawing.Point(0, 22);
            this.CurrentInfoTextBox.Multiline = true;
            this.CurrentInfoTextBox.Name = "CurrentInfoTextBox";
            this.CurrentInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CurrentInfoTextBox.Size = new System.Drawing.Size(273, 276);
            this.CurrentInfoTextBox.TabIndex = 2;
            // 
            // GetInfoButton
            // 
            this.GetInfoButton.Location = new System.Drawing.Point(6, 304);
            this.GetInfoButton.Name = "GetInfoButton";
            this.GetInfoButton.Size = new System.Drawing.Size(96, 23);
            this.GetInfoButton.TabIndex = 2;
            this.GetInfoButton.Text = "Get info";
            this.GetInfoButton.UseVisualStyleBackColor = true;
            this.GetInfoButton.Click += new System.EventHandler(this.GetInfoButton_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 352);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InfoForm";
            this.Text = "InfoForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private TextBox UpdateInfoTextBox;
        private Button UpdateInfoButton;
        private GroupBox groupBox2;
        private TextBox CurrentInfoTextBox;
        private Button GetInfoButton;
    }
}