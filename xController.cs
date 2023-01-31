using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xLights22;
using XMLhelper;

namespace wLights
{
	public class xController : xMember
	// For the xlights_networks.xml (not rgb_effects.xml)
	{
		private string xmlData = "";
		protected int myChannelCount = 0;
		protected int myStartChannel = 0;
		protected xNetworks myNetwork= null;
		protected List<string> networks = new List<string>();


		public xController(string xmlData, xNetworks parent)
		{
			myXMLdata = xmlData;
			myName = XMLhelp.getKeyWord(xmlData, "name");
			myNetwork = parent;
		}

		public override xMemberType MemberType
		{ get { return xMemberType.Controller; } }

		public void AddNetwork(string networkInfo)
		{ networks.Add(networkInfo); }

		public int StartChannel
		{
			get
			{
				// Already cached?
				if (myStartChannel < 1)
				{
					// No, calculate it then cache it for future queries
					myStartChannel= 1;
					// Begin a loop thru all the [other] controllers that come BEFORE this one.
					foreach (xController ctlr in xNetworks.Controllers)
					{
						// Have we reached ourself in the list?
						if (ctlr.Name == myName)
						{
							// Yes, exit the loop.  All controllers AFTER this do not effect start channel
							break;
						}
						else
						{
							// No, controller comes before this one, so add its count to the start of this one
							myStartChannel += ctlr.ChannelCount;
						}
					}

				}
				return myStartChannel;
			}
		}

		public int EndChannel
		{ get { return (StartChannel + ChannelCount - 1); } }

		public int ChannelCount
		{
			get
			{
				// Already cached?
				if (myChannelCount < 1)
				{
					// No, calculate it then cache it for future queries
					foreach (string nw in networks)
					{
						int mc = XMLhelp.getKeyValue(nw, "MaxChannels");
						myChannelCount += mc;
					}
				}
				return myChannelCount;
			}
		}

	}

}
