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
        private DataSet dataSet;
        private string origin;
        private List<string> aggregatable;
        private string columnName;
        private string attribute;
        private string expression;

        public SelectAggregationAttribute(DataSet ds, string table, List<string> attributes)
        {
            InitializeComponent();

            dataSet = ds;
            origin = table;
            aggregatable = attributes;
            CheckOptions();
        }

        private void CheckOptions()
        {
            btnOk.Enabled = false;
            if (cbAttributes.SelectedItem != null)
            {
                if (rbOperation.Checked)
                {
                    if (cbOperations.SelectedItem != null)
                        btnOk.Enabled = true;

                    cbOperations.Enabled = true;
                    txtExpression.Enabled = false;
                }
                else if (rbExpression.Checked)
                {
                    if (txtExpression.Text.Length > 0)
                        btnOk.Enabled = true;

                    txtExpression.Enabled = true;
                    cbOperations.Enabled = false;
                }
                else if (rbAsIs.Checked)
                {
                    txtExpression.Enabled = false;
                    cbOperations.Enabled = false;
                    btnOk.Enabled = true;
                }
            }
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

        private void SelectAggregationAttribute_Load(object sender, EventArgs e)
        {
            cbAttributes.Items.Clear();
            foreach (string attr in aggregatable)
                cbAttributes.Items.Add(attr);
        }

        private void SelectedIndex_Changed(object senders, EventArgs e)
        {
            if (senders.GetType().Equals(typeof(ComboBox)))
            {
                ComboBox cb = (ComboBox)senders;
                if (cb.Name == "cbAttributes" && cbAttributes.SelectedItem != null)
                {
                    DataColumn dc = dataSet.Tables[origin].Columns[cbAttributes.SelectedItem.ToString()];
                    if (IsForeignKey(dc) || IsPrimaryKey(dc))
                    {
                        rbOperation.Enabled = false;
                        cbOperations.Enabled = false;
                        rbExpression.Enabled = false;
                        txtExpression.Enabled = false;
                        rbAsIs.Checked = true;
                    }
                    else
                    {
                        rbOperation.Enabled = true;
                        cbOperations.Enabled = true;
                        rbExpression.Enabled = true;
                        txtExpression.Enabled = true;
                    }
                }
            }

            if (senders.GetType().Equals(typeof(RadioButton)))
            {
                RadioButton rb = (RadioButton)senders;
                if (rb.Name == "rbExpression" && cbAttributes.SelectedItem != null)
                {
                    txtExpression.Text = cbAttributes.SelectedItem.ToString();
                }
            }
            CheckOptions();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            columnName = cbAttributes.SelectedItem.ToString();
            attribute = ColumnName;
            if (rbOperation.Checked)
                expression = cbOperations.SelectedItem.ToString().Replace("()", "(" + cbAttributes.SelectedItem.ToString() + ")");
            else if (rbExpression.Checked)
                expression = txtExpression.Text;
            else if (rbAsIs.Checked)
                expression = columnName;
        }

        public string Attribute
        {
            get { return attribute; }
        }

        public string Expression
        {
            get { return expression; }
        }

        public string ColumnName
        {
            get { return columnName; }
        }
    }
}