using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using oltp2olap.controls;

namespace oltp2olap
{
    public partial class SelectAggregate : Form
    {
        private DataSet dataSet;
        private string origin;
        private List<string> possibleAggregationAttributes;
        private List<string> possibleGroupingAttributes;

        public SelectAggregate(DataSet ds, string table)
        {
            InitializeComponent();

            dataSet = ds;
            origin = table;
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
            possibleAggregationAttributes = new List<string>();

            foreach (DataColumn dc in table.Columns)
            {
                if (IsForeignKey(dc) || IsPrimaryKey(dc))
                    continue;

                Type t = dc.DataType;
                if (t.Equals(typeof(System.Int16)) ||
                    t.Equals(typeof(System.Int32)) ||
                    t.Equals(typeof(System.Int64)) ||
                    t.Equals(typeof(System.Decimal)) ||
                    t.Equals(typeof(System.Double)) ||
                    t.Equals(typeof(System.Single)))
                {
                    possibleAggregationAttributes.Add(dc.ColumnName);
                }
            }
        }

        public void SetPossibleGroupingAttributes()
        {
            DataTable table = dataSet.Tables[origin].Clone();
            possibleGroupingAttributes = new List<string>();

            foreach (DataColumn dc in table.Columns)
            {
                Type t = dc.DataType;
                if (!t.Equals(typeof(System.Guid)) &&
                    !lvGrouping.Items.ContainsKey(dc.ColumnName) &&
                    !lvAggregation.Items.ContainsKey(dc.ColumnName))
                    possibleGroupingAttributes.Add(dc.ColumnName);
            }
        }

        private void SelectAggregate_Load(object sender, EventArgs e)
        {
            txtTableName.Text = "NewTable";
            txtTableName.SelectAll();
        }

        private void BuildNewTable()
        {
            if (lvGrouping.Items.Count == 0)
                return;

            DataTable table = new DataTable(txtTableName.Text);

            foreach (ListViewItem lvi in lvGrouping.Items)
            {
                string attr = lvi.Text;
                DataColumn oldC = dataSet.Tables[origin].Columns[attr];
                DataColumn dc = new DataColumn(oldC.ColumnName, oldC.DataType);
                table.Columns.Add(dc);

                List<DataColumn> pks = new List<DataColumn>(table.PrimaryKey);
                pks.Add(table.Columns[dc.ColumnName]);
                table.PrimaryKey = pks.ToArray();

                // TODO: handle FK's
             }

             foreach (ListViewItem lvi in lvAggregation.Items)
             {
                 DataColumn oldC = dataSet.Tables[origin].Columns[lvi.Text];
                 DataColumn dc = new DataColumn(lvi.SubItems[1].Text, oldC.DataType);
                 table.Columns.Add(dc);
             }
            if (!dataSet.Tables.Contains(table.TableName))
                dataSet.Tables.Add(table);
            else
                MessageBox.Show("J� existe uma tabela com esse nome");
        }

        private void btnAddAgg_Click(object sender, EventArgs e)
        {
            SetPossibleAggregationAttributes();
            SelectAggregationAttribute saa = new SelectAggregationAttribute(dataSet, origin, possibleAggregationAttributes);
            DialogResult result = saa.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!lvAggregation.Items.ContainsKey(saa.Attribute))
                {
                    ListViewItem item = lvAggregation.Items.Add(saa.Attribute, saa.Attribute, 0);
                    item.SubItems.Add(saa.Expression);
                }
                else
                {
                    MessageBox.Show("An attribute with the same name already exists!");
                }
            }
        }

        private void btnRemoveAgg_Click(object sender, EventArgs e)
        {
            List<ListViewItem> lista = new List<ListViewItem>();

            foreach (ListViewItem lvi in lvAggregation.SelectedItems)
                lista.Add(lvi);

            if (lista.Count > 0)
            {
                foreach (ListViewItem lvi in lista)
                {
                    lvAggregation.Items.Remove(lvi);
                }
            }
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            SetPossibleGroupingAttributes();
            SelectGroupingAttributes sga = new SelectGroupingAttributes(dataSet, origin, possibleGroupingAttributes);
            DialogResult result = sga.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (string attr in sga.Attributes)
                {
                    if (!lvGrouping.Items.ContainsKey(attr))
                        lvGrouping.Items.Add(attr, attr, 0);
                }
            }
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            List<ListViewItem> lista = new List<ListViewItem>();

            foreach (ListViewItem lvi in lvGrouping.SelectedItems)
                lista.Add(lvi);

            if (lista.Count > 0)
            {
                foreach (ListViewItem lvi in lista)
                {
                    lvGrouping.Items.Remove(lvi);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            BuildNewTable();
        }

        public DataSet Result
        {
            get { return dataSet; }
        }

        public string ResultTable
        {
            get { return txtTableName.Text; }
        }
    }
}