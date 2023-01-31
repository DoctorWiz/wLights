using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	// Program Option settings
	public class xSetting : xMember
	{
		protected string xValue = "";
		public xSetting(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Setting; } }

		public string Value
		{
			get
			{
				if (xValue == "")
				{
					xValue = XMLhelp.getKeyWord(myXMLdata, "value");
				}
				return xValue;
			}
		}
	}
}
