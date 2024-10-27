using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace UPnPDetection.UPnP;

public class UPnPService
{
	public delegate void NotificationFinishedHandler();

	public delegate void InvokeFinishedHandler(UPnPServiceAction a_Action, params object[] a_Params);

	public delegate void InvokeFailedHandler(UPnPServiceAction a_Action);

	[CompilerGenerated]
	private sealed class Class39
	{
		public XNamespace xnamespace_0;

		private static Func<XElement, string> func_0;

		public UPnPServiceAction method_0(XElement xelement_0)
		{
			return new UPnPServiceAction
			{
				Name = xelement_0.Element(xnamespace_0 + "name").Value,
				Arguments = ((xelement_0.Element(xnamespace_0 + "argumentList") != null) ? (from argumentElement in xelement_0.Element(xnamespace_0 + "argumentList").Elements(xnamespace_0 + "argument")
					select new UPnPServiceAction.UPnPServiceActionArgument
					{
						Name = xelement_0.Element(xnamespace_0 + "name").Value,
						RelatedStateVariable = xelement_0.Element(xnamespace_0 + "relatedStateVariable").Value,
						Direction = ((!(xelement_0.Element(xnamespace_0 + "direction").Value == "in")) ? UPnPServiceAction.UPnPServiceActionArgumentDirection.Out : UPnPServiceAction.UPnPServiceActionArgumentDirection.In)
					}).ToList() : new List<UPnPServiceAction.UPnPServiceActionArgument>())
			};
		}

		public UPnPServiceStateTable.UPnPServiceStateVariable method_1(XElement xelement_0)
		{
			return new UPnPServiceStateTable.UPnPServiceStateVariable
			{
				Name = xelement_0.Element(xnamespace_0 + "name").Value,
				DataType = xelement_0.Element(xnamespace_0 + "dataType").Value,
				AllowedValueList = ((xelement_0.Element(xnamespace_0 + "allowedValueList") != null) ? (from argumentElement in xelement_0.Element(xnamespace_0 + "allowedValueList").Elements(xnamespace_0 + "allowedValue")
					select xelement_0.Value).ToList() : null)
			};
		}

		public UPnPServiceAction.UPnPServiceActionArgument method_2(XElement xelement_0)
		{
			return new UPnPServiceAction.UPnPServiceActionArgument
			{
				Name = xelement_0.Element(xnamespace_0 + "name").Value,
				RelatedStateVariable = xelement_0.Element(xnamespace_0 + "relatedStateVariable").Value,
				Direction = ((!(xelement_0.Element(xnamespace_0 + "direction").Value == "in")) ? UPnPServiceAction.UPnPServiceActionArgumentDirection.Out : UPnPServiceAction.UPnPServiceActionArgumentDirection.In)
			};
		}

		private static string smethod_0(XElement xelement_0)
		{
			return xelement_0.Value;
		}
	}

	private NotificationFinishedHandler notificationFinishedHandler_0;

	private InvokeFinishedHandler invokeFinishedHandler_0;

	private InvokeFailedHandler invokeFailedHandler_0;

	public static string[] UPnPServiceNames = new string[3] { "urn:schemas-upnp-org:service:WANIPConnection:1", "urn:schemas-upnp-org:service:WANPPPConnection:1", "urn:schemas-upnp-org:service:WANIPConnection:2" };

	private string string_0;

	private string string_1;

	private Uri uri_0;

	private Uri uri_1;

	private Uri uri_2;

	private List<UPnPServiceAction> list_0;

	private UPnPServiceStateTable upnPServiceStateTable_0;

	private UPnPNotificationState upnPNotificationState_0;

	private bool bool_0;

	[CompilerGenerated]
	private static Predicate<UPnPServiceAction.UPnPServiceActionArgument> predicate_0;

	[CompilerGenerated]
	private static Predicate<UPnPServiceAction.UPnPServiceActionArgument> predicate_1;

