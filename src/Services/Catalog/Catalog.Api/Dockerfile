# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj files
COPY src/Services/Catalog/Catalog.Api/Catalog.Api.csproj ./
COPY src/BuildingBlocks/BuildingBlocks.csproj ../BuildingBlocks/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY src/Services/Catalog/Catalog.Api/. ./
COPY src/BuildingBlocks ../BuildingBlocks

# Publish
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
