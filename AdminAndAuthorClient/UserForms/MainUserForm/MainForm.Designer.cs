namespace AdminAndAuthorClient.UserForms.MainUserForm
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
            this.ModsListDataGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateModListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getUserByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ModsListDataGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModsListDataGrid
            // 
            this.ModsListDataGrid.AllowUserToAddRows = false;
            this.ModsListDataGrid.AllowUserToDeleteRows = false;
            this.ModsListDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ModsListDataGrid.Location = new System.Drawing.Point(12, 27);
            this.ModsListDataGrid.Name = "ModsListDataGrid";
            this.ModsListDataGrid.ReadOnly = true;
            this.ModsListDataGrid.RowTemplate.Height = 25;
            this.ModsListDataGrid.Size = new System.Drawing.Size(776, 150);
            this.ModsListDataGrid.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modsToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.clientToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modsToolStripMenuItem
            // 
            this.modsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.filesToolStripMenuItem,
            this.updateModListToolStripMenuItem});
            this.modsToolStripMenuItem.Name = "modsToolStripMenuItem";
            this.modsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.modsToolStripMenuItem.Text = "Mods";
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.uploadToolStripMenuItem.Text = "Upload";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.UploadToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItem_Click);
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.filesToolStripMenuItem.Text = "Files";
            this.filesToolStripMenuItem.Click += new System.EventHandler(this.FilesToolStripMenuItem_Click);
            // 
            // updateModListToolStripMenuItem
            // 
            this.updateModListToolStripMenuItem.Name = "updateModListToolStripMenuItem";
            this.updateModListToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.updateModListToolStripMenuItem.Text = "Update mod list";
            this.updateModListToolStripMenuItem.Click += new System.EventHandler(this.UpdateModListToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.getUserByIdToolStripMenuItem,
            this.deleteUserByIdToolStripMenuItem,
            this.checkTokenToolStripMenuItem});
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // getUserByIdToolStripMenuItem
            // 
            this.getUserByIdToolStripMenuItem.Name = "getUserByIdToolStripMenuItem";
            this.getUserByIdToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.getUserByIdToolStripMenuItem.Text = "Get user by id";
            // 
            // deleteUserByIdToolStripMenuItem
            // 
            this.deleteUserByIdToolStripMenuItem.Name = "deleteUserByIdToolStripMenuItem";
            this.deleteUserByIdToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteUserByIdToolStripMenuItem.Text = "Delete user by id";
            // 
            // checkTokenToolStripMenuItem
            // 
            this.checkTokenToolStripMenuItem.Name = "checkTokenToolStripMenuItem";
            this.checkTokenToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.checkTokenToolStripMenuItem.Text = "Check token";
            this.checkTokenToolStripMenuItem.Click += new System.EventHandler(this.CheckTokenToolStripMenuItem_Click);
            // 
            // clientToolStripMenuItem
            // 
            this.clientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem1,
            this.downloadToolStripMenuItem1});
            this.clientToolStripMenuItem.Name = "clientToolStripMenuItem";
            this.clientToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.clientToolStripMenuItem.Text = "Client";
            // 
            // uploadToolStripMenuItem1
            // 
            this.uploadToolStripMenuItem1.Name = "uploadToolStripMenuItem1";
            this.uploadToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.uploadToolStripMenuItem1.Text = "Upload";
            // 
            // downloadToolStripMenuItem1
            // 
            this.downloadToolStripMenuItem1.Name = "downloadToolStripMenuItem1";
            this.downloadToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem1.Text = "Download";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 191);
            this.Controls.Add(this.ModsListDataGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ModsListDataGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView ModsListDataGrid;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem modsToolStripMenuItem;
        private ToolStripMenuItem uploadToolStripMenuItem;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem filesToolStripMenuItem;
        private ToolStripMenuItem usersToolStripMenuItem;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem getUserByIdToolStripMenuItem;
        private ToolStripMenuItem deleteUserByIdToolStripMenuItem;
        private ToolStripMenuItem checkTokenToolStripMenuItem;
        private ToolStripMenuItem clientToolStripMenuItem;
        private ToolStripMenuItem uploadToolStripMenuItem1;
        private ToolStripMenuItem downloadToolStripMenuItem1;
        private ToolStripMenuItem updateModListToolStripMenuItem;
    }
}