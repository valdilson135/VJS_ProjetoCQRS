#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Vjs.MVC/Vjs.MVC.csproj", "src/Vjs.MVC/"]
COPY ["src/EventSourcing/EventSourcing.csproj", "src/EventSourcing/"]
COPY ["src/Vjs.NegocioCore/Vjs.NegocioCore.csproj", "src/Vjs.NegocioCore/"]
COPY ["src/Vjs.Catalogo.Application/Vjs.Catalogo.Application.csproj", "src/Vjs.Catalogo.Application/"]
COPY ["src/Vjs.Catalogo.DataRepository/Vjs.Catalogo.DataRepository.csproj", "src/Vjs.Catalogo.DataRepository/"]
COPY ["src/Vjs.Catalogo.Domain/Vjs.Catalogo.Domain.csproj", "src/Vjs.Catalogo.Domain/"]
COPY ["src/Vjs.Pagamentos.AntiCorruption/Vjs.Pagamentos.AntiCorruption.csproj", "src/Vjs.Pagamentos.AntiCorruption/"]
COPY ["src/Vjs.Pagamentos.Business/Vjs.Pagamentos.Business.csproj", "src/Vjs.Pagamentos.Business/"]
COPY ["src/Vjs.Pagamentos.DataRepository/Vjs.Pagamentos.DataRepository.csproj", "src/Vjs.Pagamentos.DataRepository/"]
COPY ["src/Vjs.Vendas.Application/Vjs.Vendas.Application.csproj", "src/Vjs.Vendas.Application/"]
COPY ["src/Vjs.Vendas.Domain/Vjs.Vendas.Domain.csproj", "src/Vjs.Vendas.Domain/"]
COPY ["src/Vjs.Vendas.DataRepository/Vjs.Vendas.DataRepository.csproj", "src/Vjs.Vendas.DataRepository/"]
RUN dotnet restore "src/Vjs.MVC/Vjs.MVC.csproj"
COPY . .
WORKDIR "/src/src/Vjs.MVC"
RUN dotnet build "Vjs.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Vjs.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vjs.MVC.dll"]