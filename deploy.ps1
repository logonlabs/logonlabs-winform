Set-StrictMode -Version Latest
$ProgressPreference = "SilentlyContinue"
$versionGrab = Get-Content .\version.txt -First 1

#Remove-Item -Force -Recurse D:\home\site\repository\src\LogonLabs.Client\nuget
Write-Host "Step One"
dotnet pack ".\src\LogonLabs.Client.WinForms\LogonLabs.Client.WinForms.csproj" -p:PackageVersion=$versionGrab -c Debug   -o nuget
# $ENV:buildMode
Write-Host "Step Two"
#dotnet nuget push ".\src\LogonLabs.Client.Winforms\bin\$ENV:buildMode\LogonLabs.Client.WinForms.$versionGrab.nupkg" -k $ENV:MyGetApiKey -s $ENV:MyGetApiUrl
dotnet nuget push ".\nuget\LogonLabs.Client.WinForms.$versionGrab.nupkg" -k $ENV:MyGetApiKey -s $ENV:MyGetApiUrl