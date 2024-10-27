using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zip;

internal class Class22 : Class17
{
	private FileAttributes fileAttributes_0;

	internal Enum14 enum14_0;

	internal override bool vmethod_0(ZipEntry zipEntry_0)
	{
		FileAttributes attributes = zipEntry_0.Attributes;
		return method_4(attributes);
	}

	[SpecialName]
	internal string method_1()
	{
		string text = "";
		if ((fileAttributes_0 & FileAttributes.Hidden) != 0)
		{
			text += "H";
		}
		if ((fileAttributes_0 & FileAttributes.System) != 0)
		{
			text += "S";
		}
		if ((fileAttributes_0 & FileAttributes.ReadOnly) != 0)
		{
			text += "R";
		}
		if ((fileAttributes_0 & FileAttributes.Archive) != 0)
		{
			text += "A";
		}
		if ((fileAttributes_0 & FileAttributes.NotContentIndexed) != 0)
		{
			text += "I";
		}
		if ((fileAttributes_0 & FileAttributes.ReparsePoint) != 0)
		{
			text += "I";
		}
		return text;
	}

	[SpecialName]
	internal void method_2(string string_0)
	{
		fileAttributes_0 = FileAttributes.Normal;
		string text = string_0.ToUpper();
		foreach (char c in text)
		{
			switch (c)
			{
			case 'R':
				if ((fileAttributes_0 & FileAttributes.ReadOnly) == 0)
				{
					fileAttributes_0 |= FileAttributes.ReadOnly;
					break;
				}
				throw new ArgumentException($"Repeated flag. ({c})", "value");
			case 'S':
				if ((fileAttributes_0 & FileAttributes.System) == 0)
				{
					fileAttributes_0 |= FileAttributes.System;
					break;
				}
				throw new ArgumentException($"Repeated flag. ({c})", "value");
			case 'H':
				if ((fileAttributes_0 & FileAttributes.Hidden) == 0)
				{
					fileAttributes_0 |= FileAttributes.Hidden;
					break;
				}
				throw new ArgumentException($"Repeated flag. ({c})", "value");
			case 'I':
				if ((fileAttributes_0 & FileAttributes.NotContentIndexed) == 0)
				{
					fileAttributes_0 |= FileAttributes.NotContentIndexed;
					break;
				}
				throw new ArgumentException($"Repeated flag. ({c})", "value");
			case 'L':
				if ((fileAttributes_0 & FileAttributes.ReparsePoint) == 0)
				{
					fileAttributes_0 |= FileAttributes.ReparsePoint;
					break;
				}
				throw new ArgumentException($"Repeated flag. ({c})", "value");
			case 'A':
				if ((fileAttributes_0 & FileAttributes.Archive) == 0)
				{
					fileAttributes_0 |= FileAttributes.Archive;
					break;
				}
				throw new ArgumentException($"Repeated flag. ({c})", "value");
			default:
				throw new ArgumentException(string_0);
			}
		}
	}

    public override string ToString()
    {
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("attributes ").Append(Class27.smethod_0(enum14_0)).Append(" ")
			.Append(method_1());
		return stringBuilder.ToString();
	}

	private bool method_3(FileAttributes fileAttributes_1, FileAttributes fileAttributes_2)
	{
		bool flag = false;
		if ((fileAttributes_0 & fileAttributes_2) == fileAttributes_2)
		{
			return (fileAttributes_1 & fileAttributes_2) == fileAttributes_2;
		}
		return true;
	}

	internal override bool vmethod_1(string string_0)
	{
		FileAttributes attributes = File.GetAttributes(string_0);
		return method_4(attributes);
	}

	private bool method_4(FileAttributes fileAttributes_1)
	{
		bool flag;
		if (flag = method_3(fileAttributes_1, FileAttributes.Hidden))
		{
			flag = method_3(fileAttributes_1, FileAttributes.System);
		}
		if (flag)
		{
			flag = method_3(fileAttributes_1, FileAttributes.ReadOnly);
		}
		if (flag)
		{
			flag = method_3(fileAttributes_1, FileAttributes.Archive);
		}
		if (flag)
		{
			flag = method_3(fileAttributes_1, FileAttributes.NotContentIndexed);
		}
		if (flag)
		{
			flag = method_3(fileAttributes_1, FileAttributes.ReparsePoint);
		}
		if (enum14_0 != Enum14.const_4)
		{
			flag = !flag;
		}
		return flag;
	}
}
