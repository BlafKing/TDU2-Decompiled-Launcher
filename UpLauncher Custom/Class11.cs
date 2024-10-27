using System.Runtime.CompilerServices;
using Ionic.Zlib;

internal sealed class Class11
{
	private enum Enum9
	{
		const_0,
		const_1,
		const_2,
		const_3,
		const_4,
		const_5,
		const_6,
		const_7,
		const_8,
		const_9,
		const_10,
		const_11,
		const_12,
		const_13
	}

	private const int int_0 = 32;

	private const int int_1 = 8;

	private Enum9 enum9_0;

	internal ZlibCodec zlibCodec_0;

	internal int int_2;

	internal uint uint_0;

	internal uint uint_1;

	internal int int_3;

	private bool bool_0 = true;

	internal int int_4;

	internal Class8 class8_0;

	private static readonly byte[] byte_0 = new byte[4] { 0, 0, 255, 255 };

	[SpecialName]
	internal bool method_0()
	{
		return bool_0;
	}

	[SpecialName]
	internal void method_1(bool bool_1)
	{
		bool_0 = bool_1;
	}

	public Class11()
	{
	}

	public Class11(bool bool_1)
	{
		bool_0 = bool_1;
	}

	internal int method_2()
	{
		ZlibCodec zlibCodec = zlibCodec_0;
		zlibCodec_0.TotalBytesOut = 0L;
		zlibCodec.TotalBytesIn = 0L;
		zlibCodec_0.Message = null;
		enum9_0 = ((!method_0()) ? Enum9.const_7 : Enum9.const_0);
		class8_0.method_0();
		return 0;
	}

	internal int method_3()
	{
		if (class8_0 != null)
		{
			class8_0.method_2();
		}
		class8_0 = null;
		return 0;
	}

	internal int method_4(ZlibCodec zlibCodec_1, int int_5)
	{
		zlibCodec_0 = zlibCodec_1;
		zlibCodec_0.Message = null;
		class8_0 = null;
		if (int_5 >= 8 && int_5 <= 15)
		{
			int_4 = int_5;
			class8_0 = new Class8(zlibCodec_1, method_0() ? this : null, 1 << int_5);
			method_2();
			return 0;
		}
		method_3();
		throw new ZlibException("Bad window size.");
	}

