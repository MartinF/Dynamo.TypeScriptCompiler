using System;
using System.Collections.Generic;

namespace Dynamo.TypeScriptCompiler
{
	public interface ITypeScriptCompiler
	{
		ITypeScriptCompilerResult Compile(IEnumerable<String> files, String options);
		ITypeScriptCompilerResult Compile(IEnumerable<String> files, ITypeScriptOptions options);
	}
}
