using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace UPnPDetection.UPnP;

public sealed class UPnPDetector
{
	public delegate void SearchHandler(IPEndPoint ResponseFromEndPoint, IPEndPoint ResponseReceivedOnEndPoint, Uri DescriptionLocation, string USN, string SearchTarget, int MaxAge);

	[CompilerGenerated]
	private sealed class Class35
	{
		public IPEndPoint ipendPoint_0;

		public Uri uri_0;

		public bool method_0(UPnPRootDevice upnPRootDevice_0)
		{
			if (upnPRootDevice_0.RemoteEndPoint.Equals(ipendPoint_0))
			{
				return upnPRootDevice_0.DescriptionLocation.Equals(uri_0);
			}
			return false;
		}
	}

	private const int int_0 = 3000;

	private const string string_0 = "upnp:rootdevice";

	private SearchHandler searchHandler_0;

	private static int int_1 = 5;

	private UPnPSessionManager upnPSessionManager_0;

	private UPnPDetectionFlow upnPDetectionFlow_0;

	private UPnPState upnPState_0;

	private List<UPnPRootDevice> list_0;

	private int int_2;

	private BackgroundWorker backgroundWorker_0;

	public static int MX
	{
		get
		{
			return int_1;
		}
		set
		{
			if (value > 0)
			{
				int_1 = value;
			}
		}
	}

	public UPnPSessionManager Sessions => upnPSessionManager_0;

	public UPnPDetectionFlow DetectionState => upnPDetectionFlow_0;

	public UPnPState State => upnPState_0;

	public List<UPnPRootDevice> RootDevices => list_0;

