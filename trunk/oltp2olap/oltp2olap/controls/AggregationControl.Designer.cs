namespace oltp2olap.controls
{
    partial class AggregationControl
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
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.lvAggregation = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddAgg = new System.Windows.Forms.Button();
            this.btnRemoveAgg = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(6, 16);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(212, 20);
            this.txtTableName.TabIndex = 1;
            // 
            // lvAggregation
            // 
            this.lvAggregation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvAggregation.FullRowSelect = true;
            this.lvAggregation.Location = new System.Drawing.Point(6, 60);
            this.lvAggregation.MultiSelect = false;
            this.lvAggregation.Name = "lvAggregation";
            this.lvAggregation.Size = new System.Drawing.Size(212, 108);
            this.lvAggregation.TabIndex = 2;
            this.lvAggregation.UseCompatibleStateImageBehavior = false;
            this.lvAggregation.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Expression";
            this.columnHeader2.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Aggregate Table Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aggregation Attributes";
            // 
            // btnAddAgg
            // 
            this.btnAddAgg.Location = new System.Drawing.Point(224, 60);
            this.btnAddAgg.Name = "btnAddAgg";
            this.btnAddAgg.Size = new System.Drawing.Size(75, 23);
            this.btnAddAgg.TabIndex = 4;
            this.btnAddAgg.Text = "Add...";
            this.btnAddAgg.UseVisualStyleBackColor = true;
            this.btnAddAgg.Click += new System.EventHandler(this.btnAddAgg_Click);
            // 
            // btnRemoveAgg
            // 
            this.btnRemoveAgg.Location = new System.Drawing.Point(224, 89);
            this.btnRemoveAgg.Name = "btnRemoveAgg";
            this.btnRemoveAgg.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAgg.TabIndex = 5;
            this.btnRemoveAgg.Text = "Remove";
            this.btnRemoveAgg.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Grouping Attributes";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(6, 187);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(212, 108);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Column";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Expression";
            this.columnHeader4.Width = 120;
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Location = new System.Drawing.Point(224, 216);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveGroup.TabIndex = 9;
            this.btnRemoveGroup.Text = "Remove";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(224, 187);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroup.TabIndex = 8;
            this.btnAddGroup.Text = "Add...";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            // 
            // AggregationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemoveGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnRemoveAgg);
            this.Controls.Add(this.btnAddAgg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvAggregation);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label1);
            this.Name = "AggregationControl";
            this.Size = new System.Drawing.Size(312, 308);
            this.Load += new System.EventHandler(this.AggregationControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvAggregation;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAddAgg;
        private System.Windows.Forms.Button btnRemoveAgg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.Button btnAddGroup;
    }
}
