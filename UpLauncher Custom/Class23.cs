using System;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zip;

internal class Class23 : Class17
{
	internal Enum12 enum12_0;

	internal Class17 class17_0;

	private Class17 class17_1;

	internal override bool vmethod_0(ZipEntry zipEntry_0)
	{
		bool flag = class17_0.vmethod_0(zipEntry_0);
		switch (enum12_0)
		{
		case Enum12.const_1:
			if (flag)
			{
				flag = method_1().vmethod_0(zipEntry_0);
			}
			break;
		case Enum12.const_2:
			if (!flag)
			{
				flag = method_1().vmethod_0(zipEntry_0);
			}
			break;
		case Enum12.const_3:
			flag ^= method_1().vmethod_0(zipEntry_0);
			break;
		}
		return flag;
	}

	[SpecialName]
	internal Class17 method_1()
	{
		return class17_1;
	}

	[SpecialName]
	internal void method_2(Class17 class17_2)
	{
		class17_1 = class17_2;
		if (class17_2 == null)
		{
			enum12_0 = Enum12.const_0;
		}
		else if (enum12_0 == Enum12.const_0)
		{
			enum12_0 = Enum12.const_1;
		}
	}

	internal override bool vmethod_1(string string_0)
	{
		bool flag = class17_0.vmethod_1(string_0);
		switch (enum12_0)
		{
		default:
			throw new ArgumentException("Conjunction");
		case Enum12.const_1:
			if (flag)
			{
				flag = method_1().vmethod_1(string_0);
			}
			break;
		case Enum12.const_2:
			if (!flag)
			{
				flag = method_1().vmethod_1(string_0);
			}
			break;
		case Enum12.const_3:
			flag ^= method_1().vmethod_1(string_0);
			break;
		}
		return flag;
	}

    public override string ToString()
    {
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("(").Append((class17_0 != null) ? class17_0.ToString() : "null").Append(" ")
			.Append(enum12_0.ToString())
			.Append(" ")
			.Append((method_1() != null) ? method_1().ToString() : "null")
			.Append(")");
		return stringBuilder.ToString();
	}
}
