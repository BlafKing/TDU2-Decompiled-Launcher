using System;
using System.IO;
using System.Windows.Forms;

internal static class Class25
{
	public static void smethod_0()
	{
		string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Eden Games\\Test Drive Unlimited 2\\";
		text += "UpLauncher.exe.log";
		try
		{
			if (File.Exists(text))
			{
				File.Delete(text);
			}
		}
		catch (Exception)
		{
		}
		text = Application.ExecutablePath + ".log";
		try
		{
			if (File.Exists(text))
			{
				File.Delete(text);
			}
		}
		catch (Exception)
		{
		}
	}

	private static void smethod_1(string string_0)
	{
	}

	public static void smethod_2(string string_0)
	{
	}

	public static void smethod_3(string string_0)
	{
	}

	public static void smethod_4(string string_0)
	{
	}

	public static void smethod_5()
	{
	}
}
