using System;

// TODO
// Could add which files was compiled, and the output .js files + maps
// Could also contain details about the options used when compiling

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptCompilerResult : ITypeScriptCompilerResult
	{
		// Constructors
		public TypeScriptCompilerResult(String error = null)
		{
			Error = error;

			if (error == null)
				IsSuccess = true;
		}

		// Properties
		public Boolean IsSuccess { get; private set; }
		public String Error { get; private set; }
	}
}
