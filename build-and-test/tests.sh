#!/bin/bash

cd tools
mono nunit/nuget.exe install NUnit
mono nunit/nuget.exe install NUnit.Runners
cd ..
mono /build/tools/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe  /build/Assembler.Tests/bin/Debug/Assembler.Tests.dll
