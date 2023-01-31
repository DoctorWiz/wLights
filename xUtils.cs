using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Text;
using System.Drawing;

using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
//using System.DirectoryServices;
using FileHelper;

//using FuzzORama;

namespace xLights22
{
	public class xAdmin
	{
		public const int UNDEFINED = -1;

		public const string LOG_Error = "Error";
		public const string LOG_Info = "SeqInfo";
		public const string LOG_Debug = "Debug";
		private const string FORMAT_DATETIME = "MM/dd/yyyy hh:mm:ss tt";
		private const string FORMAT_FILESIZE = "###,###,###,###,##0";

		public static int nodeIndex = UNDEFINED;

		public const string FIELDname = " name";
		public const string FIELDtype = " type";

		public const string FILE_SEQ = "All Sequences *.xsq|*.xsq";
		public const string FILE_ALL = "All Files *.*|*.*";
		//public const string EXT_CHMAP = "ChMap";
		public const string EXT_XML = "xml";
		public const string EXT_XSQ = "xsq";
		public const string XML_HEADER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";


		public const string SPC = " ";
		public const string LEVEL0 = "";
		// USING SPACES
		//public const string LEVEL1 = "  ";
		//public const string LEVEL2 = "    ";
		//public const string LEVEL3 = "      ";
		//public const string LEVEL4 = "        ";
		//public const string LEVEL5 = "          ";
		// USING TABS
		public const string LEVEL1 = "\t";
		public const string LEVEL2 = "\t\t";
		public const string LEVEL3 = "\t\t\t";
		public const string LEVEL4 = "\t\t\t\t";
		public const string LEVEL5 = "\t\t\t\t\t";
		public const string CRLF = "\r\n";
		public const string PLURAL = "s";
		public const string FIELDEQ = "=\"";
		public const string ENDQT = "\"";
		public const string STFLD = "<";
		public const string ENDFLD = "/>";
		public const string FINFLD = ">";
		public const string STTBL = "<";
		public const string FINTBL = "</";
		public const string ENDTBL = ">";

		public const string COMMA = ",";
		public const string SLASH = "/";
		public const char DELIM1 = '⬖';
		public const char DELIM2 = '⬘';
		public const char DELIM3 = '⬗';
		public const char DELIM4 = '⬙';
		private const char DELIM_Map = (char)164;  // ¤
		private const char DELIM_SID = (char)177;  // ±
		private const char DELIM_Name = (char)167;  // §
		private const char DELIM_X = (char)182;  // ¶
		private const string ROOT = "C:\\";
		private const string REGKEYx = "HKEY_CURRENT_USER\\SOFTWARE\\xLights";


		public struct RGB
		{
			private byte _r;
			private byte _g;
			private byte _b;

			public RGB(byte r, byte g, byte b)
			{
				this._r = r;
				this._g = g;
				this._b = b;
			}

			public byte R
			{
				get { return this._r; }
				set { this._r = value; }
			}

			public byte G
			{
				get { return this._g; }
				set { this._g = value; }
			}

			public byte B
			{
				get { return this._b; }
				set { this._b = value; }
			}

			public bool Equals(RGB rgb)
			{
				return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
			}
		}

		public struct HSV
		{
			private double _h;
			private double _s;
			private double _v;

			public HSV(double h, double s, double v)
			{
				this._h = h;
				this._s = s;
				this._v = v;
			}

			public double H
			{
				get { return this._h; }
				set { this._h = value; }
			}

			public double S
			{
				get { return this._s; }
				set { this._s = value; }
			}

			public double V
			{
				get { return this._v; }
				set { this._v = value; }
			}

			public bool Equals(HSV hsv)
			{
				return (this.H == hsv.H) && (this.S == hsv.S) && (this.V == hsv.V);
			}
		}

