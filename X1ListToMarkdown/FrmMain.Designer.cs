﻿namespace X1ListToMarkdown
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
			this.btnConvertToWiki = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnConvertToSidebar = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(12, 14);
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(394, 20);
			this.txtPath.TabIndex = 0;
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// btnPath
			// 
			this.btnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPath.Location = new System.Drawing.Point(412, 12);
			this.btnPath.Name = "btnPath";
			this.btnPath.Size = new System.Drawing.Size(75, 23);
			this.btnPath.TabIndex = 1;
			this.btnPath.Text = "Browse";
			this.btnPath.UseVisualStyleBackColor = true;
			this.btnPath.Click += new System.EventHandler(this.btnCsvPath_Click);
			// 
			// btnConvertToWiki
			// 
			this.btnConvertToWiki.Enabled = false;
			this.btnConvertToWiki.Location = new System.Drawing.Point(12, 40);
			this.btnConvertToWiki.Name = "btnConvertToWiki";
			this.btnConvertToWiki.Size = new System.Drawing.Size(237, 23);
			this.btnConvertToWiki.TabIndex = 2;
			this.btnConvertToWiki.Text = "Convert To Wiki";
			this.btnConvertToWiki.UseVisualStyleBackColor = true;
			this.btnConvertToWiki.Click += new System.EventHandler(this.btnConvertToWiki_Click);
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
			this.txtResult.TextChanged += new System.EventHandler(this.txtResult_TextChanged);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Enabled = false;
			this.btnCopy.Location = new System.Drawing.Point(12, 393);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(384, 23);
			this.btnCopy.TabIndex = 4;
			this.btnCopy.Text = "Copy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnConvertToSidebar
			// 
			this.btnConvertToSidebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConvertToSidebar.Enabled = false;
			this.btnConvertToSidebar.Location = new System.Drawing.Point(250, 40);
			this.btnConvertToSidebar.Name = "btnConvertToSidebar";
			this.btnConvertToSidebar.Size = new System.Drawing.Size(237, 23);
			this.btnConvertToSidebar.TabIndex = 5;
			this.btnConvertToSidebar.Text = "Convert To Sidebar";
			this.btnConvertToSidebar.UseVisualStyleBackColor = true;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Enabled = false;
			this.btnClear.Location = new System.Drawing.Point(397, 393);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(90, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 428);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnConvertToSidebar);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnConvertToWiki);
			this.Controls.Add(this.btnPath);
			this.Controls.Add(this.txtPath);
			this.Name = "FrmMain";
			this.Text = "X1 CSV To Markdown";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnPath;
		private System.Windows.Forms.Button btnConvertToWiki;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnConvertToSidebar;
		private System.Windows.Forms.Button btnClear;
	}
}

