using System;
using System.Net;
using System.Text;

namespace StunLib.Lib;

public class StunMessage
{
	private enum Enum1
	{
		const_0 = 1,
		const_1 = 2,
		const_2 = 3,
		const_3 = 4,
		const_4 = 5,
		const_5 = 6,
		const_6 = 7,
		const_7 = 8,
		const_8 = 9,
		const_9 = 10,
		const_10 = 11,
		const_11 = 32800,
		const_12 = 33,
		const_13 = 32802
	}

	private enum Enum2
	{
		const_0 = 1,
		const_1
	}

	private StunMessageType stunMessageType_0 = StunMessageType.BindingRequest;

	private Guid guid_0 = Guid.Empty;

	private IPEndPoint ipendPoint_0;

	private IPEndPoint ipendPoint_1;

	private StunChangeRequest stunChangeRequest_0;

	private IPEndPoint ipendPoint_2;

	private IPEndPoint ipendPoint_3;

	private string string_0;

	private string string_1;

	private StunErrorCode stunErrorCode_0;

	private IPEndPoint ipendPoint_4;

	private string string_2;

	public StunMessageType Type
	{
		get
		{
			return stunMessageType_0;
		}
		set
		{
			stunMessageType_0 = value;
		}
	}

	public Guid TransactionID => guid_0;

	public IPEndPoint MappedAddress
	{
		get
		{
			return ipendPoint_0;
		}
		set
		{
			ipendPoint_0 = value;
		}
	}

	public IPEndPoint ResponseAddress
	{
		get
		{
			return ipendPoint_1;
		}
		set
		{
			ipendPoint_1 = value;
		}
	}

	public StunChangeRequest ChangeRequest
	{
		get
		{
			return stunChangeRequest_0;
		}
		set
		{
			stunChangeRequest_0 = value;
		}
	}

	public IPEndPoint SourceAddress
	{
		get
		{
			return ipendPoint_2;
		}
		set
		{
			ipendPoint_2 = value;
		}
	}

	public IPEndPoint ChangedAddress
	{
		get
		{
			return ipendPoint_3;
		}
		set
		{
			ipendPoint_3 = value;
		}
	}

