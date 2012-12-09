namespace MouseButtonSwapper.Tools
{
	partial class HotkeyForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.hotkeyTextBox = new System.Windows.Forms.TextBox();
			this.enableHotkeyCheckBox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// hotkeyTextBox
			// 
			this.hotkeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hotkeyTextBox.Location = new System.Drawing.Point(34, 9);
			this.hotkeyTextBox.Name = "hotkeyTextBox";
			this.hotkeyTextBox.Size = new System.Drawing.Size(155, 20);
			this.hotkeyTextBox.TabIndex = 1;
			// 
			// enableHotkeyCheckBox
			// 
			this.enableHotkeyCheckBox.AutoSize = true;
			this.enableHotkeyCheckBox.Location = new System.Drawing.Point(12, 12);
			this.enableHotkeyCheckBox.Name = "enableHotkeyCheckBox";
			this.enableHotkeyCheckBox.Size = new System.Drawing.Size(15, 14);
			this.enableHotkeyCheckBox.TabIndex = 0;
			this.enableHotkeyCheckBox.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(196, 9);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(37, 20);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// HotkeyForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(245, 38);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.enableHotkeyCheckBox);
			this.Controls.Add(this.hotkeyTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "HotkeyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Hotkey - Mouse Button Swapper";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyFormKeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox hotkeyTextBox;
		private System.Windows.Forms.CheckBox enableHotkeyCheckBox;
		private System.Windows.Forms.Button okButton;

	}
}