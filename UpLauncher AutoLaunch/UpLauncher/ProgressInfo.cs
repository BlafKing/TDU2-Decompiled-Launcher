using System;

namespace UpLauncher;

public class ProgressInfo
{
	private FileEntry fileEntry_0;

	private ulong ulong_0;

	private ulong ulong_1;

	private uint uint_0;

	private DateTime dateTime_0;

	private DateTime dateTime_1;

	private uint uint_1;

	private uint uint_2;

	private ulong ulong_2;

	private ulong ulong_3;

	private ulong ulong_4;

	private uint uint_3;

	private ulong ulong_5;

	private bool bool_0;

	public ulong TotalFilesReceived => uint_2;

	public ulong TotalFilesToReceive => uint_1;

	public ulong TotalBytesReceived => ulong_3;

	public ulong TotalBytesToReceive => ulong_2;

	public ulong TotalRequiredBytes => ulong_5;

	public uint TotalBytesPercentage => uint_3;

	public ulong BytesPerSeconds => ulong_4;

	public DateTime TimeStart => dateTime_0;

	public DateTime EstimatedEndTime => dateTime_1;

	public string CurrentFileName
	{
		get
		{
			if (fileEntry_0 == null)
			{
				return null;
			}
			if (fileEntry_0.FileIsMultiPart)
			{
				if (fileEntry_0.CurrentFilePart > 0)
				{
					return $"{fileEntry_0.LocalFileName} part {fileEntry_0.CurrentFilePart}/{fileEntry_0.FileParts.Count + 1}";
				}
				return $"{fileEntry_0.LocalFileName} part {fileEntry_0.FileParts.Count + 1}/{fileEntry_0.FileParts.Count + 1}";
			}
			return fileEntry_0.LocalFileName;
		}
	}

	public ulong CurrentBytesReceived => ulong_1;

	public ulong CurrentBytesToReceive => ulong_0;

	public uint CurrentPercentage => uint_0;

	public bool Error => bool_0;

	public ProgressInfo()
	{
		dateTime_0 = (dateTime_1 = DateTime.Now);
		fileEntry_0 = null;
		ulong_0 = 0uL;
		ulong_1 = 0uL;
		uint_0 = 0u;
		uint_1 = 0u;
		uint_2 = 0u;
		ulong_2 = 0uL;
		ulong_3 = 0uL;
		uint_3 = 0u;
		ulong_5 = 0uL;
		ulong_4 = 0uL;
		bool_0 = false;
	}

	public void ResetCurrent()
	{
		fileEntry_0 = null;
		ulong_1 = 0uL;
		ulong_0 = 0uL;
		uint_0 = 0u;
	}

	public void SetTotalInfo(ulong a_64BytesToReceive, ulong a_u64BytesReceived, uint a_uPercentage, bool a_bRecomputePercentage)
	{
		ulong_2 = a_64BytesToReceive;
		ulong_3 = a_u64BytesReceived;
		uint_3 = a_uPercentage;
		if (a_bRecomputePercentage)
		{
			ComputeTotalPercent();
		}
	}

	public void SetNewFileInfo(FileEntry a_rEntry)
	{
		if (a_rEntry == fileEntry_0)
		{
			return;
		}
		fileEntry_0 = a_rEntry;
		foreach (FileEntry filePart in fileEntry_0.FileParts)
		{
			_ = filePart;
			uint_2++;
		}
		uint_2++;
	}

	public void SetCurrentInfo(ulong a_64BytesToReceive, ulong a_u64BytesReceived, uint a_uPercentage, bool a_bRecomputePercentage)
	{
		ulong_0 = a_64BytesToReceive;
		ulong_1 = a_u64BytesReceived;
		uint_0 = a_uPercentage;
		if (a_bRecomputePercentage)
		{
			ComputeCurrentPercent();
		}
	}

	public void AddToTotalBytesReceived(ulong a_u64Value)
	{
		ulong_3 += a_u64Value;
	}

	public void AddFileToTotalBytesToReceive(ulong a_u64FileSize, ulong a_u64RequiredBytes)
	{
		ulong_2 += a_u64FileSize;
		ulong_5 += a_u64RequiredBytes;
	}

	public void ErrorOccurs()
	{
		ulong_2 -= ulong_0;
		ulong_3 -= ulong_1;
		bool_0 = true;
	}

	public void ComputeCurrentPercent()
	{
		if (ulong_0 > 0L)
		{
			uint_0 = (uint)(ulong_1 * 100L / ulong_0);
			if (uint_0 > 100)
			{
				uint_0 = 100u;
			}
		}
		else
		{
			uint_0 = 100u;
		}
	}

	public void ComputeTotalPercent()
	{
		if (ulong_2 > 0L)
		{
			uint_3 = (uint)(ulong_3 * 100L / ulong_2);
			if (uint_3 > 100)
			{
				uint_3 = 100u;
			}
		}
		else
		{
			uint_3 = 100u;
		}
		ulong num = (ulong)(DateTime.Now - dateTime_0).TotalMilliseconds;
		if (num > 0L)
		{
			ulong_4 = ulong_3 * 1000L / num;
		}
		if (ulong_3 > 0L)
		{
			num = ulong_2 * num / ulong_3;
			dateTime_1 = dateTime_0.AddMilliseconds(num);
		}
	}
}
