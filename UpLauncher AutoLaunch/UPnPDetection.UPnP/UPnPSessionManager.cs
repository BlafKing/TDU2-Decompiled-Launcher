using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace UPnPDetection.UPnP;

public class UPnPSessionManager
{
	[CompilerGenerated]
	private sealed class Class37
	{
		public object object_0;

		public bool method_0(UPnPSession upnPSession_0)
		{
			try
			{
				if (object_0 is IPAddress)
				{
					return upnPSession_0.Ip.Equals(object_0 as IPAddress);
				}
				if (object_0 is UdpClient)
				{
					return upnPSession_0.Udp.Equals(object_0 as UdpClient);
				}
				return false;
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"GetSession {object_0}: {ex.Message}");
				return false;
			}
		}
	}

	private List<UPnPSession> list_0;

	public List<UPnPSession> Sessions => list_0;

	public int Count
	{
		get
		{
			int num = 0;
			lock (list_0)
			{
				return list_0.Count;
			}
		}
	}

	public UPnPSessionManager()
	{
		list_0 = new List<UPnPSession>();
	}

	public UPnPSession GetSession(object a_searchPattern)
	{
		if (a_searchPattern == null)
		{
			return null;
		}
		return list_0.Find(delegate(UPnPSession upnPSession_0)
		{
			try
			{
				if (a_searchPattern is IPAddress)
				{
					return upnPSession_0.Ip.Equals(a_searchPattern as IPAddress);
				}
				if (a_searchPattern is UdpClient)
				{
					return upnPSession_0.Udp.Equals(a_searchPattern as UdpClient);
				}
				return false;
			}
			catch (Exception ex)
			{
				UPnPTraceLog.Error($"GetSession {a_searchPattern}: {ex.Message}");
				return false;
			}
		});
	}

	public void Add(UPnPSession a_session)
	{
		if (!list_0.Contains(a_session))
		{
			list_0.Add(a_session);
		}
	}

	public void Remove(object a_searchPattern)
	{
		lock (list_0)
		{
			UPnPSession session = GetSession(a_searchPattern);
			if (session != null)
			{
				list_0.Remove(session);
			}
		}
	}

	public void CleanTimedOut()
	{
		lock (list_0)
		{
			List<IPAddress> list = new List<IPAddress>();
			foreach (UPnPSession item in list_0)
			{
				if (item.TimedOut)
				{
					list.Add(item.Ip);
				}
			}
			foreach (IPAddress item2 in list)
			{
				UPnPTraceLog.Log($"UPnP SessionManager CleanTimedOut: remove session {item2}");
				Remove(item2);
			}
		}
	}
}
