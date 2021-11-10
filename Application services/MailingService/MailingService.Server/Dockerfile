FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MailingService.Server/MailingService.Server.csproj", "MailingService.Server/"]
RUN dotnet restore "MailingService.Server/MailingService.Server.csproj"
COPY . .
WORKDIR "/src/MailingService.Server"
RUN dotnet build "MailingService.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MailingService.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MailingService.Server.dll"]
