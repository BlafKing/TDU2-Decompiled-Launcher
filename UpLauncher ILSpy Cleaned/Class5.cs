using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Ionic.Zip;

internal class Class5
{
	public static bool smethod_0(Stream stream_0, ICollection<ZipEntry> icollection_0, uint uint_0, Zip64Option zip64Option_0, string string_0, Encoding encoding_0)
	{
		Stream0 stream = stream_0 as Stream0;
		stream?.method_2(bool_1: true);
		MemoryStream memoryStream = new MemoryStream();
		foreach (ZipEntry item in icollection_0)
		{
			if (item.IncludedInMostRecentSave)
			{
				item.method_2(memoryStream);
			}
		}
		byte[] array = memoryStream.ToArray();
		stream_0.Write(array, 0, array.Length);
		long num = ((stream_0 is Stream2 stream2) ? stream2.method_4() : stream_0.Position);
		long num2 = num - array.Length;
		uint num3 = stream?.method_3() ?? 0;
		long num4 = num - num2;
		int num5 = smethod_3(icollection_0);
		bool flag = zip64Option_0 == Zip64Option.Always || num5 >= 65535 || num4 > 4294967295L || num2 > 4294967295L;
		byte[] array2 = null;
		if (flag)
		{
			if (zip64Option_0 == Zip64Option.Default)
			{
				StackFrame stackFrame = new StackFrame(1);
				if (stackFrame.GetMethod().DeclaringType == typeof(ZipFile))
				{
					throw new ZipException("The archive requires a ZIP64 Central Directory. Consider setting the ZipFile.UseZip64WhenSaving property.");
				}
				throw new ZipException("The archive requires a ZIP64 Central Directory. Consider setting the ZipOutputStream.EnableZip64 property.");
			}
			array = smethod_2(num2, num, num5, uint_0);
			array2 = smethod_1(num2, num, zip64Option_0, num5, string_0, encoding_0);
			if (num3 != 0)
			{
				uint value = stream.method_7(array.Length + array2.Length);
				Array.Copy(BitConverter.GetBytes(value), 0, array, 16, 4);
				Array.Copy(BitConverter.GetBytes(value), 0, array, 20, 4);
				Array.Copy(BitConverter.GetBytes(value), 0, array, 60, 4);
				Array.Copy(BitConverter.GetBytes(value), 0, array, 72, 4);
			}
			stream_0.Write(array, 0, array.Length);
		}
		else
		{
			array2 = smethod_1(num2, num, zip64Option_0, num5, string_0, encoding_0);
		}
		if (num3 != 0)
		{
			ushort value2 = (ushort)stream.method_7(array2.Length);
			Array.Copy(BitConverter.GetBytes(value2), 0, array2, 4, 2);
			Array.Copy(BitConverter.GetBytes(value2), 0, array2, 6, 2);
		}
		stream_0.Write(array2, 0, array2.Length);
		stream?.method_2(bool_1: false);
		return flag;
	}

	private static byte[] smethod_1(long long_0, long long_1, Zip64Option zip64Option_0, int int_0, string string_0, Encoding encoding_0)
	{
		int num = 0;
		int num2 = 22;
		byte[] array = null;
		short num3 = 0;
		if (string_0 != null && string_0.Length != 0)
		{
			array = encoding_0.GetBytes(string_0);
			num3 = (short)array.Length;
		}
		num2 += num3;
		byte[] array2 = new byte[num2];
		int num4 = 0;
		byte[] bytes = BitConverter.GetBytes(101010256u);
		Array.Copy(bytes, 0, array2, 0, 4);
		num4 = 4;
		num4 = 5;
		array2[4] = 0;
		num4 = 6;
		array2[5] = 0;
		num4 = 7;
		array2[6] = 0;
		num4 = 8;
		array2[7] = 0;
		if (int_0 < 65535 && zip64Option_0 != Zip64Option.Always)
		{
			array2[num4++] = (byte)((uint)int_0 & 0xFFu);
			array2[num4++] = (byte)((int_0 & 0xFF00) >> 8);
			array2[num4++] = (byte)((uint)int_0 & 0xFFu);
			array2[num4++] = (byte)((int_0 & 0xFF00) >> 8);
		}
		else
		{
			for (num = 0; num < 4; num++)
			{
				array2[num4++] = byte.MaxValue;
			}
		}
		long num5 = long_1 - long_0;
		if (num5 < 4294967295L && long_0 < 4294967295L)
		{
			array2[num4++] = (byte)((ulong)num5 & 0xFFuL);
			array2[num4++] = (byte)((num5 & 0xFF00L) >> 8);
			array2[num4++] = (byte)((num5 & 0xFF0000L) >> 16);
			array2[num4++] = (byte)((num5 & 0xFF000000L) >> 24);
			array2[num4++] = (byte)((ulong)long_0 & 0xFFuL);
			array2[num4++] = (byte)((long_0 & 0xFF00L) >> 8);
			array2[num4++] = (byte)((long_0 & 0xFF0000L) >> 16);
			array2[num4++] = (byte)((long_0 & 0xFF000000L) >> 24);
		}
		else
		{
			for (num = 0; num < 8; num++)
			{
				array2[num4++] = byte.MaxValue;
			}
		}
		if (string_0 != null && string_0.Length != 0)
		{
			if (num3 + num4 + 2 > array2.Length)
			{
				num3 = (short)(array2.Length - num4 - 2);
			}
			array2[num4++] = (byte)((uint)num3 & 0xFFu);
			array2[num4++] = (byte)((num3 & 0xFF00) >> 8);
			if (num3 != 0)
			{
				for (num = 0; num < num3 && num4 + num < array2.Length; num++)
				{
					array2[num4 + num] = array[num];
				}
				num4 += num;
			}
		}
		else
		{
			array2[num4++] = 0;
			array2[num4++] = 0;
		}
		return array2;
	}

	private static byte[] smethod_2(long long_0, long long_1, int int_0, uint uint_0)
	{
		byte[] array = new byte[76];
		int num = 0;
		byte[] bytes = BitConverter.GetBytes(101075792u);
		Array.Copy(bytes, 0, array, 0, 4);
		num = 4;
		Array.Copy(BitConverter.GetBytes(44L), 0, array, 4, 8);
		num = 12;
		num = 13;
		array[12] = 45;
		num = 14;
		array[13] = 0;
		num = 15;
		array[14] = 45;
		num = 16;
		array[15] = 0;
		for (int i = 0; i < 8; i++)
		{
			array[num++] = 0;
		}
		long value = int_0;
		Array.Copy(BitConverter.GetBytes(value), 0, array, num, 8);
		num += 8;
		Array.Copy(BitConverter.GetBytes(value), 0, array, num, 8);
		num += 8;
		long value2 = long_1 - long_0;
		Array.Copy(BitConverter.GetBytes(value2), 0, array, num, 8);
		num += 8;
		Array.Copy(BitConverter.GetBytes(long_0), 0, array, num, 8);
		num += 8;
		bytes = BitConverter.GetBytes(117853008u);
		Array.Copy(bytes, 0, array, num, 4);
		num += 4;
		uint value3 = ((uint_0 != 0) ? (uint_0 - 1) : 0u);
		Array.Copy(BitConverter.GetBytes(value3), 0, array, num, 4);
		num += 4;
		Array.Copy(BitConverter.GetBytes(long_1), 0, array, num, 8);
		num += 8;
		Array.Copy(BitConverter.GetBytes(uint_0 - 1), 0, array, num, 4);
		num += 4;
		return array;
	}

	private static int smethod_3(ICollection<ZipEntry> icollection_0)
	{
		int num = 0;
		foreach (ZipEntry item in icollection_0)
		{
			if (item.IncludedInMostRecentSave)
			{
				num++;
			}
		}
		return num;
	}
}
