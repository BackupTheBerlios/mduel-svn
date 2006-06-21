namespace oltp2olap.controls
{
    partial class SelectTables
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbDbTables = new System.Windows.Forms.ListBox();
            this.lbWsTables = new System.Windows.Forms.ListBox();
            this.btnIntoWorkspace = new System.Windows.Forms.Button();
            this.btnOutWorkspace = new System.Windows.Forms.Button();
            this.cbRelatedTables = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbDbTables);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox1.Size = new System.Drawing.Size(200, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Tables";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbWsTables);
            this.groupBox2.Location = new System.Drawing.Point(290, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox2.Size = new System.Drawing.Size(200, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables In Workspace";
            // 
            // lbDbTables
            // 
            this.lbDbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDbTables.FormattingEnabled = true;
            this.lbDbTables.Location = new System.Drawing.Point(10, 16);
            this.lbDbTables.Name = "lbDbTables";
            this.lbDbTables.Size = new System.Drawing.Size(180, 173);
            this.lbDbTables.TabIndex = 0;

            // 
            // lbWsTables
            // 
            this.lbWsTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWsTables.FormattingEnabled = true;
            this.lbWsTables.Location = new System.Drawing.Point(10, 16);
            this.lbWsTables.Name = "lbWsTables";
            this.lbWsTables.Size = new System.Drawing.Size(180, 173);
            this.lbWsTables.TabIndex = 1;

            // 
            // btnIntoWorkspace
            // 
            this.btnIntoWorkspace.Enabled = false;
            this.btnIntoWorkspace.Location = new System.Drawing.Point(209, 75);
            this.btnIntoWorkspace.Name = "btnIntoWorkspace";
            this.btnIntoWorkspace.Size = new System.Drawing.Size(75, 23);
            this.btnIntoWorkspace.TabIndex = 2;
            this.btnIntoWorkspace.Text = ">>";
            this.btnIntoWorkspace.UseVisualStyleBackColor = true;
            this.btnIntoWorkspace.Click += new System.EventHandler(this.btnIntoWorkspace_Click);
            // 
            // btnOutWorkspace
            // 
            this.btnOutWorkspace.Enabled = false;
            this.btnOutWorkspace.Location = new System.Drawing.Point(209, 104);
            this.btnOutWorkspace.Name = "btnOutWorkspace";
            this.btnOutWorkspace.Size = new System.Drawing.Size(75, 23);
            this.btnOutWorkspace.TabIndex = 3;
            this.btnOutWorkspace.Text = "<<";
            this.btnOutWorkspace.UseVisualStyleBackColor = true;
            this.btnOutWorkspace.Click += new System.EventHandler(this.btnOutWorkspace_Click);
            // 
            // cbRelatedTables
            // 
            this.cbRelatedTables.AutoSize = true;
            this.cbRelatedTables.Location = new System.Drawing.Point(13, 209);
            this.cbRelatedTables.Name = "cbRelatedTables";
            this.cbRelatedTables.Size = new System.Drawing.Size(122, 17);
            this.cbRelatedTables.TabIndex = 4;
            this.cbRelatedTables.Text = "Select related tables";
            this.cbRelatedTables.UseVisualStyleBackColor = true;
            // 
            // SelectTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbRelatedTables);
            this.Controls.Add(this.btnOutWorkspace);
            this.Controls.Add(this.btnIntoWorkspace);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectTables";
            this.Size = new System.Drawing.Size(493, 234);
            this.Load += new System.EventHandler(this.SelectTables_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbDbTables;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbWsTables;
        private System.Windows.Forms.Button btnIntoWorkspace;
        private System.Windows.Forms.Button btnOutWorkspace;
        private System.Windows.Forms.CheckBox cbRelatedTables;
    }
}
