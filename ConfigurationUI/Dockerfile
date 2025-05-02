
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY ConfigurationReader/*.csproj ./ConfigurationReader/
COPY ConfigurationUI/*.csproj ./ConfigurationUI/
COPY ExampleConsoleApp/ExampleConsoleApp.csproj ExampleConsoleApp/

RUN dotnet restore

COPY . .
WORKDIR /src/ConfigurationUI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "ConfigurationUI.dll"]
