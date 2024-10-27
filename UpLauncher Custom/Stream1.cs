using System;
using System.IO;

internal class Stream1 : Stream
{
	private Class6 class6_0;

	private Stream stream_0;

	private Enum5 enum5_0;

    public override bool CanRead => enum5_0 == Enum5.const_1;

    public override bool CanSeek => false;

    public override bool CanWrite => enum5_0 == Enum5.const_0;

    public override long Length
	{
		get
		{
			throw new NotImplementedException();
		}
	}

    public override long Position
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public Stream1(Stream stream_1, Class6 class6_1, Enum5 enum5_1)
	{
		class6_0 = class6_1;
		stream_0 = stream_1;
		enum5_0 = enum5_1;
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (enum5_0 == Enum5.const_0)
		{
			throw new NotImplementedException();
		}
		byte[] array = new byte[count];
		int num = stream_0.Read(array, 0, count);
		byte[] array2 = class6_0.method_1(array, num);
		for (int i = 0; i < num; i++)
		{
			buffer[offset + i] = array2[i];
		}
		return num;
	}

    public override void Write(byte[] buffer, int offset, int count)
	{
		if (enum5_0 == Enum5.const_1)
		{
			throw new NotImplementedException();
		}
		if (count == 0)
		{
			return;
		}
		byte[] array = null;
		if (offset != 0)
		{
			array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = buffer[offset + i];
			}
		}
		else
		{
			array = buffer;
		}
		byte[] array2 = class6_0.method_2(array, count);
		stream_0.Write(array2, 0, array2.Length);
	}

    public override void Flush()
	{
	}

    public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}
}
