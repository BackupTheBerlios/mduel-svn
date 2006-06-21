using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap
{
    public partial class DataSetViewer : UserControl
    {
        private DataSet dataSet;

        public DataSetViewer(DataSet dataSet)
        {
            InitializeComponent();

            this.dataSet = dataSet;
        }

        private void LoadTables()
        {
            int idx = 0;

            foreach (DataTable table in dataSet.Tables)
            {
                TableControl ctlTable = new TableControl(table);
                Controls.Add(ctlTable);
                ctlTable.Location = new Point(15 * idx, 15 * idx);
                idx++;
                
                foreach (DataRelation dr in dataSet.Relations)
                {
                    if (dr.ChildTable.TableName.Equals(table.TableName))
                    {
                        Control[] ctl = ctlTable.Controls.Find(dr.ChildColumns[0].ColumnName, false);
                        Label label = (Label)ctl[0];
                        label.ImageList = ctlTable.DataIcons;
                        label.ImageIndex = 1;
                        label.ImageAlign = ContentAlignment.MiddleLeft;
                    }
                }
            }
        }

        private void LoadRelations()
        {
            foreach (DataRelation dr in dataSet.Relations)
            {
                Control[] ctlParent = Controls.Find(dr.ParentTable.TableName, false);
                Control[] ctlChild = Controls.Find(dr.ChildTable.TableName, false);
                Control[] ctlParentCol = ctlParent[0].Controls.Find(dr.ParentColumns[0].ColumnName, false);
                Control[] ctlChildCol = ctlChild[0].Controls.Find(dr.ChildColumns[0].ColumnName, false);

                Pen myPen = new Pen(Color.Black, 2.0f);
                System.Drawing.Graphics g;
                g = this.CreateGraphics();
                g.DrawLine(
                    myPen,
                    ctlParent[0].Location.X + ctlParentCol[0].Location.X,
                    ctlParent[0].Location.Y + ctlParentCol[0].Location.Y + 10,
                    ctlChild[0].Location.X + ctlChildCol[0].Location.X,
                    ctlChild[0].Location.Y + ctlChildCol[0].Location.Y + 10);
                myPen.Dispose();
                g.Dispose();
            }
        }

        private void DataSetViewer_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadRelations();
        }

        private void DataSetViewer_Paint(object sender, PaintEventArgs e)
        {
            LoadRelations();
        }
    }
}
