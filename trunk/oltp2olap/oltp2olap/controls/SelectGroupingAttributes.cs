using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap.controls
{
    public partial class SelectGroupingAttributes : Form
    {
        private DataSet dataSet;
        private string origin;
        private List<string> groupable;
        private List<string> attributes;

        public SelectGroupingAttributes(DataSet ds, string table, List<string> attributes)
        {
            InitializeComponent();

            dataSet = ds;
            origin = table;
            groupable = attributes;
        }

        public List<string> Attributes
        {
            get { return attributes; }
        }

        private void SelectGroupingAttributes_Load(object sender, EventArgs e)
        {
            foreach (string attr in groupable)
                lbGroupBy.Items.Add(attr);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            attributes = new List<string>();
            foreach (string attr in lbGroupBy.SelectedItems)
            {
                attributes.Add(attr);
            }
        }
    }
}