using System;

namespace Dynamo.TypeScriptCompiler
{
	public interface ITypeScriptCompiler
	{
		ITypeScriptCompilerResult Compile(String filePath);
	}
}