	public string UserName
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
		}
	}

	public string Password
	{
		get
		{
			return string_1;
		}
		set
		{
			string_1 = value;
		}
	}

	public StunErrorCode ErrorCode
	{
		get
		{
			return stunErrorCode_0;
		}
		set
		{
			stunErrorCode_0 = value;
		}
	}

	public IPEndPoint ReflectedFrom
	{
		get
		{
			return ipendPoint_4;
		}
		set
		{
			ipendPoint_4 = value;
		}
	}

	public string ServerName
	{
		get
		{
			return string_2;
		}
		set
		{
			string_2 = value;
		}
	}

	public StunMessage()
	{
		guid_0 = Guid.NewGuid();
	}

	public void Parse(byte[] data, int dataLength)
	{
		if (data.Length >= 20 && dataLength >= 20 && data.Length >= dataLength)
		{
			int num = 0;
			num = 1;
			int num2 = data[0] << 8;
			num = 2;
			switch ((StunMessageType)(num2 | data[1]))
			{
			default:
				throw new ArgumentException("Invalid STUN message type value !");
			case StunMessageType.BindingErrorResponse:
				stunMessageType_0 = StunMessageType.BindingErrorResponse;
				break;
			case StunMessageType.SharedSecretErrorResponse:
				stunMessageType_0 = StunMessageType.SharedSecretErrorResponse;
				break;
			case StunMessageType.BindingResponse:
				stunMessageType_0 = StunMessageType.BindingResponse;
				break;
			case StunMessageType.BindingRequest:
				stunMessageType_0 = StunMessageType.BindingRequest;
				break;
			case StunMessageType.SharedSecretRequest:
				stunMessageType_0 = StunMessageType.SharedSecretRequest;
				break;
			}
			int num3 = (data[num++] << 8) | data[num++];
			Class25.smethod_2($"STUNMESSAGE: Type={stunMessageType_0.ToString()}, Length={num3} bytes");
			byte[] array = new byte[16];
			Array.Copy(data, num, array, 0, 16);
			guid_0 = new Guid(array);
			num += 16;
			while (num - 20 < num3)
			{
				method_0(data, ref num);
			}
			return;
		}
		throw new ArgumentException("Invalid STUN message value !");
	}

	public byte[] ToByteData()
	{
		byte[] array = new byte[512];
		int num = 0;
		num = 1;
		array[0] = (byte)((int)Type >> 8);
		num = 2;
		array[1] = (byte)(Type & (StunMessageType)255);
		num = 3;
		array[2] = 0;
		num = 4;
		array[3] = 0;
		Array.Copy(guid_0.ToByteArray(), 0, array, 4, 16);
		num = 20;
		if (MappedAddress != null)
		{
			method_2(Enum1.const_0, MappedAddress, array, ref num);
		}
		else if (ResponseAddress != null)
		{
			method_2(Enum1.const_1, ResponseAddress, array, ref num);
		}
		else if (ChangeRequest != null)
		{
			array[num++] = 0;
			array[num++] = 3;
			array[num++] = 0;
			array[num++] = 4;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = (byte)((Convert.ToInt32(ChangeRequest.ChangeIP) << 2) | (Convert.ToInt32(ChangeRequest.ChangePort) << 1));
		}
		else if (SourceAddress != null)
		{
			method_2(Enum1.const_3, SourceAddress, array, ref num);
		}
		else if (ChangedAddress != null)
		{
			method_2(Enum1.const_4, ChangedAddress, array, ref num);
		}
		else if (UserName != null)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(UserName);
			array[num++] = 0;
			array[num++] = 6;
			array[num++] = (byte)(bytes.Length >> 8);
			array[num++] = (byte)((uint)bytes.Length & 0xFFu);
			Array.Copy(bytes, 0, array, num, bytes.Length);
			num += bytes.Length;
		}
		else if (Password != null)
		{
			byte[] bytes2 = Encoding.ASCII.GetBytes(UserName);
			array[num++] = 0;
			array[num++] = 7;
			array[num++] = (byte)(bytes2.Length >> 8);
			array[num++] = (byte)((uint)bytes2.Length & 0xFFu);
			Array.Copy(bytes2, 0, array, num, bytes2.Length);
			num += bytes2.Length;
		}
		else if (ErrorCode != null)
		{
			byte[] bytes3 = Encoding.ASCII.GetBytes(ErrorCode.Message);
			array[num++] = 0;
			array[num++] = 9;
			array[num++] = 0;
			array[num++] = (byte)(4 + bytes3.Length);
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = (byte)Math.Floor((double)(ErrorCode.Code / 100));
			array[num++] = (byte)((uint)ErrorCode.Code & 0xFFu);
			Array.Copy(bytes3, array, bytes3.Length);
			num += bytes3.Length;
		}
		else if (ReflectedFrom != null)
		{
			method_2(Enum1.const_10, ReflectedFrom, array, ref num);
		}
		array[2] = (byte)(num - 20 >> 8);
		array[3] = (byte)((uint)(num - 20) & 0xFFu);
		byte[] array2 = new byte[num];
		Array.Copy(array, array2, array2.Length);
		return array2;
	}

	private void method_0(byte[] byte_0, ref int int_0)
	{
		Enum1 @enum = (Enum1)((byte_0[int_0++] << 8) | byte_0[int_0++]);
		int num = (byte_0[int_0++] << 8) | byte_0[int_0++];
		Class25.smethod_2($"STUNMESSAGE: AttributeType={@enum.ToString()}, Length={num} bytes");
		switch (@enum)
		{
		default:
			int_0 += num;
			break;
		case Enum1.const_13:
			string_2 = Encoding.Default.GetString(byte_0, int_0, num);
			int_0 += num;
			Class25.smethod_2($"STUNMESSAGE: ServerName={ServerName}");
			break;
		case Enum1.const_0:
			ipendPoint_0 = method_1(byte_0, ref int_0);
			Class25.smethod_2($"STUNMESSAGE: MappedAddress={ipendPoint_0.ToString()}");
			break;
		case Enum1.const_1:
			ipendPoint_1 = method_1(byte_0, ref int_0);
			Class25.smethod_2($"STUNMESSAGE: ResponseAddress={ipendPoint_1.ToString()}");
			break;
		case Enum1.const_2:
			int_0 += 3;
			stunChangeRequest_0 = new StunChangeRequest((byte_0[int_0] & 4) != 0, (byte_0[int_0] & 2) != 0);
			Class25.smethod_2($"STUNMESSAGE: ChangeRequest=IP:{stunChangeRequest_0.ChangeIP.ToString()},Port:{stunChangeRequest_0.ChangePort.ToString()}");
			int_0++;
			break;
		case Enum1.const_3:
			ipendPoint_2 = method_1(byte_0, ref int_0);
			Class25.smethod_2($"STUNMESSAGE: SourceAddress={ipendPoint_2.ToString()}");
			break;
		case Enum1.const_4:
			ipendPoint_3 = method_1(byte_0, ref int_0);
			Class25.smethod_2($"STUNMESSAGE: ChangedAddress={ipendPoint_3.ToString()}");
			break;
		case Enum1.const_5:
			string_0 = Encoding.Default.GetString(byte_0, int_0, num);
			int_0 += num;
			Class25.smethod_2($"STUNMESSAGE: Username={string_0}");
			break;
		case Enum1.const_6:
			string_1 = Encoding.Default.GetString(byte_0, int_0, num);
			int_0 += num;
			Class25.smethod_2($"STUNMESSAGE: Password={string_1}");
			break;
		case Enum1.const_7:
			int_0 += num;
			Class25.smethod_2("STUNMESSAGE: MessageIntegrity");
			break;
		case Enum1.const_8:
		{
			int code = (byte_0[int_0 + 2] & 7) * 100 + (byte_0[int_0 + 3] & 0xFF);
			stunErrorCode_0 = new StunErrorCode(code, Encoding.Default.GetString(byte_0, int_0 + 4, num - 4));
			int_0 += num;
			Class25.smethod_2($"STUNMESSAGE: ErrorCode={stunErrorCode_0.Code.ToString()}:{stunErrorCode_0.Message}");
			break;
		}
		case Enum1.const_9:
			int_0 += num;
			Class25.smethod_2("STUNMESSAGE: UnknownAttribute");
			break;
		case Enum1.const_10:
			ipendPoint_4 = method_1(byte_0, ref int_0);
			Class25.smethod_2($"STUNMESSAGE: ReflectedFrom={ipendPoint_4.ToString()}");
			break;
		}
	}

	private IPEndPoint method_1(byte[] byte_0, ref int int_0)
	{
		int_0++;
		int_0++;
		int port = (byte_0[int_0++] << 8) | byte_0[int_0++];
		return new IPEndPoint(new IPAddress(new byte[4]
		{
			byte_0[int_0++],
			byte_0[int_0++],
			byte_0[int_0++],
			byte_0[int_0++]
		}), port);
	}

	private void method_2(Enum1 enum1_0, IPEndPoint ipendPoint_5, byte[] byte_0, ref int int_0)
	{
		byte_0[int_0++] = (byte)((int)enum1_0 >> 8);
		byte_0[int_0++] = (byte)(enum1_0 & (Enum1)255);
		byte_0[int_0++] = 0;
		byte_0[int_0++] = 8;
		byte_0[int_0++] = 0;
		byte_0[int_0++] = 1;
		byte_0[int_0++] = (byte)(ipendPoint_5.Port >> 8);
		byte_0[int_0++] = (byte)((uint)ipendPoint_5.Port & 0xFFu);
		byte[] addressBytes = ipendPoint_5.Address.GetAddressBytes();
		byte_0[int_0++] = addressBytes[0];
		byte_0[int_0++] = addressBytes[0];
		byte_0[int_0++] = addressBytes[0];
		byte_0[int_0++] = addressBytes[0];
	}
}
