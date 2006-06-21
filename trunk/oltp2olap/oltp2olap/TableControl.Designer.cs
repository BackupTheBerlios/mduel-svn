namespace oltp2olap
{
    partial class TableControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableControl));
            this.lblHeader = new System.Windows.Forms.Label();
            this.DataIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Bisque;
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(87, 15);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "TableName";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataIcons
            // 
            this.DataIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DataIcons.ImageStream")));
            this.DataIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.DataIcons.Images.SetKeyName(0, "keys.png");
            this.DataIcons.Images.SetKeyName(1, "fk.png");
            // 
            // TableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblHeader);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TableControl";
            this.Size = new System.Drawing.Size(87, 25);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TableControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TableControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TableControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        public System.Windows.Forms.ImageList DataIcons;

    }
}
