using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ionic.Zlib;

namespace Ionic.Zip;

[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("ebc25cf6-9120-4283-b972-0e5520d00004")]
[ComVisible(true)]
public class ZipEntry
{
	private Class6 class6_0;

	private Class6 class6_1;

	internal DateTime dateTime_0;

	private DateTime dateTime_1;

	private DateTime dateTime_2;

	private DateTime dateTime_3;

	private bool bool_0;

	private bool bool_1 = true;

	private bool bool_2;

	private bool bool_3 = true;

	internal string string_0;

	private string string_1;

	internal short short_0;

	internal short short_1;

	internal short short_2;

	private short short_3;

	private CompressionLevel compressionLevel_0;

	internal string string_2;

	private bool bool_4;

	private byte[] byte_0;

	internal long long_0;

	internal long long_1;

	internal long long_2;

	internal int int_0;

	private bool bool_5;

	internal int int_1;

	internal byte[] byte_1;

	private bool bool_6;

	private bool bool_7;

	private bool bool_8;

	private bool bool_9;

	private uint uint_0;

	private static Encoding encoding_0 = Encoding.GetEncoding("IBM437");

	private Encoding encoding_1 = Encoding.GetEncoding("IBM437");

	private Encoding encoding_2;

	internal Class31 class31_0;

	internal long long_3 = -1L;

	private byte[] byte_2;

	internal long long_4;

	private long long_5;

	private long long_6;

	internal int int_2;

	internal int int_3;

	internal bool bool_10;

	private uint uint_1;

	internal string string_3;

	internal ZipEntrySource zipEntrySource_0;

	internal EncryptionAlgorithm encryptionAlgorithm_0;

	internal EncryptionAlgorithm encryptionAlgorithm_1;

	internal byte[] byte_3;

	internal Stream stream_0;

	private Stream stream_1;

	private long? nullable_0;

	private bool bool_11;

	private bool bool_12;

	private bool bool_13;

	private bool? nullable_1;

	private bool? nullable_2;

	private bool bool_14;

	private ZipEntryTimestamp zipEntryTimestamp_0;

	private static DateTime dateTime_4 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	private static DateTime dateTime_5 = DateTime.FromFileTimeUtc(0L);

	private static DateTime dateTime_6 = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	private WriteDelegate writeDelegate_0;

	private OpenDelegate openDelegate_0;

	private CloseDelegate closeDelegate_0;

	private object object_0 = new object();

	private short short_4;

	private short short_5;

	private int int_4;

	private short short_6;

	private short short_7;

	private short short_8;

	private Stream stream_2;

	private int int_5;

	[CompilerGenerated]
	private ExtractExistingFileAction extractExistingFileAction_0;

	[CompilerGenerated]
	private ZipErrorAction zipErrorAction_0;

	[CompilerGenerated]
	private SetCompressionCallback setCompressionCallback_0;

	public DateTime LastModified
	{
		get
		{
			return dateTime_0.ToLocalTime();
		}
		set
		{
			dateTime_0 = ((value.Kind == DateTimeKind.Unspecified) ? DateTime.SpecifyKind(value, DateTimeKind.Local) : value.ToLocalTime());
			dateTime_1 = Class7.smethod_13(dateTime_0).ToUniversalTime();
			bool_6 = true;
		}
	}

	private int BufferSize => class31_0.method_5();

	public DateTime ModifiedTime
	{
		get
		{
			return dateTime_1;
		}
		set
		{
			SetEntryTimes(dateTime_3, dateTime_2, value);
		}
	}

	public DateTime AccessedTime
	{
		get
		{
			return dateTime_2;
		}
		set
		{
			SetEntryTimes(dateTime_3, value, dateTime_1);
		}
	}

	public DateTime CreationTime
	{
		get
		{
			return dateTime_3;
		}
		set
		{
			SetEntryTimes(value, dateTime_2, dateTime_1);
		}
	}

	public bool EmitTimesInWindowsFormatWhenSaving
	{
		get
		{
			return bool_1;
		}
		set
		{
			bool_1 = value;
			bool_6 = true;
		}
	}

	public bool EmitTimesInUnixFormatWhenSaving
	{
		get
		{
			return bool_2;
		}
		set
		{
			bool_2 = value;
			bool_6 = true;
		}
	}

	public ZipEntryTimestamp Timestamp => zipEntryTimestamp_0;

	public FileAttributes Attributes
	{
		get
		{
			return (FileAttributes)int_4;
		}
		set
		{
			int_4 = (int)value;
			short_4 = 45;
			bool_6 = true;
		}
	}

	internal string LocalFileName => string_0;

	public string FileName
	{
		get
		{
			return string_1;
		}
		set
		{
			if (class31_0.method_0() == null)
			{
				throw new ZipException("Cannot rename ZipEntry; not supported in ZipOutputStream/ZipInputStream.");
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ZipException("The FileName must be non empty and non-null.");
			}
			string text = smethod_0(value, null);
			if (!(string_1 == text))
			{
				class31_0.method_0().RemoveEntry(this);
				class31_0.method_0().method_31(text, this);
				string_1 = text;
				class31_0.method_0().method_22();
				bool_6 = true;
			}
		}
	}

	public Stream InputStream
	{
		get
		{
			return stream_1;
		}
		set
		{
			if (zipEntrySource_0 != ZipEntrySource.Stream)
			{
				throw new ZipException("You must not set the input stream for this ZipEntry.");
			}
			bool_11 = true;
			stream_1 = value;
		}
	}

	public bool InputStreamWasJitProvided => bool_11;

	public ZipEntrySource Source => zipEntrySource_0;

	public short VersionNeeded => short_0;

	public string Comment
	{
		get
		{
			return string_2;
		}
		set
		{
			string_2 = value;
			bool_6 = true;
		}
	}

	public bool? RequiresZip64 => nullable_1;

	public bool? OutputUsedZip64 => nullable_2;

	public short BitField => short_1;

	public CompressionMethod CompressionMethod
	{
		get
		{
			return (CompressionMethod)short_2;
		}
		set
		{
			if (value != (CompressionMethod)short_2)
			{
				if (value != 0 && value != CompressionMethod.Deflate)
				{
					throw new InvalidOperationException("Unsupported compression method. Specify CompressionMethod.Deflate or CompressionMethod.None.");
				}
				short_2 = (short)value;
				if (short_2 == 0)
				{
					compressionLevel_0 = CompressionLevel.None;
				}
				else if (CompressionLevel == CompressionLevel.None)
				{
					compressionLevel_0 = CompressionLevel.Default;
				}
				class31_0.method_0().method_22();
				bool_7 = true;
			}
		}
	}

	public CompressionLevel CompressionLevel
	{
		get
		{
			return compressionLevel_0;
		}
		set
		{
			if (value == CompressionLevel.Default && short_2 == 8)
			{
				return;
			}
			compressionLevel_0 = value;
			if (value != 0 || short_2 != 0)
			{
				short_2 = (short)((compressionLevel_0 != 0) ? 8 : 0);
				if (class31_0.method_0() != null)
				{
					class31_0.method_0().method_22();
				}
				bool_7 = true;
			}
		}
	}

	public long CompressedSize => long_0;

	public long UncompressedSize => long_2;

	public double CompressionRatio
	{
		get
		{
			if (UncompressedSize == 0L)
			{
				return 0.0;
			}
			return 100.0 * (1.0 - 1.0 * (double)CompressedSize / (1.0 * (double)UncompressedSize));
		}
	}

	public int Crc => int_1;

	public bool IsDirectory => bool_4;

	public bool UsesEncryption => encryptionAlgorithm_1 != EncryptionAlgorithm.None;

	public EncryptionAlgorithm Encryption
	{
		get
		{
			return encryptionAlgorithm_0;
		}
		set
		{
			if (value != encryptionAlgorithm_0)
			{
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				encryptionAlgorithm_0 = value;
				bool_7 = true;
				if (class31_0.method_0() != null)
				{
					class31_0.method_0().method_22();
				}
			}
		}
	}

	public string Password
	{
		set
		{
			string_3 = value;
			if (string_3 == null)
			{
				encryptionAlgorithm_0 = EncryptionAlgorithm.None;
				return;
			}
			if (zipEntrySource_0 == ZipEntrySource.ZipFile && !bool_8)
			{
				bool_7 = true;
			}
			if (Encryption == EncryptionAlgorithm.None)
			{
				encryptionAlgorithm_0 = EncryptionAlgorithm.PkzipWeak;
			}
		}
	}

	internal bool IsChanged => bool_7 | bool_6;

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
		[CompilerGenerated]
		get
		{
			return zipErrorAction_0;
		}
		[CompilerGenerated]
		set
		{
			zipErrorAction_0 = value;
		}
	}

	public bool IncludedInMostRecentSave => !bool_9;

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

	public bool UseUnicodeAsNecessary
	{
		get
		{
			return encoding_1 == Encoding.GetEncoding("UTF-8");
		}
		set
		{
			encoding_1 = (value ? Encoding.GetEncoding("UTF-8") : ZipFile.DefaultEncoding);
		}
	}

	public Encoding ProvisionalAlternateEncoding
	{
		get
		{
			return encoding_1;
		}
		set
		{
			encoding_1 = value;
		}
	}

	public Encoding ActualEncoding => encoding_2;

	public bool IsText
	{
		get
		{
			return bool_14;
		}
		set
		{
			bool_14 = value;
		}
	}

	internal Stream ArchiveStream
	{
		get
		{
			if (stream_0 == null)
			{
				if (class31_0.method_0() != null)
				{
					ZipFile zipFile = class31_0.method_0();
					zipFile.method_24();
					stream_0 = zipFile.method_23(uint_0);
				}
				else
				{
					stream_0 = class31_0.method_1().OutputStream;
				}
			}
			return stream_0;
		}
	}

	internal long FileDataPosition
	{
		get
		{
			if (long_3 == -1L)
			{
				method_1();
			}
			return long_3;
		}
	}

	private int LengthOfHeader
	{
		get
		{
			if (int_2 == 0)
			{
				method_1();
			}
			return int_2;
		}
	}

	internal bool AttributesIndicateDirectory
	{
		get
		{
			if (short_5 == 0)
			{
				return (int_4 & 0x10) == 16;
			}
			return false;
		}
	}

	public string Info
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"ZipEntry: {FileName}\n").Append($"  Version Made By: 0x{short_4:X}\n").Append($"  Version Needed: 0x{VersionNeeded:X}\n")
				.Append($"  Compression Method: {CompressionMethod}\n")
				.Append($"  Compressed: 0x{CompressedSize:X}\n")
				.Append($"  Uncompressed: 0x{UncompressedSize:X}\n")
				.Append($"  Disk Number: {uint_0}\n")
				.Append($"  Relative Offset: 0x{long_4:X}\n")
				.Append($"  Bit Field: 0x{short_1:X4}\n")
				.Append($"  Encrypted?: {bool_8}\n")
				.Append($"  Timeblob: 0x{int_0:X8} ({Class7.smethod_15(int_0)})\n")
				.Append($"  CRC: 0x{int_1:X8}\n")
				.Append($"  Is Text?: {bool_14}\n")
				.Append($"  Is Directory?: {bool_4}\n")
				.Append($"  Is Zip64?: {bool_10}\n");
			if (!string.IsNullOrEmpty(string_2))
			{
				stringBuilder.Append($"  Comment: {string_2}\n");
			}
			return stringBuilder.ToString();
		}
	}

	private string UnsupportedAlgorithm
	{
		get
		{
			string empty = string.Empty;
			return uint_1 switch
			{
				26113u => "DES", 
				26114u => "RC2", 
				26115u => "3DES-168", 
				0u => "--", 
				26126u => "PKWare AES128", 
				26127u => "PKWare AES192", 
				26128u => "PKWare AES256", 
				26121u => "3DES-112", 
				26400u => "Blowfish", 
				26401u => "Twofish", 
				26370u => "RC2", 
				26625u => "RC4", 
				_ => $"Unknown (0x{uint_1:X4})", 
			};
		}
	}

	private string UnsupportedCompressionMethod
	{
		get
		{
			string empty = string.Empty;
			return short_2 switch
			{
				8 => "DEFLATE", 
				9 => "Deflate64", 
				0 => "Store", 
				1 => "Shrink", 
				98 => "PPMd", 
				19 => "LZ77", 
				14 => "LZMA", 
				_ => $"Unknown (0x{short_2:X4})", 
			};
		}
	}

	public ZipEntry()
	{
		short_2 = 8;
		compressionLevel_0 = CompressionLevel.Default;
		encryptionAlgorithm_0 = EncryptionAlgorithm.None;
		zipEntrySource_0 = ZipEntrySource.None;
	}

	public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified)
	{
		bool_0 = true;
		if (created == dateTime_6 && created.Kind == dateTime_6.Kind)
		{
			created = dateTime_5;
		}
		if (accessed == dateTime_6 && accessed.Kind == dateTime_6.Kind)
		{
			accessed = dateTime_5;
		}
		if (modified == dateTime_6 && modified.Kind == dateTime_6.Kind)
		{
			modified = dateTime_5;
		}
		dateTime_3 = created.ToUniversalTime();
		dateTime_2 = accessed.ToUniversalTime();
		dateTime_1 = modified.ToUniversalTime();
		dateTime_0 = dateTime_1;
		if (!bool_2 && !bool_1)
		{
			bool_1 = true;
		}
		bool_6 = true;
	}

	internal static string smethod_0(string string_4, string string_5)
	{
		string text = null;
		text = ((string_5 == null) ? string_4 : ((!string.IsNullOrEmpty(string_5)) ? Path.Combine(string_5, Path.GetFileName(string_4)) : Path.GetFileName(string_4)));
		return Class7.smethod_3(text);
	}

	internal static ZipEntry smethod_1(string string_4)
	{
		return smethod_7(string_4, ZipEntrySource.None, null, null);
	}

	internal static ZipEntry smethod_2(string string_4, string string_5)
	{
		return smethod_7(string_5, ZipEntrySource.FileSystem, string_4, null);
	}

	internal static ZipEntry smethod_3(string string_4, Stream stream_3)
	{
		return smethod_7(string_4, ZipEntrySource.Stream, stream_3, null);
	}

	internal static ZipEntry smethod_4(string string_4, WriteDelegate writeDelegate_1)
	{
		return smethod_7(string_4, ZipEntrySource.WriteDelegate, writeDelegate_1, null);
	}

	internal static ZipEntry smethod_5(string string_4, OpenDelegate openDelegate_1, CloseDelegate closeDelegate_1)
	{
		return smethod_7(string_4, ZipEntrySource.JitStream, openDelegate_1, closeDelegate_1);
	}

	internal static ZipEntry smethod_6(string string_4)
	{
		return smethod_7(string_4, ZipEntrySource.ZipOutputStream, null, null);
	}

	private static ZipEntry smethod_7(string string_4, ZipEntrySource zipEntrySource_1, object object_1, object object_2)
	{
		if (string.IsNullOrEmpty(string_4))
		{
			throw new ZipException("The entry name must be non-null and non-empty.");
		}
		ZipEntry zipEntry = new ZipEntry();
		zipEntry.short_4 = 45;
		zipEntry.zipEntrySource_0 = zipEntrySource_1;
		zipEntry.dateTime_1 = (zipEntry.dateTime_2 = (zipEntry.dateTime_3 = DateTime.UtcNow));
		switch (zipEntrySource_1)
		{
		case ZipEntrySource.Stream:
			zipEntry.stream_1 = object_1 as Stream;
			break;
		case ZipEntrySource.WriteDelegate:
			zipEntry.writeDelegate_0 = object_1 as WriteDelegate;
			break;
		case ZipEntrySource.JitStream:
			zipEntry.openDelegate_0 = object_1 as OpenDelegate;
			zipEntry.closeDelegate_0 = object_2 as CloseDelegate;
			break;
		case ZipEntrySource.None:
			zipEntry.zipEntrySource_0 = ZipEntrySource.FileSystem;
			break;
		default:
		{
			string text = object_1 as string;
			if (string.IsNullOrEmpty(text))
			{
				throw new ZipException("The filename must be non-null and non-empty.");
			}
			zipEntry.dateTime_1 = File.GetLastWriteTimeUtc(text);
			zipEntry.dateTime_3 = File.GetCreationTimeUtc(text);
			zipEntry.dateTime_2 = File.GetLastAccessTimeUtc(text);
			if (File.Exists(text) || Directory.Exists(text))
			{
				zipEntry.int_4 = (int)File.GetAttributes(text);
			}
			zipEntry.bool_0 = true;
			zipEntry.string_0 = Path.GetFullPath(text);
			break;
		}
		case ZipEntrySource.ZipOutputStream:
			break;
		}
		zipEntry.dateTime_0 = zipEntry.dateTime_1;
		zipEntry.string_1 = Class7.smethod_3(string_4);
		return zipEntry;
	}

	internal void method_0()
	{
		bool_4 = true;
		if (!string_1.EndsWith("/"))
		{
			string_1 += "/";
		}
	}

	public override string ToString()
	{
		return $"ZipEntry::{FileName}";
	}

	private void method_1()
	{
		long position = ArchiveStream.Position;
		try
		{
			ArchiveStream.Seek(long_4, SeekOrigin.Begin);
		}
		catch (IOException innerException)
		{
			string message = $"Exception seeking  entry({FileName}) offset(0x{long_4:X8}) len(0x{ArchiveStream.Length:X8})";
			throw new BadStateException(message, innerException);
		}
		byte[] array = new byte[30];
		ArchiveStream.Read(array, 0, array.Length);
		short num = (short)(array[26] + array[27] * 256);
		short num2 = (short)(array[28] + array[29] * 256);
		ArchiveStream.Seek(num + num2, SeekOrigin.Current);
		int_2 = 30 + num2 + num + smethod_8(encryptionAlgorithm_1);
		long_3 = long_4 + int_2;
		ArchiveStream.Seek(position, SeekOrigin.Begin);
	}

	internal static int smethod_8(EncryptionAlgorithm encryptionAlgorithm_2)
	{
		return encryptionAlgorithm_2 switch
		{
			EncryptionAlgorithm.None => 0, 
			EncryptionAlgorithm.PkzipWeak => 12, 
			_ => throw new ZipException("internal error"), 
		};
	}

	internal void method_2(Stream stream_3)
	{
		method_3(stream_3);
	}

	private void method_3(Stream stream_3)
	{
		byte[] array = new byte[4096];
		int num = 0;
		num = 1;
		array[0] = 80;
		num = 2;
		array[1] = 75;
		num = 3;
		array[2] = 1;
		num = 4;
		array[3] = 2;
		num = 5;
		array[4] = (byte)((uint)short_4 & 0xFFu);
		num = 6;
		array[5] = (byte)((short_4 & 0xFF00) >> 8);
		short num2 = (short)(nullable_2.Value ? 45 : 20);
		array[num++] = (byte)((uint)num2 & 0xFFu);
		array[num++] = (byte)((num2 & 0xFF00) >> 8);
		array[num++] = (byte)((uint)short_1 & 0xFFu);
		array[num++] = (byte)((short_1 & 0xFF00) >> 8);
		array[num++] = (byte)((uint)short_2 & 0xFFu);
		array[num++] = (byte)((short_2 & 0xFF00) >> 8);
		array[num++] = (byte)((uint)int_0 & 0xFFu);
		array[num++] = (byte)((int_0 & 0xFF00) >> 8);
		array[num++] = (byte)((int_0 & 0xFF0000) >> 16);
		array[num++] = (byte)((int_0 & 0xFF000000L) >> 24);
		array[num++] = (byte)((uint)int_1 & 0xFFu);
		array[num++] = (byte)((int_1 & 0xFF00) >> 8);
		array[num++] = (byte)((int_1 & 0xFF0000) >> 16);
		array[num++] = (byte)((int_1 & 0xFF000000L) >> 24);
		int num3 = 0;
		if (nullable_2.Value)
		{
			for (num3 = 0; num3 < 8; num3++)
			{
				array[num++] = byte.MaxValue;
			}
		}
		else
		{
			array[num++] = (byte)((ulong)long_0 & 0xFFuL);
			array[num++] = (byte)((long_0 & 0xFF00L) >> 8);
			array[num++] = (byte)((long_0 & 0xFF0000L) >> 16);
			array[num++] = (byte)((long_0 & 0xFF000000L) >> 24);
			array[num++] = (byte)((ulong)long_2 & 0xFFuL);
			array[num++] = (byte)((long_2 & 0xFF00L) >> 8);
			array[num++] = (byte)((long_2 & 0xFF0000L) >> 16);
			array[num++] = (byte)((long_2 & 0xFF000000L) >> 24);
		}
		byte[] array2 = method_6();
		short num4 = (short)array2.Length;
		array[num++] = (byte)((uint)num4 & 0xFFu);
		array[num++] = (byte)((num4 & 0xFF00) >> 8);
		bool_13 = nullable_2.Value;
		byte_1 = method_4(bool_15: true);
		short num5 = (short)((byte_1 != null) ? byte_1.Length : 0);
		array[num++] = (byte)((uint)num5 & 0xFFu);
		array[num++] = (byte)((num5 & 0xFF00) >> 8);
		int num6 = ((byte_0 != null) ? byte_0.Length : 0);
		if (num6 + num > array.Length)
		{
			num6 = array.Length - num;
		}
		array[num++] = (byte)((uint)num6 & 0xFFu);
		array[num++] = (byte)((num6 & 0xFF00) >> 8);
		array[num++] = (byte)(uint_0 & 0xFFu);
		array[num++] = (byte)((uint_0 & 0xFF00) >> 8);
		array[num++] = (byte)(bool_14 ? 1u : 0u);
		array[num++] = 0;
		array[num++] = (byte)((uint)int_4 & 0xFFu);
		array[num++] = (byte)((int_4 & 0xFF00) >> 8);
		array[num++] = (byte)((int_4 & 0xFF0000) >> 16);
		array[num++] = (byte)((int_4 & 0xFF000000L) >> 24);
		if (nullable_2.Value)
		{
			for (num3 = 0; num3 < 4; num3++)
			{
				array[num++] = byte.MaxValue;
			}
		}
		else
		{
			array[num++] = (byte)((ulong)long_4 & 0xFFuL);
			array[num++] = (byte)((long_4 & 0xFF00L) >> 8);
			array[num++] = (byte)((long_4 & 0xFF0000L) >> 16);
			array[num++] = (byte)((long_4 & 0xFF000000L) >> 24);
		}
		for (num3 = 0; num3 < num4; num3++)
		{
			array[num + num3] = array2[num3];
		}
		num += num3;
		if (byte_1 != null)
		{
			for (num3 = 0; num3 < num5; num3++)
			{
				array[num + num3] = byte_1[num3];
			}
			num += num3;
		}
		if (num6 != 0)
		{
			for (num3 = 0; num3 < num6 && num + num3 < array.Length; num3++)
			{
				array[num + num3] = byte_0[num3];
			}
			num += num3;
		}
		stream_3.Write(array, 0, num);
	}

	private byte[] method_4(bool bool_15)
	{
		List<byte[]> list = new List<byte[]>();
		if (class31_0.method_4() == Zip64Option.Always || (class31_0.method_4() == Zip64Option.AsNecessary && (!bool_15 || nullable_1.Value)))
		{
			int num = 4 + (bool_15 ? 28 : 16);
			byte[] array = new byte[num];
			int num2 = 0;
			if (!bool_13 && !bool_15)
			{
				array[num2++] = 153;
				array[num2++] = 153;
			}
			else
			{
				array[num2++] = 1;
				array[num2++] = 0;
			}
			array[num2++] = (byte)(num - 4);
			array[num2++] = 0;
			Array.Copy(BitConverter.GetBytes(long_2), 0, array, num2, 8);
			num2 += 8;
			Array.Copy(BitConverter.GetBytes(long_0), 0, array, num2, 8);
			if (bool_15)
			{
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(long_4), 0, array, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(0), 0, array, num2, 4);
			}
			list.Add(array);
		}
		if (bool_0 && bool_1)
		{
			byte[] array = new byte[36]
			{
				10, 0, 32, 0, 0, 0, 0, 0, 1, 0,
				24, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0
			};
			long value = dateTime_1.ToFileTime();
			Array.Copy(BitConverter.GetBytes(value), 0, array, 12, 8);
			value = dateTime_2.ToFileTime();
			Array.Copy(BitConverter.GetBytes(value), 0, array, 20, 8);
			value = dateTime_3.ToFileTime();
			Array.Copy(BitConverter.GetBytes(value), 0, array, 28, 8);
			list.Add(array);
		}
		if (bool_0 && bool_2)
		{
			int num3 = 9;
			if (!bool_15)
			{
				num3 += 8;
			}
			byte[] array = new byte[num3];
			int num4 = 0;
			byte[] array2 = array;
			num4 = 1;
			array2[0] = 85;
			byte[] array3 = array;
			num4 = 2;
			array3[1] = 84;
			byte[] array4 = array;
			num4 = 3;
			array4[2] = (byte)(num3 - 4);
			byte[] array5 = array;
			num4 = 4;
			array5[3] = 0;
			byte[] array6 = array;
			num4 = 5;
			array6[4] = 7;
			int value2 = (int)(dateTime_1 - dateTime_4).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(value2), 0, array, 5, 4);
			num4 = 9;
			if (!bool_15)
			{
				value2 = (int)(dateTime_2 - dateTime_4).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(value2), 0, array, num4, 4);
				num4 += 4;
				value2 = (int)(dateTime_3 - dateTime_4).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(value2), 0, array, num4, 4);
				num4 += 4;
			}
			list.Add(array);
		}
		byte[] array7 = null;
		if (list.Count > 0)
		{
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				num5 += list[i].Length;
			}
			array7 = new byte[num5];
			for (int i = 0; i < list.Count; i++)
			{
				Array.Copy(list[i], 0, array7, num6, list[i].Length);
				num6 += list[i].Length;
			}
		}
		return array7;
	}

	private Encoding method_5()
	{
		byte_0 = encoding_0.GetBytes(string_2);
		string @string = encoding_0.GetString(byte_0, 0, byte_0.Length);
		if (@string == string_2)
		{
			return encoding_0;
		}
		byte_0 = encoding_1.GetBytes(string_2);
		return encoding_1;
	}

	private byte[] method_6()
	{
		string text = FileName.Replace("\\", "/");
		string text2 = null;
		if (bool_3 && FileName.Length >= 3 && FileName[1] == ':' && text[2] == '/')
		{
			text2 = text.Substring(3);
		}
		else if (FileName.Length < 4 || text[0] != '/' || text[1] != '/')
		{
			text2 = ((FileName.Length < 3 || text[0] != '.' || text[1] != '/') ? text : text.Substring(2));
		}
		else
		{
			int num = text.IndexOf('/', 2);
			if (num == -1)
			{
				throw new ArgumentException("The path for that entry appears to be badly formatted");
			}
			text2 = text.Substring(num + 1);
		}
		byte[] bytes = encoding_0.GetBytes(text2);
		string @string = encoding_0.GetString(bytes, 0, bytes.Length);
		byte_0 = null;
		if (@string == text2)
		{
			if (string_2 != null && string_2.Length != 0)
			{
				Encoding encoding = method_5();
				if (encoding.CodePage == 437)
				{
					encoding_2 = encoding_0;
					return bytes;
				}
				encoding_2 = encoding;
				return encoding.GetBytes(text2);
			}
			encoding_2 = encoding_0;
			return bytes;
		}
		bytes = encoding_1.GetBytes(text2);
		if (string_2 != null && string_2.Length != 0)
		{
			byte_0 = encoding_1.GetBytes(string_2);
		}
		encoding_2 = encoding_1;
		return bytes;
	}

	private bool method_7()
	{
		if (long_2 < 16L)
		{
			return false;
		}
		if (short_2 == 0)
		{
			return false;
		}
		if (CompressionLevel == CompressionLevel.None)
		{
			return false;
		}
		if (long_0 < long_2)
		{
			return false;
		}
		if (zipEntrySource_0 == ZipEntrySource.Stream && !stream_1.CanSeek)
		{
			return false;
		}
		if (class6_1 != null && CompressedSize - 12L <= UncompressedSize)
		{
			return false;
		}
		return true;
	}

	private void method_8(int int_6)
	{
		if (int_6 > 1)
		{
			short_2 = 0;
		}
		else if (IsDirectory)
		{
			short_2 = 0;
		}
		else
		{
			if (zipEntrySource_0 == ZipEntrySource.ZipFile)
			{
				return;
			}
			if (zipEntrySource_0 == ZipEntrySource.Stream)
			{
				if (stream_1 != null && stream_1.CanSeek)
				{
					long length = stream_1.Length;
					if (length == 0L)
					{
						short_2 = 0;
						return;
					}
				}
			}
			else if (zipEntrySource_0 == ZipEntrySource.FileSystem && Class7.smethod_0(LocalFileName) == 0L)
			{
				short_2 = 0;
				return;
			}
			if (SetCompression != null)
			{
				CompressionLevel = SetCompression(LocalFileName, string_1);
			}
			short_2 = (short)((CompressionLevel != 0) ? 8 : 0);
		}
	}

	internal void method_9(Stream stream_3, int int_6)
	{
		long_5 = (stream_3 as Stream2)?.method_4() ?? stream_3.Position;
		int num = 0;
		int num2 = 0;
		byte[] array = new byte[512];
		num2 = 1;
		array[0] = 80;
		num2 = 2;
		array[1] = 75;
		num2 = 3;
		array[2] = 3;
		num2 = 4;
		array[3] = 4;
		bool_13 = class31_0.method_4() == Zip64Option.Always || (class31_0.method_4() == Zip64Option.AsNecessary && !stream_3.CanSeek);
		short num3 = (short)(bool_13 ? 45 : 20);
		array[num2++] = (byte)((uint)num3 & 0xFFu);
		array[num2++] = (byte)((num3 & 0xFF00) >> 8);
		byte[] array2 = method_6();
		short num4 = (short)array2.Length;
		if (encryptionAlgorithm_0 == EncryptionAlgorithm.None)
		{
			short_1 &= -2;
		}
		else
		{
			short_1 |= 1;
		}
		if (ActualEncoding.CodePage == Encoding.UTF8.CodePage)
		{
			short_1 |= 2048;
		}
		if (!IsDirectory && int_6 != 99)
		{
			if (!stream_3.CanSeek)
			{
				short_1 |= 8;
			}
		}
		else
		{
			short_1 &= -9;
			short_1 &= -2;
			Encryption = EncryptionAlgorithm.None;
			Password = null;
		}
		array[num2++] = (byte)((uint)short_1 & 0xFFu);
		array[num2++] = (byte)((short_1 & 0xFF00) >> 8);
		if (long_3 == -1L)
		{
			long_0 = 0L;
			bool_5 = false;
		}
		method_8(int_6);
		array[num2++] = (byte)((uint)short_2 & 0xFFu);
		array[num2++] = (byte)((short_2 & 0xFF00) >> 8);
		if (int_6 == 99)
		{
			method_18();
		}
		int_0 = Class7.smethod_16(LastModified);
		array[num2++] = (byte)((uint)int_0 & 0xFFu);
		array[num2++] = (byte)((int_0 & 0xFF00) >> 8);
		array[num2++] = (byte)((int_0 & 0xFF0000) >> 16);
		array[num2++] = (byte)((int_0 & 0xFF000000L) >> 24);
		array[num2++] = (byte)((uint)int_1 & 0xFFu);
		array[num2++] = (byte)((int_1 & 0xFF00) >> 8);
		array[num2++] = (byte)((int_1 & 0xFF0000) >> 16);
		array[num2++] = (byte)((int_1 & 0xFF000000L) >> 24);
		if (bool_13)
		{
			for (num = 0; num < 8; num++)
			{
				array[num2++] = byte.MaxValue;
			}
		}
		else
		{
			array[num2++] = (byte)((ulong)long_0 & 0xFFuL);
			array[num2++] = (byte)((long_0 & 0xFF00L) >> 8);
			array[num2++] = (byte)((long_0 & 0xFF0000L) >> 16);
			array[num2++] = (byte)((long_0 & 0xFF000000L) >> 24);
			array[num2++] = (byte)((ulong)long_2 & 0xFFuL);
			array[num2++] = (byte)((long_2 & 0xFF00L) >> 8);
			array[num2++] = (byte)((long_2 & 0xFF0000L) >> 16);
			array[num2++] = (byte)((long_2 & 0xFF000000L) >> 24);
		}
		array[num2++] = (byte)((uint)num4 & 0xFFu);
		array[num2++] = (byte)((num4 & 0xFF00) >> 8);
		byte_1 = method_4(bool_15: false);
		short num5 = (short)((byte_1 != null) ? byte_1.Length : 0);
		array[num2++] = (byte)((uint)num5 & 0xFFu);
		array[num2++] = (byte)((num5 & 0xFF00) >> 8);
		for (num = 0; num < array2.Length && num2 + num < array.Length; num++)
		{
			array[num2 + num] = array2[num];
		}
		num2 += num;
		if (byte_1 != null)
		{
			for (num = 0; num < byte_1.Length; num++)
			{
				array[num2 + num] = byte_1[num];
			}
			num2 += num;
		}
		int_2 = num2;
		Stream0 stream = stream_3 as Stream0;
		if (stream != null)
		{
			stream.method_2(bool_1: true);
			uint num6 = stream.method_7(num2);
			if (num6 != stream.method_3())
			{
				long_5 = 0L;
			}
			else
			{
				long_5 = stream.Position;
			}
			uint_0 = num6;
		}
		if (class31_0.method_4() == Zip64Option.Default && (uint)long_4 >= uint.MaxValue)
		{
			throw new ZipException("Offset within the zip archive exceeds 0xFFFFFFFF. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
		}
		stream_3.Write(array, 0, num2);
		stream?.method_2(bool_1: false);
		byte_2 = new byte[num2];
		for (num = 0; num < num2; num++)
		{
			byte_2[num] = array[num];
		}
	}

	private int method_10()
	{
		if (!bool_5)
		{
			Stream stream = null;
			if (zipEntrySource_0 == ZipEntrySource.WriteDelegate)
			{
				CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(Stream.Null);
				writeDelegate_0(FileName, crcCalculatorStream);
				int_1 = crcCalculatorStream.Crc;
			}
			else if (zipEntrySource_0 != ZipEntrySource.ZipFile)
			{
				if (zipEntrySource_0 == ZipEntrySource.Stream)
				{
					method_11();
					stream = stream_1;
				}
				else if (zipEntrySource_0 == ZipEntrySource.JitStream)
				{
					if (stream_1 == null)
					{
						stream_1 = openDelegate_0(FileName);
					}
					method_11();
					stream = stream_1;
				}
				else if (zipEntrySource_0 != ZipEntrySource.ZipOutputStream)
				{
					stream = File.Open(LocalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				}
				CRC32 cRC = new CRC32();
				int_1 = cRC.GetCrc32(stream);
				if (stream_1 == null)
				{
					stream.Close();
					stream.Dispose();
				}
			}
			bool_5 = true;
		}
		return int_1;
	}

	private void method_11()
	{
		if (stream_1 == null)
		{
			throw new ZipException($"The input stream is null for entry '{FileName}'.");
		}
		if (nullable_0.HasValue)
		{
			stream_1.Position = nullable_0.Value;
		}
		else if (stream_1.CanSeek)
		{
			nullable_0 = stream_1.Position;
		}
		else if (Encryption == EncryptionAlgorithm.PkzipWeak && zipEntrySource_0 != ZipEntrySource.ZipFile && (short_1 & 8) != 8)
		{
			throw new ZipException("It is not possible to use PKZIP encryption on a non-seekable input stream");
		}
	}

	internal void method_12(ZipEntry zipEntry_0)
	{
		long_3 = zipEntry_0.long_3;
		CompressionMethod = zipEntry_0.CompressionMethod;
		short_3 = zipEntry_0.short_3;
		long_1 = zipEntry_0.long_1;
		long_2 = zipEntry_0.long_2;
		short_1 = zipEntry_0.short_1;
		zipEntrySource_0 = zipEntry_0.zipEntrySource_0;
		dateTime_0 = zipEntry_0.dateTime_0;
		dateTime_1 = zipEntry_0.dateTime_1;
		dateTime_2 = zipEntry_0.dateTime_2;
		dateTime_3 = zipEntry_0.dateTime_3;
		bool_0 = zipEntry_0.bool_0;
		bool_2 = zipEntry_0.bool_2;
		bool_1 = zipEntry_0.bool_1;
	}

	private void method_13(long long_7, long long_8)
	{
		if (class31_0.method_0() != null)
		{
			bool_12 = class31_0.method_0().method_3(this, long_7, long_8);
		}
	}

	private void method_14(Stream stream_3)
	{
		Stream stream_4 = null;
		long num = -1L;
		try
		{
			num = stream_3.Position;
		}
		catch
		{
		}
		try
		{
			long num2 = method_15(ref stream_4);
			method_19(stream_3, num2, out var stream2_, out var stream_5, out var stream_6, out var crcCalculatorStream_);
			if (zipEntrySource_0 == ZipEntrySource.WriteDelegate)
			{
				writeDelegate_0(FileName, crcCalculatorStream_);
			}
			else
			{
				byte[] array = new byte[BufferSize];
				int count;
				while ((count = Class7.smethod_19(stream_4, array, 0, array.Length, FileName)) != 0)
				{
					crcCalculatorStream_.Write(array, 0, count);
					method_13(crcCalculatorStream_.TotalBytesSlurped, num2);
					if (bool_12)
					{
						break;
					}
				}
			}
			method_16(stream_3, stream2_, stream_5, stream_6, crcCalculatorStream_);
		}
		finally
		{
			if (zipEntrySource_0 == ZipEntrySource.JitStream)
			{
				if (closeDelegate_0 != null)
				{
					closeDelegate_0(FileName, stream_4);
				}
			}
			else if (stream_4 is FileStream)
			{
				stream_4.Close();
				stream_4.Dispose();
			}
		}
		if (!bool_12)
		{
			long_3 = num;
			method_17(stream_3);
		}
	}

	private long method_15(ref Stream stream_3)
	{
		long result = -1L;
		if (zipEntrySource_0 == ZipEntrySource.Stream)
		{
			method_11();
			stream_3 = stream_1;
			try
			{
				result = stream_1.Length;
			}
			catch (NotSupportedException)
			{
			}
		}
		else if (zipEntrySource_0 == ZipEntrySource.ZipFile)
		{
			string string_ = ((encryptionAlgorithm_1 == EncryptionAlgorithm.None) ? null : (string_3 ?? class31_0.method_3()));
			stream_1 = method_33(string_);
			method_11();
			stream_3 = stream_1;
			result = stream_1.Length;
		}
		else if (zipEntrySource_0 == ZipEntrySource.JitStream)
		{
			if (stream_1 == null)
			{
				stream_1 = openDelegate_0(FileName);
			}
			method_11();
			stream_3 = stream_1;
			try
			{
				result = stream_1.Length;
			}
			catch (NotSupportedException)
			{
			}
		}
		else if (zipEntrySource_0 == ZipEntrySource.FileSystem)
		{
			stream_3 = File.Open(LocalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
			result = stream_3.Length;
		}
		return result;
	}

	internal void method_16(Stream stream_3, Stream2 stream2_0, Stream stream_4, Stream stream_5, CrcCalculatorStream crcCalculatorStream_0)
	{
		if (crcCalculatorStream_0 != null)
		{
			crcCalculatorStream_0.Close();
			if (stream_5 is DeflateStream)
			{
				stream_5.Close();
			}
			else if (stream_5 is ParallelDeflateOutputStream)
			{
				stream_5.Close();
			}
			stream_4.Flush();
			stream_4.Close();
			int_3 = 0;
			long_2 = crcCalculatorStream_0.TotalBytesSlurped;
			long_1 = stream2_0.method_1();
			long_0 = long_1;
			int_1 = crcCalculatorStream_0.Crc;
			method_24();
		}
	}

	internal void method_17(Stream stream_3)
	{
		if (long_2 == 0L && long_0 == 0L)
		{
			if (zipEntrySource_0 == ZipEntrySource.ZipOutputStream)
			{
				return;
			}
			if (string_3 != null)
			{
				int num = 0;
				if (Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					num = 12;
				}
				if (zipEntrySource_0 == ZipEntrySource.ZipOutputStream && !stream_3.CanSeek)
				{
					throw new ZipException("Zero bytes written, encryption in use, and non-seekable output.");
				}
				if (Encryption != 0)
				{
					stream_3.Seek(-1 * num, SeekOrigin.Current);
					stream_3.SetLength(stream_3.Position);
					int_2 -= num;
				}
				string_3 = null;
				short_1 &= -2;
				byte_2[6] = (byte)((uint)short_1 & 0xFFu);
				byte_2[7] = (byte)((short_1 & 0xFF00) >> 8);
			}
			CompressionMethod = CompressionMethod.None;
			Encryption = EncryptionAlgorithm.None;
		}
		else if (class6_1 != null && Encryption == EncryptionAlgorithm.PkzipWeak)
		{
			long_0 += 12L;
		}
		int num2 = 8;
		byte[] array = byte_2;
		num2 = 9;
		array[8] = (byte)((uint)short_2 & 0xFFu);
		byte[] array2 = byte_2;
		num2 = 10;
		array2[9] = (byte)((short_2 & 0xFF00) >> 8);
		num2 = 14;
		byte[] array3 = byte_2;
		num2 = 15;
		array3[14] = (byte)((uint)int_1 & 0xFFu);
		byte[] array4 = byte_2;
		num2 = 16;
		array4[15] = (byte)((int_1 & 0xFF00) >> 8);
		byte[] array5 = byte_2;
		num2 = 17;
		array5[16] = (byte)((int_1 & 0xFF0000) >> 16);
		byte[] array6 = byte_2;
		num2 = 18;
		array6[17] = (byte)((int_1 & 0xFF000000L) >> 24);
		method_18();
		short num3 = (short)(byte_2[26] + byte_2[27] * 256);
		short num4 = (short)(byte_2[28] + byte_2[29] * 256);
		if (nullable_2.Value)
		{
			byte_2[4] = 45;
			byte_2[5] = 0;
			for (int i = 0; i < 8; i++)
			{
				byte_2[num2++] = byte.MaxValue;
			}
			num2 = 30 + num3;
			byte_2[num2++] = 1;
			byte_2[num2++] = 0;
			num2 += 2;
			Array.Copy(BitConverter.GetBytes(long_2), 0, byte_2, num2, 8);
			num2 += 8;
			Array.Copy(BitConverter.GetBytes(long_0), 0, byte_2, num2, 8);
		}
		else
		{
			byte_2[4] = 20;
			byte_2[5] = 0;
			num2 = 18;
			byte[] array7 = byte_2;
			num2 = 19;
			array7[18] = (byte)((ulong)long_0 & 0xFFuL);
			byte[] array8 = byte_2;
			num2 = 20;
			array8[19] = (byte)((long_0 & 0xFF00L) >> 8);
			byte[] array9 = byte_2;
			num2 = 21;
			array9[20] = (byte)((long_0 & 0xFF0000L) >> 16);
			byte[] array10 = byte_2;
			num2 = 22;
			array10[21] = (byte)((long_0 & 0xFF000000L) >> 24);
			byte[] array11 = byte_2;
			num2 = 23;
			array11[22] = (byte)((ulong)long_2 & 0xFFuL);
			byte[] array12 = byte_2;
			num2 = 24;
			array12[23] = (byte)((long_2 & 0xFF00L) >> 8);
			byte[] array13 = byte_2;
			num2 = 25;
			array13[24] = (byte)((long_2 & 0xFF0000L) >> 16);
			byte[] array14 = byte_2;
			num2 = 26;
			array14[25] = (byte)((long_2 & 0xFF000000L) >> 24);
			if (num4 != 0)
			{
				num2 = 30 + num3;
				short num5 = (short)(byte_2[num2 + 2] + byte_2[num2 + 3] * 256);
				if (num5 == 16)
				{
					byte_2[num2++] = 153;
					byte_2[num2++] = 153;
				}
			}
		}
		if ((short_1 & 8) != 8 || (zipEntrySource_0 == ZipEntrySource.ZipOutputStream && stream_3.CanSeek))
		{
			if (stream_3 is Stream0 stream && uint_0 != stream.method_3())
			{
				using Stream stream2 = Stream0.smethod_2(class31_0.method_0().Name, uint_0);
				stream2.Seek(long_4, SeekOrigin.Begin);
				stream2.Write(byte_2, 0, byte_2.Length);
			}
			else
			{
				stream_3.Seek(long_4, SeekOrigin.Begin);
				stream_3.Write(byte_2, 0, byte_2.Length);
				if (stream_3 is Stream2 stream3)
				{
					stream3.method_3(byte_2.Length);
				}
				stream_3.Seek(long_0, SeekOrigin.Current);
			}
		}
		if ((short_1 & 8) == 8 && !IsDirectory)
		{
			byte[] array15 = new byte[16 + (nullable_2.Value ? 8 : 0)];
			num2 = 0;
			Array.Copy(BitConverter.GetBytes(134695760), 0, array15, 0, 4);
			num2 = 4;
			Array.Copy(BitConverter.GetBytes(int_1), 0, array15, 4, 4);
			num2 = 8;
			if (nullable_2.Value)
			{
				Array.Copy(BitConverter.GetBytes(long_0), 0, array15, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(long_2), 0, array15, num2, 8);
				num2 += 8;
			}
			else
			{
				array15[num2++] = (byte)((ulong)long_0 & 0xFFuL);
				array15[num2++] = (byte)((long_0 & 0xFF00L) >> 8);
				array15[num2++] = (byte)((long_0 & 0xFF0000L) >> 16);
				array15[num2++] = (byte)((long_0 & 0xFF000000L) >> 24);
				array15[num2++] = (byte)((ulong)long_2 & 0xFFuL);
				array15[num2++] = (byte)((long_2 & 0xFF00L) >> 8);
				array15[num2++] = (byte)((long_2 & 0xFF0000L) >> 16);
				array15[num2++] = (byte)((long_2 & 0xFF000000L) >> 24);
			}
			stream_3.Write(array15, 0, array15.Length);
			int_3 += array15.Length;
		}
	}

	private void method_18()
	{
		nullable_1 = long_0 >= 4294967295L || long_2 >= 4294967295L || long_4 >= 4294967295L;
		if (class31_0.method_4() == Zip64Option.Default && nullable_1.Value)
		{
			throw new ZipException("Compressed or Uncompressed size, or offset exceeds the maximum value. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
		}
		nullable_2 = class31_0.method_4() == Zip64Option.Always || nullable_1.Value;
	}

	internal void method_19(Stream stream_3, long long_7, out Stream2 stream2_0, out Stream stream_4, out Stream stream_5, out CrcCalculatorStream crcCalculatorStream_0)
	{
		stream2_0 = new Stream2(stream_3);
		if (long_7 != 0L)
		{
			stream_4 = method_21(stream2_0);
			stream_5 = method_20(stream_4, long_7);
		}
		else
		{
			stream_4 = (stream_5 = stream2_0);
		}
		crcCalculatorStream_0 = new CrcCalculatorStream(stream_5, leaveOpen: true);
	}

	private Stream method_20(Stream stream_3, long long_7)
	{
		if (short_2 == 8 && CompressionLevel != 0)
		{
			if (class31_0.method_8() != 0L && (long_7 <= class31_0.method_8() || class31_0.method_8() <= 0L))
			{
				DeflateStream deflateStream = new DeflateStream(stream_3, CompressionMode.Compress, CompressionLevel, leaveOpen: true);
				if (class31_0.method_9() > 0)
				{
					deflateStream.BufferSize = class31_0.method_9();
				}
				deflateStream.Strategy = class31_0.method_10();
				return deflateStream;
			}
			if (class31_0.method_6() == null)
			{
				class31_0.method_7(new ParallelDeflateOutputStream(stream_3, CompressionLevel, class31_0.method_10(), leaveOpen: true));
				if (class31_0.method_9() > 0)
				{
					class31_0.method_6().BufferSize = class31_0.method_9();
				}
			}
			ParallelDeflateOutputStream parallelDeflateOutputStream = class31_0.method_6();
			parallelDeflateOutputStream.Reset(stream_3);
			return parallelDeflateOutputStream;
		}
		return stream_3;
	}

	private Stream method_21(Stream stream_3)
	{
		if (Encryption == EncryptionAlgorithm.PkzipWeak)
		{
			return new Stream1(stream_3, class6_1, Enum5.const_0);
		}
		return stream_3;
	}

	private void method_22(Exception exception_0)
	{
		if (class31_0.method_0() != null)
		{
			bool_12 = class31_0.method_0().method_21(this, exception_0);
		}
	}

	internal void method_23(Stream stream_3)
	{
		bool flag = false;
		while (zipEntrySource_0 != ZipEntrySource.ZipFile || bool_7)
		{
			try
			{
				if (IsDirectory)
				{
					method_9(stream_3, 1);
					method_24();
					nullable_1 = long_4 >= 4294967295L;
					nullable_2 = class31_0.method_4() == Zip64Option.Always || nullable_1.Value;
					if (stream_3 is Stream0 stream)
					{
						uint_0 = stream.method_3();
					}
					return;
				}
				bool flag2 = true;
				int num = 0;
				do
				{
					num++;
					method_9(stream_3, num);
					method_26(stream_3);
					flag2 = num <= 1 && stream_3.CanSeek && method_7();
					if (flag2)
					{
						if (stream_3 is Stream0 stream2)
						{
							stream2.method_11(uint_0, long_4);
						}
						else
						{
							stream_3.Seek(long_4, SeekOrigin.Begin);
						}
						stream_3.SetLength(stream_3.Position);
						if (stream_3 is Stream2 stream3)
						{
							stream3.method_3(long_6);
						}
					}
				}
				while (flag2);
				bool_9 = false;
				flag = true;
			}
			catch (Exception ex)
			{
				ZipErrorAction zipErrorAction = ZipErrorAction;
				int num2 = 0;
				while (true)
				{
					if (ZipErrorAction != 0)
					{
						if (ZipErrorAction != ZipErrorAction.Skip && ZipErrorAction != ZipErrorAction.Retry)
						{
							if (num2 <= 0)
							{
								if (ZipErrorAction == ZipErrorAction.InvokeErrorEvent)
								{
									method_22(ex);
									if (bool_12)
									{
										flag = true;
										break;
									}
								}
								num2++;
								continue;
							}
							throw;
						}
						if (!stream_3.CanSeek)
						{
							throw;
						}
						long position = stream_3.Position;
						stream_3.Seek(long_5, SeekOrigin.Begin);
						long position2 = stream_3.Position;
						stream_3.SetLength(stream_3.Position);
						if (stream_3 is Stream2 stream4)
						{
							stream4.method_3(position - position2);
						}
						if (ZipErrorAction == ZipErrorAction.Skip)
						{
							method_38("Skipping file {0} (exception: {1})", LocalFileName, ex.ToString());
							bool_9 = true;
							flag = true;
						}
						else
						{
							ZipErrorAction = zipErrorAction;
						}
						break;
					}
					throw;
				}
			}
			if (flag)
			{
				return;
			}
		}
		method_28(stream_3);
	}

	internal void method_24()
	{
		long_4 = long_5;
	}

	internal void method_25()
	{
		encryptionAlgorithm_1 = encryptionAlgorithm_0;
		short_3 = short_2;
		bool_7 = false;
		bool_6 = false;
		zipEntrySource_0 = ZipEntrySource.None;
	}

	private void method_26(Stream stream_3)
	{
		method_27(stream_3);
		method_14(stream_3);
		long_6 = int_2 + long_1 + int_3;
	}

	internal void method_27(Stream stream_3)
	{
		string text = string_3;
		if (zipEntrySource_0 == ZipEntrySource.ZipFile && text == null)
		{
			text = class31_0.method_3();
		}
		if (text == null)
		{
			class6_1 = null;
		}
		else if (Encryption == EncryptionAlgorithm.PkzipWeak)
		{
			class6_1 = Class6.smethod_0(text);
			Random random = new Random();
			byte[] array = new byte[12];
			random.NextBytes(array);
			if ((short_1 & 8) == 8)
			{
				int_0 = Class7.smethod_16(LastModified);
				array[11] = (byte)((uint)(int_0 >> 8) & 0xFFu);
			}
			else
			{
				method_10();
				array[11] = (byte)((uint)(int_1 >> 24) & 0xFFu);
			}
			byte[] array2 = class6_1.method_2(array, array.Length);
			stream_3.Write(array2, 0, array2.Length);
			int_2 += array2.Length;
		}
	}

	private void method_28(Stream stream_3)
	{
		if (LengthOfHeader == 0)
		{
			throw new BadStateException("Bad header length.");
		}
		if (bool_6 || (bool_10 && class31_0.method_11() == Zip64Option.Default) || (!bool_10 && class31_0.method_11() == Zip64Option.Always))
		{
			method_29(stream_3);
		}
		else
		{
			method_30(stream_3);
		}
		nullable_1 = long_0 >= 4294967295L || long_2 >= 4294967295L || long_4 >= 4294967295L;
		nullable_2 = class31_0.method_4() == Zip64Option.Always || nullable_1.Value;
	}

	private void method_29(Stream stream_3)
	{
		byte[] array = new byte[BufferSize];
		Stream2 stream = new Stream2(ArchiveStream);
		long num = long_4;
		int lengthOfHeader = LengthOfHeader;
		method_9(stream_3, 0);
		method_24();
		if (!FileName.EndsWith("/"))
		{
			long num2 = num + lengthOfHeader;
			int num3 = smethod_8(encryptionAlgorithm_1);
			num2 -= num3;
			int_2 += num3;
			stream.Seek(num2, SeekOrigin.Begin);
			long num4 = long_0;
			while (num4 > 0L)
			{
				num3 = (int)((num4 > array.Length) ? array.Length : num4);
				int num5 = stream.Read(array, 0, num3);
				stream_3.Write(array, 0, num5);
				num4 -= num5;
				method_13(stream.method_2(), long_0);
				if (bool_12)
				{
					break;
				}
			}
			if ((short_1 & 8) == 8)
			{
				int num6 = 16;
				if (bool_10)
				{
					num6 += 8;
				}
				byte[] buffer = new byte[num6];
				stream.Read(buffer, 0, num6);
				if (bool_10 && class31_0.method_11() == Zip64Option.Default)
				{
					stream_3.Write(buffer, 0, 8);
					if (long_0 > 4294967295L)
					{
						throw new InvalidOperationException("ZIP64 is required");
					}
					stream_3.Write(buffer, 8, 4);
					if (long_2 > 4294967295L)
					{
						throw new InvalidOperationException("ZIP64 is required");
					}
					stream_3.Write(buffer, 16, 4);
					int_3 -= 8;
				}
				else if (!bool_10 && class31_0.method_11() == Zip64Option.Always)
				{
					byte[] buffer2 = new byte[4];
					stream_3.Write(buffer, 0, 8);
					stream_3.Write(buffer, 8, 4);
					stream_3.Write(buffer2, 0, 4);
					stream_3.Write(buffer, 12, 4);
					stream_3.Write(buffer2, 0, 4);
					int_3 += 8;
				}
				else
				{
					stream_3.Write(buffer, 0, num6);
				}
			}
		}
		long_6 = int_2 + long_1 + int_3;
	}

	private void method_30(Stream stream_3)
	{
		byte[] array = new byte[BufferSize];
		Stream2 stream = new Stream2(ArchiveStream);
		stream.Seek(long_4, SeekOrigin.Begin);
		if (long_6 == 0L)
		{
			long_6 = int_2 + long_1 + int_3;
		}
		long_4 = (stream_3 as Stream2)?.method_4() ?? stream_3.Position;
		long num = long_6;
		while (num > 0L)
		{
			int count = (int)((num > array.Length) ? array.Length : num);
			int num2 = stream.Read(array, 0, count);
			stream_3.Write(array, 0, num2);
			num -= num2;
			method_13(stream.method_2(), long_6);
			if (bool_12)
			{
				break;
			}
		}
	}

	[Conditional("Trace")]
	private void method_31(string string_4, params object[] object_1)
	{
		lock (object_0)
		{
			int hashCode = Thread.CurrentThread.GetHashCode();
			Console.ForegroundColor = (ConsoleColor)(hashCode % 8 + 8);
			Console.Write("{0:000} ZipEntry.Write ", hashCode);
			Console.WriteLine(string_4, object_1);
			Console.ResetColor();
		}
	}

	internal void method_32()
	{
		long_3 = -1L;
		int_2 = 0;
	}

	internal static ZipEntry smethod_9(ZipFile zipFile_0)
	{
		Stream readStream = zipFile_0.ReadStream;
		Encoding provisionalAlternateEncoding = zipFile_0.ProvisionalAlternateEncoding;
		int num = Class7.smethod_8(readStream);
		if (smethod_10(num))
		{
			readStream.Seek(-4L, SeekOrigin.Current);
			if (num != 101010256L && num != 101075792L && num != 67324752)
			{
				throw new BadReadException($"  ZipEntry::ReadDirEntry(): Bad signature (0x{num:X8}) at position 0x{readStream.Position:X8}");
			}
			return null;
		}
		int num2 = 46;
		byte[] array = new byte[42];
		int num3 = readStream.Read(array, 0, array.Length);
		if (num3 != array.Length)
		{
			return null;
		}
		ZipEntry zipEntry = new ZipEntry();
		zipEntry.ProvisionalAlternateEncoding = provisionalAlternateEncoding;
		zipEntry.zipEntrySource_0 = ZipEntrySource.ZipFile;
		zipEntry.class31_0 = new Class31(zipFile_0);
		zipEntry.short_4 = (short)(array[0] + array[1] * 256);
		zipEntry.short_0 = (short)(array[2] + array[3] * 256);
		zipEntry.short_1 = (short)(array[4] + array[5] * 256);
		zipEntry.short_2 = (short)(array[6] + array[7] * 256);
		zipEntry.int_0 = array[8] + array[9] * 256 + array[10] * 256 * 256 + array[11] * 256 * 256 * 256;
		zipEntry.dateTime_0 = Class7.smethod_15(zipEntry.int_0);
		zipEntry.zipEntryTimestamp_0 |= ZipEntryTimestamp.DOS;
		zipEntry.int_1 = array[12] + array[13] * 256 + array[14] * 256 * 256 + array[15] * 256 * 256 * 256;
		zipEntry.long_0 = (uint)(array[16] + array[17] * 256 + array[18] * 256 * 256 + array[19] * 256 * 256 * 256);
		zipEntry.long_2 = (uint)(array[20] + array[21] * 256 + array[22] * 256 * 256 + array[23] * 256 * 256 * 256);
		zipEntry.short_3 = zipEntry.short_2;
		zipEntry.short_6 = (short)(array[24] + array[25] * 256);
		zipEntry.short_7 = (short)(array[26] + array[27] * 256);
		zipEntry.short_8 = (short)(array[28] + array[29] * 256);
		zipEntry.uint_0 = (uint)(array[30] + array[31] * 256);
		zipEntry.short_5 = (short)(array[32] + array[33] * 256);
		zipEntry.int_4 = array[34] + array[35] * 256 + array[36] * 256 * 256 + array[37] * 256 * 256 * 256;
		zipEntry.long_4 = (uint)(array[38] + array[39] * 256 + array[40] * 256 * 256 + array[41] * 256 * 256 * 256);
		zipEntry.IsText = (zipEntry.short_5 & 1) == 1;
		array = new byte[zipEntry.short_6];
		num3 = readStream.Read(array, 0, array.Length);
		num2 += num3;
		if ((zipEntry.short_1 & 0x800) == 2048)
		{
			zipEntry.string_1 = Class7.smethod_6(array);
		}
		else
		{
			zipEntry.string_1 = Class7.smethod_7(array, provisionalAlternateEncoding);
		}
		if (zipEntry.AttributesIndicateDirectory)
		{
			zipEntry.method_0();
		}
		else if (zipEntry.string_1.EndsWith("/"))
		{
			zipEntry.method_0();
		}
		zipEntry.long_1 = zipEntry.long_0;
		if ((zipEntry.short_1 & 1) == 1)
		{
			zipEntry.encryptionAlgorithm_0 = EncryptionAlgorithm.PkzipWeak;
			zipEntry.encryptionAlgorithm_1 = EncryptionAlgorithm.PkzipWeak;
			zipEntry.bool_8 = true;
		}
		if (zipEntry.short_7 > 0)
		{
			zipEntry.bool_10 = zipEntry.long_0 == 4294967295L || zipEntry.long_2 == 4294967295L || zipEntry.long_4 == 4294967295L;
			num2 += zipEntry.method_52(readStream, zipEntry.short_7);
			zipEntry.long_1 = zipEntry.long_0;
		}
		if (zipEntry.encryptionAlgorithm_0 == EncryptionAlgorithm.PkzipWeak)
		{
			zipEntry.long_1 -= 12L;
		}
		if ((zipEntry.short_1 & 8) == 8)
		{
			if (zipEntry.bool_10)
			{
				zipEntry.int_3 += 24;
			}
			else
			{
				zipEntry.int_3 += 16;
			}
		}
		if (zipEntry.short_8 > 0)
		{
			array = new byte[zipEntry.short_8];
			num3 = readStream.Read(array, 0, array.Length);
			num2 += num3;
			if ((zipEntry.short_1 & 0x800) == 2048)
			{
				zipEntry.string_2 = Class7.smethod_6(array);
			}
			else
			{
				zipEntry.string_2 = Class7.smethod_7(array, provisionalAlternateEncoding);
			}
		}
		return zipEntry;
	}

	internal static bool smethod_10(int int_6)
	{
		return int_6 != 33639248;
	}

	public void Extract()
	{
		method_39(".", null, null);
	}

	public void Extract(ExtractExistingFileAction extractExistingFile)
	{
		ExtractExistingFile = extractExistingFile;
		method_39(".", null, null);
	}

	public void Extract(Stream stream)
	{
		method_39(null, stream, null);
	}

	public void Extract(string baseDirectory)
	{
		method_39(baseDirectory, null, null);
	}

	public void Extract(string baseDirectory, ExtractExistingFileAction extractExistingFile)
	{
		ExtractExistingFile = extractExistingFile;
		method_39(baseDirectory, null, null);
	}

	public void ExtractWithPassword(string password)
	{
		method_39(".", null, password);
	}

	public void ExtractWithPassword(string baseDirectory, string password)
	{
		method_39(baseDirectory, null, password);
	}

	public void ExtractWithPassword(ExtractExistingFileAction extractExistingFile, string password)
	{
		ExtractExistingFile = extractExistingFile;
		method_39(".", null, password);
	}

	public void ExtractWithPassword(string baseDirectory, ExtractExistingFileAction extractExistingFile, string password)
	{
		ExtractExistingFile = extractExistingFile;
		method_39(baseDirectory, null, password);
	}

	public void ExtractWithPassword(Stream stream, string password)
	{
		method_39(null, stream, password);
	}

	public CrcCalculatorStream OpenReader()
	{
		return method_33(string_3 ?? class31_0.method_3());
	}

	public CrcCalculatorStream OpenReader(string password)
	{
		return method_33(password);
	}

	internal CrcCalculatorStream method_33(string string_4)
	{
		method_48();
		method_47();
		method_49(string_4);
		if (zipEntrySource_0 != ZipEntrySource.ZipFile)
		{
			throw new BadStateException("You must call ZipFile.Save before calling OpenReader.");
		}
		long length = ((short_3 == 0) ? long_1 : UncompressedSize);
		Stream archiveStream = ArchiveStream;
		ArchiveStream.Seek(FileDataPosition, SeekOrigin.Begin);
		stream_2 = method_45(archiveStream);
		Stream stream = method_44(stream_2);
		return new CrcCalculatorStream(stream, length);
	}

	private void method_34(long long_7, long long_8)
	{
		if (class31_0.method_0() != null)
		{
			bool_12 = class31_0.method_0().method_13(this, long_7, long_8);
		}
	}

	private void method_35(string string_4)
	{
		if (class31_0.method_0() != null && !class31_0.method_0().bool_11)
		{
			bool_12 = class31_0.method_0().method_14(this, string_4, bool_17: true);
		}
	}

	private void method_36(string string_4)
	{
		if (class31_0.method_0() != null && !class31_0.method_0().bool_11)
		{
			class31_0.method_0().method_14(this, string_4, bool_17: false);
		}
	}

	private void method_37(string string_4)
	{
		if (class31_0.method_0() != null)
		{
			bool_12 = class31_0.method_0().method_15(this, string_4);
		}
	}

	private static void smethod_11(string string_4)
	{
		if ((File.GetAttributes(string_4) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
		{
			File.SetAttributes(string_4, FileAttributes.Normal);
		}
		File.Delete(string_4);
	}

	private void method_38(string string_4, params object[] object_1)
	{
		if (class31_0.method_0() != null && class31_0.method_0().Verbose)
		{
			class31_0.method_0().StatusMessageTextWriter.WriteLine(string_4, object_1);
		}
	}

	private void method_39(string string_4, Stream stream_3, string string_5)
	{
		if (class31_0 == null)
		{
			throw new BadStateException("This ZipEntry is an orphan.");
		}
		class31_0.method_0().method_24();
		if (zipEntrySource_0 != ZipEntrySource.ZipFile)
		{
			throw new BadStateException("You must call ZipFile.Save before calling any Extract method.");
		}
		method_35(string_4);
		bool_12 = false;
		string string_6 = null;
		Stream stream = null;
		bool flag = false;
		bool flag2 = false;
		try
		{
			method_48();
			method_47();
			if (method_50(string_4, stream_3, out string_6))
			{
				method_38("extract dir {0}...", string_6);
				method_36(string_4);
				return;
			}
			string text = string_5 ?? string_3 ?? class31_0.method_3();
			if (encryptionAlgorithm_1 != 0)
			{
				if (text == null)
				{
					throw new BadPasswordException();
				}
				method_49(text);
			}
			if (string_6 != null)
			{
				method_38("extract file {0}...", string_6);
				if (!Directory.Exists(Path.GetDirectoryName(string_6)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(string_6));
				}
				else if (class31_0.method_0() != null)
				{
					flag2 = class31_0.method_0().bool_11;
				}
				if (File.Exists(string_6))
				{
					flag = true;
					int num = method_41(string_4, string_6);
					if (num == 2 || num == 1)
					{
						return;
					}
				}
				stream = new FileStream(string_6, FileMode.CreateNew);
			}
			else
			{
				method_38("extract entry {0} to stream...", FileName);
				stream = stream_3;
			}
			if (bool_12)
			{
				return;
			}
			int int_ = method_43(stream);
			if (bool_12)
			{
				return;
			}
			method_40(int_);
			if (string_6 != null)
			{
				stream.Close();
				stream = null;
				method_46(string_6, bool_15: true);
				if (flag2 && FileName.IndexOf('/') != -1)
				{
					string directoryName = Path.GetDirectoryName(FileName);
					if (class31_0.method_0()[directoryName] == null)
					{
						method_46(Path.GetDirectoryName(string_6), bool_15: false);
					}
				}
				if ((short_4 & 0xFF00) == 2560 || (short_4 & 0xFF00) == 0)
				{
					File.SetAttributes(string_6, (FileAttributes)int_4);
				}
			}
			method_36(string_4);
		}
		catch (Exception)
		{
			bool_12 = true;
			throw;
		}
		finally
		{
			if (bool_12 && string_6 != null)
			{
				stream?.Close();
				if (File.Exists(string_6) && (!flag || ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently))
				{
					File.Delete(string_6);
				}
			}
		}
	}

	internal void method_40(int int_6)
	{
		if (int_6 != int_1)
		{
			throw new BadCrcException("CRC error: the file being extracted appears to be corrupted. " + $"Expected 0x{int_1:X8}, Actual 0x{int_6:X8}");
		}
	}

	private int method_41(string string_4, string string_5)
	{
		int num = 0;
		while (true)
		{
			switch (ExtractExistingFile)
			{
			case ExtractExistingFileAction.InvokeExtractProgressEvent:
				if (num <= 0)
				{
					method_37(string_4);
					if (bool_12)
					{
						return 2;
					}
					break;
				}
				throw new ZipException($"The file {string_5} already exists.");
			default:
				throw new ZipException($"The file {string_5} already exists.");
			case ExtractExistingFileAction.OverwriteSilently:
				method_38("the file {0} exists; deleting it...", string_5);
				smethod_11(string_5);
				return 0;
			case ExtractExistingFileAction.DoNotOverwrite:
				method_38("the file {0} exists; not extracting entry...", FileName);
				method_36(string_4);
				return 1;
			}
			num++;
		}
	}

	private void method_42(int int_6)
	{
		if (int_6 == 0)
		{
			throw new BadReadException($"bad read of entry {FileName} from compressed archive.");
		}
	}

	private int method_43(Stream stream_3)
	{
		Stream archiveStream = ArchiveStream;
		archiveStream.Seek(FileDataPosition, SeekOrigin.Begin);
		int result = 0;
		byte[] array = new byte[BufferSize];
		long num = ((short_3 == 8) ? UncompressedSize : long_1);
		stream_2 = method_45(archiveStream);
		Stream stream = method_44(stream_2);
		long num2 = 0L;
		using (CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(stream))
		{
			while (num > 0L)
			{
				int count = (int)((num > array.Length) ? array.Length : num);
				int num3 = crcCalculatorStream.Read(array, 0, count);
				method_42(num3);
				stream_3.Write(array, 0, num3);
				num -= num3;
				num2 += num3;
				method_34(num2, UncompressedSize);
				if (bool_12)
				{
					break;
				}
			}
			result = crcCalculatorStream.Crc;
		}
		if (stream.CanRead)
		{
			stream.Close();
		}
		return result;
	}

	internal Stream method_44(Stream stream_3)
	{
		return (short_3 == 0) ? stream_3 : new DeflateStream(stream_3, CompressionMode.Decompress, leaveOpen: true);
	}

	internal Stream method_45(Stream stream_3)
	{
		Stream stream = null;
		if (encryptionAlgorithm_1 == EncryptionAlgorithm.PkzipWeak)
		{
			return new Stream1(stream_3, class6_0, Enum5.const_1);
		}
		return stream_3;
	}

	internal void method_46(string string_4, bool bool_15)
	{
		try
		{
			if (bool_0)
			{
				if (bool_15)
				{
					if (File.Exists(string_4))
					{
						File.SetCreationTimeUtc(string_4, dateTime_3);
						File.SetLastAccessTimeUtc(string_4, dateTime_2);
						File.SetLastWriteTimeUtc(string_4, dateTime_1);
					}
				}
				else if (Directory.Exists(string_4))
				{
					Directory.SetCreationTimeUtc(string_4, dateTime_3);
					Directory.SetLastAccessTimeUtc(string_4, dateTime_2);
					Directory.SetLastWriteTimeUtc(string_4, dateTime_1);
				}
			}
			else
			{
				DateTime lastWriteTime = Class7.smethod_13(LastModified);
				if (bool_15)
				{
					File.SetLastWriteTime(string_4, lastWriteTime);
				}
				else
				{
					Directory.SetLastWriteTime(string_4, lastWriteTime);
				}
			}
		}
		catch (IOException ex)
		{
			method_38("failed to set time on {0}: {1}", string_4, ex.Message);
		}
	}

	internal void method_47()
	{
		if (Encryption != EncryptionAlgorithm.PkzipWeak && Encryption != 0)
		{
			if (uint_1 != 0)
			{
				throw new ZipException($"Cannot extract: Entry {FileName} is encrypted with an algorithm not supported by DotNetZip: {UnsupportedAlgorithm}");
			}
			throw new ZipException($"Cannot extract: Entry {FileName} uses an unsupported encryption algorithm ({(int)Encryption:X2})");
		}
	}

	private void method_48()
	{
		if (short_3 != 0 && short_3 != 8)
		{
			throw new ZipException($"Entry {FileName} uses an unsupported compression method (0x{short_3:X2}, {UnsupportedCompressionMethod})");
		}
	}

	private void method_49(string string_4)
	{
		if (encryptionAlgorithm_1 != 0 && encryptionAlgorithm_1 == EncryptionAlgorithm.PkzipWeak)
		{
			if (string_4 == null)
			{
				throw new ZipException("Missing password.");
			}
			ArchiveStream.Seek(FileDataPosition - 12L, SeekOrigin.Begin);
			class6_0 = Class6.smethod_1(string_4, this);
		}
	}

	private bool method_50(string string_4, Stream stream_3, out string string_5)
	{
		if (string_4 != null)
		{
			string text = FileName;
			if (text.StartsWith("/"))
			{
				text = FileName.Substring(1);
			}
			if (class31_0.method_0().FlattenFoldersOnExtract)
			{
				string_5 = Path.Combine(string_4, (text.IndexOf('/') != -1) ? Path.GetFileName(text) : text);
			}
			else
			{
				string_5 = Path.Combine(string_4, text);
			}
			if (!IsDirectory && !FileName.EndsWith("/"))
			{
				return false;
			}
			if (!Directory.Exists(string_5))
			{
				Directory.CreateDirectory(string_5);
				method_46(string_5, bool_15: false);
			}
			else if (ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently)
			{
				method_46(string_5, bool_15: false);
			}
			return true;
		}
		if (stream_3 != null)
		{
			string_5 = null;
			if (!IsDirectory && !FileName.EndsWith("/"))
			{
				return false;
			}
			return true;
		}
		throw new ArgumentException("Invalid input.", "outstream");
	}

	private void method_51()
	{
		int_5++;
		long position = ArchiveStream.Position;
		ArchiveStream.Seek(long_4, SeekOrigin.Begin);
		byte[] array = new byte[30];
		ArchiveStream.Read(array, 0, array.Length);
		short num = (short)(array[26] + array[27] * 256);
		short short_ = (short)(array[28] + array[29] * 256);
		ArchiveStream.Seek(num, SeekOrigin.Current);
		method_52(ArchiveStream, short_);
		ArchiveStream.Seek(position, SeekOrigin.Begin);
		int_5--;
	}

	private static bool smethod_12(ZipEntry zipEntry_0, Encoding encoding_3)
	{
		int num = 0;
		zipEntry_0.long_4 = zipEntry_0.ArchiveStream.Position;
		int num2 = Class7.smethod_9(zipEntry_0.ArchiveStream);
		num = 4;
		if (smethod_14(num2))
		{
			zipEntry_0.ArchiveStream.Seek(-4L, SeekOrigin.Current);
			if (smethod_10(num2) && num2 != 101010256L)
			{
				throw new BadReadException($"  ZipEntry::ReadHeader(): Bad signature (0x{num2:X8}) at position  0x{zipEntry_0.ArchiveStream.Position:X8}");
			}
			return false;
		}
		byte[] array = new byte[26];
		int num3 = zipEntry_0.ArchiveStream.Read(array, 0, array.Length);
		if (num3 != array.Length)
		{
			return false;
		}
		num += num3;
		int num4 = 0;
		byte[] array2 = array;
		num4 = 1;
		byte num5 = array2[0];
		byte[] array3 = array;
		num4 = 2;
		zipEntry_0.short_0 = (short)(num5 + array3[1] * 256);
		byte[] array4 = array;
		num4 = 3;
		byte num6 = array4[2];
		byte[] array5 = array;
		num4 = 4;
		zipEntry_0.short_1 = (short)(num6 + array5[3] * 256);
		byte[] array6 = array;
		num4 = 5;
		byte num7 = array6[4];
		byte[] array7 = array;
		num4 = 6;
		zipEntry_0.short_3 = (zipEntry_0.short_2 = (short)(num7 + array7[5] * 256));
		byte[] array8 = array;
		num4 = 7;
		byte num8 = array8[6];
		byte[] array9 = array;
		num4 = 8;
		int num9 = num8 + array9[7] * 256;
		byte[] array10 = array;
		num4 = 9;
		int num10 = num9 + array10[8] * 256 * 256;
		byte[] array11 = array;
		num4 = 10;
		zipEntry_0.int_0 = num10 + array11[9] * 256 * 256 * 256;
		zipEntry_0.dateTime_0 = Class7.smethod_15(zipEntry_0.int_0);
		zipEntry_0.zipEntryTimestamp_0 |= ZipEntryTimestamp.DOS;
		if ((zipEntry_0.short_1 & 1) == 1)
		{
			zipEntry_0.encryptionAlgorithm_0 = EncryptionAlgorithm.PkzipWeak;
			zipEntry_0.encryptionAlgorithm_1 = EncryptionAlgorithm.PkzipWeak;
			zipEntry_0.bool_8 = true;
		}
		zipEntry_0.int_1 = array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256;
		zipEntry_0.long_0 = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
		zipEntry_0.long_2 = (uint)(array[num4++] + array[num4++] * 256 + array[num4++] * 256 * 256 + array[num4++] * 256 * 256 * 256);
		if ((int)zipEntry_0.long_0 == -1 || (int)zipEntry_0.long_2 == -1)
		{
			zipEntry_0.bool_10 = true;
		}
		short num11 = (short)(array[num4++] + array[num4++] * 256);
		short short_ = (short)(array[num4++] + array[num4++] * 256);
		array = new byte[num11];
		num3 = zipEntry_0.ArchiveStream.Read(array, 0, array.Length);
		num += num3;
		zipEntry_0.encoding_2 = (((zipEntry_0.short_1 & 0x800) == 2048) ? Encoding.UTF8 : encoding_3);
		zipEntry_0.string_1 = zipEntry_0.encoding_2.GetString(array, 0, array.Length);
		if (zipEntry_0.string_1.EndsWith("/"))
		{
			zipEntry_0.method_0();
		}
		num += zipEntry_0.method_52(zipEntry_0.ArchiveStream, short_);
		zipEntry_0.int_3 = 0;
		if (!zipEntry_0.string_1.EndsWith("/") && (zipEntry_0.short_1 & 8) == 8)
		{
			long position = zipEntry_0.ArchiveStream.Position;
			bool flag = true;
			long num12 = 0L;
			int num13 = 0;
			while (flag)
			{
				num13++;
				if (zipEntry_0.class31_0.method_0() != null)
				{
					zipEntry_0.class31_0.method_0().method_10(zipEntry_0);
				}
				long num14 = Class7.smethod_12(zipEntry_0.ArchiveStream, 134695760);
				if (num14 != -1L)
				{
					num12 += num14;
					if (zipEntry_0.bool_10)
					{
						array = new byte[20];
						num3 = zipEntry_0.ArchiveStream.Read(array, 0, array.Length);
						if (num3 != 20)
						{
							return false;
						}
						num4 = 0;
						byte[] array12 = array;
						num4 = 1;
						byte num15 = array12[0];
						byte[] array13 = array;
						num4 = 2;
						int num16 = num15 + array13[1] * 256;
						byte[] array14 = array;
						num4 = 3;
						int num17 = num16 + array14[2] * 256 * 256;
						byte[] array15 = array;
						num4 = 4;
						zipEntry_0.int_1 = num17 + array15[3] * 256 * 256 * 256;
						zipEntry_0.long_0 = BitConverter.ToInt64(array, 4);
						num4 = 12;
						zipEntry_0.long_2 = BitConverter.ToInt64(array, 12);
						num4 = 20;
						zipEntry_0.int_3 += 24;
					}
					else
					{
						array = new byte[12];
						num3 = zipEntry_0.ArchiveStream.Read(array, 0, array.Length);
						if (num3 != 12)
						{
							return false;
						}
						num4 = 0;
						byte[] array16 = array;
						num4 = 1;
						byte num18 = array16[0];
						byte[] array17 = array;
						num4 = 2;
						int num19 = num18 + array17[1] * 256;
						byte[] array18 = array;
						num4 = 3;
						int num20 = num19 + array18[2] * 256 * 256;
						byte[] array19 = array;
						num4 = 4;
						zipEntry_0.int_1 = num20 + array19[3] * 256 * 256 * 256;
						byte[] array20 = array;
						num4 = 5;
						byte num21 = array20[4];
						byte[] array21 = array;
						num4 = 6;
						int num22 = num21 + array21[5] * 256;
						byte[] array22 = array;
						num4 = 7;
						int num23 = num22 + array22[6] * 256 * 256;
						byte[] array23 = array;
						num4 = 8;
						zipEntry_0.long_0 = (uint)(num23 + array23[7] * 256 * 256 * 256);
						byte[] array24 = array;
						num4 = 9;
						byte num24 = array24[8];
						byte[] array25 = array;
						num4 = 10;
						int num25 = num24 + array25[9] * 256;
						byte[] array26 = array;
						num4 = 11;
						int num26 = num25 + array26[10] * 256 * 256;
						byte[] array27 = array;
						num4 = 12;
						zipEntry_0.long_2 = (uint)(num26 + array27[11] * 256 * 256 * 256);
						zipEntry_0.int_3 += 16;
					}
					if (flag = num12 != zipEntry_0.long_0)
					{
						zipEntry_0.ArchiveStream.Seek(-12L, SeekOrigin.Current);
						num12 += 4L;
					}
					continue;
				}
				return false;
			}
			zipEntry_0.ArchiveStream.Seek(position, SeekOrigin.Begin);
		}
		zipEntry_0.long_1 = zipEntry_0.long_0;
		if ((zipEntry_0.short_1 & 1) == 1)
		{
			zipEntry_0.byte_3 = new byte[12];
			num += smethod_13(zipEntry_0.stream_0, zipEntry_0.byte_3);
			zipEntry_0.long_1 -= 12L;
		}
		zipEntry_0.int_2 = num;
		zipEntry_0.long_6 = zipEntry_0.int_2 + zipEntry_0.long_1 + zipEntry_0.int_3;
		return true;
	}

	internal static int smethod_13(Stream stream_3, byte[] byte_4)
	{
		int num = stream_3.Read(byte_4, 0, 12);
		if (num != 12)
		{
			throw new ZipException($"Unexpected end of data at position 0x{stream_3.Position:X8}");
		}
		return num;
	}

	private static bool smethod_14(int int_6)
	{
		return int_6 != 67324752;
	}

	internal static ZipEntry smethod_15(Class31 class31_1, bool bool_15)
	{
		ZipFile zipFile = class31_1.method_0();
		Stream stream = class31_1.method_13();
		Encoding encoding_ = class31_1.method_12();
		ZipEntry zipEntry = new ZipEntry();
		zipEntry.zipEntrySource_0 = ZipEntrySource.ZipFile;
		zipEntry.class31_0 = class31_1;
		zipEntry.stream_0 = stream;
		zipFile?.method_11(bool_17: true, null);
		if (bool_15)
		{
			smethod_16(stream);
		}
		if (!smethod_12(zipEntry, encoding_))
		{
			return null;
		}
		zipEntry.long_3 = zipEntry.ArchiveStream.Position;
		stream.Seek(zipEntry.long_1 + zipEntry.int_3, SeekOrigin.Current);
		smethod_17(zipEntry);
		if (zipFile != null)
		{
			zipFile.method_10(zipEntry);
			zipFile.method_11(bool_17: false, zipEntry);
		}
		return zipEntry;
	}

	internal static void smethod_16(Stream stream_3)
	{
		uint num = (uint)Class7.smethod_10(stream_3);
		if (num != 808471376)
		{
			stream_3.Seek(-4L, SeekOrigin.Current);
		}
	}

	private static void smethod_17(ZipEntry zipEntry_0)
	{
		Stream archiveStream = zipEntry_0.ArchiveStream;
		uint num = (uint)Class7.smethod_10(archiveStream);
		if (num == zipEntry_0.int_1)
		{
			int num2 = Class7.smethod_10(archiveStream);
			if (num2 == zipEntry_0.long_0)
			{
				num2 = Class7.smethod_10(archiveStream);
				if (num2 != zipEntry_0.long_2)
				{
					archiveStream.Seek(-12L, SeekOrigin.Current);
				}
			}
			else
			{
				archiveStream.Seek(-8L, SeekOrigin.Current);
			}
		}
		else
		{
			archiveStream.Seek(-4L, SeekOrigin.Current);
		}
	}

	internal int method_52(Stream stream_3, short short_9)
	{
		int num = 0;
		if (short_9 > 0)
		{
			byte[] array = (byte_1 = new byte[short_9]);
			num = stream_3.Read(array, 0, array.Length);
			long long_ = stream_3.Position - num;
			int num2 = 0;
			while (num2 + 3 < array.Length)
			{
				int num3 = num2;
				ushort num4 = (ushort)(array[num2] + array[num2 + 1] * 256);
				short num5 = (short)(array[num2 + 2] + array[num2 + 3] * 256);
				num2 += 4;
				switch (num4)
				{
				case 23:
					num2 = method_53(array, num2);
					break;
				case 10:
					num2 = method_57(array, num2, num5, long_);
					break;
				case 1:
					num2 = method_54(array, num2, num5, long_);
					break;
				case 22613:
					num2 = method_55(array, num2, num5, long_);
					break;
				case 21589:
					num2 = method_56(array, num2, num5, long_);
					break;
				}
				num2 = num3 + num5 + 4;
			}
		}
		return num;
	}

	private int method_53(byte[] byte_4, int int_6)
	{
		int_6 += 2;
		uint_1 = (ushort)(byte_4[int_6] + byte_4[int_6 + 1] * 256);
		int_6 += 2;
		encryptionAlgorithm_0 = EncryptionAlgorithm.Unsupported;
		encryptionAlgorithm_1 = EncryptionAlgorithm.Unsupported;
		return int_6;
	}

	private int method_54(byte[] byte_4, int int_6, short short_9, long long_7)
	{
		bool_10 = true;
		if (short_9 > 28)
		{
			throw new BadReadException($"  Inconsistent datasize (0x{short_9:X4}) for ZIP64 extra field at position 0x{long_7:X16}");
		}
		int num = short_9;
		if (long_2 == 4294967295L)
		{
			if (num < 8)
			{
				throw new BadReadException(string.Format("  Missing data for ZIP64 extra field (Uncompressed Size) at position 0x{1:X16}", long_7));
			}
			long_2 = BitConverter.ToInt64(byte_4, int_6);
			int_6 += 8;
			num -= 8;
		}
		if (long_0 == 4294967295L)
		{
			if (num < 8)
			{
				throw new BadReadException(string.Format("  Missing data for ZIP64 extra field (Compressed Size) at position 0x{1:X16}", long_7));
			}
			long_0 = BitConverter.ToInt64(byte_4, int_6);
			int_6 += 8;
			num -= 8;
		}
		if (long_4 == 4294967295L)
		{
			if (num < 8)
			{
				throw new BadReadException(string.Format("  Missing data for ZIP64 extra field (Relative Offset) at position 0x{1:X16}", long_7));
			}
			long_4 = BitConverter.ToInt64(byte_4, int_6);
			int_6 += 8;
			num -= 8;
		}
		return int_6;
	}

	private int method_55(byte[] byte_4, int int_6, short short_9, long long_7)
	{
		if (short_9 != 12 && short_9 != 8)
		{
			throw new BadReadException($"  Unexpected datasize (0x{short_9:X4}) for InfoZip v1 extra field at position 0x{long_7:X16}");
		}
		int num = BitConverter.ToInt32(byte_4, int_6);
		dateTime_1 = dateTime_4.AddSeconds(num);
		int_6 += 4;
		num = BitConverter.ToInt32(byte_4, int_6);
		dateTime_2 = dateTime_4.AddSeconds(num);
		int_6 += 4;
		dateTime_3 = DateTime.UtcNow;
		bool_0 = true;
		zipEntryTimestamp_0 |= ZipEntryTimestamp.InfoZip1;
		return int_6;
	}

	private int method_56(byte[] byte_4, int int_6, short short_9, long long_7)
	{
		if (short_9 != 13 && short_9 != 9 && short_9 != 5)
		{
			throw new BadReadException($"  Unexpected datasize (0x{short_9:X4}) for Extended Timestamp extra field at position 0x{long_7:X16}");
		}
		int num = short_9;
		if (short_9 != 13 && int_5 <= 0)
		{
			method_51();
		}
		else
		{
			byte b = byte_4[int_6++];
			num--;
			if (((uint)b & (true ? 1u : 0u)) != 0 && num >= 4)
			{
				int num2 = BitConverter.ToInt32(byte_4, int_6);
				dateTime_1 = dateTime_4.AddSeconds(num2);
				int_6 += 4;
				num -= 4;
			}
			if ((b & 2u) != 0 && num >= 4)
			{
				int num3 = BitConverter.ToInt32(byte_4, int_6);
				dateTime_2 = dateTime_4.AddSeconds(num3);
				int_6 += 4;
				num -= 4;
			}
			else
			{
				dateTime_2 = DateTime.UtcNow;
			}
			if ((b & 4u) != 0 && num >= 4)
			{
				int num4 = BitConverter.ToInt32(byte_4, int_6);
				dateTime_3 = dateTime_4.AddSeconds(num4);
				int_6 += 4;
				num -= 4;
			}
			else
			{
				dateTime_3 = DateTime.UtcNow;
			}
			zipEntryTimestamp_0 |= ZipEntryTimestamp.Unix;
			bool_0 = true;
			bool_2 = true;
		}
		return int_6;
	}

	private int method_57(byte[] byte_4, int int_6, short short_9, long long_7)
	{
		if (short_9 != 32)
		{
			throw new BadReadException($"  Unexpected datasize (0x{short_9:X4}) for NTFS times extra field at position 0x{long_7:X16}");
		}
		int_6 += 4;
		short num = (short)(byte_4[int_6] + byte_4[int_6 + 1] * 256);
		short num2 = (short)(byte_4[int_6 + 2] + byte_4[int_6 + 3] * 256);
		int_6 += 4;
		if (num == 1 && num2 == 24)
		{
			long fileTime = BitConverter.ToInt64(byte_4, int_6);
			dateTime_1 = DateTime.FromFileTimeUtc(fileTime);
			int_6 += 8;
			fileTime = BitConverter.ToInt64(byte_4, int_6);
			dateTime_2 = DateTime.FromFileTimeUtc(fileTime);
			int_6 += 8;
			fileTime = BitConverter.ToInt64(byte_4, int_6);
			dateTime_3 = DateTime.FromFileTimeUtc(fileTime);
			int_6 += 8;
			bool_0 = true;
			zipEntryTimestamp_0 |= ZipEntryTimestamp.Windows;
			bool_1 = true;
		}
		return int_6;
	}
}
