namespace Ionic.Zip;

public class ReadProgressEventArgs : ZipProgressEventArgs
{
	internal ReadProgressEventArgs()
	{
	}

	private ReadProgressEventArgs(string string_1, ZipProgressEventType zipProgressEventType_1)
		: base(string_1, zipProgressEventType_1)
	{
	}

	internal static ReadProgressEventArgs smethod_0(string string_1, int int_1)
	{
		ReadProgressEventArgs readProgressEventArgs = new ReadProgressEventArgs(string_1, ZipProgressEventType.Reading_BeforeReadEntry);
		readProgressEventArgs.EntriesTotal = int_1;
		return readProgressEventArgs;
	}

	internal static ReadProgressEventArgs smethod_1(string string_1, ZipEntry zipEntry_1, int int_1)
	{
		ReadProgressEventArgs readProgressEventArgs = new ReadProgressEventArgs(string_1, ZipProgressEventType.Reading_AfterReadEntry);
		readProgressEventArgs.EntriesTotal = int_1;
		readProgressEventArgs.CurrentEntry = zipEntry_1;
		return readProgressEventArgs;
	}

	internal static ReadProgressEventArgs smethod_2(string string_1)
	{
		return new ReadProgressEventArgs(string_1, ZipProgressEventType.Reading_Started);
	}

	internal static ReadProgressEventArgs smethod_3(string string_1, ZipEntry zipEntry_1, long long_2, long long_3)
	{
		ReadProgressEventArgs readProgressEventArgs = new ReadProgressEventArgs(string_1, ZipProgressEventType.Reading_ArchiveBytesRead);
		readProgressEventArgs.CurrentEntry = zipEntry_1;
		readProgressEventArgs.BytesTransferred = long_2;
		readProgressEventArgs.TotalBytesToTransfer = long_3;
		return readProgressEventArgs;
	}

	internal static ReadProgressEventArgs smethod_4(string string_1)
	{
		return new ReadProgressEventArgs(string_1, ZipProgressEventType.Reading_Completed);
	}
}
