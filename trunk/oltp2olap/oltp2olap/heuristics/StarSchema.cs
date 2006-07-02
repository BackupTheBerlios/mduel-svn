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

        public void CollapseClassificationTypes()
        {
            Classification c = new Classification(dataSet, visibleTables);
            c.CalculateHierarquies();
            List<LinkedList<string>> maximal = c.MaximalStringHierarchies;

            foreach (LinkedList<string> hierarchy in maximal)
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
                        Collapse cp = new Collapse(dataSet, newTable, new List<string>(), visibleTables);
                        dataSet = cp.GetResult();
                    }
                }
            }
        }

        private void CreateNewTables()
        {
            foreach (string table in visibleTables)
            {
                if (entityTypes[table].Equals(EntityTypes.TransactionEntity))
                {
                    DataTable newTable = new DataTable(table + "Fact");

                    foreach (DataColumn dc in dataSet.Tables[table].Columns)
                    {
                        DataColumn newDc = new DataColumn(dc.ColumnName, dc.DataType);
                        newTable.Columns.Add(newDc);

                        if (DataHelper.IsForeignKeyToComponent(dataSet, dc, entityTypes) ||
                            DataHelper.IsPrimaryKey(dc) )
                        {
                            List<DataColumn> pks = new List<DataColumn>(newTable.PrimaryKey);
                            if (!pks.Contains(dc))
                                pks.Add(newTable.Columns[dc.ColumnName]);
                            newTable.PrimaryKey = pks.ToArray();
                        }
                    }
                    dataSet.Tables.Add(newTable);
                }
            }
        }

        private void SetPrimaryKeys()
        {
            foreach (string table in visibleTables)
            {
                if (entityTypes[table].Equals(EntityTypes.TransactionEntity))
                {
                    DataTable dt = dataSet.Tables[table];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (DataHelper.IsForeignKeyToComponent(dataSet, dc, entityTypes))
                        {
                            List<DataRelation> relations = DataHelper.RemoveRelations(dataSet, table);

                            List<DataColumn> pks = new List<DataColumn>(dt.PrimaryKey);
                            if (!pks.Contains(dc))
                                pks.Add(dt.Columns[dc.ColumnName]);
                            dt.PrimaryKey = pks.ToArray();

                            DataHelper.RestoreRelations(dataSet, relations);
                        }
                    }
                }
            }
        }

        public override DataSet DeriveModel()
        {
            CollapseClassificationTypes();
            CreateNewTables();
            //SetPrimaryKeys();

            return dataSet;
        }
    }
}
