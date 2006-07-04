using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using oltp2olap.helpers;
using oltp2olap.heuristics;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Data;

namespace oltp2olap
{
    public partial class MainForm : Form
    {
        private ProjectExplorer prjExplorer = new ProjectExplorer();
        private string projectName = String.Empty;
        private string projectDirectory = String.Empty;
        private string projectExtension = ".BDDW";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            prjExplorer.MdiParent = this;
            prjExplorer.Show(dockPanel1);

            cbZoom.SelectedItem = "75%";
            cbZoom.Enabled = false;
        }

        private void mnuZoom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string zoom = ((ToolStripComboBox)sender).Text.TrimEnd('%');
            int iZoom = System.Int32.Parse(zoom);
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
                frmModel.SetZoom(iZoom);
        }

        private void ClassifyEntities(ClassificationTypes type)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.ClassifyEntities(type);
            }
        }

        public void SetZoomOn()
        {
            cbZoom.Enabled = true;
        }

        public void SetZoomOff()
        {
            cbZoom.Enabled = false;
        }

        private void classify_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            if (menu.Text == "Algorithm #1")
                ClassifyEntities(ClassificationTypes.AlgorithmNumber1);
            else if (menu.Text == "Algorithm #2")
                ClassifyEntities(ClassificationTypes.AlgorithmNumber2);
        }

        public DockPanel DockPanel
        {
            get { return dockPanel1; }
        }

        private void flatSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveFlatSchema();
            }
        }

        private void terracedSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveTerracedSchema();
            }
        }

        private void starSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveStarSchema();
            }
        }

        private void snowflakeSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveSnowFlakeSchema();
            }
        }

        private void starClusterSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel != null)
            {
                frmModel.DeriveStarClusterSchema();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDockContent dock = dockPanel1.ActiveDocument;
            if (dock == null)
                return;

            if (dock.GetType().Equals(typeof(ModelForm)))
            {
                if (projectName.Equals(string.Empty))
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                    return;
                }
                ModelForm frmModel = (ModelForm)dock;

                //projectDirectory will be dinamic
                //string xmlExtension = ".xml";
                string filename = projectDirectory + projectName + projectExtension;
                string dataSetName = frmModel.DataSet.DataSetName;
                string dataSetFileName = projectDirectory + dataSetName + projectExtension;
                //                frmModel.DataSet.WriteXml(dataSetFileName, System.Data.XmlWriteMode.WriteSchema);
                writeProjectStatus(filename, frmModel);
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fDialog;
            fDialog = new SaveFileDialog();
            fDialog.Filter = "BDDW Project Files|*" + projectExtension;
            //PROBLEMA com extensões
            fDialog.CheckFileExists = false;
            fDialog.AddExtension = false;
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(fDialog.FileName.ToString());
                projectDirectory = file.Directory.ToString() + "\\";
                projectName = file.Name.ToString();
                if (projectName.EndsWith(projectExtension))
                {
                    projectName.Replace(projectExtension, "");
                }
                saveToolStripMenuItem_Click(sender, e);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel == null)
            {
                frmModel = new ModelForm();
                frmModel.Closed += new EventHandler(frmModel_Closed);
            }
            
            frmModel.SqlSchema = new SqlSchema();

            OpenFileDialog fDialog;
            fDialog = new OpenFileDialog();
            fDialog.Filter = "BDDW Project Files|*" + projectExtension;
            fDialog.Multiselect = false;
            fDialog.CheckFileExists = false;
            fDialog.AddExtension = false;
            fDialog.CheckFileExists = true;
            FileInfo file;
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                file = new FileInfo(fDialog.FileName.ToString());
                projectDirectory = file.Directory.ToString() + "\\";
                projectName = file.Name.ToString();

                //open pode fazer validação com schema ;)
                //pode ser o current em ultino caso se ja houver nenhum aberto

                //VALIDAR COM SCHEMA == GOOD PROJECT FILE!!!!

                //XmlTextReader reader = new XmlTextReader(fDialog.FileName.ToString());
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fDialog.FileName.ToString());


                /////#################################################
                /*
                MessageBox.Show("will validate file");
                //ValidationEventArgs
                //http://www.codeproject.com/soap/Simple_XML_Validator.asp
                //http://www.codeproject.com/cs/webservices/XmlSchemaValidator.asp
                xdoc.Schemas.Add(null, @"D:\-=FCT=-\FCT-SEMESTRE PAR 2005-2006\BDDW\TI\currentState.xsd");
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                xdoc.Validate(eventHandler);
                MessageBox.Show("file validated");
                 * */
                /////#################################################


                //root : ProjectStatus
                XmlNode projectStatusNode = xdoc.SelectSingleNode("/ProjectStatus");

                //ProjectName
                XmlNode pnNode = projectStatusNode.SelectSingleNode("ProjectName");
                this.projectName = pnNode.InnerText;

                //ConnectionSettings
                XmlNode csNode = projectStatusNode.SelectSingleNode("ConnectionSettings");
                XmlNode waNode, sNode, unNode, pNode;

                //WindowsAuthorization
                waNode = csNode.SelectSingleNode("WindowsAuthorization");
                bool wa = false;
                string waString = waNode.InnerText;
                if (waString.Equals("true"))
                    wa = true;
                frmModel.SqlSchema.WindowsAuthorization = wa;

                //Server
                sNode = csNode.SelectSingleNode("Server");
                frmModel.SqlSchema.Server = sNode.InnerText;

                //UserName
                unNode = csNode.SelectSingleNode("UserName");
                frmModel.SqlSchema.Username = unNode.InnerText;

                //Password
                pNode = csNode.SelectSingleNode("Password");
                frmModel.SqlSchema.Password = pNode.InnerText;

                //DataSetFileName
                XmlNode dsf = projectStatusNode.SelectSingleNode("DataSetFileName");
                string dataSetFileName = dsf.InnerText;
                frmModel.DataSet = new DataSet();
                //dataset load
                if (!dataSetFileName.Equals(string.Empty))
                {
                    frmModel.DataSet.ReadXml(dataSetFileName);
                }

                //WorkingTables
                frmModel.VisibleTables = new List<string>();
                XmlNode wtNode = projectStatusNode.SelectSingleNode("WorkingTables");
                foreach (XmlNode elem in wtNode)
                {
                    XmlNode tmp = elem.SelectSingleNode("TableName");
                    frmModel.VisibleTables.Add(elem.InnerText.ToString());
                }
                //MessageBox.Show(frmModel.VisibleTables.Count.ToString ());

                //EntitiesSuggestion
                XmlNode esNode = projectStatusNode.SelectSingleNode("EntitiesSuggestion");

                //EntityTypes
                XmlNode etNode = esNode.SelectSingleNode("EntityTypes");

                frmModel.EntityTypesDic = new Dictionary<string, EntityTypes>();

                foreach (XmlNode elem in etNode)
                {
                    string tableName;
                    int tableType;
                    XmlNode tableNode = elem.SelectSingleNode("TableName");
                    tableName = tableNode.InnerText;
                    XmlNode typeNode = elem.SelectSingleNode("TableType");
                    tableType = System.Convert.ToInt32(typeNode.InnerText.ToString());
                    EntityTypes eType = EntityTypes.Unclassified;
                    switch (tableType)
                    {
                        case -1:
                            eType = EntityTypes.Unclassified;
                            break;
                        case 0:
                            eType = EntityTypes.ClassificationEntity;
                            break;
                        case 1:
                            eType = EntityTypes.ComponentEntity;
                            break;
                        case 2:
                            eType = EntityTypes.TransactionEntity;
                            break;
                    }
                    frmModel.EntityTypesDic.Add(tableName, eType);
                }
                //MessageBox.Show(frmModel.EntityTypesDic.Count.ToString());

                //MinimalEntities
                frmModel.MinimalEntities = new List<int>();
                XmlNodeList minNode = esNode.SelectNodes("MinimalEntities/TableName");
                foreach (XmlNode elem in minNode)
                {
                    frmModel.MinimalEntities.Add(System.Convert.ToInt32(elem.InnerText.ToString()));
                }

                //MaximalEntities
                frmModel.MaximalEntities = new List<int>();
                XmlNodeList maxNode = esNode.SelectNodes("MaximalEntities/TableName");
                foreach (XmlNode elem in maxNode)
                {
                    frmModel.MaximalEntities.Add(System.Convert.ToInt32(elem.InnerText.ToString()));
                }

                //MaximalHierarchies
                frmModel.MaximalHierarchies = new List<List<int>>();
                XmlNodeList listNode = esNode.SelectNodes("MaximalHierarchies/Vector/List");
                foreach (XmlNode list in listNode)
                {
                    List<int> toAdd = new List<int>();
                    XmlNodeList elemList = list.SelectNodes("Elem");
                    foreach (XmlNode elem in elemList)
                    {
                        toAdd.Add(System.Convert.ToInt32(elem.InnerText.ToString()));
                    }
                    frmModel.MaximalHierarchies.Add(toAdd);
                }

                prjExplorer.TreeView.Nodes[0].Nodes.Clear();
                prjExplorer.TreeView.Nodes[0].Nodes.Add(frmModel.DataSet.DataSetName);
                prjExplorer.TreeView.Nodes[0].Expand();
                frmModel.Show(DockPanel);

                frmModel.SetZoom(75);
                frmModel.LoadDataSet(frmModel.DataSet);
            }
        }

        void frmModel_Closed(object sender, EventArgs e)
        {
            prjExplorer.TreeView.Nodes[0].Nodes.Clear();
        }

        private void writeProjectStatus(string fileName, ModelForm frmModel)
        {
            //string xmlExtension = ".xml";

            Stream fileStream = new FileStream(fileName, FileMode.Create);
            StreamWriter stw = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
            XmlTextWriter xmlWriter = new XmlTextWriter(stw);
            xmlWriter.Formatting = Formatting.Indented;

            //xml doc
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlWriter.WriteStartElement("ProjectStatus");

            //ProjectName
            xmlWriter.WriteElementString("ProjectName", projectName);


            //ConnectionSettings
            xmlWriter.WriteStartElement("ConnectionSettings");
            writeConnectionSettings(xmlWriter, frmModel);
            xmlWriter.WriteEndElement(); // /ConnectionSettings

            //DataSetFileName
            String dsFileName = projectDirectory + frmModel.DataSet.DataSetName + projectExtension;
            dsFileName.Replace("&", "&amp;");
            dsFileName.Replace("'", "&apos;");
            xmlWriter.WriteElementString("DataSetFileName", dsFileName);
            //writing dataset
            frmModel.DataSet.WriteXml(dsFileName, System.Data.XmlWriteMode.WriteSchema);

            //WorkingTables
            xmlWriter.WriteStartElement("WorkingTables");
            writeWorkingTables(xmlWriter, frmModel);
            xmlWriter.WriteEndElement(); // /WorkingTables

            //EntitiesSuggestion
            xmlWriter.WriteStartElement("EntitiesSuggestion");

            writeEntityTypes(xmlWriter, frmModel);
            writeMinimalEntities(xmlWriter, frmModel);
            writeMaximalEntities(xmlWriter, frmModel);
            writeMaximalHierarchies(xmlWriter, frmModel);

            xmlWriter.WriteEndElement(); // /EntitiesSuggestion

            xmlWriter.WriteEndElement(); // /ProjectStatus

            // finalization
            xmlWriter.Close();
            stw.Close();
        }

        private void writeConnectionSettings(XmlTextWriter xmlWriter, ModelForm frmModel)
        {
            //WindowsAuthorization
            xmlWriter.WriteElementString("WindowsAuthorization", frmModel.SqlSchema.WindowsAuthorization.ToString().ToLower());

            //Server
            xmlWriter.WriteElementString("Server", frmModel.SqlSchema.Server.ToString());

            //UserName
            xmlWriter.WriteElementString("UserName", frmModel.SqlSchema.Username.ToString());

            //Password
            xmlWriter.WriteElementString("Password", frmModel.SqlSchema.Password.ToString());
        }


        private void writeEntityTypes(XmlTextWriter xmlWriter, ModelForm frmModel)
        {
            IEnumerator dicIt = frmModel.EntityTypesDic.GetEnumerator();
            xmlWriter.WriteStartElement("EntityTypes");
            while (dicIt.MoveNext())
            {
                KeyValuePair<string, EntityTypes> dicElem;
                dicElem = (KeyValuePair<string, EntityTypes>)dicIt.Current;
                string dicString = dicElem.Key;
                int eTypes = (int)dicElem.Value;
                xmlWriter.WriteStartElement("EntityType");
                xmlWriter.WriteElementString("TableName", dicString);
                xmlWriter.WriteElementString("TableType", eTypes.ToString());
                xmlWriter.WriteEndElement(); // /EntityType
            }
            xmlWriter.WriteEndElement(); // /EntityTypes
        }

        private void writeMinimalEntities(XmlTextWriter xmlWriter, ModelForm frmModel)
        {
            IEnumerator minIt = frmModel.MinimalEntities.GetEnumerator();
            xmlWriter.WriteStartElement("MinimalEntities");
            while (minIt.MoveNext())
            {
                int minElem = (int)minIt.Current;
                xmlWriter.WriteElementString("TableName", minElem.ToString());
            }
            xmlWriter.WriteEndElement(); // /MinimalEntities
        }

        private void writeMaximalEntities(XmlTextWriter xmlWriter, ModelForm frmModel)
        {
            IEnumerator maxIt = frmModel.MaximalEntities.GetEnumerator();
            xmlWriter.WriteStartElement("MaximalEntities");
            while (maxIt.MoveNext())
            {
                int maxElem = (int)maxIt.Current;
                xmlWriter.WriteElementString("TableName", maxElem.ToString());
            }
            xmlWriter.WriteEndElement(); // /MaximalEntities
        }

        private void writeMaximalHierarchies(XmlTextWriter xmlWriter, ModelForm frmModel)
        {
            IEnumerator maxHIt = frmModel.MaximalHierarchies.GetEnumerator();
            xmlWriter.WriteStartElement("MaximalHierarchies");
            if (frmModel.MaximalHierarchies.Count != 0)
            {
                xmlWriter.WriteStartElement("Vector");
                while (maxHIt.MoveNext())
                {
                    List<int> tmpList = (List<int>)maxHIt.Current;
                    IEnumerator listIt = tmpList.GetEnumerator();
                    xmlWriter.WriteStartElement("List");
                    while (listIt.MoveNext())
                    {
                        int elem = (int)listIt.Current;
                        xmlWriter.WriteElementString("Elem", elem.ToString());
                    }
                    xmlWriter.WriteEndElement(); // /List
                }
                xmlWriter.WriteEndElement(); // /Vector
            }
            xmlWriter.WriteEndElement(); // /MaximalHierarchies
        }

        private void writeWorkingTables(XmlTextWriter xmlWriter, ModelForm frmModel)
        {
            IEnumerator vtIterator = frmModel.VisibleTables.GetEnumerator();
            while (vtIterator.MoveNext())
            {
                string vTable = (string)vtIterator.Current;
                xmlWriter.WriteElementString("TableName", vTable);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prjExplorer.Go();
        }

        private void exportToSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelForm frmModel = (ModelForm)dockPanel1.ActiveDocument;
            if (frmModel == null)
                return;

            DumpSql dsql = new DumpSql(frmModel.DataSet, frmModel.VisibleTables);
            string sql = dsql.SqlCode;
            string preview = Path.GetTempFileName();

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(preview, false);
                sw.Write(sql);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao abrir ficheiro!", "Ocorreu um erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                sw.Close();
            }

            System.Diagnostics.Process.Start("notepad.exe", preview);
        }
    }
}