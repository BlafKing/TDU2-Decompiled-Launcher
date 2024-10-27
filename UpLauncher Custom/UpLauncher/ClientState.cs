namespace UpLauncher;

public enum ClientState
{
	StateNone,
	StateDeleteCache,
	StateLoadLocalDb,
	StateTransferRemoteMainDb,
	StateDbUdpate,
	StateFilesTransfer,
	StateRelaunch,
	StateInstallFiles,
	StateRevertFiles,
	StateClearCache,
	StateWaitUserLaunchGame,
	StateLaunchGame,
	StateFinished,
	StateError
}
