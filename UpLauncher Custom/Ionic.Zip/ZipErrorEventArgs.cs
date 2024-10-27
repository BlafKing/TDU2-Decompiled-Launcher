using System;

namespace Ionic.Zip;

public class ZipErrorEventArgs : ZipProgressEventArgs
{
	private Exception exception_0;

	public Exception Exception => exception_0;

	public string FileName => base.CurrentEntry.LocalFileName;

	private ZipErrorEventArgs()
	{
	}

	internal static ZipErrorEventArgs smethod_0(string string_1, ZipEntry zipEntry_1, Exception exception_1)
	{
		ZipErrorEventArgs zipErrorEventArgs = new ZipErrorEventArgs();
		zipErrorEventArgs.EventType = ZipProgressEventType.Error_Saving;
		zipErrorEventArgs.ArchiveName = string_1;
		zipErrorEventArgs.CurrentEntry = zipEntry_1;
		zipErrorEventArgs.exception_0 = exception_1;
		return zipErrorEventArgs;
	}
}
