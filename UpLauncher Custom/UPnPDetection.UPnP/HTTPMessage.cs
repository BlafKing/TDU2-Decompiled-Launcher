using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Text;

namespace UPnPDetection.UPnP;

[Serializable]
public sealed class HTTPMessage : ICloneable
{
	internal bool bool_0;

	public bool OverrideContentLength;

	private string string_0;

	private string string_1;

	private int int_0;

	private string string_2;

	private Hashtable hashtable_0;

	private byte[] byte_0;

	public string m_Version = "1.1";

	[NonSerialized]
	public object StateObject;

	public IPEndPoint LocalEndPoint;

	public IPEndPoint RemoteEndPoint;

	public byte[] BodyBuffer
	{
		get
		{
			return byte_0;
		}
		set
		{
			byte_0 = value;
		}
	}

	public string CharSet
	{
		get
		{
			string contentType = ContentType;
			DText dText = new DText();
			dText.ATTRMARK = ";";
			dText.MULTMARK = "=";
			dText[0] = contentType;
			string text = "";
			if (dText.DCOUNT() > 1)
			{
				for (int i = 1; i <= dText.DCOUNT(); i++)
				{
					if (dText[i, 1].Trim().ToUpper() == "CHARSET")
					{
						text = dText[i, 2].Trim().ToUpper();
						if (text.StartsWith("\""))
						{
							text = text.Substring(1);
						}
						if (text.EndsWith("\""))
						{
							text = text.Substring(0, text.Length - 1);
						}
						break;
					}
				}
				return text;
			}
			return "";
		}
	}

	public string ContentType
	{
		get
		{
			return GetTag("Content-Type");
		}
		set
		{
			AddTag("Content-Type", value);
		}
	}

