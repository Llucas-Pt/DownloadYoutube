# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app  # mudar de /src para /app

# Copia apenas o csproj e restaura pacotes
COPY ["YoutubeDownload.csproj", "./"]
RUN dotnet restore

# Copia tudo da pasta back
COPY Back/. .  
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5136
ENTRYPOINT ["dotnet", "YoutubeDownload.dll"]
