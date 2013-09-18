using System;

namespace Dynamo.TypeScriptCompiler
{
	public interface ITypeScriptCompilerResult
	{
		int ExitCode { get; }
		Boolean HasError { get; }
		String Error { get; }
		String Source { get; }
		String SourceMap { get; }
	}
}
