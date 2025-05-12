# Stage 1: Build
FROM bitnami/dotnet-sdk:8.0.408-debian-12-r5 AS build
WORKDIR /src


COPY ETLPipelineTool/ETLPipelineTool.csproj ETLPipelineTool/
COPY ETLPipelineTool.IntegrationTest/ETLPipelineTool.IntegrationTest.csproj ETLPipelineTool.IntegrationTest/

WORKDIR /src/ETLPipelineTool
RUN dotnet restore

WORKDIR /src
COPY . .

RUN dotnet publish ETLPipelineTool/ETLPipelineTool.csproj -c Release -o /app/publish --no-restore

# Stage 2: Runtime
FROM bitnami/aspnet-core:8.0.15-debian-12-r4
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ETLPipelineTool.dll"]
