using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using UpLauncher;
using UpLauncher.Gui;

internal static class Class32
{
	public const ulong ulong_0 = 1024uL;

	public const ulong ulong_1 = 1048576uL;

	public const ulong ulong_2 = 1073741824uL;

	public const ulong ulong_3 = 1099511627776uL;

	public const string string_0 = "TDU2 Launcher & Updater";

	public const string string_1 = "UpLauncher";

	public const string string_2 = "UpLauncher.exe";

	public const string string_3 = "_UpLauncher.exe";

	public const string string_4 = "DownloadCache";

	public const string string_5 = "DownloadCache\\UpLauncher.exe";

	public const string string_6 = "44938b8f";

	public const string string_7 = "957e4cc3";

	public const string string_8 = "http://testdriveunlimited2.com";

	public const string string_9 = "http://store.steampowered.com/app/9930/";

	public const string string_10 = "http://patchserver.testdriveunlimited2.com/final/";

	public const string string_11 = "http://clientstats.testdriveunlimited2.com/final/";

	public const string string_12 = "http://www.testdriveunlimited2.com/launchernews/";

	public const ushort ushort_0 = 8889;

	public const ushort ushort_1 = 3478;

	public const uint uint_0 = 3u;

	public const string string_13 = "http://forums.testdriveunlimited2.com/showthread.php?p=212112";

	public const uint uint_1 = 5u;

	public const ulong ulong_4 = 1610612736uL;

	public const ulong ulong_5 = 536870912uL;

	public const string string_14 = "-uninstall";

	public const string string_15 = "-suninstall";

	public static string[] string_16;

	public static readonly string[] string_17;

	public static string string_18;

	public static string string_19;

	public static string string_20;

	public static string string_21;

	public static string string_22;

	public static string string_23;

	public static string string_24;

	public static string string_25;

	public static string string_26;

	public static string string_27;

	public static string string_28;

	public static string string_29;

	public static string string_30;

	public static string string_31;

	public static bool bool_0;

	public static bool bool_1;

	public static ISteamAPI isteamAPI_0;

	public static KeyChecker keyChecker_0;

	public static Statistics statistics_0;

	public static GameServersStatus gameServersStatus_0;

	public static NatResolver natResolver_0;

	public static IPAddress ipaddress_0;

	public static AudioMode audioMode_0;

	public static string[] string_32;

