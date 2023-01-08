dotnet build D2Launcher --configuration Release  -o .\launcher
@rmdir /S /Q ..\buildedlauncher
@mkdir ..\buildedlauncher
@copy .\launcher\*.* ..\buildedlauncher
@mkdir ..\buildedlauncher\config
@copy .\D2Launcher\config ..\buildedlauncher\config
@rmdir /S /Q .\launcher
pause