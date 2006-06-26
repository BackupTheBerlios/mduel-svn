using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap.controls
{
    public partial class AggregationControl : UserControl
    {
        private DataSet dataSet;
        private string origin;
        private List<string> possibleAggreationAttributes;
        private Dictionary<string, string> aggregationAttributes;
        private List<string> possibleGroupingAttributes;
        private List<string> groupingAttributes;

        public AggregationControl()
        {
            InitializeComponent();
        }

        public void SetData(DataSet ds, string table)
        {
            InitializeComponent();

            this.dataSet = ds;
            this.origin = table;
        }

         public bool IsPrimaryKey(DataColumn dc)
        {
             foreach (DataColumn c in dc.Table.PrimaryKey)
             {
                 if (c.ColumnName.Equals(dc.ColumnName))
                     return true;
             }
            return false;
        }

        public bool IsForeignKey(DataColumn dc)
        {
            foreach (DataRelation dr in dataSet.Relations)
            {
                foreach (DataColumn c1 in dr.ChildColumns)
                {
                    if (c1.ColumnName.Equals(dc.ColumnName))
                        return true;
                }
            }
            return false;
        }

        public void SetPossibleAggregationAttributes()
        {
            DataTable table = dataSet.Tables[origin].Clone();
            possibleAggreationAttributes = new List<string>();

            foreach (DataColumn dc in table.Columns)
            {
                Type t = dc.DataType;
                if (t.Equals(typeof(System.Int16)) ||
                    t.Equals(typeof(System.Int32)) ||
                    t.Equals(typeof(System.Int64)) ||
                    t.Equals(typeof(System.Decimal)) ||
                    t.Equals(typeof(System.Double)) ||
                    t.Equals(typeof(System.Single)))
                {
                    if (!IsPrimaryKey(dc) && !IsForeignKey(dc))
                        possibleAggreationAttributes.Add(dc.ColumnName);
                }
            }
        }

        private void AggregationControl_Load(object sender, EventArgs e)
        {
            txtTableName.Text = "New Table";
            txtTableName.SelectAll();
        }

        private void btnAddAgg_Click(object sender, EventArgs e)
        {
            SetPossibleAggregationAttributes();
            SelectAggregationAttribute saa = new SelectAggregationAttribute(possibleAggreationAttributes);
            DialogResult result = saa.ShowDialog();

            if (result == DialogResult.OK)
            {
            }
        }
    }
}
