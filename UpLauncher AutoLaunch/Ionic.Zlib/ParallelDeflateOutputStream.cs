using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Ionic.Zlib;

public class ParallelDeflateOutputStream : Stream
{
	[Flags]
	private enum Enum0
	{
		flag_0 = 0,
		flag_1 = 1,
		flag_2 = 2,
		flag_3 = 4,
		flag_4 = 8,
		flag_5 = 0x10,
		flag_6 = 0x20,
		flag_7 = 0x40,
		flag_8 = 0x80,
		flag_9 = 0x100,
		flag_10 = 0x200,
		flag_11 = 0x400
	}

	private static readonly int int_0 = 65536;

	private List<Class24> list_0;

	private bool leaveOpen;

	private Stream stream;

	private int int_1;

	private int int_2;

	private int int_3 = int_0;

	private ManualResetEvent manualResetEvent_0;

	private ManualResetEvent manualResetEvent_1;

	private bool bool_0;

	private object object_0 = new object();

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private int int_4;

	private int int_5;

	private long long_0;

	private CompressionLevel level;

	private volatile Exception exception_0;

	private object object_1 = new object();

	private Enum0 enum0_0 = Enum0.flag_8 | Enum0.flag_9 | Enum0.flag_10 | Enum0.flag_11;

	[CompilerGenerated]
	private CompressionStrategy compressionStrategy_0;

	[CompilerGenerated]
	private int int_6;

	public CompressionStrategy Strategy
	{
		[CompilerGenerated]
		get
		{
			return compressionStrategy_0;
		}
		[CompilerGenerated]
		private set
		{
			compressionStrategy_0 = value;
		}
	}

	public int BuffersPerCore
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	public int BufferSize
	{
		get
		{
			return int_3;
		}
		set
		{
			if (value < 1024)
			{
				throw new ArgumentException();
			}
			int_3 = value;
		}
	}

	public int Crc32 => int_5;

	public long BytesProcessed => long_0;

	public override bool CanSeek => false;

	public override bool CanRead => false;

	public override bool CanWrite => stream.CanWrite;