	private event SearchHandler _OnSearch
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			searchHandler_0 = (SearchHandler)Delegate.Combine(searchHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			searchHandler_0 = (SearchHandler)Delegate.Remove(searchHandler_0, value);
		}
	}

	public UPnPDetector()
	{
		upnPDetectionFlow_0 = UPnPDetectionFlow.Idle;
		upnPState_0 = UPnPState.UPnPNotTested;
		upnPSessionManager_0 = new UPnPSessionManager();
		list_0 = new List<UPnPRootDevice>();
		searchHandler_0 = (SearchHandler)Delegate.Combine(searchHandler_0, new SearchHandler(method_3));
		backgroundWorker_0 = new BackgroundWorker();
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.RunWorkerCompleted += backgroundWorker_0_RunWorkerCompleted;
		backgroundWorker_0.WorkerSupportsCancellation = true;
		int_2 = 0;
	}

	public void FindDevicesAsync()
	{
		upnPDetectionFlow_0 = UPnPDetectionFlow.Detecting;
		UPnPTraceLog.Log("Start finding UPnP devices on IP V4 network");
		method_0("upnp:rootdevice", UdpAddresses.UpnpMulticastV4EndPoint);
		backgroundWorker_0.RunWorkerAsync(this);
	}

	public void AddPortMapping()
	{
		lock (list_0)
		{
			for (int i = int_2; i < list_0.Count; i++)
			{
				lock (list_0[i])
				{
					if (list_0[i].State == UPnPState.UPnPDetectedMatching)
					{
						int_2 = i + 1;
						list_0[i].PortMappingFinished += method_7;
						list_0[i].AddPortMapping();
						return;
					}
				}
			}
		}
		UPnPTraceLog.Error("No root device was able to map the required port successfully");
		upnPDetectionFlow_0 = UPnPDetectionFlow.Done;
		upnPState_0 = UPnPState.UPnPMappingFailed;
	}

	private void method_0(string string_1, IPEndPoint ipendPoint_0)
	{
		HTTPMessage hTTPMessage = new HTTPMessage();
		hTTPMessage.Directive = "M-SEARCH";
		hTTPMessage.DirectiveObj = "*";
		hTTPMessage.AddTag("ST", string_1);
		hTTPMessage.AddTag("MX", MX.ToString());
		hTTPMessage.AddTag("MAN", "\"ssdp:discover\"");
		if (ipendPoint_0.AddressFamily == AddressFamily.InterNetwork)
		{
			hTTPMessage.AddTag("HOST", ipendPoint_0.ToString());
		}
		else if (ipendPoint_0.AddressFamily == AddressFamily.InterNetworkV6)
		{
			hTTPMessage.AddTag("HOST", $"[{ipendPoint_0.Address.ToString()}]:{ipendPoint_0.Port}");
		}
		byte[] bytes = Encoding.UTF8.GetBytes(hTTPMessage.StringPacket);
		string hostName = Dns.GetHostName();
		IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
		IPAddress[] addressList = hostEntry.AddressList;
		IPAddress[] array = addressList;
		foreach (IPAddress iPAddress in array)
		{
			try
			{
				UPnPSession uPnPSession = upnPSessionManager_0.GetSession(iPAddress);
				if (uPnPSession == null)
				{
					uPnPSession = new UPnPSession(new UdpClient(new IPEndPoint(iPAddress, 0)), iPAddress);
					uPnPSession.Udp.EnableBroadcast = true;
					uPnPSession.Udp.BeginReceive(method_1, uPnPSession);
					upnPSessionManager_0.Add(uPnPSession);
				}
				if (ipendPoint_0.AddressFamily == uPnPSession.Udp.Client.AddressFamily && (ipendPoint_0.AddressFamily != AddressFamily.InterNetworkV6 || !((IPEndPoint)uPnPSession.Udp.Client.LocalEndPoint).Address.IsIPv6LinkLocal || ipendPoint_0 == UdpAddresses.UpnpMulticastV6EndPoint2) && (ipendPoint_0.AddressFamily != AddressFamily.InterNetworkV6 || ((IPEndPoint)uPnPSession.Udp.Client.LocalEndPoint).Address.IsIPv6LinkLocal || ipendPoint_0 == UdpAddresses.UpnpMulticastV6EndPoint1))
				{
					_ = (IPEndPoint)uPnPSession.Udp.Client.LocalEndPoint;
					if (uPnPSession.Udp.Client.AddressFamily == AddressFamily.InterNetwork)
					{
						uPnPSession.Udp.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, iPAddress.GetAddressBytes());
					}
					else if (uPnPSession.Udp.Client.AddressFamily == AddressFamily.InterNetworkV6)
					{
						uPnPSession.Udp.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastInterface, BitConverter.GetBytes((int)iPAddress.ScopeId));
					}
					uPnPSession.Udp.Send(bytes, bytes.Length, ipendPoint_0);
					uPnPSession.Udp.Send(bytes, bytes.Length, ipendPoint_0);
				}
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"_FindDeviceAsync {iPAddress}: {ex.Source} - {ex.Message}");
			}
		}
	}

	private void method_1(IAsyncResult iasyncResult_0)
	{
		UPnPTraceLog.Log("Device answer handled");
		IPEndPoint remoteEP = null;
		UPnPSession uPnPSession = iasyncResult_0.AsyncState as UPnPSession;
		try
		{
			byte[] array = uPnPSession.Udp.EndReceive(iasyncResult_0, ref remoteEP);
			if (array != null)
			{
				lock (uPnPSession)
				{
					uPnPSession.Update();
					method_2(array, remoteEP, (IPEndPoint)uPnPSession.Udp.Client.LocalEndPoint);
					uPnPSession.Udp.BeginReceive(method_1, uPnPSession);
					return;
				}
			}
		}
		catch (Exception)
		{
		}
		lock (upnPSessionManager_0)
		{
			upnPSessionManager_0.Remove(uPnPSession.Ip);
		}
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		do
		{
			Thread.Sleep(10);
			lock (upnPSessionManager_0)
			{
				upnPSessionManager_0.CleanTimedOut();
			}
		}
		while (upnPSessionManager_0.Count > 0);
		UPnPTraceLog.Log("UPnP Session cleaned. Starting devices notifications");
	}

	private void method_2(byte[] byte_0, IPEndPoint ipendPoint_0, IPEndPoint ipendPoint_1)
	{
		HTTPMessage hTTPMessage;
		try
		{
			hTTPMessage = HTTPMessage.ParseByteArray(byte_0, 0, byte_0.Length);
		}
		catch
		{
			hTTPMessage = new HTTPMessage();
			hTTPMessage.Directive = "---";
			hTTPMessage.DirectiveObj = "---";
			hTTPMessage.BodyBuffer = byte_0;
		}
		hTTPMessage.LocalEndPoint = ipendPoint_1;
		hTTPMessage.RemoteEndPoint = ipendPoint_0;
		DText dText = new DText();
		string tag = hTTPMessage.GetTag("Location");
		int maxAge = 0;
		string text = hTTPMessage.GetTag("Cache-Control").Trim();
		if (text != "")
		{
			dText.ATTRMARK = ",";
			dText.MULTMARK = "=";
			dText[0] = text;
			for (int i = 1; i <= dText.DCOUNT(); i++)
			{
				if (dText[i, 1].Trim().ToUpper() == "MAX-AGE")
				{
					maxAge = int.Parse(dText[i, 2].Trim());
					break;
				}
			}
		}
		text = hTTPMessage.GetTag("USN");
		string text2 = text.Substring(text.IndexOf(":") + 1);
		string tag2 = hTTPMessage.GetTag("ST");
		if (text2.IndexOf("::") != -1)
		{
			text2 = text2.Substring(0, text2.IndexOf("::"));
		}
		if (searchHandler_0 != null)
		{
			searchHandler_0(hTTPMessage.RemoteEndPoint, hTTPMessage.LocalEndPoint, new Uri(tag), text2, tag2, maxAge);
		}
	}

	private void method_3(IPEndPoint ipendPoint_0, IPEndPoint ipendPoint_1, Uri uri_0, string string_1, string string_2, int int_3)
	{
		lock (list_0)
		{
			UPnPRootDevice uPnPRootDevice = list_0.Find((UPnPRootDevice upnPRootDevice_0) => upnPRootDevice_0.RemoteEndPoint.Equals(ipendPoint_0) && upnPRootDevice_0.DescriptionLocation.Equals(uri_0));
			if (uPnPRootDevice == null)
			{
				UPnPTraceLog.Log($"New root device detected at: {ipendPoint_0.ToString()}");
				list_0.Add(new UPnPRootDevice(ipendPoint_1, ipendPoint_0, uri_0));
			}
		}
	}

	private void backgroundWorker_0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		lock (list_0)
		{
			UPnPTraceLog.Log($"{list_0.Count} Root devices were found");
			if (list_0.Count == 0)
			{
				upnPDetectionFlow_0 = UPnPDetectionFlow.Done;
				upnPState_0 = UPnPState.UPnPNotDetected;
				return;
			}
			upnPDetectionFlow_0 = UPnPDetectionFlow.Notifying;
			foreach (UPnPRootDevice item in list_0)
			{
				UPnPTraceLog.Log($"Get description of device: {item.RemoteEndPoint.ToString()}");
				item.DiscoveryFinished += method_4;
				item.GetDescription();
			}
		}
	}

	private void method_4(UPnPRootDevice upnPRootDevice_0)
	{
		UPnPTraceLog.Log($"Root device {upnPRootDevice_0.RemoteEndPoint.ToString()} is set up");
		bool flag = true;
		lock (list_0)
		{
			foreach (UPnPRootDevice item in list_0)
			{
				if (!item.DevicesDiscoveryFinished)
				{
					flag = false;
				}
			}
		}
		if (!flag)
		{
			return;
		}
		Console.WriteLine("UPnPDetection: _OnDiscoveryFinished" + DateTime.Now.ToString("HH:mm:ss"));
		method_6();
		bool flag2 = false;
		lock (list_0)
		{
			foreach (UPnPRootDevice item2 in list_0)
			{
				lock (item2)
				{
					if (item2.State == UPnPState.UPnPDetectedNotTested)
					{
						flag2 = true;
						item2.NotificationFinished += method_5;
						item2.StartDevicesNotification();
					}
				}
			}
		}
		if (!flag2)
		{
			upnPDetectionFlow_0 = UPnPDetectionFlow.Done;
		}
	}

	private void method_5()
	{
		bool flag = true;
		lock (list_0)
		{
			foreach (UPnPRootDevice item in list_0)
			{
				lock (item)
				{
					if (!item.DevicesNotificationFinished)
					{
						flag = false;
					}
				}
			}
		}
		if (flag)
		{
			Console.WriteLine("UPnPDetection: _OnNotificationFinished" + DateTime.Now.ToString("HH:mm:ss"));
			method_6();
			if (upnPState_0 == UPnPState.UPnPDetectedMatching)
			{
				upnPDetectionFlow_0 = UPnPDetectionFlow.Mapping;
			}
			else
			{
				upnPDetectionFlow_0 = UPnPDetectionFlow.Done;
			}
		}
	}

	private void method_6()
	{
		UPnPTraceLog.Log("Updating UPnP status");
		UPnPState uPnPState = UPnPState.UPnPNotDetected;
		lock (list_0)
		{
			foreach (UPnPRootDevice item in list_0)
			{
				lock (item)
				{
					if (item.State > uPnPState)
					{
						uPnPState = item.State;
					}
				}
			}
		}
		UPnPTraceLog.Log($"UPnP State: {uPnPState}");
		upnPState_0 = uPnPState;
	}

	private void method_7(UPnPRootDevice upnPRootDevice_0)
	{
		if (upnPRootDevice_0.MappingState == UPnPMappinPortFlow.Failed)
		{
			UPnPTraceLog.Log($"Root device {upnPRootDevice_0.RemoteEndPoint.ToString()} could not map the required port, trying on the next root device");
			AddPortMapping();
		}
		else
		{
			UPnPTraceLog.Log($"Root device {upnPRootDevice_0.RemoteEndPoint.ToString()} mapped the required port successfully");
			upnPDetectionFlow_0 = UPnPDetectionFlow.Done;
			upnPState_0 = UPnPState.UPnPMapped;
		}
	}
}
