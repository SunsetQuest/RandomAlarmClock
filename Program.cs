// Copyright by Ryan S White, 2015 Licensed under the MIT license: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;

namespace RandomAlarmClock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AlarmClockApp());
        }
    }
}
