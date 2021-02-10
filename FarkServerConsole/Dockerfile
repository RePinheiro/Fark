FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

COPY ./bin/Release/netcoreapp3.1/publish .

ENTRYPOINT ["dotnet", "FarkServerConsole.dll"]
