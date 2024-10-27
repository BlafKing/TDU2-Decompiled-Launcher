using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace UpLauncher;

public class KeyChecker
{
	public enum eStatus
	{
		Unchecked,
		Failed,
		Ok
	}

	private string string_0;

	private bool bool_0;

	private eStatus eStatus_0;

	private WebClient webClient_0;

	private BackgroundWorker backgroundWorker_0;

	public eStatus Status => eStatus_0;

	public bool KeyIsValid => bool_0;

	public KeyChecker()
	{
		string_0 = null;
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
			Class25.smethod_2($"KeyChecker._EventDownloadDataCompleted() Error : #{e.Error}");
		}
		webClient_0 = null;
	}

	private bool method_0()
	{
		string path = Class32.string_27 + "key.txt";
		if (File.Exists(path))
		{
			byte[] bytes = File.ReadAllBytes(path);
			string_0 = Encoding.ASCII.GetString(bytes);
			if (!string.IsNullOrEmpty(string_0))
			{
				string_0 = string_0.Trim();
				string_0 = string_0.Substring(0, 19).ToUpper();
			}
			return !string.IsNullOrEmpty(string_0);
		}
		return false;
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		if (!method_0())
		{
			eStatus_0 = eStatus.Failed;
			backgroundWorker_0 = null;
			return;
		}
		string text = "http://clientstats.testdriveunlimited2.com/final/isKeyValid.php";
		text = text + "?key=" + HttpUtility.UrlEncode(string_0);
		try
		{
			webClient_0 = new WebClient();
			webClient_0.DownloadDataCompleted += webClient_0_DownloadDataCompleted;
			webClient_0.Headers.Add(Class32.string_22);
			webClient_0.DownloadDataAsync(new Uri(text), null);
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
