FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PaymentService.Server/PaymentService.Server.csproj", "PaymentService.Server/"]
COPY ["PaymentService.Domain/PaymentService.Domain.csproj", "PaymentService.Domain/"]
COPY ["PaymentService.Database/PaymentService.Database.csproj", "PaymentService.Database/"]
RUN dotnet restore "PaymentService.Server/PaymentService.Server.csproj"
COPY . .
WORKDIR "/src/PaymentService.Server"
RUN dotnet build "PaymentService.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PaymentService.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaymentService.Server.dll"]
CMD ["dotnet", "PaymentService.Server.dll", "--server.urls", "https://+:$(PORT)"]
#COPY . .
#WORKDIR "/src/PaymentService.Server"
#RUN dotnet build "PaymentService.Server.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "PaymentService.Server.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PaymentService.Server.dll"]
