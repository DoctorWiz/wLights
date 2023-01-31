using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLhelper;

namespace wLights
{
	// Camera views, position, angle, zoom, etc.
	// Note: Unrelated to Views and/or ViewObjects.
	public class xViewpoint : xMember
	{
		protected string myCamera = "";
		public xViewpoint(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Viewpoint; } }

		public string Camera
		{
			get
			{
				if (myCamera == "")
				{
					string t = myXMLdata.Trim();
					int i = t.IndexOf(',');
					string cam = t.Substring(1, i);
					myCamera = cam;
				}
				return myCamera;
			}
		}

	}
}
