using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	// Which child dialogs are displayed (such as Model Preview, House Preview, Effect Settings, Layer Settings...)
	public class xPerspective : xMember
	{
	public xPerspective(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Perspective; } }

	}
}
