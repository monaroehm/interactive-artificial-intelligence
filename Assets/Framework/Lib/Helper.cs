using System;
using System.Collections.Generic;

/// <summary>
/// https://stackoverflow.com/a/972323
/// </summary>
public static class EnumUtil
{
	public static IEnumerable<T> GetValues<T>()
	{
		return (T[])Enum.GetValues(typeof(T));
	}
}