FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5069
EXPOSE 7015

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Zeus.Api.gRPC/Zeus.Api.gRPC.csproj", "Zeus.Api.gRPC/"]
RUN dotnet restore "Zeus.Api.gRPC/Zeus.Api.gRPC.csproj"
COPY . .
WORKDIR "/src/Zeus.Api.gRPC"
RUN dotnet build "Zeus.Api.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Zeus.Api.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeus.Api.gRPC.dll", "--urls", "http://*:5069"]