		public static Int32 HSVToRGB(HSV hsv)
		{
			double r = 0, g = 0, b = 0;

			if (hsv.S == 0)
			{
				r = hsv.V;
				g = hsv.V;
				b = hsv.V;
			}
			else
			{
				int i;
				double f, p, q, t;

				if (hsv.H == 360)
					hsv.H = 0;
				else
					hsv.H = hsv.H / 60;

				i = (int)Math.Truncate(hsv.H);
				f = hsv.H - i;

				p = hsv.V * (1.0 - hsv.S);
				q = hsv.V * (1.0 - (hsv.S * f));
				t = hsv.V * (1.0 - (hsv.S * (1.0 - f)));

				switch (i)
				{
					case 0:
						r = hsv.V;
						g = t;
						b = p;
						break;

					case 1:
						r = q;
						g = hsv.V;
						b = p;
						break;

					case 2:
						r = p;
						g = hsv.V;
						b = t;
						break;

					case 3:
						r = p;
						g = q;
						b = hsv.V;
						break;

					case 4:
						r = t;
						g = p;
						b = hsv.V;
						break;

					default:
						r = hsv.V;
						g = p;
						b = q;
						break;
				}

			}

			RGB x = new RGB((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
			Int32 ret = x.R * 0x10000 + x.G * 0x100 + x.B;
			return ret;
		}

		public static int ColorIcon(ImageList icons, Int32 colorVal)
		{
			int ret = -1;
			string tempID = colorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			ret = icons.Images.IndexOfKey(colorID);
			if (ret < 0)
			{
				// Convert rearranged hex value a real color
				Color theColor = System.Drawing.ColorTranslator.FromHtml("#" + colorID);
				// Create a temporary working bitmap
				Bitmap bmp = new Bitmap(16, 16);
				// get the graphics handle from it
				Graphics gr = Graphics.FromImage(bmp);
				// A colored solid brush to fill the middle
				SolidBrush b = new SolidBrush(theColor);
				// define a rectangle for the middle
				Rectangle r = new Rectangle(2, 2, 12, 12);
				// Fill the middle rectangle with color
				gr.FillRectangle(b, r);
				// Draw a 3D button around it
				Pen p = new Pen(Color.Black);
				gr.DrawLine(p, 1, 15, 15, 15);
				gr.DrawLine(p, 15, 1, 15, 14);
				p = new Pen(Color.DarkGray);
				gr.DrawLine(p, 2, 14, 14, 14);
				gr.DrawLine(p, 14, 2, 14, 13);
				p = new Pen(Color.White);
				gr.DrawLine(p, 0, 0, 15, 0);
				gr.DrawLine(p, 0, 1, 0, 15);
				p = new Pen(Color.LightGray);
				gr.DrawLine(p, 1, 1, 14, 1);
				gr.DrawLine(p, 1, 2, 1, 14);

				// Add it to the image list, using it's hex color code as the key
				icons.Images.Add(colorID, bmp);
				// get it's numeric index
				ret = icons.Images.Count - 1;
			}
			// Return the numeric index of the new image
			return ret;
		}


		public static void WriteLogEntry(string message, string logType, string applicationName)
		{
			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string mySubDir = "\\wLights\\";
			if (applicationName.Length > 2)
			{
				applicationName.Replace("-", "");
				mySubDir += applicationName + "\\";
			}
			string file = appDataDir + mySubDir;
			if (!Directory.Exists(file)) Directory.CreateDirectory(file);
			file += logType + ".log";
			//string dt = DateTime.Now.ToString("yyyy-MM-dd ")
			string dt = DateTime.Now.ToString("s") + "=";
			string msgLine = dt + message;
			try
			{
				StreamWriter writer = new StreamWriter(file, append: true);
				writer.WriteLine(msgLine);
				writer.Flush();
				writer.Close();
			}
			catch (System.IO.IOException ex)
			{
				string errMsg = "An error has occurred in this application!\r\n";
				errMsg += "Another error has occurred while trying to write the details of the first error to a log file!\r\n\r\n";
				errMsg += "The first error was: " + message + "\r\n";
				errMsg += "The second error was: " + ex.ToString();
				errMsg += "The log file is: " + file;
				DialogResult dr = MessageBox.Show(errMsg, "Errors!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
			}
			finally
			{
			}

		} // end write log entry


		public static Color ColorHTMLtoNet(string HTMLColorCode)
		{
			return System.Drawing.ColorTranslator.FromHtml(HTMLColorCode);
		}


		public static void ClearOutputWindow()
		{
			if (!Debugger.IsAttached)
			{
				return;
			}

			/*
			//Application.DoEvents();  // This is for Windows.Forms.
			// This delay to get it to work. Unsure why. See http://stackoverflow.com/questions/2391473/can-the-visual-studio-debug-output-window-be-programatically-cleared
			Thread.Sleep(1000);
			// In VS2008 use EnvDTE80.DTE2
			EnvDTE.DTE ide = (EnvDTE.DTE)Marshal.GetActiveObject("VisualStudio.DTE.14.0");
			if (ide != null)
			{
				ide.ExecuteCommand("Edit.ClearOutputWindow", "");
				Marshal.ReleaseComObject(ide);
			}
			*/
		}

		public static string GetAppTempFolder()
		{

			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string mySubDir = "\\xUtils\\";
			string tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			mySubDir += Application.ProductName + "\\";
			mySubDir = mySubDir.Replace("-", "");
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			return tempPath;
		}

		public static string FormatSongTime(int milliseconds)
		{
			string timeOut = "";

			int totsecs = (int)(milliseconds / 1000);
			int millis = milliseconds % 1000;
			int min = (int)(totsecs / 60);
			int secs = totsecs % 60;

			if (min > 0)
			{
				timeOut = min.ToString() + ":";
				timeOut += secs.ToString("00");
			}
			else
			{
				timeOut = secs.ToString();
			}
			timeOut += "." + millis.ToString("00");

			return timeOut;
		}

		public static int DecodeTime(string theTime)
		{
			// format mm:ss.cs
			//! Redundant?  Same as TimeToMilliseconds (below)??
			int msOut = UNDEFINED;
			int msTmp = UNDEFINED;

			// Split time by :
			string[] tmpM = theTime.Split(':');
			// Not more than 1 : ?
			if (tmpM.Length < 3)
			{
				// has a : ?
				if (tmpM.Length == 2)
				{
					// first part is minutes
					int min = 0;
					// try to parse minutes from first part of string
					int.TryParse(tmpM[0], out min);
					// each minute is 60000 milliseconds
					msTmp = min * 60000;
					// place second part of split into first part for next step of decoding
					tmpM[0] = tmpM[1];
				}
				// split seconds by . ?
				string[] tmpS = tmpM[0].Split('.');
				// not more than 1 . ?
				if (tmpS.Length < 3)
				{
					// has a . ?
					if (tmpS.Length == 2)
					{
						// next part is seconds
						int sec = 0;
						// try to parse seconds from first part of remaining string
						int.TryParse(tmpS[0], out sec);
						// each second is 1000 milliseconds (duh!)
						msTmp += (sec * 1000);
						// no more than 2 decimal places allowed
						if (tmpS[1].Length > 2)
						{
							tmpS[1] = tmpS[1].Substring(0, 2);
						}
						// place second part into first part for next step of decoding
						tmpS[0] = tmpS[1];
					}
					int cs = 0;
					int.TryParse(tmpS[0], out cs);
					msTmp += cs;
					msOut = msTmp;
				}
			}

			return msOut;
		}

		public static int FastIndexOf(string source, string pattern)
		{
			if (pattern == null) throw new ArgumentNullException();
			if (pattern.Length == 0) return 0;
			if (pattern.Length == 1) return source.IndexOf(pattern[0]);
			bool found;
			int limit = source.Length - pattern.Length + 1;
			if (limit < 1) return -1;
			// Store the first 2 characters of "pattern"
			char c0 = pattern[0];
			char c1 = pattern[1];
			// Find the first occurrence of the first character
			int first = source.IndexOf(c0, 0, limit);
			while (first != -1)
			{
				// Check if the following character is the same like
				// the 2nd character of "pattern"
				if (source[first + 1] != c1)
				{
					first = source.IndexOf(c0, ++first, limit - first);
					continue;
				}
				// Check the rest of "pattern" (starting with the 3rd character)
				found = true;
				for (int j = 2; j < pattern.Length; j++)
					if (source[first + j] != pattern[j])
					{
						found = false;
						break;
					}
				// If the whole word was found, return its index, otherwise try again
				if (found) return first;
				first = source.IndexOf(c0, ++first, limit - first);
			}
			return -1;
		}

		public static string Time_MillisecondsToMinutes(int milliseconds)
		{
			int mm = (int)(milliseconds / 60000);
			int ss = (int)((milliseconds - mm * 60000) / 1000);
			int ms = (int)(milliseconds - mm * 60000 - ss * 1000);
			string ret = mm.ToString("0") + ":" + ss.ToString("00") + "." + ms.ToString("00");
			return ret;
		}

		public static int Time_MinutesToMilliseconds(string timeInMinutes)
		{
			// Time string must be formated as mm:ss.cs
			// Where mm is minutes.  Must be specified, even if zero.
			// Where ss is seconds 0-59.
			// Where cs is milliseconds 0-99.  Must be specified, even if zero.
			// Time string must contain one colon (:) and one period (.)
			// Maximum of 60 minutes  (Anything longer can result in unmanageable sequences)
			string newTime = timeInMinutes.Trim();
			int ret = UNDEFINED;
			int posColon = newTime.IndexOf(':');
			if ((posColon > 0) && (posColon < 3))
			{
				int posc2 = newTime.IndexOf(':', posColon + 1);
				if (posc2 < 0)
				{
					string min = newTime.Substring(0, posColon);
					string rest = newTime.Substring(posColon + 1);
					int posPer = rest.IndexOf('.');
					if ((posPer == 2))
					{
						int posp2 = rest.IndexOf('.', posPer + 1);
						if (posp2 < 0)
						{
							string sec = rest.Substring(0, posPer);
							string cs = rest.Substring(posPer + 1);
							int mn = xAdmin.UNDEFINED;
							int.TryParse(min, out mn);
							if ((mn >= 0) && (mn < 61))
							{
								int sc = xAdmin.UNDEFINED;
								int.TryParse(sec, out sc);
								if ((sc >= 0) && (sc < 60))
								{
									int c = xAdmin.UNDEFINED;
									int.TryParse(cs, out c);
									if ((c >= 0) && (c < 1000))
									{
										ret = mn * 60000 + sc * 1000 + c;
									}
								}
							}
						}
					}
				}
			}

			return ret;
		}

		public static string SequenceEditor
		{
			get
			{
				string exe = "";
				string root = ROOT;
				try
				{
					string ky = "HKEY_CLASSES_ROOT\\xLights\\shell\\open";
					exe = (string)Registry.GetValue(ky, "command", root);
					exe = exe.Replace(" \"%1\"", "");
					if (exe == null)
					{
						exe = "C:\\Program Files\\xLights\\xLights.exe";
					}
					if (exe.Length < 10)
					{
						exe = "C:\\Program Files\\xLights\\xLights.exe";
					}
					bool valid = Fyle.Exists(exe);
					if (!valid)
					{
						exe = "";
					}
				}
				catch
				{
					exe = "";
				}
				return exe;

			}
		}

		public static string ShowDirectory
		{
			// AKA Sequences Folder
			get
			{
				string fldr = "";
				string root = ROOT;
				string userDocs = Fyle.DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(REGKEYx, "LastDir", root);
					if (fldr.Length > 6)
					{
						if (!Directory.Exists(fldr))
						{
							Directory.CreateDirectory(fldr);
						}
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get ShowDirectory
		}

		public static int ParseMilliseconds(string secondsValue)
		{
			double dsec = xAdmin.UNDEFINED;
			double.TryParse(secondsValue, out dsec);
			double msec = Math.Round(dsec * 1000D);
			int ms = (int)msec;
			return ms;
		}


		public static int ClearTempDir(string tempPath)
		{
			int errs = 0;

			try
			{
				List<string> fls = new List<string>(Directory.EnumerateFiles(tempPath));

				foreach (string fil in fls)
				{
					try
					{
						System.IO.File.Delete(fil);
					}
					catch
					{
						errs++;
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				errs = 99999;
			}
			catch (PathTooLongException ex)
			{
				errs = 99998;
			}
			return errs;
		}

		public static string NoteUnicodeToASCII(string unicodeNoteName)
		{
			string tmp = unicodeNoteName.Replace('♯', '#');
			string ret = tmp.Replace('♭', 'b');
			return ret;
		}

		public static string NoteASCIIToUnicode(string asciiNoteName)
		{
			// Use with caution as all lower-case b's will be replaced with ♭
			//   (All other lower-case letters will be unaffected, as will upper-case letters ihcluing B's)
			string tmp = asciiNoteName.Replace('#', '♯');
			string ret = tmp.Replace('b', '♭');
			return ret;
		}



	} // end class xAdmin
} // end namespace xAdmin
