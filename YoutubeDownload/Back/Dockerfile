FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["YoutubeDownload.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# INSTALA FFMPEG NO LINUX (necessário para executar no Render)
RUN apt-get update && apt-get install -y ffmpeg

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV YOUTUBE_COOKIES="VISITOR_INFO1_LIVE=TCRjE9s0_4E; PREF=f6=40000000&tz=America.Sao_Paulo&f5=20000&f7=100; YSC=dV22YUFOXiA"

EXPOSE 8080
ENTRYPOINT ["dotnet", "YoutubeDownload.dll"]
