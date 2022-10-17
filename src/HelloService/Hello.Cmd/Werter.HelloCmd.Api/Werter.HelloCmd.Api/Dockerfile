FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Werter.HelloCmd.Api/Werter.HelloCmd.Api.csproj", "Werter.HelloCmd.Api/"]
RUN dotnet restore "Werter.HelloCmd.Api/Werter.HelloCmd.Api.csproj"
COPY . .
WORKDIR "/src/Werter.HelloCmd.Api"
RUN dotnet build "Werter.HelloCmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Werter.HelloCmd.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Werter.HelloCmd.Api.dll"]
