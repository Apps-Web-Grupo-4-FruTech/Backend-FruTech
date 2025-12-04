# Etapa de construcción (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar el csproj y restaurar dependencias
# Ajustamos la ruta basándonos en tu estructura de carpetas
COPY ["FruTech.Backend.API/FruTech.Backend.API.csproj", "FruTech.Backend.API/"]
RUN dotnet restore "FruTech.Backend.API/FruTech.Backend.API.csproj"

# Copiar el resto del código
COPY . .
WORKDIR "/src/FruTech.Backend.API"

# Construir y publicar la aplicación
RUN dotnet build "FruTech.Backend.API.csproj" -c Release -o /app/build
RUN dotnet publish "FruTech.Backend.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render asigna el puerto mediante la variable de entorno PORT (por defecto 10000 o 8080)
# Configuramos ASP.NET para escuchar en el puerto correcto
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FruTech.Backend.API.dll"]
