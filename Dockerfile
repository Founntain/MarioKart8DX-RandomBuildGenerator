FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MkBuildBot.csproj", "MkBuildBot/"]
RUN dotnet restore "MkBuildBot/MkBuildBot.csproj"
COPY . .
WORKDIR "/src/MkBuildBot"
RUN dotnet build "MkBuildBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MkBuildBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MkBuildBot.dll"]
