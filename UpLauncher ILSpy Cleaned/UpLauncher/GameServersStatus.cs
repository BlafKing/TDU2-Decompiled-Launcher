using System;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace UpLauncher;

public class GameServersStatus
{
	public enum eStatus
	{
		Unchecked,
		Failed,
		Ok
	}

	private bool bool_0;

	private eStatus eStatus_0;

	private WebClient webClient_0;

	private BackgroundWorker backgroundWorker_0;

	public eStatus Status => eStatus_0;

	public bool ServersAreAvailable => bool_0;

	public GameServersStatus()
	{
		bool_0 = false;
		eStatus_0 = eStatus.Unchecked;
		webClient_0 = null;
		backgroundWorker_0 = null;
	}

	public void Begin()
	{
		backgroundWorker_0 = new BackgroundWorker();
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.RunWorkerCompleted += backgroundWorker_0_RunWorkerCompleted;
		backgroundWorker_0.RunWorkerAsync(this);
	}

	private void webClient_0_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
	{
		if (!e.Cancelled && e.Error == null)
		{
			byte[] result = e.Result;
			string @string = Encoding.UTF8.GetString(result);
			bool_0 = string.Compare(@string, 0, "+OK ", 0, 4, ignoreCase: true) == 0;
			eStatus_0 = eStatus.Ok;
		}
		else if (e.Error != null)
		{
			eStatus_0 = eStatus.Failed;
			Class25.smethod_2($"GameServersStatus._EventDownloadDataCompleted() Error : #{e.Error}");
		}
		webClient_0 = null;
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		string uriString = Class32.string_29 + "getGameServersStatus.php";
		try
		{
			webClient_0 = new WebClient();
			webClient_0.DownloadDataCompleted += webClient_0_DownloadDataCompleted;
			webClient_0.Headers.Add(Class32.string_22);
			webClient_0.DownloadDataAsync(new Uri(uriString), null);
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
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
