using System;
using System.IO;
using System.Runtime.CompilerServices;
using Ionic.Zip;

internal class Stream0 : Stream, IDisposable
{
	private int int_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	private uint uint_0;

	private uint uint_1;

	private int int_1;

	private Stream stream_0;

	[CompilerGenerated]
	private bool bool_0;

	public override bool CanRead
	{
		get
		{
			if (int_0 == 1)
			{
				return stream_0.CanRead;
			}
			return false;
		}
	}

	public override bool CanSeek => stream_0.CanSeek;

	public override bool CanWrite
	{
		get
		{
			if (int_0 == 2)
			{
				return stream_0.CanWrite;
			}
			return false;
		}
	}

    public override long Length => stream_0.Length;

    public override long Position
	{
		get
		{
			return stream_0.Position;
		}
		set
		{
			stream_0.Position = value;
		}
	}

	private Stream0()
	{
	}

	public static Stream0 smethod_0(string string_4, uint uint_2, uint uint_3)
	{
		Stream0 stream = new Stream0();
		stream.int_0 = 1;
		stream.method_4(uint_2);
		stream.uint_1 = uint_3;
		stream.string_0 = string_4;
		Stream0 stream2 = stream;
		stream2.method_9();
		return stream2;
	}

	public static Stream0 smethod_1(string string_4, int int_2)
	{
		Stream0 stream = new Stream0();
		stream.int_0 = 2;
		stream.method_4(0u);
		stream.string_0 = string_4;
		stream.int_1 = int_2;
		stream.string_1 = Path.GetDirectoryName(string_4);
		Stream0 stream2 = stream;
		if (stream2.string_1 == "")
		{
			stream2.string_1 = ".";
		}
		stream2.method_10(0u);
		return stream2;
	}

	public static Stream0 smethod_2(string string_4, uint uint_2)
	{
		Stream0 stream = new Stream0();
		stream.int_0 = 3;
		stream.method_4(uint_2);
		stream.string_0 = string_4;
		stream.int_1 = int.MaxValue;
		Stream0 stream2 = stream;
		stream2.method_0();
		return stream2;
	}

	private void method_0()
	{
		stream_0 = new FileStream(method_5(), FileMode.Open);
	}

	[SpecialName]
	[CompilerGenerated]
	public bool method_1()
	{
		return bool_0;
	}

	[SpecialName]
	[CompilerGenerated]
	public void method_2(bool bool_1)
	{
		bool_0 = bool_1;
	}

	[SpecialName]
	public uint method_3()
	{
		return uint_0;
	}

	[SpecialName]
	private void method_4(uint uint_2)
	{
		uint_0 = uint_2;
		string_2 = null;
	}

	[SpecialName]
	public string method_5()
	{
		if (string_2 == null)
		{
			string_2 = method_6(method_3());
		}
		return string_2;
	}

	private string method_6(uint uint_2)
	{
		return $"{Path.Combine(Path.GetDirectoryName(string_0), Path.GetFileNameWithoutExtension(string_0))}.z{uint_2 + 1:D2}";
	}

	public uint method_7(int int_2)
	{
		if (stream_0.Position + int_2 > int_1)
		{
			return method_3() + 1;
		}
		return method_3();
	}

	public override string ToString()
	{
		return string.Format("{0}[{1}][{2}], pos=0x{3:X})", "ZipSegmentedStream", method_5(), (int_0 == 1) ? "Read" : ((int_0 == 2) ? "Write" : ((int_0 == 3) ? "Update" : "???")), Position);
	}

	public void method_8()
	{
		method_4(0u);
		method_10(0u);
	}

	private void method_9()
	{
		if (stream_0 != null)
		{
			stream_0.Close();
		}
		if (method_3() + 1 == uint_1)
		{
			string_2 = string_0;
		}
		stream_0 = File.OpenRead(method_5());
	}

    public override int Read(byte[] buffer, int offset, int count)
	{
		if (int_0 != 1)
		{
			throw new ZipException("Stream Error: Cannot Read.");
		}
		int num = stream_0.Read(buffer, offset, count);
		int num2 = num;
		while (true)
		{
			if (num2 != count)
			{
				if (stream_0.Position == stream_0.Length)
				{
					if (method_3() + 1 == uint_1)
					{
						break;
					}
					method_4(method_3() + 1);
					method_9();
					offset += num2;
					count -= num2;
					num2 = stream_0.Read(buffer, offset, count);
					num += num2;
					continue;
				}
				throw new ZipException($"Read error in file {method_5()}");
			}
			return num;
		}
		return num;
	}

	private void method_10(uint uint_2)
	{
		if (stream_0 != null)
		{
			stream_0.Close();
			if (File.Exists(method_5()))
			{
				File.Delete(method_5());
			}
			File.Move(string_3, method_5());
		}
		if (uint_2 != 0)
		{
			method_4(method_3() + uint_2);
		}
		Class7.smethod_17(string_1, out stream_0, out string_3);
		if (method_3() == 0)
		{
			stream_0.Write(BitConverter.GetBytes(134695760), 0, 4);
		}
	}

    public override void Write(byte[] buffer, int offset, int count)
	{
		if (int_0 == 2)
		{
			if (method_1())
			{
				if (stream_0.Position + count > int_1)
				{
					method_10(1u);
				}
			}
			else
			{
				while (stream_0.Position + count > int_1)
				{
					int num = int_1 - (int)stream_0.Position;
					stream_0.Write(buffer, offset, num);
					method_10(1u);
					count -= num;
					offset += num;
				}
			}
			stream_0.Write(buffer, offset, count);
		}
		else
		{
			if (int_0 != 3)
			{
				throw new ZipException("Stream Error: Cannot Write.");
			}
			stream_0.Write(buffer, offset, count);
		}
	}

	public long method_11(uint uint_2, long long_0)
	{
		if (int_0 != 2)
		{
			throw new ZipException("bad state.");
		}
		if (uint_2 == method_3())
		{
			return stream_0.Seek(long_0, SeekOrigin.Begin);
		}
		if (stream_0 != null)
		{
			stream_0.Close();
			if (File.Exists(string_3))
			{
				File.Delete(string_3);
			}
		}
		for (uint num = method_3() - 1; num > uint_2; num--)
		{
			string path = method_6(num);
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
		method_4(uint_2);
		for (int i = 0; i < 3; i++)
		{
			try
			{
				string_3 = Class7.smethod_18();
				File.Move(method_5(), string_3);
			}
			catch (IOException)
			{
				if (i == 2)
				{
					throw;
				}
			}
		}
		stream_0 = new FileStream(string_3, FileMode.Open);
		return stream_0.Seek(long_0, SeekOrigin.Begin);
	}

    public override void Flush()
	{
		stream_0.Flush();
	}

    public override long Seek(long offset, SeekOrigin origin)
	{
		return stream_0.Seek(offset, origin);
	}

    public override void SetLength(long value)
	{
		if (int_0 != 2)
		{
			throw new NotImplementedException();
		}
		stream_0.SetLength(value);
	}

	void IDisposable.Dispose()
	{
		Close();
	}

    public override void Close()
	{
		if (stream_0 != null)
		{
			stream_0.Close();
			stream_0 = null;
			if (int_0 == 2)
			{
				if (File.Exists(method_5()))
				{
					File.Delete(method_5());
				}
				if (File.Exists(string_3))
				{
					File.Move(string_3, method_5());
				}
			}
		}
		base.Close();
	}
}
