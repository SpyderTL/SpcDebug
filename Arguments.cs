using System;

namespace SpcDebug
{
	internal class Arguments
	{
		internal static string Path;

		internal static void Load(string[] args)
		{
			Path = args[0];
		}
	}
}