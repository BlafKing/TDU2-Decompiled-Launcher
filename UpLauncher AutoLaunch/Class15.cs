internal sealed class Class15
{
	private static readonly int int_0 = 65521;

	private static readonly int int_1 = 5552;

	internal static uint smethod_0(uint uint_0, byte[] byte_0, int int_2, int int_3)
	{
		if (byte_0 == null)
		{
			return 1u;
		}
		int num = (int)(uint_0 & 0xFFFF);
		int num2 = (int)((uint_0 >> 16) & 0xFFFF);
		while (int_3 > 0)
		{
			int num3 = ((int_3 < int_1) ? int_3 : int_1);
			int_3 -= num3;
			while (num3 >= 16)
			{
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num += byte_0[int_2++];
				num2 += num;
				num3 -= 16;
			}
			if (num3 != 0)
			{
				do
				{
					num += byte_0[int_2++];
					num2 += num;
				}
				while (--num3 != 0);
			}
			num %= int_0;
			num2 %= int_0;
		}
		return (uint)((num2 << 16) | num);
	}
}
