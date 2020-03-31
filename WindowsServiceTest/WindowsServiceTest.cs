using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsServiceTest
{
    public partial class WindowsServiceTest : ServiceBase
    {
        private int eventId = 1;
        private string eventLogName = "WindowsServiceTestEventLog";
        private string eventSourceName = "WindowsServiceTest";

        public WindowsServiceTest(string[] args)
        {
            InitializeComponent();

            if(args.Length > 0)
            {
                StringBuilder sb = new StringBuilder("WindowsServiceTest - Started with these parameters:");

                foreach (string arg in args)
                {
                    sb.AppendLine(arg);
                }

                eventLog.WriteEntry(sb.ToString());
            }

            eventLog = new EventLog
            {
                Source = eventSourceName,
                Log = eventLogName
            };
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("WindowsServiceTest - In OnStart.");

            Timer timer = new Timer
            {
                Interval = 10000 // 10 seconds

            };
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("WindowsServiceTest - In OnStop.");
        }

        protected override void OnContinue()
        {
            eventLog.WriteEntry("WindowsServiceTest - In OnContinue.");
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            eventLog.WriteEntry("WindowsServiceTest - Monitoring the System", EventLogEntryType.Information, eventId++);
        }
    }
}
