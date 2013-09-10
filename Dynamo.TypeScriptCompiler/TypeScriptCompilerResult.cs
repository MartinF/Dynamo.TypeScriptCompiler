using System;

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptCompilerResult
	{
		public TypeScriptCompilerResult()
		{
			ExitCode = -1;
		}

		public int ExitCode { get; set; }
		public String CompiledSource { get; set; }
		public String SourceMap { get; set; }

		// Error
	}
}
