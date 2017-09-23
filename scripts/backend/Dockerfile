FROM microsoft/dotnet:2.0-sdk AS base
WORKDIR /app

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ./src/ .
RUN dotnet restore
WORKDIR /src/Settings
RUN dotnet build -c Release -o /app

FROM build as test
WORKDIR /src/Settings.Tests.Services
RUN dotnet test

FROM build AS publish
WORKDIR /src/Settings
RUN dotnet publish -c Release -o /app/published

FROM microsoft/dotnet:2.0-runtime-deps AS production
WORKDIR /app
COPY --from=publish /app/published .
RUN rm -rf *.pdb
EXPOSE 5000
ENTRYPOINT ["dotnet", "Settings.dll"]
