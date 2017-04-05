#!/usr/bin/env bash
# using from MaxMind-DB-Reader-dotnet

cd `dirname $0`/..

if [ -n "$DOTNETCORE" ]; then

  echo Using .NET CLI

  dotnet restore

  # Running Benchmark
  dotnet run -f netcoreapp1.0 -c $CONFIGURATION -p ./src/SaasCore.Web/SaasCore.Web.xproj
 
fi