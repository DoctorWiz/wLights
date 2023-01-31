using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Xsl;
using wLights.Properties;
using xLights22;
using FileHelper;
using FormHelper;

namespace wLights
{
	public partial class frmW : Form
	{
		private string showDir = "";
		private string fileRGBeffects = "";
		private string fileNetworks = "";
		public const string FILE_RGBEFFECTS = "xlights_rgbeffects.xml";
		public const string FILE_NETWORKS = "xlights_networks.xml";
		public static xRGBeffects RGBeffects = null;
		public static xNetworks networks = null;
		public bool shown = false;
		private Properties.Settings userSettings = Properties.Settings.Default;




		public frmW()
		{
			InitializeComponent();
		}

		public int OLD_LoadFiles(string fromFolder)
		{
			if (Fyle.PathExists(fromFolder))
			{
				fileNetworks = fromFolder + FILE_NETWORKS;
				if (Fyle.Exists(fileNetworks))
				{
					fileRGBeffects = fromFolder + FILE_RGBEFFECTS;
					if (Fyle.Exists(fileRGBeffects))
					{
						RGBeffects = new xRGBeffects(fileRGBeffects);

						string info = RGBeffects.LineCount.ToString() + " Lines\r\n";
						info += RGBeffects.Models.Count.ToString() + " Models\r\n";
						info += "With " + RGBeffects.SubMemberCount.ToString() + " SubModels\r\n";
						info += "In " + RGBeffects.ModelGroups.Count.ToString() + " Model Groups\r\n";
						info += RGBeffects.EffectCount.ToString() + " Effects\r\n";
						info += "In " + RGBeffects.EffectGroups.Count.ToString() + " Effect Groups\r\n";
						info += RGBeffects.Viewpoints.Count.ToString() + " Viewpoints\r\n";
						info += RGBeffects.Palettes.Count.ToString() + " Palettes\r\n";
						info += RGBeffects.Layouts.Count.ToString() + " Layouts\r\n";
						info += "In " + RGBeffects.LayoutGroups.Count.ToString() + " LayoutGroups\r\n";
						info += RGBeffects.Views.Count.ToString() + " Views\r\n";
						info += RGBeffects.Perspectives.Count.ToString() + " Persectives\r\n";
						info += RGBeffects.xSettings.Count.ToString() + " Settings\r\n";
						info += RGBeffects.xColors.Count.ToString() + " Colors\r\n";
						info += RGBeffects.ViewObjects.Count.ToString() + " ViewObjects\r\n";

						lblInfo.Text = info;
					}
				}
			}

			return RGBeffects.ErrorCount;
		}

