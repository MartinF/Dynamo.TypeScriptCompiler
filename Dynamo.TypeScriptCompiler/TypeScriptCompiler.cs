using System;
using System.Diagnostics;
using System.IO;

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptCompiler : ITypeScriptCompiler
	{
        // Fields
		private readonly int _timeout;
		private readonly String _executablePath;

		// Constructors
		public TypeScriptCompiler(TypeScriptCompilerOptions options = null, int timeout = 10000)
		{
		    Options = options ?? new TypeScriptCompilerOptions();
			_timeout = timeout;
            _executablePath = GetExecutablePath();
		}

        // Properties
	    public TypeScriptCompilerOptions Options { get; private set; }

        // Methods
		public ITypeScriptCompilerResult Compile(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			if (!File.Exists(filePath))
				throw new ArgumentException("File does not exist", "filePath");

			var fileName = Path.GetFileName(filePath);
		    var outputSourceFileName = Path.ChangeExtension(fileName, ".js");
			var outputFolder = Options.SaveToDisk ? Path.GetDirectoryName(filePath) : Path.GetTempPath().TrimEnd(new[] { '\\' });
			
			if (Options.SourceMap && !Options.SaveToDisk)
			{
				// Files need to be saved to the same folder when a sourcemap is genereated (because of the reference to the source)
				// Easiest way to solve it is to output to either the folder of the target file or copy the target file to the temp folder

				var newFilePath = Path.Combine(outputFolder, fileName);
				File.Copy(filePath, newFilePath);
				filePath = newFilePath;
			}

			String outputSourcePath = Path.Combine(outputFolder, outputSourceFileName);
			String outputSourceMapPath = outputSourcePath + ".map";

			var args = GetArguments(filePath, outputFolder);

			var processStartInfo = new ProcessStartInfo
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				Arguments = args,
				FileName = _executablePath,
				UseShellExecute = false,
				RedirectStandardError = true
			};
			processStartInfo.EnvironmentVariables.Add("file", filePath);

            using (var process = new Process() { StartInfo = processStartInfo })
		    {
				// Start
                process.Start();

				// Wait until compiling has finished
			    if (!process.WaitForExit(_timeout) || process.ExitCode != 0)
			    {
					// Time-out or ExitCode not 0
					return new TypeScriptCompilerResult(process.ExitCode, error: process.StandardError.ReadToEnd());
			    }

			    if (Options.SaveToDisk)
			    {
				    
					var sourceFactory = new Func<String>(() => File.ReadAllText(outputSourcePath));
				    Func<String> sourceMapFactory = null;

				    if (Options.SourceMap)
						sourceMapFactory = () => File.ReadAllText(outputSourceMapPath);
				
				    return new TypeScriptCompilerResult(process.ExitCode, sourceFactory, sourceMapFactory);
			    }
			    else
			    {
					// Read temporary file
				    var source = File.ReadAllText(outputSourcePath);

					// Delete temporary file (else it wouldnt be temporary)
				    File.Delete(outputSourcePath);

				    String sourceMap = null;

				    if (Options.SourceMap)
				    {
						sourceMap = File.ReadAllText(outputSourceMapPath);
						File.Delete(outputSourceMapPath);
						// Also delete the filePath as it is temporary - needed to fix the reference in the sourcemap
					    File.Delete(filePath);
				    }

				    return new TypeScriptCompilerResult(process.ExitCode, source, sourceMap);
			    }
		    }
		}

		private String GetArguments(String filePath, String outputFolder)
		{
			var args = "\"" + filePath + "\" --outDir \"" + outputFolder + "\" --target " + Options.CompilerTarget;
	
			if (Options.AllowBool)
				args += " --allowBool";

			if (Options.RemoveComments)
				args += " --removeComments";

			if (Options.SourceMap)
				args += " --sourcemap";

			return args;
		}

		private static String GetExecutablePath()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			string executablePath = Path.Combine(folderPath, "Microsoft SDKs\\TypeScript\\tsc.exe");

			if (!File.Exists(executablePath))
			{
                // TODO: What kind of exception to throw? TypeScriptCompilerException? Mention the location it couldnt be found at?
				throw new Exception("ERROR: The TypeScript compiler couldn't be found. Download http://www.typescriptlang.org/#Download");
			}

			return executablePath;
		}
	}
}
