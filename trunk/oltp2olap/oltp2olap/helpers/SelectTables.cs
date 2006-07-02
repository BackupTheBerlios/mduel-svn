using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace oltp2olap.controls
{
    public partial class SelectTables : UserControl
    {
        private DataSet wsDataSet;
        private List<string> visibleTables;

        public SelectTables()
        {
            InitializeComponent();

            wsDataSet = null;
            visibleTables = null;
        }

        public void SetTables(DataSet ds, List<string> visible)
        {
            wsDataSet = ds.Clone();
            visibleTables = visible;
        }

        private string[] GetParents(string table)
        {
            List<DataRelation> drc = new List<DataRelation>();
            List<string> parents = new List<string>();
            foreach (DataRelation dr in wsDataSet.Relations)
            {
                if (dr.ChildTable.TableName.Equals(table))
                {
                    drc.Add(dr);
                    parents.Add(dr.ParentTable.TableName);
                }
            }

            return parents.ToArray();
        }

        List<string> related = new List<string>();
        private void RelatedTables(string table)
        {
            string[] parents = GetParents(table);
            foreach (string p in parents)
            {
                if (related.Contains(p))
                    continue;

                related.Add(p);
                RelatedTables(p);
            }
        }
        
        private void SelectTables_Load(object sender, System.EventArgs e)
        {
            if (wsDataSet == null || wsDataSet.Tables == null)
                return;

            lbDbTables.Items.Clear();
            foreach(DataTable table in wsDataSet.Tables)
            {
                if (!visibleTables.Contains(table.TableName))
                    lbDbTables.Items.Add(table.TableName);
            }
            if (lbDbTables.Items.Count > 0)
                btnIntoWorkspace.Enabled = true;

            lbWsTables.Items.Clear();
            foreach (DataTable table in wsDataSet.Tables)
            {
                if (!lbDbTables.Items.Contains(table.TableName) && visibleTables.Contains(table.TableName))
                    lbWsTables.Items.Add(table.TableName);
            }
            if (lbWsTables.Items.Count > 0)
                btnOutWorkspace.Enabled = true;
        }

        private void btnIntoWorkspace_Click(object sender, System.EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (object obj in lbDbTables.SelectedItems)
            {
                selection.Add((string)obj);
                if (cbRelatedTables.Checked)
                {
                    RelatedTables((string)obj);
                    selection.AddRange(related);
                }
            }

            foreach (string item in selection)
            {
                if (!lbWsTables.Items.Contains(item))
                {
                    visibleTables.Add(item);
                    lbWsTables.Items.Add(item);
                    lbDbTables.Items.Remove(item);
                }
            }

            ToggleButtons();
        }

        private void btnOutWorkspace_Click(object sender, System.EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (object obj in lbWsTables.SelectedItems)
                selection.Add((string)obj);

            foreach (string item in selection)
            {
                if (!lbDbTables.Items.Contains(item))
                {
                    visibleTables.Remove(item);
                    lbDbTables.Items.Add(item);
                    lbWsTables.Items.Remove(item);
                }
            }

            ToggleButtons();
        }

        private void ToggleButtons()
        {
            if (lbDbTables.Items.Count == 0)
                btnIntoWorkspace.Enabled = false;
            else
                btnIntoWorkspace.Enabled = true;

            if (lbWsTables.Items.Count == 0)
                btnOutWorkspace.Enabled = false;
            else
                btnOutWorkspace.Enabled = true;
        }

        public DataSet WorkDataSet
        {
            get { return wsDataSet; }
        }

        public List<string> VisibleTables
        {
            get { return visibleTables; }
        }
    }
}
