# producao local / azure
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY src/bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "src.dll"]
