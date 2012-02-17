using System;
using System.Windows.Forms;

namespace MouseButtonsSwapper
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			using (new SwapperView(new Swapper()))
				Application.Run();
		}
	}
}
