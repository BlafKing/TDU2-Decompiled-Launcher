using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UPnPDetection.UPnP;

public static class UPnPBase64
{
	public static string StringToBase64(string a_String)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(a_String);
		return Encode(bytes);
	}

	public static string Base64ToString(string a_Base64String)
	{
		byte[] bytes = Decode(a_Base64String);
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		return uTF8Encoding.GetString(bytes);
	}

	public static string Encode(byte[] a_Buffer)
	{
		return Encode(a_Buffer, 0, a_Buffer.Length);
	}

	public static string Encode(byte[] a_Buffer, int a_Offset, int a_Length)
	{
		a_Length += a_Offset;
		ToBase64Transform toBase64Transform = new ToBase64Transform();
		MemoryStream memoryStream = new MemoryStream();
		int num = a_Offset;
		int num2 = 3;
		if (a_Length < 3)
		{
			num2 = a_Length;
		}
		byte[] array;
		do
		{
			array = toBase64Transform.TransformFinalBlock(a_Buffer, num, num2);
			num += num2;
			if (a_Length - num < num2)
			{
				num2 = a_Length - num;
			}
			memoryStream.Write(array, 0, array.Length);
		}
		while (num < a_Length);
		array = memoryStream.ToArray();
		memoryStream.Close();
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		return uTF8Encoding.GetString(array);
	}

	public static byte[] Decode(string a_Text)
	{
		FromBase64Transform fromBase64Transform = new FromBase64Transform();
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(a_Text);
		byte[] array = new byte[bytes.Length * 3];
		int num = fromBase64Transform.TransformBlock(bytes, 0, bytes.Length, array, 0);
		byte[] array2 = new byte[num];
		Array.Copy(array, 0, array2, 0, array2.Length);
		return array2;
	}
}
