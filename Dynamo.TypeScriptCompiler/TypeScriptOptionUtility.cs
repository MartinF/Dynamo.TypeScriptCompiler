using System;

namespace Dynamo.TypeScriptCompiler
{
	internal static class TypeScriptOptionUtility
	{
		public static void Add(ref String current, String option)
		{
			if (current != String.Empty)
				current += " ";

			current += option;
		}

		public static void Add(ref String current, Boolean condition, String option)
		{
			if (condition)
				Add(ref current, option);
		}

		public static void Add(ref String current, String optionArg, String option)
		{
			if (!String.IsNullOrWhiteSpace(optionArg))
				Add(ref current, option + " " + optionArg);
		}

		public static void Add(ref String current, Object optionArg, String option)
		{
			if (optionArg == null)
				return;

			var arg = optionArg.ToString().Trim();

			if (arg != String.Empty)
				Add(ref current, option + " " + optionArg);
		}
	}
}
