using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace UpLauncher;

public class Database : IEnumerable<FileEntry>
{
	public delegate void OnReadXmlFile(string a_strName, XmlNode a_rNode, uint a_uDepth);

	private string a_strName;

	private string string_0;

	private string string_1;

	private XmlDocument xmlDocument_0;

	private XmlNode xmlNode_0;

	private List<FileEntry> list_0;

	private bool bool_0;

	private LocalDatabase localDatabase_0;

	[CompilerGenerated]
	private static Predicate<FileEntry> predicate_0;

	public LocalDatabase DbLocalFilesEntries => localDatabase_0;

	public string Name => a_strName;

	public string Date => string_0;

	public string HttpRoot => string_1;

	public int Count
	{
		get
		{
			if (list_0 == null)
			{
				return 0;
			}
			return list_0.Count;
		}
	}

	public bool FilesMustBeLaunched => bool_0;

	public Database(string a_strName)
	{
		this.a_strName = a_strName;
		string_0 = null;
		string_1 = Path.GetDirectoryName(a_strName);
		xmlDocument_0 = null;
		xmlNode_0 = null;
		list_0 = null;
		bool_0 = false;
		localDatabase_0 = new LocalDatabase();
	}

	public override string ToString()
	{
		string text = $"Database name=\"{a_strName}\" entries=\"{((list_0 != null) ? list_0.Count : 0)}\"";
		if (list_0 != null)
		{
			foreach (FileEntry item in list_0)
			{
				text += $"\n{item}";
			}
		}
		return text;
	}

	public FileEntry FindEntryByNodeName(string a_strName)
	{
		if (string.IsNullOrEmpty(a_strName))
		{
			return null;
		}
		if (list_0 == null)
		{
			return null;
		}
		foreach (FileEntry item in list_0)
		{
			if (string.Compare(item.NodeName, a_strName, ignoreCase: true) == 0)
			{
				return item;
			}
		}
		return null;
	}

	public FileEntry AddEntry(XmlNode a_rNode, string a_strLocalPath, string a_strLocalTempPath, string a_strRemotePath)
	{
		if (a_rNode == null)
		{
			return null;
		}
		if (list_0 == null)
		{
			list_0 = new List<FileEntry>();
		}
		FileEntry fileEntry = new FileEntry(a_rNode, a_strLocalPath, a_strLocalTempPath, a_strRemotePath);
		list_0.Add(fileEntry);
		return fileEntry;
	}

	public bool WriteXmlFile(string a_strPath)
	{
		if (string.IsNullOrEmpty(a_strPath))
		{
			return false;
		}
		Class25.smethod_2($"Database.WriteXmlFile({a_strPath})");
		if (xmlDocument_0 != null)
		{
			try
			{
				using FileStream outStream = File.OpenWrite(a_strPath);
				xmlDocument_0.Save(outStream);
				return true;
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
			}
		}
		return false;
	}

	private string method_0(XmlNode xmlNode_1, string string_2)
	{
		return xmlNode_1.Attributes[string_2]?.Value;
	}

	private void method_1(XmlNode xmlNode_1, OnReadXmlFile onReadXmlFile_0, uint uint_0)
	{
		if (string.Compare(xmlNode_1.Name, "VersionConfig", ignoreCase: true) == 0)
		{
			string_0 = method_0(xmlNode_1, "date");
			string value = method_0(xmlNode_1, "root");
			if (!string.IsNullOrEmpty(value))
			{
				string_1 = value;
			}
		}
		onReadXmlFile_0?.Invoke(xmlNode_1.Name, xmlNode_1, uint_0);
		foreach (XmlNode childNode in xmlNode_1.ChildNodes)
		{
			method_1(childNode, onReadXmlFile_0, uint_0 + 1);
		}
	}

