using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace UPnPDetection.UPnP;

public class UPnPRootDevice
{
	public delegate void DiscoveryFinishedHandler(UPnPRootDevice a_RootDevice);

	public delegate void NotificationFinishedHandler();

	public delegate void PortMappingFinishedHandler(UPnPRootDevice a_RootDevice);

	[CompilerGenerated]
	private sealed class Class38
	{
		public string string_0;

		public UPnPRootDevice upnPRootDevice_0;

		public UPnPDevice method_0(XElement xelement_0)
		{
			return UPnPDevice.Serialize(upnPRootDevice_0.a_LocalEndPoint, string_0, xelement_0);
		}
	}

	private const string string_0 = "InternetGatewayDevice";

	private DiscoveryFinishedHandler discoveryFinishedHandler_0;

	private NotificationFinishedHandler notificationFinishedHandler_0;

	private PortMappingFinishedHandler portMappingFinishedHandler_0;

	private IPEndPoint a_LocalEndPoint;

	private IPEndPoint a_RemoteEndPoint;

	private Uri a_DescriptionLocation;

	private Uri uri_0;

	private List<UPnPDevice> list_0;

	private bool bool_0;

	private bool bool_1;

	private UPnPState upnPState_0;

	private UPnPMappinPortFlow upnPMappinPortFlow_0;

	private bool bool_2;

	private int int_0;

	[CompilerGenerated]
	private static Func<XElement, bool> func_0;

	public IPEndPoint LocalEndPoint => a_LocalEndPoint;

	public IPEndPoint RemoteEndPoint => a_RemoteEndPoint;

	public Uri DescriptionLocation => a_DescriptionLocation;

	public Uri RootLocation => uri_0;

	public List<UPnPDevice> Devices
	{
		get
		{
			return list_0;
		}
		set
		{
			list_0 = value;
		}
	}

	public bool DevicesDiscoveryFinished => bool_0;

	public bool DevicesNotificationFinished => bool_1;

	public UPnPState State => upnPState_0;

	public UPnPMappinPortFlow MappingState => upnPMappinPortFlow_0;

	public bool RootDeviceChecked
	{
		get
		{
			return bool_2;
		}
		set
		{
			bool_2 = value;
		}
	}

