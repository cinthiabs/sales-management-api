FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5284

ENV ASPNETCORE_URLS=http://+:5284

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Api/sales-management-api.csproj", "src/Api/"]
RUN dotnet restore "src/Api/sales-management-api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "sales-management-api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "sales-management-api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sales-management-api.dll"]
