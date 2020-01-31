FROM registry.cn-hangzhou.aliyuncs.com/x-image/netcore-aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
FROM registry.cn-hangzhou.aliyuncs.com/x-image/netcore-sdk:3.1 AS build
WORKDIR /src
COPY ["Craftsman.Mercury.WebApis/*.csproj","./Craftsman.Mercury.WebApis/"]
COPY ["Craftsman.Mercury.Domain/*.csproj","./Craftsman.Mercury.Domain/"]
COPY ["Craftsman.Mercury.Infrastructure/*.csproj","./Craftsman.Mercury.Infrastructure/"]
COPY ["Framework/Craftsman.Core/*.csproj","./Framework/Craftsman.Core/"]
COPY ["Framework/Craftsman.Core.UnitTest/*.csproj","./Framework/Craftsman.Core.UnitTest/"]
RUN dotnet restore "Craftsman.Mercury.WebApis/Craftsman.Mercury.WebApi.csproj"
COPY . .
WORKDIR "/src/Craftsman.Mercury.WebApis"
RUN dotnet build "Craftsman.Mercury.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Craftsman.Mercury.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV kafka 192.168.100.20
ENTRYPOINT ["dotnet", "Craftsman.Mercury.WebApi.dll"]