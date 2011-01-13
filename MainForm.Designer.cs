namespace Polyriser {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.Tabs = new System.Windows.Forms.TabControl();
			this.tabMain = new System.Windows.Forms.TabPage();
			this.lnkMainCoreLen = new System.Windows.Forms.LinkLabel();
			this.lnkMainNapLen = new System.Windows.Forms.LinkLabel();
			this.txtHistory = new System.Windows.Forms.TextBox();
			this.lblMainForce = new System.Windows.Forms.Label();
			this.cmdMainTestOptions = new System.Windows.Forms.Button();
			this.cboMainTestMethod = new System.Windows.Forms.ComboBox();
			this.cmdMainCore = new System.Windows.Forms.Button();
			this.txtMainCoreLen = new System.Windows.Forms.TextBox();
			this.lblMainCore = new System.Windows.Forms.Label();
			this.cmdMainNap = new System.Windows.Forms.Button();
			this.txtMainNapLen = new System.Windows.Forms.TextBox();
			this.lblMainNap = new System.Windows.Forms.Label();
			this.tabSetup = new System.Windows.Forms.TabPage();
			this.txtSoundFadeInLen = new System.Windows.Forms.TextBox();
			this.lblSoundFadeInLen = new System.Windows.Forms.Label();
			this.cmdSetupBrowseAppData = new System.Windows.Forms.Button();
			this.txtSoundWarningLen = new System.Windows.Forms.TextBox();
			this.lblSoundWarningLen = new System.Windows.Forms.Label();
			this.tbrSoundAlarm = new System.Windows.Forms.ToolBar();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.imlImages = new System.Windows.Forms.ImageList(this.components);
			this.tbrSoundWarning = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.txtSoundAlarmFile = new System.Windows.Forms.TextBox();
			this.txtSoundWarningFile = new System.Windows.Forms.TextBox();
			this.lblSoundAlarm = new System.Windows.Forms.Label();
			this.lblSoundWarning = new System.Windows.Forms.Label();
			this.txtSetupCoreCooldown = new System.Windows.Forms.TextBox();
			this.txtSetupCoreLen = new System.Windows.Forms.TextBox();
			this.txtSetupNapCooldown = new System.Windows.Forms.TextBox();
			this.txtSetupNapLen = new System.Windows.Forms.TextBox();
			this.lblSetupCoreCooldown = new System.Windows.Forms.Label();
			this.lblSetupCoreLen = new System.Windows.Forms.Label();
			this.lblSetupNapCooldown = new System.Windows.Forms.Label();
			this.lblSetupNapLen = new System.Windows.Forms.Label();
			this.cmdExit = new System.Windows.Forms.Button();
			this.cmdToTray = new System.Windows.Forms.Button();
			this.cmdAbout = new System.Windows.Forms.Button();
			this.mnuTray = new System.Windows.Forms.ContextMenu();
			this.mnuTrayShow = new System.Windows.Forms.MenuItem();
			this.mnuTrayExit = new System.Windows.Forms.MenuItem();
			this.lnkHistory = new System.Windows.Forms.LinkLabel();
			this.Tabs.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabSetup.SuspendLayout();
			this.SuspendLayout();
			// 
			// Tabs
			// 
			this.Tabs.Controls.Add(this.tabMain);
			this.Tabs.Controls.Add(this.tabSetup);
			this.Tabs.Location = new System.Drawing.Point(12, 12);
			this.Tabs.Name = "Tabs";
			this.Tabs.SelectedIndex = 0;
			this.Tabs.Size = new System.Drawing.Size(332, 253);
			this.Tabs.TabIndex = 0;
			this.Tabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.Tabs_Selecting);
			this.Tabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.Tabs_Deselecting);
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.lnkMainCoreLen);
			this.tabMain.Controls.Add(this.lnkHistory);
			this.tabMain.Controls.Add(this.lnkMainNapLen);
			this.tabMain.Controls.Add(this.txtHistory);
			this.tabMain.Controls.Add(this.lblMainForce);
			this.tabMain.Controls.Add(this.cmdMainTestOptions);
			this.tabMain.Controls.Add(this.cboMainTestMethod);
			this.tabMain.Controls.Add(this.cmdMainCore);
			this.tabMain.Controls.Add(this.txtMainCoreLen);
			this.tabMain.Controls.Add(this.lblMainCore);
			this.tabMain.Controls.Add(this.cmdMainNap);
			this.tabMain.Controls.Add(this.txtMainNapLen);
			this.tabMain.Controls.Add(this.lblMainNap);
			this.tabMain.Location = new System.Drawing.Point(4, 25);
			this.tabMain.Name = "tabMain";
			this.tabMain.Padding = new System.Windows.Forms.Padding(3);
			this.tabMain.Size = new System.Drawing.Size(324, 224);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Dashboard";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// lnkMainCoreLen
			// 
			this.lnkMainCoreLen.Location = new System.Drawing.Point(111, 39);
			this.lnkMainCoreLen.Name = "lnkMainCoreLen";
			this.lnkMainCoreLen.Size = new System.Drawing.Size(33, 14);
			this.lnkMainCoreLen.TabIndex = 11;
			this.lnkMainCoreLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkMainCoreLen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMainCoreLen_LinkClicked);
			// 
			// lnkMainNapLen
			// 
			this.lnkMainNapLen.Location = new System.Drawing.Point(111, 12);
			this.lnkMainNapLen.Name = "lnkMainNapLen";
			this.lnkMainNapLen.Size = new System.Drawing.Size(33, 10);
			this.lnkMainNapLen.TabIndex = 10;
			this.lnkMainNapLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkMainNapLen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMainNapLen_LinkClicked);
			// 
			// txtHistory
			// 
			this.txtHistory.Location = new System.Drawing.Point(6, 114);
			this.txtHistory.Multiline = true;
			this.txtHistory.Name = "txtHistory";
			this.txtHistory.ReadOnly = true;
			this.txtHistory.Size = new System.Drawing.Size(312, 104);
			this.txtHistory.TabIndex = 9;
			// 
			// lblMainForce
			// 
			this.lblMainForce.AutoSize = true;
			this.lblMainForce.Location = new System.Drawing.Point(6, 65);
			this.lblMainForce.Name = "lblMainForce";
			this.lblMainForce.Size = new System.Drawing.Size(86, 16);
			this.lblMainForce.TabIndex = 7;
			this.lblMainForce.Text = "Wakeup test:";
			// 
			// cmdMainTestOptions
			// 
			this.cmdMainTestOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdMainTestOptions.Location = new System.Drawing.Point(290, 62);
			this.cmdMainTestOptions.Name = "cmdMainTestOptions";
			this.cmdMainTestOptions.Size = new System.Drawing.Size(28, 24);
			this.cmdMainTestOptions.TabIndex = 6;
			this.cmdMainTestOptions.Text = "...";
			this.cmdMainTestOptions.UseVisualStyleBackColor = true;
			this.cmdMainTestOptions.Click += new System.EventHandler(this.cmdMainTestOptions_Click);
			// 
			// cboMainTestMethod
			// 
			this.cboMainTestMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMainTestMethod.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboMainTestMethod.FormattingEnabled = true;
			this.cboMainTestMethod.Items.AddRange(new object[] {
            "Do nothing",
            "Type some text",
            "Solve math problems"});
			this.cboMainTestMethod.Location = new System.Drawing.Point(98, 62);
			this.cboMainTestMethod.Name = "cboMainTestMethod";
			this.cboMainTestMethod.Size = new System.Drawing.Size(186, 24);
			this.cboMainTestMethod.TabIndex = 0;
			this.cboMainTestMethod.SelectedIndexChanged += new System.EventHandler(this.cboMainTestMethod_SelectedIndexChanged);
			// 
			// cmdMainCore
			// 
			this.cmdMainCore.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdMainCore.Location = new System.Drawing.Point(230, 34);
			this.cmdMainCore.Name = "cmdMainCore";
			this.cmdMainCore.Size = new System.Drawing.Size(88, 22);
			this.cmdMainCore.TabIndex = 5;
			this.cmdMainCore.Text = "Core >>";
			this.cmdMainCore.Click += new System.EventHandler(this.cmdMainCore_Click);
			// 
			// txtMainCoreLen
			// 
			this.txtMainCoreLen.Location = new System.Drawing.Point(98, 34);
			this.txtMainCoreLen.Name = "txtMainCoreLen";
			this.txtMainCoreLen.Size = new System.Drawing.Size(56, 22);
			this.txtMainCoreLen.TabIndex = 4;
			// 
			// lblMainCore
			// 
			this.lblMainCore.AutoSize = true;
			this.lblMainCore.Location = new System.Drawing.Point(6, 37);
			this.lblMainCore.Name = "lblMainCore";
			this.lblMainCore.Size = new System.Drawing.Size(79, 16);
			this.lblMainCore.TabIndex = 3;
			this.lblMainCore.Text = "Core length:";
			// 
			// cmdMainNap
			// 
			this.cmdMainNap.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdMainNap.Location = new System.Drawing.Point(230, 6);
			this.cmdMainNap.Name = "cmdMainNap";
			this.cmdMainNap.Size = new System.Drawing.Size(88, 22);
			this.cmdMainNap.TabIndex = 2;
			this.cmdMainNap.Text = "Nap >>";
			this.cmdMainNap.Click += new System.EventHandler(this.cmdMainNap_Click);
			// 
			// txtMainNapLen
			// 
			this.txtMainNapLen.Location = new System.Drawing.Point(98, 6);
			this.txtMainNapLen.Name = "txtMainNapLen";
			this.txtMainNapLen.Size = new System.Drawing.Size(56, 22);
			this.txtMainNapLen.TabIndex = 1;
			// 
			// lblMainNap
			// 
			this.lblMainNap.AutoSize = true;
			this.lblMainNap.Location = new System.Drawing.Point(6, 9);
			this.lblMainNap.Name = "lblMainNap";
			this.lblMainNap.Size = new System.Drawing.Size(76, 16);
			this.lblMainNap.TabIndex = 0;
			this.lblMainNap.Text = "Nap length:";
			// 
			// tabSetup
			// 
			this.tabSetup.Controls.Add(this.txtSoundFadeInLen);
			this.tabSetup.Controls.Add(this.lblSoundFadeInLen);
			this.tabSetup.Controls.Add(this.cmdSetupBrowseAppData);
			this.tabSetup.Controls.Add(this.txtSoundWarningLen);
			this.tabSetup.Controls.Add(this.lblSoundWarningLen);
			this.tabSetup.Controls.Add(this.tbrSoundAlarm);
			this.tabSetup.Controls.Add(this.tbrSoundWarning);
			this.tabSetup.Controls.Add(this.txtSoundAlarmFile);
			this.tabSetup.Controls.Add(this.txtSoundWarningFile);
			this.tabSetup.Controls.Add(this.lblSoundAlarm);
			this.tabSetup.Controls.Add(this.lblSoundWarning);
			this.tabSetup.Controls.Add(this.txtSetupCoreCooldown);
			this.tabSetup.Controls.Add(this.txtSetupCoreLen);
			this.tabSetup.Controls.Add(this.txtSetupNapCooldown);
			this.tabSetup.Controls.Add(this.txtSetupNapLen);
			this.tabSetup.Controls.Add(this.lblSetupCoreCooldown);
			this.tabSetup.Controls.Add(this.lblSetupCoreLen);
			this.tabSetup.Controls.Add(this.lblSetupNapCooldown);
			this.tabSetup.Controls.Add(this.lblSetupNapLen);
			this.tabSetup.Location = new System.Drawing.Point(4, 25);
			this.tabSetup.Name = "tabSetup";
			this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
			this.tabSetup.Size = new System.Drawing.Size(324, 224);
			this.tabSetup.TabIndex = 1;
			this.tabSetup.Text = "Setup";
			this.tabSetup.UseVisualStyleBackColor = true;
			// 
			// txtSoundFadeInLen
			// 
			this.txtSoundFadeInLen.Location = new System.Drawing.Point(290, 112);
			this.txtSoundFadeInLen.Name = "txtSoundFadeInLen";
			this.txtSoundFadeInLen.Size = new System.Drawing.Size(28, 22);
			this.txtSoundFadeInLen.TabIndex = 19;
			// 
			// lblSoundFadeInLen
			// 
			this.lblSoundFadeInLen.AutoSize = true;
			this.lblSoundFadeInLen.Location = new System.Drawing.Point(173, 115);
			this.lblSoundFadeInLen.Name = "lblSoundFadeInLen";
			this.lblSoundFadeInLen.Size = new System.Drawing.Size(111, 16);
			this.lblSoundFadeInLen.TabIndex = 18;
			this.lblSoundFadeInLen.Text = "Fade in seconds:";
			// 
			// cmdSetupBrowseAppData
			// 
			this.cmdSetupBrowseAppData.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSetupBrowseAppData.Location = new System.Drawing.Point(6, 194);
			this.cmdSetupBrowseAppData.Name = "cmdSetupBrowseAppData";
			this.cmdSetupBrowseAppData.Size = new System.Drawing.Size(160, 24);
			this.cmdSetupBrowseAppData.TabIndex = 17;
			this.cmdSetupBrowseAppData.Text = "Open AppData Folder";
			this.cmdSetupBrowseAppData.Click += new System.EventHandler(this.cmdSetupBrowseAppData_Click);
			// 
			// txtSoundWarningLen
			// 
			this.txtSoundWarningLen.Location = new System.Drawing.Point(128, 112);
			this.txtSoundWarningLen.Name = "txtSoundWarningLen";
			this.txtSoundWarningLen.Size = new System.Drawing.Size(28, 22);
			this.txtSoundWarningLen.TabIndex = 16;
			// 
			// lblSoundWarningLen
			// 
			this.lblSoundWarningLen.AutoSize = true;
			this.lblSoundWarningLen.Location = new System.Drawing.Point(6, 115);
			this.lblSoundWarningLen.Name = "lblSoundWarningLen";
			this.lblSoundWarningLen.Size = new System.Drawing.Size(116, 16);
			this.lblSoundWarningLen.TabIndex = 15;
			this.lblSoundWarningLen.Text = "Warning seconds:";
			// 
			// tbrSoundAlarm
			// 
			this.tbrSoundAlarm.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton4,
            this.toolBarButton5,
            this.toolBarButton6});
			this.tbrSoundAlarm.Divider = false;
			this.tbrSoundAlarm.Dock = System.Windows.Forms.DockStyle.None;
			this.tbrSoundAlarm.DropDownArrows = true;
			this.tbrSoundAlarm.ImageList = this.imlImages;
			this.tbrSoundAlarm.Location = new System.Drawing.Point(246, 162);
			this.tbrSoundAlarm.Name = "tbrSoundAlarm";
			this.tbrSoundAlarm.ShowToolTips = true;
			this.tbrSoundAlarm.Size = new System.Drawing.Size(72, 26);
			this.tbrSoundAlarm.TabIndex = 14;
			this.tbrSoundAlarm.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrSoundAlarm_ButtonClick);
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.ImageIndex = 0;
			this.toolBarButton4.Name = "toolBarButton4";
			this.toolBarButton4.ToolTipText = "Browse";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.ImageIndex = 1;
			this.toolBarButton5.Name = "toolBarButton5";
			this.toolBarButton5.ToolTipText = "Preview";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.ImageIndex = 2;
			this.toolBarButton6.Name = "toolBarButton6";
			this.toolBarButton6.ToolTipText = "Stop";
			// 
			// imlImages
			// 
			this.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imlImages.ImageSize = new System.Drawing.Size(16, 16);
			this.imlImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbrSoundWarning
			// 
			this.tbrSoundWarning.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarButton3});
			this.tbrSoundWarning.Divider = false;
			this.tbrSoundWarning.Dock = System.Windows.Forms.DockStyle.None;
			this.tbrSoundWarning.DropDownArrows = true;
			this.tbrSoundWarning.ImageList = this.imlImages;
			this.tbrSoundWarning.Location = new System.Drawing.Point(246, 84);
			this.tbrSoundWarning.Name = "tbrSoundWarning";
			this.tbrSoundWarning.ShowToolTips = true;
			this.tbrSoundWarning.Size = new System.Drawing.Size(72, 26);
			this.tbrSoundWarning.TabIndex = 13;
			this.tbrSoundWarning.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrSoundWarning_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.Name = "toolBarButton1";
			this.toolBarButton1.ToolTipText = "Browse";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.ImageIndex = 1;
			this.toolBarButton2.Name = "toolBarButton2";
			this.toolBarButton2.ToolTipText = "Preview";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.ImageIndex = 2;
			this.toolBarButton3.Name = "toolBarButton3";
			this.toolBarButton3.ToolTipText = "Stop";
			// 
			// txtSoundAlarmFile
			// 
			this.txtSoundAlarmFile.Location = new System.Drawing.Point(6, 162);
			this.txtSoundAlarmFile.Name = "txtSoundAlarmFile";
			this.txtSoundAlarmFile.Size = new System.Drawing.Size(234, 22);
			this.txtSoundAlarmFile.TabIndex = 12;
			// 
			// txtSoundWarningFile
			// 
			this.txtSoundWarningFile.Location = new System.Drawing.Point(6, 84);
			this.txtSoundWarningFile.Name = "txtSoundWarningFile";
			this.txtSoundWarningFile.Size = new System.Drawing.Size(234, 22);
			this.txtSoundWarningFile.TabIndex = 11;
			// 
			// lblSoundAlarm
			// 
			this.lblSoundAlarm.AutoSize = true;
			this.lblSoundAlarm.Location = new System.Drawing.Point(6, 143);
			this.lblSoundAlarm.Name = "lblSoundAlarm";
			this.lblSoundAlarm.Size = new System.Drawing.Size(86, 16);
			this.lblSoundAlarm.TabIndex = 10;
			this.lblSoundAlarm.Text = "Alarm sound:";
			// 
			// lblSoundWarning
			// 
			this.lblSoundWarning.AutoSize = true;
			this.lblSoundWarning.Location = new System.Drawing.Point(6, 65);
			this.lblSoundWarning.Name = "lblSoundWarning";
			this.lblSoundWarning.Size = new System.Drawing.Size(101, 16);
			this.lblSoundWarning.TabIndex = 9;
			this.lblSoundWarning.Text = "Warning sound:";
			// 
			// txtSetupCoreCooldown
			// 
			this.txtSetupCoreCooldown.Location = new System.Drawing.Point(262, 34);
			this.txtSetupCoreCooldown.Name = "txtSetupCoreCooldown";
			this.txtSetupCoreCooldown.Size = new System.Drawing.Size(56, 22);
			this.txtSetupCoreCooldown.TabIndex = 7;
			// 
			// txtSetupCoreLen
			// 
			this.txtSetupCoreLen.Location = new System.Drawing.Point(98, 34);
			this.txtSetupCoreLen.Name = "txtSetupCoreLen";
			this.txtSetupCoreLen.Size = new System.Drawing.Size(56, 22);
			this.txtSetupCoreLen.TabIndex = 6;
			// 
			// txtSetupNapCooldown
			// 
			this.txtSetupNapCooldown.Location = new System.Drawing.Point(262, 6);
			this.txtSetupNapCooldown.Name = "txtSetupNapCooldown";
			this.txtSetupNapCooldown.Size = new System.Drawing.Size(56, 22);
			this.txtSetupNapCooldown.TabIndex = 5;
			// 
			// txtSetupNapLen
			// 
			this.txtSetupNapLen.Location = new System.Drawing.Point(98, 6);
			this.txtSetupNapLen.Name = "txtSetupNapLen";
			this.txtSetupNapLen.Size = new System.Drawing.Size(56, 22);
			this.txtSetupNapLen.TabIndex = 4;
			// 
			// lblSetupCoreCooldown
			// 
			this.lblSetupCoreCooldown.AutoSize = true;
			this.lblSetupCoreCooldown.Location = new System.Drawing.Point(161, 37);
			this.lblSetupCoreCooldown.Name = "lblSetupCoreCooldown";
			this.lblSetupCoreCooldown.Size = new System.Drawing.Size(95, 16);
			this.lblSetupCoreCooldown.TabIndex = 3;
			this.lblSetupCoreCooldown.Text = "and cooldown:";
			// 
			// lblSetupCoreLen
			// 
			this.lblSetupCoreLen.AutoSize = true;
			this.lblSetupCoreLen.Location = new System.Drawing.Point(6, 37);
			this.lblSetupCoreLen.Name = "lblSetupCoreLen";
			this.lblSetupCoreLen.Size = new System.Drawing.Size(79, 16);
			this.lblSetupCoreLen.TabIndex = 2;
			this.lblSetupCoreLen.Text = "Core length:";
			// 
			// lblSetupNapCooldown
			// 
			this.lblSetupNapCooldown.AutoSize = true;
			this.lblSetupNapCooldown.Location = new System.Drawing.Point(161, 9);
			this.lblSetupNapCooldown.Name = "lblSetupNapCooldown";
			this.lblSetupNapCooldown.Size = new System.Drawing.Size(95, 16);
			this.lblSetupNapCooldown.TabIndex = 1;
			this.lblSetupNapCooldown.Text = "and cooldown:";
			// 
			// lblSetupNapLen
			// 
			this.lblSetupNapLen.AutoSize = true;
			this.lblSetupNapLen.Location = new System.Drawing.Point(6, 9);
			this.lblSetupNapLen.Name = "lblSetupNapLen";
			this.lblSetupNapLen.Size = new System.Drawing.Size(76, 16);
			this.lblSetupNapLen.TabIndex = 0;
			this.lblSetupNapLen.Text = "Nap length:";
			// 
			// cmdExit
			// 
			this.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdExit.Location = new System.Drawing.Point(248, 271);
			this.cmdExit.Name = "cmdExit";
			this.cmdExit.Size = new System.Drawing.Size(96, 32);
			this.cmdExit.TabIndex = 1;
			this.cmdExit.Text = "Exit";
			this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
			// 
			// cmdToTray
			// 
			this.cmdToTray.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdToTray.Location = new System.Drawing.Point(146, 271);
			this.cmdToTray.Name = "cmdToTray";
			this.cmdToTray.Size = new System.Drawing.Size(96, 32);
			this.cmdToTray.TabIndex = 2;
			this.cmdToTray.Text = "Move to tray";
			this.cmdToTray.Click += new System.EventHandler(this.cmdToTray_Click);
			// 
			// cmdAbout
			// 
			this.cmdAbout.Location = new System.Drawing.Point(44, 271);
			this.cmdAbout.Name = "cmdAbout";
			this.cmdAbout.Size = new System.Drawing.Size(96, 32);
			this.cmdAbout.TabIndex = 3;
			this.cmdAbout.Text = "About";
			this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
			// 
			// mnuTray
			// 
			this.mnuTray.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuTrayShow,
            this.mnuTrayExit});
			// 
			// mnuTrayShow
			// 
			this.mnuTrayShow.DefaultItem = true;
			this.mnuTrayShow.Index = 0;
			this.mnuTrayShow.Text = "&Show";
			this.mnuTrayShow.Click += new System.EventHandler(this.mnuTrayShow_Click);
			// 
			// mnuTrayExit
			// 
			this.mnuTrayExit.Index = 1;
			this.mnuTrayExit.Text = "E&xit";
			this.mnuTrayExit.Click += new System.EventHandler(this.mnuTrayExit_Click);
			// 
			// lnkHistory
			// 
			this.lnkHistory.AutoSize = true;
			this.lnkHistory.Location = new System.Drawing.Point(6, 95);
			this.lnkHistory.Name = "lnkHistory";
			this.lnkHistory.Size = new System.Drawing.Size(53, 16);
			this.lnkHistory.TabIndex = 10;
			this.lnkHistory.TabStop = true;
			this.lnkHistory.Text = "History:";
			this.lnkHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHistory_LinkClicked);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(356, 315);
			this.Controls.Add(this.cmdAbout);
			this.Controls.Add(this.cmdToTray);
			this.Controls.Add(this.cmdExit);
			this.Controls.Add(this.Tabs);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Polyriser";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Tabs.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabMain.PerformLayout();
			this.tabSetup.ResumeLayout(false);
			this.tabSetup.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl Tabs;
		private System.Windows.Forms.TabPage tabMain;
		private System.Windows.Forms.Button cmdMainCore;
		private System.Windows.Forms.TextBox txtMainCoreLen;
		private System.Windows.Forms.Label lblMainCore;
		private System.Windows.Forms.Button cmdMainNap;
		private System.Windows.Forms.TextBox txtMainNapLen;
		private System.Windows.Forms.Label lblMainNap;
		private System.Windows.Forms.TabPage tabSetup;
		private System.Windows.Forms.TextBox txtSetupCoreCooldown;
		private System.Windows.Forms.TextBox txtSetupCoreLen;
		private System.Windows.Forms.TextBox txtSetupNapCooldown;
		private System.Windows.Forms.TextBox txtSetupNapLen;
		private System.Windows.Forms.Label lblSetupCoreCooldown;
		private System.Windows.Forms.Label lblSetupCoreLen;
		private System.Windows.Forms.Label lblSetupNapCooldown;
		private System.Windows.Forms.Label lblSetupNapLen;
		private System.Windows.Forms.TextBox txtHistory;
		private System.Windows.Forms.Label lblMainForce;
		private System.Windows.Forms.Button cmdMainTestOptions;
		private System.Windows.Forms.ComboBox cboMainTestMethod;
		private System.Windows.Forms.Button cmdExit;
		private System.Windows.Forms.LinkLabel lnkMainCoreLen;
		private System.Windows.Forms.LinkLabel lnkMainNapLen;
		private System.Windows.Forms.TextBox txtSoundWarningLen;
		private System.Windows.Forms.Label lblSoundWarningLen;
		private System.Windows.Forms.ToolBar tbrSoundAlarm;
		private System.Windows.Forms.ToolBar tbrSoundWarning;
		private System.Windows.Forms.TextBox txtSoundAlarmFile;
		private System.Windows.Forms.TextBox txtSoundWarningFile;
		private System.Windows.Forms.Label lblSoundAlarm;
		private System.Windows.Forms.Label lblSoundWarning;
		private System.Windows.Forms.ImageList imlImages;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.Button cmdToTray;
		private System.Windows.Forms.Button cmdSetupBrowseAppData;
		private System.Windows.Forms.TextBox txtSoundFadeInLen;
		private System.Windows.Forms.Label lblSoundFadeInLen;
		private System.Windows.Forms.Button cmdAbout;
		private System.Windows.Forms.ContextMenu mnuTray;
		private System.Windows.Forms.MenuItem mnuTrayShow;
		private System.Windows.Forms.MenuItem mnuTrayExit;
		private System.Windows.Forms.LinkLabel lnkHistory;
	}
}

