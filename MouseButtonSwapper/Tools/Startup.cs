using System;
using Microsoft.Win32;

namespace MouseButtonSwapper.Tools
{
	public class Startup
	{
		private const string StartupRegistryValueName = "Mouse Button Swapper";


		public Startup(string targetExePath)
		{
			this.targetExePath = targetExePath;
		}


		public bool RunOnStartup
		{
			get
			{
				return StartupShortcutExists();
			}
			set
			{
				if (value)
					CreateStartupShortcut();
				else
					DeleteStartupShortcuts();
			}
		}


		private bool StartupShortcutExists()
		{
			var shortcutExists = false;
			InvokeStartupRegistryValueAction(key => shortcutExists = (null != key.GetValue(StartupRegistryValueName)));
			return shortcutExists;
		}

		private void CreateStartupShortcut()
		{
			InvokeStartupRegistryValueAction(key => key.SetValue(StartupRegistryValueName, targetExePath), true);
		}

		private void DeleteStartupShortcuts()
		{
			InvokeStartupRegistryValueAction(key => key.DeleteValue(StartupRegistryValueName), true);
		}


		private void InvokeStartupRegistryValueAction(Action<RegistryKey> startupRegistryValueAction, bool writable = false)
		{
			using (var startupRegistryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", writable))
				startupRegistryValueAction(startupRegistryKey);
		}


		private readonly string targetExePath;
	}
}