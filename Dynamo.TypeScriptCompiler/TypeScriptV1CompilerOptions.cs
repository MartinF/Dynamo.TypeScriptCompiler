using System;

namespace Dynamo.TypeScriptCompiler
{
    public class TypeScriptV1CompilerOptions : ITypeScriptOptions
    {
	    public TypeScriptV1CompilerOptions()
	    {
			// Defaults
			Codepage = null;
		    Declaration = false;
		    MapRoot = null;
			Module = null;
		    NoImplicitAny = false;
			Out = null;
			OutDir = null;
			RemoveComments = false;
			SourceMap = false;
		    SourceRoot = null;
			Target = null;
	    }

		/// <summary>
		/// Specify the codepage to use when opening source files.
		/// </summary>
	    public String Codepage { get; set; }
		/// <summary>
		/// Generates corresponding .d.ts file.
		/// </summary>
	    public Boolean Declaration { get; set; }
		/// <summary>
		/// Specifies the location where debugger should locate map files instead of generated location.
		/// </summary>
	    public String MapRoot { get; set; }
		/// <summary>
		/// Specify module code generation: "commonjs" or "amd".
		/// </summary>
	    public String Module { get; set; }
		/// <summary>
		/// Wan on expressions and declarations with an implied 'any' type.
		/// </summary>
		public Boolean NoImplicitAny { get; set; }
		/// <summary>
		/// Concatenate and emit output to single file.
		/// </summary>
	    public String Out { get; set; }
		/// <summary>
		/// Redirect output structure to the directory.
		/// </summary>
	    public String OutDir { get; set; }
		/// <summary>
		/// Do not emit comments to output.
		/// </summary>
		public Boolean RemoveComments { get; set; }
		/// <summary>
		/// Generates corresponding .map file.
		/// </summary>
		public Boolean SourceMap { get; set; }
		/// <summary>
		/// Specifies the location where debugger should locate TypeScript files intead of source locations.
		/// </summary>
	    public String SourceRoot { get; set; }
		/// <summary>
		/// Specify ECMAScript target version: 'ES3' (default), or 'ES5'.
		/// </summary>
	    public TypeScriptCompilerTarget? Target { get; set; }

		public string ToOptionsString()
		{
			var options = "";

			TypeScriptOptionUtility.Add(ref options, Codepage, "--codepage");
			TypeScriptOptionUtility.Add(ref options, Declaration, "--declaration");
			TypeScriptOptionUtility.Add(ref options, MapRoot, "--mapRoot");
			TypeScriptOptionUtility.Add(ref options, Module, "--module");
			TypeScriptOptionUtility.Add(ref options, NoImplicitAny, "--noImplicitAny");
			TypeScriptOptionUtility.Add(ref options, Out, "--out");
			TypeScriptOptionUtility.Add(ref options, OutDir, "--outDir");
			TypeScriptOptionUtility.Add(ref options, RemoveComments, "--removeComments");
			TypeScriptOptionUtility.Add(ref options, SourceMap, "--sourceMap");
			TypeScriptOptionUtility.Add(ref options, SourceRoot, "--sourceRoot");
			TypeScriptOptionUtility.Add(ref options, Target, "--mapRoot");

			return options;
		}
	}
}
