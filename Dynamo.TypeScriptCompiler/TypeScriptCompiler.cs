using System;
using System.Diagnostics;
using System.IO;

// Make a SaveToDisk option so it will output next to the source instead of using the Temp folder and deleting the files
// If not saving to disk creating a source map option should not be available as it depends on the file name?

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
		public TypeScriptCompilerResult Compile(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			if (!File.Exists(filePath))
				throw new ArgumentException("File does not exist", "filePath");

			var result = new TypeScriptCompilerResult();

            var tempFolder = Path.GetTempPath().TrimEnd(new[] { '\\' });
		    var fileName = Path.GetFileName(filePath);
		    var newFileName = Path.ChangeExtension(fileName, ".js");
		    var newFilePath = Path.Combine(tempFolder, newFileName);

			// Create the Arguments
		    var args = "\"" + filePath + "\"";
		    args += " --outDir \"" + tempFolder + "\"";
			args += GenerateArgumentsFromOptions();

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
                if (process.WaitForExit(_timeout))
                {
                    // Exited

	                if (process.ExitCode == 0)
	                {
						// Read temporary generated file
						result.CompiledSource = File.ReadAllText(newFilePath);
						// Delete the temporary file (else it wouldnt be temporary)
						File.Delete(newFilePath);

						//if (Options.GenerateSourceMap)
						//{
						//	var sourceMapPath = newFilePath + ".map";
							
						//	// Read temporary source map
						//	result.SourceMap = File.ReadAllText(sourceMapPath);
						//	// Delete temporary source map
						//	File.Delete(sourceMapPath);
						//}
	                }
	                else
	                {
		                // TODO: Get error and add it to the result or throw exception ?
	                }
                }
                else
                {
                    // Timeout
					
                    // TODO: How to handle this? throw new TypeScriptCompilerException("Compiling timed out!"); ?
                }

				result.ExitCode = process.ExitCode;
		    }

			return result;
		}

		private String GenerateArgumentsFromOptions()
		{
			var args = " --target " + Options.Target.ToString();
			
			if (Options.AllowBool)
				args += " --allowBool";

			if (Options.RemoveComments)
				args += " --removeComments";

			//if (Options.GenerateSourceMap)
			//	args += " --sourcemap";

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
