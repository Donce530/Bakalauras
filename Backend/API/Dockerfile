FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY bin/Debug/net5.0/publish/ App/
WORKDIR /App

RUN apt-get update && apt-get install -y libc6-dev libgdiplus

ENTRYPOINT ["dotnet", "API.dll", "--environment=Development"]

