using System;
using System.Threading;
using UPnPDetection.UPnP;

namespace UpLauncher;

public class UPnPResolver
{
	private UPnPDetectionFlow upnPDetectionFlow_0;

	private bool bool_0;

	private UPnPDetector upnPDetector_0;

	public UPnPDetectionFlow UPnPResult => upnPDetectionFlow_0;

	public bool HasMappedPort => bool_0;

	public UPnPResolver()
	{
		bool_0 = false;
		upnPDetectionFlow_0 = UPnPDetectionFlow.Idle;
		upnPDetector_0 = new UPnPDetector();
		upnPDetector_0.FindDevicesAsync();
	}

	public void CheckUPnP()
	{
		bool flag = false;
		DateTime now = DateTime.Now;
		int num = 30;
		bool flag2 = false;
		while (!flag && !flag2)
		{
			if (DateTime.Now.AddSeconds(-num) > now)
			{
				flag2 = true;
				continue;
			}
			lock (upnPDetector_0)
			{
				if (upnPDetector_0.DetectionState != upnPDetectionFlow_0)
				{
					upnPDetectionFlow_0 = upnPDetector_0.DetectionState;
					switch (upnPDetector_0.DetectionState)
					{
					case UPnPDetectionFlow.Detecting:
						Class25.smethod_2("Detecting UPnP network devices");
						break;
					case UPnPDetectionFlow.Mapping:
						Class25.smethod_2("An UPnP device was found matching TDU2 needs");
						Class25.smethod_2("Mapping the required port...");
						upnPDetector_0.AddPortMapping();
						break;
					case UPnPDetectionFlow.Done:
						flag = true;
						switch (upnPDetector_0.State)
						{
						case UPnPState.UPnPNotDetected:
							Class25.smethod_2("No UPnP device was found");
							break;
						case UPnPState.UPnPDetectedNotMatching:
							Class25.smethod_2("An UPnP device was found but it does not match the TDU2 needs");
							break;
						default:
							Class25.smethod_3($"UPnP Detection finished while state is {upnPDetector_0.State}");
							flag = false;
							break;
						case UPnPState.UPnPFailed:
							Class25.smethod_3("UPnP detection failed");
							break;
						case UPnPState.UPnPMapped:
							Class25.smethod_2("The required port was well mapped");
							bool_0 = true;
							break;
						case UPnPState.UPnPMappingFailed:
							Class25.smethod_2("Unable to map the required port");
							break;
						}
						break;
					case UPnPDetectionFlow.Failed:
						Class25.smethod_3("UPnP detection failed");
						break;
					case UPnPDetectionFlow.Notifying:
						break;
					}
				}
			}
			Thread.Sleep(10);
		}
		if (flag)
		{
			Class25.smethod_2("UPnP Detection has finished. You can report the result");
		}
	}
}
