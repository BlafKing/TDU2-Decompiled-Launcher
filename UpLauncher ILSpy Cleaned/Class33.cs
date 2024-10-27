using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using UpLauncher;

internal static class Class33
{
	private static Downloader downloader_0;

	[SpecialName]
	public static Downloader smethod_0()
	{
		return downloader_0;
	}

	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
			Class25.smethod_0();
			bool flag = false;
			foreach (string text in args)
			{
				if (text == "-uninstall")
				{
					flag = true;
					break;
				}
			}
			Class25.smethod_2("------------------------------------------------------------------------------------------------------------------------");
			Class25.smethod_2($"{Path.GetFileName(Application.ExecutablePath)} Main starts at {DateTime.Now.ToUniversalTime().ToString()} UTC");
			Class32.smethod_17(args);
			bool createdNew = false;
			Mutex mutex = new Mutex(initiallyOwned: false, "44938b8f", out createdNew);
			mutex.WaitOne();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Class25.smethod_2($"Environment.OSVersion = {Environment.OSVersion.ToString()}");
			Class25.smethod_2($"Environment.Version  = {Environment.Version.ToString()}");
			FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
			Class25.smethod_2($"Application.ExecutablePath = {Application.ExecutablePath}");
			Class25.smethod_2($"Application.StartupPath = {Application.StartupPath}");
			Class25.smethod_2($"Application.ProductVersion = {Application.ProductVersion}");
			Class25.smethod_2($"Environment.CurrentDirectory = {Environment.CurrentDirectory}");
			Class25.smethod_2($"Executable MD5 = {FileEntry.GetLocalFileMD5(Application.ExecutablePath)}");
			if (flag)
			{
				downloader_0.Uninstall();
			}
			else if (!string.IsNullOrEmpty(Class32.string_27) && !string.IsNullOrEmpty(Class32.string_19))
			{
				Class4.smethod_2();
				bool flag2 = false;
				if (Class4.smethod_9())
				{
					flag2 = Class4.smethod_3();
				}
				if (!flag2)
				{
					downloader_0 = new Downloader(Class32.string_27);
					downloader_0.StartUpdate();
					Application.Run(new MainForm());
				}
			}
			else
			{
				ErrorCodeEx errorCodeEx_ = ErrorCodeEx.ErrorGameExeNull;
				if (string.IsNullOrEmpty(Class32.string_27))
				{
					errorCodeEx_ = ErrorCodeEx.ErrorInstallDirectoryNull;
				}
				Class32.smethod_12(ErrorCode.ErrorGameInstall, errorCodeEx_);
			}
			mutex.ReleaseMutex();
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		Class25.smethod_5();
	}
}
