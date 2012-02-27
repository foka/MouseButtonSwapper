using System.Windows.Forms;

namespace MouseButtonsSwapper.Tools
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
			var shift = Modifiers.HasFlag(Keys.Shift);
			var control = Modifiers.HasFlag(Keys.Control);
			var alt = Modifiers.HasFlag(Keys.Alt);

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

		private void okButton_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void HotkeyForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				DialogResult = DialogResult.Cancel;
		}
	}

}
