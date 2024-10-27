using System;
using System.Collections.Generic;

namespace UPnPDetection.UPnP;

public sealed class UPnPServiceStateTable
{
	public class UPnPServiceStateVariable
	{
		private string string_0;

		private string string_1;

		private List<string> list_0;

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

		public string DataType
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

		public List<string> AllowedValueList
		{
			get
			{
				return list_0;
			}
			set
			{
				list_0 = value;
			}
		}
	}

	private List<UPnPServiceStateVariable> list_0;

	public List<UPnPServiceStateVariable> Variables
	{
		get
		{
			return list_0;
		}
		set
		{
			list_0 = value;
		}
	}

	public Type GetVariableType(UPnPServiceAction.UPnPServiceActionArgument a_Argument)
	{
		if (list_0.Count == 0)
		{
			return typeof(object);
		}
		foreach (UPnPServiceStateVariable item in list_0)
		{
			if (item.Name == a_Argument.RelatedStateVariable)
			{
				return method_0(item.DataType);
			}
		}
		return typeof(object);
	}

	private Type method_0(string string_0)
	{
		return string_0 switch
		{
			"string" => typeof(string), 
			"boolean" => typeof(bool), 
			"uri" => typeof(Uri), 
			"ui1" => typeof(byte), 
			"ui2" => typeof(ushort), 
			"ui4" => typeof(uint), 
			"int" => typeof(int), 
			"i4" => typeof(int), 
			"i2" => typeof(short), 
			"i1" => typeof(sbyte), 
			"r4" => typeof(float), 
			"r8" => typeof(double), 
			"number" => typeof(double), 
			"float" => typeof(float), 
			"char" => typeof(char), 
			"bin.base64" => typeof(byte[]), 
			"dateTime" => typeof(DateTime), 
			_ => typeof(object), 
		};
	}
}
