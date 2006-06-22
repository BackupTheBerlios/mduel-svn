using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using Crainiate.ERM4;
using Crainiate.ERM4.Layouts;
using WeifenLuo.WinFormsUI;
using System.Windows.Forms;
using System.Collections.Generic;
using oltp2olap.helpers;
using oltp2olap.heuristics;
using System.Text;

namespace oltp2olap
{
    public partial class ModelForm : DockContent
    {
        private DataSet dataSet;
        private Dictionary<string, EntityTypes> entityTypes;

        public ModelForm()
        {
            InitializeComponent();
            Component.Instance.DefaultFont = new Font("Tahoma", 8);

            entityTypes = new Dictionary<string, EntityTypes>(13);
        }

        public void LoadDataSet(DataSet ds)
        {
            Text = ds.DataSetName;
            TabText = ds.DataSetName;
            dataSet = ds;

            if (dataSet == null || dataSet.Relations == null)
                return;

            List<string> remove = new List<string>();
            foreach (string table in entityTypes.Keys)
            {
                if (!dataSet.Tables.Contains(table))
                    remove.Add(table);
            }
            foreach (string str in remove)
                entityTypes.Remove(str);

            model1.Suspend();
            model1.Shapes.Clear();
            foreach (DataTable table in dataSet.Tables)
            {
                Table t = new Table();
                t.BackColor = Color.White;
                t.GradientColor = Color.FromArgb(96, SystemColors.Highlight);
                t.Location = new PointF(100, 50);
                t.Width = 140;
                t.Height = 200;
                t.Indent = 10;
                t.Heading = table.TableName;
                t.SubHeading = "Table";
                t.MaximumSize = new SizeF(new Point(1000, 1000));
                t.DrawExpand = true;
                t.AllowScale = false;

                TableGroup tg = new TableGroup();
                tg.Text = "Columns";
                t.Groups.Add(tg);

                //float maxWidth = 0.0f;
                StringBuilder sb = new StringBuilder();
                foreach (DataColumn column in table.Columns)
                {
                    TableRow row = new TableRow();
                    row.Text = column.ColumnName;
                    row.Image = new Crainiate.ERM4.Image("Resource.publicfield.gif", "Crainiate.ERM4.Component");
                    foreach (DataColumn col in table.PrimaryKey)
                    {
                        if (col.Equals(column))
                            row.Image = new Crainiate.ERM4.Image("Resource.protectedfield.gif", "Crainiate.ERM4.Component");
                    }
                    tg.Rows.Add(row);
                    /*Graphics g = model1.CreateGraphics();
                    SizeF size = g.MeasureString(row.Text, Component.Instance.DefaultFont);
                    float width = size.Width + 50;
                    if (width > maxWidth)
                        maxWidth = width;*/
                    sb.Append("\r\n" + row.Text);
                }
                model1.Shapes.Add(table.TableName, t);
                //t.Width = maxWidth;
                t.Tooltip = sb.ToString();

                if (!entityTypes.ContainsKey(table.TableName))
                    entityTypes[table.TableName] = EntityTypes.Unclassified;
            }

            model1.Lines.Clear();
            foreach (DataRelation dr in dataSet.Relations)
            {
                Connector line = new Connector((Shape)model1.Shapes[dr.ParentTable.TableName], (Shape)model1.Shapes[dr.ChildTable.TableName]);
                line.Rounded = true;
                line.End.Marker = new Arrow();
                line.Tag = dr.RelationName;
                model1.Lines.Add(model1.Lines.CreateKey(), line);
            }
            model1.Resume();

            DoLayout();
        }

        private void DoLayout()
        {
            model1.Suspend();
            Graph graph = new Graph();
            graph.AddDiagram(model1);

            HierarchicalLayout layout = new HierarchicalLayout();
            layout.ConnectedComponentDistance = 20;
            layout.LayerDistance = 40;
            layout.ObjectDistance = 20;
            
            layout.DoLayout(graph);

            if (layout != null && layout.Status == LayoutStatus.Success)
            {
                graph.ScaleToFit(model1.DiagramSize);
                graph.Apply();
            }
            model1.Resume();
            model1.Refresh();
            DrawEntityTypes();
        }

