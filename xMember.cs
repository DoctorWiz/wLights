using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace wLights
{
	//! THIS CODE FILE CONTAINS THE MEMBER INTERFACE AND THE MEMBER BASE CLASS
	//! ALONG WITH RELATED ENUMS

	public enum xMemberType
	{
		None,
		Undefined,
		Model,
		SubModel,
		ModelGroup,
		Layout,
		LayoutGroup,
		Effect,
		EffectGroup,
		Palette,
		Perspective,
		View,
		Viewpoint,
		ViewObject,
		Setting,
		Color,
		Controller
	}

	public enum xSortMode
	{
		Name, Index, ID, Group, StartChannel, ModelType, Color, LeftToRight, BottomToTop, FrontToBack, ChannelCount, BigToSmall
	}

	public partial interface iMember : IComparable<iMember>
	{
		string Name
		{ get; }
		string xmlInfo
		{ get; }
		iMember Parent
		{ get; }
		void SetParent(iMember newParent);
		int Index
		{ get; }
		void SetIndex(int index);
		int ID
		{ get; set; }
		void SetID(int newID);
		CheckState SelectedState
		{ get; set; }
		bool Dirty
		{ get; }
		void MakeDirty(bool dirtyState = true);
		//LOR4MemberType MemberType
		//{ get; }
		string SortKey
		{ get; }
		int CompareTo(iMember otherMember);
		string LineOut();
		string ToString();
		bool Written
		{ get; set; }
		xMember Clone();
		xMember Clone(string newName);
		object Tag
		{ get; set; }
		string Comment
		{ get; set; }
		xSortMode SortModePrimary
		{ get; set; }
		xSortMode SortModeSecondary
		{ get; set; }
		xMemberType MemberType
		{ get; }
	}


	public abstract partial class xMember : iMember, IComparable<iMember>
	{
		protected string myName = "";
		protected string myXMLdata = "";
		protected int myIndex = -1; // xTools.Undefined
		protected int myID = -1; // xTools.Undefined
		protected iMember myParent = null;
		//protected bool imSelected = false;
		protected CheckState myCheckState = CheckState.Unchecked;
		protected bool isDirty = false;
		protected bool isExactMatch = false;
		protected object myTag = null; // General Purpose Object
																	 // Note: Mapped to is used by Map-O-Rama and possibly by other utils in the future
																	 // Only holds a single member so only works for destination to source mapping
																	 // source-to-destination mapping may include multiple members, and is stored in a List<iLOR4Member> stored in the Tag property
		protected string myComment = ""; // Not really a comment somuch as a general purpose temporary string.
		protected xSortMode mySortModePrimary = xSortMode.Name;
		protected xSortMode mySortModeSecondary = xSortMode.ID;
		protected bool writtenFlag = false;

		internal xMember()
		// Necessary to be the base member of other members
		// Should never be called directly!
		{ }

		public virtual string Name
		{ get { return myName; } }
		// Note: Name property does not have a 'set'.  Uses ChangeName() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public string xmlInfo
		{ get { return myXMLdata; } }
		public virtual iMember Parent
		{ get { return myParent; } }
		// Note: Parent property does not have a 'set'.  Uses SetParent() instead-- because this property is
		// usually only set once and not usually changed thereafter
		public void SetParent(iMember newParent)
		{
			myParent = newParent;
		}
		public int Index
		{ get { return myIndex; } }
		// Note: Index property does not have a 'set'.  Uses SetIndex() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public void SetIndex(int newIndex)
		{
			myIndex = newIndex;
			//MakeDirty(true);
		}

		public int ID
		// Note: SavedIndex property does not have a 'set'.  Uses SetSavedIndex() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		{ get { return myID; } set { myID = value; } }

		public void SetID(int newID)
		{
			myID = newID;
			//MakeDirty(true);
		}

		public virtual CheckState SelectedState
			// Overridden in Group type members
		{ get { return myCheckState; } set { myCheckState = value; } }

		public bool Dirty
		{ get { return isDirty; } }
		// Note: Dirty flag is read-only.  Uses MakeDirty() instead-- to set it
		// and optionally to clear it.
		public virtual void MakeDirty(bool dirtyState = true)
		{
			isDirty = dirtyState;
			if (dirtyState)
			{
				if (myParent != null)
				{
					if (!myParent.Dirty)
					{
						myParent.MakeDirty(true);
					}
				}
			}
		}

		// This property is included here to be part of the base interface
		// But every subclass should override it and return their own value
		public string SortKey
		{
			get
			{
				string delim = "■";
				string key = "";
				switch(mySortModePrimary)
				{
					case xSortMode.Name:
						key = myName; break;
					case xSortMode.ID:
						key = myID.ToString("00000"); break;
					case xSortMode.Index:
						key = myIndex.ToString("00000"); break;
				}
				switch(mySortModeSecondary)
				{
					case xSortMode.Name:
						key += delim + myName; break;
					case xSortMode.ID:
						key += delim + myID.ToString("00000"); break;
					case xSortMode.Index:
						key += delim + myIndex.ToString("00000"); break;
				}
				return key;
			}
		}

		public virtual int CompareTo(iMember otherMember)
		{
			int result = 0;
			//if (parentSequence.Members.sortMode == LOR4Membership.SORTbySavedIndex)
			if (otherMember == null)
			{
				result = 1;
				//string msg = "Why are we comparing " + this.Name + " to null?";
				//msg+= "\r\nClick Cancel, step thru code, check call stack!";
				//Fyle.BUG(msg);
			}
			else
			{
				result = SortKey.CompareTo(otherMember.SortKey);
			}
			return result;
		}

		// This function is included here to be part of the base interface
		// But every subclass should override it and return their own value
		public virtual string LineOut()
		{
			return "";
		}

		// The 'Name' property is the default return value for ToString()
		// But subclasses may override it, for sorting, or other reasons
		public override string ToString()
		{
			return myName;
		}

		// This function is included here to be a skeleton for the base interface
		// But every subclass should override it and return their own value
		public bool Written
		{
			get { return writtenFlag; }
			set { writtenFlag = value; }

		}

		public virtual xMember Clone()
		{
			return Clone(myName);
		}

		public virtual xMember Clone(string newName)
		{
			xMember mbr = null;
			mbr.SetIndex(myIndex);
			mbr.SetID(myID);
			mbr.SelectedState = myCheckState;
			mbr.Tag = myTag;
			mbr.MakeDirty(isDirty);
			mbr.SetParent(myParent);
			mbr.Comment = myComment;
			//TODO: More properties...
			return mbr;
		}

		public object Tag
		{ get { return myTag; } set { myTag = value; } }


		public string Comment
		{ get { return myComment; } set { myComment = value; } }

		public xSortMode SortModePrimary
		{ get { return mySortModePrimary; } set { mySortModePrimary = value; } }

		public xSortMode SortModeSecondary
		{ get { return mySortModeSecondary; } set { mySortModeSecondary = value; } }

		public virtual xMemberType MemberType
			// Overridden in individual members
		{ get { return xMemberType.Undefined; }		}
	}
}
