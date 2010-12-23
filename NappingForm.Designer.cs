namespace Polyriser {
	partial class NappingForm {
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
			this.lblTime = new System.Windows.Forms.Label();
			this.cmdClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTime
			// 
			this.lblTime.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTime.Location = new System.Drawing.Point(12, 9);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(403, 107);
			this.lblTime.TabIndex = 0;
			this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// cmdClose
			// 
			this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdClose.Location = new System.Drawing.Point(12, 119);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(403, 142);
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// NappingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(427, 273);
			this.ControlBox = false;
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lblTime);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NappingForm";
			this.Text = "NappingForm";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NappingForm_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Button cmdClose;
	}
}