using System;
using System.Runtime.CompilerServices;

namespace Ionic.Zip;

public class SelfExtractorSaveOptions
{
	[CompilerGenerated]
	private SelfExtractorFlavor selfExtractorFlavor_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private string string_2;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private ExtractExistingFileAction extractExistingFileAction_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private Version version_0;

	[CompilerGenerated]
	private string string_3;

	[CompilerGenerated]
	private string string_4;

	[CompilerGenerated]
	private string string_5;

	[CompilerGenerated]
	private string string_6;

	public SelfExtractorFlavor Flavor
	{
		[CompilerGenerated]
		get
		{
			return selfExtractorFlavor_0;
		}
		[CompilerGenerated]
		set
		{
			selfExtractorFlavor_0 = value;
		}
	}

	public string PostExtractCommandLine
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public string DefaultExtractDirectory
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public string IconFile
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	public bool Quiet
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public ExtractExistingFileAction ExtractExistingFile
	{
		[CompilerGenerated]
		get
		{
			return extractExistingFileAction_0;
		}
		[CompilerGenerated]
		set
		{
			extractExistingFileAction_0 = value;
		}
	}

	public bool RemoveUnpackedFilesAfterExecute
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public Version FileVersion
	{
		[CompilerGenerated]
		get
		{
			return version_0;
		}
		[CompilerGenerated]
		set
		{
			version_0 = value;
		}
	}

	public string ProductVersion
	{
		[CompilerGenerated]
		get
		{
			return string_3;
		}
		[CompilerGenerated]
		set
		{
			string_3 = value;
		}
	}

	public string Copyright
	{
		[CompilerGenerated]
		get
		{
			return string_4;
		}
		[CompilerGenerated]
		set
		{
			string_4 = value;
		}
	}

	public string Description
	{
		[CompilerGenerated]
		get
		{
			return string_5;
		}
		[CompilerGenerated]
		set
		{
			string_5 = value;
		}
	}

	public string ProductName
	{
		[CompilerGenerated]
		get
		{
			return string_6;
		}
		[CompilerGenerated]
		set
		{
			string_6 = value;
		}
	}
}
