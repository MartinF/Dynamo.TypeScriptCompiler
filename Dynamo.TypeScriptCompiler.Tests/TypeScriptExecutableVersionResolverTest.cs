using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dynamo.TypeScriptCompiler.Tests
{
	[TestClass]
	public class TypeScriptExecutableVersionResolverTest
	{
		[TestMethod]
		public void CanFindExecutable()
		{
			var resolver = new TypeScriptExecutableVersionResolver();

			var path = resolver.GetExecutablePath();

			Assert.IsTrue(!String.IsNullOrEmpty(path));
		}
	}
}
