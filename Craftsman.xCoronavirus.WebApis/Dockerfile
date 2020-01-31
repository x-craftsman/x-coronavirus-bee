FROM registry.cn-hangzhou.aliyuncs.com/x-image/netcore-aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
FROM registry.cn-hangzhou.aliyuncs.com/x-image/netcore-sdk:3.1 AS build
WORKDIR /src
COPY ["Craftsman.xCoronavirus.WebApis/*.csproj","./Craftsman.xCoronavirus.WebApis/"]
COPY ["Craftsman.xCoronavirus.Domain/*.csproj","./Craftsman.xCoronavirus.Domain/"]
COPY ["Craftsman.xCoronavirus.Infrastructure/*.csproj","./Craftsman.xCoronavirus.Infrastructure/"]
COPY ["Framework/Craftsman.Core/*.csproj","./Framework/Craftsman.Core/"]
COPY ["Framework/Craftsman.Core.UnitTest/*.csproj","./Framework/Craftsman.Core.UnitTest/"]
RUN dotnet restore "Craftsman.xCoronavirus.WebApis/Craftsman.xCoronavirus.WebApi.csproj"
COPY . .
WORKDIR "/src/Craftsman.xCoronavirus.WebApis"
RUN dotnet build "Craftsman.xCoronavirus.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Craftsman.xCoronavirus.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV kafka 192.168.100.20
ENTRYPOINT ["dotnet", "Craftsman.xCoronavirus.WebApi.dll"]