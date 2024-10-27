using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace UpLauncher;

public class Statistics
{
	public enum eStatus
	{
		Unchecked,
		Failed,
		Ok
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	private class Class3
	{
		public uint uint_0;

		public uint uint_1;

		public ulong ulong_0;

		public ulong ulong_1;

		public ulong ulong_2;

		public ulong ulong_3;

		public ulong ulong_4;

		public ulong ulong_5;

		public ulong ulong_6;

		public Class3()
		{
			uint_0 = (uint)Marshal.SizeOf(typeof(Class3));
		}
	}

	private eStatus eStatus_0;

	private WebRequest webRequest_0;

	private BackgroundWorker backgroundWorker_0;

	public string ProcessorName;

	public ushort ProcessorArchitecture;

	public uint ProcessorClockSpeed;

	public uint ProcessorNumberOfCores;

	public uint ProcessorNumberOfLogicalProcessors;

	public ulong MemoryTotalPhysical;

	public string OperatingSystemName;

	public ushort OperatingSystemArchitecture;

	public uint OperatingSystemSKU;

	public ushort OperatingSystemType;

	public ushort OperatingSystemLanguage;

	public ushort OperatingSystemServicePackMajorVersion;

	public ushort OperatingSystemServicePackMinorVersion;

	public string OperatingSystemVersion;

	public string OperatingSystemCountryCode;

	public ushort OperatingSystemLocale;

	public short OperatingSystemCurrentTimeZone;

	public string VideoControllerName;

	public string VideoControllerProcessor;

	public string VideoControllerDriverVersion;

	public ulong VideoControllerAdapterRAM;

	public uint VideoControllerHorizontalResolution;

	public uint VideoControllerVerticalResolution;

	public uint VideoControllerBitsPerPixel;

	public string VideoControllerDirectXVersion;

	public string NetworkControllerName;

	public ushort NetworkAdapterType;

	public string NetworkControllerMacAddress;

	public ulong NetworkControllerSpeed;

	public string NetworkNatType;

	public uint GameLastPlayerId;

	public string GameKey;

	public eStatus Status => eStatus_0;

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GlobalMemoryStatusEx([In][Out] Class3 class3_0);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern int GetLocaleInfoEx(string lpLocaleName, uint LCType, StringBuilder lpLCData, int cchData);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern int GetLocaleInfo(int Locale, uint LCType, StringBuilder lpLCData, int cchData);

	public Statistics()
	{
		eStatus_0 = eStatus.Unchecked;
		webRequest_0 = null;
		ProcessorName = null;
		ProcessorArchitecture = 32;
		ProcessorClockSpeed = 0u;
		ProcessorNumberOfCores = 1u;
		ProcessorNumberOfLogicalProcessors = 1u;
		MemoryTotalPhysical = 0uL;
		OperatingSystemName = null;
		OperatingSystemArchitecture = 32;
		OperatingSystemSKU = 0u;
		OperatingSystemType = 0;
		OperatingSystemLanguage = 0;
		OperatingSystemServicePackMajorVersion = 0;
		OperatingSystemServicePackMinorVersion = 0;
		OperatingSystemVersion = null;
		OperatingSystemCountryCode = null;
		OperatingSystemLocale = 0;
		OperatingSystemCurrentTimeZone = 0;
		VideoControllerName = null;
		VideoControllerProcessor = null;
		VideoControllerDriverVersion = null;
		VideoControllerAdapterRAM = 0uL;
		VideoControllerHorizontalResolution = 0u;
		VideoControllerVerticalResolution = 0u;
		VideoControllerBitsPerPixel = 0u;
		VideoControllerDirectXVersion = null;
		NetworkControllerName = null;
		NetworkAdapterType = 0;
		NetworkControllerMacAddress = null;
		NetworkControllerSpeed = 0uL;
		NetworkNatType = null;
		GameLastPlayerId = 0u;
		GameKey = null;
	}

	public void Begin()
	{
		backgroundWorker_0 = new BackgroundWorker();
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.RunWorkerCompleted += backgroundWorker_0_RunWorkerCompleted;
		backgroundWorker_0.RunWorkerAsync(this);
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		Class25.smethod_2("Statistics._DoWork() begins");
		if (method_1())
		{
			method_18();
		}
		Class25.smethod_2($"Statistics._DoWork() ends with status {eStatus_0.ToString()}");
	}

