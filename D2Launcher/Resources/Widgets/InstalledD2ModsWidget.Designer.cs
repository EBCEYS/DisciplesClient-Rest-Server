namespace D2Launcher.Resources.Widgets
{
    partial class InstalledD2ModsWidget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstalledD2ModsWidget));
            this.InstalledModsComboBox = new System.Windows.Forms.ComboBox();
            this.RunButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.RemoveButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.CheckUpdatesButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.SuspendLayout();
            // 
            // InstalledModsComboBox
            // 
            this.InstalledModsComboBox.BackColor = System.Drawing.Color.Silver;
            this.InstalledModsComboBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InstalledModsComboBox.FormattingEnabled = true;
            this.InstalledModsComboBox.Location = new System.Drawing.Point(3, 3);
            this.InstalledModsComboBox.Name = "InstalledModsComboBox";
            this.InstalledModsComboBox.Size = new System.Drawing.Size(418, 29);
            this.InstalledModsComboBox.TabIndex = 0;
            // 
            // RunButton
            // 
            this.RunButton.FlatAppearance.BorderSize = 0;
            this.RunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RunButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RunButton.ForeColor = System.Drawing.Color.Black;
            this.RunButton.Image = ((System.Drawing.Image)(resources.GetObject("RunButton.Image")));
            this.RunButton.Location = new System.Drawing.Point(3, 404);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(134, 46);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.FlatAppearance.BorderSize = 0;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemoveButton.ForeColor = System.Drawing.Color.Black;
            this.RemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveButton.Image")));
            this.RemoveButton.Location = new System.Drawing.Point(287, 404);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(134, 46);
            this.RemoveButton.TabIndex = 2;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // CheckUpdatesButton
            // 
            this.CheckUpdatesButton.FlatAppearance.BorderSize = 0;
            this.CheckUpdatesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckUpdatesButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckUpdatesButton.ForeColor = System.Drawing.Color.Black;
            this.CheckUpdatesButton.Image = ((System.Drawing.Image)(resources.GetObject("CheckUpdatesButton.Image")));
            this.CheckUpdatesButton.Location = new System.Drawing.Point(147, 404);
            this.CheckUpdatesButton.Name = "CheckUpdatesButton";
            this.CheckUpdatesButton.Size = new System.Drawing.Size(134, 46);
            this.CheckUpdatesButton.TabIndex = 3;
            this.CheckUpdatesButton.Text = "Check updates";
            this.CheckUpdatesButton.UseVisualStyleBackColor = true;
            this.CheckUpdatesButton.Click += new System.EventHandler(this.CheckUpdatesButton_Click);
            // 
            // InstalledD2ModsWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::D2Launcher.Properties.Resources.InstalledModsBackGroundImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.CheckUpdatesButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.InstalledModsComboBox);
            this.Name = "InstalledD2ModsWidget";
            this.Load += new System.EventHandler(this.InstalledD2ModsWidget_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox InstalledModsComboBox;
        private Buttons.D2MainMenuButton RunButton;
        private Buttons.D2MainMenuButton RemoveButton;
        private Buttons.D2MainMenuButton CheckUpdatesButton;
    }
}
