using Ionic.Zlib;

internal class Class24
{
	internal enum Enum11
	{
		const_0,
		const_1,
		const_2,
		const_3,
		const_4,
		const_5,
		const_6
	}

	public byte[] byte_0;

	public byte[] byte_1;

	public int int_0;

	public int int_1;

	public int int_2;

	public int int_3;

	public int int_4;

	public ZlibCodec zlibCodec_0;

	public Class24(int int_5, CompressionLevel compressionLevel_0, CompressionStrategy compressionStrategy_0)
	{
		byte_0 = new byte[int_5];
		int num = int_5 + (int_5 / 32768 + 1) * 5 * 2;
		byte_1 = new byte[num];
		int_0 = 0;
		zlibCodec_0 = new ZlibCodec();
		zlibCodec_0.InitializeDeflate(compressionLevel_0, wantRfc1950Header: false);
		zlibCodec_0.OutputBuffer = byte_1;
		zlibCodec_0.InputBuffer = byte_0;
	}
}
