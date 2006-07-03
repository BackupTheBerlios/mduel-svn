using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using oltp2olap.helpers;

namespace oltp2olap.heuristics
{
    public class StarSchema: OlapModel
    {
        public StarSchema(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
            :base(ds, types, visible)
        {
        }

        protected virtual void CollapseClassificationTypes()
        {
            Classification c = new Classification(dataSet, visibleTables);
            c.CalculateHierarquies();
            List<List<string>> maximal = c.MaximalStringHierarchies;

            foreach (List<string> hierarchy in maximal)
            {
                foreach (string table in hierarchy)
                {
                    string newTable = table;

                    if (table.IndexOf("(") != -1)
                        newTable = table.Substring(0, table.IndexOf("("));

                    if (entityTypes[newTable] > EntityTypes.ClassificationEntity)
                        continue;

                    if (dataSet.Tables.Contains(newTable))
                    {
                        Collapse cp = new Collapse(dataSet, newTable, new List<string>(), VisibleTables);
                        dataSet = cp.GetResult();
                    }
                }
            }
        }

        protected void CreateNewTables()
        {
            Dictionary<string, string> newTables = new Dictionary<string, string>();

            foreach (string table in visibleTables)
            {
                if (entityTypes[table].Equals(EntityTypes.TransactionEntity))
                {
                    DataTable newTable = new DataTable(table + "Fact");

                    // copy columns
                    foreach (DataColumn dc in dataSet.Tables[table].Columns)
                    {
                        DataColumn newDc = new DataColumn(dc.ColumnName, dc.DataType);
                        newTable.Columns.Add(newDc);
                    }

                    // get a list of Parent Transaction Entities
                    // & copy their FK columns to dimensions
                    string[] ptt = GetParentTransactionEntities(table);
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
                            DataHelper.IsPrimaryKey(dc) )
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
            }

            foreach (string str in newTables.Keys)
            {
                visibleTables.Remove(str);
                visibleTables.Add(newTables[str]);
            }
        }

        protected string[] GetParentTransactionEntities(string table)
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

        public override DataSet DeriveModel()
        {
            CollapseClassificationTypes();
            CreateNewTables();

            return dataSet;
        }
    }
}
