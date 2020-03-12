FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Contoso.Online.Orders.sln", "."]
COPY ["Client/Contoso.Online.Orders.Client.csproj", "Client/"]
COPY ["Server/Contoso.Online.Orders.Server.csproj", "Server/"]
COPY ["Shared/Contoso.Online.Orders.Shared.csproj", "Shared/"]
COPY . .
WORKDIR "/src"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contoso.Online.Orders.Server.dll"]