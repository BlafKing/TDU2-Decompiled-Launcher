using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip;

public class ZipOutputStream : Stream
{
	private EncryptionAlgorithm encryptionAlgorithm_0;

	private ZipEntryTimestamp zipEntryTimestamp_0;

	internal string string_0;

	private string string_1;

	private Stream stream_0;

	private ZipEntry zipEntry_0;

	internal Zip64Option zip64Option_0;

	private Dictionary<string, ZipEntry> dictionary_0;

	private int int_0;

	private Encoding encoding_0;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private Stream2 stream2_0;

	private Stream stream_1;

	private Stream stream_2;

	private CrcCalculatorStream crcCalculatorStream_0;

	private bool bool_5;

	private string fileName;

	private bool bool_6;

	internal ParallelDeflateOutputStream parallelDeflateOutputStream_0;

	private long long_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private CompressionStrategy compressionStrategy_0;

	[CompilerGenerated]
	private CompressionLevel compressionLevel_0;

	public string Password
	{
		set
		{
			if (bool_1)
			{
				bool_2 = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			string_0 = value;
			if (string_0 == null)
			{
				encryptionAlgorithm_0 = EncryptionAlgorithm.None;
			}
			else if (encryptionAlgorithm_0 == EncryptionAlgorithm.None)
			{
				encryptionAlgorithm_0 = EncryptionAlgorithm.PkzipWeak;
			}
		}
	}

	public EncryptionAlgorithm Encryption
	{
		get
		{
			return encryptionAlgorithm_0;
		}
		set
		{
			if (bool_1)
			{
				bool_2 = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (value == EncryptionAlgorithm.Unsupported)
			{
				bool_2 = true;
				throw new InvalidOperationException("You may not set Encryption to that value.");
			}
			encryptionAlgorithm_0 = value;
		}
	}

	public int CodecBufferSize
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public CompressionStrategy Strategy
	{
		[CompilerGenerated]
		get
		{
			return compressionStrategy_0;
		}
		[CompilerGenerated]
		set
		{
			compressionStrategy_0 = value;
		}
	}

	public ZipEntryTimestamp Timestamp
	{
		get
		{
			return zipEntryTimestamp_0;
		}
		set
		{
			if (bool_1)
			{
				bool_2 = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			zipEntryTimestamp_0 = value;
		}
	}

	public CompressionLevel CompressionLevel
	{
		[CompilerGenerated]
		get
		{
			return compressionLevel_0;
		}
		[CompilerGenerated]
		set
		{
			compressionLevel_0 = value;
		}
	}

	public string Comment
	{
		get
		{
			return string_1;
		}
		set
		{
			if (bool_1)
			{
				bool_2 = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			string_1 = value;
		}
	}

	public Zip64Option EnableZip64
	{
		get
		{
			return zip64Option_0;
		}
		set
		{
			if (bool_1)
			{
				bool_2 = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			zip64Option_0 = value;
		}
	}

	public bool OutputUsedZip64
	{
		get
		{
			if (!bool_3)
			{
				return bool_4;
			}
			return true;
		}
	}

	public bool IgnoreCase
	{
		get
		{
			return !bool_6;
		}
		set
		{
			bool_6 = !value;
		}
	}

	public bool UseUnicodeAsNecessary
	{
		get
		{
			return encoding_0 == Encoding.GetEncoding("UTF-8");
		}
		set
		{
			encoding_0 = (value ? Encoding.GetEncoding("UTF-8") : ZipFile.DefaultEncoding);
		}
	}

	public Encoding ProvisionalAlternateEncoding
	{
		get
		{
			return encoding_0;
		}
		set
		{
			encoding_0 = value;
		}
	}

	public long ParallelDeflateThreshold
	{
		get
		{
			return long_0;
		}
		set
		{
			if (value != 0L && value != -1L && value < 65536L)
			{
				throw new ArgumentException();
			}
			long_0 = value;
		}
	}

	internal Stream OutputStream => stream_0;

	internal string Name => fileName;

	public override bool CanRead => false;

	public override bool CanSeek => false;

	public override bool CanWrite => true;

	public override long Length
	{
		get
		{
			throw new NotSupportedException();
		}
	}

	public override long Position
	{
		get
		{
			return stream_0.Position;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public ZipOutputStream(Stream stream)
		: this(stream, leaveOpen: false)
	{
	}

	public ZipOutputStream(string fileName)
	{
		Stream stream_ = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
		method_0(stream_, bool_7: false);
		this.fileName = fileName;
	}

	public ZipOutputStream(Stream stream, bool leaveOpen)
	{
		method_0(stream, leaveOpen);
	}

	private void method_0(Stream stream_3, bool bool_7)
	{
		stream_0 = (stream_3.CanRead ? stream_3 : new Stream2(stream_3));
		CompressionLevel = CompressionLevel.Default;
		encryptionAlgorithm_0 = EncryptionAlgorithm.None;
		dictionary_0 = new Dictionary<string, ZipEntry>(StringComparer.Ordinal);
		zip64Option_0 = Zip64Option.Default;
		bool_0 = bool_7;
		Strategy = CompressionStrategy.Default;
		fileName = "unknown";
		ParallelDeflateThreshold = -1L;
	}

	public override string ToString()
	{
		return $"ZipOutputStream::{fileName}(leaveOpen({bool_0})))";
	}

	private void method_1(ZipEntry zipEntry_1)
	{
		if (dictionary_0.ContainsKey(zipEntry_1.FileName))
		{
			bool_2 = true;
			throw new ArgumentException($"The entry '{zipEntry_1.FileName}' already exists in the zip archive.");
		}
	}

	public bool ContainsEntry(string name)
	{
		return dictionary_0.ContainsKey(Class7.smethod_3(name));
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (bool_1)
		{
			bool_2 = true;
			throw new InvalidOperationException("The stream has been closed.");
		}
		if (zipEntry_0 == null)
		{
			bool_2 = true;
			throw new InvalidOperationException("You must call PutNextEntry() before calling Write().");
		}
		if (zipEntry_0.IsDirectory)
		{
			bool_2 = true;
			throw new InvalidOperationException("You cannot Write() data for an entry that is a directory.");
		}
		if (bool_5)
		{
			method_2(bool_7: false);
		}
		crcCalculatorStream_0.Write(buffer, offset, count);
	}

	public ZipEntry PutNextEntry(string entryName)
	{
		if (bool_1)
		{
			bool_2 = true;
			throw new InvalidOperationException("The stream has been closed.");
		}
		method_3();
		zipEntry_0 = ZipEntry.smethod_6(entryName);
		zipEntry_0.class31_0 = new Class31(this);
		zipEntry_0.short_1 |= 8;
		zipEntry_0.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
		zipEntry_0.CompressionLevel = CompressionLevel;
		zipEntry_0.Encryption = Encryption;
		zipEntry_0.Password = string_0;
		if (entryName.EndsWith("/"))
		{
			zipEntry_0.method_0();
		}
		zipEntry_0.EmitTimesInWindowsFormatWhenSaving = (zipEntryTimestamp_0 & ZipEntryTimestamp.Windows) != 0;
		zipEntry_0.EmitTimesInUnixFormatWhenSaving = (zipEntryTimestamp_0 & ZipEntryTimestamp.Unix) != 0;
		method_1(zipEntry_0);
		bool_5 = true;
		return zipEntry_0;
	}

	private void method_2(bool bool_7)
	{
		dictionary_0.Add(zipEntry_0.FileName, zipEntry_0);
		int_0++;
		if (int_0 > 65534 && zip64Option_0 == Zip64Option.Default)
		{
			bool_2 = true;
			throw new InvalidOperationException("Too many entries. Consider setting ZipOutputStream.EnableZip64.");
		}
		zipEntry_0.method_9(stream_0, bool_7 ? 99 : 0);
		zipEntry_0.method_24();
		if (!zipEntry_0.IsDirectory)
		{
			zipEntry_0.method_27(stream_0);
			zipEntry_0.method_19(stream_0, (!bool_7) ? (-1) : 0, out stream2_0, out stream_1, out stream_2, out crcCalculatorStream_0);
		}
		bool_5 = false;
	}

	private void method_3()
	{
		if (zipEntry_0 != null)
		{
			if (bool_5)
			{
				method_2(bool_7: true);
			}
			zipEntry_0.method_16(stream_0, stream2_0, stream_1, stream_2, crcCalculatorStream_0);
			zipEntry_0.method_17(stream_0);
			bool_3 |= zipEntry_0.OutputUsedZip64.Value;
			stream2_0 = null;
			stream_1 = (stream_2 = null);
			crcCalculatorStream_0 = null;
		}
	}

	protected override void Dispose(bool notCalledFromFinalizer)
	{
		if (bool_1)
		{
			return;
		}
		if (notCalledFromFinalizer && !bool_2)
		{
			method_3();
			bool_4 = Class5.smethod_0(stream_0, dictionary_0.Values, 1u, zip64Option_0, Comment, ProvisionalAlternateEncoding);
			Stream stream = null;
			if (stream_0 is Stream2 stream2)
			{
				stream = stream2.method_0();
				stream2.Dispose();
			}
			else
			{
				stream = stream_0;
			}
			if (!bool_0)
			{
				stream.Dispose();
			}
			stream_0 = null;
		}
		bool_1 = true;
	}

	public override void Flush()
	{
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException("Read");
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotSupportedException("Seek");
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException();
	}
}
