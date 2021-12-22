FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PolyclinicService.Server/PolyclinicService.Server.csproj", "PolyclinicService.Server/"]
COPY ["PolyclinicService.Domain/PolyclinicService.Domain.csproj", "PolyclinicService.Domain/"]
COPY ["PolyclinicService.Database/PolyclinicService.Database.csproj", "PolyclinicService.Database/"]
RUN dotnet restore "PolyclinicService.Server/PolyclinicService.Server.csproj"
COPY . .
WORKDIR "/src/PolyclinicService.Server"
RUN dotnet build "PolyclinicService.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PolyclinicService.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PolyclinicService.Server.dll"]
CMD ["dotnet", "PolyclinicService.Server.dll", "--server.urls", "http://+:$(PORT)"]