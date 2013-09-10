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
			//GenerateSourceMap = false;
	    }

	    public Target Target { get; set; }
	    public Boolean AllowBool { get; set; }
	    public Boolean RemoveComments { get; set; }
	    //public Boolean GenerateSourceMap { get; set; }

		// Save file to disk (keep it - instead of reading it into memory and deleting it again)?
    }
}
