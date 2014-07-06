using System;
using System.IO;

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptExecutableResolver : ITypeScriptExecutableResolver
	{
		private readonly String _tsExecPath;

		public TypeScriptExecutableResolver(String typeScriptExecutablePath)
		{
			if (typeScriptExecutablePath == null)
				throw new ArgumentNullException("typeScriptExecutablePath");

			if (!File.Exists(typeScriptExecutablePath))
				throw new FileNotFoundException(typeScriptExecutablePath + " was not found");

			_tsExecPath = typeScriptExecutablePath;
		}
		
		public string GetExecutablePath()
		{
			return _tsExecPath;
		}
	}
}
