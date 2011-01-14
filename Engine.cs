using System;
using System.Windows.Forms;


namespace Polyriser {
	sealed class Engine {
#if DEBUG
		public const int CancelGraceSeconds = 2;
		public const int VitalWaitSeconds = 5;
#else
		public const int CancelGraceSeconds = 5 * 60;
		public const int VitalWaitSeconds = 1 * 60;
#endif

		public EngineState State {get; private set;}
		public Control Invoker {get; set;}
		public TimeSpan NapLength {get; private set;}
		public TimeSpan CooldownLength {get; private set;}
		Countdown _napTimer, _graceTimer, _warningTimer, _cooldownTimer, _vitalTimer;
		bool _anySignsOfLife;

		public event EventHandler<EngineEventArgs> Hook;


		public Engine() {
			_napTimer = new Countdown();
			_napTimer.Elapsed += NapTimer_Elapsed;
			_graceTimer = new Countdown();
			_graceTimer.Elapsed += GraceTimer_Elapsed;
			_warningTimer = new Countdown();
			_warningTimer.Elapsed += WarningTimer_Elapsed;
			_cooldownTimer = new Countdown();
			_cooldownTimer.Elapsed += CooldownTimer_Elapsed;
			_vitalTimer = new Countdown();
			_vitalTimer.Elapsed += VitalTimer_Elapsed;

			Hook += SelfHook;
		}


		public bool AllowedToNapOrExit {get {
			return State == EngineState.Idle;
		}}

		public bool WithinGracePeriod {get {
			return State == EngineState.NapStarting;
		}}

		public TimeSpan NapTimeElapsed {get {
			return _napTimer.TimeElapsed;
		}}

		public TimeSpan NapTimeLeft {get {
			return _napTimer.TimeLeft;
		}}


		public void BeginNap(TimeSpan napLength, TimeSpan cooldownLength) {
			App.Assert(AllowedToNapOrExit);

			NapLength = napLength;
			CooldownLength = cooldownLength;
			RaiseEvent(EngineEvent.NapStart);
		}

		public void EndNap() {
			if(WithinGracePeriod)
				RaiseEvent(EngineEvent.NapCancelled);
			else
				RaiseEvent(EngineEvent.NapDone);
		}

		public void EnableVitalChecks(bool enabled) {
			App.Settings.VitalEnabled = enabled;
			App.Settings.SaveToFile();

			if(enabled)
				_vitalTimer.Start(App.Settings.VitalPeriod);
			else
				_vitalTimer.Stop();
		}


		public void LoadSettingsFailed(string text) {
			RaiseEvent(EngineEvent.LoadSettingsFailed, text);
		}

		public void Log(DateTime time, string message) {
			var data = new LogData(time, message);
			RaiseEvent(EngineEvent.LogMessage, data);
		}

		public void SignOfLife() {
			if(!_anySignsOfLife && State != EngineState.NapStarting) {
				_anySignsOfLife = true;
				RaiseEvent(EngineEvent.FirstSignOfLife);
			}
		}


		public void RaiseEvent(EngineEvent əvənt) {
			RaiseEvent(əvənt, null);
		}

		void RaiseEvent(EngineEvent ƐƲƺŋƫ, object data) {
			var args = new EngineEventArgs(ƐƲƺŋƫ, data);

			// Invoke used liberally for thread safety
			if(Invoker != null && Invoker.IsHandleCreated && Invoker.InvokeRequired)
				Invoker.Invoke(Hook, this, args);
			else
				Hook(this, args);
		}


