using System;
using System.IO;
using System.Runtime.CompilerServices;

internal class Stream2 : Stream
{
	private Stream stream_0;

	private long long_0;

	private long long_1;

	private long long_2;

    public override bool CanRead => stream_0.CanRead;

    public override bool CanSeek => stream_0.CanSeek;

    public override bool CanWrite => stream_0.CanWrite;

    public override long Length => stream_0.Length;

    public override long Position
	{
		get
		{
			return stream_0.Position;
		}
		set
		{
			stream_0.Seek(value, SeekOrigin.Begin);
		}
	}

	public Stream2(Stream stream_1)
	{
		stream_0 = stream_1;
		try
		{
			long_2 = stream_0.Position;
		}
		catch
		{
			long_2 = 0L;
		}
	}

	[SpecialName]
	public Stream method_0()
	{
		return stream_0;
	}

	[SpecialName]
	public long method_1()
	{
		return long_0;
	}

	[SpecialName]
	public long method_2()
	{
		return long_1;
	}

	public void method_3(long long_3)
	{
		long_0 -= long_3;
		if (long_0 < 0L)
		{
			throw new InvalidOperationException();
		}
		if (stream_0 is Stream2)
		{
			((Stream2)stream_0).method_3(long_3);
		}
	}

    public override int Read(byte[] buffer, int offset, int count)
	{
		int num = stream_0.Read(buffer, offset, count);
		long_1 += num;
		return num;
	}

    public override void Write(byte[] buffer, int offset, int count)
	{
		if (count != 0)
		{
			stream_0.Write(buffer, offset, count);
			long_0 += count;
		}
	}

    public override void Flush()
	{
		stream_0.Flush();
	}

	[SpecialName]
	public long method_4()
	{
		return long_2 + long_0;
	}

    public override long Seek(long offset, SeekOrigin origin)
	{
		return stream_0.Seek(offset, origin);
	}

    public override void SetLength(long value)
	{
		stream_0.SetLength(value);
	}
}
