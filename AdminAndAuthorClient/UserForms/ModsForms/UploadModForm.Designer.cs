namespace AdminAndAuthorClient.UserForms.ModsForms
{
    partial class UploadModForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UploadButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SelectArchiveButton = new System.Windows.Forms.Button();
            this.ModNameTextBox = new System.Windows.Forms.TextBox();
            this.ModVersionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Archive path:";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ModVersionTextBox);
            this.groupBox1.Controls.Add(this.ModNameTextBox);
            this.groupBox1.Controls.Add(this.UploadButton);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.SelectArchiveButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 265);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // UploadButton
            // 
            this.UploadButton.AutoSize = true;
            this.UploadButton.Location = new System.Drawing.Point(215, 218);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(75, 25);
            this.UploadButton.TabIndex = 4;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(89, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(201, 89);
            this.textBox1.TabIndex = 3;
            // 
            // SelectArchiveButton
            // 
            this.SelectArchiveButton.AutoSize = true;
            this.SelectArchiveButton.Location = new System.Drawing.Point(6, 218);
            this.SelectArchiveButton.Name = "SelectArchiveButton";
            this.SelectArchiveButton.Size = new System.Drawing.Size(102, 25);
            this.SelectArchiveButton.TabIndex = 2;
            this.SelectArchiveButton.Text = "Select archive";
            this.SelectArchiveButton.UseVisualStyleBackColor = true;
            this.SelectArchiveButton.Click += new System.EventHandler(this.SelectArchiveButton_Click);
            // 
            // ModNameTextBox
            // 
            this.ModNameTextBox.Location = new System.Drawing.Point(89, 111);
            this.ModNameTextBox.Multiline = true;
            this.ModNameTextBox.Name = "ModNameTextBox";
            this.ModNameTextBox.Size = new System.Drawing.Size(201, 56);
            this.ModNameTextBox.TabIndex = 5;
            // 
            // ModVersionTextBox
            // 
            this.ModVersionTextBox.Location = new System.Drawing.Point(89, 173);
            this.ModVersionTextBox.Name = "ModVersionTextBox";
            this.ModVersionTextBox.Size = new System.Drawing.Size(201, 23);
            this.ModVersionTextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mod name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mod version:";
            // 
            // UploadModForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 282);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UploadModForm";
            this.Text = "UploadModForm";
            this.Load += new System.EventHandler(this.UploadModForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Button SelectArchiveButton;
        private Button UploadButton;
        private Label label3;
        private Label label2;
        private TextBox ModVersionTextBox;
        private TextBox ModNameTextBox;
    }
}