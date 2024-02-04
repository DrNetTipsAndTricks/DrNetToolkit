
powershell "$Env:DrNetToolkitNugetAPIKEY = 'APIKEY'"
powershell "[Environment]::SetEnvironmentVariable('DrNetToolkitNugetAPIKEY', 'APIKEY', 'User')"

SET NugetPackage="PACKAGE"

dotnet nuget push %NugetPackage% --api-key %DrNetToolkitNugetAPIKEY% --source https://api.nuget.org/v3/index.json
