using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using oltp2olap.helpers;

namespace oltp2olap.heuristics
{
    public class TerracedSchema: OlapModel
    {
        public TerracedSchema(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
            : base(ds, types, visible)
        {
        }

        public override DataSet DeriveModel()
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

                    if (entityTypes[newTable] == EntityTypes.TransactionEntity)
                        continue;

                    if (dataSet.Tables.Contains(newTable))
                    {
                        Collapse cp = new Collapse(dataSet, newTable, new List<string>(), visibleTables);
                        dataSet = cp.GetResult();
                        visibleTables = cp.VisibleTables;
                    }
                }
            }

            return dataSet;
        }
    }
}
