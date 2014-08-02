using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Remove constructor taking path as string ? or put in File.Exist check ? Then the TypeScriptPathResolver can be removed

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptCompiler : ITypeScriptCompiler
	{
		// Constructors
		public TypeScriptCompiler(String tscExecPath, int timeout = 10000)
		{
			if (tscExecPath == null)
				throw new ArgumentNullException("tscExecPath");

			TypeScriptCompilerPath = tscExecPath;
			Timeout = timeout;
		}

		public TypeScriptCompiler(ITypeScriptExecutableResolver tscExecResolver = null, int timeout = 10000)
			: this(tscExecResolver != null ? tscExecResolver.GetExecutablePath() : new TypeScriptExecutableVersionResolver().GetExecutablePath(), timeout)
		{
		}

        // Properties
		public int Timeout { get; private set; }
		public String TypeScriptCompilerPath { get; private set; }

		public ITypeScriptCompilerResult Compile(IEnumerable<String> files, ITypeScriptOptions options)
		{
			if (options == null)
				throw new ArgumentNullException("options");

			var strOpt = options.ToOptionsString();

			return Compile(files, strOpt);
		}

        // Methods
		public ITypeScriptCompilerResult Compile(IEnumerable<String> files, String options)
		{
			if (files == null)
				throw new ArgumentNullException("files");
			if (!files.Any())
				throw new ArgumentException("Files are needed");

			if (options == null)
				options = "";

			var fileArg = String.Join(" ", files);

			var args = fileArg + " " + options;

			var processStartInfo = new ProcessStartInfo
			{
				FileName = TypeScriptCompilerPath,
				Arguments = args,

				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardError = true,
			};

            using (var process = new Process() { StartInfo = processStartInfo })
		    {
				// Start
                process.Start();

				// Wait until compiling has finished
			    if (!process.WaitForExit(Timeout) || process.ExitCode != 0)
			    {
					// Time-out or ExitCode not 0
					return new TypeScriptCompilerResult(error: process.StandardError.ReadToEnd() ?? "Error happend when compiling. Reason unknown.");
			    }

			    return new TypeScriptCompilerResult(error: null);
		    }
		}
	}
}
