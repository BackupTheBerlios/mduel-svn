using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace oltp2olap.heuristics
{
    public class StarClusterSchema : OlapModel
    {
        public StarClusterSchema(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
            :base(ds, types, visible)
        {
        }
        
        public override DataSet DeriveModel()
        {
            return dataSet;
        }
    }
}
