namespace D2Launcher.Resources.Widgets
{
    partial class ModsListWidget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModsListWidget));
            this.ModsListComboBox = new System.Windows.Forms.ComboBox();
            this.InfoButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.DownloadButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.ProcessInfoLabel = new System.Windows.Forms.Label();
            this.UpdateModListButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.SuspendLayout();
            // 
            // ModsListComboBox
            // 
            this.ModsListComboBox.BackColor = System.Drawing.Color.Gray;
            this.ModsListComboBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ModsListComboBox.FormattingEnabled = true;
            this.ModsListComboBox.Location = new System.Drawing.Point(3, 3);
            this.ModsListComboBox.Name = "ModsListComboBox";
            this.ModsListComboBox.Size = new System.Drawing.Size(241, 29);
            this.ModsListComboBox.TabIndex = 0;
            // 
            // InfoButton
            // 
            this.InfoButton.FlatAppearance.BorderSize = 0;
            this.InfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InfoButton.ForeColor = System.Drawing.Color.Black;
            this.InfoButton.Image = ((System.Drawing.Image)(resources.GetObject("InfoButton.Image")));
            this.InfoButton.Location = new System.Drawing.Point(3, 404);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(134, 46);
            this.InfoButton.TabIndex = 1;
            this.InfoButton.Text = "Info";
            this.InfoButton.UseVisualStyleBackColor = true;
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.FlatAppearance.BorderSize = 0;
            this.DownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DownloadButton.ForeColor = System.Drawing.Color.Black;
            this.DownloadButton.Image = ((System.Drawing.Image)(resources.GetObject("DownloadButton.Image")));
            this.DownloadButton.Location = new System.Drawing.Point(287, 404);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(134, 46);
            this.DownloadButton.TabIndex = 2;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ProcessInfoLabel
            // 
            this.ProcessInfoLabel.AutoSize = true;
            this.ProcessInfoLabel.BackColor = System.Drawing.Color.Gray;
            this.ProcessInfoLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ProcessInfoLabel.Location = new System.Drawing.Point(143, 429);
            this.ProcessInfoLabel.Name = "ProcessInfoLabel";
            this.ProcessInfoLabel.Size = new System.Drawing.Size(0, 21);
            this.ProcessInfoLabel.TabIndex = 3;
            // 
            // UpdateModListButton
            // 
            this.UpdateModListButton.FlatAppearance.BorderSize = 0;
            this.UpdateModListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateModListButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UpdateModListButton.ForeColor = System.Drawing.Color.Black;
            this.UpdateModListButton.Image = ((System.Drawing.Image)(resources.GetObject("UpdateModListButton.Image")));
            this.UpdateModListButton.Location = new System.Drawing.Point(287, 3);
            this.UpdateModListButton.Name = "UpdateModListButton";
            this.UpdateModListButton.Size = new System.Drawing.Size(134, 46);
            this.UpdateModListButton.TabIndex = 4;
            this.UpdateModListButton.Text = "Update list";
            this.UpdateModListButton.UseVisualStyleBackColor = true;
            this.UpdateModListButton.Click += new System.EventHandler(this.UpdateModListButton_Click);
            // 
            // ModsListWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::D2Launcher.Properties.Resources.ModsListBackGroundImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.UpdateModListButton);
            this.Controls.Add(this.ProcessInfoLabel);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.InfoButton);
            this.Controls.Add(this.ModsListComboBox);
            this.Name = "ModsListWidget";
            this.Load += new System.EventHandler(this.ModsListWidget_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox ModsListComboBox;
        private Buttons.D2MainMenuButton InfoButton;
        private Buttons.D2MainMenuButton DownloadButton;
        private Label ProcessInfoLabel;
        private Buttons.D2MainMenuButton UpdateModListButton;
    }
}
