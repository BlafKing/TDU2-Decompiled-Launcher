using System;
using System.IO;
using System.Text;
using Ionic.Zip;

internal class Class19 : Class17
{
	internal Enum14 enum14_0;

	internal long long_0;

	internal override bool vmethod_0(ZipEntry zipEntry_0)
	{
		return method_1(zipEntry_0.UncompressedSize);
	}

    public override string ToString()
    {
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("size ").Append(Class27.smethod_0(enum14_0)).Append(" ")
			.Append(long_0.ToString());
		return stringBuilder.ToString();
	}

	internal override bool vmethod_1(string string_0)
	{
		FileInfo fileInfo = new FileInfo(string_0);
		return method_1(fileInfo.Length);
	}

	private bool method_1(long long_1)
	{
		bool flag = false;
		return enum14_0 switch
		{
			Enum14.const_0 => long_1 > long_0, 
			Enum14.const_1 => long_1 >= long_0, 
			Enum14.const_2 => long_1 < long_0, 
			Enum14.const_3 => long_1 <= long_0, 
			Enum14.const_4 => long_1 == long_0, 
			Enum14.const_5 => long_1 != long_0, 
			_ => throw new ArgumentException("Operator"), 
		};
	}
}
