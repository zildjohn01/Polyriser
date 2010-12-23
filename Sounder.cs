using System;
using System.Runtime.InteropServices;
using System.Timers;


namespace Polyriser {
	class Sounder {
		const uint MMSYSERR_NOERROR = 0;

		[DllImport("winmm")]
		static extern uint waveOutGetVolume(IntPtr hwo, out uint pdwVolume);
		[DllImport("winmm")]
		static extern uint waveOutSetVolume(IntPtr hwo, uint dwVolume);


		const int FadeInGranMsec = 250;

		Engine _engine;
		Timer _fadeInTimer;
		MciAudio _warning, _alarm;
		uint? _originalVolume;
		ushort _curVolume = 0xFFFF;
		bool _muted;


		public Sounder(Engine engine) {
			_engine = engine;
			_engine.Hook += EngineHook;

			_fadeInTimer = new Timer(FadeInGranMsec);
			_fadeInTimer.AutoReset = true;
			_fadeInTimer.Elapsed += FadeInTimer_Elapsed;

			_warning = new MciAudio();
			_warning.Repeat = true;
			_alarm = new MciAudio();
			_alarm.Repeat = true;
		}

		void CloseAll() {
			if(_warning != null)
				_warning.Close();
			if(_alarm != null)
				_alarm.Close();
		}

		void EngineHook(object sender, EngineEventArgs e) {
			switch(e.Event) {
			case EngineEvent.Init: {
				uint origVol;
				if(waveOutGetVolume(IntPtr.Zero, out origVol) == MMSYSERR_NOERROR)
					_originalVolume = origVol;
				else
					_engine.Log(DateTime.Now, "Warning: couldn't determine current volume");
				break;
			}
			case EngineEvent.Shutdown:
				if(_originalVolume != null)
					waveOutSetVolume(IntPtr.Zero, (uint)_originalVolume);
				break;

			case EngineEvent.LoadSettings:
				CloseAll();
				if(!_warning.TryOpen(App.Settings.WarningSoundFile))
					_engine.LoadSettingsFailed("SoundWarningFile");
				if(!_alarm.TryOpen(App.Settings.AlarmSoundFile))
					_engine.LoadSettingsFailed("SoundAlarmFile");
				break;
			case EngineEvent.NapDone:
				App.Assert(!_warning.Playing);
				App.Assert(!_alarm.Playing);
				break;
			case EngineEvent.SoundWarning:
				if(_engine.State == EngineState.Warning &&
						App.Settings.WarningSoundFadeIn.Ticks != 0) {
					SetVolume(0);
					_fadeInTimer.Start();
					FadeInTimer_Elapsed(_fadeInTimer, EventArgs.Empty);
				}

				_warning.SeekToStart();
				_warning.Play();
				break;
			case EngineEvent.SoundAlarm:
				_warning.Stop();
				_alarm.SeekToStart();
				_alarm.Play();
				break;
			case EngineEvent.SoundStopAll:
				_warning.Stop();
				_alarm.Stop();
				break;

			case EngineEvent.SoundMute:
				SetMuted(true);
				break;
			case EngineEvent.SoundUnmute:
				SetMuted(false);
				break;
			}
		}

		void FadeInTimer_Elapsed(object sender, EventArgs e) {
			ushort vol = GetVolume();

			var totalMsec = (int)App.Settings.WarningSoundFadeIn.TotalMilliseconds;
			var stepFactor = (ushort)(0xFFFF * FadeInGranMsec / totalMsec);

			// add with protection from overflow
			vol = (ushort)(vol + stepFactor > 0xFFFF ? 0xFFFF : vol + stepFactor);
			SetVolume(vol);

			if(vol == 0xFFFF)
				_fadeInTimer.Stop();
		}


		ushort GetVolume() {
			return _curVolume;
		}

		void SetVolume(ushort value) {
			_curVolume = value;
			if(!_muted)
				waveOutSetVolume(IntPtr.Zero, (uint)(_curVolume | (_curVolume << 16)));
		}

		void SetMuted(bool value) {
			_muted = value;
			if(_muted)
				waveOutSetVolume(IntPtr.Zero, 0);
			else
				SetVolume(GetVolume());
		}
	}
}