#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ManuFacture.API/ManuFacture.API.csproj", "ManuFacture.API/"]
RUN dotnet restore "ManuFacture.API/ManuFacture.API.csproj"
COPY . .
WORKDIR "/src/ManuFacture.API"
RUN dotnet build "ManuFacture.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ManuFacture.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManuFacture.API.dll"]