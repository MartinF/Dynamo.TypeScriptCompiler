using System;

// Could split it into TypeScriptCompilerFileResult and TypeScriptCompilerSourceResult
// Add sourceFilePath/Name and sourceMapFilePath/Name

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
		public TypeScriptCompilerResult(int exitCode, String source = null, String sourceMap = null, String error = null)
		{
			ExitCode = exitCode;
			_source = source;
			_sourceMap = sourceMap;
			Error = error;
		}

		public TypeScriptCompilerResult(int exitCode, Func<String> sourceFactory, Func<String> sourceMapFactory = null, String error = null)
			: this(exitCode, (String) null, (String) null, error)
		{
			_sourceFactory = sourceFactory;
			_sourceMapFactory = sourceMapFactory;
		}

		// Properties
		public int ExitCode { get; private set; }
		public String Error { get; private set; }
		public Boolean HasError { get { return Error != null; } }

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
