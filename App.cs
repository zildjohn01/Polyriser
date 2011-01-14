using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Polyriser {
	public delegate void DelayedAction();


	static class App {
		static Engine _engine;
		public static Settings Settings {get; private set;}


		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if(!CheckSingleInstance())
				return;

			Settings = new Settings();
			Settings.LoadFromFile();

			_engine = new Engine();
			new Logger(_engine);

			// This is created before the engine is init'd so it can receive and
			// display messages that happen while the engine is loading
			var mainForm = new MainForm(_engine);

			new Sounder(_engine);
			_engine.RaiseEvent(EngineEvent.Init);
			_engine.RaiseEvent(EngineEvent.LoadSettings);

			try {
				Application.Run(mainForm);
			} finally {
				_engine.RaiseEvent(EngineEvent.Shutdown);
			}
		}

		static bool CheckSingleInstance() {
			var list = Process.GetProcesses();
			var current = Process.GetCurrentProcess();
			foreach(var existing in list)
				try {
					if(existing.MainModule.FileName == current.MainModule.FileName
					&& existing.Id != current.Id)
						return false;
				} catch {
					// Hey, what can you do...
				}

			return true;
		}


		public static void Assert(bool condition) {
			Debug.Assert(condition);
		}


		public static string StoragePath {get {
			var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			return Path.Combine(path, Strings.DataPath);
		}}

		public static string SettingsFilename {get {
			return Path.Combine(StoragePath, Strings.SettingsFile);
		}}

		public static string LogFilename {get {
			return Path.Combine(StoragePath, Strings.LogFile);
		}}


		public static string TimeToStringHHMM(TimeSpan time) {
			return string.Format("{0}:{1:00.##}",
				time.Hours, time.TotalMinutes - time.Hours * 60);
		}

		public static string TimeToStringHHMMSS(TimeSpan time) {
			if(time.Hours > 0)
				return string.Format("{0}:{1:D2}:{2:00.##}",
					time.Hours, time.Minutes, time.TotalSeconds - time.Minutes * 60);
			return string.Format("{0}:{1:00.##}",
				time.Minutes, time.TotalSeconds - time.Minutes * 60);
		}

		public static bool TryParseHHMM(string text, out TimeSpan result) {
			result = default(TimeSpan);
			int seconds = 0;

			var colonPos = text.IndexOf(':');
			if(colonPos != -1) {
				int tmpint;
				if(!int.TryParse(text.Substring(0, colonPos), out tmpint))
					return false;
				seconds += tmpint * 60 * 60;
			}

			double tmpdbl;
			if(!double.TryParse(text.Substring(colonPos + 1), out tmpdbl))
				return false;
			seconds += (int)(tmpdbl * 60);

			result = new TimeSpan(0, 0, seconds);
			return true;
		}


		public static void FlashControl(Control control, string message) {
			var oldText = control.Text;
			var oldForeColor = control.ForeColor;
			var oldFont = control.Font;
			try {
				control.Text = message;
				control.ForeColor = Color.FromArgb(0x00e00000);
				var newSize = (float)Math.Pow(oldFont.Size, 1.8) / 5.7;  // please don't ask
				control.Font = new Font(oldFont.FontFamily, oldFont.Size * 1.25f, FontStyle.Bold);
				control.Refresh();
				Thread.Sleep(750);
				control.Text = oldText;
			} finally {
				control.Text = oldText;
				control.ForeColor = oldForeColor;
				control.Font = oldFont;
				control.Focus();
			}
		}

		public static void FlashTextBox(TextBox textBox, string message) {
			var align = textBox.TextAlign;
			try {
				textBox.TextAlign = HorizontalAlignment.Center;
				FlashControl(textBox, message);
			} finally {
				textBox.TextAlign = align;
				textBox.SelectAll();
			}
		}


		public static string GetCopyrightString() {
			var attr = (AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly()
				.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
			return attr.Copyright;
		}

		public static string GetProductName() {
			var attr = (AssemblyProductAttribute)Assembly.GetExecutingAssembly()
				.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
			return attr.Product;
		}

		public static string GetVersionString(bool includeBuild) {
			var ver = Assembly.GetExecutingAssembly().GetName().Version;
			var ret = string.Format("{0}.{1:D2}", ver.Major, ver.Minor);
			if(includeBuild)
				ret += string.Format(".{0:D4}", ver.Build);
			return ret;
		}
	}


	static class Strings {
		public const string DebugSuffix = "   *** DEBUG VERSION ***";

		public static string DataPath {get {return App.GetProductName();}}
#if DEBUG
		public const string SettingsFile = "settings-testing.ini";
		public const string LogFile = "log-testing.txt";
#else
		public const string SettingsFile = "settings.ini";
		public const string LogFile = "log.txt";
#endif

		public const string NapStopPreGrace = "Cancel nap";
		public const string NapStopPostGrace = "I'm awake";
		public const string ExitCoolingDown = "Not yet...";
		public const string ExitIdle = "Exit";

		public const string TrayFirstMessage = "I'll stay out of your way!";

		public const string InvalidTimeFlash = "h:mm";
		public const string CantOpenAudioFile = "Couldn't open file";
		public const string FailedTest = "Wake up!!";
	}
}
