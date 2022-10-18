FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7034
EXPOSE 5064

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloService/Hello.Query/Werter.HelloQuery.Api/", "HelloService/Hello.Query/Werter.HelloQuery.Api/"]
COPY ["HelloService/Hello.Query/Werter.HelloQuery.Infrastructure/", "HelloService/Hello.Query/Werter.HelloQuery.Infrastructure/"]
COPY ["HelloService/CQRS.Core/", "HelloService/CQRS.Core/"]
COPY ["HelloService/Hello.Common/", "HelloService/Hello.Common/"]

RUN dotnet restore "HelloService/Hello.Query/Werter.HelloQuery.Api/Werter.HelloQuery.Api/Werter.HelloQuery.Api.csproj"

WORKDIR "/src"
RUN dotnet build "HelloService/Hello.Query/Werter.HelloQuery.Api/Werter.HelloQuery.Api/Werter.HelloQuery.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloService/Hello.Query/Werter.HelloQuery.Api/Werter.HelloQuery.Api/Werter.HelloQuery.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Werter.HelloQuery.Api.dll"]
