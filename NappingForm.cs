using System;
using System.Windows.Forms;


namespace Polyriser {
	enum TestMethod {
		// This enum must be kept in sync with MainForm.cboMainTestMethod's list
		None, Repeat, Math
	}

	sealed partial class NappingForm : Form {
		Engine _engine;
		Timer _secondsTimer;
		TestMethod _testMethod;
		object _testData;
		bool _okToClose;


		public NappingForm(Engine engine) {
			InitializeComponent();

#if DEBUG
			TopMost = false;
#endif

			_engine = engine;
			_engine.Hook += EngineHook;

			_secondsTimer = new Timer();
			_secondsTimer.Interval = 1000;
			_secondsTimer.Tick += SecondsTick;
		}

		void PseudoDispose() {
			_secondsTimer.Stop();
			_secondsTimer.Dispose();
			_engine.Hook -= EngineHook;
			_engine = null;
		}


		public void DoNap(IWin32Window owner, TimeSpan length, TimeSpan cooldown,
				TestMethod testMethod, object testData) {
			_testMethod = testMethod;
			_testData = testData;
			_engine.BeginNap(length, cooldown);
			ShowDialog(owner);
			PseudoDispose();
		}


		void EngineHook(object sender, EngineEventArgs e) {
			switch(e.Event) {
			case EngineEvent.NapStart:
				cmdClose.Text = Strings.NapStopPreGrace;
				_secondsTimer.Enabled = true;
				SecondsTick(_secondsTimer, EventArgs.Empty);  // Display initial time
				break;

			case EngineEvent.GracePeriodOver:
				cmdClose.Text = Strings.NapStopPostGrace;
				break;

			case EngineEvent.NapElapsed: {
				if(_testMethod == TestMethod.None)
					break;  // Just wait for the user to press the close button

				// Otherwise, hand control to the test form which will test the user
				_okToClose = true;
				Close();
				var test = new TestForm(_engine);
				test.DoTest(this.Owner, _testMethod, _testData);
				break;
			}}
		}


		void SecondsTick(object sender, EventArgs e) {
			lblTime.Text = App.TimeToStringHHMMSS(_engine.NapTimeLeft);
		}

		private void cmdClose_Click(object sender, EventArgs e) {
			_engine.SignOfLife();
			App.Assert(_engine.State == EngineState.NapStarting ||
				_engine.State == EngineState.Napping ||
				_testMethod == TestMethod.None);

			_engine.EndNap();
			_okToClose = true;
			Close();
		}

		private void NappingForm_FormClosing(object sender, FormClosingEventArgs e) {
			if(!_okToClose)
				e.Cancel = true;
		}
	}
}
