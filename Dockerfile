FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Sample/Sample.csproj", "Sample/"]
RUN dotnet restore "Sample/Sample.csproj"
COPY . .
WORKDIR "/src/Sample"
RUN dotnet build "Sample.csproj" --no-restore -c Release -o /app
RUN chmod +x structure.sh
RUN ./structure.sh

FROM build AS publish
RUN dotnet publish "Sample.csproj" --no-restore --self-contained false -c Release -o /app
RUN cp wwwroot/structure.json /app/wwwroot/structure.json

FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
COPY --from=publish /app . 

EXPOSE 80
EXPOSE 443

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=443
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="1q2w3e4r5t"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="aspnetapp.pfx"

FROM base AS final
WORKDIR /app

ENTRYPOINT ["dotnet","Sample.dll"]
