namespace D2Launcher.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.d2MainMenuButton1 = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.ModsListWidget = new D2Launcher.Resources.Widgets.ModsListWidget();
            this.InstalledModsButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.InstalledModsWidget = new D2Launcher.Resources.Widgets.InstalledD2ModsWidget();
            this.SoftWidget = new D2Launcher.Resources.Widgets.SoftWidget();
            this.SoftwareButton = new D2Launcher.Resources.Buttons.D2MainMenuButton();
            this.SuspendLayout();
            // 
            // d2MainMenuButton1
            // 
            this.d2MainMenuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.d2MainMenuButton1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.d2MainMenuButton1.ForeColor = System.Drawing.Color.Black;
            this.d2MainMenuButton1.Image = ((System.Drawing.Image)(resources.GetObject("d2MainMenuButton1.Image")));
            this.d2MainMenuButton1.Location = new System.Drawing.Point(12, 12);
            this.d2MainMenuButton1.Name = "d2MainMenuButton1";
            this.d2MainMenuButton1.Size = new System.Drawing.Size(134, 46);
            this.d2MainMenuButton1.TabIndex = 0;
            this.d2MainMenuButton1.Text = "Allowed mods";
            this.d2MainMenuButton1.UseVisualStyleBackColor = true;
            this.d2MainMenuButton1.Click += new System.EventHandler(this.D2MainMenuButton1_Click);
            // 
            // ModsListWidget
            // 
            this.ModsListWidget.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ModsListWidget.BackgroundImage")));
            this.ModsListWidget.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ModsListWidget.Location = new System.Drawing.Point(364, 12);
            this.ModsListWidget.Name = "ModsListWidget";
            this.ModsListWidget.Size = new System.Drawing.Size(424, 453);
            this.ModsListWidget.TabIndex = 1;
            // 
            // InstalledModsButton
            // 
            this.InstalledModsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InstalledModsButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InstalledModsButton.ForeColor = System.Drawing.Color.Black;
            this.InstalledModsButton.Image = ((System.Drawing.Image)(resources.GetObject("InstalledModsButton.Image")));
            this.InstalledModsButton.Location = new System.Drawing.Point(12, 64);
            this.InstalledModsButton.Name = "InstalledModsButton";
            this.InstalledModsButton.Size = new System.Drawing.Size(134, 46);
            this.InstalledModsButton.TabIndex = 2;
            this.InstalledModsButton.Text = "Installed mods";
            this.InstalledModsButton.UseVisualStyleBackColor = true;
            this.InstalledModsButton.Click += new System.EventHandler(this.InstalledModsButton_Click);
            // 
            // InstalledModsWidget
            // 
            this.InstalledModsWidget.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InstalledModsWidget.BackgroundImage")));
            this.InstalledModsWidget.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InstalledModsWidget.Location = new System.Drawing.Point(364, 12);
            this.InstalledModsWidget.Name = "InstalledModsWidget";
            this.InstalledModsWidget.Size = new System.Drawing.Size(424, 453);
            this.InstalledModsWidget.TabIndex = 3;
            // 
            // SoftWidget
            // 
            this.SoftWidget.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SoftWidget.BackgroundImage")));
            this.SoftWidget.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SoftWidget.Location = new System.Drawing.Point(364, 12);
            this.SoftWidget.Name = "SoftWidget";
            this.SoftWidget.Size = new System.Drawing.Size(424, 453);
            this.SoftWidget.TabIndex = 4;
            // 
            // SoftwareButton
            // 
            this.SoftwareButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SoftwareButton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SoftwareButton.ForeColor = System.Drawing.Color.Black;
            this.SoftwareButton.Image = ((System.Drawing.Image)(resources.GetObject("SoftwareButton.Image")));
            this.SoftwareButton.Location = new System.Drawing.Point(12, 116);
            this.SoftwareButton.Name = "SoftwareButton";
            this.SoftwareButton.Size = new System.Drawing.Size(134, 46);
            this.SoftwareButton.TabIndex = 5;
            this.SoftwareButton.Text = "Software";
            this.SoftwareButton.UseVisualStyleBackColor = true;
            this.SoftwareButton.Click += new System.EventHandler(this.SoftwareButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::D2Launcher.Properties.Resources.d2_backgroundimage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.SoftwareButton);
            this.Controls.Add(this.SoftWidget);
            this.Controls.Add(this.InstalledModsWidget);
            this.Controls.Add(this.InstalledModsButton);
            this.Controls.Add(this.ModsListWidget);
            this.Controls.Add(this.d2MainMenuButton1);
            this.MaximumSize = new System.Drawing.Size(816, 516);
            this.MinimumSize = new System.Drawing.Size(816, 516);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Resources.Buttons.D2MainMenuButton d2MainMenuButton1;
        private Resources.Widgets.ModsListWidget ModsListWidget;
        private Resources.Buttons.D2MainMenuButton InstalledModsButton;
        private Resources.Widgets.InstalledD2ModsWidget InstalledModsWidget;
        private Resources.Widgets.SoftWidget SoftWidget;
        private Resources.Buttons.D2MainMenuButton SoftwareButton;
    }
}