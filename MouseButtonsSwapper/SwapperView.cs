using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MouseButtonsSwapper.Tools;
using MouseButtonsSwapper.Tools.Hotkey;

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
				notifyIconDoubleClickTimer.Enabled = false;
				SwapButtons();
			};

			notifyIconDoubleClickTimer = new Timer { Interval = SystemInformation.DoubleClickTime };
			notifyIconDoubleClickTimer.Tick += NotifyIconDoubleClickTimerTick;

			contextMenu = CreateContextMenu();
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
			if (notifyIconDoubleClickTimer.Enabled) // The second of double click
			{
				notifyIconDoubleClickTimer.Enabled = false;
				SwapButtons();
			}
			else // The first click - start waiting for the second click
			{
				notifyIconDoubleClickTimer.Enabled = true;
			}
		}


		private ContextMenu CreateContextMenu()
		{
			var menu = new ContextMenu();

			menu.MenuItems.Add(Resources.MenuSwap, (s, a) => SwapButtons());
			menu.MenuItems.Add("-");

			runOnStartupMenuItem = new MenuItem(Resources.RunOnStartup);
			runOnStartupMenuItem.Click += (s, a) =>
			{
				startup.RunOnStartup = ! runOnStartupMenuItem.Checked;
   				runOnStartupMenuItem.Checked = ! runOnStartupMenuItem.Checked;
			};
			menu.MenuItems.Add(runOnStartupMenuItem);

			hotkeyMenuItem = new MenuItem(Resources.Hotkey, hotkeyMenuItem_Click);
			menu.MenuItems.Add(hotkeyMenuItem);

			menu.MenuItems.Add("-");
			menu.MenuItems.Add(Resources.MenuExit, (s, a) => Application.Exit());

			return menu;
		}

		private void hotkeyMenuItem_Click(object sender, EventArgs args)
		{
			ModifierKeys modifierKeys;
			Keys key;
			var useHotkey = PromptForHotkey(out modifierKeys, out key);

			if (keyboardHook != null)
			{
				keyboardHook.Dispose();
				keyboardHook = null;				
			}

			if (useHotkey)
			{
				keyboardHook = new KeyboardHook();
				keyboardHook.KeyPressed += (s, a) => SwapButtons();
				keyboardHook.RegisterHotKey(ModifierKeys.Alt | ModifierKeys.Control, Keys.M);
			}

			hotkeyMenuItem.Checked = useHotkey;
		}

		private bool PromptForHotkey(out ModifierKeys modifierKeys, out Keys key)
		{
			modifierKeys = ModifierKeys.Alt | ModifierKeys.Control;
			key = Keys.M;

			return true;
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
			if (keyboardHook != null)
				keyboardHook.Dispose();
		}


		private readonly Swapper swapper;
		private readonly Startup startup;
		private readonly NotifyIcon notifyIcon;
		private readonly Timer notifyIconDoubleClickTimer;
		private readonly ContextMenu contextMenu;
		private MenuItem runOnStartupMenuItem;
		private MenuItem hotkeyMenuItem;

		private KeyboardHook keyboardHook;
	}
}