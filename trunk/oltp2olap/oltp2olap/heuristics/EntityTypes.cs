using System;
using System.Collections.Generic;
using System.Text;

namespace oltp2olap.heuristics
{
    public enum EntityTypes
    {
        Unclassified = -1,
        ClassificationEntity = 0,
        ComponentEntity = 1,
        TransactionEntity = 2
    }
}