	public string ServiceType
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
		}
	}

	public string ServiceId
	{
		get
		{
			return string_1;
		}
		set
		{
			string_1 = value;
		}
	}

	public Uri ControlUrl
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

	public Uri EventSubUrl
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

	public Uri SCPDUrl
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

	public List<UPnPServiceAction> ServiceActions => list_0;

	public UPnPServiceStateTable ServiceState => upnPServiceStateTable_0;

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

	public event InvokeFinishedHandler InvokeFinished
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			invokeFinishedHandler_0 = (InvokeFinishedHandler)Delegate.Combine(invokeFinishedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			invokeFinishedHandler_0 = (InvokeFinishedHandler)Delegate.Remove(invokeFinishedHandler_0, value);
		}
	}

	public event InvokeFailedHandler InvokeFailed
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			invokeFailedHandler_0 = (InvokeFailedHandler)Delegate.Combine(invokeFailedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			invokeFailedHandler_0 = (InvokeFailedHandler)Delegate.Remove(invokeFailedHandler_0, value);
		}
	}

	public UPnPService()
	{
		upnPNotificationState_0 = UPnPNotificationState.Idle;
		bool_0 = false;
	}

	public void NotifyService()
	{
		upnPNotificationState_0 = UPnPNotificationState.Sending;
		try
		{
			WebClient webClient = new WebClient();
			webClient.DownloadDataCompleted += method_0;
			webClient.DownloadDataAsync(uri_2, "");
		}
		catch (Exception ex)
		{
			UPnPTraceLog.Error($"UPnP device notification: {ex.Message}");
			upnPNotificationState_0 = UPnPNotificationState.Failed;
			if (notificationFinishedHandler_0 != null)
			{
				notificationFinishedHandler_0();
			}
		}
	}

	public void Invoke(UPnPServiceAction a_Action, params object[] a_Params)
	{
		if (a_Action == null)
		{
			UPnPTraceLog.Error("Invoke: action null");
			if (invokeFailedHandler_0 != null)
			{
				invokeFailedHandler_0(a_Action);
			}
			return;
		}
		a_Action.Clean();
		List<UPnPServiceAction.UPnPServiceActionArgument> list = a_Action.Arguments.FindAll((UPnPServiceAction.UPnPServiceActionArgument upnPServiceActionArgument_0) => upnPServiceActionArgument_0.Direction == UPnPServiceAction.UPnPServiceActionArgumentDirection.In);
		if (list.Count != a_Params.Length)
		{
			UPnPTraceLog.Error($"Invoke {a_Action.Name}: Arguments count don't match");
			if (invokeFailedHandler_0 != null)
			{
				invokeFailedHandler_0(a_Action);
			}
			return;
		}
		int num = 0;
		while (true)
		{
			if (num < list.Count)
			{
				if (upnPServiceStateTable_0.GetVariableType(list[num]) != a_Params[num].GetType())
				{
					break;
				}
				num++;
				continue;
			}
			try
			{
				XNamespace xNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
				XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new XElement(xNamespace + "Envelope", new XAttribute(XNamespace.Xmlns + "s", xNamespace.NamespaceName), new XAttribute(xNamespace + "encodingStyle", "http://schemas.xmlsoap.org/soap/encoding/"), new XElement(xNamespace + "Body")));
				XNamespace xNamespace2 = string_0;
				XElement xElement = new XElement(xNamespace2 + a_Action.Name, new XAttribute(XNamespace.Xmlns + "u", xNamespace2.NamespaceName));
				for (int i = 0; i < list.Count; i++)
				{
					xElement.Add(new XElement(list[i].Name, UPnPServiceAction.SerializeArgument(a_Params[i])));
				}
				xDocument.Root.Element(xNamespace + "Body").Add(xElement);
				WebClient webClient = new WebClient();
				webClient.Headers.Add("SoapAction", string_0 + "#" + a_Action.Name);
				webClient.Headers.Add("Content-Type", "text/xml; charset=\"utf-8\"");
				webClient.UploadDataCompleted += method_1;
				webClient.UploadDataAsync(uri_0, "POST", Encoding.ASCII.GetBytes(xDocument.ToString()), a_Action);
				return;
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"UPnPService Invoke {a_Action.Name}: {ex.Message}");
				if (invokeFailedHandler_0 != null)
				{
					invokeFailedHandler_0(a_Action);
				}
				return;
			}
		}
		UPnPTraceLog.Error($"Invoke {a_Action.Name}: Bad type for argument {list[num].Name} ({upnPServiceStateTable_0.GetVariableType(list[num]).ToString()}) - {a_Params[num].GetType().ToString()} expected");
		if (invokeFailedHandler_0 != null)
		{
			invokeFailedHandler_0(a_Action);
		}
	}

	private void method_0(object sender, DownloadDataCompletedEventArgs e)
	{
		upnPNotificationState_0 = UPnPNotificationState.Receiving;
		if (e.Error == null && !e.Cancelled)
		{
			string @string = Encoding.UTF8.GetString(e.Result);
			try
			{
				XDocument xDocument = XDocument.Parse(@string);
				XNamespace xnamespace_0 = xDocument.Root.Name.Namespace;
				list_0 = (from xelement_0 in xDocument.Root.Element(xnamespace_0 + "actionList").Elements(xnamespace_0 + "action")
					select new UPnPServiceAction
					{
						Name = xelement_0.Element(xnamespace_0 + "name").Value,
						Arguments = ((xelement_0.Element(xnamespace_0 + "argumentList") != null) ? (from argumentElement in xelement_0.Element(xnamespace_0 + "argumentList").Elements(xnamespace_0 + "argument")
							select new UPnPServiceAction.UPnPServiceActionArgument
							{
								Name = xelement_0.Element(xnamespace_0 + "name").Value,
								RelatedStateVariable = xelement_0.Element(xnamespace_0 + "relatedStateVariable").Value,
								Direction = ((!(xelement_0.Element(xnamespace_0 + "direction").Value == "in")) ? UPnPServiceAction.UPnPServiceActionArgumentDirection.Out : UPnPServiceAction.UPnPServiceActionArgumentDirection.In)
							}).ToList() : new List<UPnPServiceAction.UPnPServiceActionArgument>())
					}).ToList();
				upnPServiceStateTable_0 = new UPnPServiceStateTable
				{
					Variables = (from xelement_0 in xDocument.Root.Element(xnamespace_0 + "serviceStateTable").Elements(xnamespace_0 + "stateVariable")
						select new UPnPServiceStateTable.UPnPServiceStateVariable
						{
							Name = xelement_0.Element(xnamespace_0 + "name").Value,
							DataType = xelement_0.Element(xnamespace_0 + "dataType").Value,
							AllowedValueList = ((xelement_0.Element(xnamespace_0 + "allowedValueList") != null) ? (from argumentElement in xelement_0.Element(xnamespace_0 + "allowedValueList").Elements(xnamespace_0 + "allowedValue")
								select xelement_0.Value).ToList() : null)
						}).ToList()
				};
				upnPNotificationState_0 = UPnPNotificationState.Done;
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"UPnPService {string_0} _OnServiceNotifyReceived: {ex.Message}\r\n{@string}");
				upnPNotificationState_0 = UPnPNotificationState.Failed;
			}
		}
		else
		{
			upnPNotificationState_0 = UPnPNotificationState.Failed;
		}
		if (notificationFinishedHandler_0 != null)
		{
			notificationFinishedHandler_0();
		}
	}

	private void method_1(object sender, UploadDataCompletedEventArgs e)
	{
		UPnPServiceAction uPnPServiceAction = e.UserState as UPnPServiceAction;
		if (e.Error == null && !e.Cancelled)
		{
			try
			{
				List<UPnPServiceAction.UPnPServiceActionArgument> list = uPnPServiceAction.Arguments.FindAll((UPnPServiceAction.UPnPServiceActionArgument upnPServiceActionArgument_0) => upnPServiceActionArgument_0.Direction == UPnPServiceAction.UPnPServiceActionArgumentDirection.Out);
				if (list.Count == 0)
				{
					UPnPTraceLog.Log($"_OnInvokeResponseReceived {uPnPServiceAction.Name}: No response expected");
					if (invokeFinishedHandler_0 != null)
					{
						invokeFinishedHandler_0(uPnPServiceAction);
					}
					return;
				}
				XDocument xDocument = XDocument.Parse(Encoding.ASCII.GetString(e.Result));
				XElement xElement = xDocument.Root.Element(xDocument.Root.Name.Namespace + "Body");
				XElement xElement2 = xElement.Element(xDocument.Root.Name.Namespace + "Fault");
				if (xElement2 != null)
				{
					UPnPTraceLog.Log($"_OnInvokeResponseReceived {uPnPServiceAction.Name}: SOAP Fault returned");
					string value = xElement2.Element("faultstring").Value;
					XNamespace xNamespace = "urn:schemas-upnp-org:control-1-0";
					XElement xElement3 = xElement2.Element("detail").Element(xNamespace + value).Element(xNamespace + "errorCode");
					if (xElement3.Value == "714")
					{
						UPnPTraceLog.Log($"_OnInvokeResponseReceived {uPnPServiceAction.Name}: Fault 714 detected");
						if (invokeFinishedHandler_0 != null)
						{
							invokeFinishedHandler_0(uPnPServiceAction);
						}
					}
					else
					{
						UPnPTraceLog.Error($"_OnInvokeResponseReceived {uPnPServiceAction.Name}: Fault {xElement3.Value}");
						if (invokeFailedHandler_0 != null)
						{
							invokeFailedHandler_0(uPnPServiceAction);
						}
					}
					return;
				}
				XNamespace xNamespace2 = string_0;
				XElement xElement4 = xElement.Element(string.Concat(xNamespace2 + uPnPServiceAction.Name, "Response"));
				foreach (UPnPServiceAction.UPnPServiceActionArgument item in list)
				{
					try
					{
						item.Value = xElement4.Element(item.Name).Value;
					}
					catch (Exception ex)
					{
						UPnPTraceLog.Error($"_OnInvokeResponseReceived {uPnPServiceAction.Name}: Unable to get the returned value for argument {item.Name} - {ex.Message}");
					}
				}
			}
			catch (Exception ex2)
			{
				UPnPTraceLog.Error(string.Format("_OnInvokeResponseReceived {0}: ", uPnPServiceAction.Name, ex2.Message));
				if (invokeFailedHandler_0 != null)
				{
					invokeFailedHandler_0(uPnPServiceAction);
				}
			}
			if (invokeFinishedHandler_0 != null)
			{
				invokeFinishedHandler_0(uPnPServiceAction, true);
			}
		}
		else if (!e.Error.Message.Contains("(500"))
		{
			UPnPTraceLog.Error(string.Format("_OnInvokeResponseReceived {0}: {1}", uPnPServiceAction.Name, (e.Error != null) ? e.Error.Message : "canceled"));
			if (invokeFailedHandler_0 != null)
			{
				invokeFailedHandler_0(uPnPServiceAction);
			}
		}
		else if (invokeFinishedHandler_0 != null)
		{
			invokeFinishedHandler_0(uPnPServiceAction);
		}
	}

	[CompilerGenerated]
	private static bool smethod_0(UPnPServiceAction.UPnPServiceActionArgument upnPServiceActionArgument_0)
	{
		return upnPServiceActionArgument_0.Direction == UPnPServiceAction.UPnPServiceActionArgumentDirection.In;
	}

	[CompilerGenerated]
	private static bool smethod_1(UPnPServiceAction.UPnPServiceActionArgument upnPServiceActionArgument_0)
	{
		return upnPServiceActionArgument_0.Direction == UPnPServiceAction.UPnPServiceActionArgumentDirection.Out;
	}
}
