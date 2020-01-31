FROM registry.cn-hangzhou.aliyuncs.com/x-image/netcore-aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
FROM registry.cn-hangzhou.aliyuncs.com/x-image/netcore-sdk:3.1 AS build
WORKDIR /src
COPY ["Craftsman.Waiter.WebUI/*.csproj","./Craftsman.Waiter.WebUI/"]
COPY ["Craftsman.Waiter.Domain/*.csproj","./Craftsman.Waiter.Domain/"]
COPY ["Craftsman.Waiter.Infrastructure/*.csproj","./Craftsman.Waiter.Infrastructure/"]
COPY ["Framework/Craftsman.Core/*.csproj","./Framework/Craftsman.Core/"]
COPY ["Framework/Craftsman.Core.UnitTest/*.csproj","./Framework/Craftsman.Core.UnitTest/"]
RUN dotnet restore "Craftsman.Waiter.WebUI/Craftsman.Waiter.WebUI.csproj"
COPY . .
WORKDIR "/src/Craftsman.Waiter.WebUI"
RUN dotnet build "Craftsman.Waiter.WebUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Craftsman.Waiter.WebUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Craftsman.Waiter.WebUI.dll"]