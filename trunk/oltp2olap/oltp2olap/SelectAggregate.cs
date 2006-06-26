using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap
{
    public partial class SelectAggregate : Form
    {
        private DataSet dataSet;
        private string origin;

        public SelectAggregate(DataSet ds, string table)
        {
            InitializeComponent();

            dataSet = ds;
            origin = table;
        }

        private void SelectAggregate_Load(object sender, EventArgs e)
        {
            aggregationControl1.SetData(dataSet, origin);
        }
    }
}