using System;

namespace Dynamo.TypeScriptCompiler
{
    public class TypeScriptCompilerOptions
    {
	    public TypeScriptCompilerOptions()
	    {
			// Defaults
		    Declaration = false;
		    MapRoot = null;
		    NoImplicitAny = false;
		    NoResolve = false;
			RemoveComments = false;
			SourceMap = false;
		    SourceRoot = null;
			Target = TypeScriptCompilerTarget.ES3;
		    SaveToDisk = false;
	    }

	    public Boolean Declaration { get; set; }
	    public String MapRoot { get; set; }
		public Boolean NoImplicitAny { get; set; }
		public Boolean NoResolve { get; set; }
		public Boolean RemoveComments { get; set; }
		public Boolean SourceMap { get; set; }
	    public String SourceRoot { get; set; }
	    public TypeScriptCompilerTarget Target { get; set; }
	    public Boolean SaveToDisk { get; set; }
	}
}
