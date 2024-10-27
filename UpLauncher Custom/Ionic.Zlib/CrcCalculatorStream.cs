using System;
using System.IO;

namespace Ionic.Zlib;

public class CrcCalculatorStream : Stream, IDisposable
{
	private static readonly long long_0 = -99L;

	internal Stream stream_0;

	private CRC32 crc32_0;

	private long long_1 = -99L;

	private bool bool_0;

	public long TotalBytesSlurped => crc32_0.TotalBytesRead;

	public int Crc => crc32_0.Crc32Result;

	public bool LeaveOpen
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
		}
	}

	public override bool CanRead => stream_0.CanRead;

	public override bool CanSeek => stream_0.CanSeek;

	public override bool CanWrite => stream_0.CanWrite;

	public override long Length
	{
		get
		{
			if (long_1 == long_0)
			{
				return stream_0.Length;
			}
			return long_1;
		}
	}

	public override long Position
	{
		get
		{
			return crc32_0.TotalBytesRead;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public CrcCalculatorStream(Stream stream)
		: this(bool_1: true, long_0, stream)
	{
	}

	public CrcCalculatorStream(Stream stream, bool leaveOpen)
		: this(leaveOpen, long_0, stream)
	{
	}

	public CrcCalculatorStream(Stream stream, long length)
		: this(bool_1: true, length, stream)
	{
		if (length < 0L)
		{
			throw new ArgumentException("length");
		}
	}

	public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
		: this(leaveOpen, length, stream)
	{
		if (length < 0L)
		{
			throw new ArgumentException("length");
		}
	}

	private CrcCalculatorStream(bool bool_1, long long_2, Stream stream_1)
	{
		stream_0 = stream_1;
		crc32_0 = new CRC32();
		long_1 = long_2;
		bool_0 = bool_1;
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		int count2 = count;
		if (long_1 != long_0)
		{
			if (crc32_0.TotalBytesRead >= long_1)
			{
				return 0;
			}
			long num = long_1 - crc32_0.TotalBytesRead;
			if (num < count)
			{
				count2 = (int)num;
			}
		}
		int num2 = stream_0.Read(buffer, offset, count2);
		if (num2 > 0)
		{
			crc32_0.SlurpBlock(buffer, offset, num2);
		}
		return num2;
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (count > 0)
		{
			crc32_0.SlurpBlock(buffer, offset, count);
		}
		stream_0.Write(buffer, offset, count);
	}

	public override void Flush()
	{
		stream_0.Flush();
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}

	void IDisposable.Dispose()
	{
		Close();
	}

	public override void Close()
	{
		base.Close();
		if (!bool_0)
		{
			stream_0.Close();
		}
	}
}
