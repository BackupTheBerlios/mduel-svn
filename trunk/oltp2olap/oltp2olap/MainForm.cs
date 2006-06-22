using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using oltp2olap.heuristics;

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
            Classification c = new Classification();
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.SetEntityTypes(c.ClassificateEntities(frmModel.DataSet, type));
                frmModel.DrawEntityTypes();
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
    }
}