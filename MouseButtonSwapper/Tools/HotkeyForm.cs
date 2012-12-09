using System.Windows.Forms;

namespace MouseButtonSwapper.Tools
{
	public partial class HotkeyForm : Form
	{
		public HotkeyForm()
		{
			InitializeComponent();
			hotkeyTextBox.KeyPress += (sender, args) => args.Handled = true;
			hotkeyTextBox.KeyDown += HotkeyTextBoxOnKeyDown;

			enableHotkeyCheckBox.CheckedChanged += (o, eventArgs) =>
				hotkeyTextBox.Enabled = enableHotkeyCheckBox.Checked;

			Activated += (sender, args) =>
			{
				hotkeyTextBox.Enabled = enableHotkeyCheckBox.Checked;
				hotkeyTextBox.Focus();
			};
		}


		private void HotkeyTextBoxOnKeyDown(object sender, KeyEventArgs e)
		{
			if (! enableHotkeyCheckBox.Checked)
				return;
			if (e.KeyCode == KeyCode && e.Modifiers == Modifiers)
				return;

			KeyCode = e.KeyCode;
			Modifiers = e.Modifiers;

			WriteHotkeyText();

			e.Handled = true;
		}


		private void WriteHotkeyText()
		{
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
				WriteHotkeyText();
			}
		}

		private Keys keyCode;
		public Keys KeyCode
		{
			get { return keyCode; }
			set
			{
				keyCode = value;
				WriteHotkeyText();
			}
		}

		public bool HotkeyEnabled
		{
			get { return enableHotkeyCheckBox.Checked; }
			set { enableHotkeyCheckBox.Checked = value; }
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
	}

}
