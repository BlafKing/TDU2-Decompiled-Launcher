using System;
using System.Net;
using System.Net.Sockets;

namespace UPnPDetection.UPnP;

public class UPnPSession
{
	private static TimeSpan timeSpan_0 = new TimeSpan(0, 0, 0, 2);

	private UdpClient client;

	private IPAddress address;

	private TimeSpan timeSpan_1;

	public TimeSpan LastActivity => timeSpan_1;

	public UdpClient Udp => client;

	public IPAddress Ip => address;

	public bool TimedOut
	{
		get
		{
			TimeSpan t = new TimeSpan(DateTime.Now.Ticks).Subtract(timeSpan_0);
			return TimeSpan.Compare(timeSpan_1, t) == -1;
		}
	}

	public UPnPSession(UdpClient client, IPAddress address)
	{
		this.client = client;
		this.address = address;
		Update();
	}

	public void Update()
	{
		timeSpan_1 = new TimeSpan(DateTime.Now.Ticks);
	}
}
