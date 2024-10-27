namespace Ionic.Zip;

public class AddProgressEventArgs : ZipProgressEventArgs
{
	internal AddProgressEventArgs()
	{
	}

	private AddProgressEventArgs(string string_1, ZipProgressEventType zipProgressEventType_1)
		: base(string_1, zipProgressEventType_1)
	{
	}

	internal static AddProgressEventArgs smethod_0(string string_1, ZipEntry zipEntry_1, int int_1)
	{
		AddProgressEventArgs addProgressEventArgs = new AddProgressEventArgs(string_1, ZipProgressEventType.Adding_AfterAddEntry);
		addProgressEventArgs.EntriesTotal = int_1;
		addProgressEventArgs.CurrentEntry = zipEntry_1;
		return addProgressEventArgs;
	}

	internal static AddProgressEventArgs smethod_1(string string_1)
	{
		return new AddProgressEventArgs(string_1, ZipProgressEventType.Adding_Started);
	}

	internal static AddProgressEventArgs smethod_2(string string_1)
	{
		return new AddProgressEventArgs(string_1, ZipProgressEventType.Adding_Completed);
	}
}
