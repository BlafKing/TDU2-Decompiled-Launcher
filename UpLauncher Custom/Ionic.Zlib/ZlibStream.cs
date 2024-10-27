using System;
using System.IO;

namespace Ionic.Zlib;

public class ZlibStream : Stream
{
	internal Stream3 stream3_0;

	private bool bool_0;

	public virtual FlushType FlushMode
	{
		get
		{
			return stream3_0.flushType_0;
		}
		set
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			stream3_0.flushType_0 = value;
		}
	}

	public int BufferSize
	{
		get
		{
			return stream3_0.int_0;
		}
		set
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			if (stream3_0.byte_0 != null)
			{
				throw new ZlibException("The working buffer is already set.");
			}
			if (value < 1024)
			{
				throw new ZlibException($"Don't be silly. {value} bytes?? Use a bigger buffer, at least {1024}.");
			}
			stream3_0.int_0 = value;
		}
	}

	public virtual long TotalIn => stream3_0.zlibCodec_0.TotalBytesIn;

	public virtual long TotalOut => stream3_0.zlibCodec_0.TotalBytesOut;

	public override bool CanRead
	{
		get
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return stream3_0.stream_0.CanRead;
		}
	}

	public override bool CanSeek => false;

	public override bool CanWrite
	{
		get
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return stream3_0.stream_0.CanWrite;
		}
	}

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
			if (stream3_0.enum7_0 == Stream3.Enum7.const_0)
			{
				return stream3_0.zlibCodec_0.TotalBytesOut;
			}
			if (stream3_0.enum7_0 == Stream3.Enum7.const_1)
			{
				return stream3_0.zlibCodec_0.TotalBytesIn;
			}
			return 0L;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public ZlibStream(Stream stream, CompressionMode mode)
		: this(stream, mode, CompressionLevel.Default, leaveOpen: false)
	{
	}

	public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
		: this(stream, mode, level, leaveOpen: false)
	{
	}

	public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
		: this(stream, mode, CompressionLevel.Default, leaveOpen)
	{
	}

	public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
	{
		stream3_0 = new Stream3(stream, mode, level, Enum6.const_0, leaveOpen);
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			if (!bool_0)
			{
				if (disposing && stream3_0 != null)
				{
					stream3_0.Close();
				}
				bool_0 = true;
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	public override void Flush()
	{
		if (bool_0)
		{
			throw new ObjectDisposedException("ZlibStream");
		}
		stream3_0.Flush();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (bool_0)
		{
			throw new ObjectDisposedException("ZlibStream");
		}
		return stream3_0.Read(buffer, offset, count);
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (bool_0)
		{
			throw new ObjectDisposedException("ZlibStream");
		}
		stream3_0.Write(buffer, offset, count);
	}

	public static byte[] CompressString(string string_0)
	{
		using MemoryStream memoryStream = new MemoryStream();
		Stream stream_ = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
		Stream3.smethod_0(string_0, stream_);
		return memoryStream.ToArray();
	}

	public static byte[] CompressBuffer(byte[] byte_0)
	{
		using MemoryStream memoryStream = new MemoryStream();
		Stream stream_ = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
		Stream3.smethod_1(byte_0, stream_);
		return memoryStream.ToArray();
	}

	public static string UncompressString(byte[] compressed)
	{
		using MemoryStream stream = new MemoryStream(compressed);
		Stream stream_ = new ZlibStream(stream, CompressionMode.Decompress);
		return Stream3.smethod_2(compressed, stream_);
	}

	public static byte[] UncompressBuffer(byte[] compressed)
	{
		using MemoryStream stream = new MemoryStream(compressed);
		Stream stream_ = new ZlibStream(stream, CompressionMode.Decompress);
		return Stream3.smethod_3(compressed, stream_);
	}
}
