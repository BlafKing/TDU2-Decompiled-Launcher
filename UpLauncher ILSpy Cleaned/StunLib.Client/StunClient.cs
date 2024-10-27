using System;
using System.Net;
using System.Net.Sockets;
using StunLib.Lib;

namespace StunLib.Client;

public class StunClient
{
	public const int ReceiveTimeout = 5000;

	public const int SendTimeout = 5000;

	private static int int_0;

	private static string string_0;

	private Socket socket_0;

	public StunClient(string server, ushort port)
	{
		if (string.IsNullOrEmpty(server))
		{
			throw new FormatException("no server defined");
		}
		string_0 = server;
		int_0 = port;
	}

	public bool Bind(ushort local_port)
	{
		try
		{
			socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			socket_0.Bind(new IPEndPoint(IPAddress.Any, local_port));
			return true;
		}
		catch (Exception arg)
		{
			Class25.smethod_2($"StunClient Error Socket: {arg}");
			return false;
		}
	}

	public StunResult Run()
	{
		if (socket_0 == null)
		{
			throw new ArgumentNullException("socket");
		}
		IPEndPoint ipendPoint_ = new IPEndPoint(Dns.GetHostAddresses(string_0)[0], int_0);
		socket_0.ReceiveTimeout = 5000;
		StunMessage stunMessage = new StunMessage();
		stunMessage.Type = StunMessageType.BindingRequest;
		StunMessage stunMessage2 = smethod_0(stunMessage, socket_0, ipendPoint_);
		if (stunMessage2 == null)
		{
			socket_0.Close();
			return new StunResult(StunNetType.UdpBlocked, null);
		}
		StunMessage stunMessage3 = new StunMessage();
		stunMessage3.Type = StunMessageType.BindingRequest;
		stunMessage3.ChangeRequest = new StunChangeRequest(changeIP: true, changePort: true);
		if (socket_0.LocalEndPoint.Equals(stunMessage2.MappedAddress))
		{
			StunMessage stunMessage4 = smethod_0(stunMessage3, socket_0, ipendPoint_);
			if (stunMessage4 != null)
			{
				socket_0.Close();
				return new StunResult(StunNetType.OpenInternet, stunMessage2.MappedAddress);
			}
			socket_0.Close();
			return new StunResult(StunNetType.SymmetricUdpFirewall, stunMessage2.MappedAddress);
		}
		StunMessage stunMessage5 = smethod_0(stunMessage3, socket_0, ipendPoint_);
		if (stunMessage5 != null)
		{
			socket_0.Close();
			return new StunResult(StunNetType.FullCone, stunMessage2.MappedAddress);
		}
		StunMessage stunMessage6 = new StunMessage();
		stunMessage6.Type = StunMessageType.BindingRequest;
		StunMessage stunMessage7 = smethod_0(stunMessage6, socket_0, stunMessage2.ChangedAddress);
		if (stunMessage7 == null)
		{
			socket_0.Close();
			throw new Exception("STUN Test I(2nd) didn't get any  response !");
		}
		if (!stunMessage7.MappedAddress.Equals(stunMessage2.MappedAddress))
		{
			socket_0.Close();
			return new StunResult(StunNetType.Symmetric, stunMessage2.MappedAddress);
		}
		StunMessage stunMessage8 = new StunMessage();
		stunMessage8.Type = StunMessageType.BindingRequest;
		stunMessage8.ChangeRequest = new StunChangeRequest(changeIP: false, changePort: true);
		StunMessage stunMessage9 = smethod_0(stunMessage8, socket_0, stunMessage2.ChangedAddress);
		if (stunMessage9 != null)
		{
			socket_0.Close();
			return new StunResult(StunNetType.RestrictedCone, stunMessage2.MappedAddress);
		}
		socket_0.Close();
		return new StunResult(StunNetType.PortRestrictedCone, stunMessage2.MappedAddress);
	}

	private static StunMessage smethod_0(StunMessage stunMessage_0, Socket socket_1, IPEndPoint ipendPoint_0)
	{
		EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
		byte[] array = stunMessage_0.ToByteData();
		DateTime now = DateTime.Now;
		while (now.AddSeconds(2.0) > DateTime.Now)
		{
			try
			{
				socket_1.SendTo(array, ipendPoint_0);
				Class25.smethod_2($"STUN: SendTo({array.Length} bytes, {ipendPoint_0.ToString()})");
				byte[] array2 = new byte[512];
				int num = socket_1.ReceiveFrom(array2, ref remoteEP);
				Class25.smethod_2($"STUN: ReceiveFrom({num} bytes, {remoteEP.ToString()})");
				if (num > 0)
				{
					StunMessage stunMessage = new StunMessage();
					stunMessage.Parse(array2, num);
					if (stunMessage_0.TransactionID.Equals(stunMessage.TransactionID))
					{
						return stunMessage;
					}
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_2($"StunClient Error: {arg}");
			}
		}
		return null;
	}
}
