using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Ionic.Zlib;
using Microsoft.CSharp;

namespace Ionic.Zip;

[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("ebc25cf6-9120-4283-b972-0e5520d00005")]
public class ZipFile : IEnumerable<ZipEntry>, IDisposable
{
	private class Class0
	{
		public SelfExtractorFlavor selfExtractorFlavor_0;

		public List<string> list_0;

		public List<string> list_1;

		public List<string> list_2;
	}

	[CompilerGenerated]
	private sealed class Class1
	{
		public StringComparison stringComparison_0;

		public int method_0(ZipEntry zipEntry_0, ZipEntry zipEntry_1)
		{
			return string.Compare(zipEntry_0.FileName, zipEntry_1.FileName, stringComparison_0);
		}
	}

	[CompilerGenerated]
	private sealed class Class2 : IEnumerator<ZipEntry>
	{
		private ZipEntry zipEntry_0;

		private int int_0;

		public ZipFile zipFile_0;

		public ZipEntry zipEntry_1;

		public Dictionary<string, ZipEntry>.ValueCollection.Enumerator enumerator_0;

		ZipEntry IEnumerator<ZipEntry>.Current
		{
			[DebuggerHidden]
			get
			{
				return zipEntry_0;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return zipEntry_0;
			}
		}

		bool IEnumerator.MoveNext()
		{
			try
			{
				switch (int_0)
				{
				case 0:
					int_0 = -1;
					enumerator_0 = zipFile_0.dictionary_0.Values.GetEnumerator();
					int_0 = 1;
					goto IL_004d;
				case 2:
					int_0 = 1;
					goto IL_004d;
				default:
					{
						return false;
					}
					IL_004d:
					if (!enumerator_0.MoveNext())
					{
						method_0();
						goto default;
					}
					zipEntry_1 = enumerator_0.Current;
					zipEntry_0 = zipEntry_1;
					int_0 = 2;
					return true;
				}
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		void IDisposable.Dispose()
		{
			switch (int_0)
			{
			case 1:
			case 2:
				try
				{
					break;
				}
				finally
				{
					method_0();
				}
			}
		}

		[DebuggerHidden]
		public Class2(int int_1)
		{
			int_0 = int_1;
		}

		private void method_0()
		{
			int_0 = -1;
			((IDisposable)enumerator_0).Dispose();
		}
	}

	private EventHandler<SaveProgressEventArgs> eventHandler_0;

	private EventHandler<ReadProgressEventArgs> eventHandler_1;

	private long long_0 = -99L;

	private EventHandler<ExtractProgressEventArgs> eventHandler_2;

	private EventHandler<AddProgressEventArgs> eventHandler_3;

	private EventHandler<ZipErrorEventArgs> eventHandler_4;

	public static readonly Encoding DefaultEncoding = Encoding.GetEncoding("IBM437");

	private TextWriter textWriter_0;

	private bool bool_0;

	private Stream stream_0;

	private Stream stream_1;

	private ushort ushort_0;

	private ushort ushort_1;

	private uint uint_0;

	private int int_0;

	private uint uint_1;

	private ZipErrorAction zipErrorAction_0;

	private bool bool_1;

	private Dictionary<string, ZipEntry> dictionary_0;

	private List<ZipEntry> list_0;

	private string string_0;

	private string string_1;

	internal string string_2;

	private bool bool_2 = true;

	private bool bool_3;

	private CompressionStrategy compressionStrategy_0;

	private bool bool_4;

	private string string_3;

	private bool bool_5;

	private bool bool_6;

	private string string_4;

	private bool bool_7 = true;

	private object object_0 = new object();

	private bool bool_8;

	private bool bool_9;

	private EncryptionAlgorithm encryptionAlgorithm_0;

	private bool bool_10;

	private long long_1 = -1L;

	private bool? nullable_0;

	internal bool bool_11;

	private Encoding encoding_0 = Encoding.GetEncoding("IBM437");

	private int int_1 = IoBufferSizeDefault;

	internal ParallelDeflateOutputStream parallelDeflateOutputStream_0;

	private long long_2;

	internal Zip64Option zip64Option_0;

	private bool bool_12;

	public static readonly int IoBufferSizeDefault = 32768;

	private static Class0[] class0_0 = new Class0[2]
	{
		new Class0
		{
			selfExtractorFlavor_0 = SelfExtractorFlavor.WinFormsApplication,
			list_0 = new List<string> { "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll" },
			list_1 = new List<string> { "Ionic.Zip.WinFormsSelfExtractorStub.resources", "Ionic.Zip.Forms.PasswordDialog.resources", "Ionic.Zip.Forms.ZipContentsDialog.resources" },
			list_2 = new List<string> { "WinFormsSelfExtractorStub.cs", "WinFormsSelfExtractorStub.Designer.cs", "PasswordDialog.cs", "PasswordDialog.Designer.cs", "ZipContentsDialog.cs", "ZipContentsDialog.Designer.cs", "FolderBrowserDialogEx.cs" }
		},
		new Class0
		{
			selfExtractorFlavor_0 = SelfExtractorFlavor.ConsoleApplication,
			list_0 = new List<string> { "System.dll" },
			list_1 = null,
			list_2 = new List<string> { "CommandLineSelfExtractorStub.cs" }
		}
	};

	[CompilerGenerated]
	private bool bool_13;

	[CompilerGenerated]
	private bool bool_14;

	[CompilerGenerated]
	private bool bool_15;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private bool bool_16;

	[CompilerGenerated]
	private CompressionLevel compressionLevel_0;

	[CompilerGenerated]
	private ExtractExistingFileAction extractExistingFileAction_0;

	[CompilerGenerated]
	private SetCompressionCallback setCompressionCallback_0;

	public string Info
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"ZipFile: {Name}\n");
			if (!string.IsNullOrEmpty(string_1))
			{
				stringBuilder.Append($"  Comment: {string_1}\n");
			}
			if (ushort_0 != 0)
			{
				stringBuilder.Append($"  version made by: 0x{ushort_0:X4}\n");
			}
			if (ushort_1 != 0)
			{
				stringBuilder.Append($"  version needed to extract: 0x{ushort_1:X4}\n");
			}
			stringBuilder.Append($"  uses ZIP64: {InputUsesZip64}\n");
			stringBuilder.Append($"  disk number with CD: {uint_0}\n");
			foreach (ZipEntry value in dictionary_0.Values)
			{
				stringBuilder.Append(value.Info);
			}
			return stringBuilder.ToString();
		}
	}

	private string ArchiveNameForEvent
	{
		get
		{
			if (string_0 == null)
			{
				return "(stream)";
			}
			return string_0;
		}
	}

	private long LengthOfReadStream
	{
		get
		{
			if (long_0 == -99L)
			{
				long_0 = (bool_7 ? Class7.smethod_0(string_0) : (-1L));
			}
			return long_0;
		}
	}

	public bool FullScan
	{
		[CompilerGenerated]
		get
		{
			return bool_13;
		}
		[CompilerGenerated]
		set
		{
			bool_13 = value;
		}
	}

	public bool SortEntriesBeforeSaving
	{
		[CompilerGenerated]
		get
		{
			return bool_14;
		}
		[CompilerGenerated]
		set
		{
			bool_14 = value;
		}
	}

	public bool AddDirectoryWillTraverseReparsePoints
	{
		[CompilerGenerated]
		get
		{
			return bool_15;
		}
		[CompilerGenerated]
		set
		{
			bool_15 = value;
		}
	}

	public int BufferSize
	{
		get
		{
			return int_1;
		}
		set
		{
			int_1 = value;
		}
	}

	public int CodecBufferSize
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public bool FlattenFoldersOnExtract
	{
		[CompilerGenerated]
		get
		{
			return bool_16;
		}
		[CompilerGenerated]
		set
		{
			bool_16 = value;
		}
	}

	public CompressionStrategy Strategy
	{
		get
		{
			return compressionStrategy_0;
		}
		set
		{
			compressionStrategy_0 = value;
		}
	}

	public string Name
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

	public CompressionLevel CompressionLevel
	{
		[CompilerGenerated]
		get
		{
			return compressionLevel_0;
		}
		[CompilerGenerated]
		set
		{
			compressionLevel_0 = value;
		}
	}

	public string Comment
	{
		get
		{
			return string_1;
		}
		set
		{
			string_1 = value;
			bool_5 = true;
		}
	}

	public bool EmitTimesInWindowsFormatWhenSaving
	{
		get
		{
			return bool_2;
		}
		set
		{
			bool_2 = value;
		}
	}

	public bool EmitTimesInUnixFormatWhenSaving
	{
		get
		{
			return bool_3;
		}
		set
		{
			bool_3 = value;
		}
	}

	internal bool Verbose => textWriter_0 != null;

	public bool CaseSensitiveRetrieval
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (value != bool_0)
			{
				bool_0 = value;
				method_25();
			}
		}
	}

	public bool UseUnicodeAsNecessary
	{
		get
		{
			return encoding_0 == Encoding.GetEncoding("UTF-8");
		}
		set
		{
			encoding_0 = (value ? Encoding.GetEncoding("UTF-8") : DefaultEncoding);
		}
	}

	public Zip64Option UseZip64WhenSaving
	{
		get
		{
			return zip64Option_0;
		}
		set
		{
			zip64Option_0 = value;
		}
	}

	public bool? RequiresZip64
	{
		get
		{
			if (dictionary_0.Count > 65534)
			{
				return true;
			}
			if (bool_6 && !bool_5)
			{
				foreach (ZipEntry value in dictionary_0.Values)
				{
					if (value.RequiresZip64.Value)
					{
						return true;
					}
				}
				return false;
			}
			return null;
		}
	}

	public bool? OutputUsedZip64 => nullable_0;

	public bool? InputUsesZip64
	{
		get
		{
			if (dictionary_0.Count > 65534)
			{
				return true;
			}
			using (IEnumerator<ZipEntry> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ZipEntry current = enumerator.Current;
					if (current.Source == ZipEntrySource.ZipFile)
					{
						if (current.bool_10)
						{
							return true;
						}
						continue;
					}
					return null;
				}
			}
			return false;
		}
	}

	public Encoding ProvisionalAlternateEncoding
	{
		get
		{
			return encoding_0;
		}
		set
		{
			encoding_0 = value;
		}
	}

	public TextWriter StatusMessageTextWriter
	{
		get
		{
			return textWriter_0;
		}
		set
		{
			textWriter_0 = value;
		}
	}

	public string TempFileFolder
	{
		get
		{
			return string_4;
		}
		set
		{
			string_4 = value;
			if (value == null || Directory.Exists(value))
			{
				return;
			}
			throw new FileNotFoundException($"That directory ({value}) does not exist.");
		}
	}

	public string Password
	{
		set
		{
			string_2 = value;
			if (string_2 == null)
			{
				Encryption = EncryptionAlgorithm.None;
			}
			else if (Encryption == EncryptionAlgorithm.None)
			{
				Encryption = EncryptionAlgorithm.PkzipWeak;
			}
		}
	}

	public ExtractExistingFileAction ExtractExistingFile
	{
		[CompilerGenerated]
		get
		{
			return extractExistingFileAction_0;
		}
		[CompilerGenerated]
		set
		{
			extractExistingFileAction_0 = value;
		}
	}

	public ZipErrorAction ZipErrorAction
	{
		get
		{
			if (eventHandler_4 != null)
			{
				zipErrorAction_0 = ZipErrorAction.InvokeErrorEvent;
			}
			return zipErrorAction_0;
		}
		set
		{
			zipErrorAction_0 = value;
			if (zipErrorAction_0 != ZipErrorAction.InvokeErrorEvent && eventHandler_4 != null)
			{
				eventHandler_4 = null;
			}
		}
	}

	public EncryptionAlgorithm Encryption
	{
		get
		{
			return encryptionAlgorithm_0;
		}
		set
		{
			if (value == EncryptionAlgorithm.Unsupported)
			{
				throw new InvalidOperationException("You may not set Encryption to that value.");
			}
			encryptionAlgorithm_0 = value;
		}
	}

	public SetCompressionCallback SetCompression
	{
		[CompilerGenerated]
		get
		{
			return setCompressionCallback_0;
		}
		[CompilerGenerated]
		set
		{
			setCompressionCallback_0 = value;
		}
	}

	public int MaxOutputSegmentSize
	{
		get
		{
			return int_0;
		}
		set
		{
			if (value < 65536 && value != 0)
			{
				throw new ZipException("The minimum acceptable segment size is 65536.");
			}
			int_0 = value;
		}
	}

	public int NumberOfSegmentsForMostRecentSave => (int)(uint_1 + 1);

	public long ParallelDeflateThreshold
	{
		get
		{
			return long_2;
		}
		set
		{
			if (value != 0L && value != -1L && value < 65536L)
			{
				throw new ArgumentException("ParallelDeflateThreshold should be -1, 0, or > 65536");
			}
			long_2 = value;
		}
	}

	public static Version LibraryVersion => Assembly.GetExecutingAssembly().GetName().Version;

	private List<ZipEntry> ZipEntriesAsList
	{
		get
		{
			if (list_0 == null)
			{
				list_0 = new List<ZipEntry>(dictionary_0.Values);
			}
			return list_0;
		}
	}

	public ZipEntry this[int int_3] => ZipEntriesAsList[int_3];

	public ZipEntry this[string fileName]
	{
		get
		{
			string key = Class7.smethod_3(fileName);
			if (dictionary_0.ContainsKey(key))
			{
				return dictionary_0[key];
			}
			return null;
		}
	}

	public ICollection<string> EntryFileNames => dictionary_0.Keys;

	public ICollection<ZipEntry> Entries => dictionary_0.Values;

	public ICollection<ZipEntry> EntriesSorted
	{
		get
		{
			List<ZipEntry> list = new List<ZipEntry>();
			foreach (ZipEntry entry in Entries)
			{
				list.Add(entry);
			}
			StringComparison stringComparison_0 = (CaseSensitiveRetrieval ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
			list.Sort((ZipEntry zipEntry_0, ZipEntry zipEntry_1) => string.Compare(zipEntry_0.FileName, zipEntry_1.FileName, stringComparison_0));
			return list.AsReadOnly();
		}
	}

	public int Count => dictionary_0.Count;

	internal Stream ReadStream
	{
		get
		{
			if (stream_0 == null && string_0 != null)
			{
				stream_0 = File.OpenRead(string_0);
				bool_7 = true;
			}
			return stream_0;
		}
	}

	private Stream WriteStream
	{
		get
		{
			if (stream_1 != null)
			{
				return stream_1;
			}
			if (string_0 == null)
			{
				return stream_1;
			}
			if (int_0 != 0)
			{
				stream_1 = Stream0.smethod_1(string_0, int_0);
				return stream_1;
			}
			Class7.smethod_17(TempFileFolder ?? Path.GetDirectoryName(string_0), out stream_1, out string_3);
			return stream_1;
		}
		set
		{
			if (value != null)
			{
				throw new ZipException("Cannot set the stream to a non-null value.");
			}
			stream_1 = null;
		}
	}

	public event EventHandler<SaveProgressEventArgs> SaveProgress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_0 = (EventHandler<SaveProgressEventArgs>)Delegate.Combine(eventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_0 = (EventHandler<SaveProgressEventArgs>)Delegate.Remove(eventHandler_0, value);
		}
	}

	public event EventHandler<ReadProgressEventArgs> ReadProgress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_1 = (EventHandler<ReadProgressEventArgs>)Delegate.Combine(eventHandler_1, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_1 = (EventHandler<ReadProgressEventArgs>)Delegate.Remove(eventHandler_1, value);
		}
	}

	public event EventHandler<ExtractProgressEventArgs> ExtractProgress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_2 = (EventHandler<ExtractProgressEventArgs>)Delegate.Combine(eventHandler_2, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_2 = (EventHandler<ExtractProgressEventArgs>)Delegate.Remove(eventHandler_2, value);
		}
	}

	public event EventHandler<AddProgressEventArgs> AddProgress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_3 = (EventHandler<AddProgressEventArgs>)Delegate.Combine(eventHandler_3, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_3 = (EventHandler<AddProgressEventArgs>)Delegate.Remove(eventHandler_3, value);
		}
	}

	public event EventHandler<ZipErrorEventArgs> ZipError
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			eventHandler_4 = (EventHandler<ZipErrorEventArgs>)Delegate.Combine(eventHandler_4, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			eventHandler_4 = (EventHandler<ZipErrorEventArgs>)Delegate.Remove(eventHandler_4, value);
		}
	}

	public void Save()
	{
		try
		{
			bool flag = false;
			bool_8 = false;
			uint_1 = 0u;
			method_6();
			if (WriteStream == null)
			{
				throw new BadStateException("You haven't specified where to save the zip.");
			}
			if (string_0 != null && string_0.EndsWith(".exe") && !bool_12)
			{
				throw new BadStateException("You specified an EXE for a plain zip file.");
			}
			if (!bool_5)
			{
				method_7();
				if (Verbose)
				{
					StatusMessageTextWriter.WriteLine("No save is necessary....");
				}
				return;
			}
			method_24();
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("saving....");
			}
			if (dictionary_0.Count >= 65535 && zip64Option_0 == Zip64Option.Default)
			{
				throw new ZipException("The number of entries is 65535 or greater. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			int num = 0;
			ICollection<ZipEntry> collection = (SortEntriesBeforeSaving ? EntriesSorted : Entries);
			foreach (ZipEntry item in collection)
			{
				method_4(num, item, bool_17: true);
				item.method_23(WriteStream);
				if (!bool_8)
				{
					num++;
					method_4(num, item, bool_17: false);
					if (!bool_8)
					{
						if (item.IncludedInMostRecentSave)
						{
							flag |= item.OutputUsedZip64.Value;
						}
						continue;
					}
					break;
				}
				break;
			}
			if (bool_8)
			{
				return;
			}
			Stream0 stream = WriteStream as Stream0;
			uint_1 = stream?.method_3() ?? 1;
			bool flag2 = Class5.smethod_0(WriteStream, collection, uint_1, zip64Option_0, Comment, ProvisionalAlternateEncoding);
			method_5(ZipProgressEventType.Saving_AfterSaveTempArchive);
			bool_6 = true;
			bool_5 = false;
			flag = flag || flag2;
			nullable_0 = flag;
			if (string_0 != null && (string_3 != null || stream != null))
			{
				WriteStream.Close();
				WriteStream.Dispose();
				if (bool_8)
				{
					return;
				}
				if (bool_4 && stream_0 != null)
				{
					stream_0.Close();
					stream_0 = null;
					foreach (ZipEntry item2 in collection)
					{
						item2.stream_0 = null;
					}
				}
				if (bool_4)
				{
					File.Delete(string_0);
				}
				method_5(ZipProgressEventType.Saving_BeforeRenameTempArchive);
				File.Move((stream != null) ? stream.method_5() : string_3, string_0);
				method_5(ZipProgressEventType.Saving_AfterRenameTempArchive);
				bool_4 = true;
			}
			method_0(collection);
			method_7();
			bool_10 = true;
		}
		finally
		{
			method_2();
		}
	}

	private void method_0(ICollection<ZipEntry> icollection_0)
	{
		foreach (ZipEntry item in icollection_0)
		{
			item.method_25();
		}
	}

	private void method_1()
	{
		try
		{
			if (File.Exists(string_3))
			{
				File.Delete(string_3);
			}
		}
		catch (Exception ex)
		{
			if (Verbose)
			{
				StatusMessageTextWriter.WriteLine("ZipFile::Save: could not delete temp file: {0}.", ex.Message);
			}
		}
	}

	private void method_2()
	{
		if (string_0 == null)
		{
			return;
		}
		if (stream_1 != null)
		{
			try
			{
				stream_1.Dispose();
			}
			catch
			{
			}
		}
		stream_1 = null;
		if (string_3 != null)
		{
			method_1();
			string_3 = null;
		}
	}

	public void Save(string fileName)
	{
		if (string_0 == null)
		{
			stream_1 = null;
		}
		string_0 = fileName;
		if (Directory.Exists(string_0))
		{
			throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "fileName"));
		}
		bool_5 = true;
		bool_4 = File.Exists(string_0);
		Save();
	}

	public void Save(Stream outputStream)
	{
		if (!outputStream.CanWrite)
		{
			throw new ArgumentException("The outputStream must be a writable stream.");
		}
		string_0 = null;
		stream_1 = new Stream2(outputStream);
		bool_5 = true;
		bool_4 = false;
		Save();
	}

	public static bool CheckZip(string zipFileName)
	{
		ReadOnlyCollection<string> messages;
		return CheckZip(zipFileName, fixIfNecessary: false, out messages);
	}

	public static bool CheckZip(string zipFileName, bool fixIfNecessary, out ReadOnlyCollection<string> messages)
	{
		List<string> list = new List<string>();
		ZipFile zipFile = null;
		ZipFile zipFile2 = null;
		bool flag = true;
		try
		{
			zipFile = new ZipFile();
			zipFile.FullScan = true;
			zipFile.Initialize(zipFileName);
			zipFile2 = Read(zipFileName);
			foreach (ZipEntry item in zipFile)
			{
				foreach (ZipEntry item2 in zipFile2)
				{
					if (item.FileName == item2.FileName)
					{
						if (item.long_4 != item2.long_4)
						{
							flag = false;
							list.Add($"{item.FileName}: mismatch in RelativeOffsetOfLocalHeader  (0x{item.long_4:X16} != 0x{item2.long_4:X16})");
						}
						if (item.long_0 != item2.long_0)
						{
							flag = false;
							list.Add($"{item.FileName}: mismatch in CompressedSize  (0x{item.long_0:X16} != 0x{item2.long_0:X16})");
						}
						if (item.long_2 != item2.long_2)
						{
							flag = false;
							list.Add($"{item.FileName}: mismatch in UncompressedSize  (0x{item.long_2:X16} != 0x{item2.long_2:X16})");
						}
						if (item.CompressionMethod != item2.CompressionMethod)
						{
							flag = false;
							list.Add($"{item.FileName}: mismatch in CompressionMethod  (0x{item.CompressionMethod:X4} != 0x{item2.CompressionMethod:X4})");
						}
						if (item.Crc != item2.Crc)
						{
							flag = false;
							list.Add($"{item.FileName}: mismatch in Crc32  (0x{item.Crc:X4} != 0x{item2.Crc:X4})");
						}
						break;
					}
				}
			}
			zipFile2.Dispose();
			zipFile2 = null;
			if (!flag && fixIfNecessary)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipFileName);
				fileNameWithoutExtension = $"{fileNameWithoutExtension}_fixed.zip";
				zipFile.Save(fileNameWithoutExtension);
			}
		}
		finally
		{
			zipFile?.Dispose();
			zipFile2?.Dispose();
		}
		messages = list.AsReadOnly();
		return flag;
	}

	public static void FixZipDirectory(string zipFileName)
	{
		using ZipFile zipFile = new ZipFile();
		zipFile.FullScan = true;
		zipFile.Initialize(zipFileName);
		zipFile.Save(zipFileName);
	}

	internal bool method_3(ZipEntry zipEntry_0, long long_3, long long_4)
	{
		if (eventHandler_0 != null)
		{
			lock (object_0)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.smethod_0(ArchiveNameForEvent, zipEntry_0, long_3, long_4);
				eventHandler_0(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					bool_8 = true;
				}
			}
		}
		return bool_8;
	}

	private void method_4(int int_3, ZipEntry zipEntry_0, bool bool_17)
	{
		if (eventHandler_0 == null)
		{
			return;
		}
		lock (object_0)
		{
			SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(ArchiveNameForEvent, bool_17, dictionary_0.Count, int_3, zipEntry_0);
			eventHandler_0(this, saveProgressEventArgs);
			if (saveProgressEventArgs.Cancel)
			{
				bool_8 = true;
			}
		}
	}

	private void method_5(ZipProgressEventType zipProgressEventType_0)
	{
		if (eventHandler_0 == null)
		{
			return;
		}
		lock (object_0)
		{
			SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(ArchiveNameForEvent, zipProgressEventType_0);
			eventHandler_0(this, saveProgressEventArgs);
			if (saveProgressEventArgs.Cancel)
			{
				bool_8 = true;
			}
		}
	}

	private void method_6()
	{
		if (eventHandler_0 != null)
		{
			lock (object_0)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.smethod_1(ArchiveNameForEvent);
				eventHandler_0(this, e);
			}
		}
	}

	private void method_7()
	{
		if (eventHandler_0 != null)
		{
			lock (object_0)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.smethod_2(ArchiveNameForEvent);
				eventHandler_0(this, e);
			}
		}
	}

	private void method_8()
	{
		if (eventHandler_1 != null)
		{
			lock (object_0)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.smethod_2(ArchiveNameForEvent);
				eventHandler_1(this, e);
			}
		}
	}

	private void method_9()
	{
		if (eventHandler_1 != null)
		{
			lock (object_0)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.smethod_4(ArchiveNameForEvent);
				eventHandler_1(this, e);
			}
		}
	}

	internal void method_10(ZipEntry zipEntry_0)
	{
		if (eventHandler_1 != null)
		{
			lock (object_0)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.smethod_3(ArchiveNameForEvent, zipEntry_0, ReadStream.Position, LengthOfReadStream);
				eventHandler_1(this, e);
			}
		}
	}

	internal void method_11(bool bool_17, ZipEntry zipEntry_0)
	{
		if (eventHandler_1 != null)
		{
			lock (object_0)
			{
				ReadProgressEventArgs e = (bool_17 ? ReadProgressEventArgs.smethod_0(ArchiveNameForEvent, dictionary_0.Count) : ReadProgressEventArgs.smethod_1(ArchiveNameForEvent, zipEntry_0, dictionary_0.Count));
				eventHandler_1(this, e);
			}
		}
	}

	private void method_12(int int_3, bool bool_17, ZipEntry zipEntry_0, string string_5)
	{
		if (eventHandler_2 == null)
		{
			return;
		}
		lock (object_0)
		{
			ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(ArchiveNameForEvent, bool_17, dictionary_0.Count, int_3, zipEntry_0, string_5);
			eventHandler_2(this, extractProgressEventArgs);
			if (extractProgressEventArgs.Cancel)
			{
				bool_9 = true;
			}
		}
	}

	internal bool method_13(ZipEntry zipEntry_0, long long_3, long long_4)
	{
		if (eventHandler_2 != null)
		{
			lock (object_0)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.smethod_5(ArchiveNameForEvent, zipEntry_0, long_3, long_4);
				eventHandler_2(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					bool_9 = true;
				}
			}
		}
		return bool_9;
	}

	internal bool method_14(ZipEntry zipEntry_0, string string_5, bool bool_17)
	{
		if (eventHandler_2 != null)
		{
			lock (object_0)
			{
				ExtractProgressEventArgs extractProgressEventArgs = (bool_17 ? ExtractProgressEventArgs.smethod_0(ArchiveNameForEvent, zipEntry_0, string_5) : ExtractProgressEventArgs.smethod_2(ArchiveNameForEvent, zipEntry_0, string_5));
				eventHandler_2(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					bool_9 = true;
				}
			}
		}
		return bool_9;
	}

	internal bool method_15(ZipEntry zipEntry_0, string string_5)
	{
		if (eventHandler_2 != null)
		{
			lock (object_0)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.smethod_1(ArchiveNameForEvent, zipEntry_0, string_5);
				eventHandler_2(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					bool_9 = true;
				}
			}
		}
		return bool_9;
	}

	private void method_16(string string_5)
	{
		if (eventHandler_2 != null)
		{
			lock (object_0)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.smethod_4(ArchiveNameForEvent, string_5);
				eventHandler_2(this, e);
			}
		}
	}

	private void method_17(string string_5)
	{
		if (eventHandler_2 != null)
		{
			lock (object_0)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.smethod_3(ArchiveNameForEvent, string_5);
				eventHandler_2(this, e);
			}
		}
	}

	private void method_18()
	{
		if (eventHandler_3 != null)
		{
			lock (object_0)
			{
				AddProgressEventArgs e = AddProgressEventArgs.smethod_1(ArchiveNameForEvent);
				eventHandler_3(this, e);
			}
		}
	}

	private void method_19()
	{
		if (eventHandler_3 != null)
		{
			lock (object_0)
			{
				AddProgressEventArgs e = AddProgressEventArgs.smethod_2(ArchiveNameForEvent);
				eventHandler_3(this, e);
			}
		}
	}

	internal void method_20(ZipEntry zipEntry_0)
	{
		if (eventHandler_3 != null)
		{
			lock (object_0)
			{
				AddProgressEventArgs e = AddProgressEventArgs.smethod_0(ArchiveNameForEvent, zipEntry_0, dictionary_0.Count);
				eventHandler_3(this, e);
			}
		}
	}

	internal bool method_21(ZipEntry zipEntry_0, Exception exception_0)
	{
		if (eventHandler_4 != null)
		{
			lock (object_0)
			{
				ZipErrorEventArgs zipErrorEventArgs = ZipErrorEventArgs.smethod_0(Name, zipEntry_0, exception_0);
				eventHandler_4(this, zipErrorEventArgs);
				if (zipErrorEventArgs.Cancel)
				{
					bool_8 = true;
				}
			}
		}
		return bool_8;
	}

	public bool ContainsEntry(string name)
	{
		return dictionary_0.ContainsKey(name);
	}

	public override string ToString()
	{
		return $"ZipFile::{Name}";
	}

	internal void method_22()
	{
		bool_5 = true;
	}

	internal Stream method_23(uint uint_2)
	{
		if (uint_2 + 1 != uint_0 && (uint_2 != 0 || uint_0 != 0))
		{
			return Stream0.smethod_0(string_0, uint_2, uint_0);
		}
		return ReadStream;
	}

	internal void method_24()
	{
		if (!bool_10)
		{
			return;
		}
		ZipFile zipFile = new ZipFile();
		zipFile.string_0 = string_0;
		zipFile.ProvisionalAlternateEncoding = ProvisionalAlternateEncoding;
		smethod_0(zipFile);
		foreach (ZipEntry item in zipFile)
		{
			using IEnumerator<ZipEntry> enumerator2 = GetEnumerator();
			while (enumerator2.MoveNext())
			{
				ZipEntry current2 = enumerator2.Current;
				if (item.FileName == current2.FileName)
				{
					current2.method_12(item);
					break;
				}
			}
		}
		zipFile.Dispose();
		bool_10 = false;
	}

	public ZipFile(string fileName)
	{
		try
		{
			method_26(fileName, null);
		}
		catch (Exception innerException)
		{
			throw new ZipException($"{fileName} is not a valid zip file", innerException);
		}
	}

	public ZipFile(string fileName, Encoding encoding)
	{
		try
		{
			method_26(fileName, null);
			ProvisionalAlternateEncoding = encoding;
		}
		catch (Exception innerException)
		{
			throw new ZipException($"{fileName} is not a valid zip file", innerException);
		}
	}

	public ZipFile()
	{
		method_26(null, null);
	}

	public ZipFile(Encoding encoding)
	{
		method_26(null, null);
		ProvisionalAlternateEncoding = encoding;
	}

	public ZipFile(string fileName, TextWriter statusMessageWriter)
	{
		try
		{
			method_26(fileName, statusMessageWriter);
		}
		catch (Exception innerException)
		{
			throw new ZipException($"{fileName} is not a valid zip file", innerException);
		}
	}

	public ZipFile(string fileName, TextWriter statusMessageWriter, Encoding encoding)
	{
		try
		{
			method_26(fileName, statusMessageWriter);
			ProvisionalAlternateEncoding = encoding;
		}
		catch (Exception innerException)
		{
			throw new ZipException($"{fileName} is not a valid zip file", innerException);
		}
	}

	public void Initialize(string fileName)
	{
		try
		{
			method_26(fileName, null);
		}
		catch (Exception innerException)
		{
			throw new ZipException($"{fileName} is not a valid zip file", innerException);
		}
	}

	private void method_25()
	{
		StringComparer comparer = (CaseSensitiveRetrieval ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
		dictionary_0 = ((dictionary_0 == null) ? new Dictionary<string, ZipEntry>(comparer) : new Dictionary<string, ZipEntry>(dictionary_0, comparer));
	}

	private void method_26(string string_5, TextWriter textWriter_1)
	{
		string_0 = string_5;
		textWriter_0 = textWriter_1;
		bool_5 = true;
		AddDirectoryWillTraverseReparsePoints = true;
		CompressionLevel = CompressionLevel.Default;
		ParallelDeflateThreshold = 524288L;
		method_25();
		if (File.Exists(string_0))
		{
			if (FullScan)
			{
				smethod_4(this);
			}
			else
			{
				smethod_0(this);
			}
			bool_4 = true;
		}
	}

	public void RemoveEntry(ZipEntry entry)
	{
		dictionary_0.Remove(Class7.smethod_3(entry.FileName));
		list_0 = null;
		bool_5 = true;
	}

	public void RemoveEntry(string fileName)
	{
		string fileName2 = ZipEntry.smethod_0(fileName, null);
		ZipEntry zipEntry = this[fileName2];
		if (zipEntry == null)
		{
			throw new ArgumentException("The entry you specified was not found in the zip archive.");
		}
		RemoveEntry(zipEntry);
	}

	public void Dispose()
	{
		Dispose(disposeManagedResources: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposeManagedResources)
	{
		if (bool_1)
		{
			return;
		}
		if (disposeManagedResources)
		{
			if (bool_7 && stream_0 != null)
			{
				stream_0.Dispose();
				stream_0 = null;
			}
			if (string_3 != null && string_0 != null && stream_1 != null)
			{
				stream_1.Dispose();
				stream_1 = null;
			}
			if (parallelDeflateOutputStream_0 != null)
			{
				parallelDeflateOutputStream_0.Dispose();
				parallelDeflateOutputStream_0 = null;
			}
		}
		bool_1 = true;
	}

	public void AddSelectedFiles(string selectionCriteria)
	{
		AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories: false);
	}

	public void AddSelectedFiles(string selectionCriteria, bool recurseDirectories)
	{
		AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories);
	}

	public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk)
	{
		AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories: false);
	}

	public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, bool recurseDirectories)
	{
		AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories);
	}

	public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive)
	{
		AddSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories: false);
	}

	public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
	{
		method_27(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, bool_18: false);
	}

	public void UpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
	{
		method_27(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, bool_18: true);
	}

	private void method_27(string string_5, string string_6, string string_7, bool bool_17, bool bool_18)
	{
		if (string_6 == null && Directory.Exists(string_5))
		{
			string_6 = string_5;
			string_5 = "*.*";
		}
		else if (string.IsNullOrEmpty(string_6))
		{
			string_6 = ".";
		}
		while (string_6.EndsWith("\\"))
		{
			string_6 = string_6.Substring(0, string_6.Length - 1);
		}
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("adding selection '{0}' from dir '{1}'...", string_5, string_6);
		}
		FileSelector fileSelector = new FileSelector(string_5, AddDirectoryWillTraverseReparsePoints);
		ReadOnlyCollection<string> readOnlyCollection = fileSelector.SelectFiles(string_6, bool_17);
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("found {0} files...", readOnlyCollection.Count);
		}
		method_18();
		Enum10 enum10_ = (bool_18 ? Enum10.const_1 : Enum10.const_0);
		string oldValue = string_6.ToLower();
		foreach (string item in readOnlyCollection)
		{
			string text = ((string_7 == null) ? null : Path.GetDirectoryName(item).ToLower().Replace(oldValue, string_7));
			if (File.Exists(item))
			{
				if (bool_18)
				{
					UpdateFile(item, text);
				}
				else
				{
					AddFile(item, text);
				}
			}
			else
			{
				method_32(item, text, enum10_, bool_17: false, 0);
			}
		}
		method_19();
	}

	public ICollection<ZipEntry> SelectEntries(string selectionCriteria)
	{
		FileSelector fileSelector = new FileSelector(selectionCriteria, AddDirectoryWillTraverseReparsePoints);
		return fileSelector.SelectEntries(this);
	}

	public ICollection<ZipEntry> SelectEntries(string selectionCriteria, string directoryPathInArchive)
	{
		FileSelector fileSelector = new FileSelector(selectionCriteria, AddDirectoryWillTraverseReparsePoints);
		return fileSelector.SelectEntries(this, directoryPathInArchive);
	}

	public int RemoveSelectedEntries(string selectionCriteria)
	{
		ICollection<ZipEntry> collection = SelectEntries(selectionCriteria);
		RemoveEntries(collection);
		return collection.Count;
	}

	public int RemoveSelectedEntries(string selectionCriteria, string directoryPathInArchive)
	{
		ICollection<ZipEntry> collection = SelectEntries(selectionCriteria, directoryPathInArchive);
		RemoveEntries(collection);
		return collection.Count;
	}

	public void ExtractSelectedEntries(string selectionCriteria)
	{
		foreach (ZipEntry item in SelectEntries(selectionCriteria))
		{
			item.Password = string_2;
			item.Extract();
		}
	}

	public void ExtractSelectedEntries(string selectionCriteria, ExtractExistingFileAction extractExistingFile)
	{
		foreach (ZipEntry item in SelectEntries(selectionCriteria))
		{
			item.Password = string_2;
			item.Extract(extractExistingFile);
		}
	}

	public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive)
	{
		foreach (ZipEntry item in SelectEntries(selectionCriteria, directoryPathInArchive))
		{
			item.Password = string_2;
			item.Extract();
		}
	}

	public void ExtractSelectedEntries(string selectionCriteria, string directoryInArchive, string extractDirectory)
	{
		foreach (ZipEntry item in SelectEntries(selectionCriteria, directoryInArchive))
		{
			item.Password = string_2;
			item.Extract(extractDirectory);
		}
	}

	public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive, string extractDirectory, ExtractExistingFileAction extractExistingFile)
	{
		foreach (ZipEntry item in SelectEntries(selectionCriteria, directoryPathInArchive))
		{
			item.Password = string_2;
			item.Extract(extractDirectory, extractExistingFile);
		}
	}

	public static ZipFile Read(string fileName)
	{
		return Read(fileName, null, DefaultEncoding);
	}

	public static ZipFile Read(string fileName, EventHandler<ReadProgressEventArgs> readProgress)
	{
		return Read(fileName, null, DefaultEncoding, readProgress);
	}

	public static ZipFile Read(string fileName, TextWriter statusMessageWriter)
	{
		return Read(fileName, statusMessageWriter, DefaultEncoding);
	}

	public static ZipFile Read(string fileName, TextWriter statusMessageWriter, EventHandler<ReadProgressEventArgs> readProgress)
	{
		return Read(fileName, statusMessageWriter, DefaultEncoding, readProgress);
	}

	public static ZipFile Read(string fileName, Encoding encoding)
	{
		return Read(fileName, null, encoding);
	}

	public static ZipFile Read(string fileName, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
	{
		return Read(fileName, null, encoding, readProgress);
	}

	public static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding)
	{
		return Read(fileName, statusMessageWriter, encoding, null);
	}

	public static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
	{
		ZipFile zipFile = new ZipFile();
		zipFile.ProvisionalAlternateEncoding = encoding;
		zipFile.textWriter_0 = statusMessageWriter;
		zipFile.string_0 = fileName;
		if (readProgress != null)
		{
			zipFile.eventHandler_1 = readProgress;
		}
		if (zipFile.Verbose)
		{
			zipFile.textWriter_0.WriteLine("reading from {0}...", fileName);
		}
		smethod_0(zipFile);
		zipFile.bool_4 = true;
		return zipFile;
	}

	public static ZipFile Read(Stream zipStream)
	{
		return Read(zipStream, null, DefaultEncoding);
	}

	public static ZipFile Read(Stream zipStream, EventHandler<ReadProgressEventArgs> readProgress)
	{
		return Read(zipStream, null, DefaultEncoding, readProgress);
	}

	public static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter)
	{
		return Read(zipStream, statusMessageWriter, DefaultEncoding);
	}

	public static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, EventHandler<ReadProgressEventArgs> readProgress)
	{
		return Read(zipStream, statusMessageWriter, DefaultEncoding, readProgress);
	}

	public static ZipFile Read(Stream zipStream, Encoding encoding)
	{
		return Read(zipStream, null, encoding);
	}

	public static ZipFile Read(Stream zipStream, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
	{
		return Read(zipStream, null, encoding, readProgress);
	}

	public static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding)
	{
		return Read(zipStream, statusMessageWriter, encoding, null);
	}

	public static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
	{
		if (zipStream == null)
		{
			throw new ArgumentException("The stream must be non-null", "zipStream");
		}
		ZipFile zipFile = new ZipFile();
		zipFile.encoding_0 = encoding;
		if (readProgress != null)
		{
			zipFile.eventHandler_1 = (EventHandler<ReadProgressEventArgs>)Delegate.Combine(zipFile.eventHandler_1, readProgress);
		}
		zipFile.textWriter_0 = statusMessageWriter;
		zipFile.stream_0 = ((zipStream.Position == 0L) ? zipStream : new Stream4(zipStream));
		zipFile.bool_7 = false;
		if (zipFile.Verbose)
		{
			zipFile.textWriter_0.WriteLine("reading from stream...");
		}
		smethod_0(zipFile);
		return zipFile;
	}

	public static ZipFile Read(byte[] buffer)
	{
		return Read(buffer, null, DefaultEncoding);
	}

	public static ZipFile Read(byte[] buffer, TextWriter statusMessageWriter)
	{
		return Read(buffer, statusMessageWriter, DefaultEncoding);
	}

	public static ZipFile Read(byte[] buffer, TextWriter statusMessageWriter, Encoding encoding)
	{
		ZipFile zipFile = new ZipFile();
		zipFile.textWriter_0 = statusMessageWriter;
		zipFile.encoding_0 = encoding;
		zipFile.stream_0 = new MemoryStream(buffer);
		zipFile.bool_7 = true;
		if (zipFile.Verbose)
		{
			zipFile.textWriter_0.WriteLine("reading from byte[]...");
		}
		smethod_0(zipFile);
		return zipFile;
	}

	private static void smethod_0(ZipFile zipFile_0)
	{
		Stream readStream = zipFile_0.ReadStream;
		try
		{
			if (!readStream.CanSeek)
			{
				smethod_4(zipFile_0);
				return;
			}
			zipFile_0.method_8();
			uint num = smethod_2(readStream);
			if (num == 101010256)
			{
				return;
			}
			int num2 = 0;
			bool flag = false;
			long num3 = readStream.Length - 64L;
			long num4 = Math.Max(readStream.Length - 16384L, 10L);
			do
			{
				readStream.Seek(num3, SeekOrigin.Begin);
				long num5 = Class7.smethod_12(readStream, 101010256);
				if (num5 == -1L)
				{
					num2++;
					num3 -= 32 * (num2 + 1) * num2;
					if (num3 < 0L)
					{
						num3 = 0L;
					}
				}
				else
				{
					flag = true;
				}
			}
			while (!flag && num3 > num4);
			if (flag)
			{
				zipFile_0.long_1 = readStream.Position - 4L;
				byte[] array = new byte[16];
				readStream.Read(array, 0, array.Length);
				zipFile_0.uint_0 = BitConverter.ToUInt16(array, 2);
				if (zipFile_0.uint_0 == 65535)
				{
					throw new ZipException("Spanned archives with more than 65534 segments are not supported at this time.");
				}
				zipFile_0.uint_0++;
				uint num6 = BitConverter.ToUInt32(array, 12);
				if (num6 == uint.MaxValue)
				{
					smethod_1(zipFile_0);
				}
				else
				{
					readStream.Seek(num6, SeekOrigin.Begin);
				}
				smethod_3(zipFile_0);
			}
			else
			{
				readStream.Seek(0L, SeekOrigin.Begin);
				smethod_4(zipFile_0);
			}
		}
		catch
		{
			if (zipFile_0.bool_7 && zipFile_0.stream_0 != null)
			{
				zipFile_0.stream_0.Close();
				zipFile_0.stream_0.Dispose();
				zipFile_0.stream_0 = null;
			}
			throw;
		}
		zipFile_0.bool_5 = false;
	}

	private static void smethod_1(ZipFile zipFile_0)
	{
		Stream readStream = zipFile_0.ReadStream;
		byte[] array = new byte[16];
		readStream.Seek(-40L, SeekOrigin.Current);
		readStream.Read(array, 0, 16);
		long offset = BitConverter.ToInt64(array, 8);
		readStream.Seek(offset, SeekOrigin.Begin);
		uint num = (uint)Class7.smethod_10(readStream);
		if (num != 101075792)
		{
			throw new BadReadException($"  ZipFile::Read(): Bad signature (0x{num:X8}) looking for ZIP64 EoCD Record at position 0x{readStream.Position:X8}");
		}
		readStream.Read(array, 0, 8);
		long num2 = BitConverter.ToInt64(array, 0);
		array = new byte[num2];
		readStream.Read(array, 0, array.Length);
		offset = BitConverter.ToInt64(array, 36);
		readStream.Seek(offset, SeekOrigin.Begin);
	}

	private static uint smethod_2(Stream stream_2)
	{
		return (uint)Class7.smethod_10(stream_2);
	}

	private static void smethod_3(ZipFile zipFile_0)
	{
		bool flag = false;
		ZipEntry zipEntry;
		while ((zipEntry = ZipEntry.smethod_9(zipFile_0)) != null)
		{
			zipEntry.method_32();
			zipFile_0.method_11(bool_17: true, null);
			if (zipFile_0.Verbose)
			{
				zipFile_0.StatusMessageTextWriter.WriteLine("entry {0}", zipEntry.FileName);
			}
			zipFile_0.dictionary_0.Add(zipEntry.FileName, zipEntry);
			if (zipEntry.bool_10)
			{
				flag = true;
			}
		}
		if (flag)
		{
			zipFile_0.UseZip64WhenSaving = Zip64Option.Always;
		}
		if (zipFile_0.long_1 > 0L)
		{
			zipFile_0.ReadStream.Seek(zipFile_0.long_1, SeekOrigin.Begin);
		}
		smethod_5(zipFile_0);
		if (zipFile_0.Verbose && !string.IsNullOrEmpty(zipFile_0.Comment))
		{
			zipFile_0.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zipFile_0.Comment);
		}
		if (zipFile_0.Verbose)
		{
			zipFile_0.StatusMessageTextWriter.WriteLine("read in {0} entries.", zipFile_0.dictionary_0.Count);
		}
		zipFile_0.method_9();
	}

	private static void smethod_4(ZipFile zipFile_0)
	{
		zipFile_0.method_8();
		zipFile_0.dictionary_0 = new Dictionary<string, ZipEntry>();
		if (zipFile_0.Verbose)
		{
			if (zipFile_0.Name == null)
			{
				zipFile_0.StatusMessageTextWriter.WriteLine("Reading zip from stream...");
			}
			else
			{
				zipFile_0.StatusMessageTextWriter.WriteLine("Reading zip {0}...", zipFile_0.Name);
			}
		}
		bool flag = true;
		Class31 class31_ = new Class31(zipFile_0);
		ZipEntry zipEntry;
		while ((zipEntry = ZipEntry.smethod_15(class31_, flag)) != null)
		{
			if (zipFile_0.Verbose)
			{
				zipFile_0.StatusMessageTextWriter.WriteLine("  {0}", zipEntry.FileName);
			}
			zipFile_0.dictionary_0.Add(zipEntry.FileName, zipEntry);
			flag = false;
		}
		try
		{
			ZipEntry zipEntry2;
			while ((zipEntry2 = ZipEntry.smethod_9(zipFile_0)) != null)
			{
				ZipEntry zipEntry3 = zipFile_0.dictionary_0[zipEntry2.FileName];
				if (zipEntry3 != null)
				{
					zipEntry3.string_2 = zipEntry2.Comment;
					if (zipEntry2.IsDirectory)
					{
						zipEntry3.method_0();
					}
				}
			}
			if (zipFile_0.long_1 > 0L)
			{
				zipFile_0.ReadStream.Seek(zipFile_0.long_1, SeekOrigin.Begin);
			}
			smethod_5(zipFile_0);
			if (zipFile_0.Verbose && !string.IsNullOrEmpty(zipFile_0.Comment))
			{
				zipFile_0.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zipFile_0.Comment);
			}
		}
		catch
		{
		}
		zipFile_0.method_9();
	}

	private static void smethod_5(ZipFile zipFile_0)
	{
		Stream readStream = zipFile_0.ReadStream;
		int num = Class7.smethod_8(readStream);
		byte[] array = null;
		int num2 = 0;
		if (num == 101075792L)
		{
			array = new byte[52];
			readStream.Read(array, 0, array.Length);
			long num3 = BitConverter.ToInt64(array, 0);
			if (num3 < 44L)
			{
				throw new ZipException("Bad DataSize in the ZIP64 Central Directory.");
			}
			zipFile_0.ushort_0 = BitConverter.ToUInt16(array, num2);
			num2 += 2;
			zipFile_0.ushort_1 = BitConverter.ToUInt16(array, num2);
			num2 += 2;
			zipFile_0.uint_0 = BitConverter.ToUInt32(array, num2);
			num2 += 2;
			array = new byte[num3 - 44L];
			readStream.Read(array, 0, array.Length);
			num = Class7.smethod_8(readStream);
			if (num != 117853008L)
			{
				throw new ZipException("Inconsistent metadata in the ZIP64 Central Directory.");
			}
			array = new byte[16];
			readStream.Read(array, 0, array.Length);
			num = Class7.smethod_8(readStream);
		}
		if (num != 101010256L)
		{
			readStream.Seek(-4L, SeekOrigin.Current);
			throw new BadReadException($"ZipFile::ReadCentralDirectoryFooter: Bad signature ({num:X8}) at position 0x{readStream.Position:X8}");
		}
		array = new byte[16];
		zipFile_0.ReadStream.Read(array, 0, array.Length);
		if (zipFile_0.uint_0 == 0)
		{
			zipFile_0.uint_0 = BitConverter.ToUInt16(array, 2);
		}
		smethod_6(zipFile_0);
	}

	private static void smethod_6(ZipFile zipFile_0)
	{
		byte[] array = new byte[2];
		zipFile_0.ReadStream.Read(array, 0, array.Length);
		short num = (short)(array[0] + array[1] * 256);
		if (num > 0)
		{
			array = new byte[num];
			zipFile_0.ReadStream.Read(array, 0, array.Length);
			string @string = DefaultEncoding.GetString(array, 0, array.Length);
			byte[] bytes = DefaultEncoding.GetBytes(@string);
			if (smethod_7(array, bytes))
			{
				zipFile_0.Comment = @string;
				return;
			}
			Encoding encoding = ((zipFile_0.encoding_0.CodePage == 437) ? Encoding.UTF8 : zipFile_0.encoding_0);
			zipFile_0.Comment = encoding.GetString(array, 0, array.Length);
		}
	}

	private static bool smethod_7(byte[] byte_0, byte[] byte_1)
	{
		if (byte_0.Length != byte_1.Length)
		{
			return false;
		}
		int num = 0;
		while (true)
		{
			if (num < byte_0.Length)
			{
				if (byte_0[num] != byte_1[num])
				{
					break;
				}
				num++;
				continue;
			}
			return true;
		}
		return false;
	}

	public static bool IsZipFile(string fileName)
	{
		return IsZipFile(fileName, testExtract: false);
	}

	public static bool IsZipFile(string fileName, bool testExtract)
	{
		bool result = false;
		try
		{
			if (!File.Exists(fileName))
			{
				return false;
			}
			using FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			result = IsZipFile(stream, testExtract);
		}
		catch
		{
		}
		return result;
	}

	public static bool IsZipFile(Stream stream, bool testExtract)
	{
		bool result = false;
		try
		{
			if (!stream.CanRead)
			{
				return false;
			}
			Stream @null = Stream.Null;
			using (ZipFile zipFile = Read(stream, null, Encoding.GetEncoding("IBM437")))
			{
				if (testExtract)
				{
					foreach (ZipEntry item in zipFile)
					{
						if (!item.IsDirectory)
						{
							item.Extract(@null);
						}
					}
				}
			}
			result = true;
		}
		catch
		{
		}
		return result;
	}

	public void ExtractAll(string path)
	{
		method_28(path, bool_17: true);
	}

	public void ExtractAll(string path, ExtractExistingFileAction extractExistingFile)
	{
		ExtractExistingFile = extractExistingFile;
		method_28(path, bool_17: true);
	}

	private void method_28(string string_5, bool bool_17)
	{
		bool flag = Verbose;
		bool_11 = true;
		try
		{
			method_17(string_5);
			int num = 0;
			foreach (ZipEntry value in dictionary_0.Values)
			{
				if (flag)
				{
					StatusMessageTextWriter.WriteLine("\n{1,-22} {2,-8} {3,4}   {4,-8}  {0}", "Name", "Modified", "Size", "Ratio", "Packed");
					StatusMessageTextWriter.WriteLine(new string('-', 72));
					flag = false;
				}
				if (Verbose)
				{
					StatusMessageTextWriter.WriteLine("{1,-22} {2,-8} {3,4:F0}%   {4,-8} {0}", value.FileName, value.LastModified.ToString("yyyy-MM-dd HH:mm:ss"), value.UncompressedSize, value.CompressionRatio, value.CompressedSize);
					if (!string.IsNullOrEmpty(value.Comment))
					{
						StatusMessageTextWriter.WriteLine("  Comment: {0}", value.Comment);
					}
				}
				value.Password = string_2;
				method_12(num, bool_17: true, value, string_5);
				if (bool_17)
				{
					value.ExtractExistingFile = ExtractExistingFile;
				}
				value.Extract(string_5);
				num++;
				method_12(num, bool_17: false, value, string_5);
				if (value.ArchiveStream != null && value.ArchiveStream.CanRead)
				{
					value.ArchiveStream.Close();
				}
				if (bool_9)
				{
					break;
				}
			}
			foreach (ZipEntry value2 in dictionary_0.Values)
			{
				if (value2.IsDirectory || value2.FileName.EndsWith("/"))
				{
					string text = (value2.FileName.StartsWith("/") ? Path.Combine(string_5, value2.FileName.Substring(1)) : Path.Combine(string_5, value2.FileName));
					value2.method_46(text, bool_15: false);
				}
			}
			method_16(string_5);
		}
		finally
		{
			bool_11 = false;
		}
	}

	public IEnumerator<ZipEntry> GetEnumerator()
	{
		Class2 @class = new Class2(0);
		@class.zipFile_0 = this;
		return @class;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	[DispId(-4)]
	public IEnumerator GetNewEnum()
	{
		return GetEnumerator();
	}

	public ZipEntry AddItem(string fileOrDirectoryName)
	{
		return AddItem(fileOrDirectoryName, null);
	}

	public ZipEntry AddItem(string fileOrDirectoryName, string directoryPathInArchive)
	{
		if (File.Exists(fileOrDirectoryName))
		{
			return AddFile(fileOrDirectoryName, directoryPathInArchive);
		}
		if (!Directory.Exists(fileOrDirectoryName))
		{
			throw new FileNotFoundException($"That file or directory ({fileOrDirectoryName}) does not exist!");
		}
		return AddDirectory(fileOrDirectoryName, directoryPathInArchive);
	}

	public ZipEntry AddFile(string fileName)
	{
		return AddFile(fileName, null);
	}

	public ZipEntry AddFile(string fileName, string directoryPathInArchive)
	{
		string string_ = ZipEntry.smethod_0(fileName, directoryPathInArchive);
		ZipEntry zipEntry_ = ZipEntry.smethod_2(fileName, string_);
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("adding {0}...", fileName);
		}
		return method_29(zipEntry_);
	}

	public void RemoveEntries(ICollection<ZipEntry> entriesToRemove)
	{
		foreach (ZipEntry item in entriesToRemove)
		{
			RemoveEntry(item);
		}
	}

	public void RemoveEntries(ICollection<string> entriesToRemove)
	{
		foreach (string item in entriesToRemove)
		{
			RemoveEntry(item);
		}
	}

	public void AddFiles(IEnumerable<string> fileNames)
	{
		AddFiles(fileNames, null);
	}

	public void UpdateFiles(IEnumerable<string> fileNames)
	{
		UpdateFiles(fileNames, null);
	}

	public void AddFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
	{
		AddFiles(fileNames, preserveDirHierarchy: false, directoryPathInArchive);
	}

	public void AddFiles(IEnumerable<string> fileNames, bool preserveDirHierarchy, string directoryPathInArchive)
	{
		method_18();
		if (preserveDirHierarchy)
		{
			foreach (string fileName in fileNames)
			{
				if (directoryPathInArchive != null)
				{
					string fullPath = Path.GetFullPath(Path.Combine(directoryPathInArchive, Path.GetDirectoryName(fileName)));
					AddFile(fileName, fullPath);
				}
				else
				{
					AddFile(fileName, null);
				}
			}
		}
		else
		{
			foreach (string fileName2 in fileNames)
			{
				AddFile(fileName2, directoryPathInArchive);
			}
		}
		method_19();
	}

	public void UpdateFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
	{
		method_18();
		foreach (string fileName in fileNames)
		{
			UpdateFile(fileName, directoryPathInArchive);
		}
		method_19();
	}

	public ZipEntry UpdateFile(string fileName)
	{
		return UpdateFile(fileName, null);
	}

	public ZipEntry UpdateFile(string fileName, string directoryPathInArchive)
	{
		string fileName2 = ZipEntry.smethod_0(fileName, directoryPathInArchive);
		if (this[fileName2] != null)
		{
			RemoveEntry(fileName2);
		}
		return AddFile(fileName, directoryPathInArchive);
	}

	public ZipEntry UpdateDirectory(string directoryName)
	{
		return UpdateDirectory(directoryName, null);
	}

	public ZipEntry UpdateDirectory(string directoryName, string directoryPathInArchive)
	{
		return method_30(directoryName, directoryPathInArchive, Enum10.const_1);
	}

	public void UpdateItem(string itemName)
	{
		UpdateItem(itemName, null);
	}

	public void UpdateItem(string itemName, string directoryPathInArchive)
	{
		if (File.Exists(itemName))
		{
			UpdateFile(itemName, directoryPathInArchive);
			return;
		}
		if (!Directory.Exists(itemName))
		{
			throw new FileNotFoundException($"That file or directory ({itemName}) does not exist!");
		}
		UpdateDirectory(itemName, directoryPathInArchive);
	}

	public ZipEntry AddEntry(string entryName, string content)
	{
		return AddEntry(entryName, content, Encoding.Default);
	}

	public ZipEntry AddEntry(string entryName, string content, Encoding encoding)
	{
		MemoryStream memoryStream = new MemoryStream();
		StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
		streamWriter.Write(content);
		streamWriter.Flush();
		memoryStream.Seek(0L, SeekOrigin.Begin);
		return AddEntry(entryName, memoryStream);
	}

	public ZipEntry AddEntry(string entryName, Stream stream)
	{
		ZipEntry zipEntry = ZipEntry.smethod_3(entryName, stream);
		zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
		}
		return method_29(zipEntry);
	}

	public ZipEntry AddEntry(string entryName, WriteDelegate writer)
	{
		ZipEntry zipEntry_ = ZipEntry.smethod_4(entryName, writer);
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
		}
		return method_29(zipEntry_);
	}

	public ZipEntry AddEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
	{
		ZipEntry zipEntry = ZipEntry.smethod_5(entryName, opener, closer);
		zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
		}
		return method_29(zipEntry);
	}

	private ZipEntry method_29(ZipEntry zipEntry_0)
	{
		zipEntry_0.class31_0 = new Class31(this);
		zipEntry_0.CompressionLevel = CompressionLevel;
		zipEntry_0.ExtractExistingFile = ExtractExistingFile;
		zipEntry_0.ZipErrorAction = ZipErrorAction;
		zipEntry_0.SetCompression = SetCompression;
		zipEntry_0.ProvisionalAlternateEncoding = ProvisionalAlternateEncoding;
		zipEntry_0.Password = string_2;
		zipEntry_0.Encryption = Encryption;
		zipEntry_0.EmitTimesInWindowsFormatWhenSaving = bool_2;
		zipEntry_0.EmitTimesInUnixFormatWhenSaving = bool_3;
		method_31(zipEntry_0.FileName, zipEntry_0);
		method_20(zipEntry_0);
		return zipEntry_0;
	}

	public ZipEntry UpdateEntry(string entryName, string content)
	{
		return UpdateEntry(entryName, content, Encoding.Default);
	}

	public ZipEntry UpdateEntry(string entryName, string content, Encoding encoding)
	{
		string string_ = null;
		if (entryName.IndexOf('\\') != -1)
		{
			string_ = Path.GetDirectoryName(entryName);
			entryName = Path.GetFileName(entryName);
		}
		string fileName = ZipEntry.smethod_0(entryName, string_);
		if (this[fileName] != null)
		{
			RemoveEntry(fileName);
		}
		return AddEntry(entryName, content, encoding);
	}

	public ZipEntry UpdateEntry(string entryName, Stream stream)
	{
		string string_ = null;
		if (entryName.IndexOf('\\') != -1)
		{
			string_ = Path.GetDirectoryName(entryName);
			entryName = Path.GetFileName(entryName);
		}
		string fileName = ZipEntry.smethod_0(entryName, string_);
		if (this[fileName] != null)
		{
			RemoveEntry(fileName);
		}
		return AddEntry(entryName, stream);
	}

	public ZipEntry AddEntry(string entryName, byte[] byteContent)
	{
		if (byteContent == null)
		{
			throw new ArgumentException("bad argument", "byteContent");
		}
		MemoryStream stream = new MemoryStream(byteContent);
		return AddEntry(entryName, stream);
	}

	public ZipEntry UpdateEntry(string entryName, byte[] byteContent)
	{
		string string_ = null;
		if (entryName.IndexOf('\\') != -1)
		{
			string_ = Path.GetDirectoryName(entryName);
			entryName = Path.GetFileName(entryName);
		}
		string fileName = ZipEntry.smethod_0(entryName, string_);
		if (this[fileName] != null)
		{
			RemoveEntry(fileName);
		}
		return AddEntry(entryName, byteContent);
	}

	public ZipEntry AddDirectory(string directoryName)
	{
		return AddDirectory(directoryName, null);
	}

	public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
	{
		return method_30(directoryName, directoryPathInArchive, Enum10.const_0);
	}

	public ZipEntry AddDirectoryByName(string directoryNameInArchive)
	{
		ZipEntry zipEntry = ZipEntry.smethod_1(directoryNameInArchive);
		zipEntry.class31_0 = new Class31(this);
		zipEntry.method_0();
		zipEntry.ProvisionalAlternateEncoding = ProvisionalAlternateEncoding;
		zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
		zipEntry.EmitTimesInWindowsFormatWhenSaving = bool_2;
		zipEntry.EmitTimesInUnixFormatWhenSaving = bool_3;
		zipEntry.zipEntrySource_0 = ZipEntrySource.Stream;
		method_31(zipEntry.FileName, zipEntry);
		method_20(zipEntry);
		return zipEntry;
	}

	private ZipEntry method_30(string string_5, string string_6, Enum10 enum10_0)
	{
		if (string_6 == null)
		{
			string_6 = "";
		}
		return method_32(string_5, string_6, enum10_0, bool_17: true, 0);
	}

	internal void method_31(string string_5, ZipEntry zipEntry_0)
	{
		dictionary_0.Add(string_5, zipEntry_0);
		list_0 = null;
		bool_5 = true;
	}

	private ZipEntry method_32(string string_5, string string_6, Enum10 enum10_0, bool bool_17, int int_3)
	{
		if (Verbose)
		{
			StatusMessageTextWriter.WriteLine("{0} {1}...", (enum10_0 == Enum10.const_0) ? "adding" : "Adding or updating", string_5);
		}
		if (int_3 == 0)
		{
			method_18();
		}
		string text = string_6;
		ZipEntry zipEntry = null;
		if (int_3 > 0)
		{
			int num = string_5.Length;
			for (int num2 = int_3; num2 > 0; num2--)
			{
				num = string_5.LastIndexOfAny("/\\".ToCharArray(), num - 1, num - 1);
			}
			text = string_5.Substring(num + 1);
			text = Path.Combine(string_6, text);
		}
		if (int_3 > 0 || string_6 != "")
		{
			zipEntry = ZipEntry.smethod_2(string_5, text);
			zipEntry.class31_0 = new Class31(this);
			zipEntry.ProvisionalAlternateEncoding = ProvisionalAlternateEncoding;
			zipEntry.method_0();
			zipEntry.EmitTimesInWindowsFormatWhenSaving = bool_2;
			zipEntry.EmitTimesInUnixFormatWhenSaving = bool_3;
			if (!dictionary_0.ContainsKey(zipEntry.FileName))
			{
				method_31(zipEntry.FileName, zipEntry);
				method_20(zipEntry);
			}
			text = zipEntry.FileName;
		}
		string[] files = Directory.GetFiles(string_5);
		if (bool_17)
		{
			string[] array = files;
			foreach (string fileName in array)
			{
				if (enum10_0 == Enum10.const_0)
				{
					AddFile(fileName, text);
				}
				else
				{
					UpdateFile(fileName, text);
				}
			}
			string[] directories = Directory.GetDirectories(string_5);
			string[] array2 = directories;
			foreach (string text2 in array2)
			{
				FileAttributes attributes = File.GetAttributes(text2);
				if (AddDirectoryWillTraverseReparsePoints || (attributes & FileAttributes.ReparsePoint) == 0)
				{
					method_32(text2, string_6, enum10_0, bool_17, int_3 + 1);
				}
			}
		}
		if (int_3 == 0)
		{
			method_19();
		}
		return zipEntry;
	}

	public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor)
	{
		SelfExtractorSaveOptions selfExtractorSaveOptions = new SelfExtractorSaveOptions();
		selfExtractorSaveOptions.Flavor = flavor;
		SaveSelfExtractor(exeToGenerate, selfExtractorSaveOptions);
	}

	public void SaveSelfExtractor(string exeToGenerate, SelfExtractorSaveOptions options)
	{
		if (string_0 == null)
		{
			stream_1 = null;
		}
		bool_12 = true;
		string_0 = exeToGenerate;
		if (Directory.Exists(string_0))
		{
			throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "exeToGenerate"));
		}
		bool_5 = true;
		bool_4 = File.Exists(string_0);
		method_34(exeToGenerate, options);
		Save();
		bool_12 = false;
	}

	private void method_33(Assembly assembly_0, string string_5, string string_6)
	{
		int num = 0;
		byte[] array = new byte[1024];
		using Stream stream = assembly_0.GetManifestResourceStream(string_5);
		if (stream == null)
		{
			throw new ZipException($"missing resource '{string_5}'");
		}
		using FileStream fileStream = File.OpenWrite(string_6);
		do
		{
			num = stream.Read(array, 0, array.Length);
			fileStream.Write(array, 0, num);
		}
		while (num > 0);
	}

	private void method_34(string string_5, SelfExtractorSaveOptions selfExtractorSaveOptions_0)
	{
		string text = null;
		string text2 = null;
		string text3 = null;
		try
		{
			if (File.Exists(string_5) && Verbose)
			{
				StatusMessageTextWriter.WriteLine("The existing file ({0}) will be overwritten.", string_5);
			}
			if (!string_5.EndsWith(".exe") && Verbose)
			{
				StatusMessageTextWriter.WriteLine("Warning: The generated self-extracting file will not have an .exe extension.");
			}
			text2 = smethod_8("exe");
			Assembly assembly = typeof(ZipFile).Assembly;
			CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();
			Class0 @class = null;
			Class0[] array = class0_0;
			foreach (Class0 class2 in array)
			{
				if (class2.selfExtractorFlavor_0 == selfExtractorSaveOptions_0.Flavor)
				{
					@class = class2;
					break;
				}
			}
			if (@class == null)
			{
				throw new BadStateException($"While saving a Self-Extracting Zip, Cannot find that flavor ({selfExtractorSaveOptions_0.Flavor})?");
			}
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.ReferencedAssemblies.Add(assembly.Location);
			if (@class.list_0 != null)
			{
				foreach (string item in @class.list_0)
				{
					compilerParameters.ReferencedAssemblies.Add(item);
				}
			}
			compilerParameters.GenerateInMemory = false;
			compilerParameters.GenerateExecutable = true;
			compilerParameters.IncludeDebugInformation = false;
			compilerParameters.CompilerOptions = "";
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			StringBuilder stringBuilder = new StringBuilder();
			string text4 = smethod_8("cs");
			using (ZipFile zipFile = Read(executingAssembly.GetManifestResourceStream("Ionic.Zip.Resources.ZippedResources.zip")))
			{
				text3 = smethod_8("tmp");
				if (string.IsNullOrEmpty(selfExtractorSaveOptions_0.IconFile))
				{
					Directory.CreateDirectory(text3);
					ZipEntry zipEntry = zipFile["zippedFile.ico"];
					if ((zipEntry.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						zipEntry.Attributes ^= FileAttributes.ReadOnly;
					}
					zipEntry.Extract(text3);
					text = Path.Combine(text3, "zippedFile.ico");
					compilerParameters.CompilerOptions += $"/win32icon:\"{text}\"";
				}
				else
				{
					compilerParameters.CompilerOptions += $"/win32icon:\"{selfExtractorSaveOptions_0.IconFile}\"";
				}
				compilerParameters.OutputAssembly = text2;
				if (selfExtractorSaveOptions_0.Flavor == SelfExtractorFlavor.WinFormsApplication)
				{
					compilerParameters.CompilerOptions += " /target:winexe";
				}
				if (compilerParameters.CompilerOptions == "")
				{
					compilerParameters.CompilerOptions = null;
				}
				if (@class.list_1 != null && @class.list_1.Count != 0)
				{
					if (!Directory.Exists(text3))
					{
						Directory.CreateDirectory(text3);
					}
					foreach (string item2 in @class.list_1)
					{
						string text5 = Path.Combine(text3, item2);
						method_33(executingAssembly, item2, text5);
						compilerParameters.EmbeddedResources.Add(text5);
					}
				}
				compilerParameters.EmbeddedResources.Add(assembly.Location);
				stringBuilder.Append("// " + Path.GetFileName(text4) + "\n").Append("// --------------------------------------------\n//\n").Append("// This SFX source file was generated by DotNetZip ")
					.Append(LibraryVersion.ToString())
					.Append("\n//         at ")
					.Append(DateTime.Now.ToString("yyyy MMMM dd  HH:mm:ss"))
					.Append("\n//\n// --------------------------------------------\n\n\n");
				if (!string.IsNullOrEmpty(selfExtractorSaveOptions_0.Description))
				{
					stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"" + selfExtractorSaveOptions_0.Description.Replace("\"", "") + "\")]\n");
				}
				else
				{
					stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"DotNetZip SFX Archive\")]\n");
				}
				if (!string.IsNullOrEmpty(selfExtractorSaveOptions_0.ProductVersion))
				{
					stringBuilder.Append("[assembly: System.Reflection.AssemblyInformationalVersion(\"" + selfExtractorSaveOptions_0.ProductVersion.Replace("\"", "") + "\")]\n");
				}
				string text6 = "Extractor: Copyright © Dino Chiesa 2008, 2009";
				if (!string.IsNullOrEmpty(selfExtractorSaveOptions_0.Copyright))
				{
					text6 = text6 + "Contents: " + selfExtractorSaveOptions_0.Copyright.Replace("\"", "");
				}
				if (!string.IsNullOrEmpty(selfExtractorSaveOptions_0.ProductName))
				{
					stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"").Append(selfExtractorSaveOptions_0.ProductName.Replace("\"", "")).Append("\")]\n");
				}
				else
				{
					stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"DotNetZip\")]\n");
				}
				stringBuilder.Append("[assembly: System.Reflection.AssemblyCopyright(\"" + text6 + "\")]\n").Append($"[assembly: System.Reflection.AssemblyVersion(\"{LibraryVersion.ToString()}\")]\n");
				if (selfExtractorSaveOptions_0.FileVersion != null)
				{
					stringBuilder.Append($"[assembly: System.Reflection.AssemblyFileVersion(\"{selfExtractorSaveOptions_0.FileVersion.ToString()}\")]\n");
				}
				stringBuilder.Append("\n\n\n");
				string text7 = selfExtractorSaveOptions_0.DefaultExtractDirectory;
				if (text7 != null)
				{
					text7 = text7.Replace("\"", "").Replace("\\", "\\\\");
				}
				string text8 = selfExtractorSaveOptions_0.PostExtractCommandLine;
				if (text8 != null)
				{
					text8 = text8.Replace("\\", "\\\\");
					text8 = text8.Replace("\"", "\\\"");
				}
				foreach (string item3 in @class.list_2)
				{
					using Stream stream = zipFile[item3].OpenReader();
					if (stream == null)
					{
						throw new ZipException($"missing resource '{item3}'");
					}
					using (StreamReader streamReader = new StreamReader(stream))
					{
						while (streamReader.Peek() >= 0)
						{
							string text9 = streamReader.ReadLine();
							if (text7 != null)
							{
								text9 = text9.Replace("@@EXTRACTLOCATION", text7);
							}
							text9 = text9.Replace("@@REMOVE_AFTER_EXECUTE", selfExtractorSaveOptions_0.RemoveUnpackedFilesAfterExecute.ToString());
							text9 = text9.Replace("@@QUIET", selfExtractorSaveOptions_0.Quiet.ToString());
							text9 = text9.Replace("@@EXTRACT_EXISTING_FILE", ((int)selfExtractorSaveOptions_0.ExtractExistingFile).ToString());
							if (text8 != null)
							{
								text9 = text9.Replace("@@POST_UNPACK_CMD_LINE", text8);
							}
							stringBuilder.Append(text9).Append("\n");
						}
					}
					stringBuilder.Append("\n\n");
				}
			}
			string text10 = stringBuilder.ToString();
			CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, text10);
			if (compilerResults == null)
			{
				throw new SfxGenerationException("Cannot compile the extraction logic!");
			}
			if (Verbose)
			{
				StringEnumerator enumerator4 = compilerResults.Output.GetEnumerator();
				try
				{
					while (enumerator4.MoveNext())
					{
						string current4 = enumerator4.Current;
						StatusMessageTextWriter.WriteLine(current4);
					}
				}
				finally
				{
					if (enumerator4 is IDisposable disposable)
					{
						disposable.Dispose();
					}
				}
			}
			if (compilerResults.Errors.Count != 0)
			{
				using (TextWriter textWriter = new StreamWriter(text4))
				{
					textWriter.Write(text10);
					textWriter.Write("\n\n\n// ------------------------------------------------------------------\n");
					textWriter.Write("// Errors during compilation: \n//\n");
					string fileName = Path.GetFileName(text4);
					foreach (CompilerError error in compilerResults.Errors)
					{
						textWriter.Write(string.Format("//   {0}({1},{2}): {3} {4}: {5}\n//\n", fileName, error.Line, error.Column, error.IsWarning ? "Warning" : "error", error.ErrorNumber, error.ErrorText));
					}
				}
				throw new SfxGenerationException($"Errors compiling the extraction logic!  {text4}");
			}
			method_5(ZipProgressEventType.Saving_AfterCompileSelfExtractor);
			using (Stream stream2 = File.OpenRead(text2))
			{
				byte[] array2 = new byte[4000];
				int num = 1;
				while (num != 0)
				{
					num = stream2.Read(array2, 0, array2.Length);
					if (num != 0)
					{
						WriteStream.Write(array2, 0, num);
					}
				}
			}
			method_5(ZipProgressEventType.Saving_AfterSaveTempArchive);
		}
		finally
		{
			try
			{
				if (Directory.Exists(text3))
				{
					try
					{
						Directory.Delete(text3, recursive: true);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Exception: {0}", ex.ToString());
					}
				}
				if (File.Exists(text2))
				{
					try
					{
						File.Delete(text2);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}
	}

	internal static string smethod_8(string string_5)
	{
		string text = null;
		string name = Assembly.GetExecutingAssembly().GetName().Name;
		string tempPath = Path.GetTempPath();
		int num = 0;
		do
		{
			num++;
			string path = string.Format("{0}-{1}-{2}.{3}", name, DateTime.Now.ToString("yyyyMMMdd-HHmmss"), num, string_5);
			text = Path.Combine(tempPath, path);
		}
		while (File.Exists(text) || Directory.Exists(text));
		return text;
	}
}
