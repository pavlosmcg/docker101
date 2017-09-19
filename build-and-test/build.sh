#!/bin/bash

mono tools/nuget/nuget.exe restore
msbuild
