using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace UpLauncher;

public class FileEntry
{
	private FileEntryInfo fileEntryInfo_0;

	private FileEntryInfo fileEntryInfo_1;

	private FileEntryInfo fileEntryInfo_2;

	private List<FileEntry> list_0;

	private int int_0;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private bool bool_5;

	private XmlNode a_rNode;

	private string string_0;

	private string string_1;

	private string string_2;

	public string NodeName => string_0;

	public string Action => string_1;

	public string RunParameters => string_2;

	public string LocalFilePath => fileEntryInfo_0.Path;

	public string LocalFileName => fileEntryInfo_0.Name;

	public string LocalFileMD5 => fileEntryInfo_0.MD5;

	public ulong LocalFileSize => fileEntryInfo_0.Size;

	public bool LocalFileExists => fileEntryInfo_0.FileExists;

	public string TempFilePath => fileEntryInfo_1.Path;

	public string TempFileName => fileEntryInfo_1.Name;

	public string TempFileMD5 => fileEntryInfo_1.MD5;

	public ulong TempFileSize => fileEntryInfo_1.Size;

	public bool TempFileExists => fileEntryInfo_1._CheckLocalFileExists();

	public string RemoteFileFullPath => fileEntryInfo_2.Path + "/" + fileEntryInfo_2.Name;

	public string RemoteFilePath => fileEntryInfo_2.Path;

	public string RemoteFileName => fileEntryInfo_2.Name;

	public string RemoteFileMD5 => fileEntryInfo_2.MD5;

	public ulong RemoteFileSize => fileEntryInfo_2.Size;

	public bool FileMustBeDeleted => string.Compare(string_1, "DEL", ignoreCase: true) == 0;

	public bool FileMustBeLaunched => string.Compare(string_1, 0, "RUN", 0, 3, ignoreCase: true) == 0;

	public bool FileMustBeRun => string.Compare(string_1, "RUN", ignoreCase: true) == 0;

	public bool FileMustBeRunOnce => string.Compare(string_1, "RUNONCE", ignoreCase: true) == 0;

	public bool FileMustBeUpdated => bool_1;

	public bool FileIsCompressed => bool_0;

	public bool FileIsLauncher => bool_2;

	public bool FileIsGameExeFile => bool_3;

	public bool FileIsMultiPart => bool_4;

	public bool FileIsDownloaded => method_2();

	public bool FileIsInstalled => bool_5;

	public List<FileEntry> FileParts => list_0;

	public int CurrentFilePart
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

	private bool method_0(string string_3, bool bool_6)
	{
		fileEntryInfo_0.Path = string_3 + "\\" + fileEntryInfo_0.Name;
		fileEntryInfo_0.Path = fileEntryInfo_0.Path.Replace('/', '\\');
		fileEntryInfo_0.Path = fileEntryInfo_0.Path.Replace("\\\\", "\\");
		return method_1(bool_6);
	}

