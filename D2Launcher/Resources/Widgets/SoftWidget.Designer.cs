namespace D2Launcher.Resources.Widgets
{
    partial class SoftWidget
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftWidget));
            this.AllowedSoftComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcessInfoLabel = new System.Windows.Forms.Label();
            this.DownloadSoftButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.OpenSoftDirButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.InstalledSoftsComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RemoveSoftButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.SuspendLayout();
            // 
            // AllowedSoftComboBox
            // 
            this.AllowedSoftComboBox.FormattingEnabled = true;
            this.AllowedSoftComboBox.Location = new System.Drawing.Point(0, 22);
            this.AllowedSoftComboBox.Name = "AllowedSoftComboBox";
            this.AllowedSoftComboBox.Size = new System.Drawing.Size(424, 23);
            this.AllowedSoftComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Allowed soft:";
            // 
            // ProcessInfoLabel
            // 
            this.ProcessInfoLabel.AutoSize = true;
            this.ProcessInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProcessInfoLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ProcessInfoLabel.ForeColor = System.Drawing.Color.LightGray;
            this.ProcessInfoLabel.Location = new System.Drawing.Point(158, 0);
            this.ProcessInfoLabel.Name = "ProcessInfoLabel";
            this.ProcessInfoLabel.Size = new System.Drawing.Size(0, 19);
            this.ProcessInfoLabel.TabIndex = 2;
            // 
            // DownloadSoftButton
            // 
            this.DownloadSoftButton.FlatAppearance.BorderSize = 0;
            this.DownloadSoftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadSoftButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DownloadSoftButton.ForeColor = System.Drawing.Color.Black;
            this.DownloadSoftButton.Image = ((System.Drawing.Image)(resources.GetObject("DownloadSoftButton.Image")));
            this.DownloadSoftButton.Location = new System.Drawing.Point(0, 407);
            this.DownloadSoftButton.Name = "DownloadSoftButton";
            this.DownloadSoftButton.Size = new System.Drawing.Size(134, 46);
            this.DownloadSoftButton.TabIndex = 3;
            this.DownloadSoftButton.Text = "Download";
            this.DownloadSoftButton.UseVisualStyleBackColor = true;
            this.DownloadSoftButton.Click += new System.EventHandler(this.DownloadSoftButton_Click);
            // 
            // OpenSoftDirButton
            // 
            this.OpenSoftDirButton.FlatAppearance.BorderSize = 0;
            this.OpenSoftDirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenSoftDirButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenSoftDirButton.ForeColor = System.Drawing.Color.Black;
            this.OpenSoftDirButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenSoftDirButton.Image")));
            this.OpenSoftDirButton.Location = new System.Drawing.Point(290, 407);
            this.OpenSoftDirButton.Name = "OpenSoftDirButton";
            this.OpenSoftDirButton.Size = new System.Drawing.Size(134, 46);
            this.OpenSoftDirButton.TabIndex = 4;
            this.OpenSoftDirButton.Text = "Open directory";
            this.OpenSoftDirButton.UseVisualStyleBackColor = true;
            this.OpenSoftDirButton.Click += new System.EventHandler(this.OpenSoftDirButton_Click);
            // 
            // InstalledSoftsComboBox
            // 
            this.InstalledSoftsComboBox.FormattingEnabled = true;
            this.InstalledSoftsComboBox.Location = new System.Drawing.Point(0, 184);
            this.InstalledSoftsComboBox.Name = "InstalledSoftsComboBox";
            this.InstalledSoftsComboBox.Size = new System.Drawing.Size(424, 23);
            this.InstalledSoftsComboBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(0, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Installed soft:";
            // 
            // RemoveSoftButton
            // 
            this.RemoveSoftButton.FlatAppearance.BorderSize = 0;
            this.RemoveSoftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveSoftButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemoveSoftButton.ForeColor = System.Drawing.Color.Black;
            this.RemoveSoftButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveSoftButton.Image")));
            this.RemoveSoftButton.Location = new System.Drawing.Point(140, 407);
            this.RemoveSoftButton.Name = "RemoveSoftButton";
            this.RemoveSoftButton.Size = new System.Drawing.Size(144, 46);
            this.RemoveSoftButton.TabIndex = 7;
            this.RemoveSoftButton.Text = "Remove";
            this.RemoveSoftButton.UseVisualStyleBackColor = true;
            this.RemoveSoftButton.Click += new System.EventHandler(this.RemoveSoftButton_Click);
            // 
            // SoftWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::D2Launcher.Properties.Resources.SoftWidgetBackGroundImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.RemoveSoftButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InstalledSoftsComboBox);
            this.Controls.Add(this.OpenSoftDirButton);
            this.Controls.Add(this.DownloadSoftButton);
            this.Controls.Add(this.ProcessInfoLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AllowedSoftComboBox);
            this.Name = "SoftWidget";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox AllowedSoftComboBox;
        private Label label1;
        private Label ProcessInfoLabel;
        private Buttons.D2MainMenuButton DownloadSoftButton;
        private Buttons.D2MainMenuButton OpenSoftDirButton;
        private ComboBox InstalledSoftsComboBox;
        private Label label2;
        private Buttons.D2MainMenuButton RemoveSoftButton;
    }
}
