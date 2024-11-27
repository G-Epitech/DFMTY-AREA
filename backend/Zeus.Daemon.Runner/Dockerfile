FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Zeus.Daemon.Runner/Zeus.Daemon.Runner.csproj", "Zeus.Daemon.Runner/"]
RUN dotnet restore "Zeus.Daemon.Runner/Zeus.Daemon.Runner.csproj"
COPY . .
WORKDIR "/src/Zeus.Daemon.Runner"
RUN dotnet build "Zeus.Daemon.Runner.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Zeus.Daemon.Runner.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeus.Daemon.Runner.dll"]
