using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;

namespace UpLauncher;

public class DxDiag
{
	public enum eStatus
	{
		None,
		Failed,
		Ok
	}

	private eStatus eStatus_0;

	private string string_0;

	private string string_1;

	private XmlDocument xmlDocument_0;

	public eStatus Status => eStatus_0;

	public string XmlPath
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

	public DxDiag()
	{
		eStatus_0 = eStatus.None;
		string_0 = null;
		string_1 = null;
		xmlDocument_0 = null;
	}

	public bool CollectInfo()
	{
		if (string.IsNullOrEmpty(string_1))
		{
			return false;
		}
		Class25.smethod_2("DxDiag.CollectInfo()");
		try
		{
			string_0 = Environment.GetFolderPath(Environment.SpecialFolder.System);
			if (!string.IsNullOrEmpty(string_0))
			{
				if (!File.Exists(string_1))
				{
					string_0 += "\\dxdiag.exe";
					if (!File.Exists(string_0))
					{
						eStatus_0 = eStatus.Failed;
						return false;
					}
					Process process = new Process();
					process.StartInfo.FileName = string_0;
					process.StartInfo.WorkingDirectory = Path.GetDirectoryName(string_1);
					process.StartInfo.Arguments = "/whql:off /x " + string_1;
					process.Start();
					do
					{
						Thread.Sleep(100);
						process.Refresh();
					}
					while (!process.HasExited);
				}
				if (!File.Exists(string_1))
				{
					return false;
				}
				xmlDocument_0 = new XmlDocument();
				using (FileStream fileStream = new FileStream(string_1, FileMode.Open))
				{
					xmlDocument_0.Load(fileStream);
					fileStream.Close();
				}
				if (xmlDocument_0.ChildNodes.Count <= 0)
				{
					eStatus_0 = eStatus.Failed;
					return false;
				}
				eStatus_0 = eStatus.Ok;
				return true;
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			eStatus_0 = eStatus.Failed;
		}
		eStatus_0 = eStatus.Failed;
		return false;
	}

	public string GetValue(string a_strSection, string a_strName)
	{
		if (!string.IsNullOrEmpty(a_strSection) && !string.IsNullOrEmpty(a_strName) && xmlDocument_0 != null && eStatus_0 == eStatus.Ok)
		{
			try
			{
				string xpath = "//" + a_strSection + "/" + a_strName;
				XmlNode xmlNode = xmlDocument_0.SelectSingleNode(xpath);
				if (xmlNode != null)
				{
					return xmlNode.InnerText.Trim();
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
			}
			return null;
		}
		return null;
	}

	public ulong GetValueUInt(string a_strSection, string a_strName)
	{
		try
		{
			string text = GetValue(a_strSection, a_strName);
			if (string.IsNullOrEmpty(text))
			{
				return 0uL;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] < '0' || text[i] > '9')
				{
					text = text.Substring(0, i);
					break;
				}
			}
			return ulong.Parse(text);
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return 0uL;
	}
}
