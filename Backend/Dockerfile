FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /DockerSource

COPY ../. .
RUN dotnet restore API/API.csproj
RUN dotnet publish API/API.csproj -c Debug -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY --from=build-env /DockerSource/out .
RUN apt-get update && apt-get install -y libc6-dev libgdiplus

ENTRYPOINT ["dotnet", "API.dll"]

