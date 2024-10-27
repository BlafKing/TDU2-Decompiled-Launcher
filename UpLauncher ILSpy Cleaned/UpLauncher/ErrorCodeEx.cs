namespace UpLauncher;

public enum ErrorCodeEx : uint
{
	NoError,
	ErrorDownloadUpLauncherDat,
	ErrorInstallLauncherFile,
	ErrorInstallGameExeFile,
	ErrorInstallFile,
	ErrorRecoverFile,
	ErrorDownloadFile,
	ErrorDiskSpace,
	ErrorGameExeNull,
	ErrorGameExeNotExist,
	ErrorInstallDirectoryNull,
	ErrorRegistryNull,
	ErrorMinimalRequirementRAM,
	ErrorMinimalRequirementVRAM,
	ErrorMinimalRequirementShaders,
	ErrorCreateMutex,
	ErrorKeyBanned
}
