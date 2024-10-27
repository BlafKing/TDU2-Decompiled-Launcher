using System;

namespace Ionic.Zip;

public class ZipProgressEventArgs : EventArgs
{
	private int int_0;

	private bool bool_0;

	private ZipEntry zipEntry_0;

	private ZipProgressEventType zipProgressEventType_0;

	private string string_0;

	private long long_0;

	private long long_1;

	public int EntriesTotal
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

	public ZipEntry CurrentEntry
	{
		get
		{
			return zipEntry_0;
		}
		set
		{
			zipEntry_0 = value;
		}
	}

	public bool Cancel
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = bool_0 || value;
		}
	}

	public ZipProgressEventType EventType
	{
		get
		{
			return zipProgressEventType_0;
		}
		set
		{
			zipProgressEventType_0 = value;
		}
	}

	public string ArchiveName
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

	public long BytesTransferred
	{
		get
		{
			return long_0;
		}
		set
		{
			long_0 = value;
		}
	}

	public long TotalBytesToTransfer
	{
		get
		{
			return long_1;
		}
		set
		{
			long_1 = value;
		}
	}

	internal ZipProgressEventArgs()
	{
	}

	internal ZipProgressEventArgs(string string_1, ZipProgressEventType zipProgressEventType_1)
	{
		string_0 = string_1;
		zipProgressEventType_0 = zipProgressEventType_1;
	}
}
