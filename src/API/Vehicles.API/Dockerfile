#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release

#Add GitHub NuGet sources
ENV GITHUB_PASSWORD=password

WORKDIR /src
COPY ["src/API/Vehicles.API/Vehicles.API.csproj", "src/API/Vehicles.API/"]
COPY ["src/Infrastructure/Vehicles.GraphQL/Vehicles.GraphQL.csproj", "src/Infrastructure/Vehicles.GraphQL/"]
COPY ["src/Core/Vehicles.Core/Vehicles.Core.csproj", "src/Core/Vehicles.Core/"]
COPY ["src/Infrastructure/Vehicles.Persistence/Vehicles.Persistence.csproj", "src/Infrastructure/Vehicles.Persistence/"]
RUN dotnet nuget add source -n github -u AZ -p ${GITHUB_PASSWORD} --store-password-in-clear-text https://nuget.pkg.github.com/prafaelmsantos/index.json
RUN dotnet restore "./src/API/Vehicles.API/Vehicles.API.csproj"
COPY . .
WORKDIR "/src/src/API/Vehicles.API"
RUN dotnet build "./Vehicles.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Vehicles.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vehicles.API.dll"]