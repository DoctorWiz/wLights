using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XMLhelper;

namespace wLights
{
	// Selected colors for different elements of the UI (like grid lines, timings, waveform, etc...)
	public class xColor : xMember
	{
		string myRGBvalues = "";
		Color myColor = Color.Black;
		public xColor(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Color; } }
		public string RGBvalues
		{
			get
			{
				if (myRGBvalues == "")
				{
					string v = myXMLdata.Trim();
					int i = v.IndexOf(' ');
					myRGBvalues = v.Substring(i);
					myRGBvalues = v.Substring(0, v.Length - 2);
					int r = XMLhelp.getKeyValue(myXMLdata, "Red");
					int g = XMLhelp.getKeyValue(myXMLdata, "Green");
					int b = XMLhelp.getKeyValue(myXMLdata, "Blue");
					myColor = Color.FromArgb(r, g, b);
				}
				return myRGBvalues;
			}
		}

		public Color Color
		{
			get
			{
				if (myColor == Color.Black) 
				{
					string v = myXMLdata.Trim();
					int i = v.IndexOf(' ');
					myRGBvalues = v.Substring(i);
					myRGBvalues = v.Substring(0, v.Length - 2);
					int r = XMLhelp.getKeyValue(myXMLdata, "Red");
					int g = XMLhelp.getKeyValue(myXMLdata, "Green");
					int b = XMLhelp.getKeyValue(myXMLdata, "Blue");
					myColor = Color.FromArgb(r, g, b);
				}
				return myColor;
			}
		}


	}
}