	public string StringBuffer
	{
		get
		{
			if (CharSet == "UTF-16")
			{
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				return unicodeEncoding.GetString(byte_0);
			}
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			return uTF8Encoding.GetString(byte_0);
		}
		set
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			byte_0 = uTF8Encoding.GetBytes(value);
		}
	}

	public string StringPacket
	{
		get
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			return uTF8Encoding.GetString(RawPacket);
		}
	}

	public byte[] RawPacket => method_0();

	public string Directive
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

	public string DirectiveObj
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

	public int StatusCode
	{
		get
		{
			return int_0;
		}
		set
		{
			int_0 = value;
		}
	}

	public string StatusData
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

	public HTTPMessage()
		: this("1.1")
	{
	}

	public HTTPMessage(string a_Version)
	{
		hashtable_0 = new Hashtable();
		int_0 = -1;
		string_2 = "";
		string_0 = "";
		string_1 = "";
		byte_0 = new byte[0];
		m_Version = a_Version;
	}

	public object Clone()
	{
		return MemberwiseClone();
	}

	public void RemoveTag(string a_TagName)
	{
		try
		{
			hashtable_0.Remove(a_TagName.ToUpper());
		}
		catch (Exception)
		{
		}
	}

	public IDictionaryEnumerator GetHeaderEnumerator()
	{
		return hashtable_0.GetEnumerator();
	}

	public void AddTag(string a_TagName, string a_TagData)
	{
		hashtable_0[a_TagName.ToUpper()] = a_TagData;
	}

	public void AppendTag(string a_TagName, string a_TagData)
	{
		if (!hashtable_0.ContainsKey(a_TagName.ToUpper()))
		{
			hashtable_0[a_TagName.ToUpper()] = a_TagData;
			return;
		}
		if (hashtable_0[a_TagName.ToUpper()].GetType() == typeof(string))
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(hashtable_0[a_TagName.ToUpper()]);
			hashtable_0[a_TagName.ToUpper()] = arrayList;
		}
		((ArrayList)hashtable_0[a_TagName.ToUpper()]).Add(a_TagData);
	}

	public bool HasTag(string a_TagName)
	{
		return hashtable_0.ContainsKey(a_TagName.ToUpper());
	}

	public string GetTag(string a_TagName)
	{
		object obj = hashtable_0[a_TagName.ToUpper()];
		if (obj == null)
		{
			return "";
		}
		if (obj.GetType() == typeof(string))
		{
			return ((string)obj).Trim();
		}
		string text = "";
		foreach (string item in (ArrayList)obj)
		{
			text += item.Trim();
		}
		return text;
	}

	public static HTTPMessage ParseByteArray(byte[] a_Buffer)
	{
		return ParseByteArray(a_Buffer, 0, a_Buffer.Length);
	}

	public static HTTPMessage ParseByteArray(byte[] a_Buffer, int a_Index, int a_Count)
	{
		HTTPMessage hTTPMessage = new HTTPMessage();
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		string @string = uTF8Encoding.GetString(a_Buffer, a_Index, a_Count);
		DText dText = new DText();
		int num = @string.IndexOf("\r\n\r\n");
		if (num < 0)
		{
			return null;
		}
		@string = @string.Substring(0, num);
		dText.ATTRMARK = "\r\n";
		dText.MULTMARK = ":";
		dText[0] = @string;
		string text = dText[1];
		DText dText2 = new DText();
		dText2.ATTRMARK = " ";
		dText2.MULTMARK = "/";
		dText2[0] = text;
		if (text.ToUpper().StartsWith("HTTP/"))
		{
			hTTPMessage.int_0 = int.Parse(dText2[2]);
			int num2 = text.IndexOf(" ");
			num2 = text.IndexOf(" ", num2 + 1);
			hTTPMessage.string_2 = UnEscapeString(text.Substring(num2));
			try
			{
				hTTPMessage.m_Version = dText2[1, 2];
			}
			catch (Exception)
			{
				hTTPMessage.m_Version = "0.9";
			}
		}
		else
		{
			hTTPMessage.Directive = dText2[1];
			string text2 = text.Substring(text.LastIndexOf(" ") + 1);
			if (!text2.ToUpper().StartsWith("HTTP/"))
			{
				hTTPMessage.m_Version = "0.9";
				hTTPMessage.DirectiveObj = UnEscapeString(text2);
			}
			else
			{
				hTTPMessage.m_Version = text2.Substring(text2.IndexOf("/") + 1);
				int num3 = text.IndexOf(" ") + 1;
				hTTPMessage.DirectiveObj = UnEscapeString(text.Substring(num3, text.Length - num3 - text2.Length - 1));
			}
		}
		string text3 = "";
		string text4 = "";
		for (int i = 2; i <= dText.DCOUNT(); i++)
		{
			if (text3 != "" && dText[i, 1].StartsWith(" "))
			{
				text4 = dText[i, 1].Substring(1);
			}
			else
			{
				text3 = dText[i, 1];
				text4 = "";
				for (int j = 2; j <= dText.DCOUNT(i); j++)
				{
					text4 = ((!(text4 == "")) ? (text4 + dText.MULTMARK + dText[i, j]) : dText[i, j]);
				}
			}
			hTTPMessage.AppendTag(text3, text4);
		}
		int num4 = 0;
		if (hTTPMessage.HasTag("Content-Length"))
		{
			try
			{
				num4 = int.Parse(hTTPMessage.GetTag("Content-Length"));
			}
			catch (Exception)
			{
				num4 = -1;
			}
		}
		else
		{
			num4 = -1;
		}
		if (num4 > 0)
		{
			byte[] destinationArray = new byte[num4];
			if (num + 4 + num4 <= a_Count)
			{
				Array.Copy(a_Buffer, num + 4, destinationArray, 0, num4);
				hTTPMessage.byte_0 = destinationArray;
			}
		}
		if (num4 == -1)
		{
			byte[] destinationArray = new byte[a_Count - (num + 4)];
			Array.Copy(a_Buffer, num + 4, destinationArray, 0, destinationArray.Length);
			hTTPMessage.byte_0 = destinationArray;
		}
		if (num4 == 0)
		{
			hTTPMessage.byte_0 = new byte[0];
		}
		return hTTPMessage;
	}

	private byte[] method_0()
	{
		if (byte_0 == null)
		{
			byte_0 = new byte[0];
		}
		if (m_Version == "1.0" && string_0 == "" && int_0 == -1)
		{
			return byte_0;
		}
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		IDictionaryEnumerator enumerator = hashtable_0.GetEnumerator();
		enumerator.Reset();
		string text = ((!(string_0 != "")) ? ("HTTP/" + m_Version + " " + int_0 + " " + string_2 + "\r\n") : ((!(m_Version != "")) ? (string_0 + " " + EscapeString(string_1) + "\r\n") : (string_0 + " " + EscapeString(string_1) + " HTTP/" + m_Version + "\r\n")));
		while (enumerator.MoveNext())
		{
			if (!((string)enumerator.Key != "CONTENT-LENGTH") && !OverrideContentLength)
			{
				continue;
			}
			if (enumerator.Value.GetType() == typeof(string))
			{
				string text2 = text;
				text = text2 + (string)enumerator.Key + ": " + (string)enumerator.Value + "\r\n";
				continue;
			}
			text = text + (string)enumerator.Key + ":";
			foreach (string item in (ArrayList)enumerator.Value)
			{
				text = text + " " + item + "\r\n";
			}
		}
		if (StatusCode == -1 && !bool_0)
		{
			text = text + "Content-Length: " + byte_0.Length + "\r\n";
		}
		else if (m_Version != "1.0" && m_Version != "0.9" && m_Version != "" && !bool_0 && !OverrideContentLength)
		{
			text = text + "Content-Length: " + byte_0.Length + "\r\n";
		}
		text += "\r\n";
		byte[] array = new byte[uTF8Encoding.GetByteCount(text) + byte_0.Length];
		uTF8Encoding.GetBytes(text, 0, text.Length, array, 0);
		Array.Copy(byte_0, 0, array, array.Length - byte_0.Length, byte_0.Length);
		return array;
	}

	public static string EscapeString(string a_String)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(a_String);
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array = bytes;
		for (int i = 0; i < array.Length; i++)
		{
			byte b = array[i];
			if ((b < 63 || b > 90) && (b < 97 || b > 122) && (b < 47 || b > 57) && b != 59 && b != 47 && b != 63 && b != 58 && b != 64 && b != 61 && b != 43 && b != 36 && b != 45 && b != 95 && b != 46 && b != 42)
			{
				stringBuilder.Append("%" + b.ToString("X"));
			}
			else
			{
				stringBuilder.Append((char)b);
			}
		}
		return stringBuilder.ToString();
	}

	public static string UnEscapeString(string a_String)
	{
		IEnumerator enumerator = a_String.GetEnumerator();
		ArrayList arrayList = new ArrayList();
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		while (enumerator.MoveNext())
		{
			if ((char)enumerator.Current == '%')
			{
				enumerator.MoveNext();
				string text = new string((char)enumerator.Current, 1);
				enumerator.MoveNext();
				text += new string((char)enumerator.Current, 1);
				int num = int.Parse(text.ToUpper(), NumberStyles.HexNumber);
				arrayList.Add((byte)num);
			}
			else
			{
				arrayList.Add((byte)(char)enumerator.Current);
			}
		}
		return uTF8Encoding.GetString((byte[])arrayList.ToArray(typeof(byte)));
	}
}
