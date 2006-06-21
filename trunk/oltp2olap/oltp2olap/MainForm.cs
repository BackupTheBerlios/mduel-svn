using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using oltp2olap.heuristics;

namespace oltp2olap
{
    public partial class MainForm : Form
    {
        private ProjectExplorer prjExplorer = new ProjectExplorer();

        public DockPanel DockPanel
        {
            get { return dockPanel1; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            prjExplorer.MdiParent = this;
            prjExplorer.Show(dockPanel1);

            cbZoom.SelectedItem = "100%";
            cbZoom.Enabled = false;
        }

        private void mnuZoom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string zoom = ((ToolStripComboBox)sender).Text.TrimEnd('%');
            int iZoom = System.Int32.Parse(zoom);
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
                frmModel.SetZoom(iZoom);
        }

        private void ClassifyEntities()
        {
            Classification c = new Classification();
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.SetEntityTypes(c.ClassificateEntities(frmModel.DataSet));
                frmModel.DrawEntityTypes();
            }
        }

        public void ToggleZoom()
        {
            cbZoom.Enabled = !cbZoom.Enabled;
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassifyEntities();
        }
    }
}