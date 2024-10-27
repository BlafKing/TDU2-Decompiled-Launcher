using System;
using System.Net;
using System.Net.Sockets;

namespace UPnPDetection.UPnP;

public static class UdpAddresses
{
	public static IPAddress UpnpMulticastV4Addr = IPAddress.Parse("239.255.255.250");

	public static IPAddress UpnpMulticastV6Addr1 = IPAddress.Parse("FF05::C");

	public static IPAddress UpnpMulticastV6Addr2 = IPAddress.Parse("FF02::C");

	public static IPEndPoint UpnpMulticastV4EndPoint = new IPEndPoint(UpnpMulticastV4Addr, 1900);

	public static IPEndPoint UpnpMulticastV6EndPoint1 = new IPEndPoint(UpnpMulticastV6Addr1, 1900);

	public static IPEndPoint UpnpMulticastV6EndPoint2 = new IPEndPoint(UpnpMulticastV6Addr2, 1900);

	private static bool bool_0 = false;

	private static bool bool_1 = false;

	public static string GetMulticastAddr(IPAddress addr)
	{
		if (addr.AddressFamily == AddressFamily.InterNetwork)
		{
			return "239.255.255.250";
		}
		if (addr.AddressFamily == AddressFamily.InterNetworkV6)
		{
			if (addr.IsIPv6LinkLocal)
			{
				return "FF02::C";
			}
			return "FF05::C";
		}
		return "";
	}

	public static string GetMulticastAddrBraket(IPAddress addr)
	{
		if (addr.AddressFamily == AddressFamily.InterNetwork)
		{
			return "239.255.255.250";
		}
		if (addr.AddressFamily == AddressFamily.InterNetworkV6)
		{
			if (addr.IsIPv6LinkLocal)
			{
				return "[FF02::C]";
			}
			return "[FF05::C]";
		}
		return "";
	}

	public static string GetMulticastAddrBraketPort(IPAddress addr)
	{
		if (addr.AddressFamily == AddressFamily.InterNetwork)
		{
			return "239.255.255.250:1900";
		}
		if (addr.AddressFamily == AddressFamily.InterNetworkV6)
		{
			if (addr.IsIPv6LinkLocal)
			{
				return "[FF02::C]:1900";
			}
			return "[FF05::C]:1900";
		}
		return "";
	}

	public static bool IsMono()
	{
		if (bool_0)
		{
			return bool_1;
		}
		bool_1 = Type.GetType("Mono.Runtime") != null;
		bool_0 = true;
		return bool_1;
	}
}
