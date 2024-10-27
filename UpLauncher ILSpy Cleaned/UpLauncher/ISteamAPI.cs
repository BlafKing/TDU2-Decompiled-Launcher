using System.IO;

namespace UpLauncher;

public class ISteamAPI
{
	public bool DetectCurrentDirectory(string a_szPath)
	{
		bool result = false;
		string path = a_szPath + "\\steam_api.dll";
		if (File.Exists(path))
		{
			result = true;
		}
		return result;
	}
}
