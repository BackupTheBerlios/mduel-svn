using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap.controls
{
    public partial class SelectCollapsableRelations : UserControl
    {
        private DataSet dataSet;
        private string tableName;
        private int relationCount = 0;
        private List<string> visibleTables;

        public SelectCollapsableRelations()
        {
            InitializeComponent();

            dataSet = null;
            tableName = null;
        }

        public void SetData(DataSet ds, string table, List<string> visible)
        {
            dataSet = ds;
            tableName = table;
            visibleTables = visible;

            LoadRelations();
        }

        private void LoadRelations()
        {
            if (dataSet == null || dataSet.Tables == null)
                return;

            List<Dictionary<string, string>> tableRelations = new List<Dictionary<string, string>>();

            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ParentTable.TableName.Equals(tableName) && visibleTables.Contains(dr.ChildTable.TableName))
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    dss[dr.ChildTable.TableName] = dr.RelationName;
                    tableRelations.Add(dss);

                    lbRelations.Items.Add(dr.ChildTable + "-" + dr.RelationName);
                }
            }

            if (lbRelations.Items.Count > 0)
            {
                btnInto.Enabled = true;
                relationCount = lbRelations.Items.Count;
            }

        }

        private void ToggleButtons()
        {
            if (lbRelations.Items.Count == 0)
                btnInto.Enabled = false;
            else
                btnInto.Enabled = true;

            if (lbTargets.Items.Count == 0)
                btnOut.Enabled = false;
            else
                btnOut.Enabled = true;
        }

        private void btnInto_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (object obj in lbRelations.SelectedItems)
                selection.Add((string)obj);

            foreach (string item in selection)
            {
                if (!lbTargets.Items.Contains(item))
                {
                    lbTargets.Items.Add(item);
                    lbRelations.Items.Remove(item);
                }
            }

            ToggleButtons();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (object obj in lbTargets.SelectedItems)
                selection.Add((string)obj);

            foreach (string item in selection)
            {
                if (!lbRelations.Items.Contains(item))
                {
                    lbRelations.Items.Add(item);
                    lbTargets.Items.Remove(item);
                }
            }

            ToggleButtons();
        }

        public int RelationCount
        {
            get { return relationCount; }
        }

        public List<string> SelectedRelations
        {
            get
            {
                List<string> relations = new List<string>();
                foreach (object obj in lbTargets.Items)
                {
                    string item = (string)obj;
                    item = item.Split('-')[1];
                    relations.Add(item);
                }
                return relations;
            }
        }
    }
}
