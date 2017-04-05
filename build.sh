#!/bin/bash
set -ev

# ---------------------------------
# BUILD
# ---------------------------------

dotnet restore
#dotnet run -f netcoreapp1.0 -c $CONFIGURATION -p ./src/SaasCore.Web/SaasCore.Web.xproj
dotnet build ./src/SaasCore.Web/ -c Release