namespace oltp2olap.controls
{
    partial class SelectCollapsableRelations
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
            this.btnOut = new System.Windows.Forms.Button();
            this.btnInto = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTargets = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbRelations = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOut
            // 
            this.btnOut.Enabled = false;
            this.btnOut.Location = new System.Drawing.Point(210, 107);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(75, 23);
            this.btnOut.TabIndex = 7;
            this.btnOut.Text = "<<";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // btnInto
            // 
            this.btnInto.Enabled = false;
            this.btnInto.Location = new System.Drawing.Point(210, 78);
            this.btnInto.Name = "btnInto";
            this.btnInto.Size = new System.Drawing.Size(75, 23);
            this.btnInto.TabIndex = 6;
            this.btnInto.Text = ">>";
            this.btnInto.UseVisualStyleBackColor = true;
            this.btnInto.Click += new System.EventHandler(this.btnInto_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbTargets);
            this.groupBox2.Location = new System.Drawing.Point(291, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox2.Size = new System.Drawing.Size(200, 200);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Collapse onto these";
            // 
            // lbTargets
            // 
            this.lbTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTargets.FormattingEnabled = true;
            this.lbTargets.Location = new System.Drawing.Point(10, 16);
            this.lbTargets.Name = "lbTargets";
            this.lbTargets.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbTargets.Size = new System.Drawing.Size(180, 173);
            this.lbTargets.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbRelations);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox1.Size = new System.Drawing.Size(200, 200);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Related Tables / Relations";
            // 
            // lbRelations
            // 
            this.lbRelations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRelations.FormattingEnabled = true;
            this.lbRelations.Location = new System.Drawing.Point(10, 16);
            this.lbRelations.Name = "lbRelations";
            this.lbRelations.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbRelations.Size = new System.Drawing.Size(180, 173);
            this.lbRelations.TabIndex = 0;
            // 
            // SelectCollapsableRelations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.btnInto);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectCollapsableRelations";
            this.Size = new System.Drawing.Size(498, 213);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Button btnInto;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbTargets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbRelations;
    }
}
