@echo off

echo Creating directories...
mkdir NuGet\lib\net5.0

echo Copying .NET libraries...
copy Maikelsoft.Monads\bin\Release\net5.0\*.dll NuGet\lib\net5.0\
copy Maikelsoft.Monads\bin\Release\net5.0\*.xml NuGet\lib\net5.0\

echo Building NuGet package...
NuGet.exe pack NuGet\Maikelsoft.Monads.nuspec -OutputDirectory C:\Development\nuget_packages

pause
