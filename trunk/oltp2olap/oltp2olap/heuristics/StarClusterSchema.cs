using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using oltp2olap.helpers;

namespace oltp2olap.heuristics
{
    public class StarClusterSchema : StarSchema
    {
        public StarClusterSchema(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
            :base(ds, types, visible)
        {
        }

        protected override void CollapseClassificationTypes()
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

                    bool doNotCollapse = false;
                    if (entityTypes[newTable].Equals(EntityTypes.ClassificationEntity))
                    {
                        List<DataRelation> relations = DataHelper.GetRelations(dataSet, newTable);

                        if (relations.Count > 1)
                        {
                            foreach (DataRelation dr in relations)
                            {
                                if (entityTypes[dr.ChildTable.TableName].Equals(EntityTypes.ComponentEntity))
                                    doNotCollapse = true;
                            }
                        }
                    }

                    if (doNotCollapse)
                        continue;

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
    }
}
