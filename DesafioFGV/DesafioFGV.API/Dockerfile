# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
RUN apk add --no-cache \
    icu-data-full \
    icu-libs
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DesafioFGV.API/DesafioFGV.API.csproj", "DesafioFGV.API/"]
COPY ["DesafioFGV.Application/DesafioFGV.Application.csproj", "DesafioFGV.Application/"]
COPY ["DesafioFGV.Domain/DesafioFGV.Domain.csproj", "DesafioFGV.Domain/"]
COPY ["DesafioFGV.Infrastructure/DesafioFGV.Infrastructure.csproj", "DesafioFGV.Infrastructure/"]
RUN dotnet restore "./DesafioFGV.API/DesafioFGV.API.csproj"
COPY . .
WORKDIR "/src/DesafioFGV.API"
RUN dotnet build "./DesafioFGV.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DesafioFGV.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
RUN apk add --no-cache \
    icu-data-full \
    icu-libs
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DesafioFGV.API.dll"]