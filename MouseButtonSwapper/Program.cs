using System;
using System.Windows.Forms;
using MouseButtonSwapper.Tools;

namespace MouseButtonSwapper
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			using (new SwapperView(new Swapper(), new Startup(Application.ExecutablePath)))
				Application.Run();

			Settings.Default.Save();
		}
	}
}
