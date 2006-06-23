using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace oltp2olap.helpers
{

    public class Collapse
    {
        private class DataColumnInfo
        {
            public string OldName;
            public string NewName;
            public Type DataType;
            public bool IsPK = false;
        }

        private DataSet dataSet;
        private DataTable table;
        private string[] children;
        private DataColumnInfo[] newColumns;

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

        private DataColumnInfo[] GetNewColumns()
        {
            List<DataColumnInfo> columns = new List<DataColumnInfo>();
            foreach (DataColumn c in table.Columns)
            {
                string tableName = table.TableName.Split('.')[1];
                string newName = tableName + "_" + c.ColumnName;
                DataColumnInfo nc = new DataColumnInfo();
                nc.NewName = newName;
                nc.OldName = c.ColumnName;
                nc.DataType = c.DataType;
                columns.Add(nc);
            }
            return columns.ToArray();
        }

        private void GetPossiblePrimaryKeys(string child)
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
                foreach (DataColumnInfo dci in newColumns)
                {
                    List<DataColumn> pk = new List<DataColumn>(c.Table.PrimaryKey);
                    foreach(DataColumn dc in pk)
                    {
                        if (dc.ColumnName.Equals(dci.OldName))
                            dci.IsPK = true;
                    }
                }
            }
        }

        private void SetNewPrimaryKeys(DataSet dataSet, string child)
        {
            foreach (DataColumnInfo dci in newColumns)
            {
                if (dci.IsPK)
                {
                    List<DataColumn> pkCols = new List<DataColumn>(dataSet.Tables[child].PrimaryKey);
                    pkCols.Add(dataSet.Tables[child].Columns[dci.NewName]);
                    dataSet.EnforceConstraints = false;
                    dataSet.Tables[child].PrimaryKey = pkCols.ToArray();
                    dataSet.EnforceConstraints = true;
                }
            }
        }

        private bool RemoveFKConstraints(string child)
        {
            bool nullable = false;

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
                    if (pk.Contains(dc))
                    {
                        pk.Remove(dc);
                        c.Table.DataSet.EnforceConstraints = false;
                        c.Table.DataSet.Tables[child].PrimaryKey = pk.ToArray();
                        c.Table.DataSet.EnforceConstraints = true;
                    }
                    if (dc.AllowDBNull)
                        nullable = true;
                    dataSet.Tables[child].Columns.Remove(dc.ColumnName);
                }
            }
            return nullable;
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
                GetPossiblePrimaryKeys(child);
                bool nullable = RemoveFKConstraints(child);
                foreach (DataColumnInfo dci in newColumns)
                {
                    DataColumn newDc = new DataColumn(dci.NewName, dci.DataType);
                    if (nullable)
                        newDc.AllowDBNull = true;
                    else
                        newDc.AllowDBNull = false;

                    if (dataSet.Tables[child].Columns.Contains(dci.NewName))
                    {
                        string newName = dci.OldName + "_" + dci.NewName;
                        newDc.ColumnName = newName;
                        dci.NewName = newName;
                    }

                    dataSet.Tables[child].Columns.Add(newDc);
                }
                SetNewPrimaryKeys(dataSet, child);
            }
            dataSet.Tables.Remove(table.TableName);
            return dataSet;
        }
    }
}