	internal int method_5(FlushType flushType_0)
	{
		if (zlibCodec_0.InputBuffer == null)
		{
			throw new ZlibException("InputBuffer is null. ");
		}
		int num = 0;
		int num2 = -5;
		while (true)
		{
			switch (enum9_0)
			{
			case Enum9.const_11:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 += (uint)(zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] & 0xFF);
					if (uint_0 != uint_1)
					{
						enum9_0 = Enum9.const_13;
						zlibCodec_0.Message = "incorrect data check";
						int_3 = 5;
						break;
					}
					enum9_0 = Enum9.const_12;
					return 1;
				}
				return num2;
			case Enum9.const_10:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 += (uint)((zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] << 8) & 0xFF00);
					enum9_0 = Enum9.const_11;
					break;
				}
				return num2;
			case Enum9.const_9:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 += (uint)((zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] << 16) & 0xFF0000);
					enum9_0 = Enum9.const_10;
					break;
				}
				return num2;
			case Enum9.const_8:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 = (uint)((ulong)(zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] << 24) & 0xFF000000uL);
					enum9_0 = Enum9.const_9;
					break;
				}
				return num2;
			case Enum9.const_7:
				num2 = class8_0.method_1(num2);
				switch (num2)
				{
				case -3:
					enum9_0 = Enum9.const_13;
					int_3 = 0;
					goto end_IL_05fe;
				case 0:
					num2 = num;
					break;
				}
				if (num2 == 1)
				{
					num2 = num;
					uint_0 = class8_0.method_0();
					if (method_0())
					{
						enum9_0 = Enum9.const_8;
						break;
					}
					enum9_0 = Enum9.const_12;
					return 1;
				}
				return num2;
			case Enum9.const_4:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 += (uint)((zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] << 8) & 0xFF00);
					enum9_0 = Enum9.const_5;
					break;
				}
				return num2;
			case Enum9.const_3:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 += (uint)((zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] << 16) & 0xFF0000);
					enum9_0 = Enum9.const_4;
					break;
				}
				return num2;
			case Enum9.const_2:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					uint_1 = (uint)((ulong)(zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] << 24) & 0xFF000000uL);
					enum9_0 = Enum9.const_3;
					break;
				}
				return num2;
			case Enum9.const_1:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					int num3 = zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] & 0xFF;
					if (((int_2 << 8) + num3) % 31 != 0)
					{
						enum9_0 = Enum9.const_13;
						zlibCodec_0.Message = "incorrect header check";
						int_3 = 5;
					}
					else
					{
						enum9_0 = (((num3 & 0x20) == 0) ? Enum9.const_7 : Enum9.const_2);
					}
					break;
				}
				return num2;
			case Enum9.const_0:
				if (zlibCodec_0.AvailableBytesIn != 0)
				{
					num2 = num;
					zlibCodec_0.AvailableBytesIn--;
					zlibCodec_0.TotalBytesIn++;
					if (((int_2 = zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++]) & 0xF) != 8)
					{
						enum9_0 = Enum9.const_13;
						zlibCodec_0.Message = $"unknown compression method (0x{int_2:X2})";
						int_3 = 5;
					}
					else if ((int_2 >> 4) + 8 > int_4)
					{
						enum9_0 = Enum9.const_13;
						zlibCodec_0.Message = $"invalid window size ({(int_2 >> 4) + 8})";
						int_3 = 5;
					}
					else
					{
						enum9_0 = Enum9.const_1;
					}
					break;
				}
				return num2;
			default:
				throw new ZlibException("Stream error.");
			case Enum9.const_5:
				if (zlibCodec_0.AvailableBytesIn == 0)
				{
					return num2;
				}
				num2 = num;
				zlibCodec_0.AvailableBytesIn--;
				zlibCodec_0.TotalBytesIn++;
				uint_1 += (uint)(zlibCodec_0.InputBuffer[zlibCodec_0.NextIn++] & 0xFF);
				zlibCodec_0.uint_0 = uint_1;
				enum9_0 = Enum9.const_6;
				return 2;
			case Enum9.const_6:
				enum9_0 = Enum9.const_13;
				zlibCodec_0.Message = "need dictionary";
				int_3 = 0;
				return -2;
			case Enum9.const_13:
				throw new ZlibException($"Bad state ({zlibCodec_0.Message})");
			case Enum9.const_12:
				{
					return 1;
				}
				end_IL_05fe:
				break;
			}
		}
	}

	internal int method_6(byte[] byte_1)
	{
		int int_ = 0;
		int num = byte_1.Length;
		if (enum9_0 != Enum9.const_6)
		{
			throw new ZlibException("Stream error.");
		}
		if (Class15.smethod_0(1u, byte_1, 0, byte_1.Length) != zlibCodec_0.uint_0)
		{
			return -3;
		}
		zlibCodec_0.uint_0 = Class15.smethod_0(0u, null, 0, 0);
		if (num >= 1 << int_4)
		{
			num = (1 << int_4) - 1;
			int_ = byte_1.Length - num;
		}
		class8_0.method_3(byte_1, int_, num);
		enum9_0 = Enum9.const_7;
		return 0;
	}

	internal int method_7()
	{
		if (enum9_0 != Enum9.const_13)
		{
			enum9_0 = Enum9.const_13;
			int_3 = 0;
		}
		int num;
		if ((num = zlibCodec_0.AvailableBytesIn) == 0)
		{
			return -5;
		}
		int num2 = zlibCodec_0.NextIn;
		int num3 = int_3;
		while (num != 0 && num3 < 4)
		{
			num3 = ((zlibCodec_0.InputBuffer[num2] != byte_0[num3]) ? ((zlibCodec_0.InputBuffer[num2] == 0) ? (4 - num3) : 0) : (num3 + 1));
			num2++;
			num--;
		}
		zlibCodec_0.TotalBytesIn += num2 - zlibCodec_0.NextIn;
		zlibCodec_0.NextIn = num2;
		zlibCodec_0.AvailableBytesIn = num;
		int_3 = num3;
		if (num3 != 4)
		{
			return -3;
		}
		long totalBytesIn = zlibCodec_0.TotalBytesIn;
		long totalBytesOut = zlibCodec_0.TotalBytesOut;
		method_2();
		zlibCodec_0.TotalBytesIn = totalBytesIn;
		zlibCodec_0.TotalBytesOut = totalBytesOut;
		enum9_0 = Enum9.const_7;
		return 0;
	}

	internal int method_8(ZlibCodec zlibCodec_1)
	{
		return class8_0.method_4();
	}
}
