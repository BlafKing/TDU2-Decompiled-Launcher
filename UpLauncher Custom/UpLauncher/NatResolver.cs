using System;
using System.ComponentModel;
using StunLib.Client;
using StunLib.Lib;

namespace UpLauncher;

public class NatResolver
{
	public enum eStatus
	{
		Unchecked,
		Failed,
		Ok
	}

	public enum eStunStatus
	{
		Unchecked,
		Failed,
		Ok
	}

	public enum eUPnPStatus
	{
		Unchecked,
		Failed,
		Ok
	}

	private eStatus eStatus_0;

	private eUPnPStatus eUPnPStatus_0;

	private eStunStatus eStunStatus_0;

	private StunNetType stunNetType_0;

	private StunClient stunClient_0;

	private BackgroundWorker backgroundWorker_0;

	public eStatus Status => eStatus_0;

	public eStunStatus StunStatus => eStunStatus_0;

	public StunNetType NatType => stunNetType_0;

	public eUPnPStatus UPnPStatus => eUPnPStatus_0;

	public NatResolver()
	{
		eStatus_0 = eStatus.Unchecked;
		eUPnPStatus_0 = eUPnPStatus.Unchecked;
		eStunStatus_0 = eStunStatus.Unchecked;
		stunNetType_0 = StunNetType.Unchecked;
		stunClient_0 = null;
		backgroundWorker_0 = null;
	}

	public void Begin()
	{
		backgroundWorker_0 = new BackgroundWorker();
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.RunWorkerCompleted += backgroundWorker_0_RunWorkerCompleted;
		backgroundWorker_0.WorkerSupportsCancellation = true;
		backgroundWorker_0.RunWorkerAsync(this);
	}

	public void Stop()
	{
		if (backgroundWorker_0 != null && backgroundWorker_0.IsBusy && !backgroundWorker_0.CancellationPending)
		{
			backgroundWorker_0.CancelAsync();
		}
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		try
		{
			UPnPResolver uPnPResolver = new UPnPResolver();
			uPnPResolver.CheckUPnP();
			if (uPnPResolver.HasMappedPort)
			{
				eUPnPStatus_0 = eUPnPStatus.Ok;
				stunNetType_0 = StunNetType.UPnPUsed;
			}
			else
			{
				eUPnPStatus_0 = eUPnPStatus.Failed;
			}
			Class25.smethod_2($"NatResolver UPnPDiscover.AddPortMapping(\"UDP\", {(ushort)8889}, {(ushort)8889}, {Class32.ipaddress_0.ToString()}): {uPnPResolver.UPnPResult}");
		}
		catch (Exception arg)
		{
			eUPnPStatus_0 = eUPnPStatus.Failed;
			Class25.smethod_3($"NatResolver Error: {arg}");
		}
		Class25.smethod_2($"NatResolver m_eUPnPStatus={eUPnPStatus_0.ToString()}");
		if (eUPnPStatus_0 != eUPnPStatus.Ok)
		{
			uint num = 0u;
			Random random = new Random((int)(DateTime.Now.Ticks & 0x7FFFFFFL));
			int maxValue = Class32.string_17.Length - 1;
			int num2 = random.Next(0, maxValue);
			do
			{
				Class25.smethod_2($"NatResolver begin with server \"{Class32.string_17[num2]}\"");
				try
				{
					stunClient_0 = new StunClient(Class32.string_17[num2], 3478);
					Class25.smethod_2($"NatResolver StunClient.Bind({(ushort)8889})");
					if (stunClient_0.Bind(8889))
					{
						StunResult stunResult = stunClient_0.Run();
						stunNetType_0 = stunResult.NetType;
						eStunStatus_0 = eStunStatus.Ok;
					}
					else
					{
						eStunStatus_0 = eStunStatus.Failed;
						Class25.smethod_3($"NatResolver Error: unable to bind UDP port {(ushort)8889}");
					}
				}
				catch (Exception arg2)
				{
					eStunStatus_0 = eStunStatus.Failed;
					Class25.smethod_3($"NatResolver Error: {arg2}");
				}
				num++;
				num2 = random.Next(0, maxValue);
			}
			while (eStunStatus_0 == eStunStatus.Failed && num < 3);
		}
		Class25.smethod_2($"NatResolver m_eStunStatus={eStunStatus_0.ToString()}");
		eStatus_0 = eStatus.Ok;
		if (eUPnPStatus_0 == eUPnPStatus.Failed && eStunStatus_0 == eStunStatus.Failed)
		{
			eStatus_0 = eStatus.Failed;
			stunNetType_0 = StunNetType.UdpBlocked;
		}
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
}
