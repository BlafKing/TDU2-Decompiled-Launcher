using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UPnPDetection.UPnP;

public sealed class UPnPServiceAction
{
	public enum UPnPServiceActionArgumentDirection
	{
		In,
		Out
	}

	public class UPnPServiceActionArgument
	{
		private string string_0;

		private string string_1;

		private UPnPServiceActionArgumentDirection upnPServiceActionArgumentDirection_0;

		private string string_2;

		public string Name
		{
			get
			{
				return string_0;
			}
			set
			{
				string_0 = value;
			}
		}

		public string RelatedStateVariable
		{
			get
			{
				return string_1;
			}
			set
			{
				string_1 = value;
			}
		}

		public UPnPServiceActionArgumentDirection Direction
		{
			get
			{
				return upnPServiceActionArgumentDirection_0;
			}
			set
			{
				upnPServiceActionArgumentDirection_0 = value;
			}
		}

		public string Value
		{
			get
			{
				return string_2;
			}
			set
			{
				string_2 = value;
			}
		}

		public override string ToString()
		{
			return ((upnPServiceActionArgumentDirection_0 == UPnPServiceActionArgumentDirection.In) ? "In" : "Out") + " " + string_0;
		}

		public void Clean()
		{
			string_2 = "";
		}
	}

	private string string_0;

	private List<UPnPServiceActionArgument> list_0;

	public string Name
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
		}
	}

	public List<UPnPServiceActionArgument> Arguments
	{
		get
		{
			return list_0;
		}
		set
		{
			list_0 = value;
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string_0);
		if (list_0 != null)
		{
			stringBuilder.Append("(");
			foreach (UPnPServiceActionArgument item in list_0)
			{
				stringBuilder.Append(item.ToString() + ",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(")");
		}
		return stringBuilder.ToString();
	}

	public static string SerializeArgument(object a_Argument)
	{
		if (a_Argument == null)
		{
			return "";
		}
		string fullName = a_Argument.GetType().FullName;
		string text = "";
		switch (fullName)
		{
		case "System.DateTime":
		{
			DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
			return ((DateTime)a_Argument).ToString(dateTimeFormatInfo.SortableDateTimePattern);
		}
		case "System.Boolean":
			if ((bool)a_Argument)
			{
				return "1";
			}
			return "0";
		case "System.Uri":
			return ((Uri)a_Argument).AbsoluteUri;
		case "System.Byte[]":
			return UPnPBase64.Encode((byte[])a_Argument);
		default:
			return a_Argument.ToString();
		}
	}

	public void Clean()
	{
		if (list_0 == null)
		{
			return;
		}
		foreach (UPnPServiceActionArgument item in list_0)
		{
			item.Clean();
		}
	}
}
