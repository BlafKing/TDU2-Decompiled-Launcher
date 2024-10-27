using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zlib;

internal class Stream3 : Stream
{
	internal enum Enum7
	{
		const_0,
		const_1,
		const_2
	}

	protected internal ZlibCodec zlibCodec_0;

	protected internal Enum7 enum7_0 = Enum7.const_2;

	protected internal FlushType flushType_0;

	protected internal Enum6 enum6_0;

	protected internal CompressionMode compressionMode_0;

	protected internal CompressionLevel compressionLevel_0;

	protected internal bool bool_0;

	protected internal byte[] byte_0;

	protected internal int int_0 = 16384;

	protected internal byte[] byte_1 = new byte[1];

	protected internal Stream stream_0;

	protected internal CompressionStrategy compressionStrategy_0;

	private CRC32 crc32_0;

	protected internal string string_0;

	protected internal string string_1;

	protected internal DateTime dateTime_0;

	protected internal int int_1;

	private bool bool_1;

    public override bool CanRead => stream_0.CanRead;

    public override bool CanSeek => stream_0.CanSeek;

    public override bool CanWrite => stream_0.CanWrite;

    public override long Length => stream_0.Length;

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

	[SpecialName]
	internal int method_0()
	{
		if (crc32_0 == null)
		{
			return 0;
		}
		return crc32_0.Crc32Result;
	}

	public Stream3(Stream stream_1, CompressionMode compressionMode_1, CompressionLevel compressionLevel_1, Enum6 enum6_1, bool bool_2)
	{
		flushType_0 = FlushType.None;
		stream_0 = stream_1;
		bool_0 = bool_2;
		compressionMode_0 = compressionMode_1;
		enum6_0 = enum6_1;
		compressionLevel_0 = compressionLevel_1;
		if (enum6_1 == Enum6.const_2)
		{
			crc32_0 = new CRC32();
		}
	}

	[SpecialName]
	protected internal bool method_1()
	{
		return compressionMode_0 == CompressionMode.Compress;
	}

	[SpecialName]
	private ZlibCodec method_2()
	{
		if (zlibCodec_0 == null)
		{
			bool flag = enum6_0 == Enum6.const_0;
			zlibCodec_0 = new ZlibCodec();
			if (compressionMode_0 == CompressionMode.Decompress)
			{
				zlibCodec_0.InitializeInflate(flag);
			}
			else
			{
				zlibCodec_0.Strategy = compressionStrategy_0;
				zlibCodec_0.InitializeDeflate(compressionLevel_0, flag);
			}
		}
		return zlibCodec_0;
	}

	[SpecialName]
	private byte[] method_3()
	{
		if (byte_0 == null)
		{
			byte_0 = new byte[int_0];
		}
		return byte_0;
	}

    public override void Write(byte[] buffer, int offset, int count)
	{
		if (crc32_0 != null)
		{
			crc32_0.SlurpBlock(buffer, offset, count);
		}
		if (enum7_0 == Enum7.const_2)
		{
			enum7_0 = Enum7.const_0;
		}
		else if (enum7_0 != 0)
		{
			throw new ZlibException("Cannot Write after Reading.");
		}
		if (count == 0)
		{
			return;
		}
		method_2().InputBuffer = buffer;
		zlibCodec_0.NextIn = offset;
		zlibCodec_0.AvailableBytesIn = count;
		bool flag = false;
		while (true)
		{
			zlibCodec_0.OutputBuffer = method_3();
			zlibCodec_0.NextOut = 0;
			zlibCodec_0.AvailableBytesOut = byte_0.Length;
			int num = ((!method_1()) ? zlibCodec_0.Inflate(flushType_0) : zlibCodec_0.Deflate(flushType_0));
			if (num != 0 && num != 1)
			{
				break;
			}
			stream_0.Write(byte_0, 0, byte_0.Length - zlibCodec_0.AvailableBytesOut);
			flag = zlibCodec_0.AvailableBytesIn == 0 && zlibCodec_0.AvailableBytesOut != 0;
			if (enum6_0 == Enum6.const_2 && !method_1())
			{
				flag = zlibCodec_0.AvailableBytesIn == 8 && zlibCodec_0.AvailableBytesOut != 0;
			}
			if (flag)
			{
				return;
			}
		}
		throw new ZlibException((method_1() ? "de" : "in") + "flating: " + zlibCodec_0.Message);
	}

	private void method_4()
	{
		if (zlibCodec_0 == null)
		{
			return;
		}
		if (enum7_0 == Enum7.const_0)
		{
			bool flag = false;
			do
			{
				zlibCodec_0.OutputBuffer = method_3();
				zlibCodec_0.NextOut = 0;
				zlibCodec_0.AvailableBytesOut = byte_0.Length;
				int num = ((!method_1()) ? zlibCodec_0.Inflate(FlushType.Finish) : zlibCodec_0.Deflate(FlushType.Finish));
				if (num == 1 || num == 0)
				{
					if (byte_0.Length - zlibCodec_0.AvailableBytesOut > 0)
					{
						stream_0.Write(byte_0, 0, byte_0.Length - zlibCodec_0.AvailableBytesOut);
					}
					flag = zlibCodec_0.AvailableBytesIn == 0 && zlibCodec_0.AvailableBytesOut != 0;
					if (enum6_0 == Enum6.const_2 && !method_1())
					{
						flag = zlibCodec_0.AvailableBytesIn == 8 && zlibCodec_0.AvailableBytesOut != 0;
					}
					continue;
				}
				string text = (method_1() ? "de" : "in") + "flating";
				if (zlibCodec_0.Message == null)
				{
					throw new ZlibException($"{text}: (rc = {num})");
				}
				throw new ZlibException(text + ": " + zlibCodec_0.Message);
			}
			while (!flag);
			Flush();
			if (enum6_0 == Enum6.const_2)
			{
				if (!method_1())
				{
					throw new ZlibException("Writing with decompression is not supported.");
				}
				int crc32Result = crc32_0.Crc32Result;
				stream_0.Write(BitConverter.GetBytes(crc32Result), 0, 4);
				int value = (int)(crc32_0.TotalBytesRead & 0xFFFFFFFFL);
				stream_0.Write(BitConverter.GetBytes(value), 0, 4);
			}
		}
		else
		{
			if (enum7_0 != Enum7.const_1 || enum6_0 != Enum6.const_2)
			{
				return;
			}
			if (method_1())
			{
				throw new ZlibException("Reading with compression is not supported.");
			}
			if (zlibCodec_0.TotalBytesOut == 0L)
			{
				return;
			}
			byte[] array = new byte[8];
			if (zlibCodec_0.AvailableBytesIn != 8)
			{
				Array.Copy(zlibCodec_0.InputBuffer, zlibCodec_0.NextIn, array, 0, zlibCodec_0.AvailableBytesIn);
				int num2 = 8 - zlibCodec_0.AvailableBytesIn;
				int num3 = stream_0.Read(array, zlibCodec_0.AvailableBytesIn, num2);
				if (num2 != num3)
				{
					throw new ZlibException($"Protocol error. AvailableBytesIn={zlibCodec_0.AvailableBytesIn + num3}, expected 8");
				}
			}
			else
			{
				Array.Copy(zlibCodec_0.InputBuffer, zlibCodec_0.NextIn, array, 0, array.Length);
			}
			int num4 = BitConverter.ToInt32(array, 0);
			int crc32Result2 = crc32_0.Crc32Result;
			int num5 = BitConverter.ToInt32(array, 4);
			int num6 = (int)(zlibCodec_0.TotalBytesOut & 0xFFFFFFFFL);
			if (crc32Result2 != num4)
			{
				throw new ZlibException($"Bad CRC32 in GZIP stream. (actual({crc32Result2:X8})!=expected({num4:X8}))");
			}
			if (num6 != num5)
			{
				throw new ZlibException($"Bad size in GZIP stream. (actual({num6})!=expected({num5}))");
			}
		}
	}

	private void method_5()
	{
		if (method_2() != null)
		{
			if (method_1())
			{
				zlibCodec_0.EndDeflate();
			}
			else
			{
				zlibCodec_0.EndInflate();
			}
			zlibCodec_0 = null;
		}
	}

    public override void Close()
	{
		if (stream_0 == null)
		{
			return;
		}
		try
		{
			method_4();
		}
		finally
		{
			method_5();
			if (!bool_0)
			{
				stream_0.Close();
			}
			stream_0 = null;
		}
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
		stream_0.SetLength(value);
	}

