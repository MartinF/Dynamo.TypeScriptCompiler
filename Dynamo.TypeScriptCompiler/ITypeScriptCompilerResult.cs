using System;

namespace Dynamo.TypeScriptCompiler
{
	public interface ITypeScriptCompilerResult
	{
		int ExitCode { get; }
		String Source { get; }
		String SourceMap { get; }
	}
}
