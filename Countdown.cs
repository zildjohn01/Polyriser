using System;
using System.Timers;


namespace Polyriser {
	sealed class Countdown {
		Timer _timer;
		bool _triggeredYet;
		public TimeSpan InitialTime {get; private set;}
		public TimeSpan TimeElapsed {get; private set;}

		public event EventHandler<EventArgs> Elapsed;


		public Countdown() {
			_timer = new Timer();
			_timer.AutoReset = true;
			_timer.Interval = 1000;
			_timer.Elapsed += EachSecond;
		}

		public TimeSpan TimeLeft {get {
			return TimeElapsed > InitialTime ? TimeSpan.Zero : InitialTime - TimeElapsed;
		}}


		public void Start(TimeSpan time) {
			_triggeredYet = false;
			InitialTime = time;
			TimeElapsed = TimeSpan.Zero;
			if(time.TotalSeconds == 0)
				OnElapsed(EventArgs.Empty);  // Skip the timer entirely
			else
				_timer.Start();
		}

		public void Stop() {
			_timer.Stop();
			_triggeredYet = true;
		}


		void EachSecond(object sender, ElapsedEventArgs e) {
			TimeElapsed += new TimeSpan(0, 0, 1);
			if(!_triggeredYet && TimeElapsed > InitialTime)
				OnElapsed(EventArgs.Empty);
		}

		void OnElapsed(EventArgs e) {
			_triggeredYet = true;
			if(Elapsed != null)
				Elapsed(this, e);
		}


		public static Countdown Queue(TimeSpan delay, DelayedAction action) {
			var counter = new Countdown();
			counter.Elapsed += (object sender, EventArgs e) => action();
			counter.Start(delay);
			return counter;
		}
	}
}