        public void SetEntityTypes(Dictionary<string, EntityTypes> entities)
        {
            entityTypes = new Dictionary<string, EntityTypes>(entities);
        }

        public void DrawEntityTypes()
        {
            model1.Suspend();
            foreach (string table in entityTypes.Keys)
            {
                Table t = (Table)model1.Shapes[table];
                if (t == null)
                    continue;

                string type = string.Empty;

                if (entityTypes[table].Equals(EntityTypes.TransactionEntity))
                {
                    type = "Transaction Entity";
                    t.GradientColor = Color.DarkBlue;
                }
                else if (entityTypes[table].Equals(EntityTypes.ComponentEntity))
                {
                    type = "Component Entity";
                    t.GradientColor = Color.DarkGreen;
                }
                else if (entityTypes[table].Equals(EntityTypes.ClassificationEntity))
                {
                    type = "Classification Entity";
                    t.GradientColor = Color.Yellow;
                }
                else if (entityTypes[table].Equals(EntityTypes.Unclassified))
                {
                    type = "Unclassified Entity";
                    t.GradientColor = Color.Gray;
                }

                t.SubHeading = "Table - " + type;
            }
            model1.Resume();
            model1.Refresh();
        }

        public void SetEntityType(string table, EntityTypes type)
        {
            entityTypes[table] = type;
        }

        public void SetZoom(int zoom)
        {
            model1.Zoom = zoom;
        }

        public DataSet DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }

        private void ModelForm_Load(object sender, System.EventArgs e)
        {
            MainForm frmMain = (MainForm)DockPanel.FindForm();
            frmMain.ToggleZoom();
        }

        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm frmMain = (MainForm)DockPanel.FindForm();
            frmMain.ToggleZoom();
        }

        private void changeEntityTypeMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mnuItem = (ToolStripMenuItem)sender;
            EntityTypes type = EntityTypes.Unclassified;
            if (mnuItem.Text == "Transaction Entity")
                type = EntityTypes.TransactionEntity;
            else if (mnuItem.Text == "Component Entity")
                type = EntityTypes.ComponentEntity;
            else if (mnuItem.Text == "Classification Entity")
                type = EntityTypes.ClassificationEntity;

            Elements elements = model1.SelectedElements(typeof(Table));
            foreach (string el in elements.Keys)
            {
                entityTypes[el] = type;
            }
            DrawEntityTypes();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Elements elements = model1.SelectedElements(typeof(Table));
            if (elements.Count == 1)
            {
                entityTypeToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                tableOperationsToolStripMenuItem.Visible = true;
                toolStripMenuItem2.Visible = true;
            }
            else if (elements.Count > 0)
            {
                entityTypeToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                tableOperationsToolStripMenuItem.Visible = false;
                toolStripMenuItem2.Visible = false;
            }
            else
            {
                entityTypeToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                tableOperationsToolStripMenuItem.Visible = false;
                toolStripMenuItem2.Visible = false;
            }
        }

        private void manageTablesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ProjectExplorer prjExplorer = (ProjectExplorer)DockPanel.Controls.Find("Project Explorer", true)[0];
            EditWorkingTables ewt = new EditWorkingTables(prjExplorer.DataSets[Text], dataSet);
            DialogResult result = ewt.ShowDialog();

            if (result == DialogResult.OK)
                LoadDataSet(ewt.WorkDataSet);
        }

        private void collapseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Elements elms = model1.SelectedElements(typeof(Table));
            //List<LinkedList<string>> clone = Classification.getMaximalHierarchies();
            //for each eleme GetAccessibilityObjectById ele

            foreach (string el in elms.Keys)
            {
                Shape table = (Shape) elms[el];
                Collapse c = new Collapse(dataSet, table.Key);
                LoadDataSet(c.GetResult());
            }
        }
    }
}