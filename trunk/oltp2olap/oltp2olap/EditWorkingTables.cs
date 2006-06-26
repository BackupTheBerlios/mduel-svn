
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace oltp2olap
{
    public partial class EditWorkingTables : Form
    {
        public EditWorkingTables(DataSet ds, List<string> visible)
        {
            InitializeComponent();

            selectTables1.SetTables(ds, visible);
        }

        public DataSet WorkDataSet
        {
            get { return selectTables1.WorkDataSet;  }
        }

        public List<string> VisibleTables
        {
            get { return selectTables1.VisibleTables; }
        }
    }
}