using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLhelper;

namespace wLights
{
	//! THIS CODE FILE CONTAINS EFFECTS AND EFFECT GROUPS


	// Saved effects in the user's saved effect library
	public class xEffect : xMember
	// Note: These are the saved effects library, not the effects applied to models in a sequence
	{

		public xEffect(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Effect; } }

	}

	// Groups of Effects in the User's saved effect library
	public class xEffectGroup : xMember
	// Note: These are the saved effects library, not the effects applied to models in a sequence
	{
		public List<xEffect> Effects = new List<xEffect>();

		public xEffectGroup(string xmlData, xMember parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myParent = parent;
		}


		public override xMemberType MemberType
		{ get { return xMemberType.EffectGroup; } }

	}
}
