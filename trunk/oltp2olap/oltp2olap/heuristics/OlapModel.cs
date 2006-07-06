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

            foreach (string table in entityTypes.Keys)
            {
                if (visibleTables.Contains(table))
                    if (entityTypes[table].Equals(EntityTypes.Unclassified))
                        throw new Exception("Nem todas as entidades estão classificadas!");
            }
        }

        public virtual DataSet DeriveModel()
        {
            return null;
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
