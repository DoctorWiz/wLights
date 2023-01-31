using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	// Things like grid lines, rulers, meshes, and the 3D house
	// Note: Unrelated to Views and/or Viewpoints.
	public class xViewObject : xMember
	{
		public xViewObject(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.ViewObject; } }

	}
}
