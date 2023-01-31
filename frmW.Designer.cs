namespace wLights
{
	partial class frmW
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmW));
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
			this.grpShowDir = new System.Windows.Forms.GroupBox();
			this.chkAutoLoad = new System.Windows.Forms.CheckBox();
			this.btnLoad = new System.Windows.Forms.Button();
			this.optFolderOther = new System.Windows.Forms.RadioButton();
			this.optFolderDefault = new System.Windows.Forms.RadioButton();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtDirectoryCustom = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.pnlLeft = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlAbout = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblInfo = new System.Windows.Forms.Label();
			this.button8 = new System.Windows.Forms.Button();
			this.grpReports = new System.Windows.Forms.GroupBox();
			this.grpShowDir.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.grpReports.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpShowDir
			// 
			this.grpShowDir.Controls.Add(this.chkAutoLoad);
			this.grpShowDir.Controls.Add(this.btnLoad);
			this.grpShowDir.Controls.Add(this.optFolderOther);
			this.grpShowDir.Controls.Add(this.optFolderDefault);
			this.grpShowDir.Controls.Add(this.btnBrowse);
			this.grpShowDir.Controls.Add(this.txtDirectoryCustom);
			this.grpShowDir.Location = new System.Drawing.Point(12, 12);
			this.grpShowDir.Name = "grpShowDir";
			this.grpShowDir.Size = new System.Drawing.Size(527, 117);
			this.grpShowDir.TabIndex = 3;
			this.grpShowDir.TabStop = false;
			this.grpShowDir.Text = "Show Folder: ";
			// 
			// chkAutoLoad
			// 
			this.chkAutoLoad.AutoSize = true;
			this.chkAutoLoad.Location = new System.Drawing.Point(194, 85);
			this.chkAutoLoad.Name = "chkAutoLoad";
			this.chkAutoLoad.Size = new System.Drawing.Size(115, 17);
			this.chkAutoLoad.TabIndex = 9;
			this.chkAutoLoad.Text = "Load Automatically";
			this.chkAutoLoad.UseVisualStyleBackColor = true;
			this.chkAutoLoad.CheckedChanged += new System.EventHandler(this.chkAutoLoad_CheckedChanged);
			// 
			// btnLoad
			// 
			this.btnLoad.Enabled = false;
			this.btnLoad.Location = new System.Drawing.Point(113, 79);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 8;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// optFolderOther
			// 
			this.optFolderOther.AutoSize = true;
			this.optFolderOther.Location = new System.Drawing.Point(9, 53);
			this.optFolderOther.Name = "optFolderOther";
			this.optFolderOther.Size = new System.Drawing.Size(60, 17);
			this.optFolderOther.TabIndex = 7;
			this.optFolderOther.TabStop = true;
			this.optFolderOther.Text = "Other...";
			this.optFolderOther.UseVisualStyleBackColor = true;
			this.optFolderOther.CheckedChanged += new System.EventHandler(this.optFolderOther_CheckedChanged);
			// 
			// optFolderDefault
			// 
			this.optFolderDefault.AutoSize = true;
			this.optFolderDefault.Location = new System.Drawing.Point(9, 30);
			this.optFolderDefault.Name = "optFolderDefault";
			this.optFolderDefault.Size = new System.Drawing.Size(149, 17);
			this.optFolderDefault.TabIndex = 6;
			this.optFolderDefault.TabStop = true;
			this.optFolderDefault.Text = "X:\\2023\\xLights\\Wizlights";
			this.optFolderDefault.UseVisualStyleBackColor = true;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(435, 53);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 5;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtDirectoryCustom
			// 
			this.txtDirectoryCustom.Location = new System.Drawing.Point(72, 53);
			this.txtDirectoryCustom.Name = "txtDirectoryCustom";
			this.txtDirectoryCustom.ReadOnly = true;
			this.txtDirectoryCustom.Size = new System.Drawing.Size(357, 20);
			this.txtDirectoryCustom.TabIndex = 3;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pnlLeft,
            this.pnlStatus,
            this.pnlAbout});
			this.statusStrip1.Location = new System.Drawing.Point(0, 524);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(800, 24);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "staStatus";
			// 
			// pnlLeft
			// 
			this.pnlLeft.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlLeft.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(58, 19);
			this.pnlLeft.Text = "Whuteva";
			// 
			// pnlStatus
			// 
			this.pnlStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(674, 19);
			this.pnlStatus.Spring = true;
			this.pnlStatus.Text = "(status...)";
			// 
			// pnlAbout
			// 
			this.pnlAbout.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.pnlAbout.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pnlAbout.Name = "pnlAbout";
			this.pnlAbout.Size = new System.Drawing.Size(53, 19);
			this.pnlAbout.Text = "About...";
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.Location = new System.Drawing.Point(6, 16);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(62, 13);
			this.lblInfo.TabIndex = 6;
			this.lblInfo.Text = "Information:";
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(6, 323);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(130, 70);
			this.button8.TabIndex = 18;
			this.button8.Text = "All Models Front to Back";
			this.button8.UseVisualStyleBackColor = true;
			// 
			// grpReports
			// 
			this.grpReports.Controls.Add(this.button8);
			this.grpReports.Controls.Add(this.lblInfo);
			this.grpReports.Location = new System.Drawing.Point(12, 315);
			this.grpReports.Name = "grpReports";
			this.grpReports.Size = new System.Drawing.Size(457, 206);
			this.grpReports.TabIndex = 6;
			this.grpReports.TabStop = false;
			this.grpReports.Text = "Reports: ";
			// 
			// frmW
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 548);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.grpReports);
			this.Controls.Add(this.grpShowDir);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmW";
			this.Text = "wLights -  the xLights Data Miner";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmW_FormClosing);
			this.Load += new System.EventHandler(this.frmW_Load);
			this.Shown += new System.EventHandler(this.frmW_Shown);
			this.grpShowDir.ResumeLayout(false);
			this.grpShowDir.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.grpReports.ResumeLayout(false);
			this.grpReports.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SaveFileDialog dlgFileSave;
		private System.Windows.Forms.GroupBox grpShowDir;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtDirectoryCustom;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel pnlLeft;
		private System.Windows.Forms.ToolStripStatusLabel pnlStatus;
		private System.Windows.Forms.ToolStripStatusLabel pnlAbout;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.GroupBox grpReports;
		private System.Windows.Forms.RadioButton optFolderDefault;
		private System.Windows.Forms.CheckBox chkAutoLoad;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.RadioButton optFolderOther;
	}
}

