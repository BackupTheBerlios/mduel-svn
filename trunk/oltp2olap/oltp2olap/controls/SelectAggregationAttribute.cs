using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap.controls
{
    public partial class SelectAggregationAttribute : Form
    {
        List<string> aggregatable;

        public SelectAggregationAttribute(List<string> attributes)
        {
            InitializeComponent();

            aggregatable = attributes;
        }

        private void SelectAggregationAttribute_Load(object sender, EventArgs e)
        {
            cbAttributes.Items.Clear();
            foreach (string attr in aggregatable)
                cbAttributes.Items.Add(attr);
        }
    }
}