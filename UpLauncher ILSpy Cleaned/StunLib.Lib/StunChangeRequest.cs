namespace StunLib.Lib;

public class StunChangeRequest
{
	private bool changeIP = true;

	private bool changePort = true;

	public bool ChangeIP
	{
		get
		{
			return changeIP;
		}
		set
		{
			changeIP = value;
		}
	}

	public bool ChangePort
	{
		get
		{
			return changePort;
		}
		set
		{
			changePort = value;
		}
	}

	public StunChangeRequest()
	{
	}

	public StunChangeRequest(bool changeIP, bool changePort)
	{
		this.changeIP = changeIP;
		this.changePort = changePort;
	}
}