		public int OLD_SaveFile(string rgbEffectsFile, int order)
		{
			int err = 0;
			string lineOut = "";

			StreamWriter writer = new StreamWriter(rgbEffectsFile);
			lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			writer.WriteLine(lineOut);
			lineOut = "<xrgb>";
			writer.WriteLine(lineOut);
			
			// Models First
			lineOut = "  <models>";
			writer.WriteLine(lineOut);
			for (int m=0; m< RGBeffects.Models.Count; m++)
			{
				xModel model = RGBeffects.Models[m];
				lineOut = model.xmlInfo;
				writer.WriteLine(lineOut);
				//TODO: Controller Connection
				lineOut = "    </model>";
				writer.WriteLine(lineOut);
				if (model.mySubModels.Count > 0)
				{
					for (int s=0; s< model.mySubModels.Count; s++)
					{
						xSubModel subModel = model.mySubModels[s];
						lineOut = subModel.xmlInfo;
						writer.WriteLine(lineOut);
						//TODO: Controller Connection
						lineOut = "      /subModel>";
						writer.WriteLine(lineOut);
					} // end SubModel Loop
				} // end has SubModels
			} // end Model loop
			lineOut = "  </models>";
			writer.WriteLine(lineOut);

			// View Objects next (not to be confused with Views or Viewpoints)
			lineOut = "  <view_objects>";
			writer.WriteLine(lineOut);
			for (int vo=0; vo< RGBeffects.ViewObjects.Count; vo++)
			{
				xViewObject viewObject = RGBeffects.ViewObjects[vo];
				lineOut= viewObject.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </view_objects>";
			writer.WriteLine(lineOut);

			// Effect Groups and their Effects come next
			//TODO: Collect, Save, Output the version number
			lineOut = "  <effects version=\"0007\">";
			writer.WriteLine(lineOut);
			for (int eg=0; eg< RGBeffects.EffectGroups.Count; eg++)
			{
				xEffectGroup effectGroup = RGBeffects.EffectGroups[eg];
				lineOut= effectGroup.xmlInfo;
				writer.WriteLine(lineOut);
				if (effectGroup.Effects.Count > 0)
				{
					for (int ef=0; ef < effectGroup.Effects.Count; ef++)
					{
						//TODO: Figure out how to handle recursiveness
					}
				}
			}
			lineOut = "  </effectGroup>";
			writer.WriteLine(lineOut);

			// Next up is Views (not to be confused with Viewpoints or ViewObjects)
			lineOut = "  <views>";
			writer.WriteLine(lineOut);
			for (int v=0; v< RGBeffects.Views.Count; v++)
			{
				xView view = RGBeffects.Views[v];
				lineOut= view.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </views>";
			writer.WriteLine(lineOut);

			// Palettes next (if any)
			if (RGBeffects.Palettes.Count < 1)
			{
				lineOut = "  <palettes/>";
				writer.WriteLine(lineOut);
			}
			else
			{
				lineOut = "  <palettes>";
				writer.WriteLine(lineOut);
				for (int p = 0; p < RGBeffects.Palettes.Count; p++)
				{
					xPalette palette = RGBeffects.Palettes[p];
					lineOut= palette.xmlInfo;
					writer.WriteLine(lineOut);
				}
				lineOut = "  </palletes>";
				writer.WriteLine(lineOut);
			}

			// Now come the Model Groups, which can be recursive


			// LayoutGroups and Layouts next (if any)
			if (RGBeffects.LayoutGroups.Count < 1)
			{
				lineOut = "  <layoutGroups/>";
				writer.WriteLine(lineOut);
			}
			else
			{
				lineOut = "  <layoutGroups>";
				writer.WriteLine(lineOut);
				for (int lg = 0; lg < RGBeffects.LayoutGroups.Count; lg++)
				{
					xLayoutGroup layoutGroup = RGBeffects.LayoutGroups[lg];
					lineOut = layoutGroup.xmlInfo;
					writer.WriteLine(lineOut);
				}
				lineOut = "  </layoutGroups>";
				writer.WriteLine(lineOut);
			}

			// Now Perspectives
			//TODO: Collect, Save, and Output the current perspective
			lineOut = "  <perspectives current=\"Default Perspective\">";
			writer.WriteLine(lineOut);
			for (int p = 0; p < RGBeffects.Perspectives.Count; p++)
			{
				xPerspective perspective = RGBeffects.Perspectives[p];
				lineOut = perspective.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </perspectives>";
			writer.WriteLine(lineOut);

			// Now Settings
			lineOut = "  <settings>";
			writer.WriteLine(lineOut);
			for (int s = 0; s < RGBeffects.xSettings.Count; s++)
			{
				xSetting sett = RGBeffects.xSettings[s];
				lineOut = sett.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </settings>";
			writer.WriteLine(lineOut);

			// Followed by Colors
			lineOut = "  <colors>";
			writer.WriteLine(lineOut);
			for (int c = 0; c < RGBeffects.xColors.Count; c++)
			{
				xColor clr = RGBeffects.xColors[c];
				lineOut = clr.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </colors>";
			writer.WriteLine(lineOut);

			// Followed by Viewpoints (not to be confused with Views or View Objects)
			lineOut = "  <Viewpoints>";
			writer.WriteLine(lineOut);
			for (int vp = 0; vp < RGBeffects.Viewpoints.Count; vp++)
			{
				xViewpoint viewpoint = RGBeffects.Viewpoints[vp];
				lineOut = viewpoint.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </Viewpoints>";
			writer.WriteLine(lineOut);

			// Finally, finish out the file
			lineOut = "</xrgb>";
			writer.WriteLine(lineOut);
			writer.Close();

			return err;
		}

		private void button3_Click(object sender, EventArgs e)
		{

		}

		private void frmW_Shown(object sender, EventArgs e)
		{
			if (!shown)
			{
				shown = true;
				FirstShow();
			}
		}

		private void FirstShow()
		{
			// Standard Show Directory, or Custom?
			if (optFolderDefault.Checked)
			{
				showDir = xAdmin.ShowDirectory;
			}
			else if (optFolderOther.Checked)
			{
				showDir = txtDirectoryCustom.Text.Trim();
			}

			// Test validity: Does folder exist?  Does it contain networks and rgbeffects files?			
			bool validPath = Fyle.PathExists(showDir);
			bool loaded = false;
			bool allGood = false;
			if (validPath)
			{
				fileRGBeffects = showDir + "FILE_RGBEFFECTS";
				fileNetworks = showDir + "FILE_NETWORKS";
				if (Fyle.Exists(fileNetworks))
					{
					if (Fyle.Exists(fileRGBeffects))
					{
						// Valid!
						allGood = true;
					}
				}
			}

		
			if (allGood)
			{
				if (chkAutoLoad.Checked)
				{
					// Attempt to Load automatically
					networks = new xNetworks(fileNetworks);
					if (xNetworks.Controllers.Count > 0)
					{
						RGBeffects = new xRGBeffects(showDir);
						loaded = true;
						// leave Load button disabled, no need to reload
						userSettings.LastPath = showDir;
						if (optFolderOther.Checked)
						{
							userSettings.OtherFolderPath = showDir;
						}
						userSettings.Save();
						ShowStatistics();
					}
				}
				else
				{
					// Load button is disabled by default at start.
					// Everything looks good, but AutoLoad is turned off
					// So allow user to Load manually
					btnLoad.Enabled = true;
				}
			}
			// ELSE Invalid, all is not good!
			// but Load button is already disabled by default at startup so leave it that way.
		}


		private void frmW_Load(object sender, EventArgs e)
		{
			optFolderDefault.Text = xAdmin.ShowDirectory;
			optFolderOther.Checked = userSettings.UseOtherFolder;
			chkAutoLoad.Checked = userSettings.AutoLoad;
			txtDirectoryCustom.Text = userSettings.OtherFolderPath;
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			// Standard Show Directory, or Custom?
			if (optFolderDefault.Checked)
			{
				showDir = xAdmin.ShowDirectory;
			}
			else if (optFolderOther.Checked)
			{
				showDir = txtDirectoryCustom.Text.Trim();
			}

			// Test validity: Does folder exist?  Does it contain networks and rgbeffects files?			
			bool validPath = Fyle.PathExists(showDir);
			bool loaded = false;
			bool allGood = false;
			if (validPath)
			{
				fileNetworks = showDir + "FILE_NETWORKS";
				if (Fyle.Exists(fileNetworks))
				{
					networks = new xNetworks(fileNetworks);
					if (xNetworks.Controllers.Count > 0)
					{
						fileRGBeffects = showDir + "FILE_RGBEFFECTS";
						if (Fyle.Exists(fileRGBeffects))
						{
							// Valid!
							allGood = true;
							RGBeffects = new xRGBeffects(showDir);
							loaded = true;
							userSettings.LastPath = showDir;
							if (optFolderOther.Checked)
							{
								userSettings.OtherFolderPath= showDir;
							}
							userSettings.Save();
							ShowStatistics();
						}
					}
				}
			}

			// If the file loaded, we don't need to reload, so disable this button
			// or if it didn't load that is probably because Show Directory Path is invalid
			// or the networks and/or rgbeffects files are missing.  So in that
			// case we also want to disable the button.
			btnLoad.Enabled = false;
		}

		private void ShowStatistics()
		{
			string info = RGBeffects.LineCount.ToString() + " Lines\r\n";
			info += RGBeffects.Models.Count.ToString() + " Models\r\n";
			info += "With " + RGBeffects.SubMemberCount.ToString() + " SubModels\r\n";
			info += "In " + RGBeffects.ModelGroups.Count.ToString() + " Model Groups\r\n";
			info += RGBeffects.EffectCount.ToString() + " Effects\r\n";
			info += "In " + RGBeffects.EffectGroups.Count.ToString() + " Effect Groups\r\n";
			info += RGBeffects.Viewpoints.Count.ToString() + " Viewpoints\r\n";
			info += RGBeffects.Palettes.Count.ToString() + " Palettes\r\n";
			info += RGBeffects.Layouts.Count.ToString() + " Layouts\r\n";
			info += "In " + RGBeffects.LayoutGroups.Count.ToString() + " LayoutGroups\r\n";
			info += RGBeffects.Views.Count.ToString() + " Views\r\n";
			info += RGBeffects.Perspectives.Count.ToString() + " Persectives\r\n";
			info += RGBeffects.xSettings.Count.ToString() + " Settings\r\n";
			info += RGBeffects.xColors.Count.ToString() + " Colors\r\n";
			info += RGBeffects.ViewObjects.Count.ToString() + " ViewObjects\r\n";

			lblInfo.Text = info;
		}

		private void optFolderOther_CheckedChanged(object sender, EventArgs e)
		{
			btnBrowse.Enabled = optFolderOther.Checked;
			userSettings.UseOtherFolder = optFolderOther.Checked;
			userSettings.Save();
			if (optFolderOther.Checked)
			{
				showDir = txtDirectoryCustom.Text.Trim();
			}
			else
			{
				showDir = xAdmin.ShowDirectory;
			}

			// Test validity: Does folder exist?  Does it contain networks and rgbeffects files?			
			bool validPath = Fyle.PathExists(showDir);
			bool allGood = false;
			if (validPath)
			{
				fileNetworks = showDir + "FILE_NETWORKS";
				fileRGBeffects = showDir + FILE_RGBEFFECTS;
				if (Fyle.Exists(fileNetworks))
				{
					if (Fyle.Exists(fileRGBeffects))
					{
						// Valid!
						allGood = true;
					}
				}
			}
			btnLoad.Enabled = allGood;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			// start with the last custom path used
			string startPath = txtDirectoryCustom.Text.Trim();
			if (!Fyle.PathExists(startPath))
			{
				// if no longer exists, use default show directory instead
				startPath = xAdmin.ShowDirectory;
			}
			// Prepare and set up File Open Dialog
			// We are looking not for just any ol' directory, but one that contains an rgbeffects file
			// so add rgbeffects to default filename
			dlgFileOpen.FileName = startPath + FILE_RGBEFFECTS;
			// AND make it part of the filter
			dlgFileOpen.Filter = "Show Directory with networks and rgbeffects|xlights_rgbeffects.xml";
			dlgFileOpen.DefaultExt = "xml";
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Title = "Select Show Directory";
			// Show Dialog- prompt user to select a Show Directory
			DialogResult dr = dlgFileOpen.ShowDialog();
			// OK, not Cancel?
			if (dr == DialogResult.OK)
			{
				string fn = dlgFileOpen.FileName;
				// Validate directory exists and verify it contains rgbeffects file
				if (Fyle.Exists(fn))
				{
					int n = FILE_RGBEFFECTS.Length;
					// Found an xlights_rgbeffects.xml file?
					if (fn.Substring(fn.Length - n, n) == FILE_RGBEFFECTS)
					{
						//? Do we need to add a trailing backslash?
						showDir = Path.GetFullPath(fn);
						txtDirectoryCustom.Text = showDir;
						if (chkAutoLoad.Checked)
						{
							// If AutoLoad is enabled, go ahead and load it NOW
							btnLoad_Click(btnLoad, new EventArgs());
						}
					}
				}
			}

		}

		private void chkAutoLoad_CheckedChanged(object sender, EventArgs e)
		{
			userSettings.AutoLoad = chkAutoLoad.Checked;
			userSettings.Save();
			if (chkAutoLoad.Checked)
			{
				// Attempt to load whatever directory is selected
				//   (may fail)
				btnLoad_Click(btnLoad, new EventArgs());
			}
		}

		private void frmW_FormClosing(object sender, FormClosingEventArgs e)
		{
			Fourm.SaveView(this);
		}
	}

}
