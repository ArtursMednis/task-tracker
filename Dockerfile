# =========================
# 1. Build Angular frontend
# =========================
FROM node:20-alpine AS frontend-build
WORKDIR /frontend

# Copy package files first (better caching)
COPY frontend/task-tracker/package*.json ./
RUN npm ci

# Copy the rest of the frontend
COPY frontend/task-tracker/ ./

# Build Angular (adjust if you use a custom config)
RUN npm run build -- --configuration production


# =========================
# 2. Build .NET backend
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build
WORKDIR /src

# Copy solution and restore
COPY backend/ ./
RUN dotnet restore

# Copy Angular build output into wwwroot
COPY --from=frontend-build /frontend/dist/task-tracker/browser TaskTracker.Api/wwwroot

RUN ls -R

# Publish API
RUN dotnet publish TaskTracker.Api/TaskTracker.Api.csproj \
    -c Release \
    -o /app/publish


# =========================
# 3. Runtime image
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published output
COPY --from=backend-build /app/publish .

# ASP.NET Core listens on 8080 by default in containers
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskTracker.Api.dll"]
