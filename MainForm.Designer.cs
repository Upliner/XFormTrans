namespace XFormTrans
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
        	System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Tables", System.Windows.Forms.HorizontalAlignment.Left);
        	System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Views", System.Windows.Forms.HorizontalAlignment.Left);
        	System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Procedures", System.Windows.Forms.HorizontalAlignment.Left);
        	System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Functions", System.Windows.Forms.HorizontalAlignment.Left);
        	this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        	this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
        	this.tabPage3 = new System.Windows.Forms.TabPage();
        	this.lvForms = new System.Windows.Forms.ListView();
        	this.tabDbObj = new System.Windows.Forms.TabPage();
        	this.lvDbObj = new System.Windows.Forms.ListView();
        	this.colName = new System.Windows.Forms.ColumnHeader();
        	this.tabCtl = new System.Windows.Forms.TabControl();
        	this.menuStrip1.SuspendLayout();
        	this.tabPage3.SuspendLayout();
        	this.tabDbObj.SuspendLayout();
        	this.tabCtl.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// menuStrip1
        	// 
        	this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.fileToolStripMenuItem});
        	this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        	this.menuStrip1.Name = "menuStrip1";
        	this.menuStrip1.Size = new System.Drawing.Size(702, 29);
        	this.menuStrip1.TabIndex = 1;
        	this.menuStrip1.Text = "mainMenuStrip";
        	// 
        	// fileToolStripMenuItem
        	// 
        	this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.openToolStripMenuItem,
        	        	        	this.quitToolStripMenuItem});
        	this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        	this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
        	this.fileToolStripMenuItem.Text = "File";
        	// 
        	// openToolStripMenuItem
        	// 
        	this.openToolStripMenuItem.Name = "openToolStripMenuItem";
        	this.openToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
        	this.openToolStripMenuItem.Text = "Open";
        	this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
        	// 
        	// quitToolStripMenuItem
        	// 
        	this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
        	this.quitToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
        	this.quitToolStripMenuItem.Text = "Quit";
        	this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItemClick);
        	// 
        	// openFileDialog
        	// 
        	this.openFileDialog.Filter = "XForm files (*.xml)|*.xml|All files|*.*";
        	// 
        	// tabPage3
        	// 
        	this.tabPage3.Controls.Add(this.lvForms);
        	this.tabPage3.Location = new System.Drawing.Point(4, 25);
        	this.tabPage3.Name = "tabPage3";
        	this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage3.Size = new System.Drawing.Size(694, 408);
        	this.tabPage3.TabIndex = 2;
        	this.tabPage3.Text = "Forms";
        	this.tabPage3.UseVisualStyleBackColor = true;
        	// 
        	// lvForms
        	// 
        	this.lvForms.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.lvForms.Location = new System.Drawing.Point(3, 3);
        	this.lvForms.Name = "lvForms";
        	this.lvForms.Size = new System.Drawing.Size(688, 402);
        	this.lvForms.TabIndex = 1;
        	this.lvForms.UseCompatibleStateImageBehavior = false;
        	// 
        	// tabDbObj
        	// 
        	this.tabDbObj.Controls.Add(this.lvDbObj);
        	this.tabDbObj.Location = new System.Drawing.Point(4, 25);
        	this.tabDbObj.Name = "tabDbObj";
        	this.tabDbObj.Padding = new System.Windows.Forms.Padding(3);
        	this.tabDbObj.Size = new System.Drawing.Size(694, 408);
        	this.tabDbObj.TabIndex = 0;
        	this.tabDbObj.Text = "DB Objects";
        	this.tabDbObj.UseVisualStyleBackColor = true;
        	// 
        	// lvDbObj
        	// 
        	this.lvDbObj.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
        	        	        	this.colName});
        	this.lvDbObj.Dock = System.Windows.Forms.DockStyle.Fill;
        	listViewGroup1.Header = "Tables";
        	listViewGroup1.Name = "grTables";
        	listViewGroup2.Header = "Views";
        	listViewGroup2.Name = "grViews";
        	listViewGroup3.Header = "Procedures";
        	listViewGroup3.Name = "grProcs";
        	listViewGroup4.Header = "Functions";
        	listViewGroup4.Name = "grFuncs";
        	this.lvDbObj.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
        	        	        	listViewGroup1,
        	        	        	listViewGroup2,
        	        	        	listViewGroup3,
        	        	        	listViewGroup4});
        	this.lvDbObj.Location = new System.Drawing.Point(3, 3);
        	this.lvDbObj.Name = "lvDbObj";
        	this.lvDbObj.Size = new System.Drawing.Size(688, 402);
        	this.lvDbObj.TabIndex = 0;
        	this.lvDbObj.UseCompatibleStateImageBehavior = false;
        	this.lvDbObj.View = System.Windows.Forms.View.Details;
        	// 
        	// colName
        	// 
        	this.colName.Text = "Object name";
        	this.colName.Width = 422;
        	// 
        	// tabCtl
        	// 
        	this.tabCtl.Controls.Add(this.tabDbObj);
        	this.tabCtl.Controls.Add(this.tabPage3);
        	this.tabCtl.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tabCtl.Location = new System.Drawing.Point(0, 29);
        	this.tabCtl.Margin = new System.Windows.Forms.Padding(0);
        	this.tabCtl.Name = "tabCtl";
        	this.tabCtl.SelectedIndex = 0;
        	this.tabCtl.Size = new System.Drawing.Size(702, 437);
        	this.tabCtl.TabIndex = 0;
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(702, 466);
        	this.Controls.Add(this.tabCtl);
        	this.Controls.Add(this.menuStrip1);
        	this.MainMenuStrip = this.menuStrip1;
        	this.Name = "MainForm";
        	this.Text = "MainForm";
        	this.Load += new System.EventHandler(this.MainFormLoad);
        	this.menuStrip1.ResumeLayout(false);
        	this.menuStrip1.PerformLayout();
        	this.tabPage3.ResumeLayout(false);
        	this.tabDbObj.ResumeLayout(false);
        	this.tabCtl.ResumeLayout(false);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.TabControl tabCtl;
        private System.Windows.Forms.ListView lvDbObj;
        private System.Windows.Forms.TabPage tabDbObj;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListView lvForms;
        private System.Windows.Forms.TabPage tabPage3;
    }
}
