using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip;

public class ZipInputStream : Stream
{
	private Stream stream_0;

	private Encoding encoding_0;

	private ZipEntry zipEntry_0;

	private bool bool_0;

	private bool bool_1;

	private Class31 class31_0;

	private CrcCalculatorStream crcCalculatorStream_0;

	private long long_0;

	private string string_0;

	private long long_1;

	private string string_1;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private bool bool_5;

	[CompilerGenerated]
	private int int_0;

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

	public int CodecBufferSize
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public string Password
	{
		set
		{
			if (bool_3)
			{
				bool_5 = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			string_0 = value;
		}
	}

	internal Stream ReadStream => stream_0;

	public override bool CanRead => true;

	public override bool CanSeek => stream_0.CanSeek;

	public override bool CanWrite => false;

	public override long Length => stream_0.Length;

	public override long Position
	{
		get
		{
			return stream_0.Position;
		}
		set
		{
			Seek(value, SeekOrigin.Begin);
		}
	}

	public ZipInputStream(Stream stream)
		: this(stream, leaveOpen: false)
	{
	}

	public ZipInputStream(string fileName)
	{
		Stream stream_ = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		method_0(stream_, bool_6: false, fileName);
	}

	public ZipInputStream(Stream stream, bool leaveOpen)
	{
		method_0(stream, leaveOpen, null);
	}

	private void method_0(Stream stream_1, bool bool_6, string string_2)
	{
		stream_0 = stream_1;
		if (!stream_0.CanRead)
		{
			throw new ZipException("The stream must be readable.");
		}
		class31_0 = new Class31(this);
		encoding_0 = Encoding.GetEncoding("IBM437");
		bool_2 = bool_6;
		bool_4 = true;
		string_1 = string_2 ?? "(stream)";
	}

	public override string ToString()
	{
		return $"ZipInputStream::{string_1}(leaveOpen({bool_2})))";
	}

	private void method_1()
	{
		crcCalculatorStream_0 = zipEntry_0.method_33(string_0);
		long_0 = crcCalculatorStream_0.Length;
		bool_1 = false;
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (bool_3)
		{
			bool_5 = true;
			throw new InvalidOperationException("The stream has been closed.");
		}
		if (bool_1)
		{
			method_1();
		}
		if (long_0 == 0L)
		{
			return 0;
		}
		int count2 = (int)((long_0 > count) ? count : long_0);
		int num = crcCalculatorStream_0.Read(buffer, offset, count2);
		long_0 -= num;
		if (long_0 == 0L)
		{
			int crc = crcCalculatorStream_0.Crc;
			zipEntry_0.method_40(crc);
			stream_0.Seek(long_1, SeekOrigin.Begin);
		}
		return num;
	}

	public ZipEntry GetNextEntry()
	{
		if (bool_4)
		{
			long num = Class7.smethod_12(stream_0, 67324752);
			if (num == -1L)
			{
				return null;
			}
			stream_0.Seek(-4L, SeekOrigin.Current);
		}
		zipEntry_0 = ZipEntry.smethod_15(class31_0, !bool_0);
		long_1 = stream_0.Position;
		bool_0 = true;
		bool_1 = true;
		bool_4 = false;
		return zipEntry_0;
	}

	protected override void Dispose(bool notCalledFromFinalizer)
	{
		if (bool_3)
		{
			return;
		}
		if (notCalledFromFinalizer)
		{
			if (bool_5)
			{
				return;
			}
			if (!bool_2)
			{
				stream_0.Dispose();
			}
		}
		bool_3 = true;
	}

	public override void Flush()
	{
		throw new NotSupportedException("Flush");
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException("Write");
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		bool_4 = true;
		return stream_0.Seek(offset, origin);
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException();
	}
}