	public bool ReadXmlFile(Stream a_rStream, OnReadXmlFile a_fCallBack)
	{
		Class25.smethod_2($"Database.ReadXmlFile({a_rStream})");
		if (xmlDocument_0 == null)
		{
			xmlDocument_0 = new XmlDocument();
		}
		if (a_rStream != null)
		{
			try
			{
				xmlDocument_0.Load(a_rStream);
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
				return false;
			}
		}
		if (list_0 == null)
		{
			list_0 = new List<FileEntry>();
		}
		xmlNode_0 = xmlDocument_0.SelectSingleNode("VersionConfig");
		if (xmlNode_0 != null)
		{
			method_1(xmlNode_0, a_fCallBack, 0u);
			return true;
		}
		return false;
	}

	public bool CheckLocalFiles()
	{
		Class25.smethod_2($"Database.CheckLocalFiles( {list_0.Count} files )");
		localDatabase_0.ReadFromFile(Class32.string_31);
		bool_0 = false;
		foreach (FileEntry item in list_0)
		{
			if (item.FileMustBeDeleted)
			{
				continue;
			}
			LocalFileInfo localFileInfo = localDatabase_0.Find(item.LocalFilePath);
			item.CheckIfFileMustBeUpdated(localFileInfo);
			if (!item.FileMustBeUpdated)
			{
				localDatabase_0.AddFileFromFileEntry(item);
				bool_0 |= item.FileMustBeRun;
				continue;
			}
			if (item.FileParts.Count > 0)
			{
				string a_strPath = item.GetPartTempFolder() + ".localdb";
				LocalDatabase localDatabase = new LocalDatabase();
				localDatabase.ReadFromFile(a_strPath);
				foreach (FileEntry filePart in item.FileParts)
				{
					filePart.SetTempAsLocal();
					LocalFileInfo localFileInfo2 = localDatabase.Find(filePart.TempFilePath);
					filePart.CheckIfFileMustBeUpdated(localFileInfo2);
					if (!filePart.FileMustBeUpdated && localFileInfo2 == null)
					{
						filePart.CheckLocalInfo();
						localDatabase.AddFileFromFileEntry(filePart);
					}
				}
				localDatabase.WriteInFile(a_strPath);
			}
			bool_0 |= item.FileMustBeLaunched;
		}
		localDatabase_0.WriteInFile(Class32.string_31);
		return true;
	}

	public bool AddFileInLocalFilesDatabase(FileEntry entry)
	{
		if (localDatabase_0.AddFileFromFileEntry(entry))
		{
			localDatabase_0.WriteInFile(Class32.string_31);
			return true;
		}
		return false;
	}

	public void GetGlobalInfo(ProgressInfo a_rProgressInfo)
	{
		if (list_0 != null)
		{
			foreach (FileEntry item in list_0)
			{
				if (item.FileMustBeDeleted || !item.FileMustBeUpdated)
				{
					continue;
				}
				ulong num = item.RemoteFileSize;
				if (File.Exists(item.LocalFilePath))
				{
					num += item.RemoteFileSize;
				}
				LocalFileInfo localFileInfo = localDatabase_0.Find(item.LocalFilePath);
				if (localFileInfo == null && item.FileIsCompressed)
				{
					num += item.RemoteFileSize;
				}
				foreach (FileEntry filePart in item.FileParts)
				{
					a_rProgressInfo.AddFileToTotalBytesToReceive(filePart.RemoteFileSize, filePart.RemoteFileSize);
				}
				a_rProgressInfo.AddFileToTotalBytesToReceive(item.TempFileSize, num);
			}
		}
		a_rProgressInfo.ComputeTotalPercent();
	}

	public IEnumerator<FileEntry> GetEnumerator()
	{
		return list_0.GetEnumerator();
	}

	public IEnumerator<FileEntry> GetMustBeUpdatedEnumerator()
	{
		return list_0.FindAll((FileEntry fileEntry_0) => fileEntry_0.FileMustBeUpdated).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	[CompilerGenerated]
	private static bool smethod_0(FileEntry fileEntry_0)
	{
		return fileEntry_0.FileMustBeUpdated;
	}
}
