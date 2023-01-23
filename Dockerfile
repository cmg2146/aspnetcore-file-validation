FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim

ARG NUGET_PUSH_SOURCE=https://api.nuget.org/v3/index.json
ARG NUGET_API_KEY

WORKDIR /app/src
COPY ./src/*.csproj ./
RUN ["dotnet", "restore"]
COPY ./src ./

# run tests
WORKDIR /app/test
COPY ./test/*.csproj ./
RUN ["dotnet", "restore"]
COPY ./test ./
RUN ["dotnet", "test"]

# build and push package to nuget repository
WORKDIR /app/src
RUN ["dotnet", "pack", "-c", "Release", "--no-restore"]
RUN dotnet nuget push "*.nupkg" -s $NUGET_PUSH_SOURCE -k $NUGET_API_KEY
