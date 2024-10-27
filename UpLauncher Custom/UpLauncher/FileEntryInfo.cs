using System.IO;

namespace UpLauncher;

public class FileEntryInfo
{
	private bool bool_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private ulong ulong_0;

	public bool FileExists => bool_0;

	public string Path
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

	public string Name
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

	public string MD5
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

	public ulong Size
	{
		get
		{
			return ulong_0;
		}
		set
		{
			ulong_0 = value;
		}
	}

	public void Copy(FileEntryInfo entry)
	{
		bool_0 = entry.bool_0;
		string_0 = entry.string_0;
		string_1 = entry.string_1;
		string_2 = entry.string_2;
		ulong_0 = entry.ulong_0;
	}

	public FileEntryInfo()
	{
		bool_0 = false;
		string_0 = null;
		string_1 = null;
		string_2 = null;
		ulong_0 = 0uL;
	}

	public bool _CheckLocalFileExists()
	{
		bool_0 = false;
		if (!string.IsNullOrEmpty(string_0))
		{
			bool_0 = File.Exists(string_0);
		}
		return bool_0;
	}
}
