using System;
using System.IO;
using System.Text;
using Ionic.Zip;

internal class Class20 : Class17
{
	internal Enum14 enum14_0;

	internal Enum13 enum13_0;

	internal DateTime dateTime_0;

	internal override bool vmethod_0(ZipEntry zipEntry_0)
	{
		return method_1(enum13_0 switch
		{
			Enum13.const_0 => zipEntry_0.AccessedTime, 
			Enum13.const_1 => zipEntry_0.ModifiedTime, 
			Enum13.const_2 => zipEntry_0.CreationTime, 
			_ => throw new ArgumentException("??time"), 
		});
	}

    public override string ToString()
    {
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(enum13_0.ToString()).Append(" ").Append(Class27.smethod_0(enum14_0))
			.Append(" ")
			.Append(dateTime_0.ToString("yyyy-MM-dd-HH:mm:ss"));
		return stringBuilder.ToString();
	}

	internal override bool vmethod_1(string string_0)
	{
		return method_1(enum13_0 switch
		{
			Enum13.const_0 => File.GetLastAccessTimeUtc(string_0), 
			Enum13.const_1 => File.GetLastWriteTimeUtc(string_0), 
			Enum13.const_2 => File.GetCreationTimeUtc(string_0), 
			_ => throw new ArgumentException("Operator"), 
		});
	}

	private bool method_1(DateTime dateTime_1)
	{
		bool flag = false;
		return enum14_0 switch
		{
			Enum14.const_0 => dateTime_1 > dateTime_0, 
			Enum14.const_1 => dateTime_1 >= dateTime_0, 
			Enum14.const_2 => dateTime_1 < dateTime_0, 
			Enum14.const_3 => dateTime_1 <= dateTime_0, 
			Enum14.const_4 => dateTime_1 == dateTime_0, 
			Enum14.const_5 => dateTime_1 != dateTime_0, 
			_ => throw new ArgumentException("Operator"), 
		};
	}
}
