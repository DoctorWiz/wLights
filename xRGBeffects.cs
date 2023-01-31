using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileHelper;
using XMLhelper;
using System.Windows.Forms;

namespace wLights
{
	public class xRGBeffects : xMember
	{		
		// Note: This really should be static, but can't do that AND have it use an interface
		//TODO: Consider dropping the interface and making it static
		
		
		// default layoutFile = "X:\\2023\\xLights\\Wizlights\\xlights_rgbeffects.xml";
		protected  string layoutFile = "";
		protected  int lineCount = 0;
		protected  int memberCount = 0;
		protected  int subMemberCount = 0;
		protected  int effectCount = 0;
		protected  int errorCount = 0;
		public  string showFolder = "";
		public  List<xModel> Models							= new List<xModel>();
		//public List<xEffect> Effects						= new List<xEffect>();
		public  List<xModelGroup> ModelGroups		= new List<xModelGroup>();
		public  List<xViewObject> ViewObjects = new List<xViewObject>();
		public  List<xEffectGroup> EffectGroups = new List<xEffectGroup>();
		public  List<xView> Views								= new List<xView>();
		public  List<xPalette> Palettes					= new List<xPalette>();
		
		//? Should this be nested into LayoutGroups??
		public  List<xLayout> Layouts						= new List<xLayout>();
		public  List<xLayoutGroup> LayoutGroups	= new List<xLayoutGroup>();
		
		public  List<xPerspective> Perspectives	= new List<xPerspective>();
		public  List<xSetting> xSettings					= new List<xSetting>();
		public  List<xColor> xColors							= new List<xColor>();
		public  List<xViewpoint> Viewpoints = new List<xViewpoint>();
		public  List<xController> Controllers = new List<xController>();
		public  xNetworks Networks = null; // new xNetworks();



		public xRGBeffects(string fileName) 
		{ 
			if (Fyle.Exists(fileName))
			{
				showFolder = Path.GetFullPath	(fileName);
				string fileNet = showFolder + "xlights_networks.xml";
				Networks = new xNetworks(fileNet);
				if (xNetworks.Controllers.Count > 0)
				{
					xModel lastModel = null;
					xSubModel lastSub = null;
					xEffectGroup lastEffGroup = null;
					bool doSettings = false;
					bool doColors = false;
					bool doModels = false;
					bool doSubs = false;

					layoutFile = fileName;
					StreamReader reader = new StreamReader(layoutFile);
					while (!reader.EndOfStream)
					{
						lineCount++;
						string lineIn = reader.ReadLine();
						string lineStart = lineIn.Trim();
						if (lineStart.Length > 10)
						{
							lineStart = lineStart.Substring(0, 10);
						}
						if (doSettings)
						{
							if (lineStart == "</settings")
							{
								doSettings = false;
							}
							else
							{
								xSetting stng = new xSetting(lineIn, this);
								xSettings.Add(stng);
								subMemberCount++;
							}
						}
						else if (doColors)
						{
							if (lineStart == "</colors>")
							{
								doColors = false;
							}
							else
							{
								xColor clr = new xColor(lineIn, this);
								xColors.Add(clr);
								subMemberCount++;
							}
						}
						else
						{

							int iSpc = lineStart.IndexOf(' ');
							if (iSpc > 0)
							{
								lineStart = lineStart.Substring(0, iSpc);
							}
							if (lineStart == "<model")
							{
								doModels = true;
								doSubs = false;
								lastModel = new xModel(lineIn, this);
								Models.Add(lastModel);
								memberCount++;
							}
							else if (lineStart == "<Controll") // ControllerConnection
							{
								if (doModels)
								{ lastModel.controllerConnection = lineIn; }
								else if (doSubs)
								{ lastSub.controllerConnection = lineIn; }

							}
							else if (lineStart == "<subModel")
							{
								doModels = false;
								doSubs = true;
								lastSub = new xSubModel(lineIn, lastModel);
								lastModel.mySubModels.Add(lastSub);
								subMemberCount++;
							}
							else if (lineStart == "<view_obje")
							{
								xViewObject viewObject = new xViewObject(lineIn, this);
								ViewObjects.Add(viewObject);
								memberCount++;
							}
							else if (lineStart == "<effectGro")
							{
								lastEffGroup = new xEffectGroup(lineIn, this);
								EffectGroups.Add(lastEffGroup);
								memberCount++;
							}
							else if (lineStart == "<effect")
							{
								xEffect eff = new xEffect(lineIn, this);
								lastEffGroup.Effects.Add(eff);
								effectCount++;
							}
							else if (lineStart == "<view")
							{
								xView view = new xView(lineIn, this);
								Views.Add(view);
								memberCount++;
							}
							else if (lineStart == "<palettes")
							{
								xPalette pal = new xPalette(lineIn, this);
								Palettes.Add(pal);
								memberCount++;
							}
							else if (lineStart == "<modelGrou")
							{
								if (lineIn.Trim() != "<modelGroups>")
								{
									xModelGroup modgr = new xModelGroup(lineIn, this);
									ModelGroups.Add(modgr);
									memberCount++;
								}
							}
							else if (lineStart == "<perspecti")
							{
								xPerspective pers = new xPerspective(lineIn, this);
								Perspectives.Add(pers);
								memberCount++;
							}
							else if (lineStart == "<models>")
							{
								doModels = true;
							}
							else if (lineStart == "<settings>")
							{
								doSettings = true;
								doModels = false;
							}
							else if (lineStart == "<colors>")
							{
								doColors = true;
								doModels = false;
							}
							else if (lineStart == "<Camera")
							{
								xViewpoint vwpt = new xViewpoint(lineIn, this);
								Viewpoints.Add(vwpt);
								memberCount++;
							}
							else if (lineStart == "<DefaultCa")
							{
								xViewpoint vwpt = new xViewpoint(lineIn, this);
								Viewpoints.Add(vwpt);
								memberCount++;
							}



						} // End not in or at the end of the Settings or Color sections
					} // End not end of stream
					reader.Close();
				} // End created networks class and successfully added some controllers
			} // End file exists
		// End xRGBeffects constructor
		}

