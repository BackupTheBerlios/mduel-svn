using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace oltp2olap.heuristics
{
    public class OlapModel: IOlapModel
    {
        protected DataSet dataSet;
        protected List<string> visibleTables;
        protected Dictionary<string, EntityTypes> entityTypes;

        public OlapModel(DataSet ds, Dictionary<string, EntityTypes> types, List<string> visible)
        {
            dataSet = ds;
            entityTypes = types;
            visibleTables = visible;
        }

        public virtual DataSet DeriveModel()
        {
            return null;
        }
    }
}
