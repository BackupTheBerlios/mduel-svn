
using System.Data;
using System.Windows.Forms;

namespace oltp2olap
{
    public partial class EditWorkingTables : Form
    {
        public EditWorkingTables(DataSet orig, DataSet dest)
        {
            InitializeComponent();

            selectTables1.SetTables(orig, dest);
        }
        
        public DataSet WorkDataSet
        {
            get { return selectTables1.WorkDataSet;  }
        }

        private void btnDone_Click(object sender, System.EventArgs e)
        {
            selectTables1.CopySelectedTables();
        }
    }
}