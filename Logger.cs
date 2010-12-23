using System;
using System.IO;


namespace Polyriser {
	sealed class Logger {
		Engine _engine;
		FileStream _file;
		StreamWriter _output;

		DateTime _napStartTime;
		EngineState _wakeState;
		TimeSpan _sleepTime;
		int _verifyFails;


		public Logger(Engine engine) {
			_engine = engine;
			_engine.Hook += EngineHook;
		}


		void EngineHook(object sender, EngineEventArgs e) {
			switch(e.Event) {
			case EngineEvent.Init:
				try {
					Directory.CreateDirectory(App.StoragePath);
					_file = new FileStream(App.LogFilename, FileMode.Append, FileAccess.Write);
					_output = new StreamWriter(_file);
				} catch {
					// Logging isn't critical, so just display an error and continue
					_engine.Log(DateTime.Now, "Failed to open log file on disk.");
				}
				break;
			case EngineEvent.Shutdown:
				if(_output != null)
					_output.Close();
				break;

			case EngineEvent.NapStart:
				_napStartTime = DateTime.Now;
				break;
			case EngineEvent.FirstSignOfLife:
				_wakeState = _engine.State;
				_sleepTime = _engine.NapTimeElapsed;
				_verifyFails = 0;
				break;
			case EngineEvent.VerifiedWrong:
				_verifyFails += 1;
				break;
			case EngineEvent.TriedToNapTooSoon:
				_engine.Log(DateTime.Now, "Tried to nap again too soon");
				break;
			case EngineEvent.TriedToExitTooSoon:
				_engine.Log(DateTime.Now, "Tried to exit the program :(");
				break;

			case EngineEvent.NapDone: {
				var msg = "Slept for ";
				msg += App.TimeToStringHHMMSS(_sleepTime);
				msg += ", woke to ";
				switch(_wakeState) {
				case EngineState.Napping: msg += "silence"; break;
				case EngineState.Warning: msg += "warning"; break;
				case EngineState.Alarming: msg += "alarm"; break;
				default: App.Assert(false); break;
				}
				if(_verifyFails > 0)
					msg += string.Format(", failed {0} test{1}",
						_verifyFails, _verifyFails == 1 ? null : "s");
				_engine.Log(_napStartTime, msg);
				break;
			}
			case EngineEvent.LogMessage: {
				if(_output != null) {
					var data = (LogData)e.Data;
					_output.Write(data.Time.ToString("yyyy-MM-dd HH:mm:ss  "));
					_output.WriteLine(data.Message);
					_output.Flush();
				}
				break;
			}}
		}
	}
}