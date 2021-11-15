FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Sample/Sample.csproj", "Sample/"]
RUN dotnet restore "Sample/Sample.csproj"
COPY . .
WORKDIR "/src/Sample"
RUN dotnet build "Sample.csproj" -c Release -o /app
RUN chmod +x structure.sh
RUN ./structure.sh

FROM build AS publish
RUN dotnet publish "Sample.csproj" --no-restore --self-contained false -c Release -o /app
RUN cp wwwroot/structure.json /app/wwwroot/structure.json

FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-buster-slim-amd64 AS base
WORKDIR /app
COPY --from=publish /app . 

EXPOSE 80
EXPOSE 443

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=443

FROM base AS final
WORKDIR /app

ENTRYPOINT ["dotnet","Sample.dll"]