	private void backgroundWorker_0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		if (e.Error != null)
		{
			eStatus_0 = eStatus.Failed;
		}
		else if (e.Cancelled)
		{
			eStatus_0 = eStatus.Failed;
		}
		backgroundWorker_0 = null;
	}

	private string method_0(string string_0)
	{
		string[] array = string_0.Split('.');
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].TrimStart('0');
			if (string.IsNullOrEmpty(text))
			{
				text = "0";
			}
			string_0 = ((i != 0) ? (string_0 + text) : text);
			if (i + 1 != array.Length)
			{
				string_0 += ".";
			}
		}
		return string_0.Trim();
	}

	private bool method_1()
	{
		Class25.smethod_2("Statistics.CollectInfo()");
		try
		{
			method_2();
			method_13();
			method_14();
			method_15();
			method_16();
			method_17();
			string text = Class32.smethod_5("GameLastPlayerId");
			if (!string.IsNullOrEmpty(text))
			{
				GameLastPlayerId = uint.Parse(text);
			}
			string path = Class32.string_27 + "key.txt";
			if (File.Exists(path))
			{
				byte[] bytes = File.ReadAllBytes(path);
				GameKey = Encoding.ASCII.GetString(bytes);
				if (!string.IsNullOrEmpty(GameKey))
				{
					GameKey = GameKey.Trim();
					GameKey = GameKey.Substring(0, 19);
				}
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			eStatus_0 = eStatus.Failed;
		}
		return eStatus_0 != eStatus.Failed;
	}

	public bool CheckMinimalRequirement(out ErrorCode errorCode, out ErrorCodeEx errorCodeEx)
	{
		errorCode = ErrorCode.NoError;
		errorCodeEx = ErrorCodeEx.NoError;
		if (MemoryTotalPhysical < 1610612736L)
		{
			errorCode = ErrorCode.ErrorMinimumRequirements;
			errorCodeEx = ErrorCodeEx.ErrorMinimalRequirementRAM;
		}
		if (VideoControllerAdapterRAM < 536870912L)
		{
			errorCode = ErrorCode.ErrorMinimumRequirements;
			errorCodeEx = ErrorCodeEx.ErrorMinimalRequirementVRAM;
		}
		return errorCode == ErrorCode.NoError;
	}

	private bool method_2()
	{
		Class25.smethod_2("Statistics.CollectInfoDxDiag()");
		try
		{
			DxDiag dxDiag = new DxDiag();
			dxDiag.XmlPath = Class32.string_27 + "\\dxdiag.xml";
			if (!dxDiag.CollectInfo())
			{
				return false;
			}
			OperatingSystemName = dxDiag.GetValue("SystemInformation", "OperatingSystem");
			ProcessorName = dxDiag.GetValue("SystemInformation", "Processor");
			MemoryTotalPhysical = dxDiag.GetValueUInt("SystemInformation", "Memory") * 1048576L;
			VideoControllerDirectXVersion = dxDiag.GetValue("SystemInformation", "DirectXVersion");
			VideoControllerName = dxDiag.GetValue("DisplayDevice", "CardName");
			VideoControllerProcessor = dxDiag.GetValue("DisplayDevice", "ChipType");
			VideoControllerAdapterRAM = dxDiag.GetValueUInt("DisplayDevice", "DisplayMemory") * 1048576L;
			VideoControllerDriverVersion = dxDiag.GetValue("DisplayDevice", "DriverVersion");
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return true;
	}

	private object method_3(ManagementObject managementObject_0, string string_0)
	{
		if (managementObject_0 != null && !string.IsNullOrEmpty(string_0))
		{
			foreach (PropertyData property in managementObject_0.Properties)
			{
				if (!string.IsNullOrEmpty(property.Name) && string.Compare(property.Name, string_0, ignoreCase: true) == 0)
				{
					return property.Value;
				}
			}
		}
		return null;
	}

	private string method_4(ManagementObject managementObject_0, string string_0)
	{
		return method_3(managementObject_0, string_0)?.ToString();
	}

	private ushort method_5(ManagementObject managementObject_0, string string_0, ushort ushort_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			return ushort_0;
		}
		return ushort.Parse(obj.ToString());
	}

	private short method_6(ManagementObject managementObject_0, string string_0, short short_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			return short_0;
		}
		return short.Parse(obj.ToString());
	}

	private ushort method_7(ManagementObject managementObject_0, string string_0, ushort ushort_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			return ushort_0;
		}
		return Convert.ToUInt16("0x" + obj.ToString(), 16);
	}

	private uint method_8(ManagementObject managementObject_0, string string_0, uint uint_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			return uint_0;
		}
		return uint.Parse(obj.ToString());
	}

	private int method_9(ManagementObject managementObject_0, string string_0, int int_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			return int_0;
		}
		return int.Parse(obj.ToString());
	}

	private int method_10(ManagementObject managementObject_0, string string_0, string string_1, int int_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			obj = method_3(managementObject_0, string_1);
		}
		if (obj == null)
		{
			return int_0;
		}
		return int.Parse(obj.ToString());
	}

	private ulong method_11(ManagementObject managementObject_0, string string_0, ulong ulong_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			return ulong_0;
		}
		return ulong.Parse(obj.ToString());
	}

	private ulong method_12(ManagementObject managementObject_0, string string_0, string string_1, ulong ulong_0)
	{
		object obj = method_3(managementObject_0, string_0);
		if (obj == null)
		{
			obj = method_3(managementObject_0, string_1);
		}
		if (obj == null)
		{
			return ulong_0;
		}
		return ulong.Parse(obj.ToString());
	}

	private void method_13()
	{
		Class25.smethod_2("Statistics.ReadProcessorInfo()");
		try
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementObject managementObject_ = (ManagementObject)managementObjectEnumerator.Current;
				ProcessorName = method_4(managementObject_, "Name");
				ProcessorArchitecture = method_5(managementObject_, "AddressWidth", 32);
				ProcessorClockSpeed = method_8(managementObject_, "MaxClockSpeed", 0u);
				ProcessorNumberOfCores = method_8(managementObject_, "NumberOfCores", 1u);
				ProcessorNumberOfLogicalProcessors = method_8(managementObject_, "NumberOfLogicalProcessors", 1u);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		if (string.IsNullOrEmpty(ProcessorName))
		{
			ProcessorName = Class32.smethod_4("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "ProcessorNameString");
			if (string.IsNullOrEmpty(ProcessorName))
			{
				ProcessorName = Class32.smethod_8("PROCESSOR_IDENTIFIER");
			}
			string text = Class32.smethod_8("PROCESSOR_ARCHITECTURE");
			if (!string.IsNullOrEmpty(text) && text.IndexOf("64") >= 0)
			{
				ProcessorArchitecture = 64;
			}
			string text2 = Class32.smethod_4("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "~MHz");
			if (!string.IsNullOrEmpty(text2))
			{
				ProcessorClockSpeed = uint.Parse(text2);
			}
			ProcessorNumberOfCores = (ProcessorNumberOfLogicalProcessors = (uint)Environment.ProcessorCount);
		}
		if (string.IsNullOrEmpty(ProcessorName))
		{
			ProcessorName = "Unknown";
		}
		ProcessorName = ProcessorName.Trim();
	}

	private void method_14()
	{
		Class25.smethod_2("Statistics.ReadMemoryInfo()");
		try
		{
			Class25.smethod_2("Searching Win32_ComputerSystem...");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementObject managementObject_ = (ManagementObject)managementObjectEnumerator.Current;
				MemoryTotalPhysical = method_11(managementObject_, "TotalPhysicalMemory", 0uL);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		if (MemoryTotalPhysical != 0L)
		{
			return;
		}
		try
		{
			Class3 @class = new Class3();
			if (GlobalMemoryStatusEx(@class))
			{
				MemoryTotalPhysical = @class.ulong_0;
			}
		}
		catch (Exception arg2)
		{
			Class25.smethod_3($"Exception: {arg2}");
		}
	}

	private void method_15()
	{
		Class25.smethod_2("Statistics.ReadOperatingSystemInfo()");
		string text = "32 bits";
		OperatingSystemVersion = Environment.OSVersion.Version.ToString();
		OperatingSystemLanguage = (ushort)CultureInfo.CurrentCulture.LCID;
		OperatingSystemLocale = OperatingSystemLanguage;
		try
		{
			Class25.smethod_2("Searching Win32_OperatingSystem...");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementObject managementObject_ = (ManagementObject)managementObjectEnumerator.Current;
				OperatingSystemName = method_4(managementObject_, "Caption");
				text = method_4(managementObject_, "OSArchitecture");
				OperatingSystemSKU = method_8(managementObject_, "OperatingSystemSKU", 0u);
				OperatingSystemType = method_5(managementObject_, "OSType", 0);
				OperatingSystemLanguage = method_5(managementObject_, "OSLanguage", 0);
				OperatingSystemServicePackMajorVersion = method_5(managementObject_, "ServicePackMajorVersion", 0);
				OperatingSystemServicePackMinorVersion = method_5(managementObject_, "ServicePackMinorVersion", 0);
				OperatingSystemVersion = method_4(managementObject_, "Version");
				OperatingSystemCountryCode = method_4(managementObject_, "CountryCode");
				OperatingSystemLocale = method_7(managementObject_, "Locale", ushort.MaxValue);
				if (OperatingSystemLocale == ushort.MaxValue)
				{
					OperatingSystemLocale = OperatingSystemLanguage;
				}
				OperatingSystemCurrentTimeZone = method_6(managementObject_, "CurrentTimeZone", 0);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		if (string.IsNullOrEmpty(OperatingSystemCountryCode))
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			try
			{
				if (GetLocaleInfo(CultureInfo.CurrentCulture.LCID, 5u, stringBuilder, 64) > 0)
				{
					OperatingSystemCountryCode = stringBuilder.ToString();
				}
				else
				{
					Class25.smethod_3($"Error: GetLocaleInfo( \"{CultureInfo.CurrentCulture.LCID}\", LOCALE_ICOUNTRY)");
				}
			}
			catch (Exception arg2)
			{
				Class25.smethod_3($"Exception: {arg2}");
			}
			try
			{
				if (GetLocaleInfo(CultureInfo.CurrentCulture.LCID, 1u, stringBuilder, 64) > 0)
				{
					string value = "0x" + stringBuilder.ToString();
					OperatingSystemLanguage = Convert.ToUInt16(value, 16);
				}
				else
				{
					Class25.smethod_3($"Error: GetLocaleInfo( \"{CultureInfo.CurrentCulture.LCID}\", LOCALE_ILANGUAGE)");
				}
			}
			catch (Exception arg3)
			{
				Class25.smethod_3($"Exception: {arg3}");
			}
		}
		if (string.IsNullOrEmpty(OperatingSystemCountryCode))
		{
			OperatingSystemCountryCode = OperatingSystemLanguage.ToString();
		}
		if (!string.IsNullOrEmpty(text) && text.IndexOf("64") >= 0)
		{
			OperatingSystemArchitecture = 64;
		}
		else
		{
			OperatingSystemArchitecture = 32;
		}
		if (string.IsNullOrEmpty(OperatingSystemName))
		{
			OperatingSystemName = "Windows";
		}
		OperatingSystemName = OperatingSystemName.Trim();
		OperatingSystemVersion = method_0(OperatingSystemVersion);
	}

	private void method_16()
	{
		Class25.smethod_2("Statistics.ReadVideoControllerInfo()");
		try
		{
			Class25.smethod_2("Searching Win32_VideoController...");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementObject managementObject_ = (ManagementObject)managementObjectEnumerator.Current;
				VideoControllerName = method_4(managementObject_, "Name");
				VideoControllerProcessor = method_4(managementObject_, "VideoProcessor");
				VideoControllerDriverVersion = method_4(managementObject_, "DriverVersion");
				VideoControllerAdapterRAM = method_11(managementObject_, "AdapterRAM", 0uL);
				VideoControllerHorizontalResolution = method_8(managementObject_, "CurrentHorizontalResolution", 0u);
				VideoControllerVerticalResolution = method_8(managementObject_, "CurrentVerticalResolution", 0u);
				VideoControllerBitsPerPixel = method_8(managementObject_, "CurrentBitsPerPixel", 0u);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		if (string.IsNullOrEmpty(VideoControllerName))
		{
			VideoControllerName = "Unknown";
		}
		if (string.IsNullOrEmpty(VideoControllerProcessor))
		{
			VideoControllerProcessor = "Unknown";
		}
		if (string.IsNullOrEmpty(VideoControllerDriverVersion))
		{
			VideoControllerDriverVersion = "0";
		}
		else
		{
			VideoControllerDriverVersion = method_0(VideoControllerDriverVersion);
		}
		VideoControllerName = VideoControllerName.Trim();
		VideoControllerProcessor = VideoControllerProcessor.Trim();
		try
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
			if (!string.IsNullOrEmpty(folderPath))
			{
				folderPath += "\\ddraw.dll";
				if (File.Exists(folderPath))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(folderPath);
					VideoControllerDirectXVersion = versionInfo.ProductVersion;
				}
			}
		}
		catch (Exception arg2)
		{
			Class25.smethod_3($"Exception: {arg2}");
		}
		if (string.IsNullOrEmpty(VideoControllerDirectXVersion))
		{
			VideoControllerDirectXVersion = Class32.smethod_4("SOFTWARE\\Microsoft\\DirectX", "Version");
			if (string.IsNullOrEmpty(VideoControllerDirectXVersion))
			{
				VideoControllerDirectXVersion = "0";
			}
		}
		VideoControllerDirectXVersion = method_0(VideoControllerDirectXVersion);
	}

	public static IPAddress GetLocalIPAddress()
	{
		IPAddress result = IPAddress.Loopback;
		try
		{
			IPPacketInformation iPPacketInformation = default(IPPacketInformation);
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			UnicastIPAddressInformationCollection unicastAddresses = allNetworkInterfaces[iPPacketInformation.Interface].GetIPProperties().UnicastAddresses;
			foreach (UnicastIPAddressInformation item in unicastAddresses)
			{
				if (item.Address.AddressFamily == AddressFamily.InterNetwork)
				{
					result = item.Address;
				}
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return result;
	}

	private void method_17()
	{
		Class25.smethod_2("Statistics.ReadNetworkControllerInfo()");
		string text = null;
		try
		{
			Class25.smethod_2("Searching Win32_IP4RouteTable...");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_IP4RouteTable WHERE Destination = '0.0.0.0'");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementObject managementObject_ = (ManagementObject)managementObjectEnumerator.Current;
				text = method_4(managementObject_, "NextHop");
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		if (!string.IsNullOrEmpty(text) && NetworkInterface.GetIsNetworkAvailable())
		{
			try
			{
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				NetworkInterface[] array = allNetworkInterfaces;
				foreach (NetworkInterface networkInterface in array)
				{
					if (NetworkControllerName != null)
					{
						break;
					}
					Class25.smethod_2($"NetworkInterface: \"{networkInterface.Description}\", Type={networkInterface.NetworkInterfaceType}, Speed={networkInterface.Speed}Mbps, Status={networkInterface.OperationalStatus}, Id={networkInterface.Id}");
					if (!networkInterface.Supports(NetworkInterfaceComponent.IPv4) || networkInterface.OperationalStatus != OperationalStatus.Up)
					{
						continue;
					}
					IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
					GatewayIPAddressInformationCollection gatewayAddresses = iPProperties.GatewayAddresses;
					foreach (GatewayIPAddressInformation item in gatewayAddresses)
					{
						if (item.Address.ToString() == text)
						{
							NetworkControllerName = networkInterface.Description;
							switch (networkInterface.NetworkInterfaceType)
							{
							case NetworkInterfaceType.Fddi:
								NetworkAdapterType = 2;
								break;
							case NetworkInterfaceType.TokenRing:
								NetworkAdapterType = 1;
								break;
							default:
								NetworkAdapterType = 0;
								break;
							case NetworkInterfaceType.Wireless80211:
								NetworkAdapterType = 9;
								break;
							case NetworkInterfaceType.Atm:
								NetworkAdapterType = 8;
								break;
							}
							NetworkControllerSpeed = (ulong)networkInterface.Speed;
							PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
							byte[] addressBytes = physicalAddress.GetAddressBytes();
							NetworkControllerMacAddress = $"{addressBytes[0]:X2}:{addressBytes[1]:X2}:{addressBytes[2]:X2}:{addressBytes[3]:X2}:{addressBytes[4]:X2}:{addressBytes[5]:X2}";
							Class25.smethod_2($"NetworkController: \"{NetworkControllerName}\", Speed={NetworkControllerSpeed}Mbps, MAC={NetworkControllerMacAddress}");
							break;
						}
					}
				}
			}
			catch (Exception arg2)
			{
				Class25.smethod_3($"Exception: {arg2}");
			}
		}
		if (string.IsNullOrEmpty(NetworkControllerName))
		{
			NetworkControllerName = "Unknown";
		}
		if (string.IsNullOrEmpty(NetworkControllerMacAddress))
		{
			NetworkControllerMacAddress = "00:00:00:00:00:00";
		}
		NetworkNatType = Class32.smethod_5("NetworkNatType");
		if (string.IsNullOrEmpty(NetworkNatType))
		{
			NetworkNatType = "UnChecked";
		}
	}

	private bool method_18()
	{
		Class25.smethod_2("Statistics.SendInfo()");
		string requestUriString = Class32.string_29 + "updateStats.php";
		string text = "";
		text = text + "GUID=" + Class32.string_26;
		text += "&";
		text = text + "ProcessorName=" + HttpUtility.UrlEncode(ProcessorName);
		text += "&";
		text = text + "ProcessorArchitecture=" + ProcessorArchitecture;
		text += "&";
		text = text + "ProcessorClockSpeed=" + ProcessorClockSpeed;
		text += "&";
		text = text + "ProcessorNumberOfCores=" + ProcessorNumberOfCores;
		text += "&";
		text = text + "ProcessorNumberOfLogicalProcessors=" + ProcessorNumberOfLogicalProcessors;
		text += "&";
		text = text + "MemoryTotalPhysical=" + MemoryTotalPhysical;
		text += "&";
		text = text + "OperatingSystemName=" + HttpUtility.UrlEncode(OperatingSystemName);
		text += "&";
		text = text + "OperatingSystemArchitecture=" + OperatingSystemArchitecture;
		text += "&";
		text = text + "OperatingSystemSKU=" + OperatingSystemSKU;
		text += "&";
		text = text + "OperatingSystemType=" + OperatingSystemType;
		text += "&";
		text = text + "OperatingSystemLanguage=" + OperatingSystemLanguage;
		text += "&";
		text = text + "OperatingSystemServicePackMajorVersion=" + OperatingSystemServicePackMajorVersion;
		text += "&";
		text = text + "OperatingSystemServicePackMinorVersion=" + OperatingSystemServicePackMinorVersion;
		text += "&";
		text = text + "OperatingSystemVersion=" + HttpUtility.UrlEncode(OperatingSystemVersion);
		text += "&";
		text = text + "OperatingSystemCountryCode=" + HttpUtility.UrlEncode(OperatingSystemCountryCode);
		text += "&";
		text = text + "OperatingSystemLocale=" + OperatingSystemLocale;
		text += "&";
		text = text + "OperatingSystemCurrentTimeZone=" + OperatingSystemCurrentTimeZone;
		text += "&";
		text = text + "VideoControllerName=" + HttpUtility.UrlEncode(VideoControllerName);
		text += "&";
		text = text + "VideoControllerProcessor=" + HttpUtility.UrlEncode(VideoControllerProcessor);
		text += "&";
		text = text + "VideoControllerDriverVersion=" + HttpUtility.UrlEncode(VideoControllerDriverVersion);
		text += "&";
		text = text + "VideoControllerAdapterRAM=" + VideoControllerAdapterRAM;
		text += "&";
		text = text + "VideoControllerHorizontalResolution=" + VideoControllerHorizontalResolution;
		text += "&";
		text = text + "VideoControllerVerticalResolution=" + VideoControllerVerticalResolution;
		text += "&";
		text = text + "VideoControllerBitsPerPixel=" + VideoControllerBitsPerPixel;
		text += "&";
		text = text + "VideoControllerDirectXVersion=" + HttpUtility.UrlEncode(VideoControllerDirectXVersion);
		text += "&";
		text = text + "NetworkControllerName=" + HttpUtility.UrlEncode(NetworkControllerName);
		text += "&";
		text = text + "NetworkAdapterType=" + NetworkAdapterType;
		text += "&";
		text = text + "NetworkControllerMacAddress=" + HttpUtility.UrlEncode(NetworkControllerMacAddress);
		text += "&";
		text = text + "NetworkControllerSpeed=" + NetworkControllerSpeed;
		text += "&";
		text = text + "NetworkNatType=" + HttpUtility.UrlEncode(NetworkNatType);
		text += "&";
		text = text + "GameLanguage=" + HttpUtility.UrlEncode(Class32.string_25);
		text += "&";
		text = text + "GameProductVersion=" + HttpUtility.UrlEncode(Class32.string_20);
		text += "&";
		text = text + "GameBuildVersion=" + HttpUtility.UrlEncode(Class32.string_21);
		text += "&";
		text = text + "UpLauncherVersion=" + HttpUtility.UrlEncode(Class32.string_23);
		text += "&";
		text = text + "GameLastPlayerId=" + GameLastPlayerId;
		text += "&";
		text = text + "GameKey=" + HttpUtility.UrlEncode(GameKey);
		try
		{
			ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
			byte[] bytes = aSCIIEncoding.GetBytes(text);
			webRequest_0 = WebRequest.Create(requestUriString);
			webRequest_0.Method = "POST";
			webRequest_0.ContentType = "application/x-www-form-urlencoded";
			webRequest_0.ContentLength = bytes.Length;
			Stream requestStream = webRequest_0.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			eStatus_0 = eStatus.Ok;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			eStatus_0 = eStatus.Failed;
		}
		return eStatus_0 == eStatus.Ok;
	}
}
