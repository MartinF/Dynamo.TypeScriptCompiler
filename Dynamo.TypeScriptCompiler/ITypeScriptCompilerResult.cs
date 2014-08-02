using System;

namespace Dynamo.TypeScriptCompiler
{
	public interface ITypeScriptCompilerResult
	{
		Boolean IsSuccess { get; }
		String Error { get; }
	}
}
