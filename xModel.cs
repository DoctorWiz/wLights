using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using wLights;
using XMLhelper;

namespace wLights
{
	//! THIS CODE FILE CONTAINS MODELS, SUBMODELS, AND MODEL GROUPS
	//! ALONG WITH RELATED ENUMS AND OTHER RELATED CLASSES
	public enum xModelType
	{
		None,
		Undefined,
		Arch,
		CandyCane,
		ChannelBlock,
		Circle,
		Cube,
		Custom,
		DMX,
		Image,
		Icicles,
		Matrix,
		SingleLine,
		PolyLine,
		Sphere,
		Spinner,
		Star,
		Tree,
		WindowFrame,
		Wreath,
		Download,
		Object,
		Ruler,
		GridLines,
		Mesh
	}


	public class Point3D
	{
		public double X = 0D;
		public double Y = 0D;
		public double Z = 0D;

		public Point3D(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}

	public class PointList
	{
		public List<Point3D> Points = new List<Point3D>();
		//public List<Point3D> Points = new List<Point3D>();
	}


	public class xModel : xMember
	{ 	
		// These values are cached, and not retrieved from the XML data until (if) needed
		protected int myStartChannel = -1;
		protected int myEndChannel = -1;
		protected int myChannelCount = 0;
		protected int myNodeCount = 0;
		protected xModelType myModelType = xModelType.Undefined;
		protected string myModelTypeName = "";
		protected double myX0 = 0;
		protected double myY0 = 0;
		protected double myZ0 = 0;
		protected double myX1 = 0;
		protected double myY1 = 0;
		protected double myZ1 = 0;
		protected Color myColor = Color.Black;
		protected string myStringType = "";
		public List<xSubModel> mySubModels = new List<xSubModel>();
		public string controllerConnection = "";


		public xModel(string xmlData, xMember parent)
		{
			myXMLdata = xmlData ;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent= parent ;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Model; } }

		public int StartChannel
		{	get	{
				if (myStartChannel == -1)
				{
					// If not already cached, fetch it
					myStartChannel = XMLhelp.getKeyValue(myXMLdata, "StartChannel");
				}
				return myStartChannel;	}
		}
		public int EndChannel
		{
			get
			{
				if (myStartChannel == -1)
				{
					myStartChannel = XMLhelp.getKeyValue(myXMLdata, "StartChannel");
				}
				if (myChannelCount == -1)
				{
					myChannelCount = XMLhelp.getKeyValue(myXMLdata, "ChannelCount");
				}
				return (myStartChannel + myChannelCount);
			}
		}
		public int ChannelCount
		{
			get
			{
				if (myChannelCount == -1)
				{
					myChannelCount = XMLhelp.getKeyValue(myXMLdata, "ChannelCount");
				}
				return myChannelCount;
			}
		}



		public double X0
		{	get	{
				// If not already cached, fetch it
				if (myX0 == 0)	{	myX0 = XMLhelp.getKeyFloat(myXMLdata, "WorldPosX"); }
				return myX0;	}
		}
		public double Y0
		{	get	{
				if (myY0 == 0) { myY0 = XMLhelp.getKeyFloat(myXMLdata, "WorldPosY"); }
				return myY0;	}
		}
		public double Z0
		{	get	{
				if (myZ0 == 0) { myZ0 = XMLhelp.getKeyFloat(myXMLdata, "WorldPosZ"); }
				return myZ0;	}
		}
		public double X1
		{	get	{
				if (myX1 == 0) { myX1 = XMLhelp.getKeyFloat(myXMLdata, "X2"); }
				return myX1;	}
		}
		public double Y1
		{	get	{
				if (myY1 == 0) { myY1 = XMLhelp.getKeyFloat(myXMLdata, "Y2"); }
				return myY1;	}
		}
		public double Z1
		{ get {
				if (myZ1 == 0) { myZ1 = XMLhelp.getKeyFloat(myXMLdata, "Z2"); }
				return myZ1; }
		}

		public xModelType ModelType
		{
			get {
				if (myModelType == xModelType.Undefined)
				{ 
					myModelTypeName = XMLhelp.getKeyWord(myXMLdata, "DisplayAs");
					myModelType = GetModelType(myModelTypeName);
				}
				return myModelType;	
			}
		}

		public string ModelTypeName
		{
			get
			{
				if (myModelTypeName == "") 
				{
					myModelTypeName = XMLhelp.getKeyWord(myXMLdata, "DisplayAs");
					myModelType = GetModelType(myModelTypeName);
				}
				return myModelTypeName;
			}
		}

		public static xModelType GetModelType(string displayAs)
		{
			xModelType ret = wLights.xModelType.None;
			if (displayAs == "Single Line")				{ ret = xModelType.SingleLine; }
			else if(displayAs == "Image")					{ ret = xModelType.Image; }
			else if(displayAs == "Tree 360")			{ ret = xModelType.Tree; }
			else if(displayAs == "Custom")				{ ret = xModelType.Custom; }
			else if(displayAs == "Circle")				{ ret = xModelType.Circle; }
			else if(displayAs == "Poly Line")			{ ret = xModelType.PolyLine; }
			else if(displayAs == "Tree 360")			{ ret = xModelType.Tree; }
			else if(displayAs == "Horiz Matrix")	{ ret = xModelType.Matrix; }
			else if(displayAs.Substring(0,5) == "Tree ") { ret = xModelType.Tree; }
			else if(displayAs == "Arches")				{ ret = xModelType.Arch; }
			else if(displayAs == "Candy Canes")		{ ret = xModelType.CandyCane; }
			else if(displayAs == "Ruler")					{ ret = xModelType.Ruler; }
			else if(displayAs == "GridLines")			{ ret = xModelType.GridLines; }
			else if(displayAs == "Mesh")					{ ret = xModelType.Mesh; }
			else if(displayAs == "Channel Block") { ret = xModelType.ChannelBlock; }
			else if(displayAs == "DMX")						{ ret = xModelType.DMX; }
			else if(displayAs == "Icicles")				{ ret = xModelType.Icicles; }
			else if(displayAs == "Sphere")				{ ret = xModelType.Sphere; }
			else if(displayAs == "Spinner")				{ ret = xModelType.Spinner; }
			else if(displayAs == "Star")					{ ret = xModelType.Star; }
			else if(displayAs == "Window Frame")	{ ret = xModelType.WindowFrame; }
			else if(displayAs == "Wreath")				{ ret = xModelType.Wreath; }
			else if(displayAs == "Download")			{ ret = xModelType.Download; }

			return ret;
		}



	// end class xModel
	}

	public class xSubModel : xMember
	{
		public string controllerConnection = "";
		public xSubModel(string xmlData, xModel parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}


		public override xMemberType MemberType
		{ get { return xMemberType.SubModel; } }

	}
	public class xModelGroup : xMember
	{
		//TODO: Model Groups need to be able to contain other groups recursively
		public List<iMember> Members = new List<iMember>();

		public xModelGroup(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(myXMLdata, "Name");
			myParent = parent;

			string childList = XMLhelp.getKeyWord(myXMLdata, "models");
			string[] kids = childList.Split(',');
			for (int c = 0; c < kids.Length; c++)
			{
				string childName = kids[c].Trim();
				xRGBeffects xrgbe = (xRGBeffects)myParent;
				xMember kid = xrgbe.FindMember(childName);
				if (kid != null)
				{
					Members.Add(kid);
				}
			}
		}

		public override xMemberType MemberType
		{ get { return xMemberType.ModelGroup; } }

	}

	// end namespace wLights
}
