dotnet build DisciplesClient-Update-Service --configuration Release  -o .\d2service
@del .\d2service\*.pdb
@del .\d2service\appsettings.Development.json
@del .\d2service\web.config

@rmdir /S /Q ..\builded
@mkdir ..\builded
@copy .\d2service\*.* ..\builded
@copy ..\deploysettings\*.* ..\builded\*.* /Y
@mkdir ..\builded\info
@copy ..\deploysettings\info\*.* ..\builded\info\*.* /Y
@rmdir /S /Q .\d2service
pause