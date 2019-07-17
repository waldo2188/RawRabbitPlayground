FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/RabbitMQListner/RabbitMQListner.csproj src/RabbitMQListner/
RUN dotnet restore src/RabbitMQListner/RabbitMQListner.csproj
COPY . .
WORKDIR /src/src/RabbitMQListner
RUN dotnet build RabbitMQListner.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RabbitMQListner.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RabbitMQListner.dll"]
