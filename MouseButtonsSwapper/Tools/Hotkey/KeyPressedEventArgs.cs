using System;
using System.Windows.Forms;

namespace MouseButtonsSwapper.Tools.Hotkey
{
	/// <summary>
	/// Event Args for the event that is fired after the hot key has been pressed.
	/// </summary>
	internal class KeyPressedEventArgs : EventArgs
	{
		private readonly ModifierKeys modifier;
		private readonly Keys key;

		internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
		{
			this.modifier = modifier;
			this.key = key;
		}

		public ModifierKeys Modifier
		{
			get { return modifier; }
		}

		public Keys Key
		{
			get { return key; }
		}
	}
}