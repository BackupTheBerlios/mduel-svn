using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace oltp2olap.heuristics
{
    class SnowFlakeSchema : StarSchema
    {
        public SnowFlakeSchema(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
            :base(ds, types, visible)
        {
        }

        public override DataSet DeriveModel()
        {
            CreateNewTables();

            return dataSet;
        }
    }
}
