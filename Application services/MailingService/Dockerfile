FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MailingService.Server/MailingService.Server.csproj", "MailingService.Server/"]
COPY ["MailingService.Domain/MailingService.Domain.csproj", "MailingService.Domain/"]
COPY ["MailingService.Database/MailingService.Database.csproj", "MailingService.Database/"]
RUN dotnet restore "MailingService.Server/MailingService.Server.csproj"
COPY . .
WORKDIR "/src/MailingService.Server"
RUN dotnet build "MailingService.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MailingService.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MailingService.Server.dll"]
CMD ["dotnet", "MailingService.Server.dll", "--server.urls", "http://+:$(PORT)"]