	public event DiscoveryFinishedHandler DiscoveryFinished
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			discoveryFinishedHandler_0 = (DiscoveryFinishedHandler)Delegate.Combine(discoveryFinishedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			discoveryFinishedHandler_0 = (DiscoveryFinishedHandler)Delegate.Remove(discoveryFinishedHandler_0, value);
		}
	}

	public event NotificationFinishedHandler NotificationFinished
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			notificationFinishedHandler_0 = (NotificationFinishedHandler)Delegate.Combine(notificationFinishedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			notificationFinishedHandler_0 = (NotificationFinishedHandler)Delegate.Remove(notificationFinishedHandler_0, value);
		}
	}

	public event PortMappingFinishedHandler PortMappingFinished
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			portMappingFinishedHandler_0 = (PortMappingFinishedHandler)Delegate.Combine(portMappingFinishedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			portMappingFinishedHandler_0 = (PortMappingFinishedHandler)Delegate.Remove(portMappingFinishedHandler_0, value);
		}
	}

	public UPnPRootDevice(IPEndPoint a_LocalEndPoint, IPEndPoint a_RemoteEndPoint, Uri a_DescriptionLocation)
	{
		this.a_LocalEndPoint = a_LocalEndPoint;
		this.a_RemoteEndPoint = a_RemoteEndPoint;
		this.a_DescriptionLocation = a_DescriptionLocation;
		uri_0 = new Uri(a_DescriptionLocation.AbsoluteUri.Replace(a_DescriptionLocation.AbsolutePath, ""));
		upnPState_0 = UPnPState.UPnPNotTested;
		list_0 = new List<UPnPDevice>();
		int_0 = 0;
	}

	public void GetDescription()
	{
		try
		{
			WebClient webClient = new WebClient();
			webClient.DownloadDataCompleted += method_0;
			webClient.DownloadDataAsync(a_DescriptionLocation, "");
		}
		catch (Exception ex)
		{
			UPnPTraceLog.Error($"UPnP error GetDescription: {ex.Message}");
			upnPState_0 = UPnPState.UPnPFailed;
			bool_0 = true;
			if (discoveryFinishedHandler_0 != null)
			{
				discoveryFinishedHandler_0(this);
			}
		}
	}

	public void StartDevicesNotification()
	{
		foreach (UPnPDevice item in list_0)
		{
			lock (item)
			{
				item.OnNotifyFinished += method_1;
				item.Notify();
			}
		}
	}

	public void AddPortMapping()
	{
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.Idle;
		lock (list_0)
		{
			for (int i = int_0; i < list_0.Count; i++)
			{
				lock (list_0[i])
				{
					if (list_0[i].ManageUPnP)
					{
						UPnPTraceLog.Log($"UPnPRootDevice AddPortMapping: Requesting sub device {list_0[i].FriendlyName} to map the port");
						int_0 = i + 1;
						list_0[i].OnPortMappingFinished += method_3;
						list_0[i].AddPortMapping();
						return;
					}
				}
			}
		}
		UPnPTraceLog.Error("AddPortMapping: none of the root device children could manage the UPnP");
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		if (portMappingFinishedHandler_0 != null)
		{
			portMappingFinishedHandler_0(this);
		}
	}

	private void method_0(object sender, DownloadDataCompletedEventArgs e)
	{
		if (e.Error == null && !e.Cancelled)
		{
			string @string = Encoding.UTF8.GetString(e.Result);
			try
			{
				XDocument xDocument = XDocument.Parse(@string.Replace("\r\n", ""));
				XElement root = xDocument.Root;
				XName name = XName.Get("deviceList", root.Name.NamespaceName);
				XName name2 = XName.Get("device", root.Name.NamespaceName);
				XElement xElement = root.Element(name);
				string string_0 = uri_0.OriginalString;
				if (root.Element(root.Name.Namespace + "URLBase") != null)
				{
					string_0 = root.Element(root.Name.Namespace + "URLBase").Value;
				}
				if (xElement != null)
				{
					list_0 = (from xelement_0 in xElement.Elements(name2)
						where xelement_0.Name == "device" && xelement_0.Element(xelement_0.Name.Namespace + "deviceType").Value.Contains("InternetGatewayDevice")
						select UPnPDevice.Serialize(a_LocalEndPoint, string_0, xelement_0)).ToList();
				}
				else
				{
					XElement xElement2 = root.Element(name2);
					if (xElement2.Element(xElement2.Name.Namespace + "deviceType").Value.Contains("InternetGatewayDevice"))
					{
						list_0.Add(UPnPDevice.Serialize(a_LocalEndPoint, string_0, xElement2));
					}
				}
				if (list_0.Count > 0)
				{
					upnPState_0 = UPnPState.UPnPDetectedNotTested;
				}
				else
				{
					upnPState_0 = UPnPState.UPnPDetectedNotMatching;
				}
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Log(string.Format("UPnPRootDevice {0} _OnRootDescriptionReceived: {0}\r\n{1}", a_RemoteEndPoint.ToString(), ex.Message));
				upnPState_0 = UPnPState.UPnPDetectedNotMatching;
			}
		}
		else
		{
			upnPState_0 = UPnPState.UPnPFailed;
		}
		bool_0 = true;
		if (discoveryFinishedHandler_0 != null)
		{
			discoveryFinishedHandler_0(this);
		}
	}

	private void method_1(object sender, RunWorkerCompletedEventArgs e)
	{
		bool flag = true;
		lock (list_0)
		{
			foreach (UPnPDevice item in list_0)
			{
				lock (item)
				{
					if (item.NotificationState != UPnPNotificationState.Done && item.NotificationState != UPnPNotificationState.Failed)
					{
						flag = false;
					}
				}
			}
		}
		bool_1 = flag;
		if (bool_1)
		{
			method_2();
			if (notificationFinishedHandler_0 != null)
			{
				notificationFinishedHandler_0();
			}
		}
	}

	private void method_2()
	{
		bool flag = false;
		bool flag2 = false;
		lock (list_0)
		{
			foreach (UPnPDevice item in list_0)
			{
				lock (item)
				{
					if (item.NotificationState == UPnPNotificationState.Done)
					{
						if (item.ManageUPnP)
						{
							flag = true;
						}
					}
					else if (item.NotificationState == UPnPNotificationState.Failed)
					{
						flag2 = true;
					}
				}
			}
		}
		if (flag)
		{
			upnPState_0 = UPnPState.UPnPDetectedMatching;
		}
		else if (flag2)
		{
			upnPState_0 = UPnPState.UPnPFailed;
		}
		else
		{
			upnPState_0 = UPnPState.UPnPDetectedNotMatching;
		}
	}

	private void method_3(UPnPDevice upnPDevice_0)
	{
		if (upnPDevice_0.MappingState == UPnPMappinPortFlow.Failed)
		{
			UPnPTraceLog.Error($"_OnDevicePortMappingFinished: device {upnPDevice_0.FriendlyName} failed mapping port");
			AddPortMapping();
			return;
		}
		UPnPTraceLog.Log($"_OnDevicePortMappingFinished: device {upnPDevice_0.FriendlyName} mapped the port successfully");
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.Done;
		if (portMappingFinishedHandler_0 != null)
		{
			portMappingFinishedHandler_0(this);
		}
	}

	[CompilerGenerated]
	private static bool smethod_0(XElement xelement_0)
	{
		if (xelement_0.Name == "device")
		{
			return xelement_0.Element(xelement_0.Name.Namespace + "deviceType").Value.Contains("InternetGatewayDevice");
		}
		return false;
	}
}
