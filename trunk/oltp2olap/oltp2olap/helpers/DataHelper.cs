using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using oltp2olap.heuristics;

namespace oltp2olap.helpers
{
    public class DataHelper
    {
        public static bool IsPrimaryKey(DataColumn dc)
        {
            foreach (DataColumn c in dc.Table.PrimaryKey)
            {
                if (c.ColumnName.Equals(dc.ColumnName))
                    return true;
            }
            return false;
        }

        public static bool IsForeignKey(DataSet dataSet, DataColumn dc)
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

        public static bool IsForeignKeyToComponent(DataSet dataSet, DataColumn dc, Dictionary<string, EntityTypes> entityTypes)
        {
            foreach (DataRelation dr in dataSet.Relations)
            {
                foreach (DataColumn c1 in dr.ChildColumns)
                {
                    if (entityTypes.ContainsKey(dr.ParentTable.TableName))
                    {
                        if (c1.ColumnName.Equals(dc.ColumnName) &&
                            entityTypes[dr.ParentTable.TableName].Equals(EntityTypes.ComponentEntity))
                            return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckForColumns(string origin, DataTable table, DataColumn[] cols)
        {
            foreach (DataColumn c in table.Columns)
                foreach (DataColumn cc in cols)
                    if (cc.ColumnName.Equals(c.ColumnName) && cc.Table.TableName.Equals(origin))
                        return true;

            return false;
        }

        public static DataRelation NewChildFKRelation(DataTable table, DataRelation dr)
        {
            List<DataColumn> cols = new List<DataColumn>();
            foreach (DataColumn dc in dr.ChildColumns)
            {
                if (table.Columns[dc.ColumnName] != null)
                    cols.Add(table.Columns[dc.ColumnName]);
            }

            if (dr.ParentColumns.Length != cols.Count)
                return null;

            DataRelation newDR = new DataRelation(
                dr.RelationName + "_Child" + table.TableName,
                dr.ParentColumns,
                cols.ToArray()
                );

            return newDR;
        }

        public static DataRelation NewParentFKRelation(DataTable table, DataRelation dr)
        {
            List<DataColumn> cols = new List<DataColumn>();
            foreach (DataColumn dc in dr.ParentColumns)
            {
                if (table.Columns[dc.ColumnName] != null)
                    cols.Add(table.Columns[dc.ColumnName]);
            }

            if (dr.ParentColumns.Length != cols.Count)
                return null;

            DataRelation newDR = new DataRelation(
                dr.RelationName + "_Parent" + table.TableName,
                cols.ToArray(),
                dr.ChildColumns
                );

            return newDR;
        }

        public static List<DataRelation> GetRelations(DataSet dataSet, string table)
        {
            List<DataRelation> relations = new List<DataRelation>();

            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ParentTable.TableName.Equals(table) ||
                    dr.ChildTable.TableName.Equals(table))
                    relations.Add(dr);
            }

            return relations;
        }

        public static List<DataRelation> GetChildRelations(DataSet dataSet, string table)
        {
            List<DataRelation> relations = new List<DataRelation>();

            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ParentTable.TableName.Equals(table))
                    relations.Add(dr);
            }

            return relations;
        }

        public static List<DataRelation> GetRelationsBetween(DataSet dataSet, string table1, string table2)
        {
            List<DataRelation> relations = new List<DataRelation>();

            foreach (DataRelation dr in dataSet.Relations)
            {
                if ((dr.ParentTable.TableName.Equals(table1) &&
                    dr.ChildTable.TableName.Equals(table2)) ||
                    (dr.ParentTable.TableName.Equals(table2) &&
                    dr.ChildTable.TableName.Equals(table1)))
                    relations.Add(dr);
            }

            return relations;
        }


        public static string[] GetParentTransactionEntities(DataSet dataSet, string table, Dictionary<string, EntityTypes> entityTypes, List<string> visibleTables)
        {
            List<string> parents = new List<string>();
            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ChildTable.TableName.Equals(table) &&
                    visibleTables.Contains(dr.ParentTable.TableName) &&
                    entityTypes[dr.ParentTable.TableName].Equals(EntityTypes.TransactionEntity))
                {
                    parents.Add(dr.ParentTable.TableName);
                }
            }

            return parents.ToArray();
        }

        public static void RemoveRelations(DataSet dataSet, string table)
        {
            List<DataRelation> relations = GetRelations(dataSet, table);

            foreach (DataRelation dr in relations)
                dataSet.Relations.Remove(dr);
        }

        public static void RestoreRelations(DataSet dataSet, List<DataRelation> relations)
        {
            foreach (DataRelation dr in relations)
                dataSet.Relations.Add(dr);
        }
    }
}
