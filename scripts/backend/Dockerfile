FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/aspnetcore-build:2.0 AS builder
WORKDIR /src
COPY ./src/ .
RUN dotnet restore
WORKDIR /src/Settings
RUN dotnet build -o /app

FROM builder as test
WORKDIR /src/Settings.Tests.Services
RUN dotnet test

FROM builder AS publish
RUN dotnet publish -o /app

FROM base AS production
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Settings.dll"]
