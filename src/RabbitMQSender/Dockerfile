FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/RabbitMQSender/RabbitMQSender.csproj src/RabbitMQSender/
RUN dotnet restore src/RabbitMQSender/RabbitMQSender.csproj
COPY . .
WORKDIR /src/src/RabbitMQSender
RUN dotnet build RabbitMQSender.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RabbitMQSender.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RabbitMQSender.dll"]
