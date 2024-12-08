FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Zeus.sln", "./"]
COPY ["Zeus.Api.Application/Zeus.Api.Application.csproj", "Zeus.Api.Application/"]
COPY ["Zeus.Api.Domain/Zeus.Api.Domain.csproj", "Zeus.Api.Domain/"]
COPY ["Zeus.Api.Infrastructure/Zeus.Api.Infrastructure.csproj", "Zeus.Api.Infrastructure/"]
COPY ["Zeus.Api.Web/Zeus.Api.Web.csproj", "Zeus.Api.Web/"]
RUN dotnet restore "Zeus.Api.Web/Zeus.Api.Web.csproj"
COPY . .
WORKDIR "/src/Zeus.Api.Web"
RUN dotnet build "Zeus.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Zeus.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeus.Api.Web.dll"]

