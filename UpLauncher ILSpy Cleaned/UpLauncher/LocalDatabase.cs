using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UpLauncher;

public class LocalDatabase : IEnumerable<LocalFileInfo>
{
	private List<LocalFileInfo> list_0;

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

	public LocalDatabase()
	{
		list_0 = new List<LocalFileInfo>();
	}

	public bool AddFileFromFileEntry(FileEntry entry)
	{
		if (entry == null)
		{
			return false;
		}
		string text = entry.LocalFilePath.Replace('/', '\\');
		text = entry.LocalFilePath.Replace("\\\\", "\\");
		FileInfo fileInfo = null;
		if (entry.FileIsInstalled)
		{
			fileInfo = new FileInfo(text);
			if (fileInfo == null || !fileInfo.Exists)
			{
				return false;
			}
		}
		else
		{
			fileInfo = new FileInfo(entry.GetTempFilePath());
			if (fileInfo == null || !fileInfo.Exists)
			{
				return false;
			}
		}
		if (entry.FileIsLauncher && Path.GetFileName(entry.LocalFilePath).CompareTo("_UpLauncher.exe") == 0)
		{
			text = Path.GetDirectoryName(entry.LocalFilePath) + "\\UpLauncher.exe";
		}
		LocalFileInfo localFileInfo = Find(text);
		if (localFileInfo == null)
		{
			localFileInfo = new LocalFileInfo(entry.LocalFilePath);
			if (localFileInfo == null)
			{
				return false;
			}
			list_0.Add(localFileInfo);
		}
		localFileInfo.Length = (ulong)fileInfo.Length;
		localFileInfo.FullName = text;
		localFileInfo.CreationTime = fileInfo.CreationTime;
		localFileInfo.LastWriteTime = fileInfo.LastWriteTime;
		if (!string.IsNullOrEmpty(entry.LocalFileMD5))
		{
			localFileInfo.MD5 = entry.LocalFileMD5;
		}
		localFileInfo.Installed = entry.FileIsInstalled;
		Class25.smethod_2($"LocalDatabase::AddFileFromFileEntry(\"{localFileInfo.FullName}\", {localFileInfo.Length}, {localFileInfo.LastWriteTime})");
		return true;
	}

	public bool ReadFromFile(string a_strPath)
	{
		if (!string.IsNullOrEmpty(a_strPath) && File.Exists(a_strPath))
		{
			try
			{
				Installer.RemoveFileAttributes(a_strPath);
				using FileStream serializationStream = File.OpenRead(a_strPath);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				list_0 = (List<LocalFileInfo>)binaryFormatter.Deserialize(serializationStream);
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
			}
			return list_0.Count > 0;
		}
		return false;
	}

	public bool WriteInFile(string a_strPath)
	{
		if (!string.IsNullOrEmpty(a_strPath) && list_0 != null && list_0.Count != 0)
		{
			try
			{
				Installer.RemoveFileAttributes(a_strPath);
				using FileStream fileStream = File.Create(a_strPath);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(fileStream, list_0);
				fileStream.Close();
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
				return false;
			}
			return true;
		}
		return false;
	}

	public LocalFileInfo Find(string a_strPath)
	{
		if (string.IsNullOrEmpty(a_strPath))
		{
			return null;
		}
		string text = a_strPath.Replace('/', '\\');
		text = text.Replace("\\\\", "\\");
		foreach (LocalFileInfo item in list_0)
		{
			if (string.Compare(item.FullName, text, ignoreCase: true) == 0)
			{
				return item;
			}
		}
		return null;
	}

	public IEnumerator<LocalFileInfo> GetEnumerator()
	{
		return list_0.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
