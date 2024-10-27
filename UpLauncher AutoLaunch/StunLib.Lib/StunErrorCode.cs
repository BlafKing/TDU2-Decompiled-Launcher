namespace StunLib.Lib;

public class StunErrorCode
{
	private int code;

	private string message = "";

	public int Code
	{
		get
		{
			return code;
		}
		set
		{
			code = value;
		}
	}

	public string Message
	{
		get
		{
			return message;
		}
		set
		{
			message = value;
		}
	}

	public StunErrorCode(int code, string message)
	{
		this.code = code;
		this.message = message;
	}
}
