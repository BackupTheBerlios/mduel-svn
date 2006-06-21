using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap
{
    public partial class TableControl : UserControl
    {
        private DataTable table;

        private bool moving = false;
        private int old_x = 0;
        private int old_y = 0;

        public TableControl(DataTable table)
        {
            InitializeComponent();

            lblHeader.MouseDown += new MouseEventHandler(TableControl_MouseDown);
            lblHeader.MouseUp += new MouseEventHandler(TableControl_MouseUp);
            lblHeader.MouseMove += new MouseEventHandler(TableControl_MouseMove);

            this.table = table;
            lblHeader.Text = table.TableName;
            this.Name = table.TableName;

            foreach (DataColumn col in table.Columns)
            {
                AddColumn(col);
            }
        }

        private void AddColumn(DataColumn col)
        {
            Control[] ctlArray = new Control[Controls.Count];
            Controls.CopyTo(ctlArray, 0);

            this.Controls.Clear();

            Label lblNewRow = new Label();
            lblNewRow.Text = "   " + col.ColumnName;
            lblNewRow.Name = col.ColumnName;
            lblNewRow.Size = new Size(lblNewRow.Size.Width, 20);
            lblNewRow.Dock = DockStyle.Top;
            lblNewRow.AutoSize = true;
            lblNewRow.Font = new Font("Courier New", 8.25f);
            foreach (DataColumn pkc in col.Table.PrimaryKey)
            {
                if (col.Equals(pkc))
                {
                    lblNewRow.Font = new Font("Courier New", 8.25f, FontStyle.Italic);
                    lblNewRow.ImageList = DataIcons;
                    lblNewRow.ImageIndex = 0;
                    lblNewRow.ImageAlign = ContentAlignment.MiddleLeft;
                }
            }

            lblNewRow.MouseDown += new MouseEventHandler(TableControl_MouseDown);
            lblNewRow.MouseUp += new MouseEventHandler(TableControl_MouseUp);
            lblNewRow.MouseMove += new MouseEventHandler(TableControl_MouseMove);

            int maxWidth = 0;
            this.Controls.Add(lblNewRow);
            foreach (Control ctl in ctlArray)
            {
                if (ctl.Size.Width > maxWidth)
                    maxWidth = ctl.Size.Width;
                this.Controls.Add(ctl);
            }

            AutoSize = true;
            Size = new Size(new Point(maxWidth + 20, Size.Height));
        }

        private void TableControl_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            old_x = e.X;
            old_y = e.Y;
        }

        private void TableControl_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void TableControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                this.Left += e.X - old_x;
                this.Top += e.Y - old_y;
                this.Parent.Invalidate();
            }
        }
    }
}
