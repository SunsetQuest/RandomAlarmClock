// RandomTimer operates simular to  System.Timers.Timer however the interval is random. In fact it implments System.Timers.Timer for its timer operations.

// Credits: Some of the documentation below was copied and modified from the System.Timers.Timer class. 


using System;
using System.Timers;


namespace RandomAlarmClock
{
    public class RandomTimer : IDisposable
    {
        public event ElapsedEventHandler Elapsed;

        /// <summary>
        /// Interval in ms
        /// </summary>
        private double _avgInterval;
        private readonly int _lookAheadCount;
        private readonly int _totalCount;
        private int _nextIdx = 0;
        private double[] _futureIntervals;
        private Random rand = new Random();
        private System.Timers.Timer timer;

        // Summary:
        //     Gets or sets a value indicating whether RandomTimer should raise
        //     the RandomTimer.Elapsed event each time the specified interval elapses
        //     or only after the first time it elapses.
        //
        // Returns:
        //     true if the RandomTimer should raise the RandomTimer.Elapsed
        //     event each time the interval elapses; false if it should raise the RandomTimer.Elapsed
        //     event only once, after the first time the interval elapses. The default is true.
        /// <summary>
        ///     Gets or sets whether the RandomTimer should automaticlly restart the timer on each
        ///     RandomTimer.Elapsed event. The default value is true.
        /// </summary>
        public bool AutoReset { get; set; }


        /// <summary>
        /// Creates a new instance of RandomTimer. An ArgumentException is thrown if the lookAheadCount is less then 1. 
        /// </summary>
        /// <param name="avgInterval">The average and middle interval in milliseconds.</param>
        /// <param name="lookAheadCount">The number to intervals to store in advance. Minimum is 1.</param>
        public RandomTimer(double avgInterval, int lookAheadCount = 1)
        {
            _avgInterval = avgInterval;
            _lookAheadCount = lookAheadCount;
            _totalCount = _lookAheadCount + 1;

            if (lookAheadCount <= 0)
                throw new ArgumentException("The lookAheadCount paramiter must be greater then 0.", "lookAheadCount");

            _futureIntervals = new double[_totalCount];
            for (int i = 0; i < _totalCount; i++)
                _futureIntervals[i] = GetNewInterval();

            timer = new System.Timers.Timer(GetNewInterval());

            // AutoReset needs to be false. If it were set to true then its possible that the next interval might not be assigned in time. New interval is set in the Elapsed event.
            timer.AutoReset = false;
            AutoReset = true;


            IncrementIdx();

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        /// <summary>
        /// Gets a list of future intervals. 
        /// </summary>
        public double[] GetUpcommingEvents()
        {
            double[] list = new double[_totalCount];
            int pos = 1;

            lock (this)
            {
                list[0] = _futureIntervals[_nextIdx];

                for (int i = _nextIdx + 1; i < _totalCount; i++)
                    list[pos++] = list[pos - 2] + _futureIntervals[i];

                for (int i = 0; i < _nextIdx; i++)
                    list[pos++] = list[pos - 2] + _futureIntervals[i];
            }

            return list;
        }

        /// <summary>
        /// Enables the raising of the Elapsed event.
        /// </summary>
        public void Start()
        {
            timer.Start();
        }


        public void Stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// Gets or sets whether the Elapsed event will be raised.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return timer.Enabled;
            }
            set
            {
                timer.Enabled = value;
            }
        }


        /// <summary>
        /// Gets or sets the average and middle interval in milliseconds to raise the 
        /// RandomTimer.Elapsed event. The value must be greater then zero and equal to 
        /// or less then Int32.MaxValue. The average times generated will be between
        /// 1 ms and and double the average interval.
        /// A System.ArgumentException will be thrown if the interval is less than one or if 
        /// the interval is greater than Int32.MaxValue the next time the timer is enabled.
        /// If the timer is currently enabled it will throw the exception immediatly. 
        /// </summary>
        public double Interval
        {
            get
            {
                return _avgInterval;
            }
            set
            {
                _avgInterval = value;
                timer.Interval = GetNewInterval();
                lock (this)
                {
                    for (int i = 0; i < _totalCount; i++)
                        _futureIntervals[i] = GetNewInterval();
                }
            }

        }
        /// <summary>
        /// This is simular to the Interval property but uses TimeSpan instead.
        /// </summary>
        public TimeSpan IntervalAsTimeSpan
        {
            get
            {
                long ticks = (long)(_avgInterval * 10000.0);
                return new TimeSpan(ticks);
            }
            set
            {
                _avgInterval = ((double)value.Ticks) / 10000.0;
                timer.Interval = GetNewInterval();
                lock (this)
                {
                    _futureIntervals = new double[_totalCount];
                    for (int i = 0; i < _totalCount; i++)
                        _futureIntervals[i] = GetNewInterval();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private double GetNewInterval()
        {
            return rand.NextDouble() * _avgInterval * 2;
        }
                
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                _futureIntervals[_nextIdx] = GetNewInterval();
                IncrementIdx();
            }

            timer.Interval = _futureIntervals[_nextIdx];

            if (AutoReset)
                timer.Start();

            ElapsedEventHandler handler = Elapsed;
            if (handler != null)
                handler(this, e);
        }

        private void IncrementIdx() // should already be locked
        {
            _nextIdx++;
            if (_nextIdx == _totalCount) _nextIdx = 0;
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
        }

    }
}
