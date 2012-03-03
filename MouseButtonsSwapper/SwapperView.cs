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

			if (Settings.Default.UseHotkey)
			{
				hotkeyMenuItem.Checked = true;
				RegisterHotkeyFromSettings();
			}
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

			menu.MenuItems.Add(Resources.MenuSwap, (s, a) => SwapButtons())
				.DefaultItem = true;
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
			ModifierKeys? newModifiers;
			Keys? newKey;
			var hotkeyChanged = PromptForHotkey(out newModifiers, out newKey);
			if (! hotkeyChanged)
				return;

			if (keyboardHook != null)
			{
				keyboardHook.Dispose();
				keyboardHook = null;				
			}

			Settings.Default.UseHotkey = newKey != null && newModifiers != null;
			if (Settings.Default.UseHotkey)
			{
				Settings.Default.HotkeyModifiers = (int) newModifiers.Value;
				Settings.Default.HotkeyKey = (int) newKey.Value;

				RegisterHotkeyFromSettings();
			}
			else
			{
				Settings.Default.HotkeyModifiers = 0;
				Settings.Default.HotkeyKey = 0;
			}

			hotkeyMenuItem.Checked = Settings.Default.UseHotkey;
		}

		private void RegisterHotkeyFromSettings()
		{
			keyboardHook = new KeyboardHook();
			keyboardHook.KeyPressed += (s, a) => SwapButtons();
			keyboardHook.RegisterHotKey((ModifierKeys) Settings.Default.HotkeyModifiers,
				(Keys)Settings.Default.HotkeyKey);
		}

		/// <returns>true - user changed hotkey configuration in the dialog,
		/// otherwise: false</returns>
		private bool PromptForHotkey(out ModifierKeys? newModifiers, out Keys? newKey)
		{
			newModifiers = null;
			newKey = null;

			var modifiers = (ModifierKeys) Settings.Default.HotkeyModifiers;
			var modifiersAsKeys = modifiers.HasFlag(ModifierKeys.Alt) ? Keys.Alt : 0;
			modifiersAsKeys |= modifiers.HasFlag(ModifierKeys.Control) ? Keys.Control: 0;
			modifiersAsKeys |= modifiers.HasFlag(ModifierKeys.Shift) ? Keys.Shift: 0;
			using (var hotkeyForm = new HotkeyForm
									{
										Modifiers = modifiersAsKeys,
										KeyCode = (Keys) Settings.Default.HotkeyKey,
										HotkeyEnabled = Settings.Default.UseHotkey,
									})
			{
				var dialogResult = hotkeyForm.ShowDialog();
				if (dialogResult != DialogResult.OK)
					return false;

				if (! hotkeyForm.HotkeyEnabled)
					return true;

				newKey = hotkeyForm.KeyCode;
				newModifiers = hotkeyForm.Modifiers.HasFlag(Keys.Alt) ? ModifierKeys.Alt : 0;
				newModifiers |= hotkeyForm.Modifiers.HasFlag(Keys.Control) ? ModifierKeys.Control : 0;
				newModifiers |= hotkeyForm.Modifiers.HasFlag(Keys.Shift) ? ModifierKeys.Shift : 0;
				if (newModifiers == 0)
					throw new InvalidOperationException("No modifier keys.");

				return true;
			}

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