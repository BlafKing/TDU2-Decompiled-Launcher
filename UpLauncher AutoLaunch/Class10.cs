using System;
using Ionic.Zlib;

internal sealed class Class10
{
	private const int int_0 = 0;

	private const int int_1 = 1;

	private const int int_2 = 2;

	private const int int_3 = 3;

	private const int int_4 = 4;

	private const int int_5 = 5;

	private const int int_6 = 6;

	private const int int_7 = 7;

	private const int int_8 = 8;

	private const int int_9 = 9;

	internal int int_10;

	internal int int_11;

	internal int[] int_12;

	internal int int_13;

	internal int int_14;

	internal int int_15;

	internal int int_16;

	internal int int_17;

	internal byte byte_0;

	internal byte byte_1;

	internal int[] int_18;

	internal int int_19;

	internal int[] int_20;

	internal int int_21;

	internal Class10()
	{
	}

	internal void method_0(int int_22, int int_23, int[] int_24, int int_25, int[] int_26, int int_27)
	{
		int_10 = 0;
		byte_0 = (byte)int_22;
		byte_1 = (byte)int_23;
		int_18 = int_24;
		int_19 = int_25;
		int_20 = int_26;
		int_21 = int_27;
		int_12 = null;
	}

	internal int method_1(Class8 class8_0, int int_22)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		ZlibCodec zlibCodec_ = class8_0.zlibCodec_0;
		num3 = zlibCodec_.NextIn;
		int num4 = zlibCodec_.AvailableBytesIn;
		num = class8_0.int_10;
		num2 = class8_0.int_9;
		int num5 = class8_0.int_14;
		int num6 = ((num5 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
		while (true)
		{
			switch (int_10)
			{
			case 6:
				if (num6 == 0)
				{
					if (num5 == class8_0.int_12 && class8_0.int_13 != 0)
					{
						num5 = 0;
						num6 = ((0 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
					}
					if (num6 == 0)
					{
						class8_0.int_14 = num5;
						int_22 = class8_0.method_5(int_22);
						num5 = class8_0.int_14;
						num6 = ((num5 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
						if (num5 == class8_0.int_12 && class8_0.int_13 != 0)
						{
							num5 = 0;
							num6 = ((0 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
						}
						if (num6 == 0)
						{
							class8_0.int_10 = num;
							class8_0.int_9 = num2;
							zlibCodec_.AvailableBytesIn = num4;
							zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
							zlibCodec_.NextIn = num3;
							class8_0.int_14 = num5;
							return class8_0.method_5(int_22);
						}
					}
				}
				int_22 = 0;
				class8_0.byte_0[num5++] = (byte)int_15;
				num6--;
				int_10 = 0;
				break;
			case 5:
			{
				int i;
				for (i = num5 - int_17; i < 0; i += class8_0.int_12)
				{
				}
				while (int_11 != 0)
				{
					if (num6 == 0)
					{
						if (num5 == class8_0.int_12 && class8_0.int_13 != 0)
						{
							num5 = 0;
							num6 = ((0 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
						}
						if (num6 == 0)
						{
							class8_0.int_14 = num5;
							int_22 = class8_0.method_5(int_22);
							num5 = class8_0.int_14;
							num6 = ((num5 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
							if (num5 == class8_0.int_12 && class8_0.int_13 != 0)
							{
								num5 = 0;
								num6 = ((0 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
							}
							if (num6 == 0)
							{
								class8_0.int_10 = num;
								class8_0.int_9 = num2;
								zlibCodec_.AvailableBytesIn = num4;
								zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
								zlibCodec_.NextIn = num3;
								class8_0.int_14 = num5;
								return class8_0.method_5(int_22);
							}
						}
					}
					class8_0.byte_0[num5++] = class8_0.byte_0[i++];
					num6--;
					if (i == class8_0.int_12)
					{
						i = 0;
					}
					int_11--;
				}
				int_10 = 0;
				break;
			}
			case 4:
			{
				int num7;
				for (num7 = int_16; num2 < num7; num2 += 8)
				{
					if (num4 != 0)
					{
						int_22 = 0;
						num4--;
						num |= (zlibCodec_.InputBuffer[num3++] & 0xFF) << num2;
						continue;
					}
					class8_0.int_10 = num;
					class8_0.int_9 = num2;
					zlibCodec_.AvailableBytesIn = num4;
					zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
					zlibCodec_.NextIn = num3;
					class8_0.int_14 = num5;
					return class8_0.method_5(int_22);
				}
				int_17 += num & Class9.int_0[num7];
				num >>= num7;
				num2 -= num7;
				int_10 = 5;
				goto case 5;
			}
			case 3:
			{
				int num7;
				for (num7 = int_14; num2 < num7; num2 += 8)
				{
					if (num4 != 0)
					{
						int_22 = 0;
						num4--;
						num |= (zlibCodec_.InputBuffer[num3++] & 0xFF) << num2;
						continue;
					}
					class8_0.int_10 = num;
					class8_0.int_9 = num2;
					zlibCodec_.AvailableBytesIn = num4;
					zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
					zlibCodec_.NextIn = num3;
					class8_0.int_14 = num5;
					return class8_0.method_5(int_22);
				}
				int num8 = (int_13 + (num & Class9.int_0[num7])) * 3;
				num >>= int_12[num8 + 1];
				num2 -= int_12[num8 + 1];
				int num9 = int_12[num8];
				if (((uint)num9 & 0x10u) != 0)
				{
					int_16 = num9 & 0xF;
					int_17 = int_12[num8 + 2];
					int_10 = 4;
					break;
				}
				if ((num9 & 0x40) == 0)
				{
					int_14 = num9;
					int_13 = num8 / 3 + int_12[num8 + 2];
					break;
				}
				int_10 = 9;
				zlibCodec_.Message = "invalid distance code";
				int_22 = -3;
				class8_0.int_10 = num;
				class8_0.int_9 = num2;
				zlibCodec_.AvailableBytesIn = num4;
				zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
				zlibCodec_.NextIn = num3;
				class8_0.int_14 = num5;
				return class8_0.method_5(-3);
			}
			case 2:
			{
				int num7;
				for (num7 = int_16; num2 < num7; num2 += 8)
				{
					if (num4 != 0)
					{
						int_22 = 0;
						num4--;
						num |= (zlibCodec_.InputBuffer[num3++] & 0xFF) << num2;
						continue;
					}
					class8_0.int_10 = num;
					class8_0.int_9 = num2;
					zlibCodec_.AvailableBytesIn = num4;
					zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
					zlibCodec_.NextIn = num3;
					class8_0.int_14 = num5;
					return class8_0.method_5(int_22);
				}
				int_11 += num & Class9.int_0[num7];
				num >>= num7;
				num2 -= num7;
				int_14 = byte_1;
				int_12 = int_20;
				int_13 = int_21;
				int_10 = 3;
				goto case 3;
			}
			case 1:
			{
				int num7;
				for (num7 = int_14; num2 < num7; num2 += 8)
				{
					if (num4 != 0)
					{
						int_22 = 0;
						num4--;
						num |= (zlibCodec_.InputBuffer[num3++] & 0xFF) << num2;
						continue;
					}
					class8_0.int_10 = num;
					class8_0.int_9 = num2;
					zlibCodec_.AvailableBytesIn = num4;
					zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
					zlibCodec_.NextIn = num3;
					class8_0.int_14 = num5;
					return class8_0.method_5(int_22);
				}
				int num8 = (int_13 + (num & Class9.int_0[num7])) * 3;
				num >>= int_12[num8 + 1];
				num2 -= int_12[num8 + 1];
				int num9 = int_12[num8];
				if (num9 == 0)
				{
					int_15 = int_12[num8 + 2];
					int_10 = 6;
					break;
				}
				if (((uint)num9 & 0x10u) != 0)
				{
					int_16 = num9 & 0xF;
					int_11 = int_12[num8 + 2];
					int_10 = 2;
					break;
				}
				if ((num9 & 0x40) == 0)
				{
					int_14 = num9;
					int_13 = num8 / 3 + int_12[num8 + 2];
					break;
				}
				if (((uint)num9 & 0x20u) != 0)
				{
					int_10 = 7;
					break;
				}
				int_10 = 9;
				zlibCodec_.Message = "invalid literal/length code";
				int_22 = -3;
				class8_0.int_10 = num;
				class8_0.int_9 = num2;
				zlibCodec_.AvailableBytesIn = num4;
				zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
				zlibCodec_.NextIn = num3;
				class8_0.int_14 = num5;
				return class8_0.method_5(-3);
			}
			case 0:
				if (num6 >= 258 && num4 >= 10)
				{
					class8_0.int_10 = num;
					class8_0.int_9 = num2;
					zlibCodec_.AvailableBytesIn = num4;
					zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
					zlibCodec_.NextIn = num3;
					class8_0.int_14 = num5;
					int_22 = method_2(byte_0, byte_1, int_18, int_19, int_20, int_21, class8_0, zlibCodec_);
					num3 = zlibCodec_.NextIn;
					num4 = zlibCodec_.AvailableBytesIn;
					num = class8_0.int_10;
					num2 = class8_0.int_9;
					num5 = class8_0.int_14;
					num6 = ((num5 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
					if (int_22 != 0)
					{
						int_10 = ((int_22 == 1) ? 7 : 9);
						break;
					}
				}
				int_14 = byte_0;
				int_12 = int_18;
				int_13 = int_19;
				int_10 = 1;
				goto case 1;
			default:
				int_22 = -2;
				class8_0.int_10 = num;
				class8_0.int_9 = num2;
				zlibCodec_.AvailableBytesIn = num4;
				zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
				zlibCodec_.NextIn = num3;
				class8_0.int_14 = num5;
				return class8_0.method_5(-2);
			case 7:
				if (num2 > 7)
				{
					num2 -= 8;
					num4++;
					num3--;
				}
				class8_0.int_14 = num5;
				int_22 = class8_0.method_5(int_22);
				num5 = class8_0.int_14;
				num6 = ((num5 < class8_0.int_13) ? (class8_0.int_13 - num5 - 1) : (class8_0.int_12 - num5));
				if (class8_0.int_13 != class8_0.int_14)
				{
					class8_0.int_10 = num;
					class8_0.int_9 = num2;
					zlibCodec_.AvailableBytesIn = num4;
					zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
					zlibCodec_.NextIn = num3;
					class8_0.int_14 = num5;
					return class8_0.method_5(int_22);
				}
				int_10 = 8;
				goto case 8;
			case 8:
				int_22 = 1;
				class8_0.int_10 = num;
				class8_0.int_9 = num2;
				zlibCodec_.AvailableBytesIn = num4;
				zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
				zlibCodec_.NextIn = num3;
				class8_0.int_14 = num5;
				return class8_0.method_5(1);
			case 9:
				int_22 = -3;
				class8_0.int_10 = num;
				class8_0.int_9 = num2;
				zlibCodec_.AvailableBytesIn = num4;
				zlibCodec_.TotalBytesIn += num3 - zlibCodec_.NextIn;
				zlibCodec_.NextIn = num3;
				class8_0.int_14 = num5;
				return class8_0.method_5(-3);
			}
		}
	}

	internal int method_2(int int_22, int int_23, int[] int_24, int int_25, int[] int_26, int int_27, Class8 class8_0, ZlibCodec zlibCodec_0)
	{
		int nextIn = zlibCodec_0.NextIn;
		int num = zlibCodec_0.AvailableBytesIn;
		int num2 = class8_0.int_10;
		int num3 = class8_0.int_9;
		int num4 = class8_0.int_14;
		int num5 = ((num4 < class8_0.int_13) ? (class8_0.int_13 - num4 - 1) : (class8_0.int_12 - num4));
		int num6 = Class9.int_0[int_22];
		int num7 = Class9.int_0[int_23];
		int num12;
		while (true)
		{
			if (num3 < 20)
			{
				num--;
				num2 |= (zlibCodec_0.InputBuffer[nextIn++] & 0xFF) << num3;
				num3 += 8;
				continue;
			}
			int num8 = num2 & num6;
			int[] array = int_24;
			int num9 = int_25;
			int num10 = (num9 + num8) * 3;
			int num11;
			if ((num11 = array[num10]) == 0)
			{
				num2 >>= array[num10 + 1];
				num3 -= array[num10 + 1];
				class8_0.byte_0[num4++] = (byte)array[num10 + 2];
				num5--;
			}
			else
			{
				while (true)
				{
					num2 >>= array[num10 + 1];
					num3 -= array[num10 + 1];
					if ((num11 & 0x10) == 0)
					{
						if ((num11 & 0x40) == 0)
						{
							num8 += array[num10 + 2];
							num8 += num2 & Class9.int_0[num11];
							num10 = (num9 + num8) * 3;
							if ((num11 = array[num10]) == 0)
							{
								num2 >>= array[num10 + 1];
								num3 -= array[num10 + 1];
								class8_0.byte_0[num4++] = (byte)array[num10 + 2];
								num5--;
								break;
							}
							continue;
						}
						if (((uint)num11 & 0x20u) != 0)
						{
							num12 = zlibCodec_0.AvailableBytesIn - num;
							num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
							num += num12;
							nextIn -= num12;
							num3 -= num12 << 3;
							class8_0.int_10 = num2;
							class8_0.int_9 = num3;
							zlibCodec_0.AvailableBytesIn = num;
							zlibCodec_0.TotalBytesIn += nextIn - zlibCodec_0.NextIn;
							zlibCodec_0.NextIn = nextIn;
							class8_0.int_14 = num4;
							return 1;
						}
						zlibCodec_0.Message = "invalid literal/length code";
						num12 = zlibCodec_0.AvailableBytesIn - num;
						num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
						num += num12;
						nextIn -= num12;
						num3 -= num12 << 3;
						class8_0.int_10 = num2;
						class8_0.int_9 = num3;
						zlibCodec_0.AvailableBytesIn = num;
						zlibCodec_0.TotalBytesIn += nextIn - zlibCodec_0.NextIn;
						zlibCodec_0.NextIn = nextIn;
						class8_0.int_14 = num4;
						return -3;
					}
					num11 &= 0xF;
					num12 = array[num10 + 2] + (num2 & Class9.int_0[num11]);
					num2 >>= num11;
					for (num3 -= num11; num3 < 15; num3 += 8)
					{
						num--;
						num2 |= (zlibCodec_0.InputBuffer[nextIn++] & 0xFF) << num3;
					}
					num8 = num2 & num7;
					array = int_26;
					num9 = int_27;
					num10 = (num9 + num8) * 3;
					num11 = array[num10];
					while (true)
					{
						num2 >>= array[num10 + 1];
						num3 -= array[num10 + 1];
						if (((uint)num11 & 0x10u) != 0)
						{
							break;
						}
						if ((num11 & 0x40) == 0)
						{
							num8 += array[num10 + 2];
							num8 += num2 & Class9.int_0[num11];
							num10 = (num9 + num8) * 3;
							num11 = array[num10];
							continue;
						}
						zlibCodec_0.Message = "invalid distance code";
						num12 = zlibCodec_0.AvailableBytesIn - num;
						num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
						num += num12;
						nextIn -= num12;
						num3 -= num12 << 3;
						class8_0.int_10 = num2;
						class8_0.int_9 = num3;
						zlibCodec_0.AvailableBytesIn = num;
						zlibCodec_0.TotalBytesIn += nextIn - zlibCodec_0.NextIn;
						zlibCodec_0.NextIn = nextIn;
						class8_0.int_14 = num4;
						return -3;
					}
					for (num11 &= 0xF; num3 < num11; num3 += 8)
					{
						num--;
						num2 |= (zlibCodec_0.InputBuffer[nextIn++] & 0xFF) << num3;
					}
					int num13 = array[num10 + 2] + (num2 & Class9.int_0[num11]);
					num2 >>= num11;
					num3 -= num11;
					num5 -= num12;
					int num14;
					if (num4 >= num13)
					{
						num14 = num4 - num13;
						if (num4 - num14 > 0 && 2 > num4 - num14)
						{
							class8_0.byte_0[num4++] = class8_0.byte_0[num14++];
							class8_0.byte_0[num4++] = class8_0.byte_0[num14++];
							num12 -= 2;
						}
						else
						{
							Array.Copy(class8_0.byte_0, num14, class8_0.byte_0, num4, 2);
							num4 += 2;
							num14 += 2;
							num12 -= 2;
						}
					}
					else
					{
						num14 = num4 - num13;
						do
						{
							num14 += class8_0.int_12;
						}
						while (num14 < 0);
						num11 = class8_0.int_12 - num14;
						if (num12 > num11)
						{
							num12 -= num11;
							if (num4 - num14 > 0 && num11 > num4 - num14)
							{
								do
								{
									class8_0.byte_0[num4++] = class8_0.byte_0[num14++];
								}
								while (--num11 != 0);
							}
							else
							{
								Array.Copy(class8_0.byte_0, num14, class8_0.byte_0, num4, num11);
								num4 += num11;
								num14 += num11;
								num11 = 0;
							}
							num14 = 0;
						}
					}
					if (num4 - num14 > 0 && num12 > num4 - num14)
					{
						do
						{
							class8_0.byte_0[num4++] = class8_0.byte_0[num14++];
						}
						while (--num12 != 0);
						break;
					}
					Array.Copy(class8_0.byte_0, num14, class8_0.byte_0, num4, num12);
					num4 += num12;
					num14 += num12;
					num12 = 0;
					break;
				}
			}
			if (num5 < 258 || num < 10)
			{
				break;
			}
		}
		num12 = zlibCodec_0.AvailableBytesIn - num;
		num12 = ((num3 >> 3 < num12) ? (num3 >> 3) : num12);
		num += num12;
		nextIn -= num12;
		num3 -= num12 << 3;
		class8_0.int_10 = num2;
		class8_0.int_9 = num3;
		zlibCodec_0.AvailableBytesIn = num;
		zlibCodec_0.TotalBytesIn += nextIn - zlibCodec_0.NextIn;
		zlibCodec_0.NextIn = nextIn;
		class8_0.int_14 = num4;
		return 0;
	}
}
