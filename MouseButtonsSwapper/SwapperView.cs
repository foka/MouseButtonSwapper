using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MouseButtonsSwapper
{
	internal class SwapperView : IDisposable
	{
		public SwapperView(Swapper swapper)
		{
			this.swapper = swapper;
			notifyIcon = new NotifyIcon
			{
				Icon = GetCurrentIcon(),
				Text = Resources.IconTooltip,
				Visible = true,
			};
			notifyIcon.MouseClick += notifyIcon_Click;
			notifyIcon.MouseDoubleClick += (sender, args) => { SwapButtons(); notifyIconClickTimer.Enabled = false; };

			contextMenu = CreateContextMenu();

			notifyIconClickTimer = new Timer { Interval = SystemInformation.DoubleClickTime };
			notifyIconClickTimer.Tick += notifyIconClickTimer_Tick;
		}


		private void notifyIconClickTimer_Tick(object sender, EventArgs e)
		{
			notifyIconClickTimer.Enabled = false;

			notifyIcon.ContextMenu = contextMenu;
			var mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
			mi.Invoke(notifyIcon, null);
			notifyIcon.ContextMenu = null;
		}


		private void notifyIcon_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				return;

			// second button clicked:

			if (notifyIconClickTimer.Enabled)
			{
				notifyIconClickTimer.Enabled = false;
				SwapButtons();
			}
			else
			{
				notifyIconClickTimer.Enabled = true;
			}
		}


		private ContextMenu CreateContextMenu()
		{
			var menu = new ContextMenu();

			menu.MenuItems.Add(Resources.MenuSwap, (s, a) => SwapButtons()).Enabled = false;
			menu.MenuItems.Add("-");
//			TODO:
//			menu.MenuItems.Add(Resources.ChangeHotkey);
//			menu.MenuItems.Add(Resources.RunOnLogin, (s, a) => ((MenuItem)s).Checked = !((MenuItem)s).Checked)
//				.Checked = true;
//			menu.MenuItems.Add("-");
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
		private readonly NotifyIcon notifyIcon;
		private readonly Timer notifyIconClickTimer;
		private readonly ContextMenu contextMenu;
	}
}