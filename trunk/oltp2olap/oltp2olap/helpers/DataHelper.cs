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
                    if (c1.ColumnName.Equals(dc.ColumnName) &&
                        entityTypes[dr.ParentTable.TableName].Equals(EntityTypes.ComponentEntity))
                        return true;
                }
            }
            return false;
        }

        public static List<DataRelation> RemoveRelations(DataSet dataSet, string table)
        {
            List<DataRelation> relations = new List<DataRelation>();

            foreach (DataRelation dr in dataSet.Relations)
            {
                if (dr.ParentTable.TableName.Equals(table) ||
                    dr.ChildTable.TableName.Equals(table))
                    relations.Add(dr);
            }

            foreach (DataRelation dr in relations)
            {
                dataSet.Relations.Remove(dr);
            }

            return relations;
        }

        public static void RestoreRelations(DataSet dataSet, List<DataRelation> relations)
        {
            foreach (DataRelation dr in relations)
                dataSet.Relations.Add(dr);
        }
    }
}
