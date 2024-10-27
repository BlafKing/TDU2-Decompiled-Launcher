using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib;

public class GZipStream : Stream
{
	public DateTime? LastModified;

	private int int_0;

	internal Stream3 stream3_0;

	private bool bool_0;

	private bool bool_1;

	private string string_0;

	private string string_1;

	private int int_1;

	internal static readonly DateTime dateTime_0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	internal static readonly Encoding encoding_0 = Encoding.GetEncoding("iso-8859-1");

	public string Comment
	{
		get
		{
			return string_1;
		}
		set
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			string_1 = value;
		}
	}

	public string FileName
	{
		get
		{
			return string_0;
		}
		set
		{
			if (bool_0)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			string_0 = value;
			if (string_0 != null)
			{
				if (string_0.IndexOf("/") != -1)
				{
					string_0 = string_0.Replace("/", "\\");
				}
				if (string_0.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (string_0.IndexOf("\\") != -1)
				{
					string_0 = Path.GetFileName(string_0);
				}
			}
		}
	}

	public int Crc32 => int_1;

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
				throw new ObjectDisposedException("GZipStream");
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
				throw new ObjectDisposedException("GZipStream");
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
				throw new ObjectDisposedException("GZipStream");
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
				throw new ObjectDisposedException("GZipStream");
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
				return stream3_0.zlibCodec_0.TotalBytesOut + int_0;
			}
			if (stream3_0.enum7_0 == Stream3.Enum7.const_1)
			{
				return stream3_0.zlibCodec_0.TotalBytesIn + stream3_0.int_1;
			}
			return 0L;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public GZipStream(Stream stream, CompressionMode mode)
		: this(stream, mode, CompressionLevel.Default, leaveOpen: false)
	{
	}

	public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level)
		: this(stream, mode, level, leaveOpen: false)
	{
	}

	public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
		: this(stream, mode, CompressionLevel.Default, leaveOpen)
	{
	}

	public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
	{
		stream3_0 = new Stream3(stream, mode, level, Enum6.const_2, leaveOpen);
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
					int_1 = stream3_0.method_0();
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
			throw new ObjectDisposedException("GZipStream");
		}
		stream3_0.Flush();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (bool_0)
		{
			throw new ObjectDisposedException("GZipStream");
		}
		int result = stream3_0.Read(buffer, offset, count);
		if (!bool_1)
		{
			bool_1 = true;
			FileName = stream3_0.string_0;
			Comment = stream3_0.string_1;
		}
		return result;
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
			throw new ObjectDisposedException("GZipStream");
		}
		if (stream3_0.enum7_0 == Stream3.Enum7.const_2)
		{
			if (!stream3_0.method_1())
			{
				throw new InvalidOperationException();
			}
			int_0 = method_0();
		}
		stream3_0.Write(buffer, offset, count);
	}

	private int method_0()
	{
		byte[] array = ((Comment == null) ? null : encoding_0.GetBytes(Comment));
		byte[] array2 = ((FileName == null) ? null : encoding_0.GetBytes(FileName));
		int num = ((Comment != null) ? (array.Length + 1) : 0);
		int num2 = ((FileName != null) ? (array2.Length + 1) : 0);
		int num3 = 10 + num + num2;
		byte[] array3 = new byte[num3];
		int num4 = 0;
		num4 = 1;
		array3[0] = 31;
		num4 = 2;
		array3[1] = 139;
		num4 = 3;
		array3[2] = 8;
		byte b = 0;
		if (Comment != null)
		{
			b = (byte)(b ^ 0x10u);
		}
		if (FileName != null)
		{
			b = (byte)(b ^ 8u);
		}
		array3[num4++] = b;
		if (!LastModified.HasValue)
		{
			LastModified = DateTime.Now;
		}
		int value = (int)(LastModified.Value - dateTime_0).TotalSeconds;
		Array.Copy(BitConverter.GetBytes(value), 0, array3, num4, 4);
		num4 += 4;
		array3[num4++] = 0;
		array3[num4++] = byte.MaxValue;
		if (num2 != 0)
		{
			Array.Copy(array2, 0, array3, num4, num2 - 1);
			num4 += num2 - 1;
			array3[num4++] = 0;
		}
		if (num != 0)
		{
			Array.Copy(array, 0, array3, num4, num - 1);
			num4 += num - 1;
			array3[num4++] = 0;
		}
		stream3_0.stream_0.Write(array3, 0, array3.Length);
		return array3.Length;
	}

	public static byte[] CompressString(string string_2)
	{
		using MemoryStream memoryStream = new MemoryStream();
		Stream stream_ = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
		Stream3.smethod_0(string_2, stream_);
		return memoryStream.ToArray();
	}

	public static byte[] CompressBuffer(byte[] byte_0)
	{
		using MemoryStream memoryStream = new MemoryStream();
		Stream stream_ = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
		Stream3.smethod_1(byte_0, stream_);
		return memoryStream.ToArray();
	}

	public static string UncompressString(byte[] compressed)
	{
		using MemoryStream stream = new MemoryStream(compressed);
		Stream stream_ = new GZipStream(stream, CompressionMode.Decompress);
		return Stream3.smethod_2(compressed, stream_);
	}

	public static byte[] UncompressBuffer(byte[] compressed)
	{
		using MemoryStream stream = new MemoryStream(compressed);
		Stream stream_ = new GZipStream(stream, CompressionMode.Decompress);
		return Stream3.smethod_3(compressed, stream_);
	}
}
