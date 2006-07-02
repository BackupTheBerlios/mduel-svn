using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using oltp2olap.helpers;

namespace oltp2olap.heuristics
{
    public class FlatSchema : OlapModel
    {
        public FlatSchema(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
            : base(ds, types, visible)
        {
        }

        public override DataSet DeriveModel()
        {
            Classification c = new Classification(dataSet, visibleTables);
            c.CalculateHierarquies();
            List<string> minimal = c.MinimalEntities;

            foreach (string table in minimal)
            {
                Collapse cp = new Collapse(dataSet, table, new List<string>(), VisibleTables);
                dataSet = cp.GetResult();
                visibleTables = cp.VisibleTables;
            }

            return dataSet;
        }
    }
}
