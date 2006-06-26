namespace oltp2olap.controls
{
    partial class SelectAggregationAttribute
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbAttributes = new System.Windows.Forms.ComboBox();
            this.rbOperation = new System.Windows.Forms.RadioButton();
            this.cbOperations = new System.Windows.Forms.ComboBox();
            this.rbExpression = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numerical Attributes";
            // 
            // cbAttributes
            // 
            this.cbAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAttributes.FormattingEnabled = true;
            this.cbAttributes.Location = new System.Drawing.Point(120, 21);
            this.cbAttributes.Name = "cbAttributes";
            this.cbAttributes.Size = new System.Drawing.Size(231, 21);
            this.cbAttributes.TabIndex = 1;
            // 
            // rbOperation
            // 
            this.rbOperation.AutoSize = true;
            this.rbOperation.Location = new System.Drawing.Point(16, 48);
            this.rbOperation.Name = "rbOperation";
            this.rbOperation.Size = new System.Drawing.Size(71, 17);
            this.rbOperation.TabIndex = 2;
            this.rbOperation.TabStop = true;
            this.rbOperation.Text = "Operation";
            this.rbOperation.UseVisualStyleBackColor = true;
            // 
            // cbOperations
            // 
            this.cbOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperations.FormattingEnabled = true;
            this.cbOperations.Items.AddRange(new object[] {
            "SUM()",
            "AVG()",
            "MAX()",
            "MIN()",
            "COUNT()"});
            this.cbOperations.Location = new System.Drawing.Point(120, 48);
            this.cbOperations.Name = "cbOperations";
            this.cbOperations.Size = new System.Drawing.Size(231, 21);
            this.cbOperations.TabIndex = 3;
            // 
            // rbExpression
            // 
            this.rbExpression.AutoSize = true;
            this.rbExpression.Location = new System.Drawing.Point(16, 75);
            this.rbExpression.Name = "rbExpression";
            this.rbExpression.Size = new System.Drawing.Size(76, 17);
            this.rbExpression.TabIndex = 4;
            this.rbExpression.TabStop = true;
            this.rbExpression.Text = "Expression";
            this.rbExpression.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(185, 122);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(120, 76);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(231, 20);
            this.txtExpression.TabIndex = 7;
            // 
            // SelectAggregationAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 157);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbExpression);
            this.Controls.Add(this.cbOperations);
            this.Controls.Add(this.rbOperation);
            this.Controls.Add(this.cbAttributes);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectAggregationAttribute";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aggregation Attributes";
            this.Load += new System.EventHandler(this.SelectAggregationAttribute_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAttributes;
        private System.Windows.Forms.RadioButton rbOperation;
        private System.Windows.Forms.ComboBox cbOperations;
        private System.Windows.Forms.RadioButton rbExpression;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtExpression;
    }
}