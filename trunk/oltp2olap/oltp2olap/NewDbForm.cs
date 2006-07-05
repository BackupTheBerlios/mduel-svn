using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace oltp2olap
{
    public partial class NewDbForm : Form
    {
        public NewDbForm()
        {
            InitializeComponent();
        }

        private void txtDbName_TextChanged(object sender, EventArgs e)
        {
            if (txtDbName.Text.Length > 0)
                btnOk.Enabled = true;
            else
                btnOk.Enabled = false;
        }
    }
}