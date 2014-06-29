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
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnPath = new System.Windows.Forms.Button();
			this.btnConvertToGamesWiki = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnConvertToSidebar = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnConvertToGwGWiki = new System.Windows.Forms.Button();
			this.btnConvertToDwGWiki = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(16, 17);
			this.txtPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(524, 22);
			this.txtPath.TabIndex = 0;
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// btnPath
			// 
			this.btnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPath.Location = new System.Drawing.Point(549, 15);
			this.btnPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnPath.Name = "btnPath";
			this.btnPath.Size = new System.Drawing.Size(100, 28);
			this.btnPath.TabIndex = 1;
			this.btnPath.Text = "Browse";
			this.btnPath.UseVisualStyleBackColor = true;
			this.btnPath.Click += new System.EventHandler(this.btnCsvPath_Click);
			// 
			// btnConvertToGamesWiki
			// 
			this.btnConvertToGamesWiki.Enabled = false;
			this.btnConvertToGamesWiki.Location = new System.Drawing.Point(16, 49);
			this.btnConvertToGamesWiki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnConvertToGamesWiki.Name = "btnConvertToGamesWiki";
			this.btnConvertToGamesWiki.Size = new System.Drawing.Size(316, 28);
			this.btnConvertToGamesWiki.TabIndex = 2;
			this.btnConvertToGamesWiki.Text = "Convert To Games Wiki";
			this.btnConvertToGamesWiki.UseVisualStyleBackColor = true;
			this.btnConvertToGamesWiki.Click += new System.EventHandler(this.btnConvertToWiki_Click);
			// 
			// txtResult
			// 
			this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtResult.Location = new System.Drawing.Point(16, 121);
			this.txtResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResult.Size = new System.Drawing.Size(632, 355);
			this.txtResult.TabIndex = 3;
			this.txtResult.TextChanged += new System.EventHandler(this.txtResult_TextChanged);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Enabled = false;
			this.btnCopy.Location = new System.Drawing.Point(16, 484);
			this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(512, 28);
			this.btnCopy.TabIndex = 4;
			this.btnCopy.Text = "Copy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnConvertToSidebar
			// 
			this.btnConvertToSidebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConvertToSidebar.Enabled = false;
			this.btnConvertToSidebar.Location = new System.Drawing.Point(333, 49);
			this.btnConvertToSidebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnConvertToSidebar.Name = "btnConvertToSidebar";
			this.btnConvertToSidebar.Size = new System.Drawing.Size(316, 28);
			this.btnConvertToSidebar.TabIndex = 5;
			this.btnConvertToSidebar.Text = "Convert To Sidebar";
			this.btnConvertToSidebar.UseVisualStyleBackColor = true;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Enabled = false;
			this.btnClear.Location = new System.Drawing.Point(529, 484);
			this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(120, 28);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnConvertToGwGWiki
			// 
			this.btnConvertToGwGWiki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConvertToGwGWiki.Enabled = false;
			this.btnConvertToGwGWiki.Location = new System.Drawing.Point(333, 85);
			this.btnConvertToGwGWiki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnConvertToGwGWiki.Name = "btnConvertToGwGWiki";
			this.btnConvertToGwGWiki.Size = new System.Drawing.Size(316, 28);
			this.btnConvertToGwGWiki.TabIndex = 7;
			this.btnConvertToGwGWiki.Text = "Convert to GwG Wiki";
			this.btnConvertToGwGWiki.UseVisualStyleBackColor = true;
			this.btnConvertToGwGWiki.Click += new System.EventHandler(this.btnConvertToGwGWiki_Click);
			// 
			// btnConvertToDwGWiki
			// 
			this.btnConvertToDwGWiki.Enabled = false;
			this.btnConvertToDwGWiki.Location = new System.Drawing.Point(16, 85);
			this.btnConvertToDwGWiki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnConvertToDwGWiki.Name = "btnConvertToDwGWiki";
			this.btnConvertToDwGWiki.Size = new System.Drawing.Size(316, 28);
			this.btnConvertToDwGWiki.TabIndex = 8;
			this.btnConvertToDwGWiki.Text = "Convert To DwG Wiki";
			this.btnConvertToDwGWiki.UseVisualStyleBackColor = true;
			this.btnConvertToDwGWiki.Click += new System.EventHandler(this.btnConvertToDwGWiki_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(665, 527);
			this.Controls.Add(this.btnConvertToDwGWiki);
			this.Controls.Add(this.btnConvertToGwGWiki);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnConvertToSidebar);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnConvertToGamesWiki);
			this.Controls.Add(this.btnPath);
			this.Controls.Add(this.txtPath);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "FrmMain";
			this.Text = "X1 CSV To Markdown";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnPath;
		private System.Windows.Forms.Button btnConvertToGamesWiki;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnConvertToSidebar;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnConvertToGwGWiki;
		private System.Windows.Forms.Button btnConvertToDwGWiki;
	}
}

