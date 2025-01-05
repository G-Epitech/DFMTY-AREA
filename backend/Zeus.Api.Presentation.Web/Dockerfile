FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Zeus.Api.Presentation.Web/Zeus.Api.Presentation.Web.csproj", "Zeus.Api.Presentation.Web/"]
RUN dotnet restore "Zeus.Api.Presentation.Web/Zeus.Api.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/Zeus.Api.Presentation.Web"
RUN dotnet build "Zeus.Api.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Zeus.Api.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeus.Api.Presentation.Web.dll"]
