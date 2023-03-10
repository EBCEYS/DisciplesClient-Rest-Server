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
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getUserByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SoftListDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ModsListDataGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoftListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ModsListDataGrid
            // 
            this.ModsListDataGrid.AllowUserToAddRows = false;
            this.ModsListDataGrid.AllowUserToDeleteRows = false;
            this.ModsListDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ModsListDataGrid.Location = new System.Drawing.Point(0, 57);
            this.ModsListDataGrid.Name = "ModsListDataGrid";
            this.ModsListDataGrid.ReadOnly = true;
            this.ModsListDataGrid.RowTemplate.Height = 25;
            this.ModsListDataGrid.Size = new System.Drawing.Size(800, 167);
            this.ModsListDataGrid.TabIndex = 0;
            this.ModsListDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ModsListDataGrid_CellClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modsToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.logoutToolStripMenuItem});
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
            this.updateToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.filesToolStripMenuItem,
            this.updateModListToolStripMenuItem});
            this.modsToolStripMenuItem.Name = "modsToolStripMenuItem";
            this.modsToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.modsToolStripMenuItem.Text = "Mods/Software";
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.uploadToolStripMenuItem.Text = "Upload";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.UploadToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItem_Click);
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.filesToolStripMenuItem.Text = "Files";
            this.filesToolStripMenuItem.Click += new System.EventHandler(this.FilesToolStripMenuItem_Click);
            // 
            // updateModListToolStripMenuItem
            // 
            this.updateModListToolStripMenuItem.Name = "updateModListToolStripMenuItem";
            this.updateModListToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.updateModListToolStripMenuItem.Text = "Update tables";
            this.updateModListToolStripMenuItem.Click += new System.EventHandler(this.UpdateModListToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.getUserByIdToolStripMenuItem,
            this.deleteUserByIdToolStripMenuItem,
            this.checkTokenToolStripMenuItem,
            this.changeToolStripMenuItem});
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // getUserByIdToolStripMenuItem
            // 
            this.getUserByIdToolStripMenuItem.Name = "getUserByIdToolStripMenuItem";
            this.getUserByIdToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.getUserByIdToolStripMenuItem.Text = "Get user by id";
            this.getUserByIdToolStripMenuItem.Click += new System.EventHandler(this.GetUserByIdToolStripMenuItem_Click);
            // 
            // deleteUserByIdToolStripMenuItem
            // 
            this.deleteUserByIdToolStripMenuItem.Name = "deleteUserByIdToolStripMenuItem";
            this.deleteUserByIdToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteUserByIdToolStripMenuItem.Text = "Delete user by id";
            this.deleteUserByIdToolStripMenuItem.Click += new System.EventHandler(this.DeleteUserByIdToolStripMenuItem_Click);
            // 
            // checkTokenToolStripMenuItem
            // 
            this.checkTokenToolStripMenuItem.Name = "checkTokenToolStripMenuItem";
            this.checkTokenToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.checkTokenToolStripMenuItem.Text = "Check token";
            this.checkTokenToolStripMenuItem.Click += new System.EventHandler(this.CheckTokenToolStripMenuItem_Click);
            // 
            // changeToolStripMenuItem
            // 
            this.changeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userNameToolStripMenuItem,
            this.passwordToolStripMenuItem,
            this.emailToolStripMenuItem});
            this.changeToolStripMenuItem.Name = "changeToolStripMenuItem";
            this.changeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.changeToolStripMenuItem.Text = "Change";
            // 
            // userNameToolStripMenuItem
            // 
            this.userNameToolStripMenuItem.Name = "userNameToolStripMenuItem";
            this.userNameToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.userNameToolStripMenuItem.Text = "UserName";
            this.userNameToolStripMenuItem.Click += new System.EventHandler(this.UserNameToolStripMenuItem_Click);
            // 
            // passwordToolStripMenuItem
            // 
            this.passwordToolStripMenuItem.Name = "passwordToolStripMenuItem";
            this.passwordToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.passwordToolStripMenuItem.Text = "Password";
            this.passwordToolStripMenuItem.Click += new System.EventHandler(this.PasswordToolStripMenuItem_Click);
            // 
            // emailToolStripMenuItem
            // 
            this.emailToolStripMenuItem.Name = "emailToolStripMenuItem";
            this.emailToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.emailToolStripMenuItem.Text = "Email";
            this.emailToolStripMenuItem.Click += new System.EventHandler(this.EmailToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.LogoutToolStripMenuItem_Click);
            // 
            // SoftListDataGridView
            // 
            this.SoftListDataGridView.AllowUserToAddRows = false;
            this.SoftListDataGridView.AllowUserToDeleteRows = false;
            this.SoftListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SoftListDataGridView.Location = new System.Drawing.Point(0, 264);
            this.SoftListDataGridView.Name = "SoftListDataGridView";
            this.SoftListDataGridView.ReadOnly = true;
            this.SoftListDataGridView.RowTemplate.Height = 25;
            this.SoftListDataGridView.Size = new System.Drawing.Size(800, 167);
            this.SoftListDataGridView.TabIndex = 2;
            this.SoftListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SoftListDataGridView_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mods:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Software:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 434);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SoftListDataGridView);
            this.Controls.Add(this.ModsListDataGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Disciples 2 Admin AND Author Client";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ModsListDataGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoftListDataGridView)).EndInit();
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
        private ToolStripMenuItem updateModListToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem changeToolStripMenuItem;
        private ToolStripMenuItem userNameToolStripMenuItem;
        private ToolStripMenuItem passwordToolStripMenuItem;
        private ToolStripMenuItem emailToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private DataGridView SoftListDataGridView;
        private Label label1;
        private Label label2;
        private ToolStripMenuItem updateToolStripMenuItem;
    }
}