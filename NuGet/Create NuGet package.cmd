NuGet.exe update -self

msbuild.exe ..\Dynamo.TypeScriptCompiler.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

NuGet.exe pack Dynamo.TypeScriptCompiler.nuspec

pause