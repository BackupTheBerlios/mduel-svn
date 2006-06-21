namespace oltp2olap
{
    partial class ProjectExplorer
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Project - Project1");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ctxMnuBase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newDataSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMnuBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Project - Project1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(236, 451);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // ctxMnuBase
            // 
            this.ctxMnuBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDataSourceToolStripMenuItem});
            this.ctxMnuBase.Name = "ctxMnuBase";
            this.ctxMnuBase.Size = new System.Drawing.Size(181, 48);
            // 
            // newDataSourceToolStripMenuItem
            // 
            this.newDataSourceToolStripMenuItem.Name = "newDataSourceToolStripMenuItem";
            this.newDataSourceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newDataSourceToolStripMenuItem.Text = "New Data Source...";
            this.newDataSourceToolStripMenuItem.Click += new System.EventHandler(this.newDataSourceToolStripMenuItem_Click);
            // 
            // ProjectExplorer
            // 
            this.AllowRedocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 451);
            this.CloseButton = false;
            this.Controls.Add(this.treeView1);
            this.DockableAreas = WeifenLuo.WinFormsUI.DockAreas.DockRight;
            this.Name = "ProjectExplorer";
            this.TabText = "Project Explorer";
            this.Text = "Project Explorer";
            this.Load += new System.EventHandler(this.ProjectExplorer_Load);
            this.ctxMnuBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip ctxMnuBase;
        private System.Windows.Forms.ToolStripMenuItem newDataSourceToolStripMenuItem;
    }
}