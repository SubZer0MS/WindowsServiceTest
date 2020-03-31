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

        private string eventSourceName = "WindowsServiceTest";
        private string eventLogName = "WindowsServiceTestEventLog";

        public WindowsServiceTest(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                eventSourceName = args[0];
            }

            if (args.Length > 1)
            {
                eventLogName = args[1];
            }

            eventLog = new EventLog();
            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, eventLogName);
            }
            eventLog.Source = eventSourceName;
            eventLog.Log = eventLogName;
        }

        ~WindowsServiceTest()
        {
            if (EventLog.Exists(eventLogName))
            { 
                EventLog.Delete(eventLogName);
            }
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
