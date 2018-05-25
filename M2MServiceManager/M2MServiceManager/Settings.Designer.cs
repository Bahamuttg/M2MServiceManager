namespace M2MServiceManager
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.gbPaths = new System.Windows.Forms.GroupBox();
            this.btnFBDBak = new System.Windows.Forms.Button();
            this.btnFBDTarget = new System.Windows.Forms.Button();
            this.txtSvcClientCfgPathBak = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSvcClientCfgPathTarget = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbServiceNames = new System.Windows.Forms.GroupBox();
            this.txtLegacySvcName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNotifierSvcProcessName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNotifierSvcName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProcSvcProcessName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProcSvcName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNetSvcHostProcessName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNetSvcHostName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chkKillProcesses = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.seTimeout = new System.Windows.Forms.NumericUpDown();
            this.chkIncludeProcSvc = new System.Windows.Forms.CheckBox();
            this.chkWriteSvcClientCfg = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbMailSettings = new System.Windows.Forms.GroupBox();
            this.txtMailingList = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFromMailAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMailServerName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkSendMail = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fbdFilePaths = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbPaths.SuspendLayout();
            this.gbServiceNames.SuspendLayout();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seTimeout)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbMailSettings.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPaths
            // 
            this.gbPaths.Controls.Add(this.btnFBDBak);
            this.gbPaths.Controls.Add(this.btnFBDTarget);
            this.gbPaths.Controls.Add(this.txtSvcClientCfgPathBak);
            this.gbPaths.Controls.Add(this.label2);
            this.gbPaths.Controls.Add(this.txtSvcClientCfgPathTarget);
            this.gbPaths.Controls.Add(this.label1);
            this.gbPaths.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPaths.Location = new System.Drawing.Point(0, 0);
            this.gbPaths.Name = "gbPaths";
            this.gbPaths.Size = new System.Drawing.Size(578, 119);
            this.gbPaths.TabIndex = 0;
            this.gbPaths.TabStop = false;
            this.gbPaths.Text = "Service Client Configuration Paths";
            // 
            // btnFBDBak
            // 
            this.btnFBDBak.Location = new System.Drawing.Point(531, 80);
            this.btnFBDBak.Name = "btnFBDBak";
            this.btnFBDBak.Size = new System.Drawing.Size(25, 20);
            this.btnFBDBak.TabIndex = 6;
            this.btnFBDBak.Text = "...";
            this.btnFBDBak.UseVisualStyleBackColor = true;
            this.btnFBDBak.Click += new System.EventHandler(this.btnFBDBak_Click);
            // 
            // btnFBDTarget
            // 
            this.btnFBDTarget.Location = new System.Drawing.Point(531, 37);
            this.btnFBDTarget.Name = "btnFBDTarget";
            this.btnFBDTarget.Size = new System.Drawing.Size(25, 20);
            this.btnFBDTarget.TabIndex = 5;
            this.btnFBDTarget.Text = "...";
            this.btnFBDTarget.UseVisualStyleBackColor = true;
            this.btnFBDTarget.Click += new System.EventHandler(this.btnFBDTarget_Click);
            // 
            // txtSvcClientCfgPathBak
            // 
            this.txtSvcClientCfgPathBak.Location = new System.Drawing.Point(16, 80);
            this.txtSvcClientCfgPathBak.Name = "txtSvcClientCfgPathBak";
            this.txtSvcClientCfgPathBak.Size = new System.Drawing.Size(510, 20);
            this.txtSvcClientCfgPathBak.TabIndex = 3;
            this.txtSvcClientCfgPathBak.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Service Client Configuration Backup Path";
            // 
            // txtSvcClientCfgPathTarget
            // 
            this.txtSvcClientCfgPathTarget.Location = new System.Drawing.Point(16, 37);
            this.txtSvcClientCfgPathTarget.Name = "txtSvcClientCfgPathTarget";
            this.txtSvcClientCfgPathTarget.Size = new System.Drawing.Size(509, 20);
            this.txtSvcClientCfgPathTarget.TabIndex = 1;
            this.txtSvcClientCfgPathTarget.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service Client Configuration Path";
            // 
            // gbServiceNames
            // 
            this.gbServiceNames.Controls.Add(this.txtLegacySvcName);
            this.gbServiceNames.Controls.Add(this.label10);
            this.gbServiceNames.Controls.Add(this.txtNotifierSvcProcessName);
            this.gbServiceNames.Controls.Add(this.label7);
            this.gbServiceNames.Controls.Add(this.txtNotifierSvcName);
            this.gbServiceNames.Controls.Add(this.label8);
            this.gbServiceNames.Controls.Add(this.txtProcSvcProcessName);
            this.gbServiceNames.Controls.Add(this.label5);
            this.gbServiceNames.Controls.Add(this.txtProcSvcName);
            this.gbServiceNames.Controls.Add(this.label6);
            this.gbServiceNames.Controls.Add(this.txtNetSvcHostProcessName);
            this.gbServiceNames.Controls.Add(this.label4);
            this.gbServiceNames.Controls.Add(this.txtNetSvcHostName);
            this.gbServiceNames.Controls.Add(this.label3);
            this.gbServiceNames.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbServiceNames.Location = new System.Drawing.Point(0, 119);
            this.gbServiceNames.Name = "gbServiceNames";
            this.gbServiceNames.Size = new System.Drawing.Size(578, 185);
            this.gbServiceNames.TabIndex = 1;
            this.gbServiceNames.TabStop = false;
            this.gbServiceNames.Text = "M2M Service And Process Names";
            // 
            // txtLegacySvcName
            // 
            this.txtLegacySvcName.Location = new System.Drawing.Point(16, 153);
            this.txtLegacySvcName.Name = "txtLegacySvcName";
            this.txtLegacySvcName.Size = new System.Drawing.Size(261, 20);
            this.txtLegacySvcName.TabIndex = 15;
            this.txtLegacySvcName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "VFP Legacy Service Host Name";
            // 
            // txtNotifierSvcProcessName
            // 
            this.txtNotifierSvcProcessName.Location = new System.Drawing.Point(296, 113);
            this.txtNotifierSvcProcessName.Name = "txtNotifierSvcProcessName";
            this.txtNotifierSvcProcessName.Size = new System.Drawing.Size(261, 20);
            this.txtNotifierSvcProcessName.TabIndex = 13;
            this.txtNotifierSvcProcessName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Notification Service Host Name";
            // 
            // txtNotifierSvcName
            // 
            this.txtNotifierSvcName.Location = new System.Drawing.Point(16, 113);
            this.txtNotifierSvcName.Name = "txtNotifierSvcName";
            this.txtNotifierSvcName.Size = new System.Drawing.Size(261, 20);
            this.txtNotifierSvcName.TabIndex = 11;
            this.txtNotifierSvcName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Notification Service Host Name";
            // 
            // txtProcSvcProcessName
            // 
            this.txtProcSvcProcessName.Location = new System.Drawing.Point(296, 73);
            this.txtProcSvcProcessName.Name = "txtProcSvcProcessName";
            this.txtProcSvcProcessName.Size = new System.Drawing.Size(261, 20);
            this.txtProcSvcProcessName.TabIndex = 9;
            this.txtProcSvcProcessName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Processor Service Host Process Name";
            // 
            // txtProcSvcName
            // 
            this.txtProcSvcName.Location = new System.Drawing.Point(16, 73);
            this.txtProcSvcName.Name = "txtProcSvcName";
            this.txtProcSvcName.Size = new System.Drawing.Size(261, 20);
            this.txtProcSvcName.TabIndex = 7;
            this.txtProcSvcName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Processor Service Host Name";
            // 
            // txtNetSvcHostProcessName
            // 
            this.txtNetSvcHostProcessName.Location = new System.Drawing.Point(295, 33);
            this.txtNetSvcHostProcessName.Name = "txtNetSvcHostProcessName";
            this.txtNetSvcHostProcessName.Size = new System.Drawing.Size(261, 20);
            this.txtNetSvcHostProcessName.TabIndex = 5;
            this.txtNetSvcHostProcessName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(292, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Net Services Host Process Name";
            // 
            // txtNetSvcHostName
            // 
            this.txtNetSvcHostName.Location = new System.Drawing.Point(15, 33);
            this.txtNetSvcHostName.Name = "txtNetSvcHostName";
            this.txtNetSvcHostName.Size = new System.Drawing.Size(261, 20);
            this.txtNetSvcHostName.TabIndex = 3;
            this.txtNetSvcHostName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Net Services Host Name";
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chkKillProcesses);
            this.gbOptions.Controls.Add(this.label13);
            this.gbOptions.Controls.Add(this.seTimeout);
            this.gbOptions.Controls.Add(this.chkIncludeProcSvc);
            this.gbOptions.Controls.Add(this.chkWriteSvcClientCfg);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbOptions.Location = new System.Drawing.Point(0, 0);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(276, 197);
            this.gbOptions.TabIndex = 2;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Misc Program Settings";
            // 
            // chkKillProcesses
            // 
            this.chkKillProcesses.AutoSize = true;
            this.chkKillProcesses.Location = new System.Drawing.Point(12, 55);
            this.chkKillProcesses.Name = "chkKillProcesses";
            this.chkKillProcesses.Size = new System.Drawing.Size(159, 17);
            this.chkKillProcesses.TabIndex = 5;
            this.chkKillProcesses.Text = "Kill Unresponsive Processes";
            this.chkKillProcesses.UseVisualStyleBackColor = true;
            this.chkKillProcesses.CheckedChanged += new System.EventHandler(this.HandleChkBoxChange_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Service Timeout (seconds)";
            // 
            // seTimeout
            // 
            this.seTimeout.Location = new System.Drawing.Point(12, 121);
            this.seTimeout.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.seTimeout.Name = "seTimeout";
            this.seTimeout.Size = new System.Drawing.Size(65, 20);
            this.seTimeout.TabIndex = 3;
            this.seTimeout.ValueChanged += new System.EventHandler(this.seTimeout_ValueChanged);
            // 
            // chkIncludeProcSvc
            // 
            this.chkIncludeProcSvc.AutoSize = true;
            this.chkIncludeProcSvc.Location = new System.Drawing.Point(12, 19);
            this.chkIncludeProcSvc.Name = "chkIncludeProcSvc";
            this.chkIncludeProcSvc.Size = new System.Drawing.Size(150, 17);
            this.chkIncludeProcSvc.TabIndex = 2;
            this.chkIncludeProcSvc.Text = "Include Processor Service";
            this.chkIncludeProcSvc.UseVisualStyleBackColor = true;
            this.chkIncludeProcSvc.CheckedChanged += new System.EventHandler(this.HandleChkBoxChange_CheckedChanged);
            // 
            // chkWriteSvcClientCfg
            // 
            this.chkWriteSvcClientCfg.AutoSize = true;
            this.chkWriteSvcClientCfg.Location = new System.Drawing.Point(12, 37);
            this.chkWriteSvcClientCfg.Name = "chkWriteSvcClientCfg";
            this.chkWriteSvcClientCfg.Size = new System.Drawing.Size(184, 17);
            this.chkWriteSvcClientCfg.TabIndex = 1;
            this.chkWriteSvcClientCfg.Text = "Write Service Client Configuration";
            this.chkWriteSvcClientCfg.UseVisualStyleBackColor = true;
            this.chkWriteSvcClientCfg.CheckedChanged += new System.EventHandler(this.HandleChkBoxChange_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbMailSettings);
            this.panel1.Controls.Add(this.gbOptions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 304);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 197);
            this.panel1.TabIndex = 3;
            // 
            // gbMailSettings
            // 
            this.gbMailSettings.Controls.Add(this.txtMailingList);
            this.gbMailSettings.Controls.Add(this.label9);
            this.gbMailSettings.Controls.Add(this.txtFromMailAddress);
            this.gbMailSettings.Controls.Add(this.label11);
            this.gbMailSettings.Controls.Add(this.txtMailServerName);
            this.gbMailSettings.Controls.Add(this.label12);
            this.gbMailSettings.Controls.Add(this.chkSendMail);
            this.gbMailSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMailSettings.Location = new System.Drawing.Point(276, 0);
            this.gbMailSettings.Name = "gbMailSettings";
            this.gbMailSettings.Size = new System.Drawing.Size(302, 197);
            this.gbMailSettings.TabIndex = 3;
            this.gbMailSettings.TabStop = false;
            this.gbMailSettings.Text = "Email Settings";
            // 
            // txtMailingList
            // 
            this.txtMailingList.Location = new System.Drawing.Point(9, 113);
            this.txtMailingList.Multiline = true;
            this.txtMailingList.Name = "txtMailingList";
            this.txtMailingList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMailingList.Size = new System.Drawing.Size(271, 78);
            this.txtMailingList.TabIndex = 15;
            this.txtMailingList.WordWrap = false;
            this.txtMailingList.TextChanged += new System.EventHandler(this.txtMailingList_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Email Recipients";
            // 
            // txtFromMailAddress
            // 
            this.txtFromMailAddress.Location = new System.Drawing.Point(10, 73);
            this.txtFromMailAddress.Name = "txtFromMailAddress";
            this.txtFromMailAddress.Size = new System.Drawing.Size(271, 20);
            this.txtFromMailAddress.TabIndex = 13;
            this.txtFromMailAddress.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "From Address";
            // 
            // txtMailServerName
            // 
            this.txtMailServerName.Location = new System.Drawing.Point(9, 33);
            this.txtMailServerName.Name = "txtMailServerName";
            this.txtMailServerName.Size = new System.Drawing.Size(272, 20);
            this.txtMailServerName.TabIndex = 11;
            this.txtMailServerName.TextChanged += new System.EventHandler(this.HandleTxtBoxChange_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Mail Server Address";
            // 
            // chkSendMail
            // 
            this.chkSendMail.AutoSize = true;
            this.chkSendMail.Location = new System.Drawing.Point(191, 15);
            this.chkSendMail.Name = "chkSendMail";
            this.chkSendMail.Size = new System.Drawing.Size(79, 17);
            this.chkSendMail.TabIndex = 0;
            this.chkSendMail.Text = "Send Email";
            this.chkSendMail.UseVisualStyleBackColor = true;
            this.chkSendMail.CheckedChanged += new System.EventHandler(this.HandleChkBoxChange_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(500, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.btnHelp);
            this.panel2.Controls.Add(this.btnSaveAs);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 501);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(578, 41);
            this.panel2.TabIndex = 6;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.Location = new System.Drawing.Point(3, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(108, 35);
            this.btnHelp.TabIndex = 7;
            this.btnHelp.Text = "Help/About M2M Service Manager";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAs.Location = new System.Drawing.Point(383, 15);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 6;
            this.btnSaveAs.Text = "Save As...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(302, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(578, 542);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbServiceNames);
            this.Controls.Add(this.gbPaths);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(594, 580);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(594, 580);
            this.Name = "Settings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M2M Service Manager Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.gbPaths.ResumeLayout(false);
            this.gbPaths.PerformLayout();
            this.gbServiceNames.ResumeLayout(false);
            this.gbServiceNames.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seTimeout)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbMailSettings.ResumeLayout(false);
            this.gbMailSettings.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPaths;
        private System.Windows.Forms.TextBox txtSvcClientCfgPathBak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSvcClientCfgPathTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbServiceNames;
        private System.Windows.Forms.TextBox txtNotifierSvcProcessName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNotifierSvcName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProcSvcProcessName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProcSvcName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNetSvcHostProcessName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNetSvcHostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLegacySvcName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chkIncludeProcSvc;
        private System.Windows.Forms.CheckBox chkWriteSvcClientCfg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbMailSettings;
        private System.Windows.Forms.CheckBox chkSendMail;
        private System.Windows.Forms.TextBox txtFromMailAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMailServerName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown seTimeout;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFBDBak;
        private System.Windows.Forms.Button btnFBDTarget;
        private System.Windows.Forms.FolderBrowserDialog fbdFilePaths;
        private System.Windows.Forms.TextBox txtMailingList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkKillProcesses;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnSave;
    }
}