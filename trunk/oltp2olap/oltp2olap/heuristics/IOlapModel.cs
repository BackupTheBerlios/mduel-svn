using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace oltp2olap.heuristics
{
    public interface IOlapModel
    {
        DataSet DeriveModel();
    }
}
