using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.TypeScriptCompiler.Tests
{
	[TestClass]
	public class TypeScriptExecutableResolverTest
	{
		[TestMethod]
		public void CanCreateResolverAndFindExec()
		{
			var exec = "C:\\Program Files (x86)\\Microsoft SDKs\\TypeScript\\1.0\\tsc.exe";
			var resolver = new TypeScriptExecutableResolver(exec);

			Assert.AreEqual(exec, resolver.GetExecutablePath());
		}
	}
}
