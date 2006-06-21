using System;
using System.Data;
using System.Windows.Forms;
using System.Data.Sql;
using oltp2olap.helpers;

namespace oltp2olap.wizards
{
    public partial class ConnectionWizard : Form
    {
        private SqlSchema sqlSchema;

        public ConnectionWizard()
        {
            InitializeComponent();
            
            sqlSchema = new SqlSchema();
#if DEBUG
            cbDatabase_DropDown(this, null);
            if (cbDatabase.Items.Contains("Moody"))
                cbDatabase.SelectedItem = "Moody";
#endif
        }
        
        public DataSet GetDataSet()
        {
            if (cbDatabase.Text != String.Empty)
                return sqlSchema.GetDataSet(cbDatabase.Text);
            else
                return null;
        }

        private void rbSqlAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSqlAuth.Checked)
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void cbDatabase_DropDown(object sender, EventArgs e)
        {
            cbDatabase.Items.Clear();
            try
            {
                sqlSchema.Server = cbServer.Text;
                if (rbWinAuth.Checked)
                {
                    sqlSchema.WindowsAuthorization = true;
                    sqlSchema.Username = String.Empty;
                    sqlSchema.Password = String.Empty;
                }
                else
                {
                    sqlSchema.WindowsAuthorization = false;
                    sqlSchema.Username = txtUsername.Text;
                    sqlSchema.Password = txtPassword.Text;
                }

                cbDatabase.Items.AddRange(sqlSchema.GetExistingTables());
            }
            catch (Exception)
            {
                ErrorHelper.ShowError("The server name or the authentication data you provided is not valid!");
                cbDatabase.Items.Clear();
            }
        }

        private void cbServer_DropDown(object sender, EventArgs e)
        {
            DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();

            cbServer.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                cbServer.Items.Add(row[0].ToString());
            }
        }

        private void ConnectionWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cbDatabase.Items.Count > 0 && cbDatabase.SelectedItem != null && cbDatabase.Text != String.Empty)
            {
                DialogResult = DialogResult.OK;
            }
            else if (!DialogResult.Equals(DialogResult.Cancel))
            {
                e.Cancel = true;
            }
        }

        private void appWizard_CloseFromCancel(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}