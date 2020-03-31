using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsServiceTest
{
	[RunInstaller(true)]
	public partial class ProjectInstaller : Installer
	{
		private readonly string eventLogName = "WindowsServiceTestEventLog";
		private readonly string eventSourceName = "WindowsServiceTest";

		public string ServiceName { get; set; }
		public string DisplayName { get; set; }
		public string Description { get; set; }

		private readonly EventLogInstaller myEventLogInstaller;

		public ProjectInstaller()
        {
            InitializeComponent();

			myEventLogInstaller = new EventLogInstaller
			{
				Source = eventSourceName,
				Log = eventLogName
			};

			Installers.Add(myEventLogInstaller);
		}

		protected override void OnBeforeInstall(IDictionary savedState)
		{
			SetServiceInformation();
			base.OnBeforeInstall(savedState);
		}

		protected override void OnBeforeUninstall(IDictionary savedState)
		{
			SetServiceInformation();
			base.OnBeforeUninstall(savedState);
		}

		private void SetServiceInformation()
		{
			if (Context.Parameters.ContainsKey("serviceName") && !string.IsNullOrEmpty(Context.Parameters["serviceName"]))
			{
				Context.LogMessage(string.Format("serviceName parameter has been passed in, value: {0}", Context.Parameters["serviceName"]));
			}
			else
			{
				ServiceName = "Windows Service Test";
			}
			if (Context.Parameters.ContainsKey("displayName") && !string.IsNullOrEmpty(Context.Parameters["displayName"]))
			{
				Context.LogMessage(string.Format("displayName parameter has been passed in, value: {0}", Context.Parameters["displayName"]));
			}
			else
			{
				DisplayName = "Windows Service Test Display Name";
			}
			if (Context.Parameters.ContainsKey("description") && !string.IsNullOrEmpty(Context.Parameters["description"]))
			{
				Context.LogMessage(string.Format("description parameter has been passed in, value: {0}", Context.Parameters["description"]));
			}
			else
			{
				Description = "Some description here.";
			}
		}
	}
}
