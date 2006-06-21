using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using oltp2olap.wizards;
using WeifenLuo.WinFormsUI;

namespace oltp2olap
{
    public partial class ProjectExplorer : DockContent
    {
        private Dictionary<string, DataSet> dataSets;

        public Dictionary<string, DataSet> DataSets
        {
            get { return dataSets; }
        }

        public ProjectExplorer()
        {
            InitializeComponent();

            Name = "Project Explorer";

            dataSets = new Dictionary<string, DataSet>();
        }

        private void ProjectExplorer_Load(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ctxMnuBase.Show(treeView1, e.Location);
            }
        }

        private void newDataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionWizard cwz = new ConnectionWizard();

            if (cwz.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = cwz.GetDataSet();
                dataSets[ds.DataSetName] = ds.Clone();
                DataSet workspace = new DataSet(ds.DataSetName);
                MainForm mf = (MainForm) FindForm().ParentForm;
                ModelForm frmModel = new ModelForm();
                frmModel.Closed += new EventHandler(frmModel_Closed);

                EditWorkingTables ewt = new EditWorkingTables(ds, workspace);
                DialogResult result = ewt.ShowDialog();

                frmModel.LoadDataSet(ewt.WorkDataSet);
               
                frmModel.Show(mf.DockPanel);
                treeView1.Nodes[0].Nodes.Add(ds.DataSetName);
                treeView1.Nodes[0].Expand();                
            }
        }

        // TODO: what the fuck? find does not work?!
        void frmModel_Closed(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm) sender;
            TreeNode[] nodes = treeView1.Nodes.Find(frmModel.Text, true);
            if (nodes.Length > 0)
                nodes[0].Remove();
        }
    }
}