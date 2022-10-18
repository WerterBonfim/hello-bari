FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7032
EXPOSE 5060

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloService/Hello.Cmd/Werter.HelloCmd.Api/Werter.HelloCmd.Api.csproj", "HelloService/Hello.Cmd/Werter.HelloCmd.Api/"]
COPY ["HelloService/Hello.Cmd/Werter.HelloCmd.Domain/Werter.HelloCmd.Domain.csproj", "HelloService/Hello.Cmd/Werter.HelloCmd.Domain/"]
COPY ["HelloService/Hello.Cmd/Werter.HelloCmd.Infrastructure/Werter.HelloCmd.Infrastructure.csproj", "HelloService/Hello.Cmd/Werter.HelloCmd.Infrastructure/"]
COPY ["HelloService/CQRS.Core/CQRS.Core.csproj", "HelloService/CQRS.Core/CQRS.Core/"]
COPY ["HelloService/Hello.Common/Hello.Common.csproj", "HelloService/Hello.Common/Hello.Common/"]


RUN dotnet restore "HelloService/Hello.Cmd/Werter.HelloCmd.Api/Werter.HelloCmd.Api.csproj"

COPY . .

WORKDIR "/src"

RUN dotnet build "HelloService/Hello.Cmd/Werter.HelloCmd.Api/Werter.HelloCmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloService/Hello.Cmd/Werter.HelloCmd.Api/Werter.HelloCmd.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Werter.HelloCmd.Api.dll"]
