using System;
using System.IO;
using System.Net;

namespace UpLauncher;

public static class FtpClient
{
	public static bool Upload(string a_ServerPath, string a_RemoteName, string a_LocalName, string a_UserName, string a_UserPassword)
	{
		if (!File.Exists(a_LocalName))
		{
			return false;
		}
		FtpWebRequest ftpWebRequest = null;
		FtpWebResponse ftpWebResponse = null;
		Stream stream = null;
		try
		{
			if (a_ServerPath[a_ServerPath.Length - 1] != '/')
			{
				a_ServerPath += '/';
			}
			ftpWebRequest = (FtpWebRequest)WebRequest.Create($"ftp://{a_ServerPath}{a_RemoteName}");
			ftpWebRequest.Credentials = new NetworkCredential(a_UserName, a_UserPassword);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.KeepAlive = false;
			byte[] array = File.ReadAllBytes(a_LocalName);
			ftpWebRequest.ContentLength = array.Length;
			stream = ftpWebRequest.GetRequestStream();
			stream.Write(array, 0, array.Length);
			stream.Close();
			ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
			_ = ftpWebResponse.StatusDescription;
			ftpWebResponse.Close();
			return true;
		}
		catch (Exception)
		{
		}
		return false;
	}
}
