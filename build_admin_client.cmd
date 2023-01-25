dotnet build AdminAndAuthorClient --configuration Release  -o .\client
@rmdir /S /Q ..\builded-admin-client
@mkdir ..\builded-admin-client
@copy .\client\*.* ..\builded-admin-client
@mkdir ..\builded-admin-client\config
@copy .\AdminAndAuthorClient\config ..\builded-admin-client\config /Y
@rmdir /S /Q .\client
pause