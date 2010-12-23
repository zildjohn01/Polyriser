namespace Polyriser {
	partial class TestForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.txtPrompt = new System.Windows.Forms.TextBox();
			this.txtResponse = new System.Windows.Forms.TextBox();
			this.cmdCheck = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPrompt
			// 
			this.txtPrompt.Location = new System.Drawing.Point(12, 12);
			this.txtPrompt.Multiline = true;
			this.txtPrompt.Name = "txtPrompt";
			this.txtPrompt.ReadOnly = true;
			this.txtPrompt.Size = new System.Drawing.Size(404, 103);
			this.txtPrompt.TabIndex = 0;
			// 
			// txtResponse
			// 
			this.txtResponse.Location = new System.Drawing.Point(12, 121);
			this.txtResponse.Multiline = true;
			this.txtResponse.Name = "txtResponse";
			this.txtResponse.Size = new System.Drawing.Size(404, 103);
			this.txtResponse.TabIndex = 0;
			this.txtResponse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResponse_KeyDown);
			// 
			// cmdCheck
			// 
			this.cmdCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCheck.Location = new System.Drawing.Point(154, 230);
			this.cmdCheck.Name = "cmdCheck";
			this.cmdCheck.Size = new System.Drawing.Size(120, 40);
			this.cmdCheck.TabIndex = 2;
			this.cmdCheck.Text = "Done!";
			this.cmdCheck.Click += new System.EventHandler(this.cmdCheck_Click);
			// 
			// TestForm
			// 
			this.AcceptButton = this.cmdCheck;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(428, 282);
			this.ControlBox = false;
			this.Controls.Add(this.cmdCheck);
			this.Controls.Add(this.txtResponse);
			this.Controls.Add(this.txtPrompt);
			this.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TestForm";
			this.Text = "TestForm";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPrompt;
		private System.Windows.Forms.TextBox txtResponse;
		private System.Windows.Forms.Button cmdCheck;
	}
}