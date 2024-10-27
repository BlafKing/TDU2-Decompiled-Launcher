using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace UPnPDetection.UPnP;

public sealed class UPnPDevice
{
	public delegate void DeviceNotificationFinishedHandler();

	public delegate void PortMappingFinishedHandler(UPnPDevice a_Device);

	[CompilerGenerated]
	private sealed class Class36
	{
		public UPnPDevice upnPDevice_0;

		public XElement xelement_0;

		public UPnPService method_0(XElement xelement_1)
		{
			return new UPnPService
			{
				ServiceType = smethod_0(xelement_1.Element(xelement_0.Name.Namespace + "serviceType")),
				ServiceId = smethod_0(xelement_1.Element(xelement_0.Name.Namespace + "serviceId")),
				ControlUrl = smethod_1(upnPDevice_0.string_1, xelement_1.Element(xelement_0.Name.Namespace + "controlURL")),
				EventSubUrl = smethod_1(upnPDevice_0.string_1, xelement_1.Element(xelement_0.Name.Namespace + "eventSubURL")),
				SCPDUrl = smethod_1(upnPDevice_0.string_1, xelement_1.Element(xelement_0.Name.Namespace + "SCPDURL"))
			};
		}
	}

	private const string string_0 = "urn:schemas-upnp-org:device:WANConnectionDevice:1";

	private DeviceNotificationFinishedHandler deviceNotificationFinishedHandler_0;

	private PortMappingFinishedHandler portMappingFinishedHandler_0;

	private ProgressChangedEventHandler progressChangedEventHandler_0;

	private RunWorkerCompletedEventHandler runWorkerCompletedEventHandler_0;

	private IPEndPoint ipendPoint_0;

	private string string_1;

	private string string_2;

	private Uri uri_0;

	private string string_3;

	private string string_4;

	private Uri uri_1;

	private string string_5;

	private string string_6;

	private string string_7;

	private Uri uri_2;

	private string string_8;

	private List<UPnPService> list_0;

	private List<UPnPDevice> list_1;

	private UPnPNotificationState upnPNotificationState_0;

	private bool bool_0;

	private BackgroundWorker backgroundWorker_0;

	private UPnPMappinPortFlow upnPMappinPortFlow_0;

	private object object_0;

	private int int_0;

	private bool bool_1;

	private bool bool_2;

	[CompilerGenerated]
	private static Predicate<UPnPService> predicate_0;

	[CompilerGenerated]
	private static Predicate<UPnPServiceAction> predicate_1;

	[CompilerGenerated]
	private static Predicate<UPnPServiceAction> predicate_2;

	[CompilerGenerated]
	private static Predicate<UPnPServiceAction> predicate_3;

	[CompilerGenerated]
	private static Predicate<UPnPServiceAction> predicate_4;

	public IPEndPoint LocalEndPoint
	{
		get
		{
			return ipendPoint_0;
		}
		set
		{
			ipendPoint_0 = value;
		}
	}

	public string RootLocation
	{
		set
		{
			string_1 = value;
		}
	}

	public string DeviceType
	{
		get
		{
			return string_2;
		}
		set
		{
			string_2 = value;
		}
	}

	public Uri PresentationUrl
	{
		get
		{
			return uri_0;
		}
		set
		{
			uri_0 = value;
		}
	}

	public string FriendlyName
	{
		get
		{
			return string_3;
		}
		set
		{
			string_3 = value;
		}
	}

	public string Manufacturer
	{
		get
		{
			return string_4;
		}
		set
		{
			string_4 = value;
		}
	}

	public Uri ManufacturerUrl
	{
		get
		{
			return uri_1;
		}
		set
		{
			uri_1 = value;
		}
	}

	public string ModelDescription
	{
		get
		{
			return string_5;
		}
		set
		{
			string_5 = value;
		}
	}

	public string ModelName
	{
		get
		{
			return string_6;
		}
		set
		{
			string_6 = value;
		}
	}

	public string ModelNumber
	{
		get
		{
			return string_7;
		}
		set
		{
			string_7 = value;
		}
	}

	public Uri ModelUrl
	{
		get
		{
			return uri_2;
		}
		set
		{
			uri_2 = value;
		}
	}

	public string UDN
	{
		get
		{
			return string_8;
		}
		set
		{
			string_8 = value;
		}
	}

	public List<UPnPService> Services => list_0;

