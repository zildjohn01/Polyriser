using System;
using System.IO;


namespace Polyriser {
	sealed class Settings {
		public TimeSpan NapLength {get; set;}
		public TimeSpan NapCooldown {get; set;}
		public TimeSpan CoreLength {get; set;}
		public TimeSpan CoreCooldown {get; set;}
		public bool VitalEnabled {get; set;}
		public TimeSpan VitalInitial {get; set;}
		public TimeSpan VitalPeriod {get; set;}
		public DateTime NextNapAllowed {get; set;}
		public string WarningSoundFile {get; set;}
		public TimeSpan WarningSoundLength {get; set;}
		public TimeSpan WarningSoundFadeIn {get; set;}
		public string AlarmSoundFile {get; set;}
		public bool ShowedFirstTrayBalloon {get; set;}


		public void LoadFromFile() {
			// First, apply defaults, then (maybe) overwrite them with the loaded ini
#if DEBUG
			NapLength = new TimeSpan(0, 0, 6);
			NapCooldown = new TimeSpan(0, 0, 6);
			WarningSoundLength = new TimeSpan(0, 0, 4);
			WarningSoundFadeIn = new TimeSpan(0, 0, 8);
			VitalInitial = new TimeSpan(0, 0, 5);
			VitalPeriod = new TimeSpan(0, 0, 10);
#else
			NapLength = new TimeSpan(0, 22, 0);
			NapCooldown = new TimeSpan(0, 35, 0);
			WarningSoundLength = new TimeSpan(0, 0, 60);
			WarningSoundFadeIn = new TimeSpan(0, 0, 30);
			VitalInitial = new TimeSpan(0, 5, 0);
			VitalPeriod = new TimeSpan(0, 65, 0);
#endif
			VitalEnabled = false;
			CoreLength = new TimeSpan(3, 15, 0);
			CoreCooldown = new TimeSpan(0, 50, 0);
			WarningSoundFile = "warning.wav";
			AlarmSoundFile = "loud.wav";

			FileStream iniFile = null;

			try {
				iniFile = new FileStream(App.SettingsFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
				var ini = new IniReader(iniFile);
				ini.Read();

				var misc = ini.GetSection("misc");
				if(misc != null) {
					ShowedFirstTrayBalloon = misc.GetValue("tray-cherry") == "popped";
				}

				var times = ini.GetSection("times");
				if(times != null) {
					NapLength = new TimeSpan(0, 0,
						TryGetInt(times, "nap-length", (int)NapLength.TotalSeconds));
					NapCooldown = new TimeSpan(0, 0,
						TryGetInt(times, "nap-cooldown", (int)NapCooldown.TotalSeconds));
					CoreLength = new TimeSpan(0, 0,
						TryGetInt(times, "core-length", (int)CoreLength.TotalSeconds));
					CoreCooldown = new TimeSpan(0, 0,
						TryGetInt(times, "core-cooldown", (int)CoreCooldown.TotalSeconds));
					VitalEnabled = TryGetBool(times, "vital-enabled", VitalEnabled);
					VitalInitial = new TimeSpan(0, 0,
						TryGetInt(times, "vital-initial", (int)VitalInitial.TotalSeconds));
					VitalPeriod = new TimeSpan(0, 0,
						TryGetInt(times, "vital-period", (int)VitalPeriod.TotalSeconds));
					DateTime dt;
					if(DateTime.TryParse(times.GetValue("next-nap-allowed"), out dt))
						NextNapAllowed = dt;
					else
						NextNapAllowed = DateTime.MinValue;
				}

				var sounds = ini.GetSection("sounds");
				if(sounds != null) {
					WarningSoundFile = sounds.GetValue("warning-file") ?? WarningSoundFile;
					WarningSoundLength = new TimeSpan(0, 0,
						TryGetInt(sounds, "warning-length", (int)WarningSoundLength.TotalSeconds));
					AlarmSoundFile = sounds.GetValue("alarm-file") ?? AlarmSoundFile;
					WarningSoundFadeIn = new TimeSpan(0, 0,
						TryGetInt(sounds, "fadein-time", (int)WarningSoundFadeIn.TotalSeconds));
				}
			} catch {
				// Eh, whatever
			} finally {
				if(iniFile != null)
					iniFile.Close();
			}
		}

		int TryGetInt(IniSection section, string key, int def) {
			var str = section.GetValue(key);
			if(str == null)
				return def;
			int ret;
			if(!int.TryParse(str, out ret))
				return def;
			return ret;
		}

		bool TryGetBool(IniSection section, string key, bool def) {
			var str = section.GetValue(key);
			if(str == null)
				return def;
			return str.ToLower() == "yes";
		}


		public bool SaveToFile() {
			FileStream file = null;
			IniWriter ini = null;

			try {
				Directory.CreateDirectory(App.StoragePath);
				file = new FileStream(App.SettingsFilename, FileMode.Create, FileAccess.Write);
			} catch {
				if(file != null)
					file.Close();
				return false;
			}

			try {
				ini = new IniWriter(file);
				ini.BeginSection("misc");
				ini.WriteKeyValue("config-version", "1");
				ini.WriteKeyValue("tray-cherry", ShowedFirstTrayBalloon ? "popped" : "intact");

				ini.BeginSection("times");
				ini.WriteKeyValue("nap-length", ((int)NapLength.TotalSeconds).ToString());
				ini.WriteKeyValue("nap-cooldown", ((int)NapCooldown.TotalSeconds).ToString());
				ini.WriteKeyValue("core-length", ((int)CoreLength.TotalSeconds).ToString());
				ini.WriteKeyValue("core-cooldown", ((int)CoreCooldown.TotalSeconds).ToString());
				ini.WriteKeyValue("vital-enabled", VitalEnabled ? "yes" : "no");
				ini.WriteKeyValue("vital-initial", ((int)VitalInitial.TotalSeconds).ToString());
				ini.WriteKeyValue("vital-period", ((int)VitalPeriod.TotalSeconds).ToString());
				if(NextNapAllowed != DateTime.MinValue)
					ini.WriteKeyValue("next-nap-allowed", NextNapAllowed.ToString("yyyy-MM-dd HH:mm:ss"));

				ini.BeginSection("sounds");
				ini.WriteKeyValue("warning-file", WarningSoundFile);
				ini.WriteKeyValue("warning-length", ((int)WarningSoundLength.TotalSeconds).ToString());
				ini.WriteKeyValue("alarm-file", AlarmSoundFile);
				ini.WriteKeyValue("fadein-time", ((int)WarningSoundFadeIn.TotalSeconds).ToString());
			} finally {
				if(ini != null)
					ini.Close();
				else if(file != null)
					file.Close();
			}
			return true;
		}
	}
}