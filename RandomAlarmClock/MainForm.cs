using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace RandomAlarmClock
{
    public partial class AlarmClockApp : Form
    {
        List<AlarmControl> timers = new List<AlarmControl>();
        bool shutdownRequested = false;

        public AlarmClockApp()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool atLeastOneAdded = false;

            for (int i = 0; i < 5; i++)
            {
                string shortName = (string)Properties.Settings.Default["Name" + i];
                if (!String.IsNullOrEmpty(shortName))
                {
                    AlarmControl newAlarm = new AlarmControl();
                    newAlarm.ControlId = i;
                    newAlarm.ShortName = shortName;
                    newAlarm.Description = (string)Properties.Settings.Default["Text" + i];
                    newAlarm.AvgInterval = (TimeSpan)Properties.Settings.Default["AvgInterval" + i];
                    newAlarm.Running = (bool)Properties.Settings.Default["IsRunning" + i];
                    newAlarm.PeekEnabled = (bool)Properties.Settings.Default["Peek" + i];
                    newAlarm.SettingUpdate += AlarmControl1_SettingUpdate;
                    newAlarm.Elapsed += NewAlarm_Elapsed;

                    AddAndSelectTabPage(shortName, newAlarm);

                    atLeastOneAdded = true;
                }
            }

            int lastSelected = (int)Properties.Settings.Default["LastActiveIndex"];
            tabControl1.SelectedIndex = Math.Min(lastSelected, tabControl1.TabCount - 1);

            if (!atLeastOneAdded)
            {
                AlarmControl newAlarm = new AlarmControl();
                Properties.Settings.Default["Name0"] = newAlarm.ShortName = "Poster";
                Properties.Settings.Default["Text0"] = newAlarm.Description = "Are you sitting properly?";
                Properties.Settings.Default["AvgInterval0"] = newAlarm.AvgInterval = new TimeSpan(0, 0, 30);
                Properties.Settings.Default["IsRunning0"] = newAlarm.Running = true;
                Properties.Settings.Default["Peek0"] = newAlarm.PeekEnabled = true;
                Properties.Settings.Default.Save();

                newAlarm.SettingUpdate += AlarmControl1_SettingUpdate;

                AddAndSelectTabPage(Name, newAlarm);
            }
        }

        private void NewAlarm_Elapsed(int id, string shortName, string details)
        {
            notifyIcon.ShowBalloonTip(1000, shortName, details, ToolTipIcon.Info);
        }

        private void AlarmControl1_SettingUpdate(int id, string shortName, string details, bool running, bool peekFuture, TimeSpan avgTimeSpan)
        {
            Properties.Settings.Default["Name" + id] = tabControl1.TabPages[id].Text = shortName;
            Properties.Settings.Default["Text" + id] = details;
            Properties.Settings.Default["AvgInterval" + id] = avgTimeSpan;
            Properties.Settings.Default["IsRunning" + id] = running;
            Properties.Settings.Default["Peek" + id] = peekFuture;
            Properties.Settings.Default.Save();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Text == "[Add]")
            {
                int id = tabControl1.TabPages.Count - 1;
                if (id > 4)
                {
                    MessageBox.Show(this, "The maximum number of 5 alarms has been hit.", "Maximum Reached", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                AlarmControl newAlarm = new AlarmControl();
                newAlarm.ControlId = id;
                Properties.Settings.Default["Name" + id] = newAlarm.ShortName = "*new";
                Properties.Settings.Default["Text" + id] = newAlarm.Description = "[Enter a description here]";
                Properties.Settings.Default["AvgInterval" + id] = newAlarm.AvgInterval = new TimeSpan(0, 0, 30);
                Properties.Settings.Default["IsRunning" + id] = newAlarm.Running = false;
                Properties.Settings.Default["Peek" + id] = newAlarm.PeekEnabled = true;
                
                newAlarm.SettingUpdate += AlarmControl1_SettingUpdate;

                AddAndSelectTabPage(newAlarm.ShortName, newAlarm);

            }
        }
        private void AddAndSelectTabPage(string Name, AlarmControl newAlarm)
        {
            TabPage tp = new TabPage(Name);
            tp.Controls.Add(newAlarm);

            tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, tp);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 2;

            //if (tabControl1.TabPages.Count > 4)
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int currentIdx = tabControl1.SelectedIndex;
            TabPage currentTab = tabControl1.SelectedTab;
            //AlarmControl c = (AlarmControl)currentTab.Controls[0];
            tabControl1.TabPages.Remove(currentTab);
            currentTab.Dispose();

            for (int i = currentIdx; i < tabControl1.TabPages.Count - 1; i++)
            {
                AlarmControl ac = ((AlarmControl)tabControl1.TabPages[i].Controls[0]);
                ac.ControlId = i;

                Properties.Settings.Default["Name" + i] = ac.Name;
                Properties.Settings.Default["Text" + i] = ac.Description;
                Properties.Settings.Default["AvgInterval" + i] = ac.AvgInterval;
                Properties.Settings.Default["IsRunning" + i] = ac.Running;
                Properties.Settings.Default["Peek" + i] = ac.PeekEnabled;
            }

            int idToClear = tabControl1.TabPages.Count - 1;

            Properties.Settings.Default["Name" + idToClear] = "";
            Properties.Settings.Default["Text" + idToClear] = "";
            Properties.Settings.Default["AvgInterval" + idToClear] = new TimeSpan(0, 0, 30);
            Properties.Settings.Default["IsRunning" + idToClear] = false;
            Properties.Settings.Default["Peek" + idToClear] = true;
            Properties.Settings.Default.Save();
        }

        private void AlarmClockApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shutdownRequested)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                Hide();
            }
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            shutdownRequested = true;
            Close();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ToggleSettingsWindowVisable();
        }

        private void ToggleSettingsWindowVisable()
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void toolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            ToggleSettingsWindowVisable();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                contextMenuStrip.Items[0].Text = "Hide";
            }
            else
            {
                contextMenuStrip.Items[0].Text = "Show";
            }
        }
    }
}