	public List<UPnPDevice> Children => list_1;

	public UPnPNotificationState NotificationState => upnPNotificationState_0;

	public bool NotificationProgressChecked
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
		}
	}

	public UPnPMappinPortFlow MappingState => upnPMappinPortFlow_0;

	public XElement DeviceServices
	{
		set
		{
			if (value == null)
			{
				list_0 = null;
				return;
			}
			try
			{
				list_0 = (from xelement_1 in value.Elements(value.Name.Namespace + "service")
					select new UPnPService
					{
						ServiceType = smethod_0(xelement_1.Element(value.Name.Namespace + "serviceType")),
						ServiceId = smethod_0(xelement_1.Element(value.Name.Namespace + "serviceId")),
						ControlUrl = smethod_1(string_1, xelement_1.Element(value.Name.Namespace + "controlURL")),
						EventSubUrl = smethod_1(string_1, xelement_1.Element(value.Name.Namespace + "eventSubURL")),
						SCPDUrl = smethod_1(string_1, xelement_1.Element(value.Name.Namespace + "SCPDURL"))
					}).ToList();
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"UPnPDevice {string_3}({string_2}) DeviceServices: {ex.Message}\r\n{value}");
			}
		}
	}

	public XElement DeviceSubDevices
	{
		set
		{
			try
			{
				if (value == null)
				{
					list_1 = null;
					UPnPTraceLog.Log($"UPnPDevice {string_3}({string_2}) has no sub device");
					return;
				}
				list_1 = (from xelement_0 in value.Elements(value.Name.Namespace + "device")
					select Serialize(ipendPoint_0, string_1, xelement_0)).ToList();
				UPnPTraceLog.Log($"UPnPDevice {string_3}({string_2}) has {list_1.Count} sub devices");
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"UPnPDevice {string_3}({string_2}) DeviceSubDevices: {ex.Message}\r\n{value}");
			}
		}
	}

	public bool ManageUPnP
	{
		get
		{
			if (string_2 == "urn:schemas-upnp-org:device:WANConnectionDevice:1")
			{
				return true;
			}
			if (list_1 != null)
			{
				lock (list_1)
				{
					foreach (UPnPDevice item in list_1)
					{
						if (item.ManageUPnP)
						{
							return true;
						}
					}
				}
			}
			UPnPTraceLog.Log($"UPnPDevice {string_2} does not manage UPnP route");
			return false;
		}
	}

	public event DeviceNotificationFinishedHandler OnDeviceNotificationFinished
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			deviceNotificationFinishedHandler_0 = (DeviceNotificationFinishedHandler)Delegate.Combine(deviceNotificationFinishedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			deviceNotificationFinishedHandler_0 = (DeviceNotificationFinishedHandler)Delegate.Remove(deviceNotificationFinishedHandler_0, value);
		}
	}

	public event PortMappingFinishedHandler OnPortMappingFinished
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

	public event ProgressChangedEventHandler OnNotifyProgress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			progressChangedEventHandler_0 = (ProgressChangedEventHandler)Delegate.Combine(progressChangedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			progressChangedEventHandler_0 = (ProgressChangedEventHandler)Delegate.Remove(progressChangedEventHandler_0, value);
		}
	}

	public event RunWorkerCompletedEventHandler OnNotifyFinished
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			runWorkerCompletedEventHandler_0 = (RunWorkerCompletedEventHandler)Delegate.Combine(runWorkerCompletedEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			runWorkerCompletedEventHandler_0 = (RunWorkerCompletedEventHandler)Delegate.Remove(runWorkerCompletedEventHandler_0, value);
		}
	}

	private static string smethod_0(XElement xelement_0)
	{
		if (xelement_0 != null)
		{
			return xelement_0.Value;
		}
		return "";
	}

	private static Uri smethod_1(string string_9, XElement xelement_0)
	{
		if (string.IsNullOrEmpty(string_9))
		{
			UPnPTraceLog.Error("UPnPDevice ParseUri: base uri not filled");
			return null;
		}
		if (xelement_0 != null && xelement_0.Value.Length != 0)
		{
			try
			{
				if (xelement_0.Value.Contains("http://"))
				{
					return new Uri(xelement_0.Value);
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string_9);
				if (xelement_0.Value[0] != '/')
				{
					stringBuilder.Append("/");
				}
				stringBuilder.Append(xelement_0.Value);
				return new Uri(stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"UPnPDevice ParseUri: {ex.Message}");
			}
			return new Uri(string_9);
		}
		return new Uri(string_9);
	}

	public UPnPDevice()
	{
		upnPNotificationState_0 = UPnPNotificationState.Idle;
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.Idle;
		int_0 = 0;
	}

	public static UPnPDevice Serialize(IPEndPoint a_LocalEndPoint, string a_Uri, XElement a_Xdevice)
	{
		if (a_Xdevice == null)
		{
			return null;
		}
		try
		{
			UPnPDevice uPnPDevice = new UPnPDevice();
			uPnPDevice.LocalEndPoint = a_LocalEndPoint;
			uPnPDevice.RootLocation = a_Uri;
			uPnPDevice.DeviceType = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "deviceType"));
			uPnPDevice.PresentationUrl = smethod_1(a_Uri, a_Xdevice.Element(a_Xdevice.Name.Namespace + "presentationURL"));
			uPnPDevice.FriendlyName = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "friendlyName"));
			uPnPDevice.Manufacturer = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "manufacturer"));
			uPnPDevice.ManufacturerUrl = smethod_1(a_Uri, a_Xdevice.Element(a_Xdevice.Name.Namespace + "manufacturerURL"));
			uPnPDevice.ModelDescription = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "modelDescription"));
			uPnPDevice.ModelName = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "modelName"));
			uPnPDevice.ModelNumber = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "modelNumber"));
			uPnPDevice.ModelUrl = smethod_1(a_Uri, a_Xdevice.Element(a_Xdevice.Name.Namespace + "modelURL"));
			uPnPDevice.UDN = smethod_0(a_Xdevice.Element(a_Xdevice.Name.Namespace + "UDN"));
			uPnPDevice.DeviceServices = a_Xdevice.Element(a_Xdevice.Name.Namespace + "serviceList");
			uPnPDevice.DeviceSubDevices = a_Xdevice.Element(a_Xdevice.Name.Namespace + "deviceList");
			return uPnPDevice;
		}
		catch (Exception ex)
		{
			UPnPTraceLog.Error($"UPnPDevice Serialize {ex.Message} \r\n\t {a_Xdevice.ToString()}");
			return null;
		}
	}

	public void Notify()
	{
		upnPNotificationState_0 = UPnPNotificationState.Sending;
		backgroundWorker_0 = new BackgroundWorker();
		backgroundWorker_0.WorkerSupportsCancellation = true;
		backgroundWorker_0.WorkerReportsProgress = true;
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		if (progressChangedEventHandler_0 != null)
		{
			backgroundWorker_0.ProgressChanged += progressChangedEventHandler_0;
		}
		if (runWorkerCompletedEventHandler_0 != null)
		{
			backgroundWorker_0.RunWorkerCompleted += runWorkerCompletedEventHandler_0;
		}
		backgroundWorker_0.RunWorkerAsync();
	}

	public void AddPortMapping()
	{
		if (string_2 == "urn:schemas-upnp-org:device:WANConnectionDevice:1")
		{
			UPnPTraceLog.Log($"AddPortMapping: device {string_3} manage UPnP directly");
			method_3();
			return;
		}
		UPnPTraceLog.Log($"AddPortMapping: device {string_3} doesn't manage UPnP directly, transfer to children");
		lock (list_1)
		{
			for (int i = int_0; i < list_1.Count; i++)
			{
				lock (list_1[i])
				{
					if (list_1[i].ManageUPnP)
					{
						UPnPTraceLog.Log($"UPnPDevice AddPortMapping: Requesting sub device {list_1[i].FriendlyName} to map the port");
						int_0 = i + 1;
						UPnPDevice uPnPDevice = list_1[i];
						uPnPDevice.portMappingFinishedHandler_0 = (PortMappingFinishedHandler)Delegate.Combine(uPnPDevice.portMappingFinishedHandler_0, new PortMappingFinishedHandler(method_4));
						list_1[i].AddPortMapping();
						return;
					}
				}
			}
		}
		UPnPTraceLog.Error(string.Format("AddPortMapping: neither the device or it children could manage the UPnP", string_3));
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		backgroundWorker_0_RunWorkerCompleted(this, null);
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		upnPNotificationState_0 = UPnPNotificationState.Managing;
		if (list_1 != null)
		{
			lock (list_1)
			{
				if (list_1.Count == 0)
				{
					bool_1 = true;
				}
				else
				{
					bool_1 = false;
					foreach (UPnPDevice item in list_1)
					{
						item.deviceNotificationFinishedHandler_0 = (DeviceNotificationFinishedHandler)Delegate.Combine(item.deviceNotificationFinishedHandler_0, new DeviceNotificationFinishedHandler(method_0));
						item.Notify();
					}
				}
			}
		}
		else
		{
			bool_1 = true;
		}
		if (list_0 != null)
		{
			lock (list_0)
			{
				if (list_0.Count == 0)
				{
					bool_2 = true;
				}
				else
				{
					bool_2 = false;
					foreach (UPnPService item2 in list_0)
					{
						item2.NotificationFinished += method_1;
						item2.NotifyService();
					}
				}
			}
		}
		else
		{
			bool_2 = true;
		}
		_ = DateTime.Now;
		while (!bool_1 && !bool_2)
		{
			Thread.Sleep(10);
		}
		UPnPTraceLog.Log($"_OnNotifyStarted {string_3}: devices notifications and services notifications done");
		upnPNotificationState_0 = UPnPNotificationState.Done;
		if (deviceNotificationFinishedHandler_0 != null)
		{
			deviceNotificationFinishedHandler_0();
		}
	}

	private void method_0()
	{
		bool flag = true;
		lock (list_1)
		{
			foreach (UPnPDevice item in list_1)
			{
				lock (item)
				{
					switch (item.NotificationState)
					{
					case UPnPNotificationState.Sending:
					case UPnPNotificationState.Receiving:
					case UPnPNotificationState.Managing:
						flag = false;
						break;
					}
				}
			}
		}
		bool_1 = flag;
	}

	private void method_1()
	{
		bool flag = true;
		lock (list_0)
		{
			foreach (UPnPService item in list_0)
			{
				lock (item)
				{
					switch (item.NotificationState)
					{
					case UPnPNotificationState.Sending:
					case UPnPNotificationState.Receiving:
						flag = false;
						break;
					}
				}
			}
		}
		bool_2 = flag;
	}

	private UPnPMappinPortFlow method_2(UPnPMappinPortFlow upnPMappinPortFlow_1)
	{
		switch (upnPMappinPortFlow_1)
		{
		case UPnPMappinPortFlow.Idle:
			return UPnPMappinPortFlow.RequestConnection;
		case UPnPMappinPortFlow.RequestingConnection:
			return UPnPMappinPortFlow.GetPortMapping;
		case UPnPMappinPortFlow.GettingPortMapping:
			if (object_0 != null)
			{
				return UPnPMappinPortFlow.RemovePortMapping;
			}
			return UPnPMappinPortFlow.AddPortMapping;
		case UPnPMappinPortFlow.RemovingPortMapping:
			return UPnPMappinPortFlow.AddPortMapping;
		case UPnPMappinPortFlow.AddingPortMapping:
			return UPnPMappinPortFlow.Done;
		default:
			return upnPMappinPortFlow_1;
		case UPnPMappinPortFlow.Failed:
			return UPnPMappinPortFlow.Failed;
		}
	}

	private void method_3()
	{
		UPnPService uPnPService = list_0.Find((UPnPService upnPService_0) => UPnPService.UPnPServiceNames.Contains(upnPService_0.ServiceType));
		if (uPnPService == null)
		{
			UPnPTraceLog.Error("_StartPortMapping: Service not found");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
		else
		{
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Idle;
		}
		try
		{
			if (backgroundWorker_0.IsBusy)
			{
				UPnPTraceLog.Error("_StartPortMapping: worker already working");
				backgroundWorker_0.CancelAsync();
			}
			UPnPTraceLog.Log("_StartPortMapping: Starting port mapping workflow");
			backgroundWorker_0 = new BackgroundWorker();
			backgroundWorker_0.DoWork += backgroundWorker_0_DoWork_1;
			backgroundWorker_0.RunWorkerCompleted += backgroundWorker_0_RunWorkerCompleted;
			backgroundWorker_0.WorkerSupportsCancellation = true;
			backgroundWorker_0.RunWorkerAsync(uPnPService);
		}
		catch (Exception ex)
		{
			UPnPTraceLog.Error($"_StartPortMapping: {ex.Message}");
		}
	}

	private void backgroundWorker_0_DoWork_1(object sender, DoWorkEventArgs e)
	{
		UPnPService upnPService_ = e.Argument as UPnPService;
		while (upnPMappinPortFlow_0 != UPnPMappinPortFlow.Done && upnPMappinPortFlow_0 != UPnPMappinPortFlow.Failed)
		{
			switch (upnPMappinPortFlow_0)
			{
			case UPnPMappinPortFlow.Idle:
				upnPMappinPortFlow_0 = method_2(upnPMappinPortFlow_0);
				break;
			case UPnPMappinPortFlow.RequestConnection:
				UPnPTraceLog.Log($"_PortMappingWork {string_3}: _RequestConnection() call");
				method_5(upnPService_);
				break;
			case UPnPMappinPortFlow.GetPortMapping:
				UPnPTraceLog.Log($"_PortMappingWork {string_3}: _GetPortMapping() call");
				method_8(upnPService_);
				break;
			case UPnPMappinPortFlow.RemovePortMapping:
				UPnPTraceLog.Log($"_PortMappingWork {string_3}: _RemovePortMapping() call");
				method_7(upnPService_);
				break;
			case UPnPMappinPortFlow.AddPortMapping:
				UPnPTraceLog.Log($"_PortMappingWork {string_3}: _AddPortMapping() call");
				method_6(upnPService_);
				break;
			case UPnPMappinPortFlow.Done:
				UPnPTraceLog.Log($"_PortMappingWork {string_3}: Done");
				break;
			case UPnPMappinPortFlow.Failed:
				UPnPTraceLog.Log($"_PortMappingWork {string_3}: Failed");
				break;
			}
		}
	}

	private void backgroundWorker_0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		if (portMappingFinishedHandler_0 != null)
		{
			portMappingFinishedHandler_0(this);
		}
	}

	private void method_4(UPnPDevice upnPDevice_0)
	{
		if (upnPDevice_0.MappingState == UPnPMappinPortFlow.Failed)
		{
			UPnPTraceLog.Error($"_OnChildPortMappingFinished: device {upnPDevice_0.FriendlyName} failed mapping port");
			AddPortMapping();
		}
		else
		{
			UPnPTraceLog.Log($"_OnChildPortMappingFinished: device {upnPDevice_0.FriendlyName} mapped the port successfully");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Done;
			backgroundWorker_0_RunWorkerCompleted(this, null);
		}
	}

	private void method_5(UPnPService upnPService_0, params object[] object_1)
	{
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.RequestingConnection;
		if (upnPService_0 != null && upnPService_0.ServiceActions != null && upnPService_0.ServiceActions.Count != 0)
		{
			UPnPServiceAction uPnPServiceAction = upnPService_0.ServiceActions.Find((UPnPServiceAction upnPServiceAction_0) => upnPServiceAction_0.Name == "RequestConnection");
			if (uPnPServiceAction != null)
			{
				try
				{
					UPnPTraceLog.Log("_RequestConnection: Invoking service");
					upnPService_0.InvokeFailed += method_9;
					upnPService_0.InvokeFinished += method_10;
					upnPService_0.Invoke(uPnPServiceAction);
					return;
				}
				catch (Exception ex)
				{
					UPnPTraceLog.Error($"_RequestConnection: {ex.Message}");
					upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
					return;
				}
			}
			UPnPTraceLog.Error("_RequestConnection: action null");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
		else
		{
			UPnPTraceLog.Error("_RequestConnection: Service null or no service action found");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
	}

	private void method_6(UPnPService upnPService_0, params object[] object_1)
	{
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.AddingPortMapping;
		if (upnPService_0 != null && upnPService_0.ServiceActions != null && upnPService_0.ServiceActions.Count != 0)
		{
			UPnPServiceAction uPnPServiceAction = upnPService_0.ServiceActions.Find((UPnPServiceAction upnPServiceAction_0) => upnPServiceAction_0.Name == "AddPortMapping");
			if (uPnPServiceAction != null)
			{
				try
				{
					UPnPTraceLog.Log("_AddPortMapping: Invoking service");
					upnPService_0.InvokeFailed += method_9;
					upnPService_0.InvokeFinished += method_10;
					upnPService_0.Invoke(uPnPServiceAction, "", (ushort)8889, "UDP", (ushort)8889, Class32.ipaddress_0.ToString(), true, "Test Drive Unlimited 2", 0u);
					return;
				}
				catch (Exception ex)
				{
					UPnPTraceLog.Error($"_AddPortMapping: {ex.Message}");
					upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
					return;
				}
			}
			UPnPTraceLog.Error("_AddPortMapping: action null");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
		else
		{
			UPnPTraceLog.Error("_AddPortMapping: Service null or no service action found");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
	}

	private void method_7(UPnPService upnPService_0)
	{
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.RemovingPortMapping;
		if (upnPService_0 != null && upnPService_0.ServiceActions != null && upnPService_0.ServiceActions.Count != 0)
		{
			UPnPServiceAction uPnPServiceAction = upnPService_0.ServiceActions.Find((UPnPServiceAction upnPServiceAction_0) => upnPServiceAction_0.Name == "DeletePortMapping");
			if (uPnPServiceAction != null)
			{
				try
				{
					UPnPTraceLog.Log("_RemovePortMapping: Invoking service");
					upnPService_0.InvokeFailed += method_9;
					upnPService_0.InvokeFinished += method_10;
					upnPService_0.Invoke(uPnPServiceAction, "", (ushort)8889, "UDP");
					return;
				}
				catch (Exception ex)
				{
					UPnPTraceLog.Error($"_RemovePortMapping: {ex.Message}");
					upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
					return;
				}
			}
			UPnPTraceLog.Error("_RemovePortMapping: action null");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
		else
		{
			UPnPTraceLog.Error("_RemovePortMapping: Service null or no service action found");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
	}

	private void method_8(UPnPService upnPService_0, params object[] object_1)
	{
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.GettingPortMapping;
		if (upnPService_0 != null && upnPService_0.ServiceActions != null && upnPService_0.ServiceActions.Count != 0)
		{
			UPnPServiceAction uPnPServiceAction = upnPService_0.ServiceActions.Find((UPnPServiceAction upnPServiceAction_0) => upnPServiceAction_0.Name == "GetSpecificPortMappingEntry");
			if (uPnPServiceAction != null)
			{
				try
				{
					UPnPTraceLog.Log("_GetPortMapping: Invoking service");
					upnPService_0.InvokeFailed += method_9;
					upnPService_0.InvokeFinished += method_10;
					upnPService_0.Invoke(uPnPServiceAction, "", (ushort)8889, "UDP");
					return;
				}
				catch (Exception ex)
				{
					UPnPTraceLog.Error($"_GetPortMapping: {ex.Message}");
					upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
					return;
				}
			}
			UPnPTraceLog.Error("_GetPortMapping: action null");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
		else
		{
			UPnPTraceLog.Error("_GetPortMapping: Service null or no service action found");
			upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
		}
	}

	private void method_9(UPnPServiceAction upnPServiceAction_0)
	{
		upnPMappinPortFlow_0 = UPnPMappinPortFlow.Failed;
	}

	private void method_10(UPnPServiceAction upnPServiceAction_0, params object[] object_1)
	{
		if (object_1 != null && object_1.Length == 1)
		{
			object_0 = object_1[0];
		}
		upnPMappinPortFlow_0 = method_2(upnPMappinPortFlow_0);
	}

	[CompilerGenerated]
	private UPnPDevice method_11(XElement xelement_0)
	{
		return Serialize(ipendPoint_0, string_1, xelement_0);
	}

	[CompilerGenerated]
	private static bool smethod_2(UPnPService upnPService_0)
	{
		return UPnPService.UPnPServiceNames.Contains(upnPService_0.ServiceType);
	}

	[CompilerGenerated]
	private static bool smethod_3(UPnPServiceAction upnPServiceAction_0)
	{
		return upnPServiceAction_0.Name == "RequestConnection";
	}

	[CompilerGenerated]
	private static bool smethod_4(UPnPServiceAction upnPServiceAction_0)
	{
		return upnPServiceAction_0.Name == "AddPortMapping";
	}

	[CompilerGenerated]
	private static bool smethod_5(UPnPServiceAction upnPServiceAction_0)
	{
		return upnPServiceAction_0.Name == "DeletePortMapping";
	}

	[CompilerGenerated]
	private static bool smethod_6(UPnPServiceAction upnPServiceAction_0)
	{
		return upnPServiceAction_0.Name == "GetSpecificPortMappingEntry";
	}
}
