using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using XMLhelper;
using FileHelper;

namespace wLights
{

	public class xNetworks
	{
		public static int lineCount = 0;
		public static List<xController> Controllers = new List<xController>();

		public xNetworks()
		{ }

		public xNetworks(string networksFile)
		{
			if (Fyle.Exists(networksFile))
			{
				xController lastCtlr = null;
				StreamReader reader = new StreamReader(networksFile);
				while (!reader.EndOfStream)
				{
					lineCount++;
					string lineIn = reader.ReadLine();
					string lineStart = lineIn.Trim();
					if (lineStart.Length > 10)
					{
						lineStart = lineStart.Substring(0, 10);
					}

					if (lineStart == "</Controll")
					{
					lastCtlr = new xController(lineIn, this);
					Controllers.Add(lastCtlr);
					}
					else if (lineStart == "<network")
					{
						lastCtlr.AddNetwork(lineIn);
					}
				}
				reader.Close();
			}
		}

	}
}
