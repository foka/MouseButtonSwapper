using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace MouseButtonsSwapper
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
		}


		[DllImport("user32.dll")]
		private static extern Int32 SwapMouseButton(int bSwap);
	}
}