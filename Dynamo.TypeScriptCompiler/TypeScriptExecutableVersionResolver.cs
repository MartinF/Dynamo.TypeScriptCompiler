using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Dynamo.TypeScriptCompiler
{
	public class TypeScriptExecutableVersionResolver : ITypeScriptExecutableResolver
	{
		private readonly String _tsFolder;

		public TypeScriptExecutableVersionResolver()
			: this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft SDKs\\TypeScript"))
		{
			// Default
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeScriptFolder">The folder where all typescript versions are installed (not the actual version)</param>
		public TypeScriptExecutableVersionResolver(String typeScriptFolder)
		{
			if (typeScriptFolder == null)
				throw new ArgumentNullException("typeScriptFolder");
			if (!Directory.Exists(typeScriptFolder))
				throw new DirectoryNotFoundException(typeScriptFolder + " - was not found");

			_tsFolder = typeScriptFolder;
		}

		public virtual String GetExecutablePath()
		{
			// Find all installed version folders
			var tsVersionFolders = Directory.GetDirectories(_tsFolder);
			
			// Get all the versions ordered
			var tsVersions = GetOrderedVersions(tsVersionFolders).ToList();

			if (!tsVersions.Any())
				throw new Exception("ERROR: TypeScript installation couldn't be found in " + _tsFolder + " - Please make sure it is installed. Download http://www.typescriptlang.org/#Download");

			var executable = FindExecutable(tsVersions);

			if (executable == null)
				throw new FileNotFoundException("Could not find the executable");

			return executable;
		}

		protected virtual IEnumerable<String> GetOrderedVersions(IEnumerable<String> folders)
		{
			var tsVersions = new SortedList<float, String>(new InvertedComparer());
			foreach (var folder in folders)
			{
				// Remove the full path 
				var versionStr = folder.Replace(_tsFolder + "\\", "");
				
				float version;
				if (float.TryParse(versionStr, NumberStyles.Float, CultureInfo.InvariantCulture, out version))
				{
					tsVersions.Add(version, versionStr);
				}
			}

			return tsVersions.Select(x => x.Value);
		}

		protected virtual String FindExecutable(IEnumerable<String> versions)
		{
			foreach (var version in versions)
			{
				var versionFolder = Path.Combine(_tsFolder, version);
				var execPath = Path.Combine(versionFolder, "tsc.exe");

				if (File.Exists(execPath))
					return execPath;
			}

			return null;
		}

		private class InvertedComparer : IComparer<float>
		{
			public int Compare(float x, float y)
			{
				return y.CompareTo(x);
			}
		}
	}
}