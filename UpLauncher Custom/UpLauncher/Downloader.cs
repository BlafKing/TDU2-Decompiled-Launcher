using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace UpLauncher;

public class Downloader
{
	private WebClient webClient_0;

	private BackgroundWorker backgroundWorker_0;

	private BackgroundWorker backgroundWorker_1;

	private Queue<FileEntry> queue_0;

	private Database database_0;

	private FileEntry fileEntry_0;

	private IEnumerator<FileEntry> ienumerator_0;

	private string a_strRootDirectory;

	private string string_0;

	private string string_1;

	private ProgressInfo progressInfo_0;

	private ClientState clientState_0;

	private Mutex mutex_0;

	private ErrorCode errorCode_0;

	private ErrorCodeEx errorCodeEx_0;

	private bool bool_0;

	private bool bool_1;

	private IEnumerator<FileEntry> ienumerator_1;

	private LocalDatabase localDatabase_0;

	private string string_2;

	public bool IsOnlineAllowed => bool_0;

	public bool RecoverFromReverse => bool_1;

	public ClientState State => clientState_0;

	public ErrorCode Error => errorCode_0;

	public ErrorCodeEx ErrorEx => errorCodeEx_0;

	private bool method_0(string string_3)
	{
		return mutex_0.WaitOne();
	}

	private void method_1(string string_3)
	{
		mutex_0.ReleaseMutex();
	}

	private void method_2(object sender, ElapsedEventArgs e)
	{
		System.Timers.Timer timer = (System.Timers.Timer)sender;
		timer.Stop();
		method_0("OnTimerEvent");
		if (Update())
		{
			timer.Start();
		}
		else
		{
			timer = null;
		}
		method_1("OnTimerEvent");
	}

	public void GetProgressInfos(out ProgressInfo a_rGlobalProgressInfo)
	{
		a_rGlobalProgressInfo = progressInfo_0;
	}

	public void SetWaitUserLaunchGame()
	{
		if (clientState_0 == ClientState.StateError)
		{
			method_10(ClientState.StateWaitUserLaunchGame);
		}
	}

	public Downloader(string a_strRootDirectory)
	{
		Class25.smethod_2($"Downloader({a_strRootDirectory})");
		clientState_0 = ClientState.StateNone;
		errorCode_0 = ErrorCode.NoError;
		errorCodeEx_0 = ErrorCodeEx.NoError;
		this.a_strRootDirectory = a_strRootDirectory;
		string_0 = this.a_strRootDirectory + "DownloadCache\\";
		string_1 = Class32.string_19;
		webClient_0 = null;
		database_0 = new Database(Class32.string_30);
		fileEntry_0 = null;
		progressInfo_0 = new ProgressInfo();
		queue_0 = new Queue<FileEntry>();
		backgroundWorker_1 = new BackgroundWorker();
		backgroundWorker_1.DoWork += backgroundWorker_1_DoWork;
		backgroundWorker_1.WorkerSupportsCancellation = true;
		backgroundWorker_1.RunWorkerCompleted += backgroundWorker_1_RunWorkerCompleted;
		Directory.CreateDirectory(string_0);
		bool_0 = false;
		bool_1 = false;
	}

	public void Uninstall()
	{
		Class25.smethod_2("Downloader.Uninstall");
		if (File.Exists(Class32.string_31))
		{
			database_0.DbLocalFilesEntries.ReadFromFile(Class32.string_31);
			foreach (LocalFileInfo dbLocalFilesEntry in database_0.DbLocalFilesEntries)
			{
				Class25.smethod_2($"Deleting file {dbLocalFilesEntry.FullName}");
				try
				{
					Installer.RemoveFileAttributes(dbLocalFilesEntry.FullName);
					File.Delete(dbLocalFilesEntry.FullName);
				}
				catch (Exception ex)
				{
					Class25.smethod_3($"Unable to delete the file {dbLocalFilesEntry.FullName}: {ex.Message}");
				}
			}
		}
		else
		{
			try
			{
				WebClient webClient = new WebClient();
				webClient.Headers.Add(Class32.string_22);
				byte[] buffer = webClient.DownloadData(new Uri(Class32.string_30));
				database_0.ReadXmlFile(new MemoryStream(buffer, writable: false), method_3);
				foreach (FileEntry item in database_0)
				{
					Class25.smethod_2($"Deleting file {item.LocalFilePath}");
					try
					{
						Installer.RemoveFileAttributes(item.LocalFilePath);
						File.Delete(item.LocalFilePath);
					}
					catch (Exception ex2)
					{
						Class25.smethod_3($"Unable to delete the file {item.LocalFilePath}: {ex2.Message}");
					}
				}
			}
			catch (Exception ex3)
			{
				Class25.smethod_3($"Unable to download the update file: {ex3.Message}");
			}
		}
		try
		{
			Class25.smethod_2("Deleting cache folder");
			Directory.Delete(Class32.string_27 + "\\DownloadCache", recursive: true);
		}
		catch (Exception ex4)
		{
			Class25.smethod_3($"Unable to remove cache directory: {ex4.Message}");
		}
		try
		{
			Class25.smethod_2("Deleting local database file");
			Installer.RemoveFileAttributes(Class32.string_31);
			File.Delete(Class32.string_31);
		}
		catch (Exception ex5)
		{
			Class25.smethod_3($"Unable to delete the update history: {ex5.Message}");
		}
		try
		{
			Class25.smethod_2("Deleting registry entries");
			Class32.smethod_7("HttpServerRoot");
			Class32.smethod_7("GUID");
			Class32.smethod_7("NetworkNatType");
			Class32.smethod_7("AudioLib");
		}
		catch (Exception ex6)
		{
			Class25.smethod_3($"Unable to delete some registry entries: {ex6.Message}");
		}
	}

	public void StartUpdate()
	{
		Class25.smethod_2("Downloader.StartUpdate");
		errorCode_0 = ErrorCode.NoError;
		errorCodeEx_0 = ErrorCodeEx.NoError;
		mutex_0 = new Mutex(initiallyOwned: false);
		System.Timers.Timer timer = new System.Timers.Timer();
		timer.SynchronizingObject = MainForm.MainInstance;
		timer.Interval = 100.0;
		timer.Elapsed += method_2;
		timer.Start();
		method_10(ClientState.StateDeleteCache);
	}

	private void method_3(string string_3, XmlNode xmlNode_0, uint uint_0)
	{
		XmlElement xmlElement = (XmlElement)xmlNode_0;
		if (string.Compare(xmlNode_0.Name, "VersionConfig", ignoreCase: true) == 0)
		{
			string attribute = xmlElement.GetAttribute("root");
			if (!string.IsNullOrEmpty(attribute))
			{
				Class32.string_28 = attribute;
				if (!Class32.string_28.EndsWith("/"))
				{
					Class32.string_28 += "/";
				}
				Class25.smethod_2($"Updated StringHttpServerRoot from server = {Class32.string_28}");
			}
		}
		else
		{
			if (string.Compare(xmlNode_0.Name, "FilesGroup", ignoreCase: true) == 0 || string.Compare(xmlNode_0.Name, "Part", ignoreCase: true) == 0)
			{
				return;
			}
			FileEntry fileEntry = database_0.AddEntry(xmlNode_0, a_strRootDirectory, string_0, Class32.string_28);
			if (fileEntry != null)
			{
				if (fileEntry.FileIsGameExeFile)
				{
					string_1 = fileEntry.LocalFilePath;
				}
				else if (fileEntry.FileIsLauncher)
				{
					fileEntry.ChangeUpLauncherLocalFileName(Class4.smethod_0());
				}
			}
		}
	}

