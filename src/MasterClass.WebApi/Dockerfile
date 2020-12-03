FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY src/MasterClass.WebApi/MasterClass.WebApi.csproj src/MasterClass.WebApi/
RUN dotnet restore src/MasterClass.WebApi/MasterClass.WebApi.csproj
COPY . .
WORKDIR /src/src/MasterClass.WebApi
RUN dotnet build MasterClass.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MasterClass.WebApi.csproj -c Release -o /app

FROM base AS final

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT dotnet MasterClass.WebApi.dll