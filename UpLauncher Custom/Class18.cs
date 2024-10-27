using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

internal class Class18 : Class17
{
	private Regex regex_0;

	private string string_0;

	internal Enum14 enum14_0;

	private string string_1;

	internal override bool vmethod_0(ZipEntry zipEntry_0)
	{
		string string_ = zipEntry_0.FileName.Replace("/", "\\");
		return method_1(string_);
	}

	[SpecialName]
	internal virtual void vmethod_2(string string_2)
	{
		if (Directory.Exists(string_2))
		{
			string_1 = ".\\" + string_2 + "\\*";
		}
		else
		{
			string_1 = string_2;
		}
		string_0 = "^" + Regex.Escape(string_1).Replace("\\*\\.\\*", "([^\\.]+|.*\\.[^\\\\\\.]*)").Replace("\\.\\*", "\\.[^\\\\\\.]*")
			.Replace("\\*", ".*")
			.Replace("\\?", "[^\\\\\\.]") + "$";
		regex_0 = new Regex(string_0, RegexOptions.IgnoreCase);
	}

    public override string ToString()
    {
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("name ").Append(Class27.smethod_0(enum14_0)).Append(" ")
			.Append(string_1);
		return stringBuilder.ToString();
	}

	internal override bool vmethod_1(string string_2)
	{
		return method_1(string_2);
	}

	private bool method_1(string string_2)
	{
		string input = ((string_1.IndexOf('\\') == -1) ? Path.GetFileName(string_2) : string_2);
		bool flag = regex_0.IsMatch(input);
		if (enum14_0 != Enum14.const_4)
		{
			flag = !flag;
		}
		return flag;
	}
}