	private void webClient_0_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
	{
		method_0("_EventDownloadDataCompleted");
		Class25.smethod_2("Downloader._EventDownloadDataCompleted()");
		if (!e.Cancelled && e.Error == null)
		{
			if (ClientState.StateTransferRemoteMainDb == clientState_0)
			{
				database_0.ReadXmlFile(new MemoryStream(e.Result, writable: false), method_3);
				method_10(ClientState.StateDbUdpate);
			}
		}
		else
		{
			if (e.Error != null)
			{
				Class25.smethod_2($"Downloader._EventDownloadDataCompleted() Error : #{e.Error}");
				errorCode_0 = ErrorCode.ErrorDownloadFile;
				errorCodeEx_0 = ErrorCodeEx.ErrorDownloadUpLauncherDat;
			}
			else
			{
				Class25.smethod_2("Downloader._EventDownloadDataCompleted() Cancelled");
			}
			method_10(ClientState.StateError);
		}
		webClient_0 = null;
		method_1("_EventDownloadDataCompleted");
	}

	private void webClient_0_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		method_0("_EventDownloadProgressChanged");
		if (e.UserState != null)
		{
			_ = (FileEntry)e.UserState;
			ulong a_u64Value = (ulong)e.BytesReceived - progressInfo_0.CurrentBytesReceived;
			progressInfo_0.SetCurrentInfo((ulong)e.TotalBytesToReceive, (ulong)e.BytesReceived, (uint)e.ProgressPercentage, a_bRecomputePercentage: false);
			progressInfo_0.AddToTotalBytesReceived(a_u64Value);
			progressInfo_0.ComputeTotalPercent();
		}
		method_1("_EventDownloadProgressChanged");
	}

	private void webClient_0_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
	{
		method_0("_EventDownloadFileCompleted");
		progressInfo_0.ComputeTotalPercent();
		FileEntry fileEntry = (FileEntry)e.UserState;
		Class25.smethod_2($"Downloader._EventDownloadFileCompleted({fileEntry.LocalFileName}, {progressInfo_0.CurrentBytesReceived}/{progressInfo_0.CurrentBytesToReceive} bytes)");
		progressInfo_0.ResetCurrent();
		if (!e.Cancelled && e.Error == null)
		{
			if (fileEntry_0.TempFileExists)
			{
				if (fileEntry_0.FileIsLauncher)
				{
					fileEntry_0.ChangeUpLauncherLocalFileName(Class4.smethod_1());
					if (Installer.InstallFile(fileEntry_0))
					{
						database_0.AddFileInLocalFilesDatabase(fileEntry_0);
						if (Class4.smethod_8(fileEntry_0.LocalFilePath, bool_0: true))
						{
							method_10(ClientState.StateRelaunch);
						}
					}
					else
					{
						errorCode_0 = ErrorCode.ErrorInstallFile;
						errorCodeEx_0 = ErrorCodeEx.ErrorInstallLauncherFile;
					}
				}
				else
				{
					if (fileEntry_0.FileIsMultiPart)
					{
						string text = fileEntry_0.GetPartTempFolder() + "\\" + Path.GetFileName(fileEntry_0.TempFileName);
						try
						{
							if (File.Exists(text))
							{
								File.Delete(text);
							}
							File.Move(fileEntry_0.TempFilePath, text);
						}
						catch (Exception arg)
						{
							Class25.smethod_3($"Exception: {arg}");
						}
					}
					if (fileEntry_0.FileIsCompressed)
					{
						lock (queue_0)
						{
							queue_0.Enqueue(fileEntry_0);
						}
						if (!backgroundWorker_1.IsBusy)
						{
							backgroundWorker_1.RunWorkerAsync(this);
						}
					}
					else
					{
						lock (database_0)
						{
							database_0.AddFileInLocalFilesDatabase(fileEntry_0);
						}
					}
				}
			}
		}
		else
		{
			if (e.Error != null)
			{
				Class25.smethod_2($"Downloader._EventDownloadFileComplete() Error : #{e.Error}");
				errorCode_0 = ErrorCode.ErrorDownloadFile;
				errorCodeEx_0 = ErrorCodeEx.ErrorDownloadFile;
			}
			else
			{
				Class25.smethod_2("Downloader._EventDownloadFileComplete() Cancelled");
			}
			progressInfo_0.ErrorOccurs();
			if (fileEntry_0.TempFileExists)
			{
				try
				{
					File.Delete(fileEntry_0.TempFilePath);
				}
				catch (Exception arg2)
				{
					Class25.smethod_3($"Exception: {arg2}");
				}
			}
		}
		fileEntry_0 = null;
		webClient_0 = null;
		method_1("_EventDownloadFileCompleted");
	}

	private void method_4()
	{
		string path = fileEntry_0.GetPartTempFolder() + "\\" + Path.GetFileName(fileEntry_0.TempFileName);
		if (File.Exists(path))
		{
			progressInfo_0.AddToTotalBytesReceived(fileEntry_0.LocalFileSize);
			progressInfo_0.ComputeTotalPercent();
			progressInfo_0.ResetCurrent();
			lock (queue_0)
			{
				queue_0.Enqueue(fileEntry_0);
			}
			if (!backgroundWorker_1.IsBusy)
			{
				backgroundWorker_1.RunWorkerAsync(this);
			}
			fileEntry_0 = null;
			webClient_0 = null;
			return;
		}
		webClient_0 = new WebClient();
		webClient_0.DownloadFileCompleted += webClient_0_DownloadFileCompleted;
		webClient_0.DownloadProgressChanged += webClient_0_DownloadProgressChanged;
		webClient_0.Headers.Add(Class32.string_22);
		fileEntry_0.CurrentFilePart = 0;
		progressInfo_0.SetNewFileInfo(fileEntry_0);
		string directoryName = Path.GetDirectoryName(fileEntry_0.TempFilePath);
		Directory.CreateDirectory(directoryName);
		try
		{
			Class25.smethod_2($"DownloadFileAsync({fileEntry_0.RemoteFileFullPath})");
			webClient_0.DownloadFileAsync(new Uri(fileEntry_0.RemoteFileFullPath), fileEntry_0.TempFilePath, fileEntry_0);
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		if (fileEntry_0 != null)
		{
			ienumerator_1 = fileEntry_0.FileParts.GetEnumerator();
			string partTempFolder = fileEntry_0.GetPartTempFolder();
			if (!Directory.Exists(partTempFolder))
			{
				Directory.CreateDirectory(partTempFolder);
			}
			string_2 = partTempFolder + ".localdb";
			localDatabase_0 = new LocalDatabase();
			localDatabase_0.ReadFromFile(string_2);
			method_5();
		}
	}

	private void method_5()
	{
		webClient_0 = new WebClient();
		webClient_0.Headers.Add(Class32.string_22);
		ienumerator_1.MoveNext();
		fileEntry_0.CurrentFilePart++;
		FileEntry current = ienumerator_1.Current;
		if (current != null)
		{
			progressInfo_0.SetNewFileInfo(fileEntry_0);
			if (current.FileMustBeUpdated)
			{
				try
				{
					webClient_0.DownloadProgressChanged += webClient_0_DownloadProgressChanged;
					webClient_0.DownloadFileCompleted += webClient_0_DownloadFileCompleted_1;
					webClient_0.DownloadFileAsync(new Uri(current.RemoteFileFullPath), current.TempFilePath, current);
					return;
				}
				catch (Exception arg)
				{
					Class25.smethod_3($"Exception: {arg}");
					return;
				}
			}
			progressInfo_0.AddToTotalBytesReceived(current.LocalFileSize);
			progressInfo_0.ComputeTotalPercent();
			progressInfo_0.ResetCurrent();
			method_5();
		}
		else
		{
			method_4();
		}
	}

	private void webClient_0_DownloadFileCompleted_1(object sender, AsyncCompletedEventArgs e)
	{
		FileEntry fileEntry = e.UserState as FileEntry;
		Class25.smethod_2($"Downloader._EventDownloadPartFileCompleted({fileEntry.LocalFileName}, {progressInfo_0.CurrentBytesReceived}/{progressInfo_0.CurrentBytesToReceive} bytes)");
		if (!e.Cancelled && e.Error == null)
		{
			if (fileEntry.TempFileExists)
			{
				fileEntry.CheckLocalInfo();
				progressInfo_0.ComputeTotalPercent();
				progressInfo_0.ResetCurrent();
				fileEntry.InstallFile();
				localDatabase_0.AddFileFromFileEntry(fileEntry);
				localDatabase_0.WriteInFile(string_2);
				method_5();
				return;
			}
			Class25.smethod_2($"Downloader._EventDownloadPartFileCompleted() Error : #{e.Error}");
			errorCode_0 = ErrorCode.ErrorDownloadFile;
			errorCodeEx_0 = ErrorCodeEx.ErrorDownloadFile;
		}
		else
		{
			if (e.Error != null)
			{
				Class25.smethod_2($"Downloader._EventDownloadPartFileCompleted() Error : #{e.Error}");
				errorCode_0 = ErrorCode.ErrorDownloadFile;
				errorCodeEx_0 = ErrorCodeEx.ErrorDownloadFile;
			}
			else
			{
				Class25.smethod_2("Downloader._EventDownloadPartFileCompleted() Cancelled");
			}
			progressInfo_0.ErrorOccurs();
			if (fileEntry.TempFileExists)
			{
				try
				{
					File.Delete(fileEntry.TempFilePath);
				}
				catch (Exception arg)
				{
					Class25.smethod_3($"Exception: {arg}");
				}
			}
		}
		fileEntry_0 = null;
	}

	private bool method_6()
	{
		bool flag = true;
		foreach (FileEntry item in database_0)
		{
			if (!item.FileIsLauncher)
			{
				if (item.FileMustBeDeleted)
				{
					flag = Installer.DeleteFile(item);
				}
				else if (item.FileMustBeLaunched)
				{
					if (!item.FileIsInstalled)
					{
						flag = Installer.InstallFile(item);
					}
					if (flag)
					{
						flag = Installer.LaunchFile(item);
					}
				}
				else if (item.FileMustBeUpdated)
				{
					flag = Installer.InstallFile(item);
				}
			}
			if (!flag)
			{
				errorCode_0 = ErrorCode.ErrorInstallFile;
				if (item.FileIsGameExeFile)
				{
					errorCodeEx_0 = ErrorCodeEx.ErrorInstallGameExeFile;
				}
				else
				{
					errorCodeEx_0 = ErrorCodeEx.ErrorInstallFile;
				}
				return false;
			}
		}
		return true;
	}

	private bool method_7()
	{
		database_0.DbLocalFilesEntries.ReadFromFile(Class32.string_31);
		foreach (LocalFileInfo dbLocalFilesEntry in database_0.DbLocalFilesEntries)
		{
			if (!dbLocalFilesEntry.Installed && File.Exists(dbLocalFilesEntry.FullName + ".old"))
			{
				bool_1 = true;
				if (!Installer.RecoverFileFromDatabase(dbLocalFilesEntry))
				{
					errorCode_0 = ErrorCode.ErrorRecoverFile;
					errorCodeEx_0 = ErrorCodeEx.ErrorRecoverFile;
					return false;
				}
			}
		}
		return true;
	}

	private bool method_8()
	{
		foreach (FileEntry item in database_0)
		{
			if ((item.FileMustBeUpdated || item.FileMustBeDeleted) && !item.FileIsLauncher && !Installer.RecoverFile(item))
			{
				errorCode_0 = ErrorCode.ErrorRecoverFile;
				errorCodeEx_0 = ErrorCodeEx.ErrorRecoverFile;
				return false;
			}
		}
		return true;
	}

	private void method_9()
	{
		foreach (FileEntry item in database_0)
		{
			if (item.FileMustBeDeleted || item.FileMustBeUpdated)
			{
				Installer.DeleteOldFile(item);
				Installer.DeleteTempFile(item);
			}
		}
	}

	private void method_10(ClientState clientState_1)
	{
		Class25.smethod_2($"Downloader._ChangeState({clientState_0}, {clientState_1})");
		clientState_0 = clientState_1;
	}

	public void LaunchGame()
	{
		method_10(ClientState.StateLaunchGame);
	}

	private void backgroundWorker_1_DoWork(object sender, DoWorkEventArgs e)
	{
		while (true)
		{
			FileEntry fileEntry = null;
			lock (queue_0)
			{
				if (queue_0.Count <= 0)
				{
					break;
				}
				fileEntry = queue_0.Dequeue();
			}
			lock (fileEntry)
			{
				if (Installer.UncompressEntry(fileEntry))
				{
					lock (database_0)
					{
						database_0.AddFileInLocalFilesDatabase(fileEntry);
					}
				}
			}
		}
	}

	private void backgroundWorker_1_RunWorkerCompleted(object sender, AsyncCompletedEventArgs e)
	{
		if (e.Cancelled)
		{
			Class25.smethod_2("Uncompress Async canceled");
		}
		else if (e.Error != null)
		{
			Class25.smethod_3($"Uncompress Async failed on {e.UserState}: {e.Error}");
		}
		else
		{
			Class25.smethod_2("Uncompress Async finished");
		}
	}

	public bool Update()
	{
		switch (clientState_0)
		{
		case ClientState.StateDeleteCache:
			try
			{
				if (!method_7())
				{
					method_10(ClientState.StateError);
					return true;
				}
				Installer.CleanOldFiles();
				if (bool_1)
				{
					errorCode_0 = ErrorCode.ErrorInstallFile;
					errorCodeEx_0 = ErrorCodeEx.ErrorInstallFile;
					method_10(ClientState.StateError);
					return true;
				}
				Installer.ClearCache();
			}
			catch (Exception arg2)
			{
				Class25.smethod_3($"Exception: {arg2}");
			}
			method_10(ClientState.StateLoadLocalDb);
			break;
		case ClientState.StateLoadLocalDb:
			try
			{
				webClient_0 = new WebClient();
				webClient_0.DownloadDataCompleted += webClient_0_DownloadDataCompleted;
				webClient_0.Headers.Add(Class32.string_22);
				Class25.smethod_2($"DownloadDataAsync({Class32.string_30})");
				webClient_0.DownloadDataAsync(new Uri(Class32.string_30), null);
			}
			catch (Exception arg3)
			{
				Class25.smethod_3($"Exception: {arg3}");
				errorCode_0 = ErrorCode.ErrorDownloadFile;
				errorCodeEx_0 = ErrorCodeEx.ErrorDownloadFile;
			}
			method_10(ClientState.StateTransferRemoteMainDb);
			break;
		case ClientState.StateDbUdpate:
			database_0.CheckLocalFiles();
			ienumerator_0 = database_0.GetMustBeUpdatedEnumerator();
			database_0.GetGlobalInfo(progressInfo_0);
			if (progressInfo_0.TotalBytesToReceive > 0L)
			{
				string text = "win32_logicaldisk.deviceid=\"";
				text += Path.GetPathRoot(a_strRootDirectory).Substring(0, 2);
				text += "\"";
				ManagementObject managementObject = new ManagementObject(text);
				if (managementObject != null)
				{
					managementObject.Get();
					string s = managementObject["FreeSpace"].ToString();
					ulong num2 = ulong.Parse(s);
					if (progressInfo_0.TotalRequiredBytes > num2)
					{
						Class25.smethod_3($"Error: There is not enough disk space on \"{Class32.string_27}\" to install update components. Update requires an additional {progressInfo_0.TotalRequiredBytes} bytes of free space.");
						method_10(ClientState.StateError);
						errorCode_0 = ErrorCode.ErrorDiskSpace;
						errorCodeEx_0 = ErrorCodeEx.ErrorDiskSpace;
						break;
					}
				}
				method_10(ClientState.StateFilesTransfer);
			}
			else if (database_0.FilesMustBeLaunched)
			{
				method_10(ClientState.StateFilesTransfer);
			}
			else
			{
				bool_0 = true;
				progressInfo_0.ComputeTotalPercent();
				method_10(ClientState.StateClearCache);
			}
			break;
		case ClientState.StateFilesTransfer:
			if (fileEntry_0 != null)
			{
				break;
			}
			ienumerator_0.MoveNext();
			fileEntry_0 = ienumerator_0.Current;
			if (fileEntry_0 != null)
			{
				if (fileEntry_0.FileMustBeDeleted)
				{
					fileEntry_0 = null;
				}
				else if (fileEntry_0.FileMustBeUpdated)
				{
					if (!fileEntry_0.FileIsDownloaded)
					{
						if (!fileEntry_0.FileIsMultiPart)
						{
							webClient_0 = new WebClient();
							webClient_0.DownloadFileCompleted += webClient_0_DownloadFileCompleted;
							webClient_0.DownloadProgressChanged += webClient_0_DownloadProgressChanged;
							webClient_0.Headers.Add(Class32.string_22);
							progressInfo_0.SetNewFileInfo(fileEntry_0);
							string directoryName = Path.GetDirectoryName(fileEntry_0.TempFilePath);
							Directory.CreateDirectory(directoryName);
							try
							{
								webClient_0.DownloadFileAsync(new Uri(fileEntry_0.RemoteFileFullPath), fileEntry_0.TempFilePath, fileEntry_0);
							}
							catch (Exception arg4)
							{
								Class25.smethod_3($"Exception: {arg4}");
								fileEntry_0 = null;
							}
							break;
						}
						if (backgroundWorker_0 != null)
						{
							if (backgroundWorker_0.IsBusy)
							{
								mutex_0.WaitOne();
							}
							backgroundWorker_0.Dispose();
						}
						backgroundWorker_0 = new BackgroundWorker();
						backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
						backgroundWorker_0.WorkerSupportsCancellation = false;
						backgroundWorker_0.RunWorkerAsync();
						break;
					}
					progressInfo_0.SetNewFileInfo(fileEntry_0);
					if (fileEntry_0.FileIsMultiPart)
					{
						ulong num = fileEntry_0.TempFileSize;
						foreach (FileEntry filePart in fileEntry_0.FileParts)
						{
							num += filePart.RemoteFileSize;
						}
						progressInfo_0.AddToTotalBytesReceived(num);
					}
					else
					{
						progressInfo_0.AddToTotalBytesReceived(fileEntry_0.TempFileSize);
					}
					progressInfo_0.ComputeTotalPercent();
					progressInfo_0.ResetCurrent();
					fileEntry_0 = null;
				}
				else
				{
					fileEntry_0 = null;
				}
			}
			else
			{
				method_10(ClientState.StateInstallFiles);
			}
			break;
		case ClientState.StateRelaunch:
			Class4.smethod_3();
			method_10(ClientState.StateFinished);
			break;
		case ClientState.StateInstallFiles:
			if (backgroundWorker_1.IsBusy)
			{
				if (!backgroundWorker_1.CancellationPending)
				{
					backgroundWorker_1.CancelAsync();
				}
				break;
			}
			if (!method_6())
			{
				method_10(ClientState.StateRevertFiles);
				break;
			}
			foreach (FileEntry item in database_0)
			{
				if (!item.FileIsLauncher && !item.FileMustBeDeleted && item.FileMustBeUpdated && item.FileIsDownloaded)
				{
					database_0.AddFileInLocalFilesDatabase(item);
					if (item.FileIsGameExeFile)
					{
						Class32.smethod_11();
					}
				}
			}
			bool_0 = true;
			method_10(ClientState.StateClearCache);
			break;
		case ClientState.StateRevertFiles:
			if (backgroundWorker_1.IsBusy)
			{
				if (!backgroundWorker_1.CancellationPending)
				{
					backgroundWorker_1.CancelAsync();
				}
				break;
			}
			method_8();
			if (errorCode_0 != 0)
			{
				method_10(ClientState.StateError);
			}
			else
			{
				method_10(ClientState.StateWaitUserLaunchGame);
			}
			break;
		case ClientState.StateClearCache:
			method_9();
			Installer.ClearCache();
			if (progressInfo_0.Error)
			{
				method_10(ClientState.StateError);
				errorCode_0 = ErrorCode.ErrorDownloadFile;
			}
			else if (string.IsNullOrEmpty(string_1))
			{
				Class25.smethod_3("Error: GameFileName NULL");
				method_10(ClientState.StateError);
				errorCode_0 = ErrorCode.ErrorGameInstall;
				errorCodeEx_0 = ErrorCodeEx.ErrorGameExeNull;
			}
			else if (File.Exists(string_1))
			{
				method_10(ClientState.StateWaitUserLaunchGame);
			}
			else
			{
				Class25.smethod_3($"Error: GameFileName \"{string_1}\" does not exists");
				method_10(ClientState.StateError);
				errorCode_0 = ErrorCode.ErrorGameInstall;
				errorCodeEx_0 = ErrorCodeEx.ErrorGameExeNotExist;
			}
			break;
		case ClientState.StateLaunchGame:
			if (!string.IsNullOrEmpty(string_1))
			{
				try
				{
					if (File.Exists(string_1))
					{
						Mutex mutex = null;
						if (!bool_0)
						{
							bool createdNew = false;
							mutex = new Mutex(initiallyOwned: false, "957e4cc3", out createdNew);
							if (!createdNew)
							{
								errorCode_0 = ErrorCode.Error;
								errorCodeEx_0 = ErrorCodeEx.ErrorCreateMutex;
								method_10(ClientState.StateError);
								return true;
							}
						}
						Process process = new Process();
						process.StartInfo.Arguments = Class32.smethod_18();
						process.StartInfo.FileName = string_1;
						process.StartInfo.WorkingDirectory = Class32.string_27;
						process.Start();
						do
						{
							Thread.Sleep(100);
							process.Refresh();
							Application.DoEvents();
						}
						while (process.MainWindowHandle == IntPtr.Zero);
						if (!process.HasExited)
						{
							bool createdNew2 = true;
							string name = "TestDriveUnlimited";
							while (createdNew2 && !process.HasExited)
							{
								Application.DoEvents();
								Mutex mutex2 = new Mutex(initiallyOwned: false, name, out createdNew2);
								mutex2.WaitOne();
								mutex2.Close();
							}
							if (!bool_0)
							{
								mutex?.ReleaseMutex();
							}
						}
					}
					else
					{
						Class25.smethod_3("Error: GameFileExe does not exist");
						errorCode_0 = ErrorCode.ErrorGameInstall;
						errorCodeEx_0 = ErrorCodeEx.ErrorGameExeNotExist;
						method_10(ClientState.StateError);
					}
				}
				catch (Exception arg)
				{
					Class25.smethod_3($"Exception: {arg}");
				}
				method_10(ClientState.StateFinished);
			}
			else
			{
				Class25.smethod_3("Error: GameFileName NULL");
				errorCode_0 = ErrorCode.ErrorGameInstall;
				errorCodeEx_0 = ErrorCodeEx.ErrorGameExeNull;
				method_10(ClientState.StateError);
			}
			break;
		case ClientState.StateFinished:
			Class25.smethod_2("Downloader::Update() StateFinished return false to end");
			return false;
		}
		return true;
	}
}