	private string method_6()
	{
		List<byte> list = new List<byte>();
		bool flag = false;
		do
		{
			int num = stream_0.Read(byte_1, 0, 1);
			if (num == 1)
			{
				if (byte_1[0] == 0)
				{
					flag = true;
				}
				else
				{
					list.Add(byte_1[0]);
				}
				continue;
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
		}
		while (!flag);
		byte[] array = list.ToArray();
		return GZipStream.encoding_0.GetString(array, 0, array.Length);
	}

	private int method_7()
	{
		int num = 0;
		byte[] array = new byte[10];
		int num2 = stream_0.Read(array, 0, array.Length);
		switch (num2)
		{
		case 0:
			return 0;
		default:
			throw new ZlibException("Not a valid GZIP stream.");
		case 10:
			if (array[0] == 31 && array[1] == 139 && array[2] == 8)
			{
				int num3 = BitConverter.ToInt32(array, 4);
				dateTime_0 = GZipStream.dateTime_0.AddSeconds(num3);
				num += num2;
				if ((array[3] & 4) == 4)
				{
					num2 = stream_0.Read(array, 0, 2);
					num += num2;
					short num4 = (short)(array[0] + array[1] * 256);
					byte[] array2 = new byte[num4];
					num2 = stream_0.Read(array2, 0, array2.Length);
					if (num2 != num4)
					{
						throw new ZlibException("Unexpected end-of-file reading GZIP header.");
					}
					num += num2;
				}
				if ((array[3] & 8) == 8)
				{
					string_0 = method_6();
				}
				if ((array[3] & 0x10) == 16)
				{
					string_1 = method_6();
				}
				if ((array[3] & 2) == 2)
				{
					Read(byte_1, 0, 1);
				}
				return num;
			}
			throw new ZlibException("Bad GZIP header.");
		}
	}

    public override int Read(byte[] buffer, int offset, int count)
	{
		if (enum7_0 == Enum7.const_2)
		{
			if (!stream_0.CanRead)
			{
				throw new ZlibException("The stream is not readable.");
			}
			enum7_0 = Enum7.const_1;
			method_2().AvailableBytesIn = 0;
			if (enum6_0 == Enum6.const_2)
			{
				int_1 = method_7();
				if (int_1 == 0)
				{
					return 0;
				}
			}
		}
		if (enum7_0 != Enum7.const_1)
		{
			throw new ZlibException("Cannot Read after Writing.");
		}
		if (count == 0)
		{
			return 0;
		}
		if (bool_1 && method_1())
		{
			return 0;
		}
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException("count");
		}
		if (offset < buffer.GetLowerBound(0))
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		if (offset + count > buffer.GetLength(0))
		{
			throw new ArgumentOutOfRangeException("count");
		}
		int num = 0;
		zlibCodec_0.OutputBuffer = buffer;
		zlibCodec_0.NextOut = offset;
		zlibCodec_0.AvailableBytesOut = count;
		zlibCodec_0.InputBuffer = method_3();
		do
		{
			if (zlibCodec_0.AvailableBytesIn == 0 && !bool_1)
			{
				zlibCodec_0.NextIn = 0;
				zlibCodec_0.AvailableBytesIn = stream_0.Read(byte_0, 0, byte_0.Length);
				if (zlibCodec_0.AvailableBytesIn == 0)
				{
					bool_1 = true;
				}
			}
			num = (method_1() ? zlibCodec_0.Deflate(flushType_0) : zlibCodec_0.Inflate(flushType_0));
			if (!bool_1 || num != -5)
			{
				if (num != 0 && num != 1)
				{
					throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", method_1() ? "de" : "in", num, zlibCodec_0.Message));
				}
				continue;
			}
			return 0;
		}
		while (((!bool_1 && num != 1) || zlibCodec_0.AvailableBytesOut != count) && zlibCodec_0.AvailableBytesOut > 0 && !bool_1 && num == 0);
		if (zlibCodec_0.AvailableBytesOut > 0)
		{
			if (num != 0)
			{
			}
			if (bool_1 && method_1())
			{
				num = zlibCodec_0.Deflate(FlushType.Finish);
				if (num != 0 && num != 1)
				{
					throw new ZlibException($"Deflating:  rc={num}  msg={zlibCodec_0.Message}");
				}
			}
		}
		num = count - zlibCodec_0.AvailableBytesOut;
		if (crc32_0 != null)
		{
			crc32_0.SlurpBlock(buffer, offset, num);
		}
		return num;
	}

	public static void smethod_0(string string_2, Stream stream_1)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(string_2);
		using (stream_1)
		{
			stream_1.Write(bytes, 0, bytes.Length);
		}
	}

	public static void smethod_1(byte[] byte_2, Stream stream_1)
	{
		using (stream_1)
		{
			stream_1.Write(byte_2, 0, byte_2.Length);
		}
	}

	public static string smethod_2(byte[] byte_2, Stream stream_1)
	{
		byte[] array = new byte[1024];
		Encoding uTF = Encoding.UTF8;
		using MemoryStream memoryStream = new MemoryStream();
		using (stream_1)
		{
			int count;
			while ((count = stream_1.Read(array, 0, array.Length)) != 0)
			{
				memoryStream.Write(array, 0, count);
			}
		}
		memoryStream.Seek(0L, SeekOrigin.Begin);
		StreamReader streamReader = new StreamReader(memoryStream, uTF);
		return streamReader.ReadToEnd();
	}

	public static byte[] smethod_3(byte[] byte_2, Stream stream_1)
	{
		byte[] array = new byte[1024];
		using MemoryStream memoryStream = new MemoryStream();
		using (stream_1)
		{
			int count;
			while ((count = stream_1.Read(array, 0, array.Length)) != 0)
			{
				memoryStream.Write(array, 0, count);
			}
		}
		return memoryStream.ToArray();
	}
}
