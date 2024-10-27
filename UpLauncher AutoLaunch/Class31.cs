using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;

internal class Class31
{
	private ZipFile zipFile_0;

	private ZipOutputStream zipOutputStream_0;

	private ZipInputStream zipInputStream_0;

	public Class31(object object_0)
	{
		zipFile_0 = object_0 as ZipFile;
		zipOutputStream_0 = object_0 as ZipOutputStream;
		zipInputStream_0 = object_0 as ZipInputStream;
	}

	[SpecialName]
	public ZipFile method_0()
	{
		return zipFile_0;
	}

	[SpecialName]
	public ZipOutputStream method_1()
	{
		return zipOutputStream_0;
	}

	[SpecialName]
	public string method_2()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.Name;
		}
		return zipOutputStream_0.Name;
	}

	[SpecialName]
	public string method_3()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.string_2;
		}
		return zipOutputStream_0.string_0;
	}

	[SpecialName]
	public Zip64Option method_4()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.zip64Option_0;
		}
		return zipOutputStream_0.zip64Option_0;
	}

	[SpecialName]
	public int method_5()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.BufferSize;
		}
		return 0;
	}

	[SpecialName]
	public ParallelDeflateOutputStream method_6()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.parallelDeflateOutputStream_0;
		}
		return zipOutputStream_0.parallelDeflateOutputStream_0;
	}

	[SpecialName]
	public void method_7(ParallelDeflateOutputStream parallelDeflateOutputStream_0)
	{
		if (zipFile_0 != null)
		{
			zipFile_0.parallelDeflateOutputStream_0 = parallelDeflateOutputStream_0;
		}
		else
		{
			zipOutputStream_0.parallelDeflateOutputStream_0 = parallelDeflateOutputStream_0;
		}
	}

	[SpecialName]
	public long method_8()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.ParallelDeflateThreshold;
		}
		return zipOutputStream_0.ParallelDeflateThreshold;
	}

	[SpecialName]
	public int method_9()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.CodecBufferSize;
		}
		return zipOutputStream_0.CodecBufferSize;
	}

	[SpecialName]
	public CompressionStrategy method_10()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.Strategy;
		}
		return zipOutputStream_0.Strategy;
	}

	[SpecialName]
	public Zip64Option method_11()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.UseZip64WhenSaving;
		}
		return zipOutputStream_0.EnableZip64;
	}

	[SpecialName]
	public Encoding method_12()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.ProvisionalAlternateEncoding;
		}
		return zipInputStream_0.ProvisionalAlternateEncoding;
	}

	[SpecialName]
	public Stream method_13()
	{
		if (zipFile_0 != null)
		{
			return zipFile_0.ReadStream;
		}
		return zipInputStream_0.ReadStream;
	}
}
