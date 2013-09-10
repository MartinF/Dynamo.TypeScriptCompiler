using System;

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptCompilerException : Exception
	{
		public TypeScriptCompilerException(string message)
			: base(message)
		{
		}
	}
}