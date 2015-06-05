// Copyright by Ryan S White, 2015 Licensed under the MIT license: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RandomAlarmClock
{
    public partial class AlarmControl : UserControl
    {
        public RandomTimer rTimer { get; set; }
        public event SettingUpdateEventHandler SettingUpdate;
        public delegate void SettingUpdateEventHandler(int id, string shortName, string details, bool running, bool peekFuture, TimeSpan avgTimeSpan);
        public event TimerElapsedEventHandler Elapsed;
        public delegate void TimerElapsedEventHandler(int id, string shortName, string details);
        public int ControlId { get; set; }

        public string Description
        {
            get { return txtText.Text; }
            set
            {
                if (!ControlInvokeRequired(txtText, () => txtText.Text = value))
                    txtText.Text = value;
            }
        }


        public string ShortName
        {
            get { return txtName.Text; }
            set
            {
                if (!ControlInvokeRequired(txtName, () => txtName.Text = value))
                    txtName.Text = value;

            }
        }

        public TimeSpan AvgInterval
        {
            get { return rTimer.IntervalAsTimeSpan; }
            set
            {
                if (!ControlInvokeRequired(txtInterval, () => SetAvgInterval(value)))
                    SetAvgInterval(value);
            }
        }

        private void SetAvgInterval(TimeSpan interval)
        {
            lock (this)
            {
               txtInterval.Text = interval.ToString();
               rTimer.IntervalAsTimeSpan = interval;
            }
        }

        public bool Running
        {
            get { return rTimer.Enabled; }
            set
            {
                if (!ControlInvokeRequired(btnEnabled, () => EnableRandomTimer(value)))
                    EnableRandomTimer(value);
            }
        }
        public bool PeekEnabled
        {
            get { return cbPeek.Checked; }
            set
            {
                if (!ControlInvokeRequired(cbPeek, () => SetPeekEnabled(value)))
                    SetPeekEnabled(value);
            }
        }
        private void SetPeekEnabled(bool enabled)
        {
            lock (this)
            {
                if (cbPeek.Checked != enabled)
                    cbPeek.Checked = enabled;

                if (enabled)
                    UpdateUpcommingEvents();
                else
                    listViewAlarms.Items.Clear();

                listViewAlarms.Visible = enabled;
            }
        }

        private void EnableRandomTimer(bool enabled)
        {
            lock (this)
            {
                rTimer.Enabled = enabled;
                btnEnabled.ImageIndex = enabled ? 0 : 1;
                listViewAlarms.ForeColor = enabled ? SystemColors.WindowText : SystemColors.GrayText;
            }
        }


        public void NotifyOfSettingsChange()
        {
            SettingUpdateEventHandler handler = SettingUpdate;
            if (handler != null)
                handler(ControlId, txtName.Text, txtText.Text, rTimer.Enabled, cbPeek.Checked, rTimer.IntervalAsTimeSpan);
        }

        public void TimerElapsed()
        {
            TimerElapsedEventHandler handler = Elapsed;
            if (handler != null)
                handler(ControlId, txtName.Text, txtText.Text);
        }

        public AlarmControl()
        {
            InitializeComponent();
            rTimer = new RandomTimer(100, 10);
            rTimer.Elapsed += rt_Elapsed;
        }


        private void btnEnabled_Click(object sender, EventArgs e)
        {
            bool isEnabled = rTimer.Enabled;

            EnableRandomTimer(!isEnabled);
            NotifyOfSettingsChange();
        }


        void rt_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ControlInvokeRequired(listViewAlarms, () => rt_Elapsed(sender, e)))
                return;

            TimerElapsed();

            if (PeekEnabled) 
                UpdateUpcommingEvents();
        }

        private void UpdateUpcommingEvents()
        {
            listViewAlarms.Items.Clear();
            DateTime now = DateTime.Now;
            foreach (double val in rTimer.GetUpcommingEvents())
                listViewAlarms.Items.Add("hit", now.AddMilliseconds(val).ToLongTimeString(), 0);
        }

        private void cbPeek0_CheckedChanged(object sender, EventArgs e)
        {
            //todo: update rTimer to only 1
            SetPeekEnabled(cbPeek.Checked);
            NotifyOfSettingsChange();
        }

        private static bool ControlInvokeRequired(Control control, Action action)
        {
            if (control.InvokeRequired) control.Invoke(new MethodInvoker(delegate { action(); }));
            else return false;

            return true;
        }

        private void txtInterval0_Validating(object sender, CancelEventArgs e)
        {
            TimeSpan timespan;
            if (TimeSpan.TryParse(txtInterval.Text, out timespan))
            {
                Properties.Settings.Default["AvgInterval" + ControlId] = timespan;
                rTimer.IntervalAsTimeSpan = timespan;
            }
            else
            {
                MessageBox.Show("Unable to understand interval. Use the format: DDDD:HH:MM:SS.mmm.", "Formating Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            NotifyOfSettingsChange();
        }

        private void txtText_Validating(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default["Text" + ControlId] = txtText.Text;
            NotifyOfSettingsChange();
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default["Name" + ControlId] = txtName.Text;
            NotifyOfSettingsChange();
        }
    }
}
