using System;

// Could split it into TypeScriptCompilerFileResult and TypeScriptCompilerSourceResult
// Add sourceFilePath/Name and sourceMapFilePath/Name
// Error

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptCompilerResult : ITypeScriptCompilerResult
	{
		// Fields
		private String _source;
		private String _sourceMap;
		private readonly Func<String> _sourceFactory;
		private readonly Func<String> _sourceMapFactory;

		// Constructors
		public TypeScriptCompilerResult(int exitCode, String source = null, String sourceMap = null)
		{
			ExitCode = exitCode;
			_source = source;
			_sourceMap = sourceMap;
		}

		public TypeScriptCompilerResult(int exitCode, Func<String> sourceFactory, Func<String> sourceMapFactory = null)
		{
			ExitCode = exitCode;
			_sourceFactory = sourceFactory;
			_sourceMapFactory = sourceMapFactory;
		}

		// Properties
		public int ExitCode { get; private set; }

		public String Source
		{
			get
			{
				return _source ?? (_source = _sourceFactory != null ? _sourceFactory() : null);				
			}
		}

		public String SourceMap
		{
			get
			{
				return _sourceMap ?? (_sourceMap = _sourceMapFactory != null ? _sourceMapFactory() : null);
			}
		}
	}
}
