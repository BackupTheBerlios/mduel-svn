using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using oltp2olap.helpers;
using oltp2olap.heuristics;
using System.Collections.Generic;

namespace oltp2olap
{
    public partial class MainForm : Form
    {
        private ProjectExplorer prjExplorer = new ProjectExplorer();
        private Timer timer = new Timer();

        public MainForm()
        {
            InitializeComponent();

#if DEBUG
            timer.Enabled = false;
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
#endif
        }

#if DEBUG
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            prjExplorer.Go();
        }
#endif

        private void MainForm_Load(object sender, EventArgs e)
        {
            prjExplorer.MdiParent = this;
            prjExplorer.Show(dockPanel1);

            cbZoom.SelectedItem = "100%";
            cbZoom.Enabled = false;

            timer.Enabled = true;
        }

        private void mnuZoom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string zoom = ((ToolStripComboBox)sender).Text.TrimEnd('%');
            int iZoom = System.Int32.Parse(zoom);
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
                frmModel.SetZoom(iZoom);
        }

        private void ClassifyEntities(ClassificationTypes type)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.ClassifyEntities(type);
            }
        }

        public void ToggleZoom()
        {
            cbZoom.Enabled = !cbZoom.Enabled;
        }

        private void classify_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            if (menu.Text == "Algorithm #1")
                ClassifyEntities(ClassificationTypes.AlgorithmNumber1);
            else if (menu.Text == "Algorithm #2")
                ClassifyEntities(ClassificationTypes.AlgorithmNumber2);
        }

        public DockPanel DockPanel
        {
            get { return dockPanel1; }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            IDockContent dock = dockPanel1.ActiveDocument;
            if (dock == null)
                return;

            if (dock.GetType().Equals(typeof(ModelForm)))
            {
                ModelForm frmModel = (ModelForm)dock;
                SqlSchema sqlSchema = frmModel.SqlSchema;
                MessageBox.Show(sqlSchema.Database);
            }
        }

        private void flatSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveFlatSchema();
            }
        }

        private void terracedSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveTerracedSchema();
            }
        }

        private void starSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveStarSchema();
            }
        }

        private void snowflakeSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveSnowFlakeSchema();
            }
        }

        private void starClusterSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveStarClusterSchema();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}