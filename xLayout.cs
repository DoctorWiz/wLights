using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	public class xLayout : xMember
	// Not sure what these are, or LayoutGroups, my rgb_effects file doesn't have any
	{

		public xLayout(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Layout; } }

	}

		public class xLayoutGroup : xMember
	// Not sure what these are, or Layouts, my rgb_effects file doesn't have any
	{
		public List<xLayout> Layouts = new List<xLayout>();

		public xLayoutGroup(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}


		public override xMemberType MemberType
		{ get { return xMemberType.LayoutGroup; } }

	}
}
