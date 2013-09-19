using System;

namespace Dynamo.TypeScriptCompiler
{
    public class TypeScriptCompilerOptions
    {
	    public TypeScriptCompilerOptions()
	    {
			// Defaults
		    Target = TypeScriptCompilerTarget.ES3;
		    AllowBool = false;
		    RemoveComments = false;
			SourceMap = false;
		    SaveToDisk = true;
	    }

	    public TypeScriptCompilerTarget Target { get; set; }
	    public Boolean AllowBool { get; set; }
	    public Boolean RemoveComments { get; set; }
	    public Boolean SourceMap { get; set; }
	    public Boolean SaveToDisk { get; set; }
	}
}
