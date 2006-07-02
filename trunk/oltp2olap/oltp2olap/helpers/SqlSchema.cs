using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace oltp2olap.helpers
{
    public class SqlSchema
    {
        private SqlConnectionStringBuilder connStr;
        private bool windowsAuthorization = true;
        private string server = String.Empty;
        private string username = String.Empty;
        private string password = String.Empty;
        private string database = String.Empty;
        
        public SqlSchema()
        {
            connStr = new SqlConnectionStringBuilder();
        }

        public object[]GetExistingTables()
        {
            SqlConnection conn = null;
            List<String> tables = new List<string>();

            try
            {
                connStr.DataSource = server;
                if (windowsAuthorization)
                {
                    connStr.IntegratedSecurity = true;
                    connStr.UserID = String.Empty;
                    connStr.Password = String.Empty;
                }
                else
                {
                    connStr.IntegratedSecurity = false;
                    connStr.UserID = username;
                    connStr.Password = password;
                }

                conn = new SqlConnection(connStr.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_databases";

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tables.Add(reader.GetString(0));
                }
            }
            catch (Exception)
            {
                throw new Exception("The server name or the authentication data you provided is not valid!");
            }
            finally
            {
                conn.Close();
            }
            return tables.ToArray();
        }

        public DataSet GetDataSet(string database)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DataTable tableInfo;
            this.database = database;
            connStr.InitialCatalog = database;

            using (DbConnection c = factory.CreateConnection())
            {
                c.ConnectionString = connStr.ConnectionString;

                c.Open();
                tableInfo = c.GetSchema("Tables", null);
                c.Close();
            }

            SqlConnection conn = new SqlConnection(connStr.ConnectionString);

            DataSet ds = new DataSet();
            Hashtable tableLUT = new Hashtable();
            foreach (DataRow dr in tableInfo.Rows)
            {
                string table = dr[1].ToString() + "." + dr[2].ToString();
                tableLUT[dr[2].ToString()] = table;

                if (table.Equals("dbo.sysdiagrams"))
                    continue;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT * FROM " + table, conn);
                da.FillSchema(ds, SchemaType.Source, table);
            }

            DataTable relations = GetForeignKeyConstraints(conn);
            foreach (DataRow row in relations.Rows)
            {
                string child = tableLUT[row[0].ToString()].ToString();
                string childCol = row[1].ToString();
                string parent = tableLUT[row[2].ToString()].ToString();
                string parentCol = row[3].ToString();
                string relationName = row[4].ToString();

                DataColumn childData = ds.Tables[child].Columns[childCol];
                DataColumn parentData = ds.Tables[parent].Columns[parentCol];

                DataRelation relation = new DataRelation(relationName, parentData, childData);
                if (!ds.Relations.Contains(relationName))
                    ds.Relations.Add(relation);
            }
            ds.DataSetName = conn.Database;
            return ds;
        }

        private DataTable GetForeignKeyConstraints(SqlConnection conn)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT FK_Table  = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME,  PK_Table  = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME,  Constraint_Name = C.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN     INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN ( SELECT i1.TABLE_NAME, i2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY' ) PT ON PT.TABLE_NAME = PK.TABLE_NAME ORDER BY 1,2,3,4", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            return table;
        }

        public bool WindowsAuthorization
        {
            get { return windowsAuthorization; }
            set { windowsAuthorization = value; }
        }

        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        public string ConnectionString
        {
            get { return connStr.ConnectionString; }
        }
    }
}
