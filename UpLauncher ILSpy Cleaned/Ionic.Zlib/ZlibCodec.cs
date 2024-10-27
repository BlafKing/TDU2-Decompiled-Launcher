using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib;

[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
public sealed class ZlibCodec
{
	public byte[] InputBuffer;

	public int NextIn;

	public int AvailableBytesIn;

	public long TotalBytesIn;

	public byte[] OutputBuffer;

	public int NextOut;

	public int AvailableBytesOut;

	public long TotalBytesOut;

	public string Message;

	internal Class28 class28_0;

	internal Class11 class11_0;

	internal uint uint_0;

	public CompressionLevel CompressLevel = CompressionLevel.Default;

	public int WindowBits = 15;

	public CompressionStrategy Strategy;

	public int Adler32 => (int)uint_0;

	public ZlibCodec()
	{
	}

	public ZlibCodec(CompressionMode mode)
	{
		switch (mode)
		{
		case CompressionMode.Compress:
			if (InitializeDeflate() != 0)
			{
				throw new ZlibException("Cannot initialize for deflate.");
			}
			break;
		case CompressionMode.Decompress:
			if (InitializeInflate() != 0)
			{
				throw new ZlibException("Cannot initialize for inflate.");
			}
			break;
		default:
			throw new ZlibException("Invalid ZlibStreamFlavor.");
		}
	}

	public int InitializeInflate()
	{
		return InitializeInflate(WindowBits);
	}

	public int InitializeInflate(bool expectRfc1950Header)
	{
		return InitializeInflate(WindowBits, expectRfc1950Header);
	}

	public int InitializeInflate(int windowBits)
	{
		WindowBits = windowBits;
		return InitializeInflate(windowBits, expectRfc1950Header: true);
	}

	public int InitializeInflate(int windowBits, bool expectRfc1950Header)
	{
		WindowBits = windowBits;
		if (class28_0 != null)
		{
			throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
		}
		class11_0 = new Class11(expectRfc1950Header);
		return class11_0.method_4(this, windowBits);
	}

	public int Inflate(FlushType flush)
	{
		if (class11_0 == null)
		{
			throw new ZlibException("No Inflate State!");
		}
		return class11_0.method_5(flush);
	}

	public int EndInflate()
	{
		if (class11_0 == null)
		{
			throw new ZlibException("No Inflate State!");
		}
		int result = class11_0.method_3();
		class11_0 = null;
		return result;
	}

	public int SyncInflate()
	{
		if (class11_0 == null)
		{
			throw new ZlibException("No Inflate State!");
		}
		return class11_0.method_7();
	}

	public int InitializeDeflate()
	{
		return method_0(bool_0: true);
	}

	public int InitializeDeflate(CompressionLevel level)
	{
		CompressLevel = level;
		return method_0(bool_0: true);
	}

	public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
	{
		CompressLevel = level;
		return method_0(wantRfc1950Header);
	}

	public int InitializeDeflate(CompressionLevel level, int bits)
	{
		CompressLevel = level;
		WindowBits = bits;
		return method_0(bool_0: true);
	}

	public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
	{
		CompressLevel = level;
		WindowBits = bits;
		return method_0(wantRfc1950Header);
	}

	private int method_0(bool bool_0)
	{
		if (class11_0 != null)
		{
			throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
		}
		class28_0 = new Class28();
		class28_0.method_27(bool_0);
		return class28_0.method_30(this, CompressLevel, WindowBits, Strategy);
	}

	public int Deflate(FlushType flush)
	{
		if (class28_0 == null)
		{
			throw new ZlibException("No Deflate State!");
		}
		return class28_0.method_37(flush);
	}

	public int EndDeflate()
	{
		if (class28_0 == null)
		{
			throw new ZlibException("No Deflate State!");
		}
		class28_0 = null;
		return 0;
	}

	public void ResetDeflate()
	{
		if (class28_0 == null)
		{
			throw new ZlibException("No Deflate State!");
		}
		class28_0.method_32();
	}

	public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
	{
		if (class28_0 == null)
		{
			throw new ZlibException("No Deflate State!");
		}
		return class28_0.method_35(level, strategy);
	}

	public int SetDictionary(byte[] dictionary)
	{
		if (class11_0 != null)
		{
			return class11_0.method_6(dictionary);
		}
		if (class28_0 == null)
		{
			throw new ZlibException("No Inflate or Deflate state!");
		}
		return class28_0.method_36(dictionary);
	}

	internal void method_1()
	{
		int num = class28_0.int_21;
		if (num > AvailableBytesOut)
		{
			num = AvailableBytesOut;
		}
		if (num != 0)
		{
			if (class28_0.byte_0.Length <= class28_0.int_20 || OutputBuffer.Length <= NextOut || class28_0.byte_0.Length < class28_0.int_20 + num || OutputBuffer.Length < NextOut + num)
			{
				throw new ZlibException($"Invalid State. (pending.Length={class28_0.byte_0.Length}, pendingCount={class28_0.int_21})");
			}
			Array.Copy(class28_0.byte_0, class28_0.int_20, OutputBuffer, NextOut, num);
			NextOut += num;
			class28_0.int_20 += num;
			TotalBytesOut += num;
			AvailableBytesOut -= num;
			class28_0.int_21 -= num;
			if (class28_0.int_21 == 0)
			{
				class28_0.int_20 = 0;
			}
		}
	}

	internal int method_2(byte[] byte_0, int int_0, int int_1)
	{
		int num = AvailableBytesIn;
		if (num > int_1)
		{
			num = int_1;
		}
		if (num == 0)
		{
			return 0;
		}
		AvailableBytesIn -= num;
		if (class28_0.method_26())
		{
			uint_0 = Class15.smethod_0(uint_0, InputBuffer, NextIn, num);
		}
		Array.Copy(InputBuffer, NextIn, byte_0, int_0, num);
		NextIn += num;
		TotalBytesIn += num;
		return num;
	}
}
