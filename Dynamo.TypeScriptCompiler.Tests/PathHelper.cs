using System;
using System.IO;

namespace Dynamo.TypeScriptCompiler.Tests
{
    public static class PathHelper
    {
        private static readonly String _scriptPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Scripts\");
 
        public static String GetScript(String scriptName)
        {
            return Path.Combine(_scriptPath, scriptName);
        }
    }
}
