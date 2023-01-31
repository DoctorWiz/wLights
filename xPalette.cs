using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	// Not sure what these are-- my rgb_effects file doesn't have any.  Saved color sets?
	public class xPalette : xMember
	{
		public xPalette(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Palette; } }

	}
}
