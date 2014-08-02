using System;
using System.IO;
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

			var options = new TypeScriptV1CompilerOptions()
			{
			};

			var compiler = new TypeScriptCompiler();
			var result = compiler.Compile(new[] { filePath }, options);

			Assert.IsTrue(result.IsSuccess);

			var outFile = PathHelper.GetScript("simple.js");
			var resultSource = File.ReadAllText(PathHelper.GetScript(outFile));

			var expectedResult = "\r\n// Module\r\nvar Shapes;\r\n(function (Shapes) {\r\n    // Class\r\n    var Point = (function () {\r\n        // Constructor\r\n        function Point(x, y) {\r\n            this.x = x;\r\n            this.y = y;\r\n        }\r\n        // Instance member\r\n        Point.prototype.getDist = function () {\r\n            return Math.sqrt(this.x * this.x + this.y * this.y);\r\n        };\r\n\r\n        Point.origin = new Point(0, 0);\r\n        return Point;\r\n    })();\r\n    Shapes.Point = Point;\r\n})(Shapes || (Shapes = {}));\r\n\r\n// Local variables\r\nvar p = new Shapes.Point(3, 4);\r\nvar dist = p.getDist();\r\n";

			Assert.AreEqual(expectedResult, resultSource);

			// Clean up 
			File.Delete(outFile);
		}

		[TestMethod]
		public void CanCompileTypeScriptWithSourceMap()
		{
			var filePath = PathHelper.GetScript("simple.ts");

			var options = new TypeScriptV1CompilerOptions()
			{
				SourceMap = true
			};

			var compiler = new TypeScriptCompiler();
			var result = compiler.Compile(new[] { filePath }, options);

			Assert.IsTrue(result.IsSuccess);

			var outFile = PathHelper.GetScript("simple.js");
			var outMap = PathHelper.GetScript("simple.js.map");

			var resultSource = File.ReadAllText(outFile);
			var resultMap = File.ReadAllText(outMap);

			var expectedResult = "\r\n// Module\r\nvar Shapes;\r\n(function (Shapes) {\r\n    // Class\r\n    var Point = (function () {\r\n        // Constructor\r\n        function Point(x, y) {\r\n            this.x = x;\r\n            this.y = y;\r\n        }\r\n        // Instance member\r\n        Point.prototype.getDist = function () {\r\n            return Math.sqrt(this.x * this.x + this.y * this.y);\r\n        };\r\n\r\n        Point.origin = new Point(0, 0);\r\n        return Point;\r\n    })();\r\n    Shapes.Point = Point;\r\n})(Shapes || (Shapes = {}));\r\n\r\n// Local variables\r\nvar p = new Shapes.Point(3, 4);\r\nvar dist = p.getDist();\r\n//# sourceMappingURL=simple.js.map\r\n";
			var expectedMap = "{\"version\":3,\"file\":\"simple.js\",\"sourceRoot\":\"\",\"sources\":[\"simple.ts\"],\"names\":[\"Shapes\",\"Shapes.Point\",\"Shapes.Point.constructor\",\"Shapes.Point.getDist\"],\"mappings\":\";AAKA,SAAS;AACT,IAAO,MAAM;AAaZ,CAbD,UAAO,MAAM;IAETA,QAAQA;IACRA;QAEIC,cADcA;QACdA,eAAaA,CAAgBA,EAAEA,CAAgBA;YAAlCC,MAAQA,GAADA,CAACA;AAAQA,YAAEA,MAAQA,GAADA,CAACA;AAAQA,QAAIA,CAACA;QAGpDD,kBADYA;kCACZA;YAAYE,OAAOA,IAAIA,CAACA,IAAIA,CAACA,IAAIA,CAACA,CAACA,GAAGA,IAAIA,CAACA,CAACA,GAAGA,IAAIA,CAACA,CAACA,GAAGA,IAAIA,CAACA,CAACA,CAACA;QAAEA,CAACA;;QAGlEF,eAAgBA,IAAIA,KAAKA,CAACA,CAACA,EAAEA,CAACA,CAACA;QACnCA,aAACA;IAADA,CAACA,IAAAD;IATDA,qBASCA;AACLA,CAACA,2BAAA;;AAED,kBAAkB;AAClB,IAAI,CAAC,GAAW,IAAI,MAAM,CAAC,KAAK,CAAC,CAAC,EAAE,CAAC,CAAC;AACtC,IAAI,IAAI,GAAG,CAAC,CAAC,OAAO,CAAC,CAAC\"}";

            Assert.AreEqual(expectedResult, resultSource);
            Assert.AreEqual(expectedMap, resultMap);
		
			// Clean up 
			File.Delete(outFile);
			File.Delete(outMap);
		}

		[TestMethod]
		public void CompilerResultContainsError()
		{
			var filePath = PathHelper.GetScript("error.ts");

			var options = new TypeScriptV1CompilerOptions()
			{
			};

			var compiler = new TypeScriptCompiler();
			var result = compiler.Compile(new[] { filePath }, options);

			Assert.IsFalse(result.IsSuccess);
			Assert.IsFalse(String.IsNullOrEmpty(result.Error));
		}

		[TestMethod]
		public void CanCompileMultipleTypeScript()
		{
			var filePath1 = PathHelper.GetScript("A.ts");
			var filePath2 = PathHelper.GetScript("Q.ts");

			var options = new TypeScriptV1CompilerOptions()
			{
				Target = TypeScriptCompilerTarget.ES5
			};

			var compiler = new TypeScriptCompiler();
			var result = compiler.Compile(new[] { filePath1, filePath2 }, options);

			Assert.IsTrue(result.IsSuccess);

			var outFile1 = PathHelper.GetScript("A.js");
			var outFile2 = PathHelper.GetScript("Q.js");
			var resultSource1 = File.ReadAllText(outFile1);
			var resultSource2 = File.ReadAllText(outFile2);

			var expectedResult1 = "var A;\r\n(function (A) {\r\n    var Foo = (function () {\r\n        function Foo() {\r\n        }\r\n        Foo.prototype.bar = function () {\r\n            alert(\"Foo\");\r\n        };\r\n        return Foo;\r\n    })();\r\n    A.Foo = Foo;\r\n})(A || (A = {}));\r\n";
			var expectedResult2 = "var Q;\r\n(function (Q) {\r\n    var Bar = (function () {\r\n        function Bar() {\r\n        }\r\n        Bar.prototype.bar = function () {\r\n            alert(\"bar\");\r\n        };\r\n\r\n        Object.defineProperty(Bar.prototype, \"getter\", {\r\n            get: function () {\r\n                return \"123\";\r\n            },\r\n            enumerable: true,\r\n            configurable: true\r\n        });\r\n        return Bar;\r\n    })();\r\n    Q.Bar = Bar;\r\n})(Q || (Q = {}));\r\n";

			Assert.AreEqual(expectedResult1, resultSource1);
			Assert.AreEqual(expectedResult2, resultSource2);

			// Clean up 
			File.Delete(outFile1);
			File.Delete(outFile2);
		}

		//[TestMethod]
		//public void CompilerTest()
		//{
		//	// With references ... 

		//	var filePath = PathHelper.GetScript("C.ts");

		//	var options = new TypeScriptV1CompilerOptions()
		//	{
		//	};

		//	var compiler = new TypeScriptCompiler();
		//	var result = compiler.Compile(new [] { filePath }, options);

		//	Assert.IsTrue(result.IsSuccess);

		//	// Check A,B,C .js ?
		//}
	}
}
