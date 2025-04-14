FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src/WisielecDiscordBotapp

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /src/WisielecDiscordBotapp
COPY --from=build /src/WisielecDiscordBot/app/out .

ENTRYPOINT ["dotnet", "WisielecDiscordBot.dll"]