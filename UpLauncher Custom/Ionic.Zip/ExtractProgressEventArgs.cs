namespace Ionic.Zip;

public class ExtractProgressEventArgs : ZipProgressEventArgs
{
	private int int_1;

	private string string_1;

	public int EntriesExtracted => int_1;

	public string ExtractLocation => string_1;

	internal ExtractProgressEventArgs(string string_2, bool bool_1, int int_2, int int_3, ZipEntry zipEntry_1, string string_3)
		: base(string_2, bool_1 ? ZipProgressEventType.Extracting_BeforeExtractEntry : ZipProgressEventType.Extracting_AfterExtractEntry)
	{
		base.EntriesTotal = int_2;
		base.CurrentEntry = zipEntry_1;
		int_1 = int_3;
		string_1 = string_3;
	}

	internal ExtractProgressEventArgs(string string_2, ZipProgressEventType zipProgressEventType_1)
		: base(string_2, zipProgressEventType_1)
	{
	}

	internal ExtractProgressEventArgs()
	{
	}

	internal static ExtractProgressEventArgs smethod_0(string string_2, ZipEntry zipEntry_1, string string_3)
	{
		ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs();
		extractProgressEventArgs.ArchiveName = string_2;
		extractProgressEventArgs.EventType = ZipProgressEventType.Extracting_BeforeExtractEntry;
		extractProgressEventArgs.CurrentEntry = zipEntry_1;
		extractProgressEventArgs.string_1 = string_3;
		return extractProgressEventArgs;
	}

	internal static ExtractProgressEventArgs smethod_1(string string_2, ZipEntry zipEntry_1, string string_3)
	{
		ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs();
		extractProgressEventArgs.ArchiveName = string_2;
		extractProgressEventArgs.EventType = ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite;
		extractProgressEventArgs.CurrentEntry = zipEntry_1;
		extractProgressEventArgs.string_1 = string_3;
		return extractProgressEventArgs;
	}

	internal static ExtractProgressEventArgs smethod_2(string string_2, ZipEntry zipEntry_1, string string_3)
	{
		ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs();
		extractProgressEventArgs.ArchiveName = string_2;
		extractProgressEventArgs.EventType = ZipProgressEventType.Extracting_AfterExtractEntry;
		extractProgressEventArgs.CurrentEntry = zipEntry_1;
		extractProgressEventArgs.string_1 = string_3;
		return extractProgressEventArgs;
	}

	internal static ExtractProgressEventArgs smethod_3(string string_2, string string_3)
	{
		ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(string_2, ZipProgressEventType.Extracting_BeforeExtractAll);
		extractProgressEventArgs.string_1 = string_3;
		return extractProgressEventArgs;
	}

	internal static ExtractProgressEventArgs smethod_4(string string_2, string string_3)
	{
		ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(string_2, ZipProgressEventType.Extracting_AfterExtractAll);
		extractProgressEventArgs.string_1 = string_3;
		return extractProgressEventArgs;
	}

	internal static ExtractProgressEventArgs smethod_5(string string_2, ZipEntry zipEntry_1, long long_2, long long_3)
	{
		ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(string_2, ZipProgressEventType.Extracting_EntryBytesWritten);
		extractProgressEventArgs.ArchiveName = string_2;
		extractProgressEventArgs.CurrentEntry = zipEntry_1;
		extractProgressEventArgs.BytesTransferred = long_2;
		extractProgressEventArgs.TotalBytesToTransfer = long_3;
		return extractProgressEventArgs;
	}
}