	public override long Length
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override long Position
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public ParallelDeflateOutputStream(Stream stream)
		: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen: false)
	{
	}

	public ParallelDeflateOutputStream(Stream stream, CompressionLevel level)
		: this(stream, level, CompressionStrategy.Default, leaveOpen: false)
	{
	}

	public ParallelDeflateOutputStream(Stream stream, bool leaveOpen)
		: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
	{
	}

	public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen)
		: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
	{
	}

	public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
	{
		this.level = level;
		this.leaveOpen = leaveOpen;
		Strategy = strategy;
		BuffersPerCore = 4;
		manualResetEvent_0 = new ManualResetEvent(initialState: false);
		manualResetEvent_1 = new ManualResetEvent(initialState: false);
		this.stream = stream;
	}

	private void method_0()
	{
		list_0 = new List<Class24>();
		for (int i = 0; i < BuffersPerCore * Environment.ProcessorCount; i++)
		{
			list_0.Add(new Class24(int_3, level, Strategy));
		}
		int_4 = list_0.Count;
		for (int j = 0; j < int_4; j++)
		{
			list_0[j].int_2 = j;
		}
		int_2 = 0;
		int_1 = 0;
	}

	private void method_1()
	{
		if (!ThreadPool.QueueUserWorkItem(method_3))
		{
			throw new Exception("Cannot enqueue writer thread.");
		}
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (bool_1)
		{
			throw new NotSupportedException();
		}
		if (exception_0 != null)
		{
			throw exception_0;
		}
		if (count == 0)
		{
			return;
		}
		if (!bool_3)
		{
			method_0();
			method_1();
			manualResetEvent_1.Set();
			bool_3 = true;
		}
		do
		{
			int index = int_1 % int_4;
			Class24 @class = list_0[index];
			lock (@class)
			{
				if (@class.int_0 != 0 && @class.int_0 != 6 && @class.int_0 != 1)
				{
					int num = 0;
					while (@class.int_0 != 0 && @class.int_0 != 6 && @class.int_0 != 1)
					{
						num++;
						Monitor.Pulse(@class);
						Monitor.Wait(@class);
						if (@class.int_0 != 0 && @class.int_0 == 6)
						{
						}
					}
					continue;
				}
				@class.int_0 = 1;
				int num2 = ((@class.byte_0.Length - @class.int_3 > count) ? count : (@class.byte_0.Length - @class.int_3));
				Array.Copy(buffer, offset, @class.byte_0, @class.int_3, num2);
				count -= num2;
				offset += num2;
				@class.int_3 += num2;
				if (@class.int_3 == @class.byte_0.Length)
				{
					@class.int_0 = 2;
					int_1++;
					if (!ThreadPool.QueueUserWorkItem(method_4, @class))
					{
						throw new Exception("Cannot enqueue workitem");
					}
				}
			}
		}
		while (count > 0);
	}

	public override void Flush()
	{
		method_2(bool_4: false);
	}

	private void method_2(bool bool_4)
	{
		if (bool_1)
		{
			throw new NotSupportedException();
		}
		Class24 @class = list_0[int_1 % int_4];
		lock (@class)
		{
			if (@class.int_0 == 1)
			{
				@class.int_0 = 2;
				int_1++;
				if (bool_4)
				{
					bool_0 = true;
				}
				if (!ThreadPool.QueueUserWorkItem(method_4, @class))
				{
					throw new Exception("Cannot enqueue workitem");
				}
			}
			else if (bool_4)
			{
				bool_0 = true;
			}
		}
	}

	public override void Close()
	{
		if (!bool_1)
		{
			method_2(bool_4: true);
			Class24 @class = list_0[int_1 % int_4];
			lock (@class)
			{
				Monitor.PulseAll(@class);
			}
			manualResetEvent_0.WaitOne();
			if (!leaveOpen)
			{
				stream.Close();
			}
			bool_1 = true;
		}
	}

	public new void Dispose()
	{
		bool_2 = true;
		list_0 = null;
		manualResetEvent_1.Set();
		Dispose(disposeManagedResources: true);
	}

	protected override void Dispose(bool disposeManagedResources)
	{
		if (disposeManagedResources)
		{
			manualResetEvent_0.Close();
			manualResetEvent_1.Close();
		}
	}

	public void Reset(Stream stream)
	{
		if (!bool_3)
		{
			return;
		}
		if (bool_0)
		{
			manualResetEvent_0.WaitOne();
			foreach (Class24 item in list_0)
			{
				item.int_0 = 0;
			}
			bool_0 = false;
			int_2 = 0;
			int_1 = 0;
			long_0 = 0L;
			int_5 = 0;
			bool_1 = false;
			manualResetEvent_0.Reset();
		}
		this.stream = stream;
		manualResetEvent_1.Set();
	}

	private void method_3(object object_2)
	{
		try
		{
			while (true)
			{
				manualResetEvent_1.WaitOne();
				if (bool_2)
				{
					break;
				}
				manualResetEvent_1.Reset();
				Class24 @class = null;
				CRC32 cRC = new CRC32();
				do
				{
					@class = list_0[int_2 % int_4];
					lock (@class)
					{
						do
						{
							if (@class.int_0 != 4)
							{
								int num = 0;
								while (@class.int_0 != 4 && (!bool_0 || int_2 != int_1))
								{
									num++;
									Monitor.Pulse(@class);
									Monitor.Wait(@class);
								}
								continue;
							}
							@class.int_0 = 5;
							stream.Write(@class.byte_1, 0, @class.int_4);
							cRC.Combine(@class.int_1, @class.int_3);
							long_0 += @class.int_3;
							int_2++;
							@class.int_3 = 0;
							@class.int_0 = 6;
							Monitor.Pulse(@class);
							break;
						}
						while (!bool_0 || int_2 != int_1);
					}
				}
				while (!bool_0 || int_2 != int_1);
				byte[] array = new byte[128];
				ZlibCodec zlibCodec = new ZlibCodec();
				int num2 = zlibCodec.InitializeDeflate(level, wantRfc1950Header: false);
				zlibCodec.InputBuffer = null;
				zlibCodec.NextIn = 0;
				zlibCodec.AvailableBytesIn = 0;
				zlibCodec.OutputBuffer = array;
				zlibCodec.NextOut = 0;
				zlibCodec.AvailableBytesOut = array.Length;
				num2 = zlibCodec.Deflate(FlushType.Finish);
				if (num2 == 1 || num2 == 0)
				{
					if (array.Length - zlibCodec.AvailableBytesOut > 0)
					{
						stream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
					}
					zlibCodec.EndDeflate();
					int_5 = cRC.Crc32Result;
					manualResetEvent_0.Set();
					continue;
				}
				throw new Exception("deflating: " + zlibCodec.Message);
			}
		}
		catch (Exception ex)
		{
			lock (object_1)
			{
				if (exception_0 != null)
				{
					exception_0 = ex;
				}
			}
		}
	}

	private void method_4(object object_2)
	{
		Class24 @class = (Class24)object_2;
		try
		{
			lock (@class)
			{
				if (@class.int_0 != 2)
				{
					throw new InvalidOperationException();
				}
				CRC32 cRC = new CRC32();
				cRC.SlurpBlock(@class.byte_0, 0, @class.int_3);
				method_5(@class);
				@class.int_0 = 4;
				@class.int_1 = cRC.Crc32Result;
				Monitor.Pulse(@class);
			}
		}
		catch (Exception ex)
		{
			lock (object_1)
			{
				if (exception_0 != null)
				{
					exception_0 = ex;
				}
			}
		}
	}

	private bool method_5(Class24 class24_0)
	{
		ZlibCodec zlibCodec_ = class24_0.zlibCodec_0;
		zlibCodec_.ResetDeflate();
		zlibCodec_.NextIn = 0;
		zlibCodec_.AvailableBytesIn = class24_0.int_3;
		zlibCodec_.NextOut = 0;
		zlibCodec_.AvailableBytesOut = class24_0.byte_1.Length;
		do
		{
			zlibCodec_.Deflate(FlushType.None);
		}
		while (zlibCodec_.AvailableBytesIn > 0 || zlibCodec_.AvailableBytesOut == 0);
		zlibCodec_.Deflate(FlushType.Sync);
		class24_0.int_4 = (int)zlibCodec_.TotalBytesOut;
		return true;
	}

	[Conditional("Trace")]
	private void method_6(Enum0 enum0_1, string string_0, params object[] object_2)
	{
		if ((enum0_1 & enum0_0) != 0)
		{
			lock (object_0)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 8);
				Console.Write("{0:000} PDOS ", hashCode);
				Console.WriteLine(string_0, object_2);
				Console.ResetColor();
			}
		}
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotImplementedException();
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}
}
