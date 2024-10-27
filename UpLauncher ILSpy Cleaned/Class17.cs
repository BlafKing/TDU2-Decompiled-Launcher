using System;
using System.Diagnostics;
using Ionic.Zip;

internal abstract class Class17
{
	internal abstract bool vmethod_0(ZipEntry zipEntry_0);

	internal abstract bool vmethod_1(string string_0);

	[Conditional("TRACE")]
	protected void method_0(string string_0, params object[] object_0)
	{
		Console.WriteLine("  " + string_0, object_0);
	}
}
