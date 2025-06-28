# --- Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# --- Build image (force x64/amd64 to avoid ARM64 protoc segfault)
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["src/Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["src/Services/Basket/Basket.API/Protos/discount.proto", "Services/Basket/Basket.API/Protos/discount.proto"]

RUN dotnet restore "Services/Basket/Basket.API/Basket.API.csproj"

# Copy source code explicitly (safer than COPY . .)
COPY src/BuildingBlocks "BuildingBlocks"
COPY src/Services/Basket/Basket.API "Services/Basket/Basket.API"

# Debug step – ensure Program.cs exists
RUN ls -la Services/Basket/Basket.API && cat Services/Basket/Basket.API/Program.cs

WORKDIR "/src/Services/Basket/Basket.API"

RUN dotnet publish "Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
