using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace oltp2olap.helpers
{
    public class DumpSql
    {
        public string SqlCode = String.Empty;

        public DumpSql(DataSet ds, List<string> visible)
        {
            StringBuilder sw = new StringBuilder();
            string sqlFile = @"c:\output.sql";

            writeIndex(sw, sqlFile);
            writeDBUsed(sw, ds.DataSetName);
            writeSettingsOn(sw);

            //getting tables from dataset
            DataTableCollection dtc = ds.Tables;
            IEnumerator dtcIt = dtc.GetEnumerator();

            sw.AppendLine("-- ## Tables creation ##\r\n");

            //iterating tables
            while (dtcIt.MoveNext())
            {
                DataTable dt = (DataTable)dtcIt.Current;
                if (!visible.Contains(dt.TableName))
                    continue;

                //write a table
                sw.AppendLine("\r\n--" + dt.TableName);
                writeTable(sw, dt);
            }

            //Constraints And References
            sw.AppendLine("\r\n\r\n-- ## Constraints And References ##\r\n");

            DataRelationCollection drc = ds.Relations;

            foreach (DataRelation relation in drc)
            {
                if (!visible.Contains(relation.ParentTable.TableName) ||
                    !visible.Contains(relation.ChildTable.TableName))
                    continue;

                writeConstraintAndReference(sw, relation);
            }
            writeSettingsOff(sw);

            SqlCode = sw.ToString();
        }

        public  void writeGo(StringBuilder sw)
        {
            sw.AppendLine("GO");
        }

        public void writeDBUsed(StringBuilder sw, string dataBase)
        {
            sw.AppendLine("USE [" + dataBase + "]");
        }

        public void writeSettingsOn(StringBuilder sw)
        {
            sw.AppendLine("SET ANSI_NULLS ON");
            writeGo(sw);
            sw.AppendLine("SET QUOTED_IDENTIFIER ON");
            writeGo(sw);
            sw.AppendLine("SET ANSI_PADDING ON");
            writeGo(sw);
        }

        public void writeSettingsOff(StringBuilder sw)
        {
            writeGo(sw);
            sw.AppendLine("SET ANSI_PADDING OFF");
        }

        public void writeHead(StringBuilder sw, string table)
        {
            string createTable = "CREATE TABLE ";
            string normalizedTable = "[" + table.Replace(".", "].[") + "]";
            string end = "(";
            sw.AppendLine(createTable + normalizedTable + end);
        }

        public void writeTail(StringBuilder sw)
        {
            sw.AppendLine(") ON [PRIMARY]");
        }

        public void writeFKTail(StringBuilder sw)
        {
            sw.AppendLine(")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
        }

        public void writeFKHead(StringBuilder sw)
        {
            sw.AppendLine("(");
        }

        public void writePrimaryKey(StringBuilder sw, string tableName)
        {
            string constraint = "CONSTRAINT ";
            string pkc = " PRIMARY KEY CLUSTERED";
            string normalizedTable = tableName.Replace("dbo.", "");
            sw.AppendLine(constraint + "[PK_" + normalizedTable + "]" + pkc);
        }

        public void writeForeignKeys(StringBuilder sw, DataColumn pk)
        {
            string order = " ASC";
            sw.AppendLine("\t[" + pk + "]" + order);
        }

        public void writeTable(StringBuilder sw, DataTable dt)
        {
            string tableName;
            DataColumn[] pk;//primary keys                           
            tableName = dt.TableName;

            writeHead(sw, tableName);

            DataColumnCollection dcc = dt.Columns;
            foreach (DataColumn dc in dcc)
            {
                writeColumn(sw, dc);
            }

            writePrimaryKey(sw, tableName);
            pk = dt.PrimaryKey;
            if (pk.Length != 0)
            {
                writeFKHead(sw);
                for (int i = 0; i < pk.Length; i++)
                {
                    writeForeignKeys(sw, pk[i]);
                }
                writeFKTail(sw);
            }
            writeTail(sw);
        }

        public void writeColumn(StringBuilder sw, DataColumn dc)
        {
            Type dataType;
            string columnName;
            string columnType;
            string end = ",";
            string nullField = "NULL";
            string nullable = "";
            string unique = "";
            int maxSize;

            columnName = "\t[" + dc.ColumnName + "] ";
            dataType = dc.DataType;
            maxSize = dc.MaxLength;
            columnType = getType(dataType.Name, maxSize);
            if (!dc.AllowDBNull)
                nullable = " NOT ";
            if (dc.Unique)
                unique = " UNIQUE";
            sw.AppendLine(columnName + columnType + nullable + nullField + end);
        }

        //Patego way... Faltam tipos 4 sure... 
        public  string getType(string dataType, int size)
        {
            string res = null;
            switch (dataType)
            {
                case "Int32":
                    res = "[int]";
                    break;
                case "String":
                    res = "[nchar] (" + size.ToString() + ") COLLATE Latin1_General_CI_AS";
                    break;
                case "Decimal":
                    res = "[money]";
                    break;
                case "DateTime":
                    res = "[datetime]";
                    break;
            }
            return res;
        }

        public void writeIndex(StringBuilder sw, string sqlFile)
        {
            string date;
            string time;
            date = DateTime.Now.ToLongDateString();
            time = DateTime.Now.ToLongTimeString();
            sw.AppendLine("--\tAuto-Generated on " + date + " @ " + time);
        }

        //FK_Table1_Table2_Campo1_Campo2 ;)
        public  string setForeignKey(string tableName, string relationName, string foreignKey, string foreignTable)
        {
            string tName = tableName;
            string rName = relationName;
            string fKey = foreignKey;
            string fTable = foreignTable;

            int indexOf = rName.IndexOf(foreignTable);
            int rLength = rName.Length;
            int fLength = foreignTable.Length;
            int lastLength = rLength - (indexOf + fLength);

            string constructedRelationName;
            constructedRelationName = rName.Substring(indexOf + fLength, lastLength);
            if (constructedRelationName.Length > 0)
            {
                constructedRelationName = constructedRelationName.Replace("_", "");
                return constructedRelationName + foreignKey;
            }
            else
            {
                return tName + foreignKey;
            }
        }

        public void writeConstraintAndReference(StringBuilder sw, DataRelation relation)
        {
            string aTable = "ALTER TABLE ";
            string wcac = "  WITH CHECK ADD  CONSTRAINT ";
            string fk = " FOREIGN KEY(";
            string end = ")";
            string refs = "REFERENCES ";
            string mid = " (";
            string foreignKey = relation.ParentTable.PrimaryKey[0].ToString();

            string table = relation.ChildKeyConstraint.Table.TableName;
            string foreignTable = relation.ChildKeyConstraint.RelatedTable.TableName;
            string relationName = relation.RelationName;
            string tableName = table.Replace(".", "].[");
            string normalizedTable = table.Replace("dbo.", "");
            string constructedFK = foreignKey;
            string foreignTableName = foreignTable.Replace(".", "].[");
            string normalizedFTable = foreignTable.Replace("dbo.", "");

            if (relation.ChildKeyConstraint.RelatedTable.ChildRelations.Count > 1)
            {
                if (!(relation.ChildKeyConstraint.RelatedTable.ParentRelations.Count > 1))
                {
                    constructedFK = setForeignKey(normalizedTable, relationName, foreignKey, normalizedFTable);
                }
            }

            string alterTable = aTable + "[" + tableName + "]" + wcac + "[" + relationName + "]" + fk + "[" + constructedFK + "]" + end;
            string refers = refs + "[" + foreignTableName + "]" + mid + "[" + foreignKey + "]" + end + "\r\n";

            //ALTER TABLE
            sw.AppendLine(alterTable);

            //REFERENCES
            sw.AppendLine(refers);
        }
    }
}
