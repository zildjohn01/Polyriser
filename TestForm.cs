using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Polyriser {
	sealed partial class TestForm : Form {
		const int MathBiggestTerm = 50;

		Engine _engine;
		bool _alarming;  // true = alarming, false = text prompt
		Countdown _muteTimer, _vitalTimer;
		Random _random;
		bool _okToClose;
		TestMethod _method;
		object _data;
		string _desiredResponse;
		int _trials;

		public TestForm(Engine engine) {
			InitializeComponent();

#if DEBUG
			TopMost = false;
#endif

			_engine = engine;
			_random = new Random();
			_muteTimer = new Countdown();
			_muteTimer.Elapsed += MuteTimer_Elapsed;
			_vitalTimer = new Countdown();
			_vitalTimer.Elapsed += VitalTimer_Elapsed;
		}

		public void DoTest(IWin32Window owner, TestMethod method, object data) {
			_alarming = true;
			_method = method;
			_data = data;
			MakePrompt();
			ShowDialog(owner);
		}

		public bool PromptForText(IWin32Window owner, string prompt, out string result) {
			_alarming = false;
			txtPrompt.Text = prompt;
			ShowDialog(owner);
			result = txtResponse.Text;
			return true;
		}


		private void txtResponse_KeyDown(object sender, KeyEventArgs e) {
			_engine.SignOfLife();
			_engine.RaiseEvent(EngineEvent.SoundMute);
			_muteTimer.Start(new TimeSpan(0, 0, 3));
		}

		void MuteTimer_Elapsed(object sender, EventArgs e) {
			_engine.RaiseEvent(EngineEvent.SoundUnmute);
		}

		void VitalTimer_Elapsed(object sender, EventArgs e) {
			_engine.RaiseEvent(EngineEvent.SoundVitalOuch);

			if(_trials == 0) {
				_method = TestMethod.Math;
				_engine.Invoker.Invoke((DelayedAction)MakePrompt);
			}

			if(_trials < 4) {
				_trials += 1;
				_vitalTimer.Start(new TimeSpan(0, 0, Engine.VitalWaitSeconds));
			} else {
				// Too many tries... it's probably a lost cause, so just close and give up
				_okToClose = true;
				_engine.RaiseEvent(EngineEvent.VitalGaveUp);
				_engine.Invoker.Invoke((DelayedAction)Close);
			}
		}


		void MakePrompt() {
			switch(_method) {
			case TestMethod.Repeat:
				_desiredResponse = (string)_data;
				txtPrompt.Text = _desiredResponse;
				break;
			case TestMethod.Math:
				var num1 = _random.Next(MathBiggestTerm);
				var num2 = _random.Next(MathBiggestTerm);
				var num3 = _random.Next(MathBiggestTerm);
				var num4 = _random.Next(MathBiggestTerm);
				txtPrompt.Text = string.Format("{0} + {1} + {2} - {3}",
					num1.ToString(), num2.ToString(), num3.ToString(), num4.ToString());
				_desiredResponse = (num1 + num2 + num3 - num4).ToString();
				break;
			case TestMethod.VitalTest:
				txtPrompt.Text = (string)_data;
				_engine.RaiseEvent(EngineEvent.SoundVital);
				_vitalTimer.Start(new TimeSpan(0, 0, Engine.VitalWaitSeconds));
				break;
			default:
				App.Assert(false);
				break;
			}
			txtResponse.Clear();
			txtResponse.Focus();
		}

		private void cmdCheck_Click(object sender, EventArgs e) {
			if(!_alarming) {
				_okToClose = true;
				Close();
				return;
			}

			switch(_method) {
			case TestMethod.Repeat:
				if(!CheckPrompt())
					goto FailedTest;

				goto PassedTest;

			case TestMethod.Math:
				if(!CheckPrompt())
					goto FailedTest;

				_trials += 1;
				if(_trials >= 3)
					goto PassedTest;
				MakePrompt();
				return;

			case TestMethod.VitalTest:
				if(!CheckPrompt())
					goto FailedTest;

				_vitalTimer.Stop();
				_engine.RaiseEvent(EngineEvent.VitalConfirmed);
				_okToClose = true;
				Close();
				return;
			}

			App.Assert(false);

		FailedTest:
			ScrewedUp();
			return;

		PassedTest:
			PassedTest();
			return;
		}


		bool CheckPrompt() {
			if(_desiredResponse == null)
				return true;
			return Essence(txtResponse.Text) == Essence(_desiredResponse);
		}

		void ScrewedUp() {
			_engine.RaiseEvent(EngineEvent.VerifiedWrong);
			App.FlashTextBox(txtResponse, Strings.FailedTest);
			MakePrompt();
		}

		void PassedTest() {
			_engine.EndNap();
			_okToClose = true;
			Close();
		}

		private void TestForm_FormClosing(object sender, FormClosingEventArgs e) {
			if(!_okToClose)
				e.Cancel = true;
		}


		static string Essence(string str) {
			// This isn't called often, so don't care about speed
			var chars = str.Trim().ToLower().ToCharArray();
			var list = new List<char>(chars).FindAll(ch => {
				return (ch >= 'a' && ch <= 'z')
					|| (ch >= 'A' && ch <= 'Z')
					|| (ch >= '0' && ch <= '9');
			});
			return new string(list.ToArray());
		}
	}
}
