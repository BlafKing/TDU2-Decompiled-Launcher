namespace Ionic.Zip;

public class SaveProgressEventArgs : ZipProgressEventArgs
{
	private int int_1;

	public int EntriesSaved => int_1;

	internal SaveProgressEventArgs(string string_1, bool bool_1, int int_2, int int_3, ZipEntry zipEntry_1)
		: base(string_1, bool_1 ? ZipProgressEventType.Saving_BeforeWriteEntry : ZipProgressEventType.Saving_AfterWriteEntry)
	{
		base.EntriesTotal = int_2;
		base.CurrentEntry = zipEntry_1;
		int_1 = int_3;
	}

	internal SaveProgressEventArgs()
	{
	}

	internal SaveProgressEventArgs(string string_1, ZipProgressEventType zipProgressEventType_1)
		: base(string_1, zipProgressEventType_1)
	{
	}

	internal static SaveProgressEventArgs smethod_0(string string_1, ZipEntry zipEntry_1, long long_2, long long_3)
	{
		SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(string_1, ZipProgressEventType.Saving_EntryBytesRead);
		saveProgressEventArgs.ArchiveName = string_1;
		saveProgressEventArgs.CurrentEntry = zipEntry_1;
		saveProgressEventArgs.BytesTransferred = long_2;
		saveProgressEventArgs.TotalBytesToTransfer = long_3;
		return saveProgressEventArgs;
	}

	internal static SaveProgressEventArgs smethod_1(string string_1)
	{
		return new SaveProgressEventArgs(string_1, ZipProgressEventType.Saving_Started);
	}

	internal static SaveProgressEventArgs smethod_2(string string_1)
	{
		return new SaveProgressEventArgs(string_1, ZipProgressEventType.Saving_Completed);
	}
}
