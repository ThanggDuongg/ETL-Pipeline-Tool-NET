# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ETLPipelineTool/ETLPipelineTool.sln ETLPipelineTool/
COPY ETLPipelineTool/ETLPipelineTool.csproj ETLPipelineTool/
COPY ETLPipelineTool.IntegrationTest/ETLPipelineTool.IntegrationTest.csproj ETLPipelineTool.IntegrationTest/

WORKDIR /src/ETLPipelineTool
RUN dotnet restore ETLPipelineTool.sln

WORKDIR /src
COPY . .

RUN dotnet publish ETLPipelineTool/ETLPipelineTool.csproj -c Release -o /app/publish /p:RestoreFallbackFolders=""

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ETLPipelineTool.dll"]
