using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	// In the sequencer tab, which batch of models and groups are displayed,
	// As selected from the 'View' drop down box on left above models and groups.
	// Note: Unrelated to ViewObjects and/or Viewpoints.
	public class xView : xMember
	{
		public List<xMember> Members = new List<xMember>();
		public xView(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
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
		{ get { return xMemberType.View; } }


	}
}