		void SelfHook(object sender, EngineEventArgs e) {
			switch(e.Event) {
			case EngineEvent.Init:
				State = EngineState.Idle;
				RaiseEvent(EngineEvent.SoundUnmute);
				if(App.Settings.VitalEnabled)
					_vitalTimer.Start(App.Settings.VitalPeriod);
				break;

			case EngineEvent.Shutdown:
				_napTimer.Stop();
				_graceTimer.Stop();
				RaiseEvent(EngineEvent.SoundStopAll);
				RaiseEvent(EngineEvent.SoundUnmute);
				break;

			case EngineEvent.LoadSettings:
				if(State == EngineState.Idle && App.Settings.NextNapAllowed > DateTime.Now) {
					// User tried to nap sooner by killing the process and restarting...
					Log(DateTime.Now, "So you think you're slick, huh?");
					CooldownLength = App.Settings.NextNapAllowed - DateTime.Now;
					RaiseEvent(EngineEvent.CooldownBegin);
				}
				break;

			case EngineEvent.NapStart:
				App.Assert(State == EngineState.Idle);
				State = EngineState.NapStarting;
				_vitalTimer.Stop();
				_napTimer.Start(NapLength);
				_graceTimer.Start(new TimeSpan(0, 0, CancelGraceSeconds));
				break;

			case EngineEvent.NapCancelled:
				App.Assert(State == EngineState.NapStarting);
				State = EngineState.Idle;
				_napTimer.Stop();
				_graceTimer.Stop();
				if(App.Settings.VitalEnabled)
					_vitalTimer.Start(App.Settings.VitalPeriod);
				break;

			case EngineEvent.GracePeriodOver:
				_graceTimer.Stop();
				_anySignsOfLife = false;
				App.Assert(State == EngineState.NapStarting);
				State = EngineState.Napping;
				break;

			case EngineEvent.NapElapsed:
				if(WithinGracePeriod)
					RaiseEvent(EngineEvent.GracePeriodOver);
				RaiseEvent(EngineEvent.NapWarning);
				break;

			case EngineEvent.NapWarning:
				App.Assert(State == EngineState.Napping);
				State = EngineState.Warning;
				RaiseEvent(EngineEvent.SoundUnmute);
				RaiseEvent(EngineEvent.SoundWarning);
				_warningTimer.Start(App.Settings.WarningSoundLength);
				break;

			case EngineEvent.NapAlarm:
				App.Assert(State == EngineState.Warning);
				State = EngineState.Alarming;
				RaiseEvent(EngineEvent.SoundAlarm);
				break;

			case EngineEvent.NapDone:
				_napTimer.Stop();
				_graceTimer.Stop();
				_warningTimer.Stop();
				RaiseEvent(EngineEvent.SoundStopAll);
				RaiseEvent(EngineEvent.CooldownBegin);
				if(App.Settings.VitalEnabled)
					_vitalTimer.Start(App.Settings.VitalInitial);
				break;

			case EngineEvent.VitalTest:
				// This is handled in MainForm, so that the resulting dialog
				// can be modal and owned by MainForm
				break;

			case EngineEvent.VitalConfirmed:
			case EngineEvent.VitalGaveUp:
				App.Assert(App.Settings.VitalEnabled);
				_vitalTimer.Start(App.Settings.VitalPeriod);
				break;

			case EngineEvent.CooldownBegin:
				State = EngineState.CoolingDown;
				_cooldownTimer.Start(CooldownLength);
				App.Settings.NextNapAllowed = DateTime.Now + CooldownLength;
				App.Settings.SaveToFile();
				break;

			case EngineEvent.CooldownDone:
				App.Assert(State == EngineState.CoolingDown);
				State = EngineState.Idle;
				App.Settings.NextNapAllowed = DateTime.MinValue;
				App.Settings.SaveToFile();
				break;

			case EngineEvent.TriedToNapTooSoon:
			case EngineEvent.TriedToExitTooSoon: {
				RaiseEvent(EngineEvent.SoundUnmute);
				RaiseEvent(EngineEvent.SoundAlarm);

				Countdown.Queue(new TimeSpan(0, 0, 2), () => {
					RaiseEvent(EngineEvent.SoundStopAll);
				});
				break;
			}}
		}


		void NapTimer_Elapsed(object sender, EventArgs e) {
			RaiseEvent(EngineEvent.NapElapsed);
		}

		void GraceTimer_Elapsed(object sender, EventArgs e) {
			App.Assert(State == EngineState.NapStarting);
			RaiseEvent(EngineEvent.GracePeriodOver);
		}

		void WarningTimer_Elapsed(object sender, EventArgs e) {
			App.Assert(State == EngineState.Warning);
			RaiseEvent(EngineEvent.NapAlarm);
		}

		void CooldownTimer_Elapsed(object sender, EventArgs e) {
			App.Assert(State == EngineState.CoolingDown);
			RaiseEvent(EngineEvent.CooldownDone);
		}

		void VitalTimer_Elapsed(object sender, EventArgs e) {
			App.Assert(State == EngineState.CoolingDown || State == EngineState.Idle);
			RaiseEvent(EngineEvent.VitalTest);
		}
	}


	enum EngineState {
		None, Idle, NapStarting, Napping, Warning, Alarming, CoolingDown
	}

	enum EngineEvent {
		None, LogMessage, Init, Shutdown, LoadSettings, LoadSettingsFailed,
		NapStart, NapCancelled, GracePeriodOver, NapElapsed, NapWarning, NapAlarm, NapDone,
		VitalTest, VitalConfirmed, VitalGaveUp,
		FirstSignOfLife, PostnapVerifyFail, PostnapVerifySuccess,
		CooldownBegin, CooldownDone, TriedToNapTooSoon, TriedToExitTooSoon,
		SoundWarning, SoundAlarm, SoundVital, SoundVitalOuch, SoundStopAll, SoundMute, SoundUnmute,
		VerifiedWrong, VerifiedCorrect
	}

	sealed class EngineEventArgs : EventArgs {
		readonly EngineEvent _event;
		public EngineEvent Event {get {return _event;}}
		readonly object _data;
		public object Data {get {return _data;}}

		public EngineEventArgs(EngineEvent evənt) {
			_event = evənt;
		}

		public EngineEventArgs(EngineEvent evənt, object data) {
			_event = evənt;
			_data = data;
		}
	}


	sealed class LogData {
		readonly DateTime _time;
		public DateTime Time {get {return _time;}}
		readonly string _message;
		public string Message {get {return _message;}}

		public LogData(DateTime time, string message) {
			_time = time;
			_message = message;
		}
	}
}