using System;
using System.Collections;
using System.Text;

namespace UPnPDetection.UPnP;

public class DText
{
	public string ATTRMARK = "\u0080";

	public string MULTMARK = "\u0081";

	public string SUBVMARK = "\u0082";

	private ArrayList arrayList_0 = new ArrayList();

	public string this[int A]
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (A > 0)
			{
				int num = DCOUNT(A);
				for (int i = 1; i <= num; i++)
				{
					if (i != 1)
					{
						stringBuilder.Append(MULTMARK);
					}
					stringBuilder.Append(this[A, i]);
				}
				return stringBuilder.ToString();
			}
			int num2 = DCOUNT();
			for (int j = 1; j <= num2; j++)
			{
				if (j != 1)
				{
					stringBuilder.Append(ATTRMARK);
				}
				stringBuilder.Append(this[j]);
			}
			return stringBuilder.ToString();
		}
		set
		{
			if (A == 0)
			{
				arrayList_0 = method_0(value);
				return;
			}
			while (arrayList_0.Count < A)
			{
				arrayList_0.Add(new ArrayList());
			}
			ArrayList arrayList = method_0(value);
			if (arrayList.Count > 1)
			{
				arrayList_0.Insert(A - 1, arrayList);
			}
			else if (arrayList_0.Count < A - 1)
			{
				arrayList_0.Insert(A - 1, arrayList[0]);
			}
			else
			{
				arrayList_0[A - 1] = arrayList[0];
			}
		}
	}

	public string this[int A, int M]
	{
		get
		{
			if (M == 0)
			{
				return this[A];
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = DCOUNT(A, M);
			for (int i = 1; i <= num; i++)
			{
				if (i != 1)
				{
					stringBuilder.Append(SUBVMARK);
				}
				stringBuilder.Append(this[A, M, i]);
			}
			return stringBuilder.ToString();
		}
		set
		{
			if (M == 0)
			{
				this[A] = value;
				return;
			}
			while (arrayList_0.Count < A)
			{
				arrayList_0.Add(new ArrayList());
			}
			while (((ArrayList)arrayList_0[A - 1]).Count < M)
			{
				((ArrayList)arrayList_0[A - 1]).Add(new ArrayList());
			}
			ArrayList arrayList = method_0(value);
			if (arrayList.Count > 1)
			{
				arrayList_0.Insert(A - 1, arrayList);
			}
			else if (((ArrayList)arrayList[0]).Count > 1)
			{
				((ArrayList)arrayList_0[A - 1]).Insert(M - 1, arrayList[0]);
			}
			else
			{
				((ArrayList)arrayList_0[A - 1])[M - 1] = (ArrayList)((ArrayList)arrayList[0])[0];
			}
		}
	}

	public string this[int A, int M, int V]
	{
		get
		{
			if (V == 0)
			{
				return this[A, M];
			}
			try
			{
				return (string)((ArrayList)((ArrayList)arrayList_0[A - 1])[M - 1])[V - 1];
			}
			catch (Exception)
			{
				return "";
			}
		}
		set
		{
			if (V == 0)
			{
				this[A, M] = value;
				return;
			}
			while (arrayList_0.Count < A)
			{
				arrayList_0.Add(new ArrayList());
			}
			while (((ArrayList)arrayList_0[A - 1]).Count < M)
			{
				((ArrayList)arrayList_0[A - 1]).Add(new ArrayList());
			}
			while (((ArrayList)((ArrayList)arrayList_0[A - 1])[M - 1]).Count < V)
			{
				((ArrayList)((ArrayList)arrayList_0[A - 1])[M - 1]).Add(new ArrayList());
			}
			((ArrayList)((ArrayList)arrayList_0[A - 1])[M - 1])[V - 1] = value;
		}
	}

	public DText()
	{
	}

	public DText(string STR)
	{
		method_0(STR);
	}

	public int DCOUNT()
	{
		return arrayList_0.Count;
	}

	public int DCOUNT(int A)
	{
		if (A == 0)
		{
			return DCOUNT();
		}
		if (arrayList_0.Count < A)
		{
			return 0;
		}
		return ((ArrayList)arrayList_0[A - 1]).Count;
	}

	public int DCOUNT(int A, int M)
	{
		if (M == 0)
		{
			return DCOUNT(A);
		}
		if (arrayList_0.Count < A)
		{
			return 0;
		}
		if (((ArrayList)arrayList_0[A - 1]).Count < M)
		{
			return 0;
		}
		return ((ArrayList)((ArrayList)arrayList_0[A - 1])[M - 1]).Count;
	}

	private ArrayList method_0(string string_0)
	{
		if (string_0.Length == 0)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new ArrayList());
			((ArrayList)arrayList[0]).Add(new ArrayList());
			return arrayList;
		}
		int num = 1;
		int num2 = 1;
		int num3 = 1;
		StringBuilder stringBuilder = new StringBuilder();
		ArrayList arrayList2 = new ArrayList();
		for (int i = 0; i < string_0.Length; i++)
		{
			while (arrayList2.Count < num)
			{
				arrayList2.Add(new ArrayList());
			}
			while (((ArrayList)arrayList2[num - 1]).Count < num2)
			{
				((ArrayList)arrayList2[num - 1]).Add(new ArrayList());
			}
			while (((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1]).Count < num3)
			{
				((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1]).Add(new ArrayList());
			}
			string text = string_0.Substring(i, 1);
			if (!(text == ATTRMARK.Substring(0, 1)) && !(text == MULTMARK.Substring(0, 1)) && !(text == SUBVMARK.Substring(0, 1)))
			{
				stringBuilder.Append(text);
				continue;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (i + ATTRMARK.Length <= string_0.Length && string_0.Substring(i, ATTRMARK.Length) == ATTRMARK)
			{
				flag = true;
				i += ATTRMARK.Length - 1;
			}
			if (i + MULTMARK.Length <= string_0.Length && string_0.Substring(i, MULTMARK.Length) == MULTMARK)
			{
				flag2 = true;
				i += MULTMARK.Length - 1;
			}
			if (i + SUBVMARK.Length <= string_0.Length && string_0.Substring(i, SUBVMARK.Length) == SUBVMARK)
			{
				flag3 = true;
				i += SUBVMARK.Length - 1;
			}
			if (!flag && !flag2 && !flag3)
			{
				stringBuilder.Append(text);
				continue;
			}
			((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1])[num3 - 1] = stringBuilder.ToString();
			stringBuilder = new StringBuilder();
			if (flag)
			{
				num++;
				num2 = 1;
				num3 = 1;
			}
			if (flag2)
			{
				num2++;
				num3 = 1;
			}
			if (flag3)
			{
				num3++;
			}
		}
		if (stringBuilder.Length > 0)
		{
			((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1])[num3 - 1] = stringBuilder.ToString();
		}
		else
		{
			while (arrayList2.Count < num)
			{
				arrayList2.Add(new ArrayList());
			}
			while (((ArrayList)arrayList2[num - 1]).Count < num2)
			{
				((ArrayList)arrayList2[num - 1]).Add(new ArrayList());
			}
			while (((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1]).Count < num3)
			{
				((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1]).Add(new ArrayList());
			}
			((ArrayList)((ArrayList)arrayList2[num - 1])[num2 - 1])[num3 - 1] = "";
		}
		return arrayList2;
	}
}
