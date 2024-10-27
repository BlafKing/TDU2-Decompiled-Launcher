using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zip;

internal class Class21 : Class17
{
	private char char_0;

	internal Enum14 enum14_0;

	internal override bool vmethod_0(ZipEntry zipEntry_0)
	{
		bool flag = ((char_0 == 'D') ? zipEntry_0.IsDirectory : (!zipEntry_0.IsDirectory));
		if (enum14_0 != Enum14.const_4)
		{
			flag = !flag;
		}
		return flag;
	}

	[SpecialName]
	internal string method_1()
	{
		return char_0.ToString();
	}

	[SpecialName]
	internal void method_2(string string_0)
	{
		if (string_0.Length != 1 || (string_0[0] != 'D' && string_0[0] != 'F'))
		{
			throw new ArgumentException("Specify a single character: either D or F");
		}
		char_0 = string_0[0];
	}

    public override string ToString()
    {
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("type ").Append(Class27.smethod_0(enum14_0)).Append(" ")
			.Append(method_1());
		return stringBuilder.ToString();
	}

	internal override bool vmethod_1(string string_0)
	{
		bool flag = ((char_0 == 'D') ? Directory.Exists(string_0) : File.Exists(string_0));
		if (enum14_0 != Enum14.const_4)
		{
			flag = !flag;
		}
		return flag;
	}
}
