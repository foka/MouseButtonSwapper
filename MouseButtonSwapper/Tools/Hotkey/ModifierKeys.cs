using System;

namespace MouseButtonSwapper.Tools.Hotkey
{
	/// <summary>
	/// The enumeration of possible modifiers.
	/// </summary>
	[Flags]
	internal enum ModifierKeys : uint
	{
		Alt = 1,
		Control = 2,
		Shift = 4,
		Win = 8
	}
}