# ---- Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["HelloApi.csproj", "."]
RUN dotnet restore "HelloApi.csproj"
COPY . .
RUN dotnet publish "HelloApi.csproj" -c Release -o /out /p:UseAppHost=false

# ---- Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Dossier Data + seed (reste dans l'image, pas masqué par le volume)
RUN mkdir -p /app/Data /app/seed
COPY Data/DBPersons.sqlite /app/seed/DBPersons.sqlite

# Copie **tous** les artefacts publiés
COPY --from=build /out/ /app/

# Garde trace pour debug si besoin
RUN ls -la /app && \
    test -e /app/HelloApi.dll || echo "WARN: HelloApi.dll introuvable (vérifier le nom d'assembly)"

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "/app/HelloApi.dll"]
