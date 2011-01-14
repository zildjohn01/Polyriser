using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Polyriser {
	sealed partial class MainForm : Form {
		Engine _engine;
		NotifyIcon _tray;
		bool _okToExit;
		object _testData;
		MciAudio _previewer;


		public MainForm(Engine engine) {
			InitializeComponent();

			Text = App.GetProductName() + " " + App.GetVersionString(false);
#if DEBUG
			Text += Strings.DebugSuffix;
#endif
			Icon = Properties.Resources.App;

			_engine = engine;
			_engine.Invoker = this;
			_engine.Hook += EngineHook;

			_tray = new NotifyIcon();
			_tray.MouseClick += TrayClick;
			_tray.Text = this.Text;
			_tray.Icon = this.Icon;
			_tray.ContextMenu = mnuTray;

			_previewer = new MciAudio();

			lnkMainNapLen.Bounds = txtMainNapLen.Bounds;
			lnkMainCoreLen.Bounds = txtMainCoreLen.Bounds;
			cboMainTestMethod.SelectedIndex = 0;

			imlImages.Images.Add(Properties.Resources.Open);
			imlImages.Images.Add(Properties.Resources.Play);
			imlImages.Images.Add(Properties.Resources.Stop);

			RefreshSettings();
		}


		void RefreshSettings() {
			lnkMainNapLen.Text = App.TimeToStringHHMM(App.Settings.NapLength);
			lnkMainNapLen.Show();
			lnkMainCoreLen.Text = App.TimeToStringHHMM(App.Settings.CoreLength);
			lnkMainCoreLen.Show();
			chkVitalTests.Checked = App.Settings.VitalEnabled;

			txtSetupNapLen.Text = App.TimeToStringHHMM(App.Settings.NapLength);
			txtSetupNapCooldown.Text = App.TimeToStringHHMM(App.Settings.NapCooldown);
			txtSetupCoreLen.Text = App.TimeToStringHHMM(App.Settings.CoreLength);
			txtSetupCoreCooldown.Text = App.TimeToStringHHMM(App.Settings.CoreCooldown);
			txtSoundWarningFile.Text = App.Settings.WarningSoundFile;
			txtSoundAlarmFile.Text = App.Settings.AlarmSoundFile;
			txtSoundWarningLen.Text = App.Settings.WarningSoundLength.TotalSeconds.ToString();
			txtSoundFadeInLen.Text = App.Settings.WarningSoundFadeIn.TotalSeconds.ToString();
		}

		bool StoreSettings() {
			TimeSpan time;
			int num;
			string fail = null;

			if(!App.TryParseHHMM(txtSetupNapLen.Text, out time))
				fail = fail ?? "NapLen";
			App.Settings.NapLength = time;

			if(!App.TryParseHHMM(txtSetupNapCooldown.Text, out time))
				fail = fail ?? "NapCooldown";
			App.Settings.NapCooldown = time;

			if(!App.TryParseHHMM(txtSetupCoreLen.Text, out time))
				fail = fail ?? "CoreLen";
			App.Settings.CoreLength = time;

			if(!App.TryParseHHMM(txtSetupCoreCooldown.Text, out time))
				fail = fail ?? "CoreCooldown";
			App.Settings.CoreCooldown = time;

			App.Settings.VitalEnabled = chkVitalTests.Checked;

			App.Settings.WarningSoundFile = txtSoundWarningFile.Text;
			App.Settings.AlarmSoundFile = txtSoundAlarmFile.Text;

			if(!int.TryParse(txtSoundWarningLen.Text, out num))
				fail = fail ?? "SoundWarningLen";
			App.Settings.WarningSoundLength = new TimeSpan(0, 0, num);

			if(!int.TryParse(txtSoundFadeInLen.Text, out num))
				fail = fail ?? "SoundFadeInLen";
			App.Settings.WarningSoundFadeIn = new TimeSpan(0, 0, num);

			if(fail != null)
				return StoreSettingsFail(fail);

			if(!App.Settings.SaveToFile()) {
				if(MessageBox.Show(this, "Failed to save settings. Continue anyway?",
							this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
						!= DialogResult.Yes)
					return false;

			}
			return true;
		}

		bool StoreSettingsFail(string desc) {
			_engine.LoadSettingsFailed(desc);
			return false;
		}


		void EngineHook(object sender, EngineEventArgs e) {
			switch(e.Event) {
			case EngineEvent.LoadSettings:
				RefreshSettings();
				break;

			case EngineEvent.LoadSettingsFailed:
				Tabs.SelectedTab = tabSetup;

				switch((string)e.Data) {
				case "NapLen":
					App.FlashTextBox(txtSetupNapLen, Strings.InvalidTimeFlash);
					break;
				case "NapCooldown":
					App.FlashTextBox(txtSetupNapCooldown, Strings.InvalidTimeFlash);
					break;
				case "CoreLen":
					App.FlashTextBox(txtSetupCoreLen, Strings.InvalidTimeFlash);
					break;
				case "CoreCooldown":
					App.FlashTextBox(txtSetupCoreCooldown, Strings.InvalidTimeFlash);
					break;
				case "SoundWarningFile":
					App.FlashTextBox(txtSoundWarningFile, Strings.CantOpenAudioFile);
					break;
				case "SoundAlarmFile":
					App.FlashTextBox(txtSoundAlarmFile, Strings.CantOpenAudioFile);
					break;
				case "SoundWarningLen":
					App.FlashTextBox(txtSoundWarningLen, "!!!");
					break;
				case "SoundFadeInLen":
					App.FlashTextBox(txtSoundFadeInLen, "!!!");
					break;
				}
				break;

			case EngineEvent.VitalTest:
				new TestForm(_engine).DoTest(this, TestMethod.VitalTest, null);
				break;

			case EngineEvent.CooldownBegin:
				cmdExit.Text = Strings.ExitCoolingDown;
				break;
			case EngineEvent.CooldownDone:
				cmdExit.Text = Strings.ExitIdle;
				break;

			case EngineEvent.LogMessage: {
				var data = (LogData)e.Data;
				txtHistory.AppendText(data.Time.ToString("HH:mm:ss "));
				txtHistory.AppendText(data.Message);
				txtHistory.AppendText(Environment.NewLine);
				txtHistory.Select(txtHistory.TextLength, 0);
				break;
			}}
		}


		void Tabs_Deselecting(object sender, TabControlCancelEventArgs e) {
			if(e.TabPage == tabSetup)
				if(!StoreSettings())
					e.Cancel = true;
		}

		void Tabs_Selecting(object sender, TabControlCancelEventArgs e) {
			if(e.TabPage == tabMain)
				_engine.RaiseEvent(EngineEvent.LoadSettings);
			else if(e.TabPage == tabSetup)
				RefreshSettings();
		}


		void lnkMainNapLen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			HideLinkShowText(lnkMainNapLen, txtMainNapLen, App.Settings.NapLength);
		}

		void lnkMainCoreLen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			HideLinkShowText(lnkMainCoreLen, txtMainCoreLen, App.Settings.CoreLength);
		}

		void HideLinkShowText(LinkLabel link, TextBox text, TimeSpan length) {
			text.Text = App.TimeToStringHHMM(length);
			text.SelectAll();
			text.Show();
			text.Focus();
			link.Hide();
		}


		void cmdMainNap_Click(object sender, EventArgs e) {
			DoNap(lnkMainNapLen, txtMainNapLen, App.Settings.NapCooldown);
		}

		void cmdMainCore_Click(object sender, EventArgs e) {
			DoNap(lnkMainCoreLen, txtMainCoreLen, App.Settings.CoreCooldown);
		}

		void DoNap(LinkLabel link, TextBox text, TimeSpan cooldown) {
			if(!_engine.AllowedToNapOrExit) {
				_engine.RaiseEvent(EngineEvent.TriedToNapTooSoon);
				return;
			}

			var strControl = link.Visible ? (Control)link : text;
			TimeSpan length;

			if(!App.TryParseHHMM(strControl.Text, out length)) {
				App.Assert(strControl == text);
				App.FlashTextBox(text, Strings.InvalidTimeFlash);
				return;
			}

			var napForm = new NappingForm(_engine);
			try {
				Hide();
				napForm.DoNap(null, length, cooldown,
					(TestMethod)cboMainTestMethod.SelectedIndex, _testData);
			} finally {
				Show();
			}
		}


		private void chkVitalTests_CheckedChanged(object sender, EventArgs e) {
			_engine.EnableVitalChecks(chkVitalTests.Checked);
		}

		private void cmdVitalInfo_Click(object sender, EventArgs e) {
			MessageBox.Show(this, "I'll ask you to click a button every so often, just to " +
				"make sure you're still awake." + Environment.NewLine + Environment.NewLine +
				"Make sure you uncheck this before you leave the house!",
				this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		private void lnkHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var info = new ProcessStartInfo();
			info.FileName = Path.Combine(App.StoragePath, Strings.LogFile);
			info.UseShellExecute = true;
			try {
				Process.Start(info);
			} catch {
				MessageBox.Show(this, "Error opening the log file:" +
					Environment.NewLine + Environment.NewLine + info.FileName,
					this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void cmdSetupBrowseAppData_Click(object sender, EventArgs e) {
			var info = new ProcessStartInfo();
			info.FileName = App.StoragePath;
			info.UseShellExecute = true;
			try {
				Directory.CreateDirectory(App.StoragePath);
				Process.Start(info);
			} catch {
				MessageBox.Show(this, "Couldn't open the folder:" +
					Environment.NewLine + Environment.NewLine + App.StoragePath,
					this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}


		void cboMainTestMethod_SelectedIndexChanged(object sender, EventArgs e) {
			var method = (TestMethod)cboMainTestMethod.SelectedIndex;
			cmdMainTestOptions.Enabled = method == TestMethod.Repeat;
			if(cmdMainTestOptions.Enabled)
				cmdMainTestOptions.PerformClick();
		}

		private void cmdMainTestOptions_Click(object sender, EventArgs e) {
			string text;

			var prompt = new TestForm(_engine);
			if(!prompt.PromptForText(this, "Enter the desired text:", out text))
				return;
			_testData = text;
		}

		private void tbrSoundWarning_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
			// Sorry for the nondescript button names. Windows Forms designer
			// reverts back to these excellent names every time I modify the form...
			if(e.Button == toolBarButton1)
				SoundOpen(txtSoundWarningFile);
			else if(e.Button == toolBarButton2)
				SoundPreview(txtSoundWarningFile);
			else if(e.Button == toolBarButton3)
				SoundStop();
		}

		private void tbrSoundAlarm_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
			if(e.Button == toolBarButton4)
				SoundOpen(txtSoundAlarmFile);
			else if(e.Button == toolBarButton5)
				SoundPreview(txtSoundAlarmFile);
			else if(e.Button == toolBarButton6)
				SoundStop();
		}

		void SoundOpen(TextBox dest) {
			var ofd = new OpenFileDialog();
			ofd.Filter = "Audio files (*.wav; *.mp3)|*.wav;*.mp3|All files (*.*)|*.*";
			if(ofd.ShowDialog(this) != DialogResult.OK)
				return;
			dest.Text = ofd.FileName;
		}

		void SoundPreview(TextBox source) {
			if(!_previewer.TryOpen(source.Text)) {
				App.FlashTextBox(source, Strings.CantOpenAudioFile);
				return;
			}
			_previewer.Play();
		}

		void SoundStop() {
			_previewer.Stop();
		}


		void TrayClick(object sender, MouseEventArgs e) {
			if(e.Button == MouseButtons.Left)
				mnuTrayShow.PerformClick();
		}

		private void mnuTrayShow_Click(object sender, EventArgs e) {
			TrayAnimation.FromTray(this);
			Show();
			_tray.Visible = false;
		}

		private void mnuTrayExit_Click(object sender, EventArgs e) {
			cmdExit_Click(sender, e);
		}


		private void cmdAbout_Click(object sender, EventArgs e) {
			MessageBox.Show(this,
				App.GetProductName() + " " + App.GetVersionString(true) +
					Environment.NewLine + Environment.NewLine + App.GetCopyrightString() +
					Environment.NewLine + Environment.NewLine + "http://johnsoft.com/polyriser",
				"About", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void cmdToTray_Click(object sender, EventArgs e) {
			TrayAnimation.ToTray(this);
			Visible = false;
			_tray.Visible = true;
			if(!App.Settings.ShowedFirstTrayBalloon) {
				_tray.BalloonTipTitle = this.Text;
				_tray.BalloonTipIcon = ToolTipIcon.Info;
				_tray.BalloonTipText = Strings.TrayFirstMessage;
				_tray.ShowBalloonTip(5000);
				App.Settings.ShowedFirstTrayBalloon = true;
				App.Settings.SaveToFile();
			}
		}

		void cmdExit_Click(object sender, EventArgs e) {
			_okToExit = true;
			Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			if(e.CloseReason == CloseReason.WindowsShutDown)
				return;

			if(!_okToExit) {
				cmdToTray.PerformClick();
				e.Cancel = true;
				return;
			}

			if(!_engine.AllowedToNapOrExit) {
				_okToExit = false;
				_engine.RaiseEvent(EngineEvent.TriedToExitTooSoon);
				e.Cancel = true;
			}
		}
	}
}