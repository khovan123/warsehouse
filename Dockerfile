FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8081
ENV PORT=8081
ENV HTTP_PORTS=8081
ENV HTTPS_PORTS=8082

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY src/WarehouseBusiness/API/API.csproj src/WarehouseBusiness/API/
COPY src/WarehouseBusiness/Application/Application.csproj src/WarehouseBusiness/Application/
COPY src/WarehouseBusiness/Contract/Contract.csproj src/WarehouseBusiness/Contract/
COPY src/WarehouseBusiness/Domain/Domain.csproj src/WarehouseBusiness/Domain/
COPY src/WarehouseBusiness/Infrastructure/Infrastructure.csproj src/WarehouseBusiness/Infrastructure/

RUN dotnet restore "src/WarehouseBusiness/API/API.csproj"

COPY . .

WORKDIR "/app/src/WarehouseBusiness/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/app/src/WarehouseBusiness/API"
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN mkdir -p /var/keys
RUN mkdir -p /app/certs
COPY certs/ /app/certs

EXPOSE 8081

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
