using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace oltp2olap.controls
{
    public partial class SelectTables : UserControl
    {
        private DataSet dbDataSet;
        private DataSet wsDataSet;

        public SelectTables()
        {
            InitializeComponent();

            dbDataSet = null;
            wsDataSet = null;
        }

        public void SetTables(DataSet orig, DataSet dest)
        {
            dbDataSet = orig.Clone();
            wsDataSet = dest.Clone();
        }
        
        public void CopySelectedTables()
        {
            wsDataSet = new DataSet(dbDataSet.DataSetName);
            foreach (string str in lbWsTables.Items)
            {
                wsDataSet.Tables.Add(dbDataSet.Tables[str].Clone());
            }

            foreach (DataRelation dr in dbDataSet.Relations)
            {
                if (wsDataSet.Tables.Contains(dr.ParentTable.TableName) && wsDataSet.Tables.Contains(dr.ChildTable.TableName))
                {
                    string parent = dr.ParentTable.TableName;
                    string child = dr.ChildTable.TableName;
                    string parentCol = dr.ParentColumns[0].ColumnName;
                    string childCol = dr.ChildColumns[0].ColumnName;

                    DataColumn childData = wsDataSet.Tables[child].Columns[childCol];
                    DataColumn parentData = wsDataSet.Tables[parent].Columns[parentCol];

                    DataRelation relation = new DataRelation(dr.RelationName, parentData, childData);
                    if (!wsDataSet.Relations.Contains(dr.RelationName))
                        wsDataSet.Relations.Add(relation);
                }
            }
        }
        
        private void SelectTables_Load(object sender, System.EventArgs e)
        {
            if (dbDataSet == null || dbDataSet.Tables == null)
                return;

            if (wsDataSet == null || wsDataSet.Tables == null)
                return;

            lbDbTables.Items.Clear();
            foreach(DataTable table in dbDataSet.Tables)
            {
                if (!wsDataSet.Tables.Contains(table.TableName))
                    lbDbTables.Items.Add(table.TableName);
            }
            if (lbDbTables.Items.Count > 0)
            {
                btnIntoWorkspace.Enabled = true;
                lbDbTables.SelectedIndex = 0;
            }

            lbWsTables.Items.Clear();
            foreach (DataTable table in wsDataSet.Tables)
            {
                if (!lbDbTables.Items.Contains(table.TableName))
                    lbWsTables.Items.Add(table.TableName);
            }
            if (lbWsTables.Items.Count > 0)
            {
                btnOutWorkspace.Enabled = true;
                lbWsTables.SelectedIndex = 0;
            }

#if DEBUG
            while (lbDbTables.Items.Count > 0)
                btnIntoWorkspace_Click(this, null);
#endif
        }

        private void btnIntoWorkspace_Click(object sender, System.EventArgs e)
        {
            if (cbRelatedTables.Checked)
            {
                //TODO: do something
            }
            else
            {
                int idx = lbWsTables.Items.Add(lbDbTables.SelectedItem);
                lbWsTables.SelectedIndex = idx;
                lbDbTables.Items.Remove(lbDbTables.SelectedItem);
                if (lbDbTables.Items.Count > 0)
                    lbDbTables.SelectedIndex = 0;
            }

            ToggleButtons();
        }

        private void btnOutWorkspace_Click(object sender, System.EventArgs e)
        {
            int idx = lbDbTables.Items.Add(lbWsTables.SelectedItem);
            lbDbTables.SelectedIndex = idx;
            lbWsTables.Items.Remove(lbWsTables.SelectedItem);
            if (lbWsTables.Items.Count > 0)
                lbWsTables.SelectedIndex = 0;

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
    }
}
