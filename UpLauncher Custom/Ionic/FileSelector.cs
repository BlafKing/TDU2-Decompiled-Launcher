using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic;

public class FileSelector
{
	private enum Enum4
	{
		const_0,
		const_1,
		const_2,
		const_3,
		const_4
	}

	internal Class17 class17_0;

	[CompilerGenerated]
	private bool bool_0;

	public string SelectionCriteria
	{
		get
		{
			if (class17_0 == null)
			{
				return null;
			}
			return class17_0.ToString();
		}
		set
		{
			if (value == null)
			{
				class17_0 = null;
			}
			else if (value.Trim() == "")
			{
				class17_0 = null;
			}
			else
			{
				class17_0 = smethod_0(value);
			}
		}
	}

	public bool TraverseReparsePoints
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	private bool method_0(ZipEntry zipEntry_0)
	{
		return class17_0.vmethod_0(zipEntry_0);
	}

	public ICollection<ZipEntry> SelectEntries(ZipFile zipFile_0)
	{
		List<ZipEntry> list = new List<ZipEntry>();
		foreach (ZipEntry item in zipFile_0)
		{
			if (method_0(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public ICollection<ZipEntry> SelectEntries(ZipFile zipFile_0, string directoryPathInArchive)
	{
		List<ZipEntry> list = new List<ZipEntry>();
		string text = directoryPathInArchive?.Replace("/", "\\");
		if (text != null)
		{
			while (text.EndsWith("\\"))
			{
				text = text.Substring(0, text.Length - 1);
			}
		}
		foreach (ZipEntry item in zipFile_0)
		{
			if ((directoryPathInArchive == null || Path.GetDirectoryName(item.FileName) == directoryPathInArchive || Path.GetDirectoryName(item.FileName) == text) && method_0(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public FileSelector(string selectionCriteria)
		: this(selectionCriteria, traverseDirectoryReparsePoints: true)
	{
	}

	public FileSelector(string selectionCriteria, bool traverseDirectoryReparsePoints)
	{
		if (!string.IsNullOrEmpty(selectionCriteria))
		{
			class17_0 = smethod_0(selectionCriteria);
		}
		TraverseReparsePoints = traverseDirectoryReparsePoints;
	}

	private static Class17 smethod_0(string string_0)
	{
		if (string_0 == null)
		{
			return null;
		}
		string[][] array = new string[10][]
		{
			new string[2] { "\\(\\(", "( (" },
			new string[2] { "\\)\\)", ") )" },
			new string[2] { "\\((\\S)", "( $1" },
			new string[2] { "(\\S)\\)", "$1 )" },
			new string[2] { "(\\S)\\(", "$1 (" },
			new string[2] { "\\)(\\S)", ") $1" },
			new string[2] { "([^ ]+)>([^ ]+)", "$1 > $2" },
			new string[2] { "([^ ]+)<([^ ]+)", "$1 < $2" },
			new string[2] { "([^ ]+)!=([^ ]+)", "$1 != $2" },
			new string[2] { "([^ ]+)=([^ ]+)", "$1 = $2" }
		};
		for (int i = 0; i < array.Length; i++)
		{
			Regex regex = new Regex(array[i][0]);
			string_0 = regex.Replace(string_0, array[i][1]);
		}
		if (string_0.IndexOf(" ") == -1)
		{
			string_0 = "name = " + string_0;
		}
		string[] array2 = string_0.Trim().Split(' ', '\t');
		if (array2.Length < 3)
		{
			throw new ArgumentException(string_0);
		}
		Class17 @class = null;
		Enum12 @enum = Enum12.const_0;
		Stack<Enum4> stack = new Stack<Enum4>();
		Stack<Class17> stack2 = new Stack<Class17>();
		stack.Push(Enum4.const_0);
		for (int j = 0; j < array2.Length; j++)
		{
			string text = array2[j].ToLower();
			Enum4 enum4;
			switch (text)
			{
			case "and":
			case "xor":
			case "or":
				enum4 = stack.Peek();
				if (enum4 == Enum4.const_2)
				{
					if (array2.Length > j + 3)
					{
						@enum = (Enum12)Enum.Parse(typeof(Enum12), array2[j].ToUpper());
						Class23 class7 = new Class23();
						class7.class17_0 = @class;
						class7.method_2(null);
						class7.enum12_0 = @enum;
						@class = class7;
						stack.Push(enum4);
						stack.Push(Enum4.const_3);
						stack2.Push(@class);
						goto IL_082f;
					}
					throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case "(":
				enum4 = stack.Peek();
				if (enum4 == Enum4.const_0 || enum4 == Enum4.const_3 || enum4 == Enum4.const_1)
				{
					if (array2.Length > j + 4)
					{
						stack.Push(Enum4.const_1);
						goto IL_082f;
					}
					throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case ")":
				enum4 = stack.Pop();
				if (stack.Peek() == Enum4.const_1)
				{
					stack.Pop();
					stack.Push(Enum4.const_2);
					goto IL_082f;
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case "atime":
			case "ctime":
			case "mtime":
				if (array2.Length > j + 2)
				{
					DateTime value;
					try
					{
						value = DateTime.ParseExact(array2[j + 2], "yyyy-MM-dd-HH:mm:ss", null);
					}
					catch (FormatException)
					{
						try
						{
							value = DateTime.ParseExact(array2[j + 2], "yyyy/MM/dd-HH:mm:ss", null);
						}
						catch (FormatException)
						{
							try
							{
								value = DateTime.ParseExact(array2[j + 2], "yyyy/MM/dd", null);
								goto end_IL_0461;
							}
							catch (FormatException)
							{
								try
								{
									value = DateTime.ParseExact(array2[j + 2], "MM/dd/yyyy", null);
									goto end_IL_0461;
								}
								catch (FormatException)
								{
									value = DateTime.ParseExact(array2[j + 2], "yyyy-MM-dd", null);
									goto end_IL_0461;
								}
							}
							end_IL_0461:;
						}
					}
					value = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
					Class20 class8 = new Class20();
					class8.enum13_0 = (Enum13)Enum.Parse(typeof(Enum13), array2[j]);
					class8.enum14_0 = (Enum14)Class27.smethod_1(typeof(Enum14), array2[j + 1]);
					class8.dateTime_0 = value;
					@class = class8;
					j += 2;
					stack.Push(Enum4.const_2);
					goto IL_082f;
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case "length":
			case "size":
				if (array2.Length > j + 2)
				{
					long num2 = 0L;
					string text3 = array2[j + 2];
					num2 = (text3.ToUpper().EndsWith("K") ? (long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L) : (text3.ToUpper().EndsWith("KB") ? (long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L) : (text3.ToUpper().EndsWith("M") ? (long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L * 1024L) : (text3.ToUpper().EndsWith("MB") ? (long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L * 1024L) : (text3.ToUpper().EndsWith("G") ? (long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L * 1024L * 1024L) : ((!text3.ToUpper().EndsWith("GB")) ? long.Parse(array2[j + 2]) : (long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L * 1024L * 1024L)))))));
					Class19 class5 = new Class19();
					class5.long_0 = num2;
					class5.enum14_0 = (Enum14)Class27.smethod_1(typeof(Enum14), array2[j + 1]);
					@class = class5;
					j += 2;
					stack.Push(Enum4.const_2);
					goto IL_082f;
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case "filename":
			case "name":
				if (array2.Length > j + 2)
				{
					Enum14 enum3 = (Enum14)Class27.smethod_1(typeof(Enum14), array2[j + 1]);
					if (enum3 == Enum14.const_5 || enum3 == Enum14.const_4)
					{
						string text2 = array2[j + 2];
						if (text2.StartsWith("'"))
						{
							int num = j;
							if (!text2.EndsWith("'"))
							{
								do
								{
									j++;
									if (array2.Length > j + 2)
									{
										text2 = text2 + " " + array2[j + 2];
										continue;
									}
									throw new ArgumentException(string.Join(" ", array2, num, array2.Length - num));
								}
								while (!array2[j + 2].EndsWith("'"));
							}
							text2 = text2.Substring(1, text2.Length - 2);
						}
						Class18 class4 = new Class18();
						class4.vmethod_2(text2);
						class4.enum14_0 = enum3;
						@class = class4;
						j += 2;
						stack.Push(Enum4.const_2);
						goto IL_082f;
					}
					throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case "attrs":
			case "attributes":
			case "type":
				if (array2.Length > j + 2)
				{
					Enum14 enum2 = (Enum14)Class27.smethod_1(typeof(Enum14), array2[j + 1]);
					if (enum2 == Enum14.const_5 || enum2 == Enum14.const_4)
					{
						object obj;
						if (!(text == "type"))
						{
							Class22 class2 = new Class22();
							class2.method_2(array2[j + 2]);
							class2.enum14_0 = enum2;
							obj = class2;
						}
						else
						{
							Class21 class3 = new Class21();
							class3.method_2(array2[j + 2]);
							class3.enum14_0 = enum2;
							obj = class3;
						}
						@class = (Class17)obj;
						j += 2;
						stack.Push(Enum4.const_2);
						goto IL_082f;
					}
					throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
				}
				throw new ArgumentException(string.Join(" ", array2, j, array2.Length - j));
			case "":
				stack.Push(Enum4.const_4);
				goto IL_082f;
			default:
				{
					throw new ArgumentException("'" + array2[j] + "'");
				}
				IL_082f:
				enum4 = stack.Peek();
				if (enum4 == Enum4.const_2)
				{
					stack.Pop();
					if (stack.Peek() == Enum4.const_3)
					{
						while (stack.Peek() == Enum4.const_3)
						{
							Class23 class6 = stack2.Pop() as Class23;
							class6.method_2(@class);
							@class = class6;
							stack.Pop();
							enum4 = stack.Pop();
							if (enum4 != Enum4.const_2)
							{
								throw new ArgumentException("??");
							}
						}
					}
					else
					{
						stack.Push(Enum4.const_2);
					}
				}
				if (enum4 == Enum4.const_4)
				{
					stack.Pop();
				}
				break;
			}
		}
		return @class;
	}

	public override string ToString()
	{
		return "FileSelector(" + class17_0.ToString() + ")";
	}

	private bool method_1(string string_0)
	{
		return class17_0.vmethod_1(string_0);
	}

	public ICollection<string> SelectFiles(string directory)
	{
		return SelectFiles(directory, recurseDirectories: false);
	}

	public ReadOnlyCollection<string> SelectFiles(string directory, bool recurseDirectories)
	{
		if (class17_0 == null)
		{
			throw new ArgumentException("SelectionCriteria has not been set");
		}
		List<string> list = new List<string>();
		try
		{
			if (Directory.Exists(directory))
			{
				string[] files = Directory.GetFiles(directory);
				string[] array = files;
				foreach (string text in array)
				{
					if (method_1(text))
					{
						list.Add(text);
					}
				}
				if (recurseDirectories)
				{
					string[] directories = Directory.GetDirectories(directory);
					string[] array2 = directories;
					foreach (string text2 in array2)
					{
						if (TraverseReparsePoints || (File.GetAttributes(text2) & FileAttributes.ReparsePoint) == 0)
						{
							list.AddRange(SelectFiles(text2, recurseDirectories));
						}
					}
				}
			}
		}
		catch (UnauthorizedAccessException)
		{
		}
		catch (IOException)
		{
		}
		return list.AsReadOnly();
	}
}
