using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.TypeScriptCompiler.Tests
{
	[TestClass]
	public class TypeScriptCompilerOptionsTest
	{
		[TestMethod]
		public void CanCreateOptionsString()
		{
			var options = new TypeScriptV1CompilerOptions()
			{
				Codepage = "codepage",
				Declaration = true,
				MapRoot = "mapRoot",
				Module = "amd",
				NoImplicitAny = true,
				Out = "some.js",
				OutDir = "outDir",
				RemoveComments = true,
				SourceMap = true,
				SourceRoot = "sourceRoot",
				Target = TypeScriptCompilerTarget.ES5
			};

			var actualOptStr = options.ToOptionsString();

			var expectedOptStr = "--codepage codepage --declaration --mapRoot mapRoot --module amd --noImplicitAny --out some.js --outDir outDir --removeComments --sourceMap --sourceRoot sourceRoot --target ES5";

			Assert.AreEqual(expectedOptStr, actualOptStr);
		}
	}
}
