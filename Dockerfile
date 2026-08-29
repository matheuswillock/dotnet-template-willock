FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/CleanArchTemplate.Api/CleanArchTemplate.Api.csproj", "src/CleanArchTemplate.Api/"]
COPY ["src/CleanArchTemplate.Application/CleanArchTemplate.Application.csproj", "src/CleanArchTemplate.Application/"]
COPY ["src/CleanArchTemplate.Domain/CleanArchTemplate.Domain.csproj", "src/CleanArchTemplate.Domain/"]
COPY ["src/CleanArchTemplate.Infrastructure/CleanArchTemplate.Infrastructure.csproj", "src/CleanArchTemplate.Infrastructure/"]
RUN dotnet restore "src/CleanArchTemplate.Api/CleanArchTemplate.Api.csproj"
COPY . .
RUN dotnet publish "src/CleanArchTemplate.Api/CleanArchTemplate.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CleanArchTemplate.Api.dll"]
