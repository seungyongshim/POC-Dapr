#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorApp1/Server/BlazorApp1.Server.csproj", "BlazorApp1/Server/"]
COPY ["BlazorApp1/Client/BlazorApp1.Client.csproj", "BlazorApp1/Client/"]
COPY ["BlazorApp1/Shared/BlazorApp1.Shared.csproj", "BlazorApp1/Shared/"]
RUN dotnet restore "BlazorApp1/Server/BlazorApp1.Server.csproj"
COPY . .
WORKDIR "/src/BlazorApp1/Server"
RUN dotnet build "BlazorApp1.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp1.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorApp1.Server.dll"]
