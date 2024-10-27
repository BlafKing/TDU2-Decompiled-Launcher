using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UPnPDetection.UPnP;

public static class UPnPTraceLog
{
	private const string string_0 = "edUPnPTraceLog";

	private const string string_1 = "edUPnP";

	private const string string_2 = "log.log";

	private static StreamWriter streamWriter_0;

	private static List<string> list_0;

	public static void Initialize()
	{
	}

	public static void Dispose()
	{
	}

	public static void Log(string a_Message)
	{
		Class25.smethod_2(a_Message);
	}

	public static void Error(string a_Message)
	{
		Class25.smethod_3(a_Message);
	}

	private static void smethod_0(object object_0)
	{
		try
		{
			smethod_1();
		}
		catch (Exception exception_)
		{
			smethod_2("Trace Log Timer", exception_);
		}
	}

	private static void smethod_1()
	{
		try
		{
			StringBuilder stringBuilder = new StringBuilder();
			lock (list_0)
			{
				foreach (string item in list_0)
				{
					stringBuilder.AppendLine(item);
				}
				list_0.Clear();
			}
			if (stringBuilder.Length > 0)
			{
				lock (streamWriter_0)
				{
					streamWriter_0.Write(stringBuilder.ToString());
					streamWriter_0.Flush();
					return;
				}
			}
		}
		catch (Exception exception_)
		{
			smethod_2("_StoreLog", exception_);
		}
	}

	private static void smethod_2(string string_3, Exception exception_0)
	{
		try
		{
			if (!EventLog.SourceExists("edUPnP"))
			{
				EventLog.CreateEventSource("edUPnPTraceLog", "edUPnP");
			}
			EventLog eventLog = new EventLog();
			eventLog.Source = "edUPnP";
			eventLog.WriteEntry($"{string_3}: {exception_0.Message}", EventLogEntryType.Error);
		}
		catch (Exception)
		{
		}
	}
}
