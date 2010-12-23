using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace Polyriser {
	class MciException : Exception {
		[DllImport("winmm")]
		static extern int mciGetErrorString(int fdwError, StringBuilder lpszErrorText, int cchErrorText);

		static string GetDescription(int errorCode) {
			var ret = new StringBuilder();
			ret.Append('\0', 1024);
			if(mciGetErrorString(errorCode, ret, 1024) == 0)
				return "Unknown MCI error";
			return ret.ToString();
		}

		public MciException(int errorCode) : base(GetDescription(errorCode)) {
#if DEBUG
			System.Diagnostics.Debug.WriteLine(Message);
#endif
		}

		public MciException(string message) : base(message) {}
	}


	class MciNotifyWindow : NativeWindow {
		const int HWND_MESSAGE = -3;

		const int MM_MCINOTIFY = 0x3B9;

		MciAudio _owner;

		public MciNotifyWindow(MciAudio owner) {
			_owner = owner;
		}

		public void Create() {
			var cp = new CreateParams();
			cp.ClassName = "Message";
			cp.Parent = new IntPtr(HWND_MESSAGE);
			CreateHandle(cp);
		}

		protected override void WndProc(ref Message m) {
			if(m.Msg == MM_MCINOTIFY)
				_owner.ReflectNotify(m.WParam.ToInt32(), m.LParam.ToInt32());
			base.WndProc(ref m);
		}
	}


	class MciAudio {
		const int MCI_NOTIFY_SUCCESSFUL = 0x1;
		const int MCI_NOTIFY_SUPERSEDED = 0x2;
		const int MCI_NOTIFY_ABORTED =    0x4;
		const int MCI_NOTIFY_FAILURE =    0x8;

		[DllImport("winmm")]
		static extern int mciSendString(string lpszCommand, StringBuilder lpszReturnString, int cchReturn, IntPtr hwndCallback);


		static int instanceIndex;


		string _alias;
		int _deviceID;
		bool _opened;
		public bool Repeat {get; set;}
		MciNotifyWindow _notifyWindow;

		public MciAudio() {
			instanceIndex++;
			_alias = "Polyriser" + instanceIndex.ToString() + "_" + DateTime.Now.Ticks.ToString();
			_notifyWindow = new MciNotifyWindow(this);
		}


		public void Close() {
			if(!_opened)
				return;

			SendCommand("close " + _alias + " wait");
			_notifyWindow.DestroyHandle();
			_opened = false;
		}

		public void Open(string fileName) {
			Close();

			SendCommand("open \"" + fileName + "\" type " +
				GuessDeviceType(fileName) + " alias " + _alias + " wait");

			_deviceID = SearchForDeviceID();
			_notifyWindow.Create();
			_opened = true;
		}

		public bool TryOpen(string fileName) {
			try {
				Open(fileName);
				return true;
			} catch {
				return false;
			}
		}


		public void Play() {
			SendCommand("play " + _alias + " notify");
		}

		public void Pause() {
			SendCommand("pause " + _alias + " wait");
		}

		public void Stop() {
			if(_opened) {
				SendCommand("stop " + _alias + " wait");
				SeekToStart();
			}
		}


		public void SeekToStart() {
			SendCommand("seek " + _alias + " to start wait");
		}


		public bool Playing {get {
			if(!_opened)
				return false;
			return QueryCommand("status " + _alias + " mode", 32) == "playing";
		}}

		public bool Muted {set {
			SendCommand("setaudio " + _alias + (value ? " off" : " on"));
		}}


		public void ReflectNotify(int wParam, int lParam) {
			App.Assert(lParam == _deviceID);
			if(wParam != MCI_NOTIFY_SUCCESSFUL)
				return;

			if(Repeat) {
				SeekToStart();
				Play();
			}
		}


		void SendCommand(string lpszCommand) {
			var error = mciSendString(lpszCommand, null, 0, _notifyWindow.Handle);
			if(error != 0)
				throw new MciException(error);
		}

		string QueryCommand(string lpszCommand, int bufferSize) {
			var ret = new StringBuilder();
			ret.Append('\0', bufferSize);
			var error = mciSendString(lpszCommand, ret, bufferSize, _notifyWindow.Handle);
			if(error != 0)
				throw new MciException(error);
			return ret.ToString();
		}


		int SearchForDeviceID() {
			int totalDevices = int.Parse(QueryCommand("sysinfo all quantity open wait", 16));
			for(int i = 1; i <= totalDevices; i++) {
				if(QueryCommand("sysinfo all name " + i.ToString() + " open wait", 64)
						== _alias)
					return i;
			}
			throw new MciException("Couldn't find MCI device ID");
		}


		static string GuessDeviceType(string fileName) {
			string ext = Path.GetExtension(fileName);
			if(ext.ToLower().StartsWith(".mp"))
				return "mpegvideo";
			return "waveaudio";
		}
	}
}