using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace oltp2olap.helpers
{
    public class Aggregate
    {
        // column, expression
        private Dictionary<string, string> aggregationAttributes;
        private List<string> groupingAttributes;
        private string newTableName;
        private DataSet dataSet;

        public Aggregate(DataSet ds, Dictionary<string, string> aggr, List<string> group, string table)
        {
            this.dataSet = ds;
            this.newTableName = table;
            this.aggregationAttributes = aggr;
            this.groupingAttributes = group;
        }
    }
}
