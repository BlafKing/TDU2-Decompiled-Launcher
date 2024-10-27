namespace UPnPDetection.UPnP;

public enum UPnPState
{
	UPnPNotTested,
	UPnPNotDetected,
	UPnPDetectedNotTested,
	UPnPDetectedNotMatching,
	UPnPDetectedMatching,
	UPnPFailed,
	UPnPMapped,
	UPnPMappingFailed
}
