#!/bin/bash

cd tools
mono nuget/nuget.exe install NUnit
mono nuget/nuget.exe install NUnit.Runners
cd ..
mono /build/tools/NUnit.ConsoleRunner.*/tools/nunit3-console.exe  /build/Assembler.Tests/bin/Debug/Assembler.Tests.dll