	private bool method_1(bool bool_6)
	{
		fileEntryInfo_0._CheckLocalFileExists();
		if (!fileEntryInfo_0.FileExists)
		{
			fileEntryInfo_0.MD5 = null;
			fileEntryInfo_0.Size = 0uL;
			return false;
		}
		try
		{
			FileInfo fileInfo = new FileInfo(fileEntryInfo_0.Path);
			fileEntryInfo_0.Size = (ulong)fileInfo.Length;
			if (bool_6)
			{
				fileEntryInfo_0.MD5 = GetLocalFileMD5(fileEntryInfo_0.Path);
			}
			return true;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
	}

	public FileEntry(XmlNode a_rNode, string a_strLocalPath, string a_strLocalTempPath, string a_strRemotePath)
	{
		string_1 = "ADD";
		string_2 = null;
		fileEntryInfo_0 = new FileEntryInfo();
		fileEntryInfo_1 = new FileEntryInfo();
		fileEntryInfo_2 = new FileEntryInfo();
		list_0 = new List<FileEntry>();
		bool_0 = false;
		bool_1 = false;
		bool_2 = false;
		bool_3 = false;
		bool_4 = false;
		this.a_rNode = a_rNode;
		string_0 = "FileEntry";
		bool_5 = false;
		if (a_rNode == null)
		{
			return;
		}
		string_0 = a_rNode.Name;
		foreach (XmlAttribute attribute in a_rNode.Attributes)
		{
			if (string.Compare(attribute.Name, "action", ignoreCase: true) == 0)
			{
				string_1 = attribute.Value.ToUpper();
				if (string.IsNullOrEmpty(string_1) || (string.Compare(string_1, "DEL", ignoreCase: true) != 0 && string.Compare(string_1, "RUN", ignoreCase: true) != 0 && string.Compare(string_1, "RUNONCE", ignoreCase: true) != 0))
				{
					string_1 = "ADD";
				}
			}
			if (string.Compare(attribute.Name, "runparameters", ignoreCase: true) == 0)
			{
				string_2 = attribute.Value;
			}
			else if (string.Compare(attribute.Name, "path", ignoreCase: true) == 0)
			{
				fileEntryInfo_2.Name = attribute.Value;
				fileEntryInfo_2.Path = a_strRemotePath;
				fileEntryInfo_0.Name = fileEntryInfo_2.Name;
			}
			else if (string.Compare(attribute.Name, "md5", ignoreCase: true) == 0)
			{
				fileEntryInfo_2.MD5 = attribute.Value;
			}
			else if (string.Compare(attribute.Name, "size", ignoreCase: true) == 0)
			{
				fileEntryInfo_2.Size = ulong.Parse(attribute.Value);
			}
			else if (string.Compare(attribute.Name, "remote_path", ignoreCase: true) == 0)
			{
				if (string.IsNullOrEmpty(a_strLocalPath))
				{
					string arg = attribute.Value.Substring(attribute.Value.LastIndexOf('/') + 1);
					string arg2 = attribute.Value.Substring(0, attribute.Value.IndexOf('.')).Replace("/", "\\");
					fileEntryInfo_1.Name = $"{arg2}\\{arg}";
					fileEntryInfo_1.Path = $"{a_strLocalTempPath}{fileEntryInfo_1.Name}";
				}
				else
				{
					fileEntryInfo_1.Name = attribute.Value.Replace("/", "\\");
					fileEntryInfo_1.Path = a_strLocalTempPath + fileEntryInfo_1.Name;
				}
				fileEntryInfo_2.Name = attribute.Value;
				fileEntryInfo_2.Path = a_strRemotePath;
			}
			else if (string.Compare(attribute.Name, "remote_size", ignoreCase: true) == 0)
			{
				fileEntryInfo_1.Size = ulong.Parse(attribute.Value);
			}
			else if (string.Compare(attribute.Name, "remote_md5", ignoreCase: true) == 0)
			{
				fileEntryInfo_1.MD5 = attribute.Value;
			}
		}
		_CheckMultiPart();
		method_0(a_strLocalPath, bool_6: false);
		if (string.IsNullOrEmpty(fileEntryInfo_1.Name))
		{
			fileEntryInfo_1.Copy(fileEntryInfo_0);
			fileEntryInfo_1.Path = a_strLocalTempPath + "\\" + fileEntryInfo_1.Name;
		}
		if (string.IsNullOrEmpty(a_strLocalPath))
		{
			fileEntryInfo_1.MD5 = fileEntryInfo_2.MD5;
			fileEntryInfo_1.Size = fileEntryInfo_2.Size;
		}
		bool_0 = string.Compare(Path.GetExtension(fileEntryInfo_1.Name), ".zip", ignoreCase: true) == 0;
		bool_2 = string.Compare(string_0, "UpLauncher", ignoreCase: true) == 0;
		bool_3 = string.Compare(string_0, "GameExeFile", ignoreCase: true) == 0;
	}

	protected void _CheckMultiPart()
	{
		if (a_rNode == null)
		{
			return;
		}
		if (a_rNode.ChildNodes.Count == 0)
		{
			bool_4 = false;
			return;
		}
		bool_4 = true;
		XmlNodeList childNodes = a_rNode.ChildNodes;
		string a_strLocalTempPath = TempFilePath.Replace(TempFileName, "");
		foreach (XmlNode item in childNodes)
		{
			list_0.Add(new FileEntry(item, "", a_strLocalTempPath, fileEntryInfo_2.Path));
		}
	}

	public void ChangeUpLauncherLocalFileName(string a_strNewFileName)
	{
		if (FileIsLauncher)
		{
			fileEntryInfo_0.Name = a_strNewFileName;
			method_0(Path.GetDirectoryName(fileEntryInfo_0.Path), bool_6: true);
		}
	}

	public bool CheckLocalInfo()
	{
		return method_1(bool_6: true);
	}

	public override string ToString()
	{
		return $"FileEntry LocalFileName=\"{LocalFileName}\" LocalFileSize=\"{LocalFileSize}\" LocalFileMD5=\"{LocalFileMD5}\" m_bToUpdate=\"{bool_1}\"";
	}

	public bool CheckIfFileMustBeUpdated(LocalFileInfo localFileInfo)
	{
		bool_1 = false;
		if (!File.Exists(LocalFilePath))
		{
			bool_1 = true;
		}
		else if (localFileInfo != null)
		{
			localFileInfo.Length = (ulong)new FileInfo(localFileInfo.FullName).Length;
			if (!localFileInfo.Installed || RemoteFileSize != localFileInfo.Length || RemoteFileMD5 != localFileInfo.MD5)
			{
				bool_1 = true;
			}
		}
		else
		{
			method_1(bool_6: true);
			if (LocalFileSize != RemoteFileSize || LocalFileMD5 != RemoteFileMD5)
			{
				bool_1 = true;
			}
		}
		if (bool_1)
		{
			if (bool_5 && FileIsDownloaded && GetLocalFileMD5(TempFilePath) != RemoteFileMD5)
			{
				File.Delete(TempFilePath);
			}
			bool_5 = false;
			Class25.smethod_2($"File \"{LocalFileName}\" has to be update ({RemoteFileSize} bytes).");
		}
		else
		{
			InstallFile();
		}
		return bool_1;
	}

	private bool method_2()
	{
		return File.Exists(GetTempFilePath());
	}

	public string GetTempFilePath()
	{
		if (FileIsMultiPart)
		{
			string partTempFolder = GetPartTempFolder();
			return partTempFolder.Substring(0, partTempFolder.LastIndexOf('\\') + 1) + Path.GetFileName(fileEntryInfo_0.Name);
		}
		if (FileIsCompressed)
		{
			return Path.GetDirectoryName(fileEntryInfo_1.Path) + "\\" + Path.GetFileNameWithoutExtension(fileEntryInfo_1.Name);
		}
		return fileEntryInfo_1.Path;
	}

	public string GetPartTempFolder()
	{
		try
		{
			return TempFilePath.Substring(0, TempFilePath.IndexOf('.'));
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return TempFilePath.Substring(0, TempFilePath.IndexOf(TempFileName));
		}
	}

	public void InstallFile()
	{
		bool_5 = true;
	}

	public void UnInstallFile()
	{
		bool_5 = false;
	}

	public static string GetLocalFileMD5(string a_strFileName)
	{
		if (string.IsNullOrEmpty(a_strFileName))
		{
			return null;
		}
		if (!File.Exists(a_strFileName))
		{
			return null;
		}
		try
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			FileStream fileStream = new FileStream(a_strFileName, FileMode.Open, FileAccess.Read);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(fileStream);
			fileStream.Close();
			string text = "";
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					text += $"{array[i]:x2}";
				}
			}
			Class25.smethod_2($"FileEntry.GetLocalFileMD5({a_strFileName}) returns {text}");
			return text;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return null;
		}
	}

	public void SetTempFilePath(string a_strTempFilePath)
	{
		fileEntryInfo_1.Path = a_strTempFilePath;
		fileEntryInfo_1.Name = fileEntryInfo_1.Path.Substring(fileEntryInfo_1.Path.LastIndexOf('\\'));
	}

	public void SetTempAsLocal()
	{
		fileEntryInfo_0.Copy(fileEntryInfo_1);
	}

	public void SetTempAsNonMultiPart()
	{
		string directoryName = Path.GetDirectoryName(fileEntryInfo_1.Path);
		fileEntryInfo_1.Copy(fileEntryInfo_2);
		fileEntryInfo_1.Name = fileEntryInfo_0.Name;
		fileEntryInfo_1.Path = directoryName.Substring(0, directoryName.LastIndexOf('\\') - 1) + fileEntryInfo_1.Name;
		fileEntryInfo_1._CheckLocalFileExists();
		bool_4 = false;
		list_0.Clear();
	}
}
