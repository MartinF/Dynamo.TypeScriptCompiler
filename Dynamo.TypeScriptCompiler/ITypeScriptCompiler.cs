using System;

namespace Dynamo.TypeScriptCompiler
{
	public interface ITypeScriptCompiler
	{
		TypeScriptCompilerResult Compile(String filePath);
	}
}
