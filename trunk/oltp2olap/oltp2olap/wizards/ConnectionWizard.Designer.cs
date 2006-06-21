namespace oltp2olap.wizards
{
    partial class ConnectionWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionWizard));
            this.appWizard = new Gui.Wizard.Wizard();
            this.startPage = new Gui.Wizard.WizardPage();
            this.infoContainer1 = new Gui.Wizard.InfoContainer();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbSqlAuth = new System.Windows.Forms.RadioButton();
            this.rbWinAuth = new System.Windows.Forms.RadioButton();
            this.appWizard.SuspendLayout();
            this.startPage.SuspendLayout();
            this.infoContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // appWizard
            // 
            this.appWizard.Controls.Add(this.startPage);
            this.appWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appWizard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appWizard.Location = new System.Drawing.Point(0, 0);
            this.appWizard.Name = "appWizard";
            this.appWizard.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.startPage});
            this.appWizard.Size = new System.Drawing.Size(491, 357);
            this.appWizard.TabIndex = 0;
            this.appWizard.CloseFromCancel += new System.ComponentModel.CancelEventHandler(this.appWizard_CloseFromCancel);
            // 
            // startPage
            // 
            this.startPage.Controls.Add(this.infoContainer1);
            this.startPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPage.IsFinishPage = false;
            this.startPage.Location = new System.Drawing.Point(0, 0);
            this.startPage.Name = "startPage";
            this.startPage.Size = new System.Drawing.Size(491, 309);
            this.startPage.TabIndex = 1;
            // 
            // infoContainer1
            // 
            this.infoContainer1.BackColor = System.Drawing.Color.White;
            this.infoContainer1.Controls.Add(this.cbDatabase);
            this.infoContainer1.Controls.Add(this.label3);
            this.infoContainer1.Controls.Add(this.cbServer);
            this.infoContainer1.Controls.Add(this.label1);
            this.infoContainer1.Controls.Add(this.label2);
            this.infoContainer1.Controls.Add(this.groupBox1);
            this.infoContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoContainer1.Image = ((System.Drawing.Image)(resources.GetObject("infoContainer1.Image")));
            this.infoContainer1.Location = new System.Drawing.Point(0, 0);
            this.infoContainer1.Name = "infoContainer1";
            this.infoContainer1.PageTitle = "Welcome to the DataSource wizard";
            this.infoContainer1.Size = new System.Drawing.Size(491, 309);
            this.infoContainer1.TabIndex = 0;
            // 
            // cbDatabase
            // 
            this.cbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabase.FormattingEnabled = true;
            this.cbDatabase.Location = new System.Drawing.Point(247, 270);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(234, 21);
            this.cbDatabase.TabIndex = 13;
            this.cbDatabase.DropDown += new System.EventHandler(this.cbDatabase_DropDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(168, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Database:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbServer
            // 
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(247, 103);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(234, 21);
            this.cbServer.TabIndex = 14;
            this.cbServer.Text = "localhost\\sql2005dev";
            this.cbServer.DropDown += new System.EventHandler(this.cbServer_DropDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(168, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Please configure the database server and table settings from which the applicatio" +
                "n will derive the OLAP model:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(168, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Server name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rbSqlAuth);
            this.groupBox1.Controls.Add(this.rbWinAuth);
            this.groupBox1.Location = new System.Drawing.Point(171, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 134);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log On to Server";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(106, 93);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(188, 21);
            this.txtPassword.TabIndex = 13;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(106, 66);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(188, 21);
            this.txtUsername.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(35, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Password:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(35, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbSqlAuth
            // 
            this.rbSqlAuth.AutoSize = true;
            this.rbSqlAuth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbSqlAuth.Location = new System.Drawing.Point(17, 43);
            this.rbSqlAuth.Name = "rbSqlAuth";
            this.rbSqlAuth.Size = new System.Drawing.Size(179, 18);
            this.rbSqlAuth.TabIndex = 1;
            this.rbSqlAuth.Text = "Use SQL Server Authentication";
            this.rbSqlAuth.UseVisualStyleBackColor = true;
            this.rbSqlAuth.CheckedChanged += new System.EventHandler(this.rbSqlAuth_CheckedChanged);
            // 
            // rbWinAuth
            // 
            this.rbWinAuth.AutoSize = true;
            this.rbWinAuth.Checked = true;
            this.rbWinAuth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbWinAuth.Location = new System.Drawing.Point(17, 20);
            this.rbWinAuth.Name = "rbWinAuth";
            this.rbWinAuth.Size = new System.Drawing.Size(168, 18);
            this.rbWinAuth.TabIndex = 0;
            this.rbWinAuth.TabStop = true;
            this.rbWinAuth.Text = "Use Windows Authentication";
            this.rbWinAuth.UseVisualStyleBackColor = true;
            // 
            // ConnectionWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 357);
            this.Controls.Add(this.appWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConnectionWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OLTP to OLAP Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionWizard_FormClosing);
            this.appWizard.ResumeLayout(false);
            this.startPage.ResumeLayout(false);
            this.infoContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard appWizard;
        private Gui.Wizard.WizardPage startPage;
        private Gui.Wizard.InfoContainer infoContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbSqlAuth;
        private System.Windows.Forms.RadioButton rbWinAuth;
        private System.Windows.Forms.ComboBox cbServer;


    }
}

