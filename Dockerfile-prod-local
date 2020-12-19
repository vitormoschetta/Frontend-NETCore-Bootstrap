# producao local
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY Projeto/bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Projeto.dll"]
