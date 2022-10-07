#!/bin/bash

cd tools
mono nuget/nuget.exe install NUnit
mono nuget/nuget.exe install NUnit.Runners
cd ..
mono /build/tools/NUnit.ConsoleRunner.3.12.0/tools/nunit3-console.exe  /build/Assembler.Tests/bin/Debug/Assembler.Tests.dll
