# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["FruTech.Backend.API/FruTech.Backend.API.csproj", "FruTech.Backend.API/"]
RUN dotnet restore "FruTech.Backend.API/FruTech.Backend.API.csproj"

# Copiar el resto del código
COPY . .
WORKDIR "/src/FruTech.Backend.API"
RUN dotnet build "FruTech.Backend.API.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "FruTech.Backend.API.csproj" -c Release -o /app/publish

# Etapa final / Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Puerto que Railway espera (puedes usar la variable PORT)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FruTech.Backend.API.dll"]