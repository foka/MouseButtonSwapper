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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeyForm));
			this.hotkeyTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.unbindButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// hotkeyTextBox
			// 
			this.hotkeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hotkeyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.hotkeyTextBox.Location = new System.Drawing.Point(12, 9);
			this.hotkeyTextBox.Name = "hotkeyTextBox";
			this.hotkeyTextBox.Size = new System.Drawing.Size(221, 20);
			this.hotkeyTextBox.TabIndex = 1;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.okButton.Location = new System.Drawing.Point(12, 35);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(144, 22);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// unbindButton
			// 
			this.unbindButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.unbindButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.unbindButton.Location = new System.Drawing.Point(162, 35);
			this.unbindButton.Name = "unbindButton";
			this.unbindButton.Size = new System.Drawing.Size(71, 22);
			this.unbindButton.TabIndex = 3;
			this.unbindButton.Text = "Unbind";
			this.unbindButton.UseVisualStyleBackColor = true;
			this.unbindButton.Click += new System.EventHandler(this.UnbindButtonClick);
			// 
			// HotkeyForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(245, 65);
			this.Controls.Add(this.unbindButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.hotkeyTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button unbindButton;

	}
}