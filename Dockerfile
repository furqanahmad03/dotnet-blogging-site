# Stage 1: base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js once
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs

# Use proper caching for dependencies
WORKDIR /src

# Copy only csproj first
COPY ["X.csproj", "."]
RUN dotnet restore "./X.csproj"

# Copy package.json first to cache npm install
COPY package*.json ./
RUN npm install

# Finally copy rest of source code (only if actual code changes)
COPY . .

# Build project
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "./X.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "./X.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "X.dll"]
