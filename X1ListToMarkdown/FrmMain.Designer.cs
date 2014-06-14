namespace X1ListToMarkdown
{
	partial class FrmMain
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
			this.txtCsvPath = new System.Windows.Forms.TextBox();
			this.btnCsvPath = new System.Windows.Forms.Button();
			this.btnConvert = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtCsvPath
			// 
			this.txtCsvPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCsvPath.Location = new System.Drawing.Point(12, 14);
			this.txtCsvPath.Name = "txtCsvPath";
			this.txtCsvPath.ReadOnly = true;
			this.txtCsvPath.Size = new System.Drawing.Size(394, 20);
			this.txtCsvPath.TabIndex = 0;
			// 
			// btnCsvPath
			// 
			this.btnCsvPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCsvPath.Location = new System.Drawing.Point(412, 12);
			this.btnCsvPath.Name = "btnCsvPath";
			this.btnCsvPath.Size = new System.Drawing.Size(75, 23);
			this.btnCsvPath.TabIndex = 1;
			this.btnCsvPath.Text = "Browse";
			this.btnCsvPath.UseVisualStyleBackColor = true;
			this.btnCsvPath.Click += new System.EventHandler(this.btnCsvPath_Click);
			// 
			// btnConvert
			// 
			this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConvert.Location = new System.Drawing.Point(12, 40);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(475, 23);
			this.btnConvert.TabIndex = 2;
			this.btnConvert.Text = "Convert";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// txtResult
			// 
			this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtResult.Location = new System.Drawing.Point(12, 69);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResult.Size = new System.Drawing.Size(475, 318);
			this.txtResult.TabIndex = 3;
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(12, 393);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(475, 23);
			this.btnCopy.TabIndex = 4;
			this.btnCopy.Text = "Copy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 428);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(this.btnCsvPath);
			this.Controls.Add(this.txtCsvPath);
			this.Name = "FrmMain";
			this.Text = "X1 CSV To Markdown";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtCsvPath;
		private System.Windows.Forms.Button btnCsvPath;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnCopy;
	}
}