	private static bool smethod_0(RegistryKey registryKey_0, string string_33)
	{
		if (string.IsNullOrEmpty(string_33))
		{
			return false;
		}
		try
		{
			RegistryKey registryKey = registryKey_0.OpenSubKey(string_33);
			if (registryKey != null)
			{
				registryKey.Close();
				return true;
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return false;
	}

	private static string smethod_1(RegistryKey registryKey_0, string string_33, string string_34)
	{
		if (!string.IsNullOrEmpty(string_33) && !string.IsNullOrEmpty(string_34))
		{
			string text = null;
			try
			{
				RegistryKey registryKey = registryKey_0.OpenSubKey(string_33);
				if (registryKey != null)
				{
					if ((text = (string)registryKey.GetValue(string_34)) != null)
					{
						Class25.smethod_2($"GetRegistryKey(\"\\{registryKey_0.Name}\\{string_33}\\{string_34}\") returns \"{text}\"");
					}
					registryKey.Close();
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
			}
			return text;
		}
		return null;
	}

	private static string smethod_2(RegistryKey registryKey_0, string string_33, string string_34, string string_35)
	{
		if (!string.IsNullOrEmpty(string_33) && !string.IsNullOrEmpty(string_34) && !string.IsNullOrEmpty(string_35))
		{
			try
			{
				RegistryKey registryKey = registryKey_0.OpenSubKey(string_33, RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (registryKey == null)
				{
					registryKey = registryKey_0.CreateSubKey(string_33, RegistryKeyPermissionCheck.ReadWriteSubTree);
				}
				if (registryKey != null)
				{
					Class25.smethod_2($"SetRegistryKey(\"\\{registryKey_0.Name}\\{string_33}\\{string_34}\", \"{string_35}\")");
					registryKey.SetValue(string_34, string_35, RegistryValueKind.String);
					registryKey.Close();
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
			}
			return string_35;
		}
		return null;
	}

	private static bool smethod_3(RegistryKey registryKey_0, string string_33, string string_34)
	{
		if (!string.IsNullOrEmpty(string_33) && !string.IsNullOrEmpty(string_34))
		{
			try
			{
				RegistryKey registryKey = registryKey_0.OpenSubKey(string_33, RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (registryKey == null)
				{
					registryKey = registryKey_0.CreateSubKey(string_33, RegistryKeyPermissionCheck.ReadWriteSubTree);
				}
				if (registryKey != null)
				{
					Class25.smethod_2($"RemoveRegistryKey(\"\\{registryKey_0.Name}\\{string_33}\\{string_34}\"");
					registryKey.DeleteValue(string_34, throwOnMissingValue: false);
					registryKey.Close();
				}
				return true;
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
				return false;
			}
		}
		return false;
	}

	public static string smethod_4(string string_33, string string_34)
	{
		return smethod_1(Registry.LocalMachine, string_33, string_34);
	}

	public static string smethod_5(string string_33)
	{
		return smethod_1(Registry.LocalMachine, string_18, string_33);
	}

	public static string smethod_6(string string_33, string string_34)
	{
		return smethod_2(Registry.LocalMachine, string_18, string_33, string_34);
	}

	public static bool smethod_7(string string_33)
	{
		return smethod_3(Registry.LocalMachine, string_18, string_33);
	}

	public static string smethod_8(string string_33)
	{
		if (string.IsNullOrEmpty(string_33))
		{
			return null;
		}
		try
		{
			return Environment.GetEnvironmentVariable(string_33);
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return null;
	}

	public static string smethod_9(Version version_0)
	{
		return new DateTime(2000, 1, 1).AddDays(version_0.Build).AddSeconds(version_0.Revision * 2).ToString("yyyyMMdd.HHmm");
	}

	private static void smethod_10(string string_33)
	{
		if (string.IsNullOrEmpty(string_33))
		{
			return;
		}
		string strB = string_33.ToLower();
		string[] array = string_16;
		int num = 0;
		while (true)
		{
			if (num < array.Length)
			{
				string strA = array[num];
				if (string.Compare(strA, strB, ignoreCase: true) == 0)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		string_25 = strB;
	}

	static Class32()
	{
		string_16 = new string[11]
		{
			"cs", "de", "en", "es", "fr", "it", "ja", "ko", "pl", "ru",
			"zh"
		};
		string_17 = new string[20]
		{
			"provserver.televolution.net", "sip1.lakedestiny.cordiaip.com", "stun1.voiceeclipse.net", "stun.callwithus.com", "stun.counterpath.net", "stun.endigovoip.com", "stun.ekiga.net", "stun.ideasip.com", "stun.internetcalls.com", "stun.noc.ams-ix.net",
			"stun.phonepower.com", "stun.phoneserve.com", "stun.rnktel.com", "stun.softjoys.com", "stunserver.org", "stun.sipgate.net", "stun.voip.aebc.com", "stun.voipbuster.com", "numb.viagenie.ca", "stun.ipshka.com"
		};
		bool_0 = false;
		bool_1 = false;
		ipaddress_0 = IPAddress.Loopback;
		CultureInfo currentCulture = CultureInfo.CurrentCulture;
		string_25 = "en";
		smethod_10(currentCulture.TwoLetterISOLanguageName);
		try
		{
			ipaddress_0 = Statistics.GetLocalIPAddress();
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		Class25.smethod_2($"LocalIPAddress = {ipaddress_0}");
		Version version = Assembly.GetExecutingAssembly().GetName().Version;
		string_23 = smethod_9(version);
		Class25.smethod_2($"AssemblyVersion = {string_23}");
		switch (Environment.OSVersion.Platform)
		{
		case PlatformID.Win32S:
			string_24 = "Win32s ";
			break;
		case PlatformID.Win32Windows:
			string_24 = "Windows 9x";
			break;
		case PlatformID.Win32NT:
			string_24 = "Windows NT ";
			break;
		case PlatformID.WinCE:
			string_24 = "Windows CE ";
			break;
		case PlatformID.Unix:
			string_24 = "Unix ";
			break;
		case PlatformID.Xbox:
			string_24 = "Xbox 360 ";
			break;
		case PlatformID.MacOSX:
			string_24 = "MacOSX ";
			break;
		}
		string_24 += $"{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}.{Environment.OSVersion.Version.Build}";
		string_22 = "User-Agent: UpLauncher/" + Application.ProductVersion + " (Windows; N; " + string_24 + "; " + currentCulture.IetfLanguageTag + ") UpLauncher/" + string_23;
		Class25.smethod_2(string_22);
		string_18 = "SOFTWARE\\Atari\\TDU2\\";
		if (!smethod_0(Registry.LocalMachine, string_18))
		{
			string_18 = "SOFTWARE\\Wow6432Node\\Atari\\TDU2\\";
			if (!smethod_0(Registry.LocalMachine, string_18))
			{
				Class25.smethod_3("Error: Registry keys not found");
				string_18 = null;
			}
		}
		isteamAPI_0 = new ISteamAPI();
		if (isteamAPI_0.DetectCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath)))
		{
			bool_1 = true;
			Class25.smethod_2($"Steam API detected into directory \"{Path.GetDirectoryName(Application.ExecutablePath)}\"");
			string_18 += "Steam";
		}
		string_27 = smethod_5("InstallDir");
		if (!string.IsNullOrEmpty(string_27) && !string_27.EndsWith("\\"))
		{
			string_27 += "\\";
		}
		string_31 = string_27 + "UpLauncher.localdb";
		string_29 = smethod_5("StatsServerRoot");
		if (string.IsNullOrEmpty(string_29))
		{
			string_29 = "http://clientstats.testdriveunlimited2.com/final/";
		}
		if (!string_29.EndsWith("/"))
		{
			string_29 += "/";
		}
		string_28 = "http://patchserver.testdriveunlimited2.com/final/";
		if (!string_28.EndsWith("/"))
		{
			string_28 += "/";
		}
		string_30 = string_28 + "UpLauncher.dat";
		Class25.smethod_2($"StringHttpServerRoot = {string_28}");
		Class25.smethod_2($"StringStatsServerRoot = {string_29}");
		Class25.smethod_2($"StringRemoteFileDatabase = {string_30}");
		Class25.smethod_2($"CultureInfo = {currentCulture.ToString()}");
		string text = smethod_5("Language");
		if (!string.IsNullOrEmpty(text))
		{
			smethod_10(text);
		}
		string_19 = smethod_5("ExePath");
		string_20 = smethod_5("GameProductVersion");
		string_21 = smethod_5("GameBuildVersion");
		if (string.IsNullOrEmpty(string_21))
		{
			string_21 = "0";
		}
		smethod_15();
		smethod_11();
		string_26 = smethod_5("GUID");
		if (string.IsNullOrEmpty(string_26))
		{
			string_26 = Guid.NewGuid().ToString();
			smethod_6("GUID", string_26);
		}
		natResolver_0 = new NatResolver();
		natResolver_0.Begin();
		gameServersStatus_0 = new GameServersStatus();
		keyChecker_0 = new KeyChecker();
		statistics_0 = new Statistics();
	}

	public static bool smethod_11()
	{
		try
		{
			if (!string.IsNullOrEmpty(string_19) && File.Exists(string_19))
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(string_19);
				string_20 = versionInfo.ProductVersion;
				smethod_6("GameProductVersion", string_20);
				string path = string_27 + "BuildVersion.dat";
				if (File.Exists(path))
				{
					BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read));
					int num = binaryReader.ReadInt32();
					binaryReader.Close();
					string_21 = num.ToString();
					smethod_6("GameBuildVersion", string_21);
				}
				return true;
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return false;
	}

	public static DialogResult smethod_12(ErrorCode errorCode_0, ErrorCodeEx errorCodeEx_0)
	{
		DialogResult result = DialogResult.None;
		MessageBoxButtons a_MessageBoxButtons = MessageBoxButtons.RetryCancel;
		string text = smethod_13(errorCode_0.ToString());
		if (!string.IsNullOrEmpty(text))
		{
			switch (errorCode_0)
			{
			case ErrorCode.ErrorKeyBanned:
				a_MessageBoxButtons = MessageBoxButtons.OK;
				break;
			case ErrorCode.ErrorDiskSpace:
			{
				Class33.smethod_0().GetProgressInfos(out var a_rGlobalProgressInfo);
				text = string.Format(text, string_27, smethod_14(a_rGlobalProgressInfo.TotalRequiredBytes));
				break;
			}
			case ErrorCode.ErrorInstallFile:
				text = string.Format(text, string_27);
				break;
			case ErrorCode.ErrorMinimumRequirements:
				text = string.Format(text, smethod_14(1610612736uL), smethod_14(536870912uL), smethod_14(statistics_0.MemoryTotalPhysical), smethod_14(statistics_0.VideoControllerAdapterRAM));
				break;
			case ErrorCode.ErrorRecoverFile:
			case ErrorCode.ErrorGameInstall:
				a_MessageBoxButtons = MessageBoxButtons.OK;
				break;
			}
			text += $"\n\n[ERROR #{(uint)errorCodeEx_0:x2}]";
			result = PopupForm.Show(text, a_MessageBoxButtons);
		}
		return result;
	}

	public static string smethod_13(string string_33)
	{
		string name = string_25 + "_" + string_33;
		return LocalizedStrings.ResourceManager.GetString(name);
	}

	public static string smethod_14(ulong ulong_6)
	{
		string text = "UnitBytes";
		string text2 = "";
		double num = ulong_6;
		double num2 = 1.0;
		if (ulong_6 > 1099511627776L)
		{
			num2 = 1099511627776.0;
			text = "UnitTeraBytes";
		}
		else if (ulong_6 > 1073741824L)
		{
			num2 = 1073741824.0;
			text = "UnitGigaBytes";
		}
		else if (ulong_6 > 1048576L)
		{
			num2 = 1048576.0;
			text = "UnitMegaBytes";
		}
		else if (ulong_6 > 1024L)
		{
			num2 = 1024.0;
			text = "UnitKiloBytes";
		}
		else
		{
			text2 = ulong_6.ToString();
			text = "UnitBytes";
		}
		text2 = (num / num2).ToString("F");
		text2 = text2.TrimEnd('0');
		text2 = text2.TrimEnd('.');
		return text2 + " " + smethod_13(text);
	}

	public static void smethod_15()
	{
		string text = smethod_5("AudioLib");
		if (string.IsNullOrEmpty(text))
		{
			smethod_16(AudioMode.DirectSound);
		}
		else if (string.Compare(text, "XAudio", ignoreCase: true) == 0)
		{
			audioMode_0 = AudioMode.XAudio2;
		}
		else
		{
			audioMode_0 = AudioMode.DirectSound;
		}
	}

	public static void smethod_16(AudioMode audioMode_1)
	{
		audioMode_0 = audioMode_1;
		smethod_6("AudioLib", audioMode_1.ToString());
	}

	public static void smethod_17(string[] string_33)
	{
		string_32 = string_33;
	}

	public static string smethod_18()
	{
		StringBuilder stringBuilder = new StringBuilder();
		string[] array = string_32;
		foreach (string text in array)
		{
			stringBuilder.Append(text + " ");
		}
		stringBuilder.Append("-audiolib " + audioMode_0);
		return stringBuilder.ToString();
	}
}
