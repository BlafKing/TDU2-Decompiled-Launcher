using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;

namespace UpLauncher;

public class Installer
{
	public static bool CompresssFile(string a_strSrcFileName, string a_strDestFileName)
	{
		try
		{
			using FileStream fileStream2 = new FileStream(a_strSrcFileName, FileMode.Open);
			using FileStream fileStream = new FileStream(a_strDestFileName, FileMode.Create);
			using GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Compress);
			for (int num = fileStream2.ReadByte(); num != -1; num = fileStream2.ReadByte())
			{
				gZipStream.WriteByte((byte)num);
			}
			gZipStream.Close();
			fileStream2.Close();
			fileStream.Close();
			return true;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return false;
	}

	private static bool smethod_0(string string_0, string string_1)
	{
		bool result = false;
		try
		{
			using ZipFile zipFile = ZipFile.Read(string_0);
			if (zipFile.Entries.Count == 1)
			{
				zipFile.ExtractAll(string_1, ExtractExistingFileAction.OverwriteSilently);
				result = true;
			}
			else
			{
				Class25.smethod_3($"The archive {string_0} contains more than one file");
			}
		}
		catch (Exception ex)
		{
			Class25.smethod_3($"Exception: {ex.Message}");
		}
		return result;
	}

	public static bool UncompressEntry(FileEntry a_rEntry)
	{
		string directoryName = Path.GetDirectoryName(a_rEntry.GetTempFilePath());
		string text = "";
		text = ((!a_rEntry.FileIsMultiPart) ? a_rEntry.TempFilePath : (a_rEntry.GetPartTempFolder() + "\\" + Path.GetFileName(a_rEntry.TempFileName)));
		bool result;
		if (result = smethod_0(text, directoryName))
		{
			try
			{
				if (a_rEntry.FileIsMultiPart)
				{
					string partTempFolder = a_rEntry.GetPartTempFolder();
					Directory.Delete(partTempFolder, recursive: true);
					string path = partTempFolder + ".localdb";
					if (File.Exists(path))
					{
						File.Delete(path);
					}
				}
				else
				{
					File.Delete(a_rEntry.TempFilePath);
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Unable to delete the part files {a_rEntry.TempFilePath}: {arg}");
			}
		}
		return result;
	}

	public static bool RemoveFileAttributes(string a_strSrcFilePath)
	{
		return RemoveFileAttributes(a_strSrcFilePath, FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System);
	}

	public static bool RemoveFileAttributes(string a_strSrcFilePath, FileAttributes a_eAttributesToRemove)
	{
		if (string.IsNullOrEmpty(a_strSrcFilePath))
		{
			return false;
		}
		Class25.smethod_2($"Installer.RemoveFileAttributes({a_strSrcFilePath})");
		if (!File.Exists(a_strSrcFilePath))
		{
			return true;
		}
		try
		{
			FileAttributes attributes = File.GetAttributes(a_strSrcFilePath);
			string text = "FileAttributes: ";
			text += ((FileAttributes.ReadOnly == (attributes & FileAttributes.ReadOnly)) ? "R" : "-");
			text += ((FileAttributes.Hidden == (attributes & FileAttributes.Hidden)) ? "H" : "-");
			text += ((FileAttributes.System == (attributes & FileAttributes.System)) ? "S" : "-");
			text += ((FileAttributes.Directory == (attributes & FileAttributes.Directory)) ? "D" : "-");
			text += ((FileAttributes.Archive == (attributes & FileAttributes.Archive)) ? "A" : "-");
			text += ((FileAttributes.Device == (attributes & FileAttributes.Device)) ? "d" : "-");
			text += ((FileAttributes.Normal == (attributes & FileAttributes.Normal)) ? "N" : "-");
			text += ((FileAttributes.Temporary == (attributes & FileAttributes.Temporary)) ? "T" : "-");
			text += ((FileAttributes.SparseFile == (attributes & FileAttributes.SparseFile)) ? "s" : "-");
			text += ((FileAttributes.ReparsePoint == (attributes & FileAttributes.ReparsePoint)) ? "r" : "-");
			text += ((FileAttributes.Compressed == (attributes & FileAttributes.Compressed)) ? "C" : "-");
			text += ((FileAttributes.Offline == (attributes & FileAttributes.Offline)) ? "O" : "-");
			text += ((FileAttributes.NotContentIndexed == (attributes & FileAttributes.NotContentIndexed)) ? "i" : "-");
			text += ((FileAttributes.Encrypted == (attributes & FileAttributes.Encrypted)) ? "E" : "-");
			Class25.smethod_2(text);
			if ((attributes & a_eAttributesToRemove) != 0)
			{
				attributes &= ~a_eAttributesToRemove;
				File.SetAttributes(a_strSrcFilePath, attributes);
				attributes = File.GetAttributes(a_strSrcFilePath);
				return (attributes & a_eAttributesToRemove) == 0;
			}
			return true;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		return false;
	}

	public static string RemoveFileExtension(string a_strPath)
	{
		string directoryName = Path.GetDirectoryName(a_strPath);
		return directoryName + "\\" + Path.GetFileNameWithoutExtension(a_strPath);
	}

	public static bool InstallFile(FileEntry a_rEntry)
	{
		Class25.smethod_2($"Installer.InstallFile({a_rEntry.LocalFileName})");
		try
		{
			string directoryName = Path.GetDirectoryName(a_rEntry.LocalFilePath);
			Directory.CreateDirectory(directoryName);
			if (File.Exists(a_rEntry.LocalFilePath) && !RemoveFileAttributes(a_rEntry.LocalFilePath))
			{
				return false;
			}
			try
			{
				if (File.Exists(a_rEntry.LocalFilePath))
				{
					File.Copy(a_rEntry.LocalFilePath, a_rEntry.LocalFilePath + ".old", overwrite: true);
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_3(string.Format("Error: Unable to backup the file {0}", a_rEntry.LocalFilePath, arg));
				return false;
			}
			if ((a_rEntry.FileIsCompressed || a_rEntry.FileIsMultiPart) && !a_rEntry.FileIsDownloaded && !UncompressEntry(a_rEntry))
			{
				Class25.smethod_3($"Error: Unable to uncompress the file {a_rEntry.LocalFilePath}");
				return false;
			}
			try
			{
				File.Copy(a_rEntry.GetTempFilePath(), a_rEntry.LocalFilePath, overwrite: true);
			}
			catch (Exception arg2)
			{
				Class25.smethod_3($"Error: Unable to copy the file {a_rEntry.LocalFilePath}: {arg2}");
				return false;
			}
			a_rEntry.CheckLocalInfo();
			if (a_rEntry.LocalFileMD5 != a_rEntry.RemoteFileMD5)
			{
				Class25.smethod_3("Error: MD5 of files are not the same");
				return false;
			}
		}
		catch (Exception arg3)
		{
			Class25.smethod_3($"Exception: {arg3}");
			return false;
		}
		a_rEntry.InstallFile();
		return true;
	}

	public static bool LaunchFile(FileEntry a_rEntry)
	{
		Class25.smethod_2($"Installer.LaunchFile({a_rEntry.LocalFileName})");
		try
		{
			if (File.Exists(a_rEntry.LocalFilePath))
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo(a_rEntry.LocalFileName);
				processStartInfo.WorkingDirectory = Path.GetDirectoryName(a_rEntry.LocalFilePath);
				processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
				processStartInfo.UseShellExecute = true;
				processStartInfo.Arguments = a_rEntry.RunParameters;
				Process process = Process.Start(processStartInfo);
				do
				{
					Application.DoEvents();
					Thread.Sleep(100);
					process.Refresh();
				}
				while (!process.HasExited);
				return true;
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return false;
	}

	public static bool DeleteFile(FileEntry a_rEntry)
	{
		Class25.smethod_3($"Installer.DeleteFile({a_rEntry.LocalFileName})");
		try
		{
			if (File.Exists(a_rEntry.LocalFilePath) && RemoveFileAttributes(a_rEntry.LocalFilePath))
			{
				File.Move(a_rEntry.LocalFilePath, a_rEntry.LocalFilePath + ".old");
				if (File.Exists(a_rEntry.LocalFilePath + ".old"))
				{
					a_rEntry.InstallFile();
					return true;
				}
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return false;
	}

	public static bool DeleteOldFile(FileEntry a_rEntry)
	{
		try
		{
			if (File.Exists(a_rEntry.LocalFilePath + ".old"))
			{
				File.Delete(a_rEntry.LocalFilePath + ".old");
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return true;
	}

	public static bool DeleteTempFile(FileEntry a_rEntry)
	{
		try
		{
			if (File.Exists(a_rEntry.GetTempFilePath()))
			{
				File.Delete(a_rEntry.GetTempFilePath());
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
		return true;
	}

	public static bool RecoverFile(FileEntry a_rEntry)
	{
		try
		{
			File.Delete(a_rEntry.LocalFilePath);
			if (File.Exists(a_rEntry.LocalFilePath + ".old"))
			{
				File.Move(a_rEntry.LocalFilePath + ".old", a_rEntry.LocalFilePath);
			}
			return true;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
	}

	public static bool RecoverFileFromDatabase(LocalFileInfo a_rFileInfo)
	{
		try
		{
			if (File.Exists(a_rFileInfo.FullName))
			{
				File.Delete(a_rFileInfo.FullName);
			}
			if (File.Exists(a_rFileInfo.FullName + ".old"))
			{
				File.Move(a_rFileInfo.FullName + ".old", a_rFileInfo.FullName);
			}
			return true;
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
			return false;
		}
	}

	public static void ClearCache()
	{
		string[] directories = Directory.GetDirectories(Class32.string_27 + "\\DownloadCache\\");
		string[] array = directories;
		foreach (string path in array)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				if (directoryInfo.Exists && Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length == 0)
				{
					Directory.Delete(path, recursive: true);
				}
			}
			catch (Exception arg)
			{
				Class25.smethod_3($"Exception: {arg}");
			}
		}
	}

	public static void CleanOldFiles()
	{
		try
		{
			string[] files = Directory.GetFiles(Class32.string_27, "*.old", SearchOption.AllDirectories);
			string[] array = files;
			foreach (string path in array)
			{
				File.Delete(path);
			}
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
	}
}