		public int SaveFile(string newFileName, int order)
		{
			int err = 0;
			string lineOut = "";

			StreamWriter writer = new StreamWriter(newFileName);
			lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			writer.WriteLine(lineOut);
			lineOut = "<xrgb>";
			writer.WriteLine(lineOut);
			
			// Models First
			lineOut = "  <models>";
			writer.WriteLine(lineOut);
			for (int m=0; m< Models.Count; m++)
			{
				xModel model = Models[m];
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
			for (int vo=0; vo< ViewObjects.Count; vo++)
			{
				xViewObject viewObject = ViewObjects[vo];
				lineOut= viewObject.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </view_objects>";
			writer.WriteLine(lineOut);

			// Effect Groups and their Effects come next
			//TODO: Collect, Save, Output the version number
			lineOut = "  <effects version=\"0007\">";
			writer.WriteLine(lineOut);
			for (int eg=0; eg< EffectGroups.Count; eg++)
			{
				xEffectGroup effectGroup = EffectGroups[eg];
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
			for (int v=0; v< Views.Count; v++)
			{
				xView view = Views[v];
				lineOut= view.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </views>";
			writer.WriteLine(lineOut);

			// Palettes next (if any)
			if (Palettes.Count < 1)
			{
				lineOut = "  <palettes/>";
				writer.WriteLine(lineOut);
			}
			else
			{
				lineOut = "  <palettes>";
				writer.WriteLine(lineOut);
				for (int p = 0; p < Palettes.Count; p++)
				{
					xPalette palette = Palettes[p];
					lineOut= palette.xmlInfo;
					writer.WriteLine(lineOut);
				}
				lineOut = "  </palletes>";
				writer.WriteLine(lineOut);
			}

			// Now come the Model Groups, which can be recursive


			// LayoutGroups and Layouts next (if any)
			if (LayoutGroups.Count < 1)
			{
				lineOut = "  <layoutGroups/>";
				writer.WriteLine(lineOut);
			}
			else
			{
				lineOut = "  <layoutGroups>";
				writer.WriteLine(lineOut);
				for (int lg = 0; lg < LayoutGroups.Count; lg++)
				{
					xLayoutGroup layoutGroup = LayoutGroups[lg];
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
			for (int p = 0; p < Perspectives.Count; p++)
			{
				xPerspective perspective = Perspectives[p];
				lineOut = perspective.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </perspectives>";
			writer.WriteLine(lineOut);

			// Now Settings
			lineOut = "  <settings>";
			writer.WriteLine(lineOut);
			for (int s = 0; s < xSettings.Count; s++)
			{
				xSetting sett = xSettings[s];
				lineOut = sett.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </settings>";
			writer.WriteLine(lineOut);

			// Followed by Colors
			lineOut = "  <colors>";
			writer.WriteLine(lineOut);
			for (int c = 0; c < xColors.Count; c++)
			{
				xColor clr = xColors[c];
				lineOut = clr.xmlInfo;
				writer.WriteLine(lineOut);
			}
			lineOut = "  </colors>";
			writer.WriteLine(lineOut);

			// Followed by Viewpoints (not to be confused with Views or View Objects)
			lineOut = "  <Viewpoints>";
			writer.WriteLine(lineOut);
			for (int vp = 0; vp < Viewpoints.Count; vp++)
			{
				xViewpoint viewpoint = Viewpoints[vp];
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
		public void Clear()
		{
			layoutFile = "";
			lineCount = 0;
			memberCount = 0;
			subMemberCount = 0;
			errorCount = 0;
			Models = new List<xModel>();
			//Effects = new List<xEffect>();
			ModelGroups = new List<xModelGroup>();
			EffectGroups = new List<xEffectGroup>();
			Viewpoints = new List<xViewpoint>();
			Palettes = new List<xPalette>();
			LayoutGroups = new List<xLayoutGroup>();
			Views = new List<xView>();
			Perspectives = new List<xPerspective>();
			xSettings = new List<xSetting>();
			xColors = new List<xColor>();
			ViewObjects = new List<xViewObject>();
		}


		public override string Name
		{ get { return "rgb_effects"; } }

		public override string ToString()
		{
			return "rgb_effects";
		}


		public string LayoutFile
		{	get	{	return layoutFile;	}	}
		public int LineCount
		{	get { return lineCount; } 	}
		public int MemberCount
		{ get { return memberCount; } }
		public int SubMemberCount
		{ get { return subMemberCount; } }
		public int EffectCount
		{ get { return subMemberCount; } }
		public int ErrorCount
		{ get { return errorCount; } }

		public xModel FindModel(string modelName)
		{
			xModel ret = null;
			for (int i = 0; i < Models.Count; i++)
			{
				if (Models[i].Name == modelName)
				{ ret = Models[i];
					break;
				}
			}
			return ret;
		}

		public xModelGroup FindModelGroup(string groupName)
		{
			xModelGroup ret = null;
			for (int i = 0; i < ModelGroups.Count; i++)
			{
				if (ModelGroups[i].Name == groupName)
				{
					ret = ModelGroups[i];
					break;
				}
			}
			return ret;
		}

		public xMember FindMember(string memberName)
		{
			xMember ret = FindModel(memberName);
			if (ret == null)
			{
				ret = FindModelGroup(memberName);
			}

			return ret;
		}

		// end xRGBeffects class
	}
	// End namespace
}
