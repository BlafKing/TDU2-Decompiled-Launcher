using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Zlib;

[ComVisible(true)]
[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class CRC32
{
	private const int int_0 = 8192;

	private long long_0;

	private static readonly uint[] uint_0;

	private uint uint_1 = uint.MaxValue;

	public long TotalBytesRead => long_0;

	public int Crc32Result => (int)(~uint_1);

	public int GetCrc32(Stream input)
	{
		return GetCrc32AndCopy(input, null);
	}

	public int GetCrc32AndCopy(Stream input, Stream output)
	{
		if (input == null)
		{
			throw new ZlibException("The input stream must not be null.");
		}
		byte[] array = new byte[8192];
		int count = 8192;
		long_0 = 0L;
		int num = input.Read(array, 0, 8192);
		output?.Write(array, 0, num);
		long_0 += num;
		while (num > 0)
		{
			SlurpBlock(array, 0, num);
			num = input.Read(array, 0, count);
			output?.Write(array, 0, num);
			long_0 += num;
		}
		return (int)(~uint_1);
	}

	public int ComputeCrc32(int W, byte B)
	{
		return method_0((uint)W, B);
	}

	internal int method_0(uint uint_2, byte byte_0)
	{
		return (int)(uint_0[(uint_2 ^ byte_0) & 0xFF] ^ (uint_2 >> 8));
	}

	public void SlurpBlock(byte[] block, int offset, int count)
	{
		if (block == null)
		{
			throw new ZlibException("The data buffer must not be null.");
		}
		for (int i = 0; i < count; i++)
		{
			int num = offset + i;
			uint_1 = (uint_1 >> 8) ^ uint_0[block[num] ^ (uint_1 & 0xFF)];
		}
		long_0 += count;
	}

	static CRC32()
	{
		uint num = 3988292384u;
		uint_0 = new uint[256];
		for (uint num2 = 0u; num2 < 256; num2++)
		{
			uint num3 = num2;
			for (uint num4 = 8u; num4 != 0; num4--)
			{
				num3 = (((num3 & 1) != 1) ? (num3 >> 1) : ((num3 >> 1) ^ num));
			}
			uint_0[num2] = num3;
		}
	}

	private uint method_1(uint[] uint_2, uint uint_3)
	{
		uint num = 0u;
		int num2 = 0;
		while (uint_3 != 0)
		{
			if ((uint_3 & 1) == 1)
			{
				num ^= uint_2[num2];
			}
			uint_3 >>= 1;
			num2++;
		}
		return num;
	}

	private void method_2(uint[] uint_2, uint[] uint_3)
	{
		for (int i = 0; i < 32; i++)
		{
			uint_2[i] = method_1(uint_3, uint_3[i]);
		}
	}

	public void Combine(int int_1, int length)
	{
		uint[] array = new uint[32];
		uint[] array2 = new uint[32];
		if (length == 0)
		{
			return;
		}
		uint num = ~uint_1;
		array2[0] = 3988292384u;
		uint num2 = 1u;
		for (int i = 1; i < 32; i++)
		{
			array2[i] = num2;
			num2 <<= 1;
		}
		method_2(array, array2);
		method_2(array2, array);
		uint num3 = (uint)length;
		do
		{
			method_2(array, array2);
			if ((num3 & 1) == 1)
			{
				num = method_1(array, num);
			}
			num3 >>= 1;
			if (num3 == 0)
			{
				break;
			}
			method_2(array2, array);
			if ((num3 & 1) == 1)
			{
				num = method_1(array2, num);
			}
			num3 >>= 1;
		}
		while (num3 != 0);
		num ^= (uint)int_1;
		uint_1 = ~num;
	}
}
