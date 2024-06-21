#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Person/GraphQL.Person.API/GraphQL.Person.API.csproj", "Services/Person/GraphQL.Person.API/"]
COPY ["BuildingBlocks/GraphQL.EventBus.Messages/GraphQL.EventBus.Messages.csproj", "BuildingBlocks/GraphQL.EventBus.Messages/"]
COPY ["Services/Person/GraphQL.Person.Application/GraphQL.Person.Application.csproj", "Services/Person/GraphQL.Person.Application/"]
COPY ["Services/Person/GraphQL.Person.Domain/GraphQL.Person.Domain.csproj", "Services/Person/GraphQL.Person.Domain/"]
COPY ["Services/Person/GraphQL.Person.Infrastructure/GraphQL.Person.Infrastructure.csproj", "Services/Person/GraphQL.Person.Infrastructure/"]
RUN dotnet restore "./Services/Person/GraphQL.Person.API/GraphQL.Person.API.csproj"
COPY . .
WORKDIR "/src/Services/Person/GraphQL.Person.API"
RUN dotnet build "./GraphQL.Person.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GraphQL.Person.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQL.Person.API.dll"]