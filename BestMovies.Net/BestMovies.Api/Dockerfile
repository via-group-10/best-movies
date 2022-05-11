#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
LABEL name=best-movies-api
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "./BestMovies.Api/BestMovies.Api.csproj" "./BestMovies.Api/"
RUN dotnet restore "BestMovies.Api/BestMovies.Api.csproj"
COPY . .
RUN dotnet build "BestMovies.Api/BestMovies.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BestMovies.Api/BestMovies.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BestMovies.Api.dll"]