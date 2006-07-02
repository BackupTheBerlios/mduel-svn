using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap
{
    public partial class SelectCollapse : Form
    {
        public SelectCollapse(DataSet ds, string table, List<string> visible)
        {
            InitializeComponent();

            selectCollapsableRelations1.SetData(ds, table, visible);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (SelectedRelations.Count > 0)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;
        }

        public List<string> SelectedRelations
        {
            get { return selectCollapsableRelations1.SelectedRelations; }
        }

        public int RelationCount
        {
            get { return selectCollapsableRelations1.RelationCount; }
        }
    }
}