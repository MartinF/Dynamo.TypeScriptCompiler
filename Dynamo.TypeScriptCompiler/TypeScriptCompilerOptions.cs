using System;

namespace Dynamo.TypeScriptCompiler
{
    public class TypeScriptCompilerOptions
    {
	    public TypeScriptCompilerOptions()
	    {
			// Defaults
		    Target = Target.ES3;
		    AllowBool = false;
		    RemoveComments = false;
			GenerateSourceMap = false;
		    SaveToDisk = true;
	    }

	    public Target Target { get; set; }
	    public Boolean AllowBool { get; set; }
	    public Boolean RemoveComments { get; set; }
	    public Boolean GenerateSourceMap { get; set; }
	    public Boolean SaveToDisk { get; set; }
	}
}
