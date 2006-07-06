using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using oltp2olap.heuristics;

namespace oltp2olap.helpers
{
    public class CreateFactTable
    {
        private DataSet dataSet;
        private List<string> visibleTables;
        private Dictionary<string, EntityTypes> entityTypes;

        public CreateFactTable(DataSet dataSet, string table, Dictionary<string, EntityTypes> types, List<string> tables)
        {
            Dictionary<string, string> newTables = new Dictionary<string, string>();
            this.dataSet = dataSet;
            this.entityTypes = types;
            this.visibleTables = tables;

            DataTable newTable = new DataTable(table + "Fact");
            if (entityTypes[table].Equals(EntityTypes.TransactionEntity))
            {
                // copy columns
                foreach (DataColumn dc in dataSet.Tables[table].Columns)
                {
                    DataColumn newDc = new DataColumn(dc.ColumnName, dc.DataType);
                    newTable.Columns.Add(newDc);
                }

                // get a list of Parent Transaction Entities
                // & copy their FK columns to dimensions
                string[] ptt = DataHelper.GetParentTransactionEntities(dataSet, table, entityTypes, visibleTables);
                foreach (string tbl in ptt)
                {
                    foreach (DataColumn dc in dataSet.Tables[tbl].Columns)
                    {
                        if (DataHelper.IsForeignKeyToComponent(dataSet, dc, entityTypes))
                        {
                            DataColumn newDc = new DataColumn(dc.ColumnName, dc.DataType);
                            if (!newTable.Columns.Contains(newDc.ColumnName))
                                newTable.Columns.Add(newDc);

                            List<DataColumn> pks = new List<DataColumn>(newTable.PrimaryKey);
                            if (!pks.Contains(dc))
                                pks.Add(newTable.Columns[dc.ColumnName]);
                            newTable.PrimaryKey = pks.ToArray();
                        }
                    }
                }

                foreach (DataColumn dc in dataSet.Tables[table].Columns)
                {
                    if (DataHelper.IsForeignKeyToComponent(dataSet, dc, entityTypes) ||
                        DataHelper.IsPrimaryKey(dc))
                    {
                        List<DataColumn> pks = new List<DataColumn>(newTable.PrimaryKey);
                        if (!pks.Contains(dc))
                            pks.Add(newTable.Columns[dc.ColumnName]);
                        newTable.PrimaryKey = pks.ToArray();
                    }
                }

                newTables[table] = newTable.TableName;
                entityTypes[newTable.TableName] = EntityTypes.TransactionEntity;
                dataSet.Tables.Add(newTable);

                List<DataRelation> relations = DataHelper.GetRelations(dataSet, table);
                foreach (DataRelation dr in relations)
                {
                    bool child = DataHelper.CheckForColumns(table, newTable, dr.ChildColumns);
                    bool parent = DataHelper.CheckForColumns(table, newTable, dr.ParentColumns);

                    if (child)
                    {
                        DataRelation newDr = DataHelper.NewChildFKRelation(newTable, dr);
                        if (!dataSet.Relations.Contains(newDr.RelationName))
                            dataSet.Relations.Add(newDr);
                    }
                    if (parent)
                    {
                        DataRelation newDr = DataHelper.NewParentFKRelation(newTable, dr);
                        if (!dataSet.Relations.Contains(newDr.RelationName))
                            dataSet.Relations.Add(newDr);
                    }
                }

                // copy relations from parent transational table
                foreach (string tbl in ptt)
                {
                    relations = DataHelper.GetRelations(dataSet, tbl);
                    foreach (DataRelation dr in relations)
                    {
                        if (dr.ChildTable.TableName.Equals(table))
                            continue;

                        bool child = DataHelper.CheckForColumns(tbl, newTable, dr.ChildColumns);

                        if (child)
                        {
                            DataRelation newDr = DataHelper.NewChildFKRelation(newTable, dr);
                            if (!dataSet.Relations.Contains(newDr.RelationName))
                                dataSet.Relations.Add(newDr);
                        }
                    }
                }
            }

            foreach (string str in newTables.Keys)
            {
                visibleTables.Remove(str);
                visibleTables.Add(newTables[str]);
            }

            if (entityTypes[table].Equals(EntityTypes.TransactionEntity))
            {
                // get a list of Parent Transaction Entities
                // & copy their FK columns to dimensions
                string[] ptt = DataHelper.GetParentTransactionEntities(dataSet, newTable.TableName, entityTypes, visibleTables);
                foreach (string parent in ptt)
                {
                    List<DataRelation> relations = DataHelper.GetRelationsBetween(dataSet, parent, newTable.TableName);
                    foreach (DataRelation dr in relations)
                    {
                        List<DataColumn> parentKeys = new List<DataColumn>(dr.ParentTable.PrimaryKey);
                        List<DataColumn> childKeys = new List<DataColumn>();

                        foreach (DataColumn dc in parentKeys)
                            childKeys.Add(dr.ChildTable.Columns[dc.ColumnName]);

                        DataRelation newDr = new DataRelation(
                            dr.RelationName,
                            parentKeys.ToArray(),
                            childKeys.ToArray()
                            );

                        dataSet.Relations.Remove(dr);
                        dataSet.Relations.Add(newDr);
                    }
                }
            }
        }

        public DataSet DataSet
        {
            get { return dataSet; }
        }

        public List<string> VisibleTables
        {
            get { return visibleTables; }
        }

        public Dictionary<string, EntityTypes> DicEntityTypes
        {
            get { return entityTypes; }
        }
    }
}
