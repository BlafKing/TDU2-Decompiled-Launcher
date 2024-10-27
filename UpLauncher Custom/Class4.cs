using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using UpLauncher;

internal static class Class4
{
	private static string string_0;

	private static string string_1;

	private static string string_2;

	private static string string_3;

	private static string string_4;

	[SpecialName]
	public static string smethod_0()
	{
		return string_1;
	}

	[SpecialName]
	public static string smethod_1()
	{
		return string_3;
	}

	public static void smethod_2()
	{
		string_0 = Class32.string_27;
		string_1 = Path.GetFileName(Application.ExecutablePath);
		if (string.Compare(string_1, "UpLauncher.exe", ignoreCase: true) == 0)
		{
			string_3 = "_UpLauncher.exe";
		}
		else
		{
			string_3 = "UpLauncher.exe";
		}
		string_2 = string_0 + "\\" + string_1;
		string_4 = string_0 + "\\" + string_3;
	}

	public static bool smethod_3()
	{
		Class25.smethod_2($"Launcher.RelaunchUpdater({string_4})");
		Process process = null;
		try
		{
			if (File.Exists(string_4))
			{
				process = Process.Start(string_4);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return process != null;
	}

	private static bool smethod_4(string string_5)
	{
		Class25.smethod_2($"Launcher._DoWorkCurrentFileOlderTempFile(\"{string_5}\")");
		try
		{
			if (string.Compare(string_1, "_UpLauncher.exe", ignoreCase: true) == 0 && Installer.RemoveFileAttributes(string_5))
			{
				Class25.smethod_2($"Copy file {string_2} as {string_5}");
				File.Copy(string_2, string_5, overwrite: true);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return true;
	}

	private static bool smethod_5(string string_5)
	{
		Class25.smethod_2($"Launcher._DoWorkTempFileOlderCurrentFile(\"{string_5}\")");
		try
		{
			if (string.Compare(string_1, "_UpLauncher.exe", ignoreCase: true) == 0)
			{
				if (Installer.RemoveFileAttributes(string_5))
				{
					Class25.smethod_2($"Copy file {string_2} as {string_5}");
					File.Copy(string_2, string_5, overwrite: true);
				}
			}
			else
			{
				Class25.smethod_2($"Delete file {string_5}");
				File.Delete(string_5);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return true;
	}

	private static bool smethod_6(string string_5)
	{
		Class25.smethod_2($"Launcher._DoWorkFilesAreSame(\"{string_5}\")");
		try
		{
			if (string.Compare(string_1, "UpLauncher.exe", ignoreCase: true) == 0)
			{
				Class25.smethod_2($"Delete file {string_5}");
				File.Delete(string_5);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return true;
	}

	private static string smethod_7(string string_5)
	{
		Assembly assembly = Assembly.Load(File.ReadAllBytes(string_5));
		Version version = assembly.GetName().Version;
		return Class32.smethod_9(version);
	}

	public static bool smethod_8(string string_5, bool bool_0)
	{
		Class25.smethod_2($"Launcher.CompareWithFile(\"{string_5}\")");
		if (!File.Exists(string_5))
		{
			return false;
		}
		try
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			string text = Class32.smethod_9(version);
			string text2 = smethod_7(string_5);
			Class25.smethod_2($"\t\tcurrent file \"{string_2}\" : v.{text}\n\t\ttemp file \"{string_5}\" : v.{text2}");
			int num = string.Compare(text, text2, ignoreCase: true);
			if (num < 0)
			{
				smethod_4(string_5);
				return true;
			}
			if (num > 0)
			{
				smethod_5(string_5);
				return false;
			}
			if (bool_0)
			{
				string localFileMD = FileEntry.GetLocalFileMD5(string_2);
				string localFileMD2 = FileEntry.GetLocalFileMD5(string_5);
				if (string.Compare(localFileMD, localFileMD2, ignoreCase: true) != 0)
				{
					smethod_4(string_5);
					return true;
				}
			}
			smethod_6(string_5);
			return false;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
	}

	public static bool smethod_9()
	{
		Class25.smethod_2("Launcher.CompareFiles()");
		if (File.Exists(string_2) && File.Exists(string_4))
		{
			return smethod_8(string_4, bool_0: false);
		}
		return false;
	}
}
