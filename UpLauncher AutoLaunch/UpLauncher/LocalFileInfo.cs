using System;
using System.Runtime.Serialization;

namespace UpLauncher;

[Serializable]
public class LocalFileInfo : ISerializable
{
	public ulong Length;

	public string FullName;

	public DateTime CreationTime;

	public DateTime LastWriteTime;

	public string MD5;

	public bool Installed;

	public LocalFileInfo(string a_strPath)
	{
		Length = 0uL;
		FullName = a_strPath.Replace('/', '\\');
		FullName = FullName.Replace("\\\\", "\\");
		CreationTime = DateTime.MinValue;
		LastWriteTime = DateTime.MinValue;
		MD5 = "";
		Installed = false;
	}

	protected LocalFileInfo(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException("info");
		}
		Length = (ulong)info.GetValue("Length", typeof(ulong));
		FullName = (string)info.GetValue("FullName", typeof(string));
		CreationTime = (DateTime)info.GetValue("CreationTime", typeof(DateTime));
		LastWriteTime = (DateTime)info.GetValue("LastWriteTime", typeof(DateTime));
		MD5 = (string)info.GetValue("MD5", typeof(string));
		Installed = (bool)info.GetValue("Installed", typeof(bool));
	}

	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException("info");
		}
		info.AddValue("Length", Length);
		info.AddValue("FullName", FullName);
		info.AddValue("CreationTime", CreationTime);
		info.AddValue("LastWriteTime", LastWriteTime);
		info.AddValue("MD5", MD5);
		info.AddValue("Installed", Installed);
	}
}
