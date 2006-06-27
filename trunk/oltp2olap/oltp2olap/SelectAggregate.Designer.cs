namespace oltp2olap
{
    partial class SelectAggregate
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lvAggregation = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveAgg = new System.Windows.Forms.Button();
            this.btnAddAgg = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvGrouping = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(325, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(234, 317);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lvAggregation
            // 
            this.lvAggregation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvAggregation.FullRowSelect = true;
            this.lvAggregation.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvAggregation.Location = new System.Drawing.Point(15, 69);
            this.lvAggregation.Name = "lvAggregation";
            this.lvAggregation.Size = new System.Drawing.Size(294, 97);
            this.lvAggregation.TabIndex = 19;
            this.lvAggregation.UseCompatibleStateImageBehavior = false;
            this.lvAggregation.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Attribute";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Expression";
            this.columnHeader2.Width = 190;
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Location = new System.Drawing.Point(325, 225);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveGroup.TabIndex = 18;
            this.btnRemoveGroup.Text = "Remove";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(325, 196);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroup.TabIndex = 17;
            this.btnAddGroup.Text = "Add...";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Grouping Attributes";
            // 
            // btnRemoveAgg
            // 
            this.btnRemoveAgg.Location = new System.Drawing.Point(325, 98);
            this.btnRemoveAgg.Name = "btnRemoveAgg";
            this.btnRemoveAgg.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAgg.TabIndex = 15;
            this.btnRemoveAgg.Text = "Remove";
            this.btnRemoveAgg.UseVisualStyleBackColor = true;
            this.btnRemoveAgg.Click += new System.EventHandler(this.btnRemoveAgg_Click);
            // 
            // btnAddAgg
            // 
            this.btnAddAgg.Location = new System.Drawing.Point(325, 69);
            this.btnAddAgg.Name = "btnAddAgg";
            this.btnAddAgg.Size = new System.Drawing.Size(75, 23);
            this.btnAddAgg.TabIndex = 14;
            this.btnAddAgg.Text = "Add...";
            this.btnAddAgg.UseVisualStyleBackColor = true;
            this.btnAddAgg.Click += new System.EventHandler(this.btnAddAgg_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Aggregation Attributes";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(15, 25);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(212, 20);
            this.txtTableName.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Aggregate Table Name";
            // 
            // lvGrouping
            // 
            this.lvGrouping.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvGrouping.FullRowSelect = true;
            this.lvGrouping.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGrouping.Location = new System.Drawing.Point(15, 196);
            this.lvGrouping.Name = "lvGrouping";
            this.lvGrouping.Size = new System.Drawing.Size(294, 97);
            this.lvGrouping.TabIndex = 20;
            this.lvGrouping.UseCompatibleStateImageBehavior = false;
            this.lvGrouping.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Attribute";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Expression";
            this.columnHeader4.Width = 190;
            // 
            // SelectAggregate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 352);
            this.Controls.Add(this.lvGrouping);
            this.Controls.Add(this.lvAggregation);
            this.Controls.Add(this.btnRemoveGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemoveAgg);
            this.Controls.Add(this.btnAddAgg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectAggregate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aggregate";
            this.Load += new System.EventHandler(this.SelectAggregate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListView lvAggregation;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemoveAgg;
        private System.Windows.Forms.Button btnAddAgg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvGrouping;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}