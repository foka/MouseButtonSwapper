using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MouseButtonsSwapper
{
	internal class SwapperView : IDisposable
	{
		public SwapperView(Swapper swapper, Startup startup)
		{
			this.swapper = swapper;
			this.startup = startup;


			notifyIcon = new NotifyIcon
			{
				Icon = GetCurrentIcon(),
				Text = Resources.IconTooltip,
				Visible = true,
			};
			notifyIcon.MouseClick += notifyIcon_Click;
			notifyIcon.MouseDoubleClick += (sender, args) =>
			{
			    SwapButtons();
				notifyIconDoubleClickTimer.Enabled = false;
			};

			contextMenu = CreateContextMenu();

			notifyIconDoubleClickTimer = new Timer { Interval = SystemInformation.DoubleClickTime };
			notifyIconDoubleClickTimer.Tick += NotifyIconDoubleClickTimerTick;
		}


		private void NotifyIconDoubleClickTimerTick(object sender, EventArgs e)
		{
			notifyIconDoubleClickTimer.Enabled = false;

			ShowContextMenu();
		}

		private void ShowContextMenu()
		{
			runOnStartupMenuItem.Checked = startup.RunOnStartup;

			notifyIcon.ContextMenu = contextMenu;
			var showContextMenuMethod = typeof(NotifyIcon).GetMethod("ShowContextMenu",
				BindingFlags.Instance | BindingFlags.NonPublic);
			showContextMenuMethod.Invoke(notifyIcon, null);
			notifyIcon.ContextMenu = null;
		}


		private void notifyIcon_Click(object sender, MouseEventArgs e)
		{
			if (notifyIconDoubleClickTimer.Enabled) // Double click
			{
				notifyIconDoubleClickTimer.Enabled = false;
				SwapButtons();
			}
			else // First click - waiting for the second click
			{
				notifyIconDoubleClickTimer.Enabled = true;
			}
		}


		private ContextMenu CreateContextMenu()
		{
			var menu = new ContextMenu();

			menu.MenuItems.Add(Resources.MenuSwap, (s, a) => SwapButtons());
			menu.MenuItems.Add("-");

//			TODO:
//			menu.MenuItems.Add(Resources.ChangeHotkey);

			runOnStartupMenuItem = new MenuItem(Resources.RunOnStartup);
			runOnStartupMenuItem.Click += (s, a) =>
			{
				startup.RunOnStartup = ! runOnStartupMenuItem.Checked;
   				runOnStartupMenuItem.Checked = ! runOnStartupMenuItem.Checked;
			};
			menu.MenuItems.Add(runOnStartupMenuItem);

			menu.MenuItems.Add("-");
			menu.MenuItems.Add(Resources.MenuExit, (s, a) => Application.Exit());

			return menu;
		}

		private Icon GetCurrentIcon()
		{
			return swapper.IsSwapped ? Resources.MouseSwapped : Resources.MouseNormal;
		}

		private void SwapButtons()
		{
			swapper.SwapButtons();
			notifyIcon.Icon = GetCurrentIcon();
		}


		public void Dispose()
		{
			notifyIcon.Dispose();
		}


		private readonly Swapper swapper;
		private readonly Startup startup;
		private readonly NotifyIcon notifyIcon;
		private readonly Timer notifyIconDoubleClickTimer;
		private readonly ContextMenu contextMenu;
		private MenuItem runOnStartupMenuItem;
	}
}