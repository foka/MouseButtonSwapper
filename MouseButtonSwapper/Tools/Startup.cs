using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace MouseButtonSwapper.Tools
{
	public class Startup
	{
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
			return GetStartupShortcuts().Any();
		}

		private void CreateStartupShortcut()
		{
			var shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup),
			                                Path.GetFileNameWithoutExtension(targetExePath) + ".lnk");
			var shortcut = (IWshShortcut) new WshShellClass().CreateShortcut(shortcutPath);

			shortcut.TargetPath = targetExePath;
			shortcut.WindowStyle = 1;
			shortcut.Description = Path.GetFileNameWithoutExtension(targetExePath);
			shortcut.WorkingDirectory = Path.GetDirectoryName(targetExePath);
			shortcut.IconLocation = targetExePath;

			shortcut.Save();
		}

		private void DeleteStartupShortcuts()
		{
			foreach (var shortcut in GetStartupShortcuts())
				File.Delete(shortcut);
		}

		private IEnumerable<string> GetStartupShortcuts()
		{
			var allStartupShortcuts = Directory.GetFiles(
				Environment.GetFolderPath(Environment.SpecialFolder.Startup), "*.lnk");

			return allStartupShortcuts.Where(s => GetShortcutTargetFile(s)
				.EndsWith(targetExePath, StringComparison.InvariantCultureIgnoreCase));
		}

		private string GetShortcutTargetFile(string shortcutFilename)
		{
			var pathOnly = Path.GetDirectoryName(shortcutFilename);
			var filenameOnly = Path.GetFileName(shortcutFilename);

			Shell32.Shell shell = new Shell32.ShellClass();
			Shell32.Folder folder = shell.NameSpace(pathOnly);
			Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);

			var link = (Shell32.ShellLinkObject)folderItem.GetLink;
			return link.Path;
		}


		private readonly string targetExePath;
	}
}