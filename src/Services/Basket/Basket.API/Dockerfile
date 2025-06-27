# --- Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# --- Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the project files first (for better Docker layer caching)
COPY ["src/Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
RUN dotnet restore "Services/Basket/Basket.API/Basket.API.csproj"

# Copy only the necessary project source directories explicitly
COPY src/Services/Basket/Basket.API/ Services/Basket/Basket.API/
COPY src/BuildingBlocks/ BuildingBlocks/

# Optional debug: confirm Program.cs is present and correct
RUN ls -la Services/Basket/Basket.API && cat Services/Basket/Basket.API/Program.cs

# Set working directory to the project directory
WORKDIR "/src/Services/Basket/Basket.API"

# Publish the application (no build step required)
RUN dotnet publish "Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
