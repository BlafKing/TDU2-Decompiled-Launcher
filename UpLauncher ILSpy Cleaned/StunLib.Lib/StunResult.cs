using System.Net;

namespace StunLib.Lib;

public class StunResult
{
	private StunNetType netType;

	private IPEndPoint publicEndPoint;

	public StunNetType NetType => netType;

	public IPEndPoint PublicEndPoint => publicEndPoint;

	public StunResult(StunNetType netType, IPEndPoint publicEndPoint)
	{
		this.netType = netType;
		this.publicEndPoint = publicEndPoint;
	}
}
