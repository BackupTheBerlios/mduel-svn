using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using oltp2olap.wizards;
using WeifenLuo.WinFormsUI;
using oltp2olap.heuristics;

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

        public void RefreshHierarquies(string dataset, Classification c)
        {
            c.CalculateHierarquies();
            treeView1.Nodes[0].Nodes[0].Nodes.Clear();

            TreeNode minimal = treeView1.Nodes[0].Nodes[0].Nodes.Add("Minimal Entities");
            foreach (string str in c.MinimalEntities)
                minimal.Nodes.Add(str);

            TreeNode maximal = treeView1.Nodes[0].Nodes[0].Nodes.Add("Maximal Entities");
            foreach (string str in c.MaximalEntities)
                maximal.Nodes.Add(str);

            List<LinkedList<string>> maximalHierarchies = c.MaximalStringHierarchies;

            int count = 1;
            TreeNode maxNode = treeView1.Nodes[0].Nodes[0].Nodes.Add("Maximal Hierarchies");
            foreach (LinkedList<string> hierarchy in maximalHierarchies)
            {
                TreeNode node = maxNode.Nodes.Add("Maximal Hierarchy #" + count++);

                foreach (string table in hierarchy)
                    node.Nodes.Add(table);
            }

            treeView1.ExpandAll();
            maxNode.Collapse();
        }

        private void ProjectExplorer_Load(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right &&
                treeView1.Nodes[0].Nodes.Count == 0)
            {
                ctxMnuBase.Show(treeView1, e.Location);
            }
        }

        private void newDataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionWizard cwz = new ConnectionWizard();
            DialogResult cwzResult = cwz.ShowDialog();

            if (cwzResult == DialogResult.OK)
            {
                DataSet ds = cwz.GetDataSet();
                dataSets[ds.DataSetName] = ds.Clone();
                MainForm mf = (MainForm) FindForm().ParentForm;
                ModelForm frmModel = new ModelForm();
                frmModel.SqlSchema = cwz.SqlSchema;
                frmModel.Closed += new EventHandler(frmModel_Closed);

                EditWorkingTables ewt = new EditWorkingTables(ds, new List<string>());
                DialogResult result = ewt.ShowDialog();
                if (result == DialogResult.OK)
                {
                    treeView1.Nodes[0].Nodes.Add(ds.DataSetName);
                    treeView1.Nodes[0].Expand();
                    frmModel.Show(mf.DockPanel);

                    frmModel.SetVisibleTables(ewt.VisibleTables);
                    frmModel.LoadDataSet(ewt.WorkDataSet);
                }
            }
        }

        // TODO: what the fuck? find does not work?!
        void frmModel_Closed(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Nodes.Clear();
        }

#if DEBUG
        public void Go()
        {
            newDataSourceToolStripMenuItem_Click(null, null);
        }
#endif
    }
}