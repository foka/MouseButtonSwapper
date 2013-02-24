using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MouseButtonSwapper
{
	public class Swapper
	{
		public bool IsSwapped
		{
			get { return SystemInformation.MouseButtonsSwapped; }
		}

		public void SwapButtons()
		{
			SwapMouseButton(IsSwapped ? 0 : 1);
			using (var swapKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Mouse"))
				swapKey.SetValue("SwapMouseButtons", IsSwapped ? "1" : "0", RegistryValueKind.String);
		}

		[DllImport("user32.dll")]
		private static extern Int32 SwapMouseButton(int bSwap);
	}
}