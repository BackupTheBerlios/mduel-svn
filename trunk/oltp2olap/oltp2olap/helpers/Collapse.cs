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
        private bool collapseAll;
        private List<DataRelation> childrenRelations;
        private List<string> collapsableRelations;
        private List<string> visibleTables;

        public Collapse(DataSet ds, string t, List<string> relations, List<string> visible)
        {
            dataSet = ds.Clone();
            table = ds.Tables[t].Clone();
            collapseAll = true;
            visibleTables = visible;

            if (relations.Count > 0)
            {
                collapseAll = false;
                collapsableRelations = relations;
            }
        }

        private string[] GetParents()
        {
            List<DataRelation> drc = new List<DataRelation>();
            List<string> parents = new List<string>();
            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ChildTable.TableName.Equals(table.TableName) && visibleTables.Contains(dr.ParentTable.TableName))
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
                    if (collapseAll)
                    {
                        drc.Add(dr);
                    }
                    else if (collapsableRelations.Contains(dr.RelationName))
                    {
                        drc.Add(dr);
                    }

                    if (visibleTables.Contains(dr.ChildTable.TableName))
                        children.Add(dr.ChildTable.TableName);
                }

                if (dr.ChildTable.TableName.Equals(table.TableName))
                    drc.Add(dr);
            }
            childrenRelations = drc;
            foreach (DataRelation dr in drc)
                dataSet.Relations.Remove(dr);

            return children.ToArray();
        }

        private DataColumnInfo[] GetNewColumns()
        {
            List<DataColumnInfo> columns = new List<DataColumnInfo>();
            foreach (DataColumn c in dataSet.Tables[table.TableName].Columns)
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

        private bool RemoveFKConstraints(string child, string relationName)
        {
            bool nullable = false;

            List<Constraint> lc = new List<Constraint>();
            foreach (Constraint c in dataSet.Tables[child].Constraints)
            {
                if (c.GetType().Equals(typeof(ForeignKeyConstraint)))
                {
                    ForeignKeyConstraint fkc = (ForeignKeyConstraint)c;
                    if (fkc.RelatedTable.TableName.Equals(table.TableName))
                    {
                        if (fkc.ConstraintName.Equals(relationName))
                            lc.Add(fkc);
                    }
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
            string[] parents = GetParents();
            if (GetParents().Length != 0)
            {
                foreach (string str in parents)
                {
                    if (dataSet.Tables[str] != null)
                    {
                        Collapse c = new Collapse(dataSet, str, new List<string>(), visibleTables);
                        dataSet = c.GetResult();
                    }
                }
            }

            children = GetChildren();
            if (children.Length == 0)
                return dataSet;

            newColumns = GetNewColumns();

            foreach (DataRelation relation in childrenRelations)
            {
                if (!collapseAll && !collapsableRelations.Contains(relation.RelationName))
                    continue;

                string child = relation.ChildTable.TableName;
                GetPossiblePrimaryKeys(child);
                bool nullable = RemoveFKConstraints(child, relation.RelationName);
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
            dataSet.Tables[table.TableName].Constraints.Clear();
            if (collapseAll)
                dataSet.Tables.Remove(table.TableName);
            else if (collapsableRelations.Count == children.Length)
                dataSet.Tables.Remove(table.TableName);

            return dataSet;
        }
    }
}
