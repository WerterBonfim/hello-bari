FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Werter.HelloQuery.Api/Werter.HelloQuery.Api.csproj", "Werter.HelloQuery.Api/"]
RUN dotnet restore "Werter.HelloQuery.Api/Werter.HelloQuery.Api.csproj"
COPY . .
WORKDIR "/src/Werter.HelloQuery.Api"
RUN dotnet build "Werter.HelloQuery.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Werter.HelloQuery.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Werter.HelloQuery.Api.dll"]
