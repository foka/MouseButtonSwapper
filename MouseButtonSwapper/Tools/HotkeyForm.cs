﻿using System.Windows.Forms;

namespace MouseButtonSwapper.Tools
{
	public partial class HotkeyForm : Form
	{
		public HotkeyForm()
		{
			InitializeComponent();
			hotkeyTextBox.KeyPress += (sender, args) => args.Handled = true;
			hotkeyTextBox.KeyDown += HotkeyTextBoxOnKeyDown;

			Activated += (sender, args) => hotkeyTextBox.Focus();
		}


		private void HotkeyTextBoxOnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == KeyCode && e.Modifiers == Modifiers)
				return;
			if (e.Modifiers == Keys.None)
			{
				if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
				{
					KeyCode = Keys.None;
					Modifiers = Keys.None;
				}
			}
			else
			{
				KeyCode = e.KeyCode;
				Modifiers = e.Modifiers;
			}

			OnKeysChanged();

			e.Handled = true;
		}


		private void OnKeysChanged()
		{
			unbindButton.Enabled = KeyCode != Keys.None;

			// Write keys
			var shift = EnumExtenstion.HasFlag(Modifiers, Keys.Shift);
			var control = EnumExtenstion.HasFlag(Modifiers, Keys.Control);
			var alt = EnumExtenstion.HasFlag(Modifiers, Keys.Alt);
			hotkeyTextBox.Text = (shift ? "Shift + " : "") + (control ? "Ctrl + " : "")
				+ (alt ? "Alt + " : "") + KeyCode;
		}


		private Keys modifiers;
		public Keys Modifiers
		{
			get { return modifiers; }
			set
			{
				modifiers = value;
				OnKeysChanged();
			}
		}

		private Keys keyCode;
		public Keys KeyCode
		{
			get { return keyCode; }
			set
			{
				keyCode = value;
				OnKeysChanged();
			}
		}

		public bool HotkeyEnabled
		{
			get { return KeyCode != Keys.None; }
		}

		private void OkButtonClick(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void HotkeyFormKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				DialogResult = DialogResult.Cancel;
		}

		private void UnbindButtonClick(object sender, System.EventArgs e)
		{
			KeyCode = Keys.None;
			Modifiers = Keys.None;
			DialogResult = DialogResult.OK;
		}
	}

}
