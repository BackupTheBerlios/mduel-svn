using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace oltp2olap.helpers
{
    public class Collapse
    {
        private DataSet dataSet;
        private DataTable table;
        private string[] children;
        private DataColumn[] newColumns;

        public Collapse(DataSet ds, string t)
        {
            dataSet = ds.Clone();
            table = ds.Tables[t].Clone();
        }

        private string[] GetParents()
        {
            List<DataRelation> drc = new List<DataRelation>();
            List<string> parents = new List<string>();
            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ChildTable.TableName.Equals(table.TableName))
                {
                    drc.Add(dr);
                    parents.Add(dr.ParentTable.TableName);
                }
            }

            return parents.ToArray();
        }

        private string[] GetChildren()
        {
            List<DataRelation> drc = new List<DataRelation>();
            List<string> children = new List<string>();
            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ParentTable.TableName.Equals(table.TableName))
                {
                    drc.Add(dr);
                    children.Add(dr.ChildTable.TableName);
                }
            }
            foreach (DataRelation dr in drc)
                dataSet.Relations.Remove(dr);

            return children.ToArray();
        }

        private DataColumn[] GetNewColumns()
        {
            List<DataColumn> columns = new List<DataColumn>();
            foreach (DataColumn c in table.Columns)
            {
                // discard xxx.TableName
                string tableName = table.TableName.Split('.')[1];
                DataColumn nc = new DataColumn(tableName + "_" + c.ColumnName, c.DataType);
                columns.Add(nc);
            }
            return columns.ToArray();
        }

        private void RemoveFKConstraints(string child)
        {
            List<Constraint> lc = new List<Constraint>();
            foreach (Constraint c in dataSet.Tables[child].Constraints)
            {
                if (c.GetType().Equals(typeof(ForeignKeyConstraint)))
                {
                    ForeignKeyConstraint fkc = (ForeignKeyConstraint)c;
                    if (fkc.RelatedTable.TableName.Equals(table.TableName))
                        lc.Add(fkc);
                }
            }
            foreach (ForeignKeyConstraint c in lc)
            {
                dataSet.Tables[child].Constraints.Remove(c);
                foreach (DataColumn dc in c.Columns)
                {
                    List<DataColumn> pk = new List<DataColumn>(c.Table.PrimaryKey);
                    if (!pk.Contains(dc))
                        dataSet.Tables[child].Columns.Remove(dc.ColumnName);
                }
            }
        }

        public DataSet GetResult()
        {
            if (GetParents().Length != 0)
                return dataSet;

            children = GetChildren();
            if (children.Length == 0)
                return dataSet;

            newColumns = GetNewColumns();

            foreach (string child in children)
            {
                RemoveFKConstraints(child);
                // handle name clashes??
                foreach (DataColumn dc in newColumns)
                {
                    if (!dataSet.Tables[child].Columns.Contains(dc.ColumnName))
                    {
                        DataColumn newDc = new DataColumn(dc.ColumnName, dc.DataType);
                        dataSet.Tables[child].Columns.Add(newDc);
                    }
                }
            }
            dataSet.Tables.Remove(table.TableName);
            /*
            List<DataRelation> drc = new List<DataRelation>();
            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ParentTable.TableName.Equals(table2.TableName)
                    && dr.ChildTable.TableName.Equals(table1.TableName))
                {
                    drc.Add(dr);
                    continue;
                }

                if (dr.ParentTable.TableName.Equals(table1.TableName)
                    && dr.ChildTable.TableName.Equals(table2.TableName))
                {
                    drc.Add(dr);
                    continue;
                }

                if (dr.ParentTable.TableName.Equals(table1.TableName)
                    || dr.ChildTable.TableName.Equals(table1.TableName))
                {
                    drc.Add(dr);
                    continue;
                }
            }
            
            foreach (DataRelation dr in drc)
            {
                dataSet.Relations.Remove(dr);
                DataTable t = dr.ChildTable;
                List<Constraint> lc = new List<Constraint>();
                foreach (Constraint c in t.Constraints)
                {
                    if (c.GetType() == typeof(ForeignKeyConstraint))
                    {
                        ForeignKeyConstraint fkc = (ForeignKeyConstraint)c;
                        if (fkc.RelatedTable.TableName.Equals(table1.TableName))
                            lc.Add(c);
                    }
                }
                foreach (Constraint c in lc)
                    t.Constraints.Remove(c);
            }
            DataTable copy = table1.Clone();
            dataSet.Tables.Remove(table1.TableName);

            foreach (DataColumn col in copy.Columns)
            {
                if (!table2.Columns.Contains(col.ColumnName))
                {
                    DataColumn c = new DataColumn(col.ColumnName, col.DataType);
                    dataSet.Tables[table2.TableName].Columns.Add(c);
                }
                    
            }
            */
            return dataSet;
        }
    }
}
