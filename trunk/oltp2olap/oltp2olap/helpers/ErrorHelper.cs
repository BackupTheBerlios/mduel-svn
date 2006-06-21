using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap.helpers
{
    public static class ErrorHelper
    {
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Warning - An Error Ocurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
