using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO: Write more tests

namespace Dynamo.TypeScriptCompiler.Tests
{
	[TestClass]
	public class TypeScriptCompilerTest
	{
		[TestMethod]
		public void CanCompileTypeScript()
		{
		    var filePath = PathHelper.GetScript("simple.ts");

			var options = new TypeScriptCompilerOptions()
			{
				SaveToDisk = false
			};

			var compiler = new TypeScriptCompiler(options);
			var result = compiler.Compile(filePath);

			Assert.AreEqual(result.ExitCode, 0);
            Assert.AreEqual(result.Source, "\r\n// Module\r\nvar Shapes;\r\n(function (Shapes) {\r\n    // Class\r\n    var Point = (function () {\r\n        // Constructor\r\n        function Point(x, y) {\r\n            this.x = x;\r\n            this.y = y;\r\n        }\r\n        // Instance member\r\n        Point.prototype.getDist = function () {\r\n            return Math.sqrt(this.x * this.x + this.y * this.y);\r\n        };\r\n\r\n        Point.origin = new Point(0, 0);\r\n        return Point;\r\n    })();\r\n    Shapes.Point = Point;\r\n})(Shapes || (Shapes = {}));\r\n\r\n// Local variables\r\nvar p = new Shapes.Point(3, 4);\r\nvar dist = p.getDist();\r\n");
			Assert.AreEqual(result.SourceMap, null);
		}

		[TestMethod]
		public void CanCompileTypeScriptWithSourceMap()
		{
			var filePath = PathHelper.GetScript("simple.ts");

			var options = new TypeScriptCompilerOptions()
			{
				SaveToDisk = false,
				SourceMap = true
			};

			var compiler = new TypeScriptCompiler(options);
			var result = compiler.Compile(filePath);

			Assert.AreEqual(result.ExitCode, 0);
            Assert.AreEqual(result.Source, "\r\n// Module\r\nvar Shapes;\r\n(function (Shapes) {\r\n    // Class\r\n    var Point = (function () {\r\n        // Constructor\r\n        function Point(x, y) {\r\n            this.x = x;\r\n            this.y = y;\r\n        }\r\n        // Instance member\r\n        Point.prototype.getDist = function () {\r\n            return Math.sqrt(this.x * this.x + this.y * this.y);\r\n        };\r\n\r\n        Point.origin = new Point(0, 0);\r\n        return Point;\r\n    })();\r\n    Shapes.Point = Point;\r\n})(Shapes || (Shapes = {}));\r\n\r\n// Local variables\r\nvar p = new Shapes.Point(3, 4);\r\nvar dist = p.getDist();\r\n//# sourceMappingURL=simple.js.map\r\n");
            Assert.AreEqual(result.SourceMap, "{\"version\":3,\"file\":\"simple.js\",\"sourceRoot\":\"\",\"sources\":[\"simple.ts\"],\"names\":[\"Shapes\",\"Shapes.Point\",\"Shapes.Point.constructor\",\"Shapes.Point.getDist\"],\"mappings\":\";AAKA,SAAS;AACT,IAAO,MAAM;AAaZ,CAbD,UAAO,MAAM;IAETA,QAAQA;IACRA;QAEIC,cADcA;QACdA,eAAaA,CAAgBA,EAAEA,CAAgBA;YAAlCC,MAAQA,GAADA,CAACA;AAAQA,YAAEA,MAAQA,GAADA,CAACA;AAAQA,QAAIA,CAACA;QAGpDD,kBADYA;kCACZA;YAAYE,OAAOA,IAAIA,CAACA,IAAIA,CAACA,IAAIA,CAACA,CAACA,GAAGA,IAAIA,CAACA,CAACA,GAAGA,IAAIA,CAACA,CAACA,GAAGA,IAAIA,CAACA,CAACA,CAACA;QAAEA,CAACA;;QAGlEF,eAAgBA,IAAIA,KAAKA,CAACA,CAACA,EAAEA,CAACA,CAACA;QACnCA,aAACA;IAADA,CAACA,IAAAD;IATDA,qBASCA;AACLA,CAACA,2BAAA;;AAED,kBAAkB;AAClB,IAAI,CAAC,GAAW,IAAI,MAAM,CAAC,KAAK,CAAC,CAAC,EAAE,CAAC,CAAC;AACtC,IAAI,IAAI,GAAG,CAAC,CAAC,OAAO,CAAC,CAAC\"}");
		}

		[TestMethod]
		public void CompilerResultContainsError()
		{
			var filePath = PathHelper.GetScript("error.ts");

			var options = new TypeScriptCompilerOptions()
			{
				SaveToDisk = false
			};

			var compiler = new TypeScriptCompiler(options);
			var result = compiler.Compile(filePath);

			Assert.IsTrue(result.HasError);
			Assert.IsFalse(String.IsNullOrEmpty(result.Error));
		}
	}
}
