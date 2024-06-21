#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Address/GraphQL.Address.API/GraphQL.Address.API.csproj", "Services/Address/GraphQL.Address.API/"]
COPY ["BuildingBlocks/GraphQL.EventBus.Messages/GraphQL.EventBus.Messages.csproj", "BuildingBlocks/GraphQL.EventBus.Messages/"]
COPY ["Services/Address/GraphQL.Address.Application/GraphQL.Address.Application.csproj", "Services/Address/GraphQL.Address.Application/"]
COPY ["Services/Address/GraphQL.Address.Domain/GraphQL.Address.Domain.csproj", "Services/Address/GraphQL.Address.Domain/"]
COPY ["Services/Address/GraphQL.Address.Infrastructure/GraphQL.Address.Infrastructure.csproj", "Services/Address/GraphQL.Address.Infrastructure/"]
RUN dotnet restore "./Services/Address/GraphQL.Address.API/GraphQL.Address.API.csproj"
COPY . .
WORKDIR "/src/Services/Address/GraphQL.Address.API"
RUN dotnet build "./GraphQL.Address.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GraphQL.Address.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQL.Address.API.dll"]