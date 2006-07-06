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
using System;

namespace oltp2olap
{
    public partial class ModelForm : DockContent
    {
        private SqlSchema sqlSchema;
        private DataSet dataSet;
        private Dictionary<string, EntityTypes> entityTypes;
        private List<string> visibleTables;
        private List<int> minimalEntities;
        private List<int> maximalEntities;
        private List<List<int>> maximalHierarchies;
        private string dataBaseFileName;

        public ModelForm()
        {
            InitializeComponent();
            Component.Instance.DefaultFont = new Font("Tahoma", 8);

            entityTypes = new Dictionary<string, EntityTypes>(13);
        }

        public void LoadDataSet(DataSet ds)
        {
            Route route = new Route(model1);
            route.Avoid = true;
            model1.Route = route;

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
                if (!visibleTables.Contains(table.TableName))
                    continue;

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

                float maxWidth = 0.0f;
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
                    Graphics g = model1.CreateGraphics();
                    SizeF size = g.MeasureString(row.Text, Component.Instance.DefaultFont);
                    float width = size.Width + 50;
                    if (width > maxWidth)
                        maxWidth = width;
                }
                model1.Shapes.Add(table.TableName, t);
                t.Width = maxWidth;

                if (!entityTypes.ContainsKey(table.TableName))
                    entityTypes[table.TableName] = EntityTypes.Unclassified;
            }

            model1.Lines.Clear();
            foreach (DataRelation dr in dataSet.Relations)
            {
                if (!visibleTables.Contains(dr.ParentTable.TableName))
                    continue;

                if (!visibleTables.Contains(dr.ChildTable.TableName))
                    continue;

                Connector line = new Connector((Shape)model1.Shapes[dr.ParentTable.TableName], (Shape)model1.Shapes[dr.ChildTable.TableName]);
                line.Avoid = true;
                line.Rounded = true;
                line.End.Marker = new Arrow();
                line.Tag = dr.RelationName;
                model1.Lines.Add(model1.Lines.CreateKey(), line);
            }

            DoLayout();

            RefreshHierarquies();
        }

        public void DoLayout()
        {
            Graph graph = new Graph();
            graph.AddDiagram(model1);

            HierarchicalLayout layout = new HierarchicalLayout();
            /*layout.ConnectedComponentDistance = 20;
            layout.LayerDistance = 40;
            layout.ObjectDistance = 20;*/
            
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

        public void SetVisibleTables(List<string> tables)
        {
            visibleTables = tables;
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

        public void ClassifyEntities(ClassificationTypes type)
        {
            Classification c = new Classification(dataSet, visibleTables);
            SetEntityTypes(c.ClassificateEntities(type));
            DrawEntityTypes();
        }

        public void SetZoom(int zoom)
        {
            model1.Zoom = zoom;
        }

        private void ModelForm_Load(object sender, System.EventArgs e)
        {
            MainForm frmMain = (MainForm)DockPanel.FindForm();
            frmMain.SetZoomOn();
        }

        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm frmMain = (MainForm)DockPanel.FindForm();
            frmMain.SetZoomOff();
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
            EditWorkingTables ewt = new EditWorkingTables(dataSet, visibleTables);
            DialogResult result = ewt.ShowDialog();

            if (result == DialogResult.OK)
            {
                SetVisibleTables(ewt.VisibleTables);
                LoadDataSet(ewt.WorkDataSet);
            }
        }

        private void collapseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Elements elms = model1.SelectedElements(typeof(Table));

            foreach (string el in elms.Keys)
            {
                Shape table = (Shape) elms[el];
                SelectCollapse sc = new SelectCollapse(dataSet, table.Key, visibleTables);
                DialogResult result = DialogResult.OK;
                
                if (sc.RelationCount > 1)
                    result = sc.ShowDialog();
    
                if (result == DialogResult.OK)
                {
                    Collapse c = new Collapse(dataSet, table.Key, sc.SelectedRelations, visibleTables);
                    LoadDataSet(c.GetResult());
                }
            }
        }

        private void aggregateToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Elements elms = model1.SelectedElements(typeof(Table));

            foreach (string el in elms.Keys)
            {
                Shape table = (Shape)elms[el];
                if (entityTypes[table.Key].Equals(EntityTypes.TransactionEntity))
                {
                    SelectAggregate sa = new SelectAggregate(dataSet, table.Key);
                    DialogResult result = sa.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        entityTypes[sa.ResultTable] = EntityTypes.TransactionEntity;
                        visibleTables.Add(sa.ResultTable);
                        LoadDataSet(sa.Result);
                    }
                }
            }
        }

        private void createFactTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Elements elms = model1.SelectedElements(typeof(Table));

            foreach (string el in elms.Keys)
            {
                Shape table = (Shape)elms[el];
                if (entityTypes[table.Key].Equals(EntityTypes.TransactionEntity))
                {
                    CreateFactTable cft = new CreateFactTable(dataSet, table.Key, entityTypes, visibleTables);
                    entityTypes = cft.DicEntityTypes;
                    visibleTables = cft.VisibleTables;
                    LoadDataSet(cft.DataSet);
                }
            }
        }

        public void DeriveFlatSchema()
        {
            try
            {
                FlatSchema fs = new FlatSchema(dataSet, entityTypes, visibleTables);
                dataSet = fs.DeriveModel();
                visibleTables = fs.VisibleTables;
                LoadDataSet(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeriveTerracedSchema()
        {
            try
            {
                TerracedSchema ts = new TerracedSchema(dataSet, entityTypes, visibleTables);
                dataSet = ts.DeriveModel();
                visibleTables = ts.VisibleTables;
                LoadDataSet(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeriveStarSchema()
        {
            try
            {
                StarSchema ss = new StarSchema(dataSet, entityTypes, visibleTables);
                dataSet = ss.DeriveModel();
                visibleTables = ss.VisibleTables;
                entityTypes = ss.DicEntityTypes;
                LoadDataSet(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void DeriveSnowFlakeSchema()
        {
            try
            {
                SnowFlakeSchema ss = new SnowFlakeSchema(dataSet, entityTypes, visibleTables);
                dataSet = ss.DeriveModel();
                visibleTables = ss.VisibleTables;
                entityTypes = ss.DicEntityTypes;
                LoadDataSet(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeriveStarClusterSchema()
        {
            try
            {
                StarClusterSchema scs = new StarClusterSchema(dataSet, entityTypes, visibleTables);
                dataSet = scs.DeriveModel();
                visibleTables = scs.VisibleTables;
                entityTypes = scs.DicEntityTypes;
                LoadDataSet(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RefreshHierarquies()
        {
            ProjectExplorer prjExplorer = (ProjectExplorer)DockPanel.Controls.Find("Project Explorer", true)[0];
            Classification c = new Classification(dataSet, visibleTables);
            prjExplorer.RefreshHierarquies(dataSet.DataSetName, c);
            minimalEntities = c.MinimalEntities;
            maximalEntities = c.MaximalEntities;
            maximalHierarchies = c.MaximalHierarchies;
        }

        public DataSet DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }

        public SqlSchema SqlSchema
        {
            get { return sqlSchema; }
            set { sqlSchema = value; }
        }

        public Dictionary<string, EntityTypes> EntityTypesDic
        {
            get { return entityTypes; }
            set { entityTypes = value; }
        }

        public List<string> VisibleTables
        {
            get { return visibleTables; }
            set { visibleTables = value; }
        }

        public List<int> MinimalEntities
        {
            get { return minimalEntities; }
            set { minimalEntities = value; }
        }

        public List<int> MaximalEntities
        {
            get { return maximalEntities; }
            set { maximalEntities = value; }
        }

        public List<List<int>> MaximalHierarchies
        {
            get { return maximalHierarchies; }
            set { maximalHierarchies = value; }
        }

        public string DataBaseFileName
        {
            get { return dataBaseFileName; }
            set { dataBaseFileName = value; }
        }
    }
}