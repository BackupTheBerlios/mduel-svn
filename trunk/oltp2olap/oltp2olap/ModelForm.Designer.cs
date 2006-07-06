namespace oltp2olap
{
    partial class ModelForm
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
            this.model1 = new Crainiate.ERM4.Model();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.entityTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classificationEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.componentEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aggregateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.manageTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFactTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // model1
            // 
            this.model1.AutoScroll = true;
            this.model1.AutoScrollMinSize = new System.Drawing.Size(2000, 2000);
            this.model1.AutoSize = true;
            this.model1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.model1.ContextMenuStrip = this.contextMenuStrip1;
            this.model1.DiagramSize = new System.Drawing.Size(2000, 2000);
            this.model1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.model1.DragElement = null;
            this.model1.GridColor = System.Drawing.Color.Silver;
            this.model1.GridSize = new System.Drawing.Size(20, 20);
            this.model1.Location = new System.Drawing.Point(0, 0);
            this.model1.Name = "model1";
            this.model1.PageLineSize = new System.Drawing.SizeF(0F, 0F);
            this.model1.Size = new System.Drawing.Size(579, 381);
            this.model1.TabIndex = 0;
            this.model1.WorkspaceColor = System.Drawing.SystemColors.AppWorkspace;
            this.model1.Zoom = 100F;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entityTypeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.tableOperationsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.manageTablesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 104);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // entityTypeToolStripMenuItem
            // 
            this.entityTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.classificationEntityToolStripMenuItem,
            this.componentEntityToolStripMenuItem,
            this.transactionEntityToolStripMenuItem});
            this.entityTypeToolStripMenuItem.Name = "entityTypeToolStripMenuItem";
            this.entityTypeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.entityTypeToolStripMenuItem.Text = "Entity Type";
            // 
            // classificationEntityToolStripMenuItem
            // 
            this.classificationEntityToolStripMenuItem.Name = "classificationEntityToolStripMenuItem";
            this.classificationEntityToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.classificationEntityToolStripMenuItem.Text = "Classification Entity";
            this.classificationEntityToolStripMenuItem.Click += new System.EventHandler(this.changeEntityTypeMenuItem_Click);
            // 
            // componentEntityToolStripMenuItem
            // 
            this.componentEntityToolStripMenuItem.Name = "componentEntityToolStripMenuItem";
            this.componentEntityToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.componentEntityToolStripMenuItem.Text = "Component Entity";
            this.componentEntityToolStripMenuItem.Click += new System.EventHandler(this.changeEntityTypeMenuItem_Click);
            // 
            // transactionEntityToolStripMenuItem
            // 
            this.transactionEntityToolStripMenuItem.Name = "transactionEntityToolStripMenuItem";
            this.transactionEntityToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.transactionEntityToolStripMenuItem.Text = "Transaction Entity";
            this.transactionEntityToolStripMenuItem.Click += new System.EventHandler(this.changeEntityTypeMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
            // 
            // tableOperationsToolStripMenuItem
            // 
            this.tableOperationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseToolStripMenuItem,
            this.aggregateToolStripMenuItem,
            this.createFactTableToolStripMenuItem});
            this.tableOperationsToolStripMenuItem.Name = "tableOperationsToolStripMenuItem";
            this.tableOperationsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.tableOperationsToolStripMenuItem.Text = "Table Operations";
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.collapseToolStripMenuItem.Text = "Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // aggregateToolStripMenuItem
            // 
            this.aggregateToolStripMenuItem.Name = "aggregateToolStripMenuItem";
            this.aggregateToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aggregateToolStripMenuItem.Text = "Aggregate";
            this.aggregateToolStripMenuItem.Click += new System.EventHandler(this.aggregateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(184, 6);
            // 
            // manageTablesToolStripMenuItem
            // 
            this.manageTablesToolStripMenuItem.Name = "manageTablesToolStripMenuItem";
            this.manageTablesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.manageTablesToolStripMenuItem.Text = "Add / Remove Tables";
            this.manageTablesToolStripMenuItem.Click += new System.EventHandler(this.manageTablesToolStripMenuItem_Click);
            // 
            // createFactTableToolStripMenuItem
            // 
            this.createFactTableToolStripMenuItem.Name = "createFactTableToolStripMenuItem";
            this.createFactTableToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.createFactTableToolStripMenuItem.Text = "Create Fact Table";
            this.createFactTableToolStripMenuItem.Click += new System.EventHandler(this.createFactTableToolStripMenuItem_Click);
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 381);
            this.Controls.Add(this.model1);
            this.Name = "ModelForm";
            this.TabText = "ModelForm";
            this.Text = "ModelForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModelForm_FormClosed);
            this.Load += new System.EventHandler(this.ModelForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Crainiate.ERM4.Model model1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem entityTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionEntityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem componentEntityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classificationEntityToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem manageTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableOperationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aggregateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFactTableToolStripMenuItem;
    }